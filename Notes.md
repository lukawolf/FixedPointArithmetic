# Notes for Fixed arithmetic
## Implementation
The original Fixed was implemented as a class mostly out of reflex and lack of thinking during the late night. As it was already a part of the library, it is still supported in the current version, but should be considered depreciated. As the Fixed itself is immutable, contains only an int value and otherwise behaves as a ValueType.

The Q values are coded in the name of a particular class so that it does not have to be instantiated to be used in the construction of Fixed. Furthermore the name is used so that we do not have to create a switch on the type of particular Q and can create our Fixed dynamically.

If we were ok with instantiating Q, we could for example use overrides to always return our precisions or use static constructors in Q types, which would inherit from another generic Q type. That felt cumbersome to me.

Due to C# doing the sign extension during shifts for us, most of the implementation was straightforward. Only during multiplication and division it was needed to extend our internal raw value to long as not to loose any precision due to the shifts necessary.

Furthermore only explicit conversions are allowed between different Q types, as there is always precision lost. Conversions to double are implicit, as double is more precise and conversions from int are implicit as the Q type is quite literal in the bits it provides for the whole number part.
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
#### Results
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

#### Zero measurements
* The method duration is indistinguishable from the empty method duration
 * FixedTests.CreateStructTest: Default
 * FixedTests.AddStructTest: Default
 * FixedTests.SubtractStructTest: Default
 * FixedTests.AddIntegerStructTest: Default

#### Outliers
* FixedTests.CreateStructTest: Default
 * 1 outlier  was  removed
* FixedTests.AddTest: Default
 * 2 outliers were removed, 4 outliers were detected
* FixedTests.MultiplyStructTest: Default
 * 3 outliers were removed
* FixedTests.DivideTest: Default
 * 2 outliers were removed

#### Commentary
//TODO
