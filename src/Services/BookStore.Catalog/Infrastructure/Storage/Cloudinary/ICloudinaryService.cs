using Ardalis.Result;
using BookStore.Catalog.Infrastructure.Storage.Abstractions;

namespace BookStore.Catalog.Infrastructure.Storage.Cloudinary;

public interface ICloudinaryService
{
    Task<Result<CloudinaryResult>> AddPhotoAsync(IFormFile? file);
    Task<Result<string>> DeletePhotoAsync(string publicId);
}
