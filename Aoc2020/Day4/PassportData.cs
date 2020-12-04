using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    public class PassportData
    {
        private Dictionary<string, PassportField> _values;
        private readonly Regex _hairColorRegex = new Regex("^#[0-9a-f]{6}$");
        private readonly Regex _passportIdRegex = new Regex("^[0-9]{9}$");

        public int? BirthYear => GetFieldValue("byr")?.AsInt();
        public int? IssueYear => GetFieldValue("iyr")?.AsInt();
        public int? ExpirationYear => GetFieldValue("eyr")?.AsInt();
        public string Height => GetFieldValue("hgt")?.Value;
        public string HairColor => GetFieldValue("hcl")?.Value;
        public string EyeColor => GetFieldValue("ecl")?.Value;
        public string PassportId => GetFieldValue("pid")?.Value;
        public string CountryId => GetFieldValue("cid")?.Value;

        public PassportData(IEnumerable<PassportField> fields)
        {
            _values = fields.ToDictionary(f => f.FieldName);
        }

        private PassportField GetFieldValue(string fieldName)
        {
            if(_values.TryGetValue(fieldName, out PassportField val))
            {
                return val;
            }

            return null;
        }

        public bool AreRequiredFieldsPopulated()
        {
            return BirthYear.HasValue
                && IssueYear.HasValue
                && ExpirationYear.HasValue
                && Height != null
                && HairColor != null
                && EyeColor != null
                && PassportId != null;
        }

        public bool AreAllFieldsValid()
        {
            return IsBirthYearValid()
                && IsIssueYearValid()
                && IsExpirationYearValid()
                && IsHeightValid()
                && IsHairColorValid()
                && IsEyeColorValid()
                && IsPassportIdValid();
        }

        private bool IsBirthYearValid()
        {
            return BirthYear is not null and >= 1920 and <= 2002;
        }

        private bool IsIssueYearValid()
        {
            return IssueYear is not null and >= 2010 and <= 2020;
        }

        private bool IsExpirationYearValid()
        {
            return ExpirationYear is not null and >= 2020 and <= 2030;
        }

        private bool IsHeightValid()
        {
            if(Height == null || !int.TryParse(Height.Substring(0, Height.Length - 2), out int heightNumber))
            {
                return false;
            }

            if(Height.EndsWith("cm"))
            {
                return heightNumber is >= 150 and <= 193;
            }
            else if(Height.EndsWith("in"))
            {
                return heightNumber is >= 59 and <= 76;
            }

            return false;
        }

        private bool IsHairColorValid()
        {
            return 
                HairColor != null 
                && _hairColorRegex.IsMatch(HairColor);
        }

        private bool IsEyeColorValid()
        {
            return EyeColor is not null
                and ("amb" or "blu" or "brn" or "gry" or "grn" or "hzl" or "oth");
        }

        private bool IsPassportIdValid()
        {
            return PassportId != null && _passportIdRegex.IsMatch(PassportId);
        }
    }

    public record PassportField(string FieldName, string Value)
    {
        public int? AsInt()
        {
            if(int.TryParse(Value, out int v))
            {
                return v;
            }
            return null;
        }
    }
}
