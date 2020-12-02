using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace Day2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string[] lines = await File.ReadAllLinesAsync("./input.txt");
            var parser = Map(
                (f, _, s, _, c, _, _, p) => new PasswordData 
                    { 
                        LowerLimit = f, 
                        UpperLimit = s, 
                        TargetChar = c, 
                        Password = p 
                    },
                Num,
                Char('-'),
                Num,
                Whitespace,
                Any,
                Char(':'),
                Whitespace,
                Any.ManyString());

            PasswordData[] parsed = lines
                            .Select(l => parser.ParseOrThrow(l))
                            .ToArray();

            int validCount1 = parsed
                .Count(c => c.ValidatePasswordPart1());

            Console.WriteLine($"Part 1: {validCount1}");

            int validCount2 = parsed.Count(c => c.ValidatePasswordPart2());

            Console.WriteLine($"Part 2: {validCount2}");
        }
    }
}
