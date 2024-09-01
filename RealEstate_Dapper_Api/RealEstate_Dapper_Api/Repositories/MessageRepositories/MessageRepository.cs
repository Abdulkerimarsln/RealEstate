using Dapper;
using RealEstate_Dapper_Api.Dtos.MessageDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.MessageRepositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly Context _context;

        public MessageRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ResultinBoxMessageDto>> GetInBoxMessageLast3MessageListByReceiver(int id)
        {
            string query = "Select Top(3) MessageID,Name,Subject,Detail, SendDate, IsRead, UserImageUrl from Message inner join AppUser on Message.Sender=AppUser.UserID where Receiver=@id order by MessageID Desc ";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultinBoxMessageDto>(query,parameters);
                return values.ToList();
            }
        }
    }
}
