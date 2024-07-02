using MediatR;
using Microsoft.AspNetCore.Http;

namespace Pathmaker.Application.Requests.Files.Commands.CreateFile;

public class CreateFileCommand : IRequest<CreateFileResponse> {
    public IFormFile File { get; set; } = null!;
}
