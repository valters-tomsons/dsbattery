using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using dsbattery.Enums;
using dsbattery.Interfaces;
using dsbattery.Models;

namespace dsbattery.Services
{
    public class DualshockReporter : IBatteryReporter
    {
        private const string Dualshock4_Prefix = "sony_controller_battery";
        private const string Dualsense_Prefix = "ps-controller-battery";

        private readonly IDeviceProvider _deviceProvider;

        public DualshockReporter(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
        }

        public async Task<string> GetBatteryReport()
        {
            var dualshockDevices = await _deviceProvider.QueryConnected(Dualshock4_Prefix).ConfigureAwait(false);
            var dualsenseDevices = await _deviceProvider.QueryConnected(Dualsense_Prefix).ConfigureAwait(false);

            var sonyDevices = new List<DualshockDevice>();
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

        private static void AppendDevice(StringBuilder builder, DualshockDevice device)
        {
            builder.Append("🎮");

            if (device.Status == Ds4Status.Charging)
            {
                builder.Append('↑');
            }

            builder.Append(' ').Append(device.BatteryPercentage).Append('%');
        }
    }
}