using System.Threading;

namespace Tracer.Core.Example
{
    public sealed class TestClass
    {
        private ITracer _tracer;

        public TestClass(ITracer tracer) =>
            this._tracer = tracer;

        public void M0()
        {
            var thread1 = new Thread(this.M4);
            thread1.Start();
            thread1.Join();
            var thread2 = new Thread(this.M10);
            thread2.Start();
            thread2.Join();
            this.M1();
            this.M2();
        }

        private void M1()
        {
            this._tracer.StartTrace();
            Thread.Sleep(100);
            this.M3();
            this._tracer.StopTrace();
        }

        private void M2()
        {
            this._tracer.StartTrace();
            Thread.Sleep(200);
            this._tracer.StopTrace();
        }

        private void M3()
        {
            this._tracer.StartTrace();
            Thread.Sleep(300);
            this._tracer.StopTrace();
        }

        private void M4()
        {
            this._tracer.StartTrace();
            Thread.Sleep(400);
            this.M5();
            this._tracer.StopTrace();
        }

        private void M5()
        {
            this._tracer.StartTrace();
            Thread.Sleep(500);
            this._tracer.StopTrace();
        }

        private void M10()
        {
            this._tracer.StartTrace();
            Thread.Sleep(1000);
            this._tracer.StopTrace();
        }
    }
}
