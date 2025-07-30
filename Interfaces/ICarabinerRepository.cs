using Api.Gear.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Gear.Interfaces
{
    public interface ICarabinerRepository
    {
        Task<IEnumerable<Carabiner>> GetAllAsync();
        Task<IEnumerable<string>> GetAllBrandsAsync();
        Task<IEnumerable<string>> GetAllModelsByBrandAsync(string brand);
    }
}