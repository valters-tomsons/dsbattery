using System.Threading.Tasks;
using dsbattery.Models;

namespace dsbattery.Interfaces
{
    public interface IBatteryReporter
    {
        Task<Device[]> QueryConnected(string pathQuery);
    }
}