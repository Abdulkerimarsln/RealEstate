using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.TestimonialRepositories;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialRepository _estimonialRepository;

        public TestimonialController(ITestimonialRepository estimonialRepository)
        {
            _estimonialRepository = estimonialRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTestimonial()
        {
            var value = await _estimonialRepository.GetAllTestimonialAsync();
            return Ok(value);
        }
    }
}
