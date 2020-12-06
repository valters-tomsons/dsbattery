using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UPower
{
    public static class UPowerParser
    {
        private const string PercentageLookup = "percentage:";
        private const string IconLookup = "icon-name:";

        public static int GetPercentage(ICollection<string> stringOutput)
        {
            var percentageOutput = stringOutput.Single(x => x.Contains(PercentageLookup));
            var extractPercentage = Regex.Match(percentageOutput, @"\d+").Value;

            return int.Parse(extractPercentage);
        }

        public static string GetIconName(ICollection<string> stringOutput)
        {
            var iconOutput = stringOutput.Single(x => x.Contains(IconLookup));
            var result = Regex.Match(iconOutput, "'([^']*)'").Value;

            return result.Replace("'", string.Empty);
        }
    }
}