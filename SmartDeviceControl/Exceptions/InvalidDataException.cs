using System;

namespace SmartDeviceControl.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
}