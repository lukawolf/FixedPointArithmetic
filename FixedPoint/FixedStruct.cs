using System;
using System.Text.RegularExpressions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("FixedPointUnitTest")]

namespace Cuni.Arithmetics.FixedPoint
{
    public struct FixedStruct<Q> where Q:IPrecision
    {
        private int raw;
        private static readonly int one; 
        private static readonly int beforeDot;
        private static readonly int afterDot;
        private static readonly Regex precisionRegex = new Regex(@"Q([1-9][0-9]*)_([1-9][0-9]*)", RegexOptions.Compiled);
        static FixedStruct()
        {
            var match = precisionRegex.Match(typeof(Q).Name);
            if(!match.Success) throw new ArgumentException("Precision needs to follow Q<number>_<number> format");
            beforeDot = int.Parse(match.Groups[1].Value);
            afterDot = int.Parse(match.Groups[2].Value);
            one = 1 << afterDot;
            if (beforeDot + afterDot != 32) throw new ArgumentException("Precision needs to add up to 32 bits");
        }

        public FixedStruct(int value){
            raw = value << afterDot;
        }

        public FixedStruct<Q> Add(FixedStruct<Q> value)
        {
            FixedStruct<Q> toReturn;
            toReturn.raw = raw + value.raw;
            return toReturn;
        }

        public FixedStruct<Q> Subtract(FixedStruct<Q> value)
        {
            FixedStruct<Q> toReturn;
            toReturn.raw = raw - value.raw;
            return toReturn;
        }

        public FixedStruct<Q> Multiply(FixedStruct<Q> value)
        {
            FixedStruct<Q> toReturn;
            toReturn.raw = (int)(((long)raw * (long)value.raw) >> afterDot);
            return toReturn;
        }

        public FixedStruct<Q> Divide(FixedStruct<Q> value)
        {
            FixedStruct<Q> toReturn;
            toReturn.raw = (int)(((long)raw << afterDot) / (long)value.raw);
            return toReturn;
        }

        public override string ToString()
        {
            return ((double)this).ToString();
        }

        //New stuff:
        //Conversions:

        //Implicit, double is precise
        public static implicit operator double(FixedStruct<Q> self)
        {
            return (double)self.raw / (double)one;
        }

        //Explicit, we loose some precision
        public static explicit operator FixedStruct<Q>(double d)
        {
            FixedStruct<Q> toReturn;
            toReturn.raw = (int)((double)one * d);
            return toReturn;
        }

        //Explicit as we loose decimal precision
        public static explicit operator int(FixedStruct<Q> self)
        {
            return self.raw >> afterDot;
        }

        //Implicit cast as we are not loosing any decimal precision and the type explicitly states its whole part precision. 
        //This also enables automatic conversion for use in operators +, -, *, /
        public static implicit operator FixedStruct<Q>(int i)
        {
            return new FixedStruct<Q>(i);
        }

        //Casts are explicit as we never know what Q is. Even more we will always loose eighter whole or decimal precision
        public static explicit operator FixedStruct<Q24_8>(FixedStruct<Q> self)
        {
            FixedStruct<Q24_8> toReturn;
            var afterDotDifference = FixedStruct<Q24_8>.afterDot - afterDot;
            if(afterDotDifference > 0)         
                toReturn.raw = self.raw << afterDotDifference;
            else
                toReturn.raw = self.raw >> (-afterDotDifference);

            return toReturn;
        }
        public static explicit operator FixedStruct<Q16_16>(FixedStruct<Q> self)
        {
            FixedStruct<Q16_16> toReturn;
            var afterDotDifference = FixedStruct<Q16_16>.afterDot - afterDot;
            if (afterDotDifference > 0)
                toReturn.raw = self.raw << afterDotDifference;
            else
                toReturn.raw = self.raw >> (-afterDotDifference);
            return toReturn;
        }
        public static explicit operator FixedStruct<Q8_24>(FixedStruct<Q> self)
        {
            FixedStruct<Q8_24> toReturn;
            var afterDotDifference = FixedStruct<Q8_24>.afterDot - afterDot;
            if (afterDotDifference > 0)
                toReturn.raw = self.raw << afterDotDifference;
            else
                toReturn.raw = self.raw >> (-afterDotDifference);
            return toReturn;
        }

        //Operators:
        public static FixedStruct<Q> operator +(FixedStruct<Q> a, FixedStruct<Q> b)
        {
            return a.Add(b);
        }
        public static FixedStruct<Q> operator -(FixedStruct<Q> a, FixedStruct<Q> b)
        {
            return a.Subtract(b);
        }
        public static FixedStruct<Q> operator *(FixedStruct<Q> a, FixedStruct<Q> b)
        {
            return a.Multiply(b);
        }
        public static FixedStruct<Q> operator /(FixedStruct<Q> a, FixedStruct<Q> b)
        {
            return a.Divide(b);
        }
    }
}
