using System;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuni.Arithmetics.FixedPointUnitTest
{
    [TestClass]
    public class StructQ16Test
    {
        int a = 64;
        int b = 256;

        [TestMethod]
        public void CreationTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            double doubleResult = (double)a;
            Assert.AreEqual(doubleResult.ToString(), aFixed.ToString());
        }
        [TestMethod]
        public void CreationFromTooLargeIntTest()
        {
            var q = new FixedStruct<Q16_16>(65536);
            Assert.AreEqual("0", q.ToString());
        }
        [TestMethod]
        public void OverflowTest()
        {
            var aFixed = new FixedStruct<Q16_16>(190);
            var bFixed = new FixedStruct<Q16_16>(260);
            var result = aFixed.Multiply(bFixed);
            Assert.AreEqual("-16136", result.ToString());
        }
        [TestMethod]
        public void AdditionTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(b);
            var result = aFixed.Add(bFixed);
            double doubleResult = (double)a + (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeAdditionTest()
        {
            var aFixed = new FixedStruct<Q16_16>(-a);
            var bFixed = new FixedStruct<Q16_16>(-b);
            var result = aFixed.Add(bFixed);
            double doubleResult = -(double)a + -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void SubtractionTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(b);
            var result = aFixed.Subtract(bFixed);
            double doubleResult = (double)a - (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void DivisionTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeDivisionTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(-b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionByZeroTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(0);
            var result = aFixed.Divide(bFixed);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeMultiplicationTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(-b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void MultiplicationByZeroTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(0);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * 0;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        //New tests:
        [TestMethod]
        public void ConversionToDoubleTest()
        {
            var q = ((FixedStruct<Q16_16>)103) / 2;
            double d = q;
            Assert.AreEqual(d.ToString(), q.ToString());
        }

        [TestMethod]
        public void ConversionFromDoubleTest()
        {
            var d = 1.75;
            var q = (FixedStruct<Q16_16>)d;
            Assert.AreEqual(d.ToString(), q.ToString());
        }

        [TestMethod]
        public void ConversionFromMorePreciseDoubleTest()
        {
            var d = Math.Pow(2, -17);
            var q = (FixedStruct<Q16_16>)d;
            Assert.AreNotEqual(d.ToString(), q.ToString());
            Assert.AreEqual(q.ToString(), "0");
        }

        [TestMethod]
        public void ConversionFromIntTest()
        {
            var i = 125;
            FixedStruct<Q16_16> q = i;
            Assert.AreEqual(i.ToString(), q.ToString());
        }

        [TestMethod]
        public void ConversionToIntTest()
        {
            var q = ((FixedStruct<Q16_16>)103) / 2;
            var i = 103 / 2;
            Assert.AreEqual(i, (int)q);
        }

        [TestMethod]
        public void ConversionToQ24_8Test()
        {
            var original = new FixedStruct<Q16_16>(123);
            original /= 2;
            var converted = (FixedStruct<Q24_8>)original;
            Assert.AreEqual(original.ToString(), converted.ToString());
        }
        [TestMethod]
        public void ConversionToQ8_24Test()
        {
            var original = new FixedStruct<Q16_16>(123);
            original /= 2;
            var converted = (FixedStruct<Q8_24>)original;
            Assert.AreEqual(original.ToString(), converted.ToString());
        }
        [TestMethod]
        public void AbsTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(-a);
            Assert.AreEqual(aFixed.Abs().ToString(), bFixed.Abs().ToString());
        }
        [TestMethod]
        public void GreaterThanTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(b);
            Assert.IsFalse(aFixed > bFixed);
        }
        [TestMethod]
        public void LesserThanTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(b);
            Assert.IsTrue(aFixed < bFixed);
        }
        [TestMethod]
        public void EqualsTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(a);
            Assert.IsTrue(aFixed == bFixed);
        }
        [TestMethod]
        public void DoesNotEqualTest()
        {
            var aFixed = new FixedStruct<Q16_16>(a);
            var bFixed = new FixedStruct<Q16_16>(b);
            Assert.IsTrue(aFixed != bFixed);
        }
    }
}
