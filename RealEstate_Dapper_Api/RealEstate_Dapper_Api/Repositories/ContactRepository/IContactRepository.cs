using RealEstate_Dapper_Api.Dtos.ContactDtos;

namespace RealEstate_Dapper_Api.Repositories.ContactRepository
{
    public interface IContactRepository
    {
        Task<List<ResultContactDto>> GetAllContact();
        void CreateContact(CreateContactDtos ContactDto);
        void DeleteContact(int id);
        Task<GetByIDContactDto> GetContact(int id);
        Task<List<Last4ContactResultDto>> GetLast4Contact();
    }
}
