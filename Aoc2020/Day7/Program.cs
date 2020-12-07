using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Pidgin;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Bag> bags = new();
            ParseRules(bags);

            Bag gold = bags["shiny gold"];
            Part1(gold);
            Part2(gold);
        }

        private static void Part2(Bag gold)
        {
            int answer = gold.EnumerateChildren().Sum(c => c.Item1 * c.Item3);
            Console.WriteLine($"Part 2: {answer}");
        }

        private static void Part1(Bag gold)
        {
            int answer = gold.EnumerateParents().Select(p => p.Color).Distinct().Count();
            Console.WriteLine($"Part 1: {answer}");
        }

        private static void ParseRules(Dictionary<string, Bag> bags)
        {
            Parser<char, string> colorParser = Any.AtLeastOnceUntil(Try(String(" bag")).Then(Char('s').Optional())).Map(s => new string(s.ToArray()));
            Parser<char, (string c, int n)> bagCountParser = Num.Before(Char(' ')).Then(colorParser, (n, c) => (c, n));
            Parser<char, IEnumerable<(string, int)>> emptyParser = String("no other bags").ThenReturn(Enumerable.Empty<(string, int)>());
            Parser<char, Bag> ruleParser = colorParser.Before(String(" contain ")).Then(emptyParser.Or(bagCountParser.Separated(String(", "))), (c, r) => CreateBag(bags, c, r)).Before(Char('.'));
            Parser<char, IEnumerable<Bag>> rulesParser = ruleParser.SeparatedAndOptionallyTerminated(Char('\n'));

            rulesParser.ParseOrThrow(File.ReadAllText("./input.txt"));
        }

        private static Bag CreateBag(Dictionary<string, Bag> existing, string color, IEnumerable<(string, int)> bags)
        {
            Bag parent = GetOrCreateBag(existing, color);

            foreach((string clr, int count) in bags)
            {
                if(clr != null)
                {
                    Bag child = GetOrCreateBag(existing, clr);
                    parent.Contents.Add(child, count);
                    child.ContainedBy.Add(parent);
                }
            }

            return parent;
        }

        private static Bag GetOrCreateBag(Dictionary<string, Bag> existing, string color)
        {
            if(existing.TryGetValue(color, out Bag exists))
            {
                return exists;
            }

            Bag create = new(color);
            existing[color] = create;
            return create;
        }
    }
}
