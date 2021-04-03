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
        private readonly IDeviceProvider _deviceProvider;

        public DualshockReporter(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
        }

        public async Task<string> GetBatteryReport()
        {
            var devices = await _deviceProvider.QueryConnected(Dualshock4_Prefix).ConfigureAwait(false);
            var deviceLength = devices.Length;

            var result = new StringBuilder();

            for(int i = 0; i < deviceLength; i++)
            {
                var device = devices[i];
                AppendDevice(result, device);

                if(deviceLength > 1 && deviceLength != i + 1)
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