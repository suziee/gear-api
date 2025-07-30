using Api.Gear.Interfaces;
using Api.Gear.Models;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gear.Repositories
{
    internal class CamRepository : ICamRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CamRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Cam>> GetAllAsync()
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
                        UsableMinInMillimeters,
                        UsableMaxInMillimeters,
                        UsableMinInInches,
                        UsableMaxInInches,
                        WeightInGrams
                    FROM Cam";

                var result = await connection.QueryAsync<Cam>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllBrandsAsync()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Brand FROM Cam";

                var result = await connection.QueryAsync<string>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllModelsByBrandAsync(string brand)
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Model FROM Cam WHERE Brand = @Brand";

                var queryParams = new DynamicParameters();
                queryParams.Add("@Brand", brand);

                var result = await connection.QueryAsync<string>(sql, queryParams);

                return result;
            }
        }

        public async Task<Aggregates> GetAggregates()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT
                    MIN(UsableMinInMillimeters) AS MinRange,
                    MAX(UsableMaxInMillimeters) AS MaxRange,
                    MIN(StrengthInKilonewtons) AS MinStrength,
                    MAX(StrengthInKilonewtons) AS MaxStrength
                    FROM Cam";

                var result = await connection.QuerySingleAsync<Aggregates>(sql);

                return result;
            }
        }
    }
}