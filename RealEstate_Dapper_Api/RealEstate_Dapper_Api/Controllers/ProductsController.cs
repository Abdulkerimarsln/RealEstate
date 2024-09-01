using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Repositories.ProductCategory;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var value = await _repository.GetAllProductAsync();
            return Ok(value);
        }
        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var value =await _repository.GetAllProductWithCategoryAsync();
            return Ok(value);
        }
        [HttpGet("ProductDealOfTheDayStatusChangeToTrue/{id}")]
        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToTrue( int id)
        {
             _repository.ProductDealOfTheDayStatusChangeToTrue(id);
            return Ok("İlan Eklendi");
        }
        [HttpGet("ProductDealOfTheDayStatusChangeToFalse/{id}")]
        public async Task<IActionResult> ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            _repository.ProductDealOfTheDayStatusChangeToFalse(id);
            return Ok("İlan Çıkarıldı");
        }
        [HttpGet("Last5ProductAsync")]
        public async Task<IActionResult> Last5ProductAsync()
        {
            var values= await _repository.GetLast5ProductAsync();
            return Ok(values);
        }
        [HttpGet("ProductAdvertListByEmployeeAsyncByTrue")]
        public async Task<IActionResult> ProductAdvertListByEmployeeAsyncByTrue(int id)
        { 
            var value=await _repository.GetProductAdvertListByEmployeeAsyncByTrue(id);
            return Ok(value); 
        }
        [HttpGet("ProductAdvertListByEmployeeAsyncByFalse")]
        public async Task<IActionResult> ProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            var value = await _repository.GetProductAdvertListByEmployeeAsyncByFalse(id);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult > CreateProduct(CreateProductDto createProductDto)
        {
            await _repository.CreateProduct(createProductDto);
            return Ok("Eklendi");
        }
        [HttpGet("GetProductByProductId")]
        public async Task<IActionResult> GetProductByProductId(int id)
        {
            var values= await _repository.GetProductByProductId(id);
            return Ok(values);
        }
        [HttpGet("GetProductDetailByID")]
        public async Task<IActionResult> GetProductDetailByID(int id)
        {
            var values = await _repository.GetProductDetailByID(id);
            return Ok(values);
        }
        [HttpGet("ResultProductWithSearchList")]
        public async Task<IActionResult> ResultProductWithSearchList(string searchKeyValue, int propertyCategoryID, string city)
        {
            var value=await _repository.resultProductWithSearchListDtos(searchKeyValue, propertyCategoryID, city);
            return Ok(value);
        }
        [HttpGet("ProductByDealOfTheDayTrueProductWithCategoryAsync")]
        public async Task<IActionResult> GetProductByDealOfTheDayTrueProductWithCategoryAsync()
        {
            var value = await _repository.GetProductByDealOfTheDayTrueProductWithCategoryAsync();
            return Ok(value);
        }
        [HttpGet ("Last3ProductAsync")]
        public async Task<IActionResult> GetLast3ProductAsync()
        {
            var value = await _repository.GetLast3ProductAsync();
            return Ok(value);
        }
    }
}
