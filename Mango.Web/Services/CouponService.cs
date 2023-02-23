using Mango.Web.Models;
using Mango.Web.Models.Dto;
using Mango.Web.Services.IServices;
using System.Net.Http;
using System.Reflection;

namespace Mango.Web.Services
{
    public class CouponService : BaseService, ICouponService
    {
        public CouponService(IHttpClientFactory httpClient) : base(httpClient)
        {

        }

        public async Task<T> GetCouponAsync<T>(string couponCode, string token = null)
        {
            return await SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponAPIBase}/api/coupon/{couponCode}",
                AccessToken = token
            });
        }
    }
}
