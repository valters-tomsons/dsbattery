using System.Threading.Tasks;

namespace dsbattery.Interfaces
{
    public interface IBatteryReporter
    {
        Task<string> GetBatteryReport();
    }
}