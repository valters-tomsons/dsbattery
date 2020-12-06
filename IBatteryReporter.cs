using System.Collections.Generic;
using Models;

namespace controllerbattery
{
    public interface IBatteryReporter
    {
        IEnumerable<Device> QueryConnected(string pathQuery);
    }
}