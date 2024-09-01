using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Dtos.ToDoListDtos;
using RealEstate_Dapper_Api.Repositories.ToDoListRepository;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository;

        public ToDoListController(IToDoListRepository toDoListRepository)
        {
            _toDoListRepository = toDoListRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ToDoListList()
        {
            var values = await _toDoListRepository.GetAllToDoListAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateToDoList(CreateToDoListDto createtoDoListDto)
        {
            _toDoListRepository.CreateToDoList(createtoDoListDto);
            return Ok("Kategori başarılı bir şekilde eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetoDoList(int id)
        {
            _toDoListRepository.DeleteToDoList(id);
            return Ok("Kategorili başarılı bir şekilde silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdatetoDoList(UpdateToDoListDto updatetoDoListDto)
        {
            _toDoListRepository.UpdateToDoList(updatetoDoListDto);
            return Ok("Kategori başarılı bir şekilde güncellendi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoList(int id)
        {
            var value = await _toDoListRepository.GetToDoList(id);
            return Ok(value);
        }
    }
}
