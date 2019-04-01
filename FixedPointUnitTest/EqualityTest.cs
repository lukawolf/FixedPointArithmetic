using System;
using Cuni.Arithmetics.FixedPoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cuni.Arithmetics.FixedPointUnitTest
{
    [TestClass]
    public class EqualityTest
    {
        int a = 2;
        int b = 32;

        [TestMethod]
        public void EqualsTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(a);

            Assert.IsTrue(aFixed.Equals(bFixed));
        }

        [TestMethod]
        public void NotEqualsTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);

            Assert.IsFalse(aFixed.Equals(bFixed));
        }

        [TestMethod]
        public void HashEqualityTest()
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(a);

            Assert.AreEqual(aFixed.GetHashCode(), bFixed.GetHashCode());
        }

        [TestMethod]
        //We want to test this as we know that the hash is in fact the raw value of our Fixed. Otherwise it would be a bad idea as collisions are a thing
        public void HashInEqualityTest()        
        {
            var aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);

            Assert.AreNotEqual(aFixed.GetHashCode(), bFixed.GetHashCode());
        }

        [TestMethod]
        public void EquitableTest()
        {
            IEquatable<Fixed<Q8_24>> aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(a);

            Assert.IsTrue(aFixed.Equals(bFixed));
        }

        [TestMethod]
        public void NotEquitableTest()
        {
            IEquatable<Fixed<Q8_24>> aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);

            Assert.IsFalse(aFixed.Equals(bFixed));
        }

        [TestMethod]
        public void ComparableEqualsTest()
        {
            IComparable<Fixed<Q8_24>> aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(a);

            Assert.IsTrue(aFixed.CompareTo(bFixed) == 0);
        }

        [TestMethod]
        public void ComparableLesserTest()
        {
            IComparable<Fixed<Q8_24>> aFixed = new Fixed<Q8_24>(a);
            var bFixed = new Fixed<Q8_24>(b);

            Assert.IsTrue(aFixed.CompareTo(bFixed) < 0);
        }

        [TestMethod]
        public void ComparableGreaterTest()
        {
            IComparable<Fixed<Q8_24>> aFixed = new Fixed<Q8_24>(b);
            var bFixed = new Fixed<Q8_24>(a);

            Assert.IsTrue(aFixed.CompareTo(bFixed) > 0);
        }
    }
}
