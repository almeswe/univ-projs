using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace DirectoryScanner.Core
{
    public sealed class Scanner
    {
        private int _counter;
        private DirectoryScanResult _root;
        private ConcurrentQueue<ScannerWorkerData> _queue;

        private object _mutex;
        private Semaphore _semaphore;

        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;

        public delegate void ScannerFinished(ScanResult res);
        public delegate void ScannerWorkerFinished(Exception e);

        public event ScannerFinished Finished;
        public event ScannerWorkerFinished WorkerFinished;

        public bool IsFinished { get; private set; }
        public Stopwatch Watch { get; private set; }

        public Scanner(string rootPath)
        {
            this._counter = 0;
            this.IsFinished = true;
            this.Watch = new Stopwatch();
            this.InitializePrimitives();
            this.InitializeTokenSource();
            this.InitializeEventHandlers();
            this._root = new DirectoryScanResult(rootPath);
            this._queue = new ConcurrentQueue<ScannerWorkerData>();
        }

        public void Start() =>
            this.Dispatch();

        public void Stop() =>
            this._tokenSource.Cancel();

        private void OnWorkerFinished(Exception e)
        {
            if (this._queue.Count == 0 && this._counter == 0)
                this.IsFinished = true;
        }

        private void Dispatch()
        {
            this.IsFinished = false;
            var data = new ScannerWorkerData(this._root.Path, null);
            this._queue.Enqueue(data);
            this.Watch.Start();
            while (!this.IsFinished)
                if (this._queue.TryDequeue(out data))
                    this.InitializeWorker(data);
            this.Watch.Stop();
            this.Finished?.Invoke(this._root);
        }

        private void InitializePrimitives()
        {
            this._mutex = new object();
            int maxThreads, minThreads;
            ThreadPool.GetMinThreads(out minThreads, out _);
            maxThreads = Environment.ProcessorCount*2;
            maxThreads = Math.Max(maxThreads, minThreads);
            Console.WriteLine($"max threads: {maxThreads}");
            this._semaphore = new Semaphore(maxThreads, maxThreads);
        }

        private void InitializeTokenSource()
        {
            this._tokenSource = new CancellationTokenSource();
            this._token = this._tokenSource.Token;
        }

        private void InitializeEventHandlers()
        {
            this.WorkerFinished += this.OnWorkerFinished;
        }

        private void InitializeWorker(ScannerWorkerData data)
        {
            try
            {
                this._semaphore.WaitOne();
                Interlocked.Increment(ref this._counter);
                ThreadPool.QueueUserWorkItem(this.ScanDirectory, data);
            }
            catch (NotSupportedException nse)
            {
                this.ReleaseWorker(nse);
            }
        }

        private void ReleaseWorker(Exception e)
        {
            this._semaphore.Release();
            Interlocked.Decrement(ref this._counter);
            this.WorkerFinished?.Invoke(e);
        }

        private void ScanDirectory(object args)
        {
            var data = (ScannerWorkerData)args;
            var root = data.ScanResult;
            var directoryResult = new DirectoryScanResult(data.Path);
            lock (this._mutex)
            {
                if (root == null)
                    this._root.AppendNode(directoryResult);
                else
                {
                    root?.AppendNode(directoryResult);
                }
            }
            try
            {
                foreach (var file in directoryResult.DirectoryInfo.EnumerateFiles())
                {
                    if (this._token.IsCancellationRequested)
                        break;
                    directoryResult.AppendNode(new FileScanResult(file));
                }
            }
            catch (ArgumentException ae) { }
            catch (DirectoryNotFoundException dnfe) { }
            catch (UnauthorizedAccessException uae) { }
            try
            {
                foreach (var directory in directoryResult.DirectoryInfo.EnumerateDirectories())
                {
                    if (this._token.IsCancellationRequested)
                        break;
                    this._queue.Enqueue(new ScannerWorkerData(
                        directory.FullName, directoryResult));
                }
            }
            catch (ArgumentException ae) { }
            catch (DirectoryNotFoundException dnfe) { }
            catch (UnauthorizedAccessException uae) { }
            this.ReleaseWorker(null);
        }
    }
}
