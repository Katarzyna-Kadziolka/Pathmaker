using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Pathmaker.Infrastructure.Services.Images;

public interface IImagesService {
    Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file);
    Task<GetObjectResponse> GetImageAsync(Guid id);
    Task<DeleteObjectResponse> DeleteImageAsync(Guid id);
}
