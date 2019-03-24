using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Cuni.Arithmetics.FixedPoint;

namespace Cuni.Arithmetics.FixedPointBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FixedTests>();
        }
    }
    public class FixedTests
    {
        readonly int i = 20;
        readonly int n = 16;
        readonly Fixed<Q16_16>[,] fixedMatrix;
        readonly FixedStruct<Q16_16>[,] fixedStructMatrix;
        readonly Fixed<Q24_8> q24 = new Fixed<Q24_8>(101) / 2;
        readonly Fixed<Q16_16> q16 = new Fixed<Q16_16>(101) / 2;
        readonly Fixed<Q8_24> q8 = new Fixed<Q8_24>(101) / 2;
        readonly FixedStruct<Q24_8> q24Struct = new FixedStruct<Q24_8>(101) / 2;
        readonly FixedStruct<Q16_16> q16Struct = new FixedStruct<Q16_16>(101) / 2;
        readonly FixedStruct<Q8_24> q8Struct = new FixedStruct<Q8_24>(101) / 2;

        public FixedTests()
        {
            Random random = new Random();
            fixedMatrix = new Fixed<Q16_16>[n, n];
            fixedStructMatrix = new FixedStruct<Q16_16>[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int randomInt = random.Next();
                    fixedMatrix[i, j] = randomInt;
                    fixedStructMatrix[i, j] = randomInt;
                }
            }
        }

        [Benchmark]
        public Fixed<Q24_8> CreateTest()
        {
            return new Fixed<Q24_8>(i);
        }

        [Benchmark]
        public FixedStruct<Q24_8> CreateStructTest()
        {
            return new FixedStruct<Q24_8>(i);
        }

        [Benchmark]
        public Fixed<Q24_8> AddTest()
        {
            return q24 + q24;
        }

        [Benchmark]
        public FixedStruct<Q24_8> AddStructTest()
        {
            return q24Struct + q24Struct;
        }

        [Benchmark]
        public Fixed<Q24_8> SubtractTest()
        {
            return q24 - q24;
        }

        [Benchmark]
        public FixedStruct<Q24_8> SubtractStructTest()
        {
            return q24Struct - q24Struct;
        }

        [Benchmark]
        public Fixed<Q24_8> MultiplyTest()
        {
            return q24 * q24;
        }

        [Benchmark]
        public FixedStruct<Q24_8> MultiplyStructTest()
        {
            return q24Struct * q24Struct;
        }

        [Benchmark]
        public Fixed<Q24_8> DivideTest()
        {
            return q24 / q24;
        }

        [Benchmark]
        public FixedStruct<Q24_8> DivideStructTest()
        {
            return q24Struct / q24Struct;
        }

        [Benchmark]
        public Fixed<Q16_16> ConvertPrecisionTest()
        {
            return (Fixed<Q16_16>)q24;
        }

        [Benchmark]
        public FixedStruct<Q16_16> ConvertPrecisionStructTest()
        {
            return (FixedStruct<Q16_16>)q24Struct;
        }

        [Benchmark]
        public Fixed<Q8_24> AddIntegerTest()
        {
            return q8 + i;
        }

        [Benchmark]
        public FixedStruct<Q8_24> AddIntegerStructTest()
        {
            return q8Struct + i;
        }

        [Benchmark]
        public string ToStringTest()
        {
            return q8.ToString();
        }

        [Benchmark]
        public string ToStringStructTest()
        {
            return q8Struct.ToString();
        }

        [Benchmark]
        public void GaussTest()
        {
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    fixedMatrix[i, k] = fixedMatrix[i, k] / fixedMatrix[k, k];
                    for (int j = 0; j < n; j++)
                    {
                        fixedMatrix[i, j] = fixedMatrix[i, j] - fixedMatrix[i, k] * fixedMatrix[k, j];
                    }
                }
            }
        }

        [Benchmark]
        public void GaussStructTest()
        {
            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    fixedStructMatrix[i, k] = fixedStructMatrix[i, k] / fixedStructMatrix[k, k];
                    for (int j = 0; j < n; j++)
                    {
                        fixedStructMatrix[i,j] = fixedStructMatrix[i,j] - fixedStructMatrix[i, k] * fixedStructMatrix[k, j];
                    }
                }
            }
        }
    }
}
