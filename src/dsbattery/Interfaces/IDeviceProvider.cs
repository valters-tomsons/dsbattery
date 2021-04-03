using System.Threading.Tasks;
using dsbattery.Models;

namespace dsbattery.Interfaces
{
    public interface IDeviceProvider
    {
        Task<Device[]> QueryConnected(string pathQuery);
    }
}