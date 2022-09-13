using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tracer.Core.Tests
{
    [TestClass]
    public class SingleThreadedCallTreeTests
    {
        private class TestCallTreeClassBasic1
        {
            private ITracer _tracer;

            public TestCallTreeClassBasic1(ITracer tracer) =>
                this._tracer = tracer;

            public void Initial()
            {
                this._tracer.StartTrace();
                Thread.Sleep(200);
                this.M1();
                this._tracer.StopTrace();
            }

            private void M1()
            {
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void TestCallTreeBuildingBasic1()
        {
            var tracer = new Tracer();
            new TestCallTreeClassBasic1(tracer).Initial();
            var res = tracer.GetTraceResult();
            var tree = res.Tree;
            Assert.AreEqual(tree.Roots.Count, 1);
            var enumerator = tree.Roots.Keys.GetEnumerator();
            enumerator.MoveNext();
            var id = enumerator.Current;
            Assert.AreEqual(tree.Roots[id][0].MethodName, "Initial");
            Assert.AreEqual(tree.Roots[id][0].Nodes.Count, 0);
        }

        private class TestCallTreeClassBasic2
        {
            private ITracer _tracer;

            public TestCallTreeClassBasic2(ITracer tracer) =>
                this._tracer = tracer;

            public void Initial()
            {
                this._tracer.StartTrace();
                Thread.Sleep(200);
                this.M1();
                this._tracer.StopTrace();
            }

            private void M1()
            {
                this._tracer.StartTrace();
                Thread.Sleep(100);
                this._tracer.StopTrace();
            }
        }

        [TestMethod]
        public void TestCallTreeBuildingBasic2()
        {
            var tracer = new Tracer();
            new TestCallTreeClassBasic2(tracer).Initial();
            var res = tracer.GetTraceResult();
            var tree = res.Tree;
            Assert.AreEqual(tree.Roots.Count, 1);
            var enumerator = tree.Roots.Keys.GetEnumerator();
            enumerator.MoveNext();
            var id = enumerator.Current;
            Assert.AreEqual(tree.Roots[id][0].MethodName, "Initial");
            Assert.AreEqual(tree.Roots[id][0].Nodes.Count, 1);
            Assert.AreEqual(tree.Roots[id][0].Nodes[0].MethodName, "M1");
        }

        private class TestCallTreeClassBasic3
        {
            private ITracer _tracer;

            public TestCallTreeClassBasic3(ITracer tracer) =>
                this._tracer = tracer;

            public void Initial()
            {
                this._tracer.StartTrace();
                Thread.Sleep(200);
                this.M1();
                this.M3();
                this.M2();
                this._tracer.StopTrace();
            }

            private void M1()
            {
                this._tracer.StartTrace();
                Thread.Sleep(100);
                this._tracer.StopTrace();
            }

            private void M2()
            {
                this._tracer.StartTrace();
                Thread.Sleep(100);
                this._tracer.StopTrace();
            }

            private void M3()
            {
                this._tracer.StartTrace();
                Thread.Sleep(100);
                this._tracer.StopTrace();
            }
        }

        [TestMethod]
        public void TestCallTreeBuildingBasic3()
        {
            var tracer = new Tracer();
            new TestCallTreeClassBasic3(tracer).Initial();
            var res = tracer.GetTraceResult();
            var tree = res.Tree;
            Assert.AreEqual(tree.Roots.Count, 1);
            var enumerator = tree.Roots.Keys.GetEnumerator();
            enumerator.MoveNext();
            var id = enumerator.Current;
            Assert.AreEqual(tree.Roots[id][0].MethodName, "Initial");
            Assert.AreEqual(tree.Roots[id][0].Nodes.Count, 3);
            Assert.AreEqual(tree.Roots[id][0].Nodes[0].MethodName, "M1");
            Assert.AreEqual(tree.Roots[id][0].Nodes[1].MethodName, "M3");
            Assert.AreEqual(tree.Roots[id][0].Nodes[2].MethodName, "M2");
        }

        private class TestCallTreeClassBasic4
        {
            private ITracer _tracer;

            public TestCallTreeClassBasic4(ITracer tracer) =>
                this._tracer = tracer;

            public void Initial()
            {
                this.M1();
                this.M2();
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
                this.M5();
                this._tracer.StopTrace();
            }

            private void M3()
            {
                this._tracer.StartTrace();
                this.M4();
                this.M4();
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
        public void TestCallTreeBuildingBasic4()
        {
            var tracer = new Tracer();
            new TestCallTreeClassBasic4(tracer).Initial();
            var res = tracer.GetTraceResult();
            var tree = res.Tree;
            Assert.AreEqual(tree.Roots.Count, 1);
            var enumerator = tree.Roots.Keys.GetEnumerator();
            enumerator.MoveNext();
            var id = enumerator.Current;
            Assert.AreEqual(tree.Roots[id][0].MethodName, "M1");
            Assert.AreEqual(tree.Roots[id][0].Nodes.Count, 0);
            Assert.AreEqual(tree.Roots[id][1].MethodName, "M2");
            Assert.AreEqual(tree.Roots[id][1].Nodes.Count, 1);
            Assert.AreEqual(tree.Roots[id][1].Nodes[0].MethodName, "M5");
            Assert.AreEqual(tree.Roots[id][2].Nodes.Count, 2);
            Assert.AreEqual(tree.Roots[id][2].Nodes[0].MethodName, "M4");
            Assert.AreEqual(tree.Roots[id][2].Nodes[1].MethodName, "M4");
        }
    }
}
