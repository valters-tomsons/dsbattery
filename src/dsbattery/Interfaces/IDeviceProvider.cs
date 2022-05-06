using System.Threading.Tasks;
using dsbattery.Models;

namespace dsbattery.Interfaces
{
    public interface IDeviceProvider
    {
        Task<ControllerDevice[]> QueryConnected(string pathQuery);
        ControllerDevice[] CachedQuery();
    }
}