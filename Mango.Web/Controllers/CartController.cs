﻿using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
        }
        public IActionResult CartIndex()
        {
            return View();
        }
    }
}