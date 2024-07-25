using Hangfire;
using Microsoft.Extensions.Logging;

namespace Pathmaker.Infrastructure.Services.Files;

public class FileCleanupJob {
    private readonly ILogger<FileCleanupJob> _logger;

    public FileCleanupJob(ILogger<FileCleanupJob> logger) {
        _logger = logger;
    }

    public void Cleanup() {
        _logger.LogInformation("Test");
    }
}
