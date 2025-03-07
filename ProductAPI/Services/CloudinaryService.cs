using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace ProductAPI.Services
{
    public class CloudinaryService(Cloudinary cloudinary) : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary = cloudinary;

        public async Task DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            await _cloudinary.DestroyAsync(deletionParams);
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            await using var stream = image.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, stream),
                PublicId = Guid.NewGuid().ToString()
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
    }
}
