using Mango.Web.Models.Dto;

namespace Mango.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddToCartAsync<T>(CartDto model, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto model, string token = null);
        Task<T> RemoveFromCartAsync<T>(int cartIdl, string token = null);
    }
}
