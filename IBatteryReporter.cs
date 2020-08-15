using System.Collections.Generic;

namespace controllerbattery
{
    public interface IBatteryReporter
    {
        bool IsConnected();
        int GetPercentage();
        ICollection<string> QueryDeviceInfo();
    }
}