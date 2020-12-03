using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            char[][] map = File.ReadAllLines("./input.txt")
                .Select(s => s.ToCharArray()).ToArray();
            Part1(map);
            Part2(map);
        }

        private static void Part1(char[][] map)
        {
            int treeCount = GetTreeCount(map, 3, 1);
            Console.WriteLine($"Part 1: {treeCount} trees");
        }

        private static void Part2(char[][] map)
        {
            (int Right, int Down)[] pairs = new[]
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            int mult = 1;

            foreach((int Right, int Down) pair in pairs)
            {
                int treeCount = GetTreeCount(map, pair.Right, pair.Down);
                mult *= treeCount;
                Console.WriteLine($"Tree Count for slope {pair.Right} Right {pair.Down} Down is {treeCount}");
            }
            Console.WriteLine();
            Console.WriteLine($"Part 2: {mult}");
        }

        private static int GetTreeCount(char[][] map, int rightSlope, int downSlope)
        {
            int maxHeight = map.Length;
            int maxWidth = map[0].Length;

            int curHeight = 0;
            int curWidth = 0;
            int treeCount = 0;

            while (curHeight < maxHeight)
            {
                if (map[curHeight][curWidth % maxWidth] == '#')
                {
                    treeCount++;
                }

                curWidth += rightSlope;
                curHeight += downSlope;
            }

            return treeCount;
        }
    }
}
