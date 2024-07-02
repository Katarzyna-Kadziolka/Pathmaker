using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pathmaker.Application.Services.Files;

namespace Pathmaker.Infrastructure.Services.Files;

public class FileService : IFileService {
    private readonly IAmazonS3 _s3;
    private readonly ILogger<FileService> _logger;
    private readonly AwsOptions _options;

    public FileService(IAmazonS3 s3, IOptions<AwsOptions> options, ILogger<FileService> logger) {
        _s3 = s3;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<FileServiceResult> UploadImageAsync(IFormFile file) {
        var id = Guid.NewGuid();
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
        var response = await _s3.PutObjectAsync(putObjectRequest);
        var result = new FileServiceResult();
        if (response.HttpStatusCode != HttpStatusCode.OK) {
            _logger.LogError("Error while uploading the file: {error}", response);
            result.IsSuccess = false;
        }
        else {
            result.IsSuccess = true;
        }

        result.FileId = id;

        return result;
    }

    public Task<GetObjectResponse> GetImageAsync(Guid id) {
        throw new NotImplementedException();
    }

    public Task<DeleteObjectResponse> DeleteImageAsync(Guid id) {
        throw new NotImplementedException();
    }
}
