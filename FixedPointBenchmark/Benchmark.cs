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
        readonly int n = 8;
        readonly Fixed<Q16_16>[][] fixedMatrix;
        readonly FixedStruct<Q16_16>[][] fixedStructMatrix;
        readonly Fixed<Q24_8> q24 = new Fixed<Q24_8>(101) / 2;
        readonly Fixed<Q16_16> q16 = new Fixed<Q16_16>(101) / 2;
        readonly Fixed<Q8_24> q8 = new Fixed<Q8_24>(101) / 2;
        readonly FixedStruct<Q24_8> q24Struct = new FixedStruct<Q24_8>(101) / 2;
        readonly FixedStruct<Q16_16> q16Struct = new FixedStruct<Q16_16>(101) / 2;
        readonly FixedStruct<Q8_24> q8Struct = new FixedStruct<Q8_24>(101) / 2;

        public FixedTests()
        {
            Random random = new Random();
            fixedMatrix = new Fixed<Q16_16>[n][];
            fixedStructMatrix = new FixedStruct<Q16_16>[n][];
            for (int i = 0; i < n; i++)
            {
                fixedMatrix[i] = new Fixed<Q16_16>[n];
                fixedStructMatrix[i] = new FixedStruct<Q16_16>[n];
                for (int j = 0; j < n; j++)
                {
                    int randomInt = random.Next(1, 15);
                    fixedMatrix[i][j] = randomInt;
                    fixedStructMatrix[i][j] = randomInt;
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
        public double DoubleDivisionTest()
        {
            return 123.4 / 432.1;
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
        public Fixed<Q8_24> AbsTest()
        {
            return q8.Abs();
        }

        [Benchmark]
        public FixedStruct<Q8_24> AbsStructTest()
        {
            return q8Struct.Abs();
        }

        [Benchmark]
        public void GaussTest()
        {
            var row = 0;
            var col = 0;
            while (row < n && col < n)
            {
                var maxRow = row;
                for (int i = row; i < n; i++)               
                    if (fixedMatrix[i][col].Abs() > fixedMatrix[maxRow][col].Abs()) maxRow = i;
                
                if (fixedMatrix[maxRow][col] == 0)
                {
                    col++;
                    continue;
                }

                var helperRow = fixedMatrix[row];
                fixedMatrix[row] = fixedMatrix[maxRow];
                fixedMatrix[maxRow] = helperRow;

                /* Do for all rows below pivot: */
                for (int i = row + 1; i < n; i++) {
                    var f = fixedMatrix[i][col] / fixedMatrix[row][col];
                    fixedMatrix[i][col] = 0;
                    for (int j = col + 1; j < n; j++)
                        fixedMatrix[i][j] = fixedMatrix[i][j] - fixedMatrix[row][j] * f;
                }

              row++;
              col++;
            }
        }

        [Benchmark]
        public void GaussStructTest()
        {
            var row = 0;
            var col = 0;
            while (row < n && col < n)
            {
                var maxRow = row;
                for (int i = row; i < n; i++)
                    if (fixedStructMatrix[i][col].Abs() > fixedStructMatrix[maxRow][col].Abs()) maxRow = i;

                if (fixedStructMatrix[maxRow][col] == 0)
                {
                    col++;
                    continue;
                }

                var helperRow = fixedStructMatrix[row];
                fixedStructMatrix[row] = fixedStructMatrix[maxRow];
                fixedStructMatrix[maxRow] = helperRow;

                /* Do for all rows below pivot: */
                for (int i = row + 1; i < n; i++)
                {
                    var f = fixedStructMatrix[i][col] / fixedStructMatrix[row][col];
                    fixedStructMatrix[i][col] = 0;
                    for (int j = col + 1; j < n; j++)
                        fixedStructMatrix[i][j] = fixedStructMatrix[i][j] - fixedStructMatrix[row][j] * f;
                }

                row++;
                col++;
            }
        }
    }
}
