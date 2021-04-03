using System.Threading.Tasks;
using dsbattery.Models;

namespace dsbattery.Interfaces
{
    public interface IDeviceProvider
    {
        Task<DualshockDevice[]> QueryConnected(string pathQuery);
    }
}