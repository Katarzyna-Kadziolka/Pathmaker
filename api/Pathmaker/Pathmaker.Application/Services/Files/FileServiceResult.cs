namespace Pathmaker.Application.Services.Files;

public record FileServiceResult {
    public bool IsSuccess { get; set; }
    public Guid FileId { get; set; }
}
