using Mango.Web.Models.Dto;

namespace Mango.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetProductsAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int id, string token);
        Task<T> CreateProductAsync<T>(ProductDto product, string token);
        Task<T> UpdateProductAsync<T>(ProductDto product, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);

    }
}
