using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Pathmaker.Application.Services.Files;

namespace Pathmaker.Infrastructure.Services.Files;

public class FileService : IFileService {
    private readonly IAmazonS3 _s3;
    private readonly AwsOptions _options;
    public FileService(IAmazonS3 s3, IOptions<AwsOptions> options ) {
        _s3 = s3;
        _options = options.Value;
    }
    public async Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file) {
        var putObjectRequest = new PutObjectRequest {
            BucketName = _options.BucketName,
            Key = $"{_options.Environment}/temp/{id}",
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
