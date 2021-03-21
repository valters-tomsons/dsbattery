using System.Collections.Generic;
using System.Threading.Tasks;
using dsbattery.Models;

namespace dsbattery.Interfaces
{
    public interface IBatteryReporter
    {
        Task<IEnumerable<Device>> QueryConnected(string pathQuery);
    }
}