using System;
using controllerbattery.UPower;

namespace controllerbattery
{
    internal static class Program
    {
        private const string ChargingIconName = "battery-full-charging-symbolic";

        private static void Main()
        {
            var reporter = new UPowerReporter("gaming_input_sony_controller");

            if(reporter.IsConnected())
            {
                var percentage = reporter.GetPercentage();
                var iconName = reporter.GetIconName();
                var isCharging = iconName.Equals(ChargingIconName);

                var icon = "🎮";

                if(isCharging)
                {
                    icon = "🎮↑";
                }

                Console.WriteLine($"{icon} {percentage}%");
            }

            Console.WriteLine(string.Empty);
        }
    }
}
