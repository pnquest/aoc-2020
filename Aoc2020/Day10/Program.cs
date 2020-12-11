using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> items = File
                .ReadAllLines("./input.txt")
                .Select(l => int.Parse(l))
                .ToList();

            items.Insert(0, 0);

            items.Sort();
            items.Add(items.Last() + 3);
            Part1(items);
            Part2(items);
        }

        private static void Part2(List<int> items)
        {
            List<int> distances = new();
            ComputeDistances(items, distances);

            int[] dist = distances.ToArray();
            long score = 1;
            for (int i = 0; i < dist.Length; i++)
            {
                if (i + dist[i] < dist.Length)
                {
                    int first1Index = FirstIndexOf1(dist, i, dist[i]);
                    if ((first1Index - i) > 0)
                    {
                        var slice = dist[i..(i + (first1Index - i) + 1)];
                        if (MatchFlipGroup(slice))
                        {
                            score *= slice.Length switch
                            {
                                4 => 7,
                                3 => 4,
                                2 => 2,
                                _ => throw new Exception("ugh")
                            };
                            i += slice.Length - 1;
                        }
                    }

                }
            }

            Console.WriteLine($"Part 2: {score}");
        }

        private static void ComputeDistances(List<int> items, List<int> distances)
        {
            for (int i = 0; i < items.Count - 1; i++)
            {
                if (i + 3 < items.Count && items[i + 3] - items[i] == 3)
                {
                    distances.Add(3);
                }
                else if (i + 2 < items.Count && items[i + 2] - items[i] == 2)
                {
                    distances.Add(2);
                }
                else
                {
                    distances.Add(1);
                }
            }
        }

        private static int FirstIndexOf1(int[] array, int startIndex, int searchDistance)
        {
            for(int i = startIndex; i <= startIndex + searchDistance; i++)
            {
                if(array[i] == 1)
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool MatchFlipGroup(int[] slice)
        {
            if (slice[^1] == 1)
            {
                for (int j = 1; j < slice.Length; j++)
                {
                    if (j + slice[j] < slice.Length)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private static void Part1(List<int> items)
        {
            int oneOffCount = 0;
            int threeOffCount = 0;
            for (int i = 1; i < items.Count; i++)
            {
                int diff = items[i] - items[i - 1];

                if (diff == 1)
                {
                    oneOffCount++;
                }
                else if (diff == 3)
                {
                    threeOffCount++;
                }
            }

            Console.WriteLine($"Part 1: {oneOffCount} * {threeOffCount} = {oneOffCount * threeOffCount}");
        }
    }
}
