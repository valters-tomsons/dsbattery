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

        private async static Task Main(string[] args)
        {
            var reporter = new DualshockReporter(_deviceProvider);
            var result = await reporter.GetBatteryReport().ConfigureAwait(false);

            Console.WriteLine(result);

            if(args?.Length > 0 && args[0] == "-d")
            {
                new DeviceDisconnect(_deviceProvider).Disconnect();
            }
        }
    }
}
