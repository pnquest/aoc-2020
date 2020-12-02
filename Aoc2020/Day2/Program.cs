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

            Parser<char, PasswordData> parser = Num.Then(Char('-'), (f, _) => new PasswordData { MinOccurances = f })
                                 .Then(Num, (d, n) => { d.MaxOccurances = n; return d; })
                                 .Then(Whitespace, (d, _) => d)
                                 .Then(Any, (d, c) => { d.TargetChar = c; return d; })
                                 .Then(Char(':'), (d, _) => d)
                                 .Then(Whitespace, (d, _) => d)
                                 .Then(Any.ManyString(), (d, p) => { d.Password = p; return d; });

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
