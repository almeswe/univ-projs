using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracer.Core.Tests
{
    [TestClass]
    public class MultiThreadedCallTreeTests
    {
        private class TestCallTreeClass
        {
            private ITracer _tracer;

            public TestCallTreeClass(ITracer tracer) =>
                this._tracer = tracer;

            public void Initial()
            {
                var thread1 = new Thread(this.M1);
                thread1.Start();
                thread1.Join();
                var thread2 = new Thread(this.M2);
                thread2.Start();
                thread2.Join();
                this.M3();
            }

            private void M1()
            {
                this._tracer.StartTrace();
                this._tracer.StopTrace();
            }

            private void M2()
            {
                this._tracer.StartTrace();
                this.M4();
                this._tracer.StopTrace();
            }

            private void M3()
            {
                this._tracer.StartTrace();
                this.M5();
                this._tracer.StopTrace();
            }

            private void M4()
            {
                this._tracer.StartTrace();
                this._tracer.StopTrace();
            }

            private void M5()
            {
                this._tracer.StartTrace();
                this._tracer.StopTrace();
            }
        }

        [TestMethod]
        public void TestCallTreeBuildingThreaded()
        {
            var tracer = new Tracer();
            new TestCallTreeClass(tracer).Initial();
            var res = tracer.GetTraceResult();
            var tree = res.Tree;
            Assert.AreEqual(tree.Roots.Count, 3);
            var enumerator = tree.Roots.Keys.GetEnumerator();

            enumerator.MoveNext();
            var id1 = enumerator.Current;
            Assert.AreEqual(tree.Roots[id1][0].MethodName, "M1");
            Assert.AreEqual(tree.Roots[id1][0].Nodes.Count, 0);

            enumerator.MoveNext();
            var id2 = enumerator.Current;
            Assert.AreEqual(tree.Roots[id2][0].MethodName, "M2");
            Assert.AreEqual(tree.Roots[id2][0].Nodes.Count, 1);
            Assert.AreEqual(tree.Roots[id2][0].Nodes[0].MethodName, "M4");

            enumerator.MoveNext();
            var id3 = enumerator.Current;
            Assert.AreEqual(tree.Roots[id3][0].MethodName, "M3");
            Assert.AreEqual(tree.Roots[id3][0].Nodes.Count, 1);
            Assert.AreEqual(tree.Roots[id3][0].Nodes[0].MethodName, "M5");
        }
    }
}
