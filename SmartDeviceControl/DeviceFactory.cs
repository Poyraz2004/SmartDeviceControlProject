using SmartDeviceControl.Models;
using SmartDeviceControl.Exceptions;

namespace SmartDeviceControl
{
    public static class DeviceFactory
    {
        /// <summary>
        /// Creates a device based on the provided data.
        /// </summary>
        /// <param name="line">The line containing device data.</param>
        /// <returns>A Device object or null if invalid.</returns>
        public static Device CreateDevice(string line)
        {
            var parts = line.Split(',');
            if (parts.Length < 3) return null;

            string id = parts[0];
            string name = parts[1];
            bool isOn = bool.TryParse(parts[2], out bool isOnStatus) ? isOnStatus : false;

            if (id.StartsWith("SW"))
            {
                if (parts.Length < 4 || !int.TryParse(parts[3].Trim('%'), out int battery)) return null;
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
                if (parts.Length < 4) return null;
                string ipAddress = parts[2];
                string networkName = parts[3];
                var embeddedDevice = new EmbeddedDevice(id, name, ipAddress, networkName);
                if (isOn) embeddedDevice.TurnOn();
                return embeddedDevice;
            }

            return null;
        }
    }
}