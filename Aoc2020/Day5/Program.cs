using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] passes = File.ReadAllLines("./input.txt");
            Part1(passes);
            Part2(passes);
        }

        private static void Part1(string[] passes)
        {
            int maxId = 0;
            foreach (string pass in passes)
            {
                int id = ComputeSeatId(pass);

                if (id > maxId)
                {
                    maxId = id;
                }
            }

            Console.WriteLine($"Part 1: Max Id: {maxId}");
        }

        private static void Part2(string[] passes)
        {
            List<int> ids = passes.Select(ComputeSeatId).ToList();
            ids.Sort();

            for(int i = 0; i < ids.Count - 1; i++)
            {
                if(ids[i] != ids[i + 1] - 1)
                {
                    Console.WriteLine($"Part 2: My Seat ID is {ids[i] + 1}");
                    return;
                }
            }
        }

        private static int ComputeSeatId(string pass)
        {
            BinaryPartitionResolver rowResolver = new(0, 127);
            foreach (char c in pass[0..7])
            {
                BinaryPartitionResolver.PartitionHalf half = c switch
                {
                    'F' => BinaryPartitionResolver.PartitionHalf.Lower,
                    'B' => BinaryPartitionResolver.PartitionHalf.Upper,
                    _ => throw new ArgumentException($"{c} is not valid")
                };

                rowResolver.Partition(half);
            }

            int row = rowResolver.Result.Value;

            BinaryPartitionResolver colResolver = new(0, 7);
            foreach (char c in pass[7..])
            {
                BinaryPartitionResolver.PartitionHalf half = c switch
                {
                    'L' => BinaryPartitionResolver.PartitionHalf.Lower,
                    'R' => BinaryPartitionResolver.PartitionHalf.Upper,
                    _ => throw new ArgumentException($"{c} is not valid")
                };

                colResolver.Partition(half);
            }

            int col = colResolver.Result.Value;

            int id = row * 8 + col;
            return id;
        }
    }
}
