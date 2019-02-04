namespace controllerbattery
{
    public interface IBatteryReporter
    {
        bool IsConnected();
        int GetPercentage();
    }
}