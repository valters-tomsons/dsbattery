using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace controllerbattery.UPower
{
    public class UPowerReporter : IBatteryReporter
    {
        private string DevicePath { get; set; }
        private IEnumerable<string> DeviceList { get; set; }

        private static string PercentageLookup = "percentage:";

        public UPowerReporter(string matchDeviceName)
        {
            DeviceList = EnumerateDevices();
            DevicePath = GetDevicePath(matchDeviceName);
        }

        public bool IsConnected()
        {
            return DevicePath != null;
        }

        public int GetPercentage()
        {
            var arguments = new string[] { "-i", DevicePath};
            var upower = new UPowerWrapper(arguments);

            var output = upower.GetOutput();

            var percentageOutput = output.Single(x => x.Contains(PercentageLookup));
            var extractPercentage = Regex.Match(percentageOutput, @"\d+").Value;

            return int.Parse(extractPercentage);
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