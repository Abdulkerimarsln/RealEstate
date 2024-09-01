using Dapper;
using RealEstate_Dapper_Api.Dtos.ToDoListDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ToDoListRepository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly Context _context;

        public ToDoListRepository(Context context)
        {
            _context = context;
        }

        public async void CreateToDoList(CreateToDoListDto ToDoListDto)
        {
            string query = "insert into ToDoList (Description, ToDoListStatus) values (@Description, @ToDoListStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@Description", ToDoListDto.Description);
            parameters.Add("@ToDoListStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteToDoList(int id)
        {
            string query = "Delete From ToDoList Where ToDoListID=@ToDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@ToDoListID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultToDoListDto>> GetAllToDoListAsync()
        {
            string query = "Select * From ToDoList";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultToDoListDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDToDoListDto> GetToDoList(int id)
        {
            string query = "Select * From ToDoList Where ToDoListID=@ToDoListID";
            var parameters = new DynamicParameters();
            parameters.Add("@ToDoListID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDToDoListDto>(query, parameters);
                return values;
            }
        }

        public async void UpdateToDoList(UpdateToDoListDto ToDoListDto)
        {
            string query = "Update ToDoList Set Description=@p1, ToDoListStatus=@p2 Where ToDoListID=@p3";
            var parameters = new DynamicParameters();
            parameters.Add("@Description", ToDoListDto.Description);
            parameters.Add("@ToDoListStatus", ToDoListDto.ToDoListStatus);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 =ToDoListDto.Description,
                    p2 = ToDoListDto.ToDoListStatus,
                    p3 = ToDoListDto.ToDoListID
                });
            }
        }
    }
}
