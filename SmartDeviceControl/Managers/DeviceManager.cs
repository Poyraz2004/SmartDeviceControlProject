using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmartDeviceControl.Models;
using SmartDeviceControl.Exceptions;

namespace SmartDeviceControl.Managers
{
    public class DeviceManager
    {
        private const int MaxDevices = 15;
        private List<Device> devices;
        private readonly string filePath;

        public DeviceManager(string filePath)
        {
            this.filePath = filePath;
            devices = new List<Device>();
            LoadDevices();
        }

        private void LoadDevices()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found!");
                return;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                try
                {
                    var device = ParseDevice(line);
                    if (device != null && devices.Count < MaxDevices)
                    {
                        devices.Add(device);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line} - {ex.Message}");
                }
            }
        }

        private Device ParseDevice(string line)
        {
            var parts = line.Split(',');
            if (parts.Length < 3) return null;

            string id = parts[0];
            string name = parts[1];

            if (!bool.TryParse(parts[2], out bool isOn))
                return null;

            if (id.StartsWith("SW"))
            {
                if (parts.Length < 4 || !int.TryParse(parts[3].Trim('%'), out int battery))
                    return null;

                var watch = new SmartWatch(name, id, battery);
                if (isOn) watch.TurnOn();
                return watch;
            }
            else if (id.StartsWith("P"))
            {
                string os = parts.Length > 3 ? parts[3] : null;
                var pc = new PersonalComputer(id, name, "127.0.0.1", os);
                if (isOn) pc.TurnOn();
                return pc;
            }
            else if (id.StartsWith("ED"))
            {
                if (parts.Length < 4)
                    return null;

                string ipAddress = parts[2];
                string networkName = parts[3];

                var embeddedDevice = new EmbeddedDevice(id, name, ipAddress, networkName);
                if (isOn) embeddedDevice.TurnOn();
                return embeddedDevice;
            }

            return null;
        }

        public void AddDevice(Device device)
        {
            if (devices.Count >= MaxDevices)
            {
                Console.WriteLine("Device storage is full.");
                return;
            }
            devices.Add(device);
            Console.WriteLine($"{device.Name} added.");
        }

        public void RemoveDevice(string id)
        {
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                devices.Remove(device);
                Console.WriteLine($"{device.Name} removed.");
            }
            else
            {
                Console.WriteLine("Device not found.");
            }
        }

        public void EditDevice(string id, string newName)
        {
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                device.Name = newName;
                Console.WriteLine($"{device.Id} name updated to {newName}.");
            }
            else
            {
                Console.WriteLine("Device not found.");
            }
        }

        public void TurnOnDevice(string id)
        {
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                try
                {
                    device.TurnOn();  
                    Console.WriteLine($"{device.Name} is now turned on.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error turning on device {id}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Device not found.");
            }
        }

        public void TurnOffDevice(string id)
        {
            var device = devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                device.TurnOff();  
                Console.WriteLine($"{device.Name} is now turned off.");
            }
            else
            {
                Console.WriteLine("Device not found.");
            }
        }


        public void ShowAllDevices()
        {
            foreach (var device in devices)
            {
                Console.WriteLine(device);
            }
        }

        public void SaveDataToFile()
        {
            try
            {
                var lines = devices.Select(d => d.ToString()).ToArray();
                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
    }
}
