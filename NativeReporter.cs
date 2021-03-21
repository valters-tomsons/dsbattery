using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Device>> QueryConnected(string pathQuery)
        {
            var devices = Directory.EnumerateFileSystemEntries(DeviceBasePath, pathQuery + "*");

            var result = new List<Device>(devices.Count());

            foreach(var device in devices)
            {
                var serialized = await SerializeDevice(device).ConfigureAwait(false);
                result.Add(serialized);
            }

            return result;
        }

        private static async Task<Device> SerializeDevice(string path)
        {
            var capacityPath = new StringBuilder(path);
            capacityPath.Append(Path.DirectorySeparatorChar);
            capacityPath.Append(BatteryFile);

            var batteryResultTask = File.ReadAllTextAsync(capacityPath.ToString()).ConfigureAwait(false);

            var statusPath = new StringBuilder(path);
            statusPath.Append(Path.DirectorySeparatorChar);
            statusPath.Append(StatusFile);

            var statusResult = await File.ReadAllTextAsync(statusPath.ToString()).ConfigureAwait(false);

            var statusParsed = Enum.TryParse(typeof(Ds4Status), statusResult, true, out var status);

            if(!statusParsed)
            {
                Console.WriteLine("WARNING: Unknown status, defaulting to `unknown`");
                status = Ds4Status.Unknown;
            }

            var battery = int.Parse(await batteryResultTask);

            return new Device(path){
                BatteryPercentage = battery,
                Status = (Ds4Status) status
            };
        }
    }
}