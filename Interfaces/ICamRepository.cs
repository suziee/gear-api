using Api.Gear.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gear.Interfaces
{
    public interface ICamRepository
    {
        Task<IEnumerable<Cam>> GetAllAsync();
        Task<IEnumerable<string>> GetAllBrandsAsync();
        Task<IEnumerable<string>> GetAllModelsByBrandAsync(string brand);
        Task<Aggregates> GetAggregates();
    }
}