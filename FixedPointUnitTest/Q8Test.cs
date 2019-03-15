using System;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuni.Arithmetics.FixedPointUnitTest
{
    [TestClass]
    public class Q8Test
    {
        int a = 2;
        int b = 32;

        [TestMethod]
        public void CreationTest()
        {
            var aFixed = new Fixed<Q24_8>(a);
            double doubleResult = (double)a;
            Assert.AreEqual(doubleResult.ToString(), aFixed.ToString());
        }

        [TestMethod]
        public void OverflowTest()
        {
            var aFixed = new Fixed<Q8_24>(19);
            var bFixed = new Fixed<Q8_24>(13);
            var result = aFixed.Multiply(bFixed);
            Assert.AreEqual("-9", result.ToString());
        }

        [TestMethod]
        public void AdditionTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);
            var result = aFixed.Add(bFixed);
            double doubleResult = (double)a + (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeAdditionTest()
        {
            var aFixed = new Fixed<Q8_24>(-a);
            var bFixed = new Fixed<Q8_24>(-b);
            var result = aFixed.Add(bFixed);
            double doubleResult = -(double)a + -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void SubtractionTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);
            var result = aFixed.Subtract(bFixed);
            double doubleResult = (double)a - (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void DivisionTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeDivisionTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(-b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionByZeroTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(0);
            var result = aFixed.Divide(bFixed);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeMultiplicationTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(-b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void MultiplicationByZeroTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(0);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * 0;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }
    }
}
