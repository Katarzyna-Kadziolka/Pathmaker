using MediatR;
using Pathmaker.Application.Behaviour.Exceptions;
using Pathmaker.Application.Services.Files;

namespace Pathmaker.Application.Requests.Files.Commands.CreateFile;

public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, CreateFileResponse> {
    private readonly IFileService _fileService;

    public CreateFileCommandHandler(IFileService fileService) {
        _fileService = fileService;
    }

    public async Task<CreateFileResponse> Handle(CreateFileCommand request, CancellationToken cancellationToken) {
        var response = await _fileService.UploadImageAsync(request.File);

        if (!response.IsSuccess) {
            throw new ExternalServiceFailureException();
        }

        return new CreateFileResponse {
            FileId = response.FileId
        };
    }
}
