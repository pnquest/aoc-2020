using System.Linq;

namespace Day2
{
    public class PasswordData
    {
        public int MinOccurances { get; set; }
        public int MaxOccurances { get; set; }
        public char TargetChar { get; set; }
        public string Password { get; set; }

        public bool ValidatePasswordPart1()
        {
            System.Collections.Generic.Dictionary<char, int> chars = Password.ToCharArray()
                .GroupBy(c => c)
                .ToDictionary(c => c.Key, c => c.Count());

            if(chars.TryGetValue(TargetChar, out int count))
            {
                return count >= MinOccurances && count <= MaxOccurances;
            }

            return false;
        }

        public bool ValidatePasswordPart2()
        {
            if(MinOccurances > Password.Length || MaxOccurances > Password.Length)
            {
                return false;
            }

            return Password[MinOccurances - 1] == TargetChar ^ Password[MaxOccurances - 1] == TargetChar;
        }
    }
}
