using Dapper;
using RealEstate_Dapper_Api.Dtos.ContactDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ContactRepository
{
    public class ContactRepository : IContactRepository
    {
        private readonly Context _context;

        public ContactRepository(Context context)
        {
            _context = context;
        }
        public async void CreateContact(CreateContactDtos ContactDto)
        {
            string query = "insert into Contact (Name,Subject, Email, Message,SendDate ) values (@ContactName,@ContactSubject,@ContactEmail,@ContactMessage,@ContactSendDate)";
            var parameters = new DynamicParameters();
            parameters.Add("@ContactName", ContactDto.Name);
            parameters.Add("@ContactSubject", ContactDto.Subject);
            parameters.Add("@ContactEmail", ContactDto.Email);
            parameters.Add("@ContactMessage", ContactDto.Message);
            parameters.Add("@ContactSendDate", ContactDto.SendDate);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteContact(int id)
        {
            string query = "Delete from Contact Where ContactID=@ContactID";
            var parameters = new DynamicParameters();
            parameters.Add("@ContactID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultContactDto>> GetAllContact()
        {
            string query = "Select * From Contact";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultContactDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDContactDto> GetContact(int id)
        {
            string query = "Select * From Contact Where ContactID=@ContactID";
            var parameters = new DynamicParameters();
            parameters.Add("@ContactID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDContactDto>(query, parameters);

                return values;
            }
        }

        public async Task<List<Last4ContactResultDto>> GetLast4Contact()
        {
            string query = "Select Top(4) * from Contact Order By ContactID Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<Last4ContactResultDto>(query);

                return values.ToList();
            }
        }

      
    }
}
