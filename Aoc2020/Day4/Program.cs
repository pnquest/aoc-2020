using System;
using System.IO;
using System.Linq;
using Pidgin;
using static Pidgin.Parser;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var fieldParser = AnyCharExcept(':', '\n').ManyString().Before(Char(':')).Then(AnyCharExcept(' ', '\n').ManyString(), (f, v) => new PassportField(f, v));
            Parser<char, PassportData> passportParser = fieldParser.SeparatedAndOptionallyTerminated(Whitespace).Map(f => new PassportData(f));
            Parser<char, PassportData[]> passportsParser = passportParser.SeparatedAndOptionallyTerminated(Whitespace).Map(r => r.ToArray());

            PassportData[] passports = passportsParser.ParseOrThrow(File.ReadAllText("./input.txt"));

            Part1(passports);
            Part2(passports);
        }

        private static void Part1(PassportData[] passports)
        {
            int validCount = passports.Count(p => p.AreRequiredFieldsPopulated());

            Console.WriteLine($"Part 1: {validCount} valid passports");
        }

        private static void Part2(PassportData[] passports)
        {
            int validcount = passports.Count(p => p.AreAllFieldsValid());

            Console.WriteLine($"Part 2: {validcount} valid passports");
        }
    }
}
