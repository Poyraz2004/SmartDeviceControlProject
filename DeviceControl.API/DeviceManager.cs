using System.Collections.Generic;
using System.Data.SqlClient;
using DeviceControl.Entities;

namespace DeviceControl.DataAccess
{
    /// <summary>
    /// Class responsible for device data management operations.
    /// Implements the IDeviceRepository interface.
    /// </summary>
    public class DeviceManager : IDeviceRepository
    {
        /// <summary>
        /// Retrieves all devices from the database.
        /// </summary>
        /// <returns>List of devices</returns>
        public List<Device> GetAllDevices()
        {
            var devices = new List<Device>();

            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Devices", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var device = new Device
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        DeviceType = reader.GetString(2),
                        IsEnabled = reader.GetBoolean(3)
                    };
                    devices.Add(device);
                }
            }

            return devices;
        }

        /// <summary>
        /// Retrieves a device by its unique identifier.
        /// </summary>
        /// <param name="id">The device ID</param>
        /// <returns>Device if found, otherwise null</returns>
        public Device GetDeviceById(string id)
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("SELECT * FROM Devices WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Device
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        DeviceType = reader.GetString(2),
                        IsEnabled = reader.GetBoolean(3)
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a new device to the database.
        /// </summary>
        /// <param name="device">The device to add</param>
        public void AddDevice(Device device)
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand(
                    "INSERT INTO Devices (Id, Name, DeviceType, IsEnabled) VALUES (@Id, @Name, @DeviceType, @IsEnabled)",
                    connection);
                command.Parameters.AddWithValue("@Id", device.Id);
                command.Parameters.AddWithValue("@Name", device.Name);
                command.Parameters.AddWithValue("@DeviceType", device.DeviceType);
                command.Parameters.AddWithValue("@IsEnabled", device.IsEnabled);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates an existing device in the database.
        /// </summary>
        /// <param name="id">The device ID to update</param>
        /// <param name="updatedDevice">The updated device information</param>
        public void UpdateDevice(string id, Device updatedDevice)
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand(
                    "UPDATE Devices SET Name = @Name, DeviceType = @DeviceType, IsEnabled = @IsEnabled WHERE Id = @Id",
                    connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", updatedDevice.Name);
                command.Parameters.AddWithValue("@DeviceType", updatedDevice.DeviceType);
                command.Parameters.AddWithValue("@IsEnabled", updatedDevice.IsEnabled);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a device from the database by ID.
        /// </summary>
        /// <param name="id">The device ID to delete</param>
        public void DeleteDevice(string id)
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("DELETE FROM Devices WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
