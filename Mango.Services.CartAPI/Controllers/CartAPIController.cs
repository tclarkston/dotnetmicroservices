using Mango.Services.CartAPI.Models.Dto;
using Mango.Services.CartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mango.Services.CartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartApiController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly ResponseDto _response;

        public CartApiController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            _response = new();
        }

        [HttpGet]
        [Route("GetCart/{userId}")]

        public async Task<object> GetCart(string userId)
        {
            try
            {
                var cartDto = await _cartRepository.GetCartByUserIdAsync(userId);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto model)
        {
            try
            {
                var cartDto = await _cartRepository.CreateUpdateCartAsync(model);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>{ ex.ToString() };
            }

            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto model)
        {
            try
            {
                var cartDto = await _cartRepository.CreateUpdateCartAsync(model);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody]int cartId)
        {
            try
            {
                var isSuccess = await _cartRepository.RemoveFromCartAsync(cartId);
                _response.Result = isSuccess;
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
