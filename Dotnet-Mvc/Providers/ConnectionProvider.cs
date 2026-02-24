using System.Data;
using Npgsql;

namespace Dotnet_Mvc.Providers
{
    public static class ConnectionProvider
    {
        private static string? _connectionString;

        public static void Initialize(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public static IDbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException(
                    "DapperConnectionProvider is not initialized. Call Initialize() first.");

            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}