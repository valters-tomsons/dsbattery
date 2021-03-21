using System.Text;
using System;
using System.Linq;
using System.Threading.Tasks;
using dsbattery.Interfaces;

namespace dsbattery
{
    internal static class Program
    {
        private const string Dualshock4_Prefix = "sony_controller_battery";

        private static readonly IBatteryReporter _reporter = new NativeReporter();

        private async static Task Main()
        {
            var devices = await _reporter.QueryConnected(Dualshock4_Prefix).ConfigureAwait(false);
            var deviceCount = devices.Count();

            var result = new StringBuilder();

            for(int i = 0; i < deviceCount; i++)
            {
                var device = devices.ElementAt(i);

                result.Append("🎮");

                if(device.Status == Enums.Ds4Status.Charging)
                {
                    result.Append('↑');
                }

                result.Append(' ').Append(device.BatteryPercentage).Append('%');

                if(deviceCount > 1 && deviceCount != i + 1)
                {
                    result.Append(" | ");
                }
            }

            Console.WriteLine(result.ToString());
        }
    }
}
