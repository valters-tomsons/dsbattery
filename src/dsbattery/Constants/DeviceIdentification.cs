using System;
using System.Collections.Generic;
using System.Linq;
using dsbattery.Enums;

namespace dsbattery.Constants;

public static class DeviceIdentification
{
    private static readonly Dictionary<DeviceKind, string> queryBase = new() {
        { DeviceKind.Dualshock4, "sony_controller_battery" },
        { DeviceKind.Dualsense, "ps-controller-battery" }
    };

    public static string GetQueryString(DeviceKind kind)
    {
        return queryBase[kind];
    }

    public static string GetMacAddressFromPath(string devicePath, DeviceKind kind)
    {
        switch (kind)
        {
            case DeviceKind.Dualshock4:
                {
                    return devicePath.Split('_').LastOrDefault();
                }
            case DeviceKind.Dualsense:
                {
                    return devicePath.Split('-').LastOrDefault();
                }
            default:
                throw new Exception("Unknown device kind");
        }
    }
}