using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;

using static System.Environment;

using Greeting = System.String;
using Name = System.String;
using PersonalizedGrerting = System.String;


namespace CodeWars
{
    static class Program
    {
        static void Main(string[] args)
        {
            //DynamicMethod meth = new DynamicMethod("", null, null);
            //ILGenerator il = meth.GetILGenerator();
            //il.EmitWriteLine("Hello, World!");
            //il.Emit(OpCodes.Ret);

            //Action t1 = (Action)meth.CreateDelegate(typeof(Action));
            //t1();


            //var x = (Func<int, int>)MulBy2AndAdd1().CreateDelegate(typeof(Func<int, int>));
            //Console.WriteLine(x(2));

            var aaa = CharFreq_("I like cats");
        }

        public static int GetVowelCount(string str)
        {
            int vowelCount = 0;

            char[] aeiou = new char[] { 'a', 'e', 'i', 'o', 'u' };
            str.ToList().ForEach(c =>
            {
                if (aeiou.ToList().Contains(c))
                {
                    vowelCount++;
                }
            });

            return vowelCount;
        }

        public static IEnumerable<string> FriendOrFoe(string[] names)
        {
            return names.Where(n => n.Count() == 4);
        }

        public static int CountBits(int n)
        {
            return Convert.ToString(n, 2).Count(b => b == '1');
        }

        public static int SquareDigits(int n)
        {
            StringBuilder sb = new StringBuilder();
            n.ToString().ToList().ForEach(c => sb.Append(Math.Pow(Convert.ToInt32(c.ToString()), 2)));

            return Convert.ToInt32(sb.ToString());
        }

        public static int Persistence(long n)
        {
            Func<long, int> func = num =>
                  (int)num.ToString().ToCharArray().Select(c => char.GetNumericValue(c)).Aggregate((total, next) => total * next);

            int result = n >= 10 ? 1 : 0;

            while ((n = func(n)) >= 10) result++;

            return result;
        }

        public static int Find(int[] integers)
        {
            Func<int, int> mod2 = i => i % 2;
            List<int> oddList = new List<int>();
            List<int> evenList = new List<int>();
            Array.ForEach(integers, i => { if (mod2(i) == 1) oddList.Add(i); else evenList.Add(i); });

            return oddList.Count == 1 ? oddList.FirstOrDefault() : evenList.FirstOrDefault();
        }

        public static int Find2(int[] integers)
        {
            var integersOrderBy = integers.OrderBy(x => x % 2 == 1).ToList();

            return (integersOrderBy.Take(2).Sum()) % 2 == 1 ? integersOrderBy.FirstOrDefault() : integersOrderBy.LastOrDefault();
        }

        public static bool Narcissistic(int value)
        {
            int number = 0;
            int temp = value;
            while (temp > 0)
            {
                number += (int)Math.Pow(temp % 10, value.ToString().Length);
                temp /= 10;
            }

            return number == value ? true : false;
        }

        public static int[] SortArray(int[] array)
        {
            var oddIndexList = array.Select((i, index) => new { i, index }).
                                        Where(i => i.i % 2 == 1).ToList();

            var oddIndexListOrderBy = oddIndexList.OrderBy(a => a.i).ToList();

            for (int i = 0; i < oddIndexList.Count(); i++)
            {
                array[oddIndexList[i].index] = oddIndexListOrderBy[i].i;
            }

            return array;
        }

        public static string CreatePhoneNumber(int[] numbers)
            => string.Format("({0}{1}{2}) {3}{4}{5}-{6}{7}{8}{9}", numbers.Select(n => n.ToString()).ToArray());

        public static string GetReadableTime(int seconds)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;

            second = seconds % 60;
            minute = seconds >= 60 ? (seconds % 3600) / 60 : 0;
            hour = seconds >= 3600 ? seconds / 3600 : 0;
            return string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);
        }

        public static string GetReadableTime2(int seconds)
        {
            var total = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:d2}:{1:d2}:{2:d2}", (int)total.TotalHours, total.Minutes, total.Seconds);
        }

        public static int MaxSequence(int[] arr)
        {
            int sum = 0;
            int max = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
                sum = Math.Max(0, sum);
                max = Math.Max(sum, max);
            }

            return max;
        }

        public static bool IsValidWalk(string[] walk)
        {
            if (walk.Length != 10) return false;

            Dictionary<string, int[]> direction = new Dictionary<string, int[]>()
            {
                ["n"] = new int[] { 0, 1 },
                ["s"] = new int[] { 0, -1 },
                ["e"] = new int[] { 1, 0 },
                ["w"] = new int[] { -1, 0 },
            };

            int[] position = new int[] { 0, 0 };
            Array.ForEach(walk, w => { for (int i = 0; i < 2; ++i) position[i] += direction[w][i]; });

            return position.All(p => p == 0);
        }

        public static int GetSum(int a, int b)
                => Enumerable.Range(Math.Min(a, b), Math.Abs(a - b) + 1).Sum();

        public static bool XO(string input)
        {
            int count = 0;
            input.ToList().ForEach(c =>
            {
                if (char.ToLower(c) == 'o') count++;
                else if (char.ToLower(c) == 'x') count--;
            });
            return count == 0 ? true : false;
        }

        public static string Longest(string s1, string s2)
        {
            int[] lettersCount = new int[26];

            Action<string> CountFunc = s => s.ToList().ForEach(c => lettersCount[c - 'a']++);
            CountFunc(s1);
            CountFunc(s2);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 26; i++)
                if (lettersCount[i] != 0)
                    result.Append((char)(i + 'a'));

            return result.ToString();
        }

        public static string Longest2(string s1, string s2)
        {
            return string.Concat(new SortedSet<char>(s1 + s2));
        }

        public static int FindShort(string s)
            => s.Split(" ").Min(ss => ss.Length);

        public static char FindMissingLetter(char[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i + 1] - array[i] != 1)
                {
                    return (char)(array[i] + 1);
                }
            }
            return ' ';
        }

        public static int Litres(double time)
            => (int)(time * 0.5);

        public static string boolToWord(bool word)
            => new Dictionary<bool, string>() { [true] = "Yes", [false] = "No" }[word];

        public static string seriesSum(int n)
        {
            double result = 0;

            for (int i = 1; i <= n; i++)
            {
                if (i == 1) result += 1;
                else if (i == 2) result += 1 / 4.0;
                else result += 1 / (4.0 + (i - 2) * 3);
            }

            return result.ToString("0.00");
        }

        public static bool ValidatePin(string pin)
            => (pin.Length == 4 || pin.Length == 6) && pin.All(p => char.IsDigit(p));

        public static int[] TwoSum(int[] numbers, int target)
        {
            Dictionary<int, int> pairs = new Dictionary<int, int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                int diff = target - numbers[i];
                if (pairs.ContainsKey(diff))
                    return new int[] { pairs[diff], i };
                else
                    pairs[numbers[i]] = i;
            }

            return new int[0];
        }

        public static int[] CountBy(int x, int n)
            => Enumerable.Range(1, n).Select(r => r * x).ToArray();

        public static int CenturyFromYear(int year)
        {
            if ((year % 100) != 0)
                return year / 100 + 1;
            else
                return year / 100;
        }

        public static bool IsSquare(int n)
        {
            int sqrtN = (int)Math.Sqrt(n);

            return sqrtN * sqrtN == n;
        }

        public static bool CorrectTail(string body, string tail)
            => body.Last() == tail[0];

        public static double basicOp(char operation, double value1, double value2)
            => new Dictionary<char, double>()
            {
                ['+'] = value1 + value2,
                ['-'] = value1 - value2,
                ['*'] = value1 * value2,
                ['/'] = value1 / value2,
            }[operation];

        public static string Stringy(int size)
            => string.Join("", Enumerable.Range(1, size).Select(n => n % 2));

        public static long rowSumOddNumbers(long n)
            => n * n * n;

        public static string AddBinary(int a, int b)
        {
            var c = a + b;
            var result = new StringBuilder();
            while (c > 0)
            {
                result.Insert(0, c % 2);
                c /= 2;
            }

            return result.ToString();
        }

        public static double FindAverage(double[] array)
            => array.Average();

        public static bool Check(object[] a, object x)
            => a.Contains(x);

        public static string HighAndLow(string numbers)
        {
            int min = Int32.MaxValue;
            int max = Int32.MinValue;

            string[] nums = numbers.Split();
            for (int i = 0; i < nums.Length; i++)
            {
                int n = Convert.ToInt32(nums[i]);
                if (max < n) max = n;
                if (min > n) min = n;
            }

            return $"{max} {min}";
        }

        public static string PrinterError(String s)
        {
            int errorCount = 0;
            s.ToList().ForEach(x => { if (x > 'm') errorCount++; });

            return $"{errorCount}/{s.Length}";
        }

        public static string PrinterError2(String s)
            => $"{s.Count(x => x > 'm')}/{s.Length}";

        public static string Transpose(string noteName, int transposition)
        {
            Dictionary<string, int> notes = new Dictionary<string, int>
            {
                ["C"] = 0,
                ["C#"] = 1,
                ["D"] = 2,
                ["D#"] = 3,
                ["E"] = 4,
                ["F"] = 5,
                ["F#"] = 6,
                ["G"] = 7,
                ["G#"] = 8,
                ["A"] = 9,
                ["A#"] = 10,
                ["B"] = 11
            };

            int index = (notes[noteName] + transposition) % 12;
            if (index < 0) index += 12;

            return notes.FirstOrDefault(x => x.Value == index).Key;
        }

        public static string RemoveDuplicateWords(string s)
            => string.Join(" ", new HashSet<string>(s.Split()));

        public static String Accum(string s)
            => string.Join("-",
                s.ToList().
                    Select((c, index)
                        => string.Concat(char.ToUpperInvariant(c),
                            new string(char.ToLowerInvariant(c), index))));

        public static string Solution(string str)
            => string.Concat(str.Reverse());

        public static int find_it(int[] seq)
        {
            Dictionary<int, int> pairs = new Dictionary<int, int>();

            for (int i = 0; i < seq.Length; i++)
            {
                if (!pairs.ContainsKey(seq[i])) pairs[seq[i]] = 1;
                else pairs[seq[i]] = pairs[seq[i]] + 1;
            }

            return pairs.Where(p => p.Value % 2 == 1).FirstOrDefault().Key;
        }

        public static bool IsNice(int[] arr)
        {
            if (arr.Length == 0) return false;
            for (int i = 0; i < arr.Length; i++)
                if (!arr.Contains(arr[i] + 1) && !arr.Contains(arr[i] - 1))
                    return false;
            return true;
        }

        public static int[] ReverseSeq(int n)
            => Enumerable.Range(1, n).Reverse().ToArray();

        public static long[] Digitize(long n)
            => Array.ConvertAll(n.ToString().Reverse().ToArray(), c => (long)char.GetNumericValue(c));

        public static long MinValue(int[] a)
        {
            int[] nums = new int[10];

            for (int i = 0; i < a.Length; i++)
            {
                nums[a[i]]++;
            }

            long result = 0;
            for (int i = 0; i < 10; i++)
            {
                if (nums[i] != 0)
                    result = result * 10 + i;
            }

            return result;
        }

        public static int GetAverage(int[] marks)
            => (int)marks.Average();

        public static int CountSheeps(bool[] sheeps)
            => sheeps.Count(s => s == true);

        public static int MatchArrays(int[] v, int[] r)
            => v.Count(r.Contains);

        public static int Solution(int value)
        {
            int[] nums = new int[value];

            int result = 0;
            for (int i = 3; i < value; i += 3)
            {
                result += i;
                nums[i]++;
            }
            for (int i = 5; i < value; i += 5)
            {
                if (nums[i] == 0)
                {
                    result += i;
                }
            }

            return result;
        }

        public static int DontGiveMeFive(int start, int end)
        {
            Func<int, bool> isNotHaveFive = x =>
            {
                string xStr = x.ToString();
                for (int i = 0; i < xStr.Length; i++)
                {
                    if (xStr[i] == '5')
                    {
                        return false;
                    }
                }
                return true;
            };

            return Enumerable.Range(start, end - start + 1).Count(isNotHaveFive);
        }

        public static int MakeNegative(int number)
            => -Math.Abs(number);

        public static string repeatStr(int n, string s)
            => string.Concat(Enumerable.Repeat(s, n));

        public static double[] Multiples(int m, double n)
        {
            double[] result = new double[m];
            for (int i = 0; i < m; i++)
            {
                result[i] = n * (i + 1);
            }

            return result;
        }

        public static string GetMiddle(string s)
        {
            Func<int, bool> isOdd = n => n % 2 == 1;

            if (isOdd(s.Length))
                return s[s.Length / 2].ToString();
            else
                return string.Concat(s[s.Length / 2 - 1], s[s.Length / 2]);
        }

        public static double SumArray(double[] array)
            => array.Sum();

        public static int GetASCII(char c)
            => c;

        public static int Solve(string s)
        {
            string pattern = @"\d+";
            Regex regex = new Regex(pattern);
            var matches = regex.Matches(s);

            int max = 0;
            foreach (Match m in matches)
            {
                int num = Convert.ToInt32(m.Value);
                if (num > max) max = num;
            }

            return max;
        }

        public static string Likes(string[] name)
        {
            string result = string.Empty;

            switch (name.Length)
            {
                case 0:
                    result = "no one likes this";
                    break;
                case 1:
                    result = $"{name[0]} likes this";
                    break;
                case 2:
                    result = $"{name[0]} and {name[1]} like this";
                    break;
                case 3:
                    result = $"{name[0]}, {name[1]} and {name[2]} like this";
                    break;
                default:
                    result = $"{name[0]}, {name[1]} and {name.Length - 2} others like this";
                    break;
            }

            return result;
        }

        public static bool IsPalindrome(object line)
        {
            var lineStr = line.ToString();
            for (int i = 0; i < lineStr.Length / 2; i++)
                if (lineStr[i] != lineStr[lineStr.Length - 1 - i])
                    return false;

            return true;
        }

        public static string SpinWords(string sentence)
            => string.Join(" ", sentence.Split().Select(s => { if (s.Length > 4) return string.Concat(s.Reverse()); else return s; }));

        public static int DuplicateCount(string str)
        {
            var dict = new Dictionary<char, int>();
            str.ToList().ForEach(c =>
                {
                    c = char.ToLowerInvariant(c);
                    if (!dict.ContainsKey(c)) dict[c] = 1;
                    else dict[c] = dict[c] + 1;
                }
            );

            return dict.ToList().Count(x => x.Value > 1);
        }

        public static int DuplicateCount2(string str)
        {
            return str.ToLower().GroupBy(c => c).Where(g => g.Count() > 1).Count();
        }

        public static DynamicMethod MulBy2AndAdd1()
        {
            Type[] args = { typeof(int) };
            DynamicMethod dynamicMethod = new DynamicMethod("", typeof(int), args);
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldc_I4_2);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Add);
            il.Emit(OpCodes.Ret);

            return dynamicMethod;
        }

        public static IEnumerable<T> UniqueInOrder<T>(IEnumerable<T> iterable)
        {
            if (iterable.Count() == 0) yield break;
            var iterableList = iterable.ToList();

            yield return iterableList[0];
            for (int i = 1; i < iterable.Count(); i++)
                if (!iterableList[i].Equals(iterableList[i - 1]))
                    yield return iterableList[i];
        }

        public static string Histogram(int[] results)
        {
            StringBuilder result = new StringBuilder();
            for (int i = results.Length - 1; i >= 0; i--)
            {
                result.Append($"{i + 1}|");
                for (int j = 0; j < results[i]; j++) result.Append($"#");
                if (results[i] > 0) result.Append($@" {results[i]}");
                result.AppendLine();
            }
            return result.ToString();
        }

        public static int[] ArrayDiff(int[] a, int[] b)
            => a.Where(x => !b.Contains(x)).ToArray();

        public static int binaryArrayToNumber(int[] BinaryArray)
        {
            int n = 1;
            int result = 0;
            for (int i = BinaryArray.Length - 1; i >= 0; i--)
            {
                result += BinaryArray[i] * n;
                n *= 2;
            }

            return result;
        }

        public static int binaryArrayToNumber2(int[] bitArray)
        {
            return bitArray.Aggregate(0, (x, y) => x << 1 | y);
        }

        public static int binaryArrayToNumber3(int[] bitArray)
        {
            int result = 0;

            foreach (var bit in bitArray)
            {
                result <<= 1;
                result |= bit;
            }

            return result;
        }

        public static string Rgb(int r, int g, int b)
            => $"{r.ToRgb()}{g.ToRgb()}{b.ToRgb()}";

        public static string NumberToString(int num)
            => num.ToString();

        public static ulong OddCount(ulong n)
            => n / 2;

        public static long SumCubes(int n)
            => (long)Math.Pow(Enumerable.Range(1, n).Sum(), 2);

        public static string dnaToRna(string dna)
            => dna.Replace("T", "U");

        public static bool NegationValue(string str, bool value)
            => str.Length % 2 == 0 ? value : !value;

        public static int NthEven(int n)
            => 2 * n - 2;

        public static int binToDec(string s)
            => Convert.ToInt32(s, 2);

        public static string ReverseWords(string str)
            => string.Join(" ", str.Split().Reverse());

        public static bool BetterThanAverage(int[] ClassPoints, int YourPoints)
            => ClassPoints.Average() < YourPoints;

        public static int factorial(int n)
        {
            if (n == 0 || n == 1) return 1;

            int[] result = new int[n + 1];
            result[0] = result[1] = 1;

            for (int i = 2; i <= n; i++)
                result[i] = i * result[i - 1];

            return result[n];
        }

        public static int Opposite(int number)
            => -number;

        public static int Product(int[] values)
            => values.Aggregate((_, v) => _ * v);

        public static int[] Maps(int[] x)
            => x.Select(v => v * 2).ToArray();

        public static int PositiveSum(int[] arr)
            => arr.Sum(a => a > 0);

        public static int CubeOdd(int[] arr)
            => arr.Where(a => (a & 1) != 0).Sum(s => s * s * s);

        public static int[] MonkeyCount(int n)
            => Enumerable.Range(1, n).ToArray();

        public static int FindSmallestInt(int[] args)
            => args.Min();

        public static char GetGrade(int s1, int s2, int s3)
            => new char[] { 'F', 'F', 'F', 'F', 'F', 'F', 'D', 'C', 'B', 'A', 'A' }[(s1 + s2 + s3) / 3 / 10];

        public static char GetGrade(params int[] grades)
        {
            var dict = new Dictionary<int, char>
            {
                { 90, 'A' },
                { 80, 'B' },
                { 70, 'C' },
                { 60, 'D' },
                { 0, 'F' },
            };

            return dict.First(e => grades.Average() >= e.Key).Value;
        }

        public static int sumTwoSmallestNumbers(int[] numbers)
            => numbers.OrderBy(n => n).Take(2).Sum();

        public static int CountArgs(params object[] obj)
            => obj.Count();

        public static int[] GetEvenNumbers(int[] numbers)
            => numbers.Where(n => (n & 1) == 0).ToArray();

        public static string OddOrEven(int[] array)
            => new string[] { "even", "odd" }[array.Sum() & 1];

        public static int[] DivisibleBy(int[] numbers, int divisor)
            => numbers.Where(n => n % divisor == 0).ToArray();

        public static string NoSpace(string input)
            => input.Replace(" ", "");

        public static bool CheckForFactor(int num, int factor)
            => num % factor == 0;

        public static string greet() => "hello world!";

        public static string Position(char alphabet)
            => $"Position of alphabet: {(alphabet - 'a' + 1)}";

        public static int OddOne(List<int> list)
            => list.Select((v, i) => new { v, i }).FirstOrDefault(x => (x.v & 1) == 1)?.i ?? -1;

        public static int OddOne2(List<int> list) => list.FindIndex(v => v % 2 != 0);

        public static string Solve_(string s)
        {
            int upperCount = s.ToList().Count(char.IsUpper);
            int lowerCount = s.Length - upperCount;

            if (upperCount == lowerCount) return s.ToLowerInvariant();
            else if (upperCount == 1) return s.ToLowerInvariant();
            else if (lowerCount == 1) return s.ToUpperInvariant();
            else return s;
        }

        public static int AdjacentElementsProduct(int[] array)
        {
            int max = int.MinValue;
            for (int i = 0; i < array.Length - 1; i++)
                max = Math.Max(max, array[i] * array[i + 1]);
            return max;
        }

        public static int AdjacentElementsProduct2(int[] array)
            => Enumerable.Range(0, array.Length).Select(i => array[i] * array[i + 1]).Max();

        public static int DigitalRoot(long n)
        {
            while (n > 10) n = Cal(n);
            return (int)n;
        }

        public static long Cal(long n)
        {
            long result = 0;
            while (n > 0)
            {
                result += n % 10;
                n = n / 10;
            }

            return result;
        }

        public static int DigitalRoot2(long n)
        {
            return (int)(1 + (n - 1) % 9);
        }

        public static string ToWeirdCase(string s)
            => string.Join(" ",
                s.Split().Select(
                    x => string.Concat(
                        x.Select(
                            (c, i) => (i & 1) == 0 ? char.ToUpperInvariant(c) : char.ToLowerInvariant(c)))));

        public static string PigIt(string str)
            => string.Join(" ", str.Split().Select(s => $"{s.Substring(1)}{s[0]}ay"));

        public static int[] MoveZeroes(int[] arr)
            => arr.OrderBy(a => a == 0).ToArray();

        public static int summation(int num)
            => Enumerable.Range(1, num).Sum();

        public static string FindNeedle(object[] haystack)
        {
            for (int i = 0; i < haystack.Length; i++)
                if (haystack[i] == null)
                    continue;
                else if (haystack[i].Equals("needle"))
                    return $"found the needle at position {i}";

            return "";
        }

        public static string Disemvowel(string str)
            => new Regex("[aeiouAEIOU]").Replace(str, "");

        public static string Well(string[] x)
        {
            var count = x.Count(s => string.Equals(s, "good", StringComparison.CurrentCultureIgnoreCase));
            return count == 0 ? "Fail!" : count > 2 ? "I smell a series!" : "Publish!";
        }

        public static int ToBinary(int n)
            => Convert.ToInt32(Convert.ToString(n, 2));

        public static int[] Divisors(int n)
        {
            SortedSet<int> set = new SortedSet<int>();
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    set.Add(i);
                    set.Add(n / i);
                }
            }

            return set.Count == 0 ? null : set.ToArray();
        }

        public static string[] StringToArray(string str)
            => str.Split();

        public static bool LogicalCalc(bool[] array, string op)
            => new Dictionary<string, Func<bool[], bool>>()
            {
                ["AND"] = x => x.Aggregate((t, v) => t & v),
                ["OR"] = x => x.Aggregate((t, v) => t | v),
                ["XOR"] = x => x.Aggregate((t, v) => t ^ v)
            }[op](array);

        public static long FindNextSquare(long num)
        {
            double sqrt = Math.Sqrt(num);
            if (sqrt % 1 != 0) return -1;
            else return ((long)sqrt + 1) * ((long)sqrt + 1);
        }

        public static int[] GenerateRange(int min, int max, int step)
            => Enumerable.Range(0, max + 1).Select(n => min + n * step).TakeWhile(n => n <= max).ToArray();

        public static int SumMix(object[] x)
            => x.Sum(o => Convert.ToInt32(o));

        public static int ArrayPlusArray(int[] arr1, int[] arr2)
            => arr1.Sum() + arr2.Sum();

        public static int Grow(int[] x)
            => x.Aggregate((t, v) => t * v);

        public static int MaxMultiply(int divisor, int bound)
            => divisor * (bound / divisor);

        public static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b) a %= b;
                else b %= a;
            }
            return a == 0 ? b : a;
        }

        public static string ToJadenCase(this string phrase)
            => string.Join(" ",
                phrase.Split().Select(s
                    => string.Concat(s.Select((c, i)
                        => i == 0 ? char.ToUpperInvariant(c) : char.ToLowerInvariant(c)))));

        public static string ToJadenCase2(this string phrase)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(phrase);
        }

        public static int Duplicates(int[] a)
        {
            Dictionary<int, int> pairs = new Dictionary<int, int>();
            Array.ForEach(a, x =>
            {
                if (!pairs.ContainsKey(x)) pairs[x] = 1;
                else pairs[x] = pairs[x] + 1;
            });

            return pairs.Sum(p => p.Value / 2);
        }

        public static string greet(string name)
        {
            Func<Greeting, Func<Name, PersonalizedGrerting>> GreetWith =
                gr => nam => $"{gr}, {(nam == "Johnny" ? "my love" : nam)}!";
            return GreetWith("Hello")(name);
        }

        public static int SquareSum(int[] n)
            => n.Sum(a => a * a);

        public static string Greet(string name, string owner)
            => name == owner ? "Hello boss" : "Hello guest";

        public static string CountSheep(int n)
            => string.Concat(Enumerable.Range(1, n).Select(x => $"{x} sheep..."));

        public static int DoubleInteger(int n)
            => n * 2;

        public static string SongDecoder(string input)
            => Regex.Replace(input, "(WUB)+", " ").Trim();

        public static bool Solution(string str, string ending)
            => str.EndsWith(ending);

        public static string Numericals(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            StringBuilder result = new StringBuilder();

            foreach (var c in s)
            {
                if (!dict.ContainsKey(c))
                    dict[c] = 1;
                else
                    dict[c] = dict[c] + 1;

                result.Append(dict[c]);
            }

            return result.ToString();
        }

        public static double[] GetAges(int sum, int difference)
        {
            if (sum < 0 || difference < 0 || sum < difference) return null;
            double a = (sum + difference) / 2.0;
            double b = (sum - difference) / 2.0;

            return new double[] { a, b };
        }

        public static int GetGoals(int laLigaGoals, int copaDelReyGoals, int championsLeagueGoals)
            => laLigaGoals + copaDelReyGoals + championsLeagueGoals;

        public static int[] Get_size(int w, int h, int d)
            => new int[] { 2 * (w * h + w * d + h * d), w * h * d };

        public static int[] CountPositivesSumNegatives(int[] input)
            => input == null || input.Length == 0 ?
            new int[] { } : new int[] { input.Count(i => i > 0), input.Sum(i => i < 0) };

        public static string AbbrevName(string name)
            => string.Join(".", name.Split().Select(n => n.ToUpperInvariant().First()));

        public static int MaxTriSum(int[] a)
            => a.Distinct().OrderBy(x => -x).Take(3).Sum();

        public static int Solve__(string str)
        {
            int max = 0;
            int temp = 0;
            foreach (var s in str)
            {
                if (s.IsVowel())
                {
                    temp += 1;
                    max = Math.Max(max, temp);
                }
                else
                {
                    temp = 0;
                }
            }

            return max;
        }

        public static int[] ArrayLeaders(int[] numbers)
        {
            var sum = numbers.Sum();
            List<int> result = new List<int>();
            foreach (var num in numbers)
                if (num > (sum -= num))
                    result.Add(num);

            return result.ToArray();
        }

        public static bool TidyNumber(int n)
        {
            for (int i = 0; i < n.GetNumOfDigits() - 1; i++)
                if (n.GetSpecificDigit(i) > n.GetSpecificDigit(i + 1))
                    return false;
            return true;
        }

        public static bool TidyNumber2(int n)
            => n.ToString().SequenceEqual(n.ToString().OrderBy(x => x));

        public static BigInteger Choose(int n, int p)
        {
            if (p > n) return 0;

            BigInteger result = new BigInteger(1);
            for (int i = n; i > Math.Max(n - p, p); i--)
            {
                result *= i;
            }
            for (int i = Math.Min(n - p, p); i > 1; i--)
            {
                result /= i;
            }

            return result;
        }

        public static int OtherAngle(int a, int b, int all = 180)
            => all - a - b;

        public static int[] ExtraPerfect(int n)
            => Enumerable.Range(0, (n + 1) / 2).Select(x => 1 + 2 * x).TakeWhile(t => t <= n).ToArray();

        public static string MakeUpperCase(string str)
            => str.ToUpperInvariant();

        public static int[] InvertValues(int[] input)
            => input.Select(i => -i).ToArray();

        public static string Remove_char(string s)
            => s.Remove(0, 1).Remove(s.Length - 2, 1);

        public static int StringToNumber(String str)
            => Convert.ToInt32(str);

        public static string boolean_to_string(bool b)
            => b.ToString();

        public static int StrCount(string str, string letter)
            => str.Count(c => c == char.Parse(letter));

        public static string RemoveExclamationMarks(string s)
            => s.Replace("!", "");

        public static char GetChar(int charcode)
            => Convert.ToChar(charcode);

        public static int Multiply(int x)
            => new Dictionary<int, Func<int, int>>() { [0] = n => n * 8, [1] = n => n * 9 }[x & 1](x);

        public static int HexToDec(string hexString)
            => hexString.IndexOf("-") != -1 ? -Convert.ToInt32(hexString.Replace("-", ""), 16) : Convert.ToInt32(hexString, 16);

        public static int findSum(int n)
            => Enumerable.Range(1, n).Sum(x => x % 3 == 0 || x % 5 == 0);

        public static int DescendingOrder(int num)
            => Convert.ToInt32(string.Concat(num.ToString().Select(char.GetNumericValue).OrderBy(n => n)));

        public static long IpsBetween(string start, string end)
            => start.Split('.').Zip(end.Split('.'), (s, e) => Convert.ToInt16(e) - Convert.ToInt16(s)).
                Aggregate((a, b) => (a << 8) + b);

        public static string SortGiftCode(string code)
            => string.Concat(code.OrderBy(c => c));

        public static int WordsToMarks(string str)
            => str.Aggregate(0, (t, v) => (t + (v - 'a' + 1)));

        public static int Stray(int[] numbers)
            => numbers.Aggregate((t, v) => t ^ v);

        public static void ReverseWords2(string str)
            => string.Join(" ", str.Split().Select(s => string.Concat(s.Reverse())));

        public static string Pattern(int n)
        {
            if (n > 0)
            {
                List<string> result = new List<string>();
                for (int i = 1; i <= n; i++)
                {
                    result.Add(string.Concat(Enumerable.Range(i, n - i + 1)) + string.Concat(Enumerable.Range(1, i - 1)));
                }

                return string.Join(NewLine, result);
            }

            return "";
        }

        public static bool SetAlarm(bool employed, bool vacation)
            => employed && !vacation;

        public static string EvenOrOdd(int number)
            => new List<string>() { "Even", "Odd" }[number & 1];

        public static string Correct(string text)
            => new Dictionary<string, string>() { ["5"] = "S", ["0"] = "O", ["1"] = "I" }.
                Aggregate(text, (current, replacement) => current.Replace(replacement.Key, replacement.Value));

        public static string HowManyDalmatians(int n)
        {
            string[] dogs = new string[]
            {
                "Hardly any",
                "More than a handful!",
                "Woah that's a lot of dogs!",
                "101 DALMATIONS!!!"
            };

            string respond = n <= 10 ? dogs[0]
                           : n <= 50 ? dogs[1]
                           : n == 101 ? dogs[3]
                           : dogs[2];

            return respond;
        }

        public static int CockroachSpeed(double x)
            => (int)(x * 100000 / 3600);

        public static int[] Divisors2(int n)
        {
            List<int> result = new List<int>();
            List<int> resultPair = new List<int>();
            bool isPrime = true;

            for (double i = 2; i * i <= n; i++)
            {
                double temp = 0.0;
                if ((temp = n / i) % 1 == 0)
                {
                    isPrime = false;
                    result.Add((int)i);

                    if (i != temp)
                        resultPair.Add((int)temp);
                }
            }

            for (int i = resultPair.Count - 1; i >= 0; i--)
            {
                result.Add(resultPair[i]);
            }

            return isPrime ? null : result.ToArray();
        }

        public static int[] Divisors2_(int n)
        {
            var divisors = Enumerable.Range(2, (int)Math.Sqrt(n))
              .Where(i => n % i == 0 && i < n)
              .SelectMany(i => new[] { i, n / i })
              .OrderBy(i => i)
              .Distinct()
              .ToArray();
            return divisors.Any() ? divisors : null;
        }

        public static int MinSum(int[] a)
        {
            a = a.OrderBy(x => x).ToArray();
            return Enumerable.Range(0, a.Length / 2).Aggregate(0, (total, next) => total + a[next] * a[a.Length - next - 1]);
        }

        public static int MinSum_(int[] a)
        {
            a = a.OrderBy(x => x).ToArray();
            int count = a.Length / 2;
            return a.Take(count).Zip(a.TakeLast(count).Reverse(), (x, y) => x * y).Sum();
        }

        public static int[] PartsSums(int[] ls)
        {
            List<int> result = new List<int>() { 0 };
            int sum = 0;

            for (int i = ls.Length - 1; i >= 0; i--)
                result.Add(sum += ls[i]);

            return result.AsEnumerable().Reverse().ToArray();
        }

        public static int[] PartsSums_(int[] ls)
            => ls.Reverse().Aggregate(Enumerable.Repeat(0, 1), (l, n) => l.Prepend(l.First() + n)).ToArray();

        public static int NumberOfOccurrences(int x, int[] xs)
            => xs.Count(n => n == x);

        public static bool IsVeryEvenNumber(int number)
        {
            Func<int, bool> isEven = n => (n & 1) == 0;

            //if (!isEven(number)) return false;

            Func<int, int> addEachNumber = n => { int result = 0; while (n > 0) { result += n % 10; n /= 10; } return result; };

            while (number >= 10)
            {
                number = addEachNumber(number);
            }

            if (!isEven(number)) return false;
            else return true;
        }

        public static int MaxNumber(int n)
           => Convert.ToInt32(string.Concat(n.ToString().OrderByDescending(a => a)));

        public static Dictionary<char, int> CharFreq(string message)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();
            message.ToList().ForEach(m =>
            {
                if (!result.ContainsKey(m)) result[m] = 1;
                else result[m] += 1;
            });

            return result;
        }

        public static Dictionary<char, int> CharFreq_(string message)
            => message.GroupBy(m => m).ToDictionary(g => g.Key, g => g.Count());

        public static int[] minMax(int[] lst)
            => new int[] { lst.Min(), lst.Max() };

        public static int CountLettersAndDigits(string input)
            => input.Count(char.IsLetterOrDigit);

        public static string[] Solution2(string str)
        {
            var strLen = str.Length;
            if ((strLen & 1) == 1) str += "_";

            var result = new List<string>();
            for (int i = 0; i < str.Length; i += 2)
            {
                result.Add(new string(str.ToArray(), i, 2));
            }

            return result.ToArray();
        }
    }

    public static class IntExtensions
    {
        public static string ToRgb(this int n)
            => $"{Math.Clamp(n, 0, 255):X2}";

        public static int GetFirstDigit(this int num)
        {
            if (num >= 100000000) num /= 100000000;
            if (num >= 10000) num /= 10000;
            if (num >= 100) num /= 100;
            if (num >= 10) num /= 10;

            return num;
        }

        public static int GetSpecificDigit(this int num, int index)
        {
            int count = num.GetNumOfDigits() - index;
            int result = 0;
            while (count-- > 0)
            {
                result = num % 10;
                num /= 10;
            }

            return result;
        }

        public static int GetNumOfDigits(this int num)
            => (int)Math.Log10(Math.Abs(num)) + 1;

        public static int Move(int position, int roll)
            => position + 2 * roll;
    }

    public static class StringExtensions
    {
        public static bool IsUpperCase(this string Obj)
            => Obj.Any(c => char.IsLower(c));

        public static int Score(this string str)
            => str.Sum(c => char.IsLetter(c) ? char.ToLower(c) - 'a' + 1 : 0);

        public static string CamelCase(this string str)
            => string.Concat(str.Split().Select(s => string.Concat(s.Select((c, i) => i == 0 ? char.ToUpperInvariant(c) : c))));

        public static string CamelCase2(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str).Replace(" ", String.Empty);
        }

        public static bool Vowel(this string s)
            => Regex.IsMatch(s, @"^[aeiou]{1}(?![^/\\#?])$", RegexOptions.IgnoreCase);

    }

    public static class IEnumerableExtensions
    {
        public static int Sum(this IEnumerable<int> source, Func<int, bool> predicate)
        {
            int result = 0;
            foreach (var item in source)
                if (predicate(item))
                    result += item;

            return result;
        }
    }

    public static class CharacterExtentions
    {
        public static bool IsVowel(this char c)
            => "aeiou".IndexOf(c.ToString(), StringComparison.InvariantCultureIgnoreCase) != -1;
    }
}
