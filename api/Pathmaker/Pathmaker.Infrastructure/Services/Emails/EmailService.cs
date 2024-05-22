using Pathmaker.Application.Services.Emails;

namespace Pathmaker.Infrastructure.Services.Emails;

public class EmailService : IEmailService {
    public Task SendEmail() {
        return Task.CompletedTask;
    }
}