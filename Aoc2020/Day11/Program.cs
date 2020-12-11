using System;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        private static void Part2()
        {
            char[][] rows;
            int maxHeight, maxWidth;
            GetPuzzleData(out rows, out maxHeight, out maxWidth);

            bool isChanged;
            int occupiedCount;
            do
            {
                char[][] newRows = ComputeNextStage(rows, maxHeight, maxWidth, GetRayTracingOccupiedCount, 5, out isChanged, out occupiedCount);

                rows = newRows;

            } while (isChanged);

            Console.WriteLine($"Part 2: {occupiedCount}");
        }

        private static void Part1()
        {
            char[][] rows;
            int maxHeight, maxWidth;
            GetPuzzleData(out rows, out maxHeight, out maxWidth);

            bool isChanged;
            int occupiedCount;
            do
            {
                char[][] newRows = ComputeNextStage(rows, maxHeight, maxWidth, GetAdjacentOccupiedCount, 4, out isChanged, out occupiedCount);

                rows = newRows;

            } while (isChanged);

            Console.WriteLine($"Part 1: {occupiedCount}");
        }

        private static void GetPuzzleData(out char[][] rows, out int maxHeight, out int maxWidth)
        {
            rows = File.ReadAllLines("./input.txt").Select(l => l.ToCharArray()).ToArray();
            maxHeight = rows.Length;
            maxWidth = rows[0].Length;
        }

        private static char[][] ComputeNextStage(
            char[][] rows,
            int maxHeight,
            int maxWidth,
            Func<char[][], int, int, int, int, int> adjacentSeatCountDelegate,
            int adjacentSeatTolerance,
            out bool isChanged,
            out int occupiedCount)
        {
            isChanged = false;
            occupiedCount = 0;

            char[][] newRows = new char[maxHeight][];
            for (int y = 0; y < maxHeight; y++)
            {
                newRows[y] = new char[maxWidth];
                for (int x = 0; x < maxWidth; x++)
                {
                    char newChar = rows[y][x] switch
                    {
                        'L' when adjacentSeatCountDelegate(rows, x, y, maxWidth, maxHeight) == 0 => '#',
                        '#' when adjacentSeatCountDelegate(rows, x, y, maxWidth, maxHeight) >= adjacentSeatTolerance => 'L',
                        _ => rows[y][x]
                    };

                    if (rows[y][x] != newChar)
                    {
                        isChanged = true;
                    }
                    if (newChar == '#')
                    {
                        occupiedCount++;
                    }
                    newRows[y][x] = newChar;
                }
            }

            return newRows;
        }

        private static int GetRayTracingOccupiedCount(char[][] rows, int x, int y, int maxX, int maxY)
        {
            Span<int> steps = stackalloc[] { -1, 0, 1 };
            int res = 0;
            foreach(int yOffset in steps)
            {
                foreach(int xOffset in steps)
                {
                    if(yOffset != 0 || xOffset != 0)
                    {
                        int lookX = x;
                        int lookY = y;
                        do
                        {
                            lookX += xOffset;
                            lookY += yOffset;
                        } while (IsInBounds(lookX, lookY, maxX, maxY) && rows[lookY][lookX] == '.');

                        if(IsInBounds(lookX, lookY, maxX, maxY) && rows[lookY][lookX] == '#')
                        {
                            res++;
                        }
                    }
                }
            }

            return res;
        }

        private static bool IsInBounds(int x, int y, int maxX, int maxY)
        {
            return x >= 0
                && x < maxX
                && y >= 0
                && y < maxY;
        }

        private static int GetAdjacentOccupiedCount(char[][] rows, int x, int y, int maxX, int maxY)
        {
            Span<int> steps = stackalloc[] { -1, 0, 1 };
            int res = 0;
            foreach(int yOffset in steps)
            {
                foreach(int xOffset in steps)
                {
                    int lookX = x + xOffset;
                    int lookY = y + yOffset;
                    if((yOffset != 0 || xOffset != 0)
                        && IsInBounds(lookX, lookY, maxX, maxY)
                        && rows[lookY][lookX] == '#')
                    {
                        res++;
                    }
                }
            }

            return res;
        }
    }
}
