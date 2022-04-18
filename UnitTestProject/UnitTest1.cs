using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(DLL.DLL.SaleCost(10, 0),0.15);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(DLL.DLL.SaleCost(5, 0), 0.10);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(DLL.DLL.SaleCost(3, 0), 0.05);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(DLL.DLL.SaleCost(2, 0), 0.00);
        }
        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(DLL.DLL.SaleCost(2, 500), 0.01);
        }
        [TestMethod]
        public void TestMethod6()
        {
            Assert.AreEqual(DLL.DLL.SaleCost(2, 999), 0.01);
        }
        [TestMethod]
        public void TestMethod7()
        {
            Assert.ThrowsException<AssertFailedException>(()=>(Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => DLL.DLL.SaleCost(-1, -1))));
        }
        [TestMethod]
        public void TestMethod8()
        {
            Assert.ThrowsException<AssertFailedException>(() => (Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => DLL.DLL.SaleCost(0, 0))));
        }
        [TestMethod]
        public void TestMethod9()
        {
            Assert.ThrowsException<AssertFailedException>(() => (Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => DLL.DLL.SaleCost(0, 500.5))));
        }
        [TestMethod]
        public void TestMethod10()
        {
            Assert.AreEqual(DLL.DLL.SaleCost(2, 500.5), 0.01);
        }
    }
}
