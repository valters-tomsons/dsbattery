using System.Collections.Generic;
using System.Threading.Tasks;
using dsbattery.Enums;
using dsbattery.Models;

namespace dsbattery.Interfaces;

public interface IDeviceProvider
{
    Task<IReadOnlyCollection<ControllerDevice>> QueryConnected(DeviceKind kind);
    IReadOnlyCollection<ControllerDevice> QueryCached();
}