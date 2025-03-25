using System;
using SmartDeviceControl.Exceptions;
using System.Text.RegularExpressions;

namespace SmartDeviceControl.Models
{
    public class EmbeddedDevice : Device
    {
        private string _ipAddress;
        private string _networkName;

        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                string ipPattern = @"^(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\." +
                                   @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\." +
                                   @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])\." +
                                   @"(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])$";

                if (!Regex.IsMatch(value, ipPattern))
                {
                    throw new ArgumentException("Invalid IP address format.");
                }
                _ipAddress = value; 
            }
        }

        public string NetworkName
        {
            get => _networkName;
            set => _networkName = value;
        }

        public EmbeddedDevice(string id, string name, string ipAddress, string networkName) : base(id, name)
        {
            IpAddress = ipAddress;
            NetworkName = networkName;
        }

        public void Connect()
        {
            if (!NetworkName.Contains("MD Ltd."))
            {
                throw new ConnectionException("Network name must contain 'MD Ltd.'");
            }
            Console.WriteLine($"Connected to {NetworkName}.");
        }

        public override void TurnOn()
        {
            Connect();
            Console.WriteLine($"{Name} is turning on");
            base.TurnOn();
        }

        public override string ToString()
        {
            return $"{base.ToString()}, IP: {IpAddress}, Network: {NetworkName}";
        }
    }
}