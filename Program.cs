using System;
using controllerbattery.UPower;

namespace controllerbattery
{
    internal static class Program
    {
        private static void Main()
        {
            var reporter = new UPowerReporter("gaming_input_sony_controller");

            if(reporter.IsConnected())
            {
                Console.WriteLine($"🎮 {reporter.GetPercentage()}%");
            }

            Console.WriteLine(string.Empty);
        }
    }
}
