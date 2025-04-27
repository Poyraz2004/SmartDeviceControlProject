namespace DeviceControl.Entities;

public class EmptySystemException : Exception
{
    public EmptySystemException() : base("Operating system is not installed.") { }
}