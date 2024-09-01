using Dapper;
using RealEstate_Dapper_Api.Dtos.EmployeeDtos;
using RealEstate_Dapper_Api.Dtos.WhoWeAreDetailRepositoryDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public async void CreateEmployee(CreateEmployeeDto employeeDto)
        {
            string query = "insert into Employee (Name,Title, Mail, PhoneNumber,ImageUrl ,Status) values (@employeeName,@employeeTitle,@employeeMail,@employeePhoneNumber,@employeeImageUrl, @employeeStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeName", employeeDto.Name);
            parameters.Add("@employeeTitle", employeeDto.Title);
            parameters.Add("@employeeMail", employeeDto.Mail);
            parameters.Add("@employeePhoneNumber", employeeDto.PhoneNumber);
            parameters.Add("@employeeImageUrl", employeeDto.ImageUrl);
            parameters.Add("@employeeStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteEmployee(int id)
        {
            string query = "Delete from Employee Where EmployeeId=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultEmployeeDto>> GetAllEmployee()
        {
            string query = "Select * From Employee";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDEmployeeDto> GetEmployee(int id)
        {
            string query = "Select * From Employee Where EmployeeId=@employeeId";
            var parameters = new DynamicParameters();
            parameters.Add("@employeeId", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDEmployeeDto>(query, parameters);

                return values;
            }
        }

        public async void UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            string query = "Update Employee Set Name=@p1 , Title=@p2 , Mail=@p3 , PhoneNumber=@p4 ,ImageUrl=@p5, Status=@p6 Where EmployeeId=@p7";
            var parameters = new DynamicParameters();
            parameters.Add("@name", employeeDto.Name);
            parameters.Add("@title", employeeDto.Title);
            parameters.Add("@mail",employeeDto.Mail);
            parameters.Add("@phoneNumber",employeeDto.PhoneNumber);
            parameters.Add("@imageUrl", employeeDto.ImageUrl);
            parameters.Add("@status", employeeDto.Status);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = employeeDto.Name,
                    p2 = employeeDto.Title,
                    p3 = employeeDto.Mail,
                    p4 = employeeDto.PhoneNumber,
                    p5 = employeeDto.ImageUrl,
                    p6 = employeeDto.Status,
                    p7 = employeeDto.EmployeeId
                });
            }
        }
    }
}
