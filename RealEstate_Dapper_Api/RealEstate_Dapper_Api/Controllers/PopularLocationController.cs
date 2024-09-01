using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.PopularLocationDtos;
using RealEstate_Dapper_Api.Repositories.PopularLocationRepositories;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopularLocationController : ControllerBase
    {
        private readonly IPopularLocationRepository _repository;

        public PopularLocationController(IPopularLocationRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPopularLocation()
        {
            var value =await _repository.GetAllPopularLocationAsync();
            return Ok(value);
        }
       
        [HttpPost]
        public async Task<IActionResult> CreatePopularLocation(CreatePopularLocationDto createPopularLocationDto)
        {
            _repository.CreatePopularLocation(createPopularLocationDto);
            return Ok("Başarılı şekilde eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePopularLocation(int id)
        {
            _repository.DeletePopularLocation(id);
            return Ok("başarılı bir şekilde silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePopularLocation(UpdatePopularLocationDto updatePopularLocationDto)
        {
            _repository.UpdatePopularLocation(updatePopularLocationDto);
            return Ok("başarılı bir şekilde güncellendi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPopularLocation(int id)
        {
            var value = await _repository.GetPopularLocation(id);
            return Ok(value);
        }
    }
}
