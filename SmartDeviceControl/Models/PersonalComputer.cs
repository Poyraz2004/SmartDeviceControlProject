using SmartDeviceControl.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SmartDeviceControl.Models
{
    public class PersonalComputer : Device
    {
        public string IpAddress { get; private set; }
        public string OperatingSystem { get; private set; }  // Added Operating System

        public PersonalComputer(string id, string name, string ipAddress, string operatingSystem = null) : base(id, name)
        {
            if (!IsValidIpAddress(ipAddress))
                throw new SmartDeviceControl.Exceptions.InvalidDataException($"Invalid IP address: {ipAddress}");

            IpAddress = ipAddress;
            OperatingSystem = operatingSystem;
        }

        public override void TurnOn()
        {
            // If Operating System is not specified, throw EmptySystemException
            if (string.IsNullOrEmpty(OperatingSystem))
                throw new EmptySystemException();  // Throwing the exception

            Console.WriteLine($"{Name} is turning on...");

            if (string.IsNullOrEmpty(IpAddress))
                throw new SmartDeviceControl.Exceptions.InvalidDataException($"Connection error: IP address is not defined.");


            base.TurnOn();
        }

        private bool IsValidIpAddress(string ipAddress)
        {
            string pattern = @"^(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\." +
                             @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\." +
                             @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\." +
                             @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])$";

            return Regex.IsMatch(ipAddress, pattern);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, IP: {IpAddress}, OS: {OperatingSystem ?? "None"}";
        }
    }
}