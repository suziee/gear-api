using Api.Gear.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Gear.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StopperController : ControllerBase
    {
        private readonly IStopperRepository _repo;

        public StopperController(IStopperRepository repo)
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
    }
}