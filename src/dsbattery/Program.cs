using System.Text;
using System;
using System.Threading.Tasks;
using dsbattery.Interfaces;
using dsbattery.Enums;
using dsbattery.Models;
using dsbattery.Providers;

namespace dsbattery
{
    internal static class Program
    {
        private const string Dualshock4_Prefix = "sony_controller_battery";

        private static readonly IDeviceProvider _reporter = new DeviceProvider();

        private async static Task Main()
        {
            var devices = await _reporter.QueryConnected(Dualshock4_Prefix).ConfigureAwait(false);
            var deviceLength = devices.Length;

            var result = new StringBuilder();

            for(int i = 0; i < deviceLength; i++)
            {
                var device = devices[i];
                result.AppendDevice(device);

                if(deviceLength > 1 && deviceLength != i + 1)
                {
                    result.Append(" | ");
                }
            }

            Console.WriteLine(result.ToString());
        }

        private static void AppendDevice(this StringBuilder builder, Device device)
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
