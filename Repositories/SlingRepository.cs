using Api.Gear.Interfaces;
using Api.Gear.Models;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gear.Repositories
{
    internal class SlingRepository : ISlingRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public SlingRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Sling>> GetAllAsync()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT
                        Id,
                        Guid,
                        Brand,
                        Model,
                        StrengthInKilonewtons,
                        LengthInCentimeters,
                        WidthInMillimeters,
                        WeightInGrams
                    FROM Sling";

                var result = await connection.QueryAsync<Sling>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllBrandsAsync()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Brand FROM Sling";

                var result = await connection.QueryAsync<string>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllModelsByBrandAsync(string brand)
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Model FROM Sling WHERE Brand = @Brand";

                var queryParams = new DynamicParameters();
                queryParams.Add("@Brand", brand);

                var result = await connection.QueryAsync<string>(sql, queryParams);

                return result;
            }
        }
    }
}