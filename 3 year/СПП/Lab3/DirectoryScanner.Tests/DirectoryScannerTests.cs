using DirectoryScanner.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DirectoryScanner.Tests
{
    [TestClass]
    public sealed class DirectoryScannerTests
    {
        [TestMethod]
        public void TestMethod()
        {
            var scanner = new Scanner(@"TestFolder");
            scanner.Finished += (s) =>
            {
                Assert.AreEqual(s.GetBytes(true), 312_288);
            };
            scanner.Start();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var scanner = new Scanner(@"Mathcad 14");
            scanner.Finished += (s) =>
            {
                Assert.AreEqual(s.GetBytes(true), 142_748_709);
            };
            scanner.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod3() =>
            new Scanner(@"123:\");
    }
}
