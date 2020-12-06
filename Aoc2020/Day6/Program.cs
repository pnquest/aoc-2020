using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("./input.txt");
            Part1(lines);
            Part2(lines);
        }

        private static void Part2(string[] lines)
        {
            int sum = 0;
            List<int> groupMemebers = new();
            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    sum += AggregateGroupMembers(groupMemebers);

                    groupMemebers.Clear();
                }
                else
                {
                    groupMemebers.Add(ConvertMemberToBitmask(line));
                }
            }

            sum += AggregateGroupMembers(groupMemebers);

            Console.WriteLine($"Part 2: {sum}");
        }

        private static int ConvertMemberToBitmask(string line)
        {
            return line
                .Select(c => (int)Math.Pow(2, c - 'a'))
                .Aggregate(0, (a, i) => a | i);
        }

        private static int AggregateGroupMembers(List<int> groupMemebers)
        {
            int aggregated = groupMemebers
                .Aggregate(int.MaxValue, (a, i) => a & i);

            BitArray bits = new(new[] { aggregated });
            return bits.Cast<bool>().Count(bits => bits);
        }

        private static void Part1(string[] lines)
        {
            int sum = 0;
            HashSet<char> group = new();
            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    sum += group.Count;
                    group.Clear();
                }
                else
                {
                    foreach (char c in line)
                    {
                        group.Add(c);
                    }
                }
            }
            //add last group to sum
            sum += group.Count;

            Console.WriteLine($"Part 1: {sum}");
        }
    }
}
