using Api.Gear.Interfaces;
using Api.Gear.Models;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gear.Repositories
{
    internal class CarabinerRepository : ICarabinerRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CarabinerRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Carabiner>> GetAllAsync()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT
                        Id,
                        Guid,
                        Brand,
                        Model,
                        Type,
                        ClosedMajorAxisKilonewtons,
                        ClosedMinorAxisKilonewtons,
                        OpenedMajorAxisKilonewtons,
                        LengthInMillimeters,
                        WidthInMillimeters,
                        GateOpenClearanceInMillimeters,
                        WeightInGrams
                    FROM Carabiner";

                var result = await connection.QueryAsync<Carabiner>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllBrandsAsync()
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Brand FROM Carabiner";

                var result = await connection.QueryAsync<string>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<string>> GetAllModelsByBrandAsync(string brand)
        {
            using (var connection = _connectionFactory.Get())
            {
                const string sql = @"SELECT DISTINCT Model FROM Carabiner WHERE Brand = @Brand";

                var queryParams = new DynamicParameters();
                queryParams.Add("@Brand", brand);

                var result = await connection.QueryAsync<string>(sql, queryParams);

                return result;
            }
        }
    }
}