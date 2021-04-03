using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dsbattery.Enums;
using dsbattery.Interfaces;
using dsbattery.Models;

namespace dsbattery.Providers
{
    public class DeviceProvider : IDeviceProvider
    {
        private const string DeviceBasePath = "/sys/class/power_supply";

        public async Task<Device[]> QueryConnected(string pathQuery)
        {
            var devices = Directory.EnumerateFileSystemEntries(DeviceBasePath, pathQuery + "*").ToArray();
            var serialized = new Device[devices.Length];

            for(var i = 0; i < serialized.Length; i++)
            {
                serialized[i] = await SerializeDevice(devices[i]).ConfigureAwait(false);
            }

            return serialized;
        }

        private static async Task<Device> SerializeDevice(string path)
        {
            var battery = await ReadBattery(path).ConfigureAwait(false);
            var status = await ReadStatus(path).ConfigureAwait(false);

            return new Device(path)
            {
                BatteryPercentage = battery,
                Status = status
            };
        }

        private static async Task<int> ReadBattery(string devicePath)
        {
            const string property = "capacity";

            var batteryResult = await ReadDeviceProperty(devicePath, property).ConfigureAwait(false);
            return int.Parse(batteryResult);
        }

        private static async Task<Ds4Status> ReadStatus(string devicePath)
        {
            const string property = "status";

            var statusResult = await ReadDeviceProperty(devicePath, property).ConfigureAwait(false);
            return Enum.Parse<Ds4Status>(statusResult, true);
        }

        private static async Task<string> ReadDeviceProperty(string devicePath, string propertyName)
        {
            var statusPath = new StringBuilder(devicePath);
            statusPath.Append(Path.DirectorySeparatorChar);
            statusPath.Append(propertyName);

            return await File.ReadAllTextAsync(statusPath.ToString()).ConfigureAwait(false);
        }
    }
}