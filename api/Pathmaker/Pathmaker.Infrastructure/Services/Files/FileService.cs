using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Pathmaker.Application.Services.Files;

namespace Pathmaker.Infrastructure.Services.Files;

public class FileService : IFileService {
    private readonly IAmazonS3 _s3;
    // TODO wywalić do konfiguracji nazwe bucketu
    private readonly string _bucketName = "pathmaker";

    public FileService(IAmazonS3 s3) {
        _s3 = s3;
    }
    public async Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file) {
        // TODO jak nazwać folder?
        var putObjectRequest = new PutObjectRequest {
            BucketName = _bucketName,
            Key = $"temp/{id}",
            ContentType = file.ContentType,
            InputStream = file.OpenReadStream(),
            Metadata = {
                ["x-amz-meta-originalname"] = file.FileName,
                ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
            }
        };
        return await _s3.PutObjectAsync(putObjectRequest);
    }

    public Task<GetObjectResponse> GetImageAsync(Guid id) {
        throw new NotImplementedException();
    }

    public Task<DeleteObjectResponse> DeleteImageAsync(Guid id) {
        throw new NotImplementedException();
    }
}
