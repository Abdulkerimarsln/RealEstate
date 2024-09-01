using Dapper;
using RealEstate_Dapper_Api.Dtos.BottomGridDtos;
using RealEstate_Dapper_Api.Dtos.Service;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.BottomGridRepositories
{
    public class BottomGridRepository : IBottomGridRepository
    {
        private readonly Context _context;

        public BottomGridRepository(Context context)
        {
            _context = context;
        }
        public async Task CreateBottomGrid(CreateBottomGridDto bottomGridDto)
        {
            var query = "insert into BottomGrid (Icon, Title, Description) values (@p1, @p2, @p3)";
            var parameters = new DynamicParameters();
            parameters.Add("@BottomGridIcon", bottomGridDto.Icon);
            parameters.Add("@BottomGridTitle", bottomGridDto.Title);
            parameters.Add("@BottomGridDescription", bottomGridDto.Description);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = bottomGridDto.Icon,
                    p2 = bottomGridDto.Title,
                    p3 = bottomGridDto.Description,
                });
                
            }
        }

        public async Task DeleteBottomGrid(int id)
        {
            var query = "delete from BottomGrid where BottomGridID=@p1";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = id
                });

            }
        }

        public async Task<List<ResultBottomGridDto>> GetAllBottomGridAsync()
        {
            string query = "Select * From BottomGrid";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultBottomGridDto>(query);
                return values.ToList();

            }
        }

        public async Task<GetBottomGridDto> GetBottomGrid(int id)
        {
            var query = "Select * from BottomGrid Where BottomGridID=@BottomGridID";
            var parameters = new DynamicParameters();
            parameters.Add("@BottomGridID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetBottomGridDto>(query, parameters);
                return values;
            }
        }

        public async Task UpdateBottomGrid(UpdateBottomGridDto bottomGridDto)
        {

            string query = "Update BottomGrid Set Icon=@p1, Title=@p2,Description=@p3 Where BottomGridID=@p4";
            var parameters = new DynamicParameters();
            parameters.Add("@bottomGridIcon", bottomGridDto.Icon);
            parameters.Add("@bottomGridTitle", bottomGridDto.Title);
            parameters.Add("@bottomGridDescription", bottomGridDto.Description);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = bottomGridDto.Icon,
                    p2 = bottomGridDto.Title,
                    p3 = bottomGridDto.Description,
                    p4 = bottomGridDto.BottomGridID
                });
            }
        }
    }
}
