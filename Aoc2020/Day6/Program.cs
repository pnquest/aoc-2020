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
                    sum += AggregateGroupMembers(sum, groupMemebers);

                    groupMemebers.Clear();
                }
                else
                {
                    groupMemebers.Add(ConvertMemberToBitmask(line));
                }
            }

            sum += AggregateGroupMembers(sum, groupMemebers);

            Console.WriteLine($"Part 2: {sum}");
        }

        private static int ConvertMemberToBitmask(string line)
        {
            int member = 0;

            foreach (char c in line)
            {
                int offset = c - 'a';

                member |= (int)Math.Pow(2, offset);
            }

            return member;
        }

        private static int AggregateGroupMembers(int sum, List<int> groupMemebers)
        {
            int putTogeter = groupMemebers[0];
            if (groupMemebers.Count > 1)
            {
                foreach (int m in groupMemebers.ToArray()[1..])
                {
                    putTogeter &= m;
                }
            }
            BitArray bits = new(new[] { putTogeter });
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
