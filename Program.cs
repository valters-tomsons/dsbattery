using System;
using controllerbattery.UPower;

namespace controllerbattery
{
    internal static class Program
    {
        private const string ChargingIconName = "battery-full-charging-symbolic";
        private const string Dualshock4 = "gaming_input_sony_controller";

        private static void Main()
        {
            var reporter = new UPowerReporter();

            var devices = reporter.QueryConnected(Dualshock4);

            foreach(var device in devices)
            {
                var percentage = device.BatteryPercentage;
                var iconName = device.IconName;

                var isCharging = iconName.Equals(ChargingIconName);

                var icon = "🎮";

                if(isCharging)
                {
                    icon = "🎮↑";
                }

                Console.Write($"{icon} {percentage}%");
            }

            Console.WriteLine(string.Empty);
        }
    }
}
