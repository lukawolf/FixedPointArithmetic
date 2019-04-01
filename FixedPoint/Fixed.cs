using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FixedPointUnitTest")]

namespace Cuni.Arithmetics.FixedPoint
{
    [Obsolete("Fixed class is depreciated. Use FixedStruct instead!")]
    public class Fixed<Q>:IEquatable<Fixed<Q>>, IComparable<Fixed<Q>> where Q:IPrecision
    {
        private int raw;
        private static readonly int one; 
        private static readonly int beforeDot;
        private static readonly int afterDot;
        private static readonly Regex precisionRegex = new Regex(@"Q([1-9][0-9]*)_([1-9][0-9]*)", RegexOptions.Compiled);
        static Fixed()
        {
            var match = precisionRegex.Match(typeof(Q).Name);
            if(!match.Success) throw new ArgumentException("Precision needs to follow Q<number>_<number> format");
            beforeDot = int.Parse(match.Groups[1].Value);
            afterDot = int.Parse(match.Groups[2].Value);
            one = 1 << afterDot;
            if (beforeDot + afterDot != 32) throw new ArgumentException("Precision needs to add up to 32 bits");
        }
        private Fixed()
        {

        }
        public Fixed(int value){
            raw = value << afterDot;
        }

        public Fixed<Q> Add(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>();
            toReturn.raw = raw + value.raw;
            return toReturn;
        }

        public Fixed<Q> Subtract(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>();
            toReturn.raw = raw - value.raw;
            return toReturn;
        }

        public Fixed<Q> Multiply(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>();
            toReturn.raw = (int)(((long)raw * (long)value.raw) >> afterDot);
            return toReturn;
        }

        public Fixed<Q> Divide(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>();
            toReturn.raw = (int)(((long)raw << afterDot) / (long)value.raw);
            return toReturn;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Fixed<Q> Abs()
        {
            var toReturn = new Fixed<Q>();
            toReturn.raw = raw;
            if (toReturn.raw < 0) toReturn.raw = -toReturn.raw;
            return toReturn;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return ((double)this).ToString();
        }

        //New stuff:
        //Conversions:

        //Implicit, double is precise
        public static implicit operator double(Fixed<Q> self)
        {
            return (double)self.raw / (double)one;
        }

        //Explicit, we loose some precision
        public static explicit operator Fixed<Q>(double d)
        {
            var toReturn = new Fixed<Q>();
            toReturn.raw = (int)((double)one * d);
            return toReturn;
        }

        //Explicit as we loose decimal precision
        public static explicit operator int(Fixed<Q> self)
        {
            return self.raw >> afterDot;
        }

        //Implicit cast as we are not loosing any decimal precision and the type explicitly states its whole part precision. 
        //This also enables automatic conversion for use in operators +, -, *, /
        public static implicit operator Fixed<Q>(int i)
        {
            return new Fixed<Q>(i);
        }

        //Casts are explicit as we never know what Q is. Even more we will always loose eighter whole or decimal precision
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Fixed<Q24_8>(Fixed<Q> self)
        {
            var toReturn = new Fixed<Q24_8>();
            var afterDotDifference = Fixed<Q24_8>.afterDot - afterDot;
            if(afterDotDifference > 0)         
                toReturn.raw = self.raw << afterDotDifference;
            else
                toReturn.raw = self.raw >> (-afterDotDifference);

            return toReturn;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Fixed<Q16_16>(Fixed<Q> self)
        {
            var toReturn = new Fixed<Q16_16>();
            var afterDotDifference = Fixed<Q16_16>.afterDot - afterDot;
            if (afterDotDifference > 0)
                toReturn.raw = self.raw << afterDotDifference;
            else
                toReturn.raw = self.raw >> (-afterDotDifference);
            return toReturn;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator Fixed<Q8_24>(Fixed<Q> self)
        {
            var toReturn = new Fixed<Q8_24>();
            var afterDotDifference = Fixed<Q8_24>.afterDot - afterDot;
            if (afterDotDifference > 0)
                toReturn.raw = self.raw << afterDotDifference;
            else
                toReturn.raw = self.raw >> (-afterDotDifference);
            return toReturn;
        }

        //Operators:
        public static Fixed<Q> operator +(Fixed<Q> a, Fixed<Q> b)
        {
            return a.Add(b);
        }
        public static Fixed<Q> operator -(Fixed<Q> a, Fixed<Q> b)
        {
            return a.Subtract(b);
        }
        public static Fixed<Q> operator *(Fixed<Q> a, Fixed<Q> b)
        {
            return a.Multiply(b);
        }
        public static Fixed<Q> operator /(Fixed<Q> a, Fixed<Q> b)
        {
            return a.Divide(b);
        }
        public static bool operator ==(Fixed<Q> a, Fixed<Q> b)
        {
            return a.raw == b.raw;
        }
        public static bool operator !=(Fixed<Q> a, Fixed<Q> b)
        {
            return a.raw != b.raw;
        }
        public static bool operator >(Fixed<Q> a, Fixed<Q> b)
        {
            return a.raw > b.raw;
        }
        public static bool operator <(Fixed<Q> a, Fixed<Q> b)
        {
            return a.raw < b.raw;
        }

        //Third change of requirements:
        public override int GetHashCode()
        {
            return raw;
        }

        public override bool Equals(object obj)
        {
            if (obj is Fixed<Q>) return ((Fixed<Q>)obj).raw == this.raw;

            return false;
        }
        bool IEquatable<Fixed<Q>>.Equals(Fixed<Q> other)
        {
            return this.raw == other.raw;
        }

        int IComparable<Fixed<Q>>.CompareTo(Fixed<Q> other)
        {
            return this.raw - other.raw;
        }
    }
}
