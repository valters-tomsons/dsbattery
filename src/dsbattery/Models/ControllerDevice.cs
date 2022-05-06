using dsbattery.Enums;

namespace dsbattery.Models;

public class ControllerDevice
{
    public ControllerDevice(string path)
    {
        Path = path;
    }

    public string Path { get; set; }
    public int BatteryPercentage { get; set; }
    public DeviceStatus Status { get; set; }
    public string Mac { get; set; }
    public DeviceKind Kind { get; set; }
}