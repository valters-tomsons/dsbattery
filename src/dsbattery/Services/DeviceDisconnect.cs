using System.Diagnostics;
using dsbattery.Interfaces;

namespace dsbattery.Services;

public class DeviceDisconnect
{
    private readonly IDeviceProvider _deviceProvider;

    public DeviceDisconnect(IDeviceProvider deviceProvider)
    {
        _deviceProvider = deviceProvider;
    }

    public void Disconnect()
    {
        var devices = _deviceProvider.QueryCached();

        if (devices.Count < 1)
        {
            return;
        }

        foreach (var device in devices)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo("bt-device", $"-d {device.Mac}")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true
                }
            };

            proc.Start();
        }
    }
}