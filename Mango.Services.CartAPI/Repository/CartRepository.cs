using AutoMapper;
using Mango.Services.CartAPI.DbContexts;
using Mango.Services.CartAPI.Models;
using Mango.Services.CartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartModel)
        {
            Cart cart = _mapper.Map<Cart>(cartModel);
            var product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == cartModel.CartDetails.FirstOrDefault().ProductId);
            if (product == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();

            }

            var cartHeader = _db.CartHeaders.AsNoTracking().FirstOrDefault(u => u.UserId == cart.CartHeader.UserId);
            if (cartHeader == null)
            {
                _db.CartHeaders.Add(cart.CartHeader);
                await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                var cartDetails = _db.CartDetails.AsNoTracking().FirstOrDefault(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeader.CartHeaderId);

                if (cartDetails == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Count = cartDetails.Count;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);
        }

        public Task<CartDto> GetCartByUserid(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCart(int cartDetailsId)
        {
            throw new NotImplementedException();
        }

        //    public async Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto)
        //    {
        //        Product product = _mapper.Map<Product>(productDto);
        //        if (product.ProductId > 0)
        //        {
        //            _db.Products.Update(product);
        //        }
        //        else
        //        {
        //            _db.Products.Add(product);
        //        }
        //        await _db.SaveChangesAsync();
        //        return _mapper.Map<Product, ProductDto>(product);
        //    }

        //    public async Task<bool> DeleteProductAsync(int productId)
        //    {
        //        try
        //        {
        //            Product product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        //            if (product == null)
        //            {
        //                return false;
        //            }
        //            _db.Products.Remove(product);
        //            await _db.SaveChangesAsync();

        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }

        //    public async Task<ProductDto> GetProductByIdAsync(int productId)
        //    {
        //        Product product = await _db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        //        return _mapper.Map<ProductDto>(product);
        //    }

        //    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        //    {
        //        List<Product> productList = await _db.Products.ToListAsync();
        //        return _mapper.Map<List<ProductDto>>(productList);
        //    }
        //}
    }
