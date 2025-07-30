using Api.Gear.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Api.Gear.Repositories
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration config, ILogger<DbConnectionFactory> logger)
        {
            _connectionString = config.GetConnectionString("Gear");
        }

        public IDbConnection Get()
        {
            return new SqliteConnection(_connectionString);
        }
    }
}