namespace Pathmaker.Application.Services.Files;

public class FileServiceOptions {
    public const string FileService = nameof(FileService);
    public string BucketName { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
}
