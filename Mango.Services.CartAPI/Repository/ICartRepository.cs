using Mango.Services.CartAPI.Models.Dto;

namespace Mango.Services.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartByUserid(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cart);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);
        //}
    }
