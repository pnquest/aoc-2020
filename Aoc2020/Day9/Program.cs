using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] values = File.ReadAllLines("./input.txt")
                .Select(l => long.Parse(l))
                .ToArray();
            Part1(values);
            Part2(values);
        }

        private static void Part2(long[] values)
        {
            const long numberToFind = 27911108;

            long result = LocateMatch(values, numberToFind);
            Console.WriteLine($"Part 2: {result}");
        }

        private static void Part1(long[] values)
        {
            long result = LocateMismatch(values, 25);

            Console.WriteLine($"Part 1: {result}");
        }

        private static long LocateMatch(long[] values, long numberToFind)
        {
            for (int i = 0; i < values.Length - 1; i++)
            {
                long sum = values[i];
                long min = values[i];
                long max = values[i];

                for (int j = i + 1; j < values.Length; j++)
                {
                    sum += values[j];

                    if(values[j] < min)
                    {
                        min = values[j];
                    }

                    if(values[j] > max)
                    {
                        max = values[j];
                    }

                    if (sum == numberToFind)
                    {
                        return min + max;
                    }
                    
                    if(sum > numberToFind)
                    {
                        break;
                    }
                }
            }

            return -1;
        }

        private static long LocateMismatch(long[] values, int numbersBack)
        {
            List<long> sorted = new(values[..numbersBack]);
            sorted.Sort();

            for (int i = numbersBack; i < values.Length; i++)
            {
                if (i > numbersBack)
                {
                    sorted.Remove(values[i - numbersBack - 1]);
                    sorted.Add(values[i - 1]);
                    sorted.Sort();
                }

                if (!FindSum(sorted, values[i]))
                {
                    return values[i];
                }
            }

            return -1;
        }

        private static bool FindSum(List<long> sorted, long sumToFind)
        {
            int lower = 0;
            int upper = sorted.Count - 1;

            while(lower != upper)
            {
                long sum = sorted[lower] + sorted[upper];

                if(sum > sumToFind)
                {
                    upper--;
                }
                else if(sum < sumToFind)
                {
                    lower++;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
