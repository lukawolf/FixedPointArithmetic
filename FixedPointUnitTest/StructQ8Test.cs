﻿using System;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuni.Arithmetics.FixedPointUnitTest
{
    [TestClass]
    public class StructQ8Test
    {
        int a = 2;
        int b = 32;

        [TestMethod]
        public void CreationTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            double doubleResult = (double)a;
            Assert.AreEqual(doubleResult.ToString(), aFixed.ToString());
        }
        [TestMethod]
        public void CreationFromTooLargeIntTest()
        {
            var q = new FixedStruct<Q8_24>(256);
            Assert.AreEqual("0", q.ToString());
        }

        [TestMethod]
        public void OverflowTest()
        {
            var aFixed = new FixedStruct<Q8_24>(19);
            var bFixed = new FixedStruct<Q8_24>(13);
            var result = aFixed.Multiply(bFixed);
            Assert.AreEqual("-9", result.ToString());
        }

        [TestMethod]
        public void AdditionTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(b);
            var result = aFixed.Add(bFixed);
            double doubleResult = (double)a + (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeAdditionTest()
        {
            var aFixed = new FixedStruct<Q8_24>(-a);
            var bFixed = new FixedStruct<Q8_24>(-b);
            var result = aFixed.Add(bFixed);
            double doubleResult = -(double)a + -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void SubtractionTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(b);
            var result = aFixed.Subtract(bFixed);
            double doubleResult = (double)a - (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void DivisionTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeDivisionTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(-b);
            var result = aFixed.Divide(bFixed);
            double doubleResult = (double)a / -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionByZeroTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(0);
            var result = aFixed.Divide(bFixed);
        }

        [TestMethod]
        public void MultiplicationTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * (double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void NegativeMultiplicationTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(-b);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * -(double)b;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        [TestMethod]
        public void MultiplicationByZeroTest()
        {
            var aFixed = new FixedStruct<Q8_24>(a);
            var bFixed = new FixedStruct<Q8_24>(0);
            var result = aFixed.Multiply(bFixed);
            double doubleResult = (double)a * 0;
            Assert.AreEqual(doubleResult.ToString(), result.ToString());
        }

        //New tests:
        [TestMethod]
        public void ConversionToDoubleTest()
        {
            var q = ((FixedStruct<Q8_24>)103) / 2;
            double d = q;
            Assert.AreEqual(d.ToString(), q.ToString());
        }

        [TestMethod]
        public void ConversionFromDoubleTest()
        {
            var d = 1.75;
            var q = (FixedStruct<Q8_24>)d;
            Assert.AreEqual(d.ToString(), q.ToString());
        }

        [TestMethod]
        public void ConversionFromMorePreciseDoubleTest()
        {
            var d = Math.Pow(2, -25);
            var q = (FixedStruct<Q8_24>)d;
            Assert.AreNotEqual(d.ToString(), q.ToString());
            Assert.AreEqual(q.ToString(), "0");
        }

        [TestMethod]
        public void ConversionFromIntTest()
        {
            var i = 125;
            FixedStruct<Q8_24> q = i;
            Assert.AreEqual(i.ToString(), q.ToString());
        }

        [TestMethod]
        public void ConversionToIntTest()
        {
            var q = ((FixedStruct<Q8_24>)103) / 2;
            var i = 103 / 2;
            Assert.AreEqual(i, (int)q);
        }
        [TestMethod]
        public void ConversionToQ24_8Test()
        {
            var original = new FixedStruct<Q8_24>(123);
            original /= 2;
            var converted = (FixedStruct<Q24_8>)original;
            Assert.AreEqual(original.ToString(), converted.ToString());
        }
        [TestMethod]
        public void ConversionToQ16_16Test()
        {
            var original = new FixedStruct<Q8_24>(123);
            original /= 2;
            var converted = (FixedStruct<Q16_16>)original;
            Assert.AreEqual(original.ToString(), converted.ToString());
        }
    }
}