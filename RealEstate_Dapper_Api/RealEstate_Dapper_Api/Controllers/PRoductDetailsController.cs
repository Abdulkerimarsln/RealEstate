using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.ProductCategory;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PRoductDetailsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public PRoductDetailsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetProductDetailByID")]
        public async Task<IActionResult> GetProductDetailByID(int id)
        {
            var values = await _repository.GetProductDetailByID(id);
            return Ok(values);
        }
    }
}
