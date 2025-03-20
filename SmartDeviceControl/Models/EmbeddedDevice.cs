using SmartDeviceControl.Exceptions;
using System;
using System.Text.RegularExpressions;

namespace SmartDeviceControl.Models
{
    public class EmbeddedDevice : Device
    {
        private string ipAddress;
        private string networkName;

        // IP Address Property
        public string IpAddress
        {
            get => ipAddress;
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
                ipAddress = value; 
            }
        }

       
        public string NetworkName
        {
            get => networkName;
            set => networkName = value;
        }

        
        public EmbeddedDevice(string id, string name, string ipAddress, string networkName) : base(id, name)
        {
            IpAddress = ipAddress;
            NetworkName = networkName;
        }

       
        public void Connect()
        {
            if (!networkName.Contains("MD Ltd."))
            {
                throw new ConnectionException("Network name must contain 'MD Ltd.'");
            }
            Console.WriteLine($"Connected to {networkName}.");
        }

        
        public override void TurnOn()
        {
           
            Connect();

            Console.WriteLine($"{Name} is turning on...");

            base.TurnOn();
        }

        public override string ToString()
        {
            return $"{base.ToString()}, IP: {IpAddress}, Network: {NetworkName}";
        }
    }
}
