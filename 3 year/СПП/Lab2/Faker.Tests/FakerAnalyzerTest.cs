using Faker.Analyzer;
using Faker.Analyzer.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Faker.Tests
{
    [TestClass]
    public class FakerAnalyzerTest
    {
        private FakerAnalyzer _analyzer 
            = new FakerAnalyzer();

        #region Test1

        [TestMethod]
        public void Test1()
        {
            try
            {
                this._analyzer.Analyze<bool>();
                this._analyzer.Analyze<byte>();
                this._analyzer.Analyze<sbyte>();
                this._analyzer.Analyze<short>();
                this._analyzer.Analyze<ushort>();
                this._analyzer.Analyze<int>();
                this._analyzer.Analyze<uint>();
                this._analyzer.Analyze<string>();
                this._analyzer.Analyze<float>();
                this._analyzer.Analyze<double>();
                this._analyzer.Analyze<long>();
                this._analyzer.Analyze<ulong>();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        #endregion

        #region Test2

        private class TC1a
        {
            public int a;
            public int b { get; set; }
        }

        [TestMethod]
        public void Test2()
        {
            try
            {
                this._analyzer.Analyze<TC1a>();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        #endregion

        #region Test3

        private class TC2b
        {
            public bool a;
            public float b;
        }

        private class TC2a
        {
            public int a;
            public int b { get; set; }
            public TC2b tc2b { get; set; }
        }

        [TestMethod]
        public void Test3()
        {
            try
            {
                this._analyzer.Analyze<TC2a>();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }

        #endregion

        #region Test4

        private class TC3a
        {
            public int a;
            public int b { get; set; }
            public TC3a tc3a;
        }

        [TestMethod]
        public void Test4()
        {
            Assert.ThrowsException<GraphNetCircularDependencyException>(
                () => this._analyzer.Analyze<TC3a>());
        }
        #endregion

        #region Test5

        private class TC4b
        {
            public int a;
            public int b { get; set; }
            public TC4a tc4a;
        }

        private class TC4a
        {
            public int a;
            public int b { get; set; }
            public TC4b tc4b;
        }

        [TestMethod]
        public void Test5()
        {
            Assert.ThrowsException<GraphNetCircularDependencyException>(
                () => this._analyzer.Analyze<TC4a>());
        }

        #endregion

        #region Test6

        private class TC5d
        {
            public int a;
            public int b;
            public TC5b tc5b;
        }

        private class TC5c
        {
            public int a;
            public int b;
            public TC5d tc5d;
        }

        private class TC5b
        {
            public int a;
            public int b { get; set; }
            public TC5c tc5c;
        }

        private class TC5a
        {
            public TC4b tc5b;
        }

        [TestMethod]
        public void Test6()
        {
            Assert.ThrowsException<GraphNetCircularDependencyException>(
                () => this._analyzer.Analyze<TC5a>());
        }

        #endregion
    }
}
