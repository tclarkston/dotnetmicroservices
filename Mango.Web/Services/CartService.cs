using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.IServices;
using System.Net.Http;
using System.Reflection;

namespace Mango.Web.Services
{
    public class CartService : BaseService, ICartService
    {
        public CartService(IHttpClientFactory httpClient) : base(httpClient)
        {

        }

        public async Task<T> AddToCartAsync<T>(CartDto model, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = model,
                Url = $"{SD.CartAPIBase}/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CartAPIBase}/api/cart/GetCart/{userId}",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartId,
                Url = $"{SD.CartAPIBase}/api/cart/RemoveCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto model, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = model,
                Url = $"{SD.CartAPIBase}/api/cart/UpdateCart",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = $"{SD.CartAPIBase}/api/cart/ApplyCoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveCouponAsync<T>(string userId, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = userId,
                Url = $"{SD.CartAPIBase}/api/cart/RemoveCoupon",
                AccessToken = token
            });
        }
    }
}
