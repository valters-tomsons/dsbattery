using System.Text;
using System;
using System.Collections.Generic;
using controllerbattery.UPower;
using Models;
using System.Linq;

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
            var deviceCount = devices.Count();

            var result = new StringBuilder();

            for(int i = 0; i < deviceCount; i++)
            {
                var device = devices.ElementAt(i);

                var percentage = device.BatteryPercentage;
                var iconName = device.IconName;

                var isCharging = iconName.Equals(ChargingIconName);

                var icon = "🎮";

                if(isCharging)
                {
                    icon = "🎮↑";
                }

                if(deviceCount > 1 && deviceCount == i + 1)
                {
                    result.Append(" | ");
                }

                result.Append(icon).Append(" ").Append(percentage).Append("%");
            }

            Console.WriteLine(result.ToString());
        }
    }
}
