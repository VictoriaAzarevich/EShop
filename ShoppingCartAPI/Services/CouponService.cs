using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Services
{
    public class CouponService(HttpClient httpClient) : ICouponService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<CouponDto?> GetCouponByCodeAsync(string couponCode)
        {
            var response = await _httpClient.GetAsync($"/api/coupon/{couponCode}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CouponDto>();
            }

            return null;
        }
    }
}
