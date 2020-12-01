using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int[] ints = (await File.ReadAllLinesAsync("./input.txt"))
                .Select(l => int.Parse(l))
                .ToArray();

            Part1(ints);
            Part2(ints);
        }

        private static void Part1(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                for (int j = i + 1; j < ints.Length; j++)
                {
                    int firstNumber = ints[i];
                    int secondNumber = ints[j];
                    if (firstNumber + secondNumber == 2020)
                    {
                        Console.WriteLine($"Part 1: {firstNumber} * {secondNumber} = {firstNumber * secondNumber}");
                        return;
                    }
                }
            }
        }

        private static void Part2(int[] ints)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                for(int j = i + 1; j < ints.Length; j++)
                {
                    for(int k = j + 1; k < ints.Length; k++)
                    {
                        int firstNumber = ints[i];
                        int secondNumber = ints[j];
                        int thirdNumber = ints[k];

                        if(firstNumber + secondNumber + thirdNumber == 2020)
                        {
                            Console.WriteLine($"Part 2: {firstNumber} * {secondNumber} * {thirdNumber} = {firstNumber * secondNumber * thirdNumber}");
                            return;
                        }
                    }
                }
            }
        }
    }
}
