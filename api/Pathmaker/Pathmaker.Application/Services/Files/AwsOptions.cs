namespace Pathmaker.Application.Services.Files;

public class AwsOptions {
    public const string SectionName = "AWS";
    public string BucketName { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string ServiceUrl { get; set; } = string.Empty;

}
