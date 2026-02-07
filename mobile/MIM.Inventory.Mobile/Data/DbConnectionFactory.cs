using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MIM.Inventory.Mobile.Data
{
    public interface IDbConnectionFactory
    {
        string ConnectionString { get; }
        Task<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken = default);
        Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        public DbConnectionFactory(IConfiguration configuration)
        {
            var connectionString = configuration["Database:ConnectionString"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Database:ConnectionString is not configured.");
            }

            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public async Task<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken = default)
        {
            var connection = new NpgsqlConnection(ConnectionString);
            await connection.OpenAsync(cancellationToken);
            return connection;
        }

        public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await using var connection = await CreateOpenConnectionAsync(cancellationToken);
                return connection.State == System.Data.ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }
    }
}
