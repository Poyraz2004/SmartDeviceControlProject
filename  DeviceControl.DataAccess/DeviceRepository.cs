using System.Collections.Generic;
using System.Data.SqlClient;
using DeviceControl.Entities;

namespace DeviceControl.DataAccess
{
    public class DeviceRepository
    {
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

        public void AddDevice(Device device)
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand(
                    "INSERT INTO Devices (Id, Name, DeviceType, IsEnabled) VALUES (@Id, @Name, @DeviceType, @IsEnabled)", connection);

                command.Parameters.AddWithValue("@Id", device.Id);
                command.Parameters.AddWithValue("@Name", device.Name);
                command.Parameters.AddWithValue("@DeviceType", device.DeviceType);
                command.Parameters.AddWithValue("@IsEnabled", device.IsEnabled);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateDevice(Device device)
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand(
                    "UPDATE Devices SET Name = @Name, DeviceType = @DeviceType, IsEnabled = @IsEnabled WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", device.Id);
                command.Parameters.AddWithValue("@Name", device.Name);
                command.Parameters.AddWithValue("@DeviceType", device.DeviceType);
                command.Parameters.AddWithValue("@IsEnabled", device.IsEnabled);

                command.ExecuteNonQuery();
            }
        }
        
        public void DeleteDevice(string id)
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                var command = new SqlCommand(
                    "DELETE FROM Devices WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

    }
}
