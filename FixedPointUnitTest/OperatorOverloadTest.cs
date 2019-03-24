using System;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuni.Arithmetics.FixedPointUnitTest
{
    [TestClass]
    public class OperatorOverloadTest
    {
        int a = 125;
        int b = 10;  
        [TestMethod]
        public void PlusTest()
        {
            var q1 = new Fixed<Q24_8>(a);
            var q2 = new Fixed<Q24_8>(b);
            var result = q1 + q2;
            Assert.AreEqual(((double)a + b).ToString(), result.ToString());
        }

        [TestMethod]
        public void MinusTest()
        {
            var q1 = new Fixed<Q24_8>(a);
            var q2 = new Fixed<Q24_8>(b);
            var result = q1 - q2;
            Assert.AreEqual(((double)a - b).ToString(), result.ToString());
        }

        [TestMethod]
        public void TimesTest()
        {
            var q1 = new Fixed<Q24_8>(a);
            var q2 = new Fixed<Q24_8>(b);
            var result = q1 * q2;
            Assert.AreEqual(((double)a * b).ToString(), result.ToString());
        }

        [TestMethod]
        public void SlashTest()
        {
            var q1 = new Fixed<Q24_8>(a);
            var q2 = new Fixed<Q24_8>(b);
            var result = q1 / q2;
            Assert.AreEqual(((double)a / b).ToString(), result.ToString());
        }
    }
}
