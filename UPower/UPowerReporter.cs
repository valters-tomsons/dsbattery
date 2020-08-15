using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace controllerbattery.UPower
{
    public class UPowerReporter : IBatteryReporter
    {
        private string DevicePath { get; }
        private IEnumerable<string> DeviceList { get; }
        private ICollection<string> LastInfoQuery { get; set; }

        private const string PercentageLookup = "percentage:";
        private const string IconLookup = "icon-name:";

        public UPowerReporter(string matchDeviceName)
        {
            DeviceList = EnumerateDevices();
            DevicePath = GetDevicePath(matchDeviceName);
        }

        public bool IsConnected()
        {
            return DevicePath != null;
        }

        public ICollection<string> QueryDeviceInfo()
        {
            var arguments = new string[] { "-i", DevicePath };
            var upower = new UPowerWrapper(arguments);
            LastInfoQuery = upower.GetOutput();
            return LastInfoQuery;
        }

        public int GetPercentage()
        {
            if(LastInfoQuery == null)
            {
                QueryDeviceInfo();
            }

            var output = LastInfoQuery;

            var percentageOutput = output.Single(x => x.Contains(PercentageLookup));
            var extractPercentage = Regex.Match(percentageOutput, @"\d+").Value;

            return int.Parse(extractPercentage);
        }

        public string GetIconName()
        {
            if(LastInfoQuery == null)
            {
                QueryDeviceInfo();
            }

            var output = LastInfoQuery;

            var iconOutput = output.Single(x => x.Contains(IconLookup));
            var result = Regex.Match(iconOutput, "'([^']*)'").Value;

            return result.Replace("'", string.Empty);
        }

        private string GetDevicePath(string matchName)
        {
            return DeviceList.SingleOrDefault(x => x.Contains(matchName));
        }

        private IEnumerable<string> EnumerateDevices()
        {
            var arguments = new string[] { "-e" };
            var upower = new UPowerWrapper(arguments);
            return upower.GetOutput();
        }
    }
}