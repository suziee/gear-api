using Api.Gear.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Gear.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CamController : ControllerBase
    {
        private readonly ICamRepository _repo;

        public CamController(ICamRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repo.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _repo.GetAllBrandsAsync();
            return Ok(result);
        }

        [HttpGet("{brand}/models")]
        public async Task<IActionResult> GetAllModelsByBrand(string brand)
        {
            var result = await _repo.GetAllModelsByBrandAsync(brand);
            return Ok(result);
        }

        [HttpGet("aggregates")]
        public async Task<IActionResult> GetAggregates()
        {
            var result = await _repo.GetAggregates();
            return Ok(result);
        }
    }
}