using StringFormatting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

namespace StringFormattingTests
{
    [TestClass]
    public class FormatStringTest
    {
        private class Mock
        {
            public int a = 1;
            public int b = 2;
            public List<int> x = new List<int>() { 1, 2, 3 };
            public List<List<int>> y = new List<List<int>>() { 
                new List<int>{ 1, 2, 3 }, new List<int> { 4, 5, 6 } };
        }

        private Mock _mock = new Mock();
        private IStringFormatter _formatter = new StringFormatter();

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest1() =>
            this._formatter.Format("{}", this._mock);

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest2() =>
            this._formatter.Format("{}}", this._mock);

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest3() =>
            this._formatter.Format("{{{}}", this._mock);

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest4() =>
            this._formatter.Format("{{}", this._mock);

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest5() =>
            this._formatter.Format("{", this._mock);

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest6() =>
            this._formatter.Format("}", this._mock);

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest7() =>
            this._formatter.Format("{a} {b} {a} {}", this._mock);

        [TestMethod]
        [ExpectedException(typeof(InvalidStringFormatterException))]
        public void FormatStringTest8() =>
            this._formatter.Format("{{{{{{{{{a}}}}}}}}}}", this._mock);

        [TestMethod]
        public void FormatStringTest9()
        {
            try
            {
                this._formatter.Format("{a}, {b}", this._mock);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void FormatStringTest10()
        {
            try
            {
                this._formatter.Format("{{", this._mock);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void FormatStringTest11()
        {
            try
            {
                this._formatter.Format("}}", this._mock);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void FormatStringTest12()
        {
            try
            {
                this._formatter.Format("{{ssss}}", this._mock);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(FormatterException))]
        public void FormatStringTest13() =>
            this._formatter.Format("{c}", this._mock);

        [TestMethod]
        public void FormatStringTest14()
        {
            Assert.AreEqual("143", this._formatter.Format(
                "{x[0]}{y[1][0]}{y[0][2]}", this._mock));
        }

        [TestMethod]
        public void FormatStringTest15()
        {
            Assert.AreEqual("123", this._formatter.Format(
                "{x[0]}{x[1]}{x[2]}", this._mock));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatterException))]
        public void FormatStringTest16() =>
            this._formatter.Format("{x[a]}", this._mock);
    }
}
