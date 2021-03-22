using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dsbattery.Enums;
using dsbattery.Interfaces;
using dsbattery.Models;

namespace dsbattery
{
    public class NativeReporter : IBatteryReporter
    {
        private const string DeviceBasePath = "/sys/class/power_supply";
        private const string BatteryFile = "capacity";
        private const string StatusFile = "status";

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
            var capacityPath = new StringBuilder(path);
            capacityPath.Append(Path.DirectorySeparatorChar);
            capacityPath.Append(BatteryFile);

            var capacityResult = File.ReadAllTextAsync(capacityPath.ToString()).ConfigureAwait(false);

            var statusPath = new StringBuilder(path);
            statusPath.Append(Path.DirectorySeparatorChar);
            statusPath.Append(StatusFile);

            var statusResult = await File.ReadAllTextAsync(statusPath.ToString()).ConfigureAwait(false);
            Enum.TryParse<Ds4Status>(statusResult, true, out var status);

            return new Device(path)
            {
                BatteryPercentage = int.Parse(await capacityResult),
                Status = status
            };
        }
    }
}