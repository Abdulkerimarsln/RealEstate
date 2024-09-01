using Dapper;
using RealEstate_Dapper_Api.Dtos.ProductDetailDtos;
using RealEstate_Dapper_Api.Dtos.ProductDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.ProductCategory
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            string query = "Select * From Product";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetAllProductWithCategoryAsync()
        {
            string query = "Select ProductID, Title,Price,City,District,CategoryName, Type, CoverImage, Address, DealOfTheDay, SlugUrl From Product inner join Category on Product.ProductCategory=Category.CategoryID";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast5ProductWithCategoryDto>> GetLast5ProductAsync()
        {
            string query = "Select Top(5) ProductID, Title ,Price ,City, District, ProductCategory , CategoryName,AdvertisementDate From Product inner join Category on Product.ProductCategory=Category.CategoryId Where Type = 'Kiralık' Order By ProductId Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast5ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByTrue(int id)
        {
            string query = "Select ProductID, Title,Price,City,District,CategoryName, Type, CoverImage, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID Where EmployeeID=@p1 and ProductStatus=1";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, new
                {
                    p1 = id,
                });
                return values.ToList();
            }
        }

        public async Task<List<ResultProductAdvertListWithCategoryByEmployeeDto>> GetProductAdvertListByEmployeeAsyncByFalse(int id)
        {
            string query = "Select ProductID, Title,Price,City,District,CategoryName, Type, CoverImage, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID Where EmployeeID=@p1 and ProductStatus=0";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductAdvertListWithCategoryByEmployeeDto>(query, new
                {
                    p1 = id,
                });
                return values.ToList();
            }
        }       

        public async void ProductDealOfTheDayStatusChangeToFalse(int id)
        {
            string query = "Update Product Set DealOfTheDay= 0 Where ProductID=@p7";
            var parameters = new DynamicParameters();

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    p7 = id
                });
            }
        }

        public async void ProductDealOfTheDayStatusChangeToTrue(int id)
        {
           
                string query = "Update Product Set DealOfTheDay= 1 Where ProductID=@p7";
                var parameters = new DynamicParameters();
             
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new
                    {                       
                        p7 = id
                    });
                }
    }

        public async Task CreateProduct(CreateProductDto createProductDto)
        {
            string query = "insert into Product (Title, Price, City,District,ProductCategory,CoverImage,Address, Destcription,Type,DealOfTheDay,AdvertisementDate,ProductStatus,EmployeeID) values (@Title, @Price, @City,@District,@ProductCategory,@CoverImage,@Address, @Destcription,@Type,@DealOfTheDay,@AdvertisementDate,@ProductStatus,@EmployeeID)";
            var parameters = new DynamicParameters();
            parameters.Add("@Title", createProductDto.Title);
            parameters.Add("@Price", createProductDto.Price);
            parameters.Add("@City", createProductDto.City);
            parameters.Add("@District", createProductDto.District);
            parameters.Add("@ProductCategory", createProductDto.ProductCategory);
            parameters.Add("@CoverImage", createProductDto.CoverImage);
            parameters.Add("@Address", createProductDto.Address);
            parameters.Add("@Destcription", createProductDto.Destcription);
            parameters.Add("@Type", createProductDto.Type);
            parameters.Add("@DealOfTheDay", createProductDto.DealOfTheDay);
            parameters.Add("@AdvertisementDate", createProductDto.AdvertisementDate);
            parameters.Add("@ProductStatus", createProductDto.ProductStatus);
            parameters.Add("@EmployeeID",createProductDto.EmployeeID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<GetProductByProductIdDto> GetProductByProductId(int id)
        {
            string query = "Select ProductID, Title,Price,City,District,CategoryName, Type, CoverImage, Address, DealOfTheDay, AdvertisementDate,Destcription, SlugUrl From Product inner join Category on Product.ProductCategory=Category.CategoryID where ProductID=@productId";
            
            var parameters = new DynamicParameters();
            parameters.Add("@productID" ,id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductByProductIdDto>(query,parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task<GetProductDetailByIDDto> GetProductDetailByID(int id)
        {
            string query = "Select * From ProductDetails where ProductId=@productId";

            var parameters = new DynamicParameters();
            parameters.Add("@productID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<GetProductDetailByIDDto>(query, parameters);
                return values.FirstOrDefault();
            }
        }

        public async Task<List<ResultProductWithSearchListDto>> resultProductWithSearchListDtos(string searchKeyValue, int propertyCategoryID, string city)
        {// Include wildcards for the LIKE clause
            string searchKey = $"%{searchKeyValue}%";

            string query = "SELECT * FROM Product WHERE Title LIKE @searchKeyValue AND ProductCategory = @productID AND City = @city";

            // Create parameters
            var parameters = new DynamicParameters();
            parameters.Add("@productID", propertyCategoryID);
            parameters.Add("@searchKeyValue", searchKey);
            parameters.Add("@city", city);

            // Execute the query
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithSearchListDto>(query, parameters);
                return values.ToList();
            }
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductByDealOfTheDayTrueProductWithCategoryAsync()
        {
            string query = "Select ProductID, Title,Price,City,District,CategoryName, Type, CoverImage, Address, DealOfTheDay From Product inner join Category on Product.ProductCategory=Category.CategoryID where DealOfTheDay=1";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultProductWithCategoryDto>(query);
                return values.ToList();
            }
        }

        public async Task<List<ResultLast3ProductWithCategoryDto>> GetLast3ProductAsync()
        {
            string query = "Select Top(3) ProductID, Title ,Price ,City, District, ProductCategory , CategoryName,AdvertisementDate,CoverImage,Destcription From Product inner join Category on Product.ProductCategory=Category.CategoryId  Order By ProductId Desc";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultLast3ProductWithCategoryDto>(query);
                return values.ToList();
            }
        }
    }
}
