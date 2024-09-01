using RealEstate_Dapper_Api.Dtos.ProductImageDtos;

namespace RealEstate_Dapper_Api.Repositories.ImageRepository
{
    public interface IProductImageRepositories
    {
        Task<List<GetProductImageByProductIDDto>> getProductImageByProduct(int id);

    }
}
