using dsbattery.Enums;

namespace dsbattery.Models
{
    public class DualshockDevice
    {
        public DualshockDevice(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
        public int BatteryPercentage { get; set; }
        public Ds4Status Status { get; set; }
        public string Mac { get; set; }
    }
}