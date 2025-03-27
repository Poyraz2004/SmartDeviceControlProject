
namespace SmartDeviceControl.Managers  
{
    public class DeviceManager
    {
        private const int MaxDevices = 15;
        private List<Device> _devices; 
        private readonly string _filePath;

        public DeviceManager(string filePath)
        {
            _filePath = filePath;
            _devices = new List<Device>();
            LoadDevices();
        }

        /// <summary>
        /// Loads devices from the file and adds them to the devices list.
        /// </summary>
        private void LoadDevices()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("File not found!");
                return;
            }

            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                try
                {
                    var device = DeviceFactory.CreateDevice(line);  // DeviceFactory kullanımı
                    if (device != null && _devices.Count < MaxDevices)
                    {
                        _devices.Add(device);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line} - {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Adds a new device to the devices list.
        /// </summary>
        /// <param name="device">The device to be added.</param>
        public void AddDevice(Device device)
        {
            if (_devices.Count >= MaxDevices)
            {
                Console.WriteLine("Device storage is full.");
                return;
            }
            _devices.Add(device);
            Console.WriteLine($"{device.Name} added.");
        }

        /// <summary>
        /// Removes a device from the devices list by its ID.
        /// </summary>
        /// <param name="id">The ID of the device to remove.</param>
        public void RemoveDevice(string id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                _devices.Remove(device);
                Console.WriteLine($"{device.Name} removed.");
            }
            else
            {
                Console.WriteLine("Device not found.");
            }
        }

        /// <summary>
        /// Edits a device's name by its ID.
        /// </summary>
        /// <param name="id">The ID of the device to edit.</param>
        /// <param name="newName">The new name of the device.</param>
        public void EditDevice(string id, string newName)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
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

        /// <summary>
        /// Turns on a device by its ID.
        /// </summary>
        /// <param name="id">The ID of the device to turn on.</param>
        public void TurnOnDevice(string id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
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

        /// <summary>
        /// Turns off a device by its ID.
        /// </summary>
        /// <param name="id">ID of the device to turn off.</param>
        public void TurnOffDevice(string id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
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

        /// <summary>
        /// Displays all devices in the list.
        /// </summary>
        public void ShowAllDevices()
        {
            foreach (var device in _devices)
            {
                Console.WriteLine(device);
            }
        }

        /// <summary>
        /// Saves the device data back to the file.
        /// </summary>
        public void SaveDataToFile()
        {
            try
            {
                var lines = _devices.Select(d => d.ToString()).ToArray();
                File.WriteAllLines(_filePath, lines);
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }
    }
}
