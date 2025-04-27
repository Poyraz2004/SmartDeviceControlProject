using System.Collections.Generic;
using DeviceControl.Entities;

namespace DeviceControl.DataAccess
{
    /// <summary>
    /// Interface for device repository operations.
    /// </summary>
    public interface IDeviceRepository
    {
        List<Device> GetAllDevices();
        Device GetDeviceById(string id);
        void AddDevice(Device device);
        void UpdateDevice(string id, Device updatedDevice);
        void DeleteDevice(string id);
    }
}