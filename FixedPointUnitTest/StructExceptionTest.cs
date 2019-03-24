using System;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuni.Arithmetics.FixedPointUnitTest
{
    [TestClass]
    public class StructExceptionTest
    {
        class Q8_8 : IPrecision { }
        class Q_1_2 : IPrecision { }

        [TestMethod]
        [ExpectedException(typeof(TypeInitializationException))]
        public void WrongPrecisionFormatTest()
        {
            var test = new FixedStruct<Q_1_2>(1);
        }


        [TestMethod]
        [ExpectedException(typeof(TypeInitializationException))]
        public void WrongPrecisionSumTest()
        {
            var test = new FixedStruct<Q8_8>(1);
        }
    }
}
