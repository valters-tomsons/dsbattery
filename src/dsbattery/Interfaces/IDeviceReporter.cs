using System.Threading.Tasks;
using dsbattery.Models;

namespace dsbattery.Interfaces
{
    public interface IDeviceReporter
    {
        Task<Device[]> QueryConnected(string pathQuery);
    }
}