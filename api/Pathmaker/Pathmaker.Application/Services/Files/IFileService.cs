using Microsoft.AspNetCore.Http;

namespace Pathmaker.Application.Services.Files;

public interface IFileService {
    Task<FileServiceResult> UploadImageAsync(IFormFile file);
    // TODO odpowiednie typy response
    // Task<GetObjectResponse> GetImageAsync(Guid id);
    // Task<DeleteObjectResponse> DeleteImageAsync(Guid id);
}
