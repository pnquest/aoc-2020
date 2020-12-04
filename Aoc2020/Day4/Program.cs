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
            var fieldParser = AnyCharExcept(':').ManyString().Before(Char(':')).Then(AnyCharExcept(' ', '\n').ManyString(), (f, v) => new PassportField(f, v));
            Parser<char, PassportData> passportParser = fieldParser.SeparatedAndOptionallyTerminated(Whitespace).Map(f => new PassportData(f));

            PassportData[] passports = File.ReadAllText("./input.txt").Split("\n\n").Select(s => passportParser.ParseOrThrow(s)).ToArray();

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
