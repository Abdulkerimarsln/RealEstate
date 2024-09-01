using Dapper;
using RealEstate_Dapper_Api.Dtos.Service;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ServiceRepository
{
    public class ServiceRepository:IServiceRepository
    {
        private readonly Context _context;

        public ServiceRepository(Context context)
        {
            _context = context;
        }

        public async void CreateService(CreateServiceDto serviceDto)
        {
            var query= "insert into Service (ServiceName, ServiceStatus) values (@p1, @p2)";
            var parameters = new DynamicParameters();
            parameters.Add("@serviceName", serviceDto.ServiceName);
            parameters.Add("@serviceStatus", serviceDto.ServiceStatus);
            using(var connection =_context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1=serviceDto.ServiceName,
                    p2=serviceDto.ServiceStatus
                }
                );
            }

        }

        public async void DeleteService(int id)
        {
            var query = "delete from Service where ServiceID=@p1";
            var parameters = new DynamicParameters();
            using(var connection =_context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1=id
                });

            }
        }

        public async Task<List<ResultServiceDto>> GetAllServiceAsync()
        {
            string query = "Select * From Service";
            using(var connection =_context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultServiceDto>(query);
                return values.ToList();

            }
        }

        public async Task<GetByIDServiceDto> GetService(int id)
        {
            var query = "Select * from Service Where ServiceID=@serviceID";
            var parameters = new DynamicParameters();
            parameters.Add("@serviceID", id);
            using( var connection =_context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDServiceDto>(query, parameters);
                return values;
            }
        }

        public async void UpdateService(UpdateServiceDto serviceDto)
        {
            string query = "Update Service Set ServiceName=@p1, ServiceStatus=@p2 Where ServiceID=@p3";
            var parameters = new DynamicParameters();
            parameters.Add("@serviceName", serviceDto.ServiceName);
            parameters.Add("@serviceStatus", serviceDto.ServiceStatus);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = serviceDto.ServiceName,
                    p2 = serviceDto.ServiceStatus,
                    p3 = serviceDto.ServiceID
                });
            }
        }
    }
}
