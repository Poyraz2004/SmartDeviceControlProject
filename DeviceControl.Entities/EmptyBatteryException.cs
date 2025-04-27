namespace DeviceControl.Entities;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException() : base("Battery level is too low to turn on.") { }
}   