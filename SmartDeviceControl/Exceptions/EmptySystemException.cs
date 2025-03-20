using System;

namespace SmartDeviceControl.Exceptions
{
    public class EmptySystemException : Exception
    {
        public EmptySystemException() : base("Operating system is not specified. Computer cannot be launched.") { }
    }
}