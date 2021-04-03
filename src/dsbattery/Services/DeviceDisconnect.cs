using System.Diagnostics;
using dsbattery.Interfaces;

namespace dsbattery.Services
{
    public class DeviceDisconnect
    {
        private readonly IDeviceProvider _deviceProvider;

        public DeviceDisconnect(IDeviceProvider deviceProvider)
        {
            _deviceProvider = deviceProvider;
        }

        public void Disconnect()
        {
            var devices = _deviceProvider.CachedQuery();

            if(devices?.Length < 1)
            {
                return;
            }

            for(var i = 0; i < devices.Length; i++)
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo("bt-device", $"-d {devices[i].Mac}")
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
}