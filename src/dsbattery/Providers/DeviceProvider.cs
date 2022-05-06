using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dsbattery.Enums;
using dsbattery.Interfaces;
using dsbattery.Models;
using dsbattery.Constants;

namespace dsbattery.Providers;

public class DeviceProvider : IDeviceProvider
{
    private const string DeviceBasePath = "/sys/class/power_supply";
    private readonly List<ControllerDevice> _deviceCache = new();

    public async Task<IReadOnlyCollection<ControllerDevice>> QueryConnected(DeviceKind kind)
    {
        if (kind == DeviceKind.Unknown)
        {
            throw new InvalidOperationException("Cannot query unknown device kind");
        }

        var queryString = DeviceIdentification.GetQueryString(kind);
        var devices = Directory.EnumerateFileSystemEntries(DeviceBasePath, queryString + "*").ToArray();

        var serialized = new ControllerDevice[devices.Length];
        for (var i = 0; i < serialized.Length; i++)
        {
            serialized[i] = await SerializeDevice(devices[i], kind).ConfigureAwait(false);
        }

        _deviceCache.AddRange(serialized);
        return serialized;
    }

    public IReadOnlyCollection<ControllerDevice> QueryCached()
    {
        return _deviceCache;
    }

    private static async Task<ControllerDevice> SerializeDevice(string path, DeviceKind kind)
    {
        var battery = await ReadBattery(path).ConfigureAwait(false);
        var status = await ReadStatus(path).ConfigureAwait(false);

        return new ControllerDevice(path)
        {
            BatteryPercentage = battery,
            Status = status,
            Mac = DeviceIdentification.GetMacAddressFromPath(path, kind),
            Kind = kind
        };
    }

    private static async Task<int> ReadBattery(string devicePath)
    {
        const string property = "capacity";

        var batteryResult = await ReadDeviceProperty(devicePath, property).ConfigureAwait(false);
        return int.Parse(batteryResult);
    }

    private static async Task<DeviceStatus> ReadStatus(string devicePath)
    {
        const string property = "status";

        var statusResult = await ReadDeviceProperty(devicePath, property).ConfigureAwait(false);
        return Enum.Parse<DeviceStatus>(statusResult, true);
    }

    private static async Task<string> ReadDeviceProperty(string devicePath, string propertyName)
    {
        var statusPath = new StringBuilder(devicePath);
        statusPath.Append(Path.DirectorySeparatorChar);
        statusPath.Append(propertyName);

        return await File.ReadAllTextAsync(statusPath.ToString()).ConfigureAwait(false);
    }
}