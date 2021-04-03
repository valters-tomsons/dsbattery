using System;
using System.Threading.Tasks;
using dsbattery.Interfaces;
using dsbattery.Providers;
using dsbattery.Services;

namespace dsbattery
{
    internal static class Program
    {
        private static readonly IDeviceProvider _deviceProvider = new DeviceProvider();

        private async static Task Main()
        {
            var reporter = new DualshockReporter(_deviceProvider);
            var result = await reporter.GetBatteryReport().ConfigureAwait(false);

            Console.WriteLine(result);
        }
    }
}
