using System;
using laba4_testirov;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test_modul
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void extrapolateTest()
        {
            Form1 f = new Form1();
            double[,] d = { { 1, 70 }, { 3, 75 } };
            double expected = 77.5;
            Assert.AreEqual(expected, f.extrapolate(d, 4));
        }
        [TestMethod]
        public void read_kursTest()
        {
            Form1 f = new Form1();
            double c1, c2, n;
            c1 = 25;
            c2 = 5;
            n = 3;
            double expected = 15;
            double res = f.read_kurs(c1, c2, n);
            Assert.AreEqual(expected, res);

        }
    }
}
