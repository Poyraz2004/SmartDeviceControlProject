using System;

namespace SmartDeviceControl.Exceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException(string message) : base(message) { }
    }
}