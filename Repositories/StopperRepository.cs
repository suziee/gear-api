using Api.Gear.Interfaces;
using Api.Gear.Models;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gear.Repositories
{
    internal class StopperRepository : IStopperRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public StopperRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Stopper>> GetAllAsync()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT
                        Id,
                        Guid,
                        Brand,
                        Model,
                        Color,
                        Size,
                        StrengthInKilonewtons,
                        LengthInMillimeters,
                        WidthInMillimeters,
                        SmallerWidthInMillimeters,
                        WeightInGrams
                    FROM Stopper";

                var result = await connection.QueryAsync<Stopper>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllBrandsAsync()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Brand FROM Stopper";

                var result = await connection.QueryAsync<string>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllModelsByBrandAsync(string brand)
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Model FROM Stopper WHERE Brand = @Brand";

                var queryParams = new DynamicParameters();
                queryParams.Add("@Brand", brand);

                var result = await connection.QueryAsync<string>(sql, queryParams);

                return result;
            }
        }
    }
}