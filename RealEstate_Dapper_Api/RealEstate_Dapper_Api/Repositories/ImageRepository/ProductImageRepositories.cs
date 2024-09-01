using Dapper;
using RealEstate_Dapper_Api.Dtos.ProductImageDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ImageRepository
{
    public class ProductImageRepositories : IProductImageRepositories
    {
        private readonly Context _context;

        public ProductImageRepositories(Context context)
        {
            _context = context;
        }

        public async Task<List<GetProductImageByProductIDDto>> getProductImageByProduct(int id)
        {
            string query = "Select * From ProductImage Where ProductID=@productID";
            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductImageByProductIDDto>(query, parameters);
                return values.ToList();
            }
        }
    }
}
