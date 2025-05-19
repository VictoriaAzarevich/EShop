using ShoppingCartAPI.Models.Dto;

namespace ShoppingCartAPI.Services
{
    public class CouponServiceClient(HttpClient httpClient) : ICouponServiceClient
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
