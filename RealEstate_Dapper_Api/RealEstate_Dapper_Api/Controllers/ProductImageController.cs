using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RealEstate_Dapper_Api.Repositories.ImageRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepositories _productImageRepositories;

        public ProductImageController(IProductImageRepositories productImageRepositories)
        {
            _productImageRepositories = productImageRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductImageByID(int id)
        {
            var values = await _productImageRepositories.getProductImageByProduct(id);
            return Ok(values);
        }
    }
}
