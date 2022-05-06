using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dsbattery.Enums;
using dsbattery.Interfaces;
using dsbattery.Models;

namespace dsbattery.Services;

public class ControllerDeviceReporter : IBatteryReporter
{
    private readonly IDeviceProvider _deviceProvider;

    public ControllerDeviceReporter(IDeviceProvider deviceProvider)
    {
        _deviceProvider = deviceProvider;
    }

    public async Task<string> GetBatteryReport()
    {
        var dualshockDevices = await _deviceProvider.QueryConnected(DeviceKind.Dualshock4);
        var dualsenseDevices = await _deviceProvider.QueryConnected(DeviceKind.Dualsense);

        var sonyDevices = new List<ControllerDevice>(dualsenseDevices.Count + dualshockDevices.Count);
        sonyDevices.AddRange(dualshockDevices);
        sonyDevices.AddRange(dualsenseDevices);

        var result = new StringBuilder();
        for (int i = 0; i < sonyDevices.Count; i++)
        {
            var device = sonyDevices[i];
            AppendDevice(result, device);

            if (sonyDevices.Count > 1 && sonyDevices.Count != i + 1)
            {
                result.Append(" | ");
            }
        }

        return result.ToString();
    }

    private static void AppendDevice(StringBuilder builder, ControllerDevice device)
    {
        builder.Append("🎮");

        if (device.Status == DeviceStatus.Charging)
        {
            builder.Append('↑');
        }

        builder.Append(' ').Append(device.BatteryPercentage).Append('%');
    }
}