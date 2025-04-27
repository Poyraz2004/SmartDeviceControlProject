namespace DeviceControl.Entities;

public class ConnectionException : Exception
{
    public ConnectionException() : base("Wrong network name.") { }
}