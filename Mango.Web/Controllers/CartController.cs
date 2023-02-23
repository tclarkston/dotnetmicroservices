using Mango.Web.Models.Dto;
using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;

        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartForUser());
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var userId = User.Claims.Where(u => u.Type == "sub").FirstOrDefault()?.Value;
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.ApplyCouponAsync<ResponseDto>(cartDto, token);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View(cartDto);
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveCouponAsync<ResponseDto>(cartDto.CartHeader.UserId, token);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View(cartDto);
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == "sub").FirstOrDefault()?.Value;
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsId, token);

            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(CartIndex));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartForUser());
        }

        private async Task<CartDto> LoadCartForUser()
        {
            var userId = User.Claims.Where(u => u.Type == "sub").FirstOrDefault()?.Value;
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, token);

            CartDto cartDto = new();
            if (response != null && response.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }

            if (cartDto.CartHeader!= null)
            {
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCouponAsync<ResponseDto>(cartDto.CartHeader.CouponCode, token);
                    if (coupon != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(coupon.Result));
                        cartDto.CartHeader.DiscountTotal = couponObj.DiscountAmount;
                    }
                }
                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }

                cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;
            }

            return cartDto;
        }
    }
}
