# Notes for Fixed arithmetic
## Implementation
The original Fixed was implemented as a class mostly out of reflex and lack of thinking during the late night. As it was already a part of the library, it is still supported in the current version, but should be considered depreciated. As the Fixed itself is immutable, contains only an integer value and otherwise behaves as a value type.

The Q values are coded in the name of a particular class so that it does not have to be instantiated to be used in the construction of Fixed. Furthermore the name is used so that we do not have to create a switch on the type of particular Q and can create our Fixed dynamically. In the Fixed static constructor, which is thankfully run only once per used Q value, we then can check if the precision fits into our internal integer value. The only disadvantage is, that errors are given during runtime. This should be solved by employing reasonable unit tests of the final code using the Fixed class or struct.

If we were ok with instantiating Q, we could for example use overrides to always return our precisions or use static constructors in Q types, which would inherit from another generic Q type. That felt cumbersome to me.

Due to C# doing the sign extension during shifts for us, most of the implementation was straightforward. Only during multiplication and division it was needed to extend our internal raw value to long as not to loose any precision due to the shifts necessary.

Furthermore only explicit conversions are allowed between different Q types, as there is always precision lost. Conversions to double are implicit, as double is more precise and conversions from integer are implicit as the Q type is quite literal in the bits it provides for the whole number part.

## Tests
### Used library
MSTest
### Code coverage
97%
The remaining 3% are in the branches of explicit Q conversions, which are not used by our 3 chosen Q values. But as they are the same as other branches, which are already tested, it should be ok.
### Commentary
The triplicity of tests due to testing different Q values separately is quite redundant, but seeing that I wanted to check all the possibilities and did not want to rework all the tests to be run generically, it is sufficient.
All the tests are furthermore doubled to not only test the Fixed class, which should be considered depreciated (see Implementation and Benchmark), but the new FixedStruct.
## Benchmark
### Used library
BenchmarkDotNet
### Benchmark Results
#### Run config
``` ini
BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.648 (1803/April2018Update/Redstone4)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
Frequency=2156247 Hz, Resolution=463.7688 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
```
#### Results before aggressive inlining
|                     Method |            Mean |       Error |      StdDev |          Median |
|--------------------------- |----------------:|------------:|------------:|----------------:|
|                 CreateTest |     286.7883 ns |   5.7184 ns |   5.3490 ns |     284.7110 ns |
|           CreateStructTest |       0.0000 ns |   0.0000 ns |   0.0000 ns |       0.0000 ns |
|                    AddTest |     280.8919 ns |   0.2267 ns |   0.1893 ns |     280.9646 ns |
|              AddStructTest |       0.0017 ns |   0.0061 ns |   0.0057 ns |       0.0000 ns |
|               SubtractTest |     282.6747 ns |   2.4801 ns |   2.1985 ns |     282.4867 ns |
|         SubtractStructTest |       0.0000 ns |   0.0000 ns |   0.0000 ns |       0.0000 ns |
|               MultiplyTest |     287.1909 ns |   0.2615 ns |   0.2318 ns |     287.1101 ns |
|         MultiplyStructTest |       8.8887 ns |   0.2059 ns |   0.2203 ns |       8.7979 ns |
|                 DivideTest |     293.8257 ns |   0.3782 ns |   0.3159 ns |     293.7364 ns |
|           DivideStructTest |      13.6124 ns |   0.0690 ns |   0.0538 ns |      13.5915 ns |
|       ConvertPrecisionTest |      10.1898 ns |   0.1322 ns |   0.1237 ns |      10.1250 ns |
| ConvertPrecisionStructTest |       9.8461 ns |   0.0918 ns |   0.0859 ns |       9.8067 ns |
|             AddIntegerTest |     560.3147 ns |   0.8079 ns |   0.7162 ns |     560.1603 ns |
|       AddIntegerStructTest |       0.0003 ns |   0.0011 ns |   0.0010 ns |       0.0000 ns |
|               ToStringTest |     195.0640 ns |   0.2914 ns |   0.2583 ns |     195.1005 ns |
|         ToStringStructTest |     199.7360 ns |   0.3942 ns |   0.3494 ns |     199.6724 ns |
|                  GaussTest | 113,062.1648 ns | 247.8704 ns | 193.5209 ns | 113,049.4364 ns |
|            GaussStructTest |     698.7508 ns |   1.1111 ns |   0.9850 ns |     698.9299 ns |

##### Zero measurements
* The method duration is indistinguishable from the empty method duration
 * FixedTests.CreateStructTest: Default
 * FixedTests.AddStructTest: Default
 * FixedTests.SubtractStructTest: Default
 * FixedTests.AddIntegerStructTest: Default

#### Results after aggressive inlining
#### Run config
``` ini
BenchmarkDotNet=v0.11.4, OS=Windows 10.0.17134.648 (1803/April2018Update/Redstone4)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
Frequency=2156247 Hz, Resolution=463.7688 ns, Timer=TSC
  [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
  DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 32bit LegacyJIT-v4.7.3362.0
```
|                     Method |           Mean |       Error |      StdDev |         Median |
|--------------------------- |---------------:|------------:|------------:|---------------:|
|                 CreateTest |    281.3968 ns |   0.2633 ns |   0.2334 ns |    281.3218 ns |
|           CreateStructTest |      0.0158 ns |   0.0120 ns |   0.0106 ns |      0.0104 ns |
|                    AddTest |    278.8529 ns |   0.5097 ns |   0.4519 ns |    278.6581 ns |
|              AddStructTest |      0.0014 ns |   0.0027 ns |   0.0022 ns |      0.0000 ns |
|               SubtractTest |    280.2603 ns |   1.3611 ns |   1.2066 ns |    279.7881 ns |
|         SubtractStructTest |      0.0000 ns |   0.0000 ns |   0.0000 ns |      0.0000 ns |
|               MultiplyTest |    288.3532 ns |   3.0508 ns |   2.7045 ns |    287.2582 ns |
|         MultiplyStructTest |      8.7513 ns |   0.0156 ns |   0.0138 ns |      8.7475 ns |
|                 DivideTest |    293.4669 ns |   1.4844 ns |   1.3159 ns |    293.0790 ns |
|           DivideStructTest |     13.7662 ns |   0.0182 ns |   0.0170 ns |     13.7618 ns |
|       ConvertPrecisionTest |      1.8395 ns |   0.0794 ns |   0.0663 ns |      1.8376 ns |
| ConvertPrecisionStructTest |      0.0128 ns |   0.0012 ns |   0.0011 ns |      0.0132 ns |
|             AddIntegerTest |    562.5470 ns |   6.7699 ns |   6.3326 ns |    559.1229 ns |
|       AddIntegerStructTest |      0.0143 ns |   0.0012 ns |   0.0011 ns |      0.0143 ns |
|         DoubleDivisionTest |      0.0000 ns |   0.0000 ns |   0.0000 ns |      0.0000 ns |
|               ToStringTest |    196.2562 ns |   0.1959 ns |   0.1636 ns |    196.2553 ns |
|         ToStringStructTest |    179.2702 ns |   0.0938 ns |   0.0784 ns |    179.2640 ns |
|                    AbsTest |      1.8286 ns |   0.0982 ns |   0.0919 ns |      1.8457 ns |
|              AbsStructTest |      0.2727 ns |   0.0021 ns |   0.0019 ns |      0.2726 ns |
|                  GaussTest | 92,585.7367 ns | 442.7896 ns | 369.7494 ns | 92,623.9238 ns |
|            GaussStructTest |    709.5328 ns |   3.8019 ns |   3.3703 ns |    708.8673 ns |
|             GaussFloatTest |    496.1457 ns |   0.7358 ns |   0.6144 ns |    495.9207 ns |
|            GaussDoubleTest |    507.2280 ns |   6.0729 ns |   5.6806 ns |    506.6910 ns |

##### Zero measurements
In addition to already existing zero modifiers, the following appeared after aggressive inlining:
* The method duration is indistinguishable from the empty method duration
 * FixedTests.ConvertPrecisionStructTest: Default
 * FixedTests.DoubleDivisionTest: Default

#### Commentary
Aggressive inlining was tried for conversion methods, as their execution times were too similar between struct and class. Especially considering the other struct speedups. A double division test was added as the ToString method runtime seemed too long to me. Furthermore I forgot to add the Abs call test and used aggressive inlining on that method too.

From the final result we can see, that aggressive inlining of the suspiciously long running methods worked in most cases. Only the ToString method itself takes way too long and we can see that this is not caused by the division (hidden in the conversion in code) itself. Thus I would suspect the implementation of Double.ToString. As the method is used only for user output, the problem is minimal.

It is also apparent that using struct was the right way to go for fixed arithmetic numbers. The code speedup itself speaks for itself. As does the fact that the simplest of struct operations are measured as taking no time at all.

As far as the benchmark results are concerned, I would prefer for my fixed point arithmetic to be faster than double. Yet in its implementation the fixed point operations (especially multiplication and division) are complicated enough to slow down execution even beyond floating point math. Still, this library should do better than software implementation of floating point numbers.
