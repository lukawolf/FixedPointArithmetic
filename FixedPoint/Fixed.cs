using System;
using System.Text.RegularExpressions;

namespace Cuni.Arithmetics.FixedPoint
{
    public class Fixed<Q> where Q:IPrecision
    {
        private long raw;
        private static long one; 
        private static int beforeDot;
        private static int afterDot;
        private static Regex precisionRegex = new Regex(@"Q([1-9][0-9]*)_([1-9][0-9]*)", RegexOptions.Compiled);
        static Fixed()
        {
            var match = precisionRegex.Match(typeof(Q).Name);
            if(!match.Success) throw new ArgumentException("Precision needs to follow Q<number>_<number> format");
            beforeDot = int.Parse(match.Groups[1].Value);
            afterDot = int.Parse(match.Groups[2].Value);
            one = ((long)1) << afterDot;
            if (beforeDot + afterDot != 32) throw new ArgumentException("Precision needs to add up to 32 bits");
        }
        public Fixed(int value){
            raw = value << afterDot;
        }

        public Fixed<Q> Add(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>(0);
            toReturn.raw = raw + value.raw;
            return toReturn;
        }

        public Fixed<Q> Subtract(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>(0);
            toReturn.raw = raw - value.raw;
            return toReturn;
        }

        public Fixed<Q> Multiply(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>(0);
            toReturn.raw = (raw * value.raw) >> afterDot;
            toReturn.raw = (toReturn.raw << 32) >> 32; //To simulate overflow as specified in email
            return toReturn;
        }

        public Fixed<Q> Divide(Fixed<Q> value)
        {
            var toReturn = new Fixed<Q>(0);
            toReturn.raw = (raw << afterDot) / value.raw;
            return toReturn;
        }

        public override string ToString()
        {
            double toPrint = (double)raw / (double)one;
            return toPrint.ToString();
        }
    }
}
