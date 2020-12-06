using System;

namespace Models
{
    public class Device
    {
        public Device(string path)
        {
            Path = path;
        }

        public string Path;
        public int BatteryPercentage;
        public string IconName;
    }
}