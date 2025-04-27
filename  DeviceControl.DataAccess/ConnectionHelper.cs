using System.Data.SqlClient;

namespace DeviceControl.DataAccess
{
    public static class ConnectionHelper
    {
        private static readonly string connectionString = "Server=localhost\\SQLEXPRESS;Database=DeviceControlDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}