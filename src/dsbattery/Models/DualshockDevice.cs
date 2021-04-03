using dsbattery.Enums;

namespace dsbattery.Models
{
    public class DualshockDevice
    {
        public DualshockDevice(string path)
        {
            Path = path;
        }

        public string Path;
        public int BatteryPercentage;
        public Ds4Status Status;
    }
}