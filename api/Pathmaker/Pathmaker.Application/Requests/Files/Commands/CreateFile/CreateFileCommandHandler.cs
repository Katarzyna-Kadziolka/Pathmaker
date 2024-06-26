using System.Net;
using MediatR;
using Microsoft.Extensions.Logging;
using Pathmaker.Application.Behaviour.Exceptions;
using Pathmaker.Application.Services.Files;

namespace Pathmaker.Application.Requests.Files.Commands.CreateFile;

public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, CreateFileResponse> {
    private readonly IFileService _fileService;
    private readonly ILogger<CreateFileCommandHandler> _logger;

    public CreateFileCommandHandler(IFileService fileService ,ILogger<CreateFileCommandHandler> logger) {
        _fileService = fileService;
        _logger = logger;
    }

    public async Task<CreateFileResponse> Handle(CreateFileCommand request, CancellationToken cancellationToken) {
        var id = Guid.NewGuid();
        var response = await _fileService.UploadImageAsync(id, request.file);

        if (response.HttpStatusCode != HttpStatusCode.OK) {
            _logger.LogError("Error while uploading the file: {error}", response);
            throw new ExternalServiceFailureException();
;
        }

        return new CreateFileResponse {
            FileId = id
        };
    }
}
