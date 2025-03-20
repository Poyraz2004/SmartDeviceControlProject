using System;

namespace SmartDeviceControl.Exceptions
{
    public class EmptyBatteryException : Exception
    {
        public EmptyBatteryException() : base("Battery is too low. Please recharge.") { }
    }
}