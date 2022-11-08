using System;
using System.IO;

namespace DirectoryScanner.Core
{
    public sealed class DirectoryScanResult : ScanResult
    {
        public DirectoryInfo DirectoryInfo { get; private set; }

        public DirectoryScanResult(string directoryPath) : base()
        {
            if (!Directory.Exists(directoryPath))
                throw new ArgumentException($"Directory does not exists \"{directoryPath}\"");
            this.DirectoryInfo = new DirectoryInfo(directoryPath);
            this.Name = this.DirectoryInfo.Name;
            this.Path = this.DirectoryInfo.FullName;
        }

        public override long GetBytes(bool forceRecalculation = false)
        {
            if (!this._bytesRetrieved || forceRecalculation)
            {
                this._bytes = 0;
                this._bytesRetrieved = true;
                foreach (var result in this.Nodes)
                    if (result != null)
                        this._bytes += result.GetBytes(forceRecalculation);
            }
            return this._bytes;
        }
    }
}
