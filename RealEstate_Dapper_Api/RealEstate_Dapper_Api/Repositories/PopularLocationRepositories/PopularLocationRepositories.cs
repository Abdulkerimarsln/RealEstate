using Dapper;
using RealEstate_Dapper_Api.Dtos.PopularLocationDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.PopularLocationRepositories
{
    public class PopularLocationRepositories : IPopularLocationRepository
    {
        private readonly Context _context;

        public PopularLocationRepositories(Context context)
        {
            _context = context;
        }
        public async void CreatePopularLocation(CreatePopularLocationDto PopularLocationDto)
        {
            var query = "insert into PopularLocation (CityName, ImageUrl) values (@p1, @p2)";
            var parameters = new DynamicParameters();
            parameters.Add("@PopularLocationCityName", PopularLocationDto.CityName);
            parameters.Add("@PopularLocationImageUrl", PopularLocationDto.ImageUrl);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = PopularLocationDto.CityName,
                    p2 = PopularLocationDto.ImageUrl,
                });

            }
        }

        public async void DeletePopularLocation(int id)
        {
            var query = "delete from PopularLocation where LocationID=@p1";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = id
                });

            }
        }

        public async Task<List<ResultPopularLocationDto>> GetAllPopularLocationAsync()
        {
            string query = "Select * From PopularLocation";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultPopularLocationDto>(query);
                return values.ToList();

            }
        }

      

        public async Task<GetByIDPopularLocationDto> GetPopularLocation(int id)
        {
            var query = "Select * from PopularLocation Where LocationID=@PopularLocationID";
            var parameters = new DynamicParameters();
            parameters.Add("@PopularLocationID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDPopularLocationDto>(query, parameters);
                return values;
            }
        }

        public async void UpdatePopularLocation(UpdatePopularLocationDto PopularLocationDto)
        {

            string query = "Update PopularLocation Set CityName=@p1, ImageUrl=@p2 Where LocationID=@p4";
            var parameters = new DynamicParameters();
            parameters.Add("@PopularLocationCityName", PopularLocationDto.CityName);
            parameters.Add("@PopularLocationImage", PopularLocationDto.ImageUrl);
           
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p1 = PopularLocationDto.CityName,
                    p2 = PopularLocationDto.ImageUrl,
                    p4 = PopularLocationDto.LocationID
                });
            }
        }

       
    }
}
