using System;
using System.Diagnostics;
using controllerbattery.UPower;

namespace controllerbattery
{
    class Program
    {
        static void Main(string[] args)
        {
            var reporter = new UPowerReporter("gaming_input_sony_controller");

            if(reporter.IsConnected())
            {
                System.Console.WriteLine($"🎮 {reporter.GetPercentage()}%");
            }

            System.Console.WriteLine("");
        }
    }
}
