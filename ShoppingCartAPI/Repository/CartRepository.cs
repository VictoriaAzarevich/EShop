﻿using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.DBContext;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Repository
{
    public class CartRepository (ApplicationDbContext dbContext) : ICartRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        
        public async Task ApplyCoupon(string userId, string couponCode)
        {
            var cartHeader = await _dbContext.CartHeaders
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (cartHeader == null)
            {
                throw new KeyNotFoundException("Cart not found");
            }

            cartHeader.CouponCode = couponCode;
            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearCart(string userId)
        {
            var cartHeader = await _dbContext.CartHeaders
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (cartHeader == null)
            {
                throw new KeyNotFoundException("Cart not found");
            }

            var cartDetails = _dbContext.CartDetails
                .Where(cd => cd.CartHeaderId == cartHeader.CartHeaderId);

            _dbContext.CartDetails.RemoveRange(cartDetails);
            _dbContext.CartHeaders.Remove(cartHeader);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Cart> CreateUpdateCart(Cart cart)
        {
            if (cart.CartDetails == null || !cart.CartDetails.Any())
            {
                throw new ArgumentException("Cart details cannot be empty.");
            }

            var productIds = cart.CartDetails.Select(cd => cd.ProductId).ToList();
            var existingProducts = await _dbContext.Products
                .Where(p => productIds.Contains(p.ProductId))
                .Select(p => p.ProductId)
                .ToListAsync();

            if (existingProducts.Count != productIds.Count)
            {
                throw new KeyNotFoundException("One or more products not found.");
            }

            var cartHeader = await _dbContext.CartHeaders
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
            {
                _dbContext.CartHeaders.Add(cart.CartHeader);
                await _dbContext.SaveChangesAsync();
                cartHeader = await _dbContext.CartHeaders
                    .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            }

            foreach (var cartDetail in cart.CartDetails)
            {
                var existingCartDetail = await _dbContext.CartDetails
                    .FirstOrDefaultAsync(cd => cd.ProductId == cartDetail.ProductId && cd.CartHeaderId == cartHeader.CartHeaderId);

                if (existingCartDetail == null)
                {
                    cartDetail.CartHeaderId = cartHeader.CartHeaderId;
                    _dbContext.CartDetails.Add(cartDetail);
                }
                else
                {
                    existingCartDetail.Count += cartDetail.Count;
                    _dbContext.CartDetails.Update(existingCartDetail);
                }
            }

            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            var cartHeader = await _dbContext.CartHeaders
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (cartHeader == null)
            {
                throw new KeyNotFoundException("Cart Header not found");
            }

            var cartDetails = await _dbContext.CartDetails
                .Where(cd => cd.CartHeaderId == cartHeader.CartHeaderId)
                .Include(cd => cd.Product) 
                .AsNoTracking()
                .ToListAsync();

            return new Cart
            {
                CartHeader = cartHeader,
                CartDetails = cartDetails
            };
        }

        public async Task RemoveCoupon(string userId)
        {
            var cartHeader = await _dbContext.CartHeaders
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (cartHeader == null)
            {
                throw new KeyNotFoundException("Cart Header not found");
            }

            if (!string.IsNullOrEmpty(cartHeader.CouponCode))
            {
                cartHeader.CouponCode = null;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCart(int cartDetailsId)
        {
            CartDetails cartDetails = await _dbContext.CartDetails
                .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

            if (cartDetails == null)
            {
                throw new KeyNotFoundException("Cart details not found");
            }

            bool isLastItem = !await _dbContext.CartDetails
            .AnyAsync(u => u.CartHeaderId == cartDetails.CartHeaderId && u.CartDetailsId != cartDetailsId);

            _dbContext.CartDetails.Remove(cartDetails);

            if (isLastItem)
            {
                var cartHeaderToRemove = await _dbContext.CartHeaders
                    .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                if (cartHeaderToRemove != null)
                {
                    _dbContext.CartHeaders.Remove(cartHeaderToRemove);
                }
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
