using System.Collections.Generic;
using System.Linq;

namespace Day2
{
    public class PasswordData
    {
        public int LowerLimit { get; set; }
        public int UpperLimit { get; set; }
        public char TargetChar { get; set; }
        public string Password { get; set; }

        public bool ValidatePasswordPart1()
        {
            Dictionary<char, int> chars = Password.ToCharArray()
                .GroupBy(c => c)
                .ToDictionary(c => c.Key, c => c.Count());

            if(chars.TryGetValue(TargetChar, out int count))
            {
                return count >= LowerLimit && count <= UpperLimit;
            }

            return false;
        }

        public bool ValidatePasswordPart2()
        {
            if(LowerLimit > Password.Length || UpperLimit > Password.Length)
            {
                return false;
            }

            return Password[LowerLimit - 1] == TargetChar ^ Password[UpperLimit - 1] == TargetChar;
        }
    }
}
