using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponAPIController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ResponseDto _response;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
            _response = new();
        }

        [HttpGet]
        [Route("{couponCode}")]

        public async Task<object> GetDiscountForCode(string couponCode)
        {
            try
            {
                var couponDto = await _couponRepository.GetCouponByCode(couponCode);
                _response.Result = couponDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }
    }
}
