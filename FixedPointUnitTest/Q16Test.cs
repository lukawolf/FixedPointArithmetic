using System;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuni.Arithmetics.FixedPointUnitTest
{
    [TestClass]
    public class Q16Test
    {
        int a = 64;
        int b = 256;

        [TestMethod]
        public void CreationTest()
        {
            var aFixed = new Fixed<Q24_8>(a);
            double doubleResult = (double)a;
            Assert.AreEqual(doubleResult.ToString(), aFixed.ToString());
        }

        [TestMethod]
        public void AdditionTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(b);
            var result = aFixed.Add(bFixed);
            double doubleResult = (double)a + (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeAdditionTest()
        {
            var aFixed = new Fixed<Q16_16>(-a);
            var bFixed = new Fixed<Q16_16>(-b);
            var result = aFixed.Add(bFixed);
            double doubleResult = -(double)a + -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void SubtractionTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(b);
            var result = aFixed.Subtract(bFixed);
            double doubleResult = (double)a - (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void DivisionTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeDivisionTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(-b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionByZeroTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(0);
            var result = aFixed.Divide(bFixed);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeMultiplicationTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(-b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void MultiplicationByZeroTest()
        {
            var aFixed = new Fixed<Q16_16>(a);
            var bFixed = new Fixed<Q16_16>(0);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * 0;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }
    }
}
