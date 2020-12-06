using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Models;
using UPower;

namespace controllerbattery.UPower
{
    public class UPowerReporter : IBatteryReporter
    {
        private IEnumerable<string> DeviceList { get; }

        public UPowerReporter()
        {
            DeviceList = EnumerateDevices();
        }

        public IEnumerable<Device> QueryConnected(string pathQuery)
        {
            var paths = FindConnected(pathQuery);
            var devices = new List<Device>();

            foreach(var path in paths)
            {
                var deviceInfo = QueryDeviceInfo(path);

                var device = new Device(path){
                    BatteryPercentage = UPowerParser.GetPercentage(deviceInfo),
                    IconName = UPowerParser.GetIconName(deviceInfo)
                };

                devices.Add(device);
            }

            return devices;
        }

        private ICollection<string> QueryDeviceInfo(string devicePath)
        {
            var arguments = new string[] { "-i", devicePath};
            return new UPowerWrapper(arguments).GetOutput();
        }

        private IEnumerable<string> FindConnected(string pathQuery)
        {
            return DeviceList.Where(x => x.Contains(pathQuery));
        }

        private IEnumerable<string> EnumerateDevices()
        {
            var arguments = new string[] { "-e" };
            return new UPowerWrapper(arguments).GetOutput();
        }
    }
}