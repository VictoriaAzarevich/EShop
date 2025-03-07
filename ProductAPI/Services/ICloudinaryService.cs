namespace ProductAPI.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile image);
        Task DeleteImageAsync(string publicId);
    }
}
