﻿using Mango.Web.Models;
using Mango.Web.Services.IServices;
using static Mango.Web.SD;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory httpClient) : base(httpClient)
        {
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Data= product,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                Url = $"{SD.ProductAPIBase}/api/products/{id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{SD.ProductAPIBase}/api/products/{id}",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = $"{SD.ProductAPIBase}/api/products/",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto product)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.PUT,
                Data = product,
                Url = $"{SD.ProductAPIBase}/api/products/",
                AccessToken = ""
            });
        }
    }
}
