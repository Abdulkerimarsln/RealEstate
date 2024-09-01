using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate_Dapper_Api.Repositories.ContactRepository;
using RealEstate_Dapper_Api.Dtos.ContactDtos;

namespace RealEstate_Dapper_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpGet]
        public async Task<IActionResult> contactList()
        {
            var values = await _contactRepository.GetAllContact();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> Createcontact(CreateContactDtos createcontactDto)
        {
            _contactRepository.CreateContact(createcontactDto);
            return Ok("Başarılı bir şekilde eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecontact(int id)
        {
            _contactRepository.DeleteContact(id);
            return Ok("Başarılı bir şekilde silindi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getcontact(int id)
        {
            var value = await _contactRepository.GetContact(id);
            return Ok(value);
        }
        [HttpGet("GetLast4Contact")]
        public async Task<IActionResult> GetLast4Contact()
        {
            var value = await _contactRepository.GetLast4Contact();
            return Ok(value);
        }
    }
}
