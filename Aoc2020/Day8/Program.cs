using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            IInstruction[] instructions = File
                .ReadAllLines("./input.txt")
                .Select(ParseLine)
                .ToArray();

            Part1(instructions);
            Part2(instructions);
        }

        private static void Part2(IInstruction[] instructions)
        {
            int instr = 0;
            int acc = 0;
            HashSet<int> visited = new();
            int flippedIndex = -1;
            while (instr < instructions.Length)
            {
                instr = 0;
                acc = 0;
                visited.Clear();

                //flip back
                if (flippedIndex >= 0)
                {
                    FlipInstruction(instructions, ref flippedIndex);
                }

                flippedIndex++;

                FlipInstruction(instructions, ref flippedIndex);

                while (instr < instructions.Length && visited.Add(instr))
                {
                    IInstruction cur = instructions[instr];
                    cur.Execute(ref instr, ref acc);
                }
            }

            Console.WriteLine($"Part 2: Acc = {acc}");
        }

        private static void FlipInstruction(IInstruction[] stack, ref int index)
        {
            while(stack[index] is AccInstruction)
            {
                index++;
            }

            stack[index] = stack[index].FlipInstruction();
        }

        private static void Part1(IInstruction[] instructions)
        {
            int instr = 0;
            int acc = 0;
            HashSet<int> visited = new();

            while (visited.Add(instr))
            {
                IInstruction cur = instructions[instr];
                cur.Execute(ref instr, ref acc);
            }

            Console.WriteLine($"Part 1: Acc = {acc}");
        }

        private static IInstruction CreateInstruction(string instr, int amount)
        {
            return instr switch
            {
                "acc" => new AccInstruction(amount),
                "jmp" => new JmpInstruction(amount),
                "nop" => new NopInstruction(amount),
                _ => throw new ArgumentException(nameof(instr))
            };
        }

        private static IInstruction ParseLine(string line)
        {
            string[] split = line.Split(' ');
            return CreateInstruction(split[0], int.Parse(split[1]));
        }
    }
}
