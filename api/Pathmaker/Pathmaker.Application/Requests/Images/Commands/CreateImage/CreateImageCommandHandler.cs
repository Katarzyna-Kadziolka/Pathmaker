using System.Net;
using MediatR;
using Pathmaker.Application.Services.Images;

namespace Pathmaker.Application.Requests.Images.Commands.CreateImage;

public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, Guid> {
    private readonly IImagesService _imagesService;

    public CreateImageCommandHandler(IImagesService imagesService) {
        _imagesService = imagesService;
    }

    public async Task<Guid> Handle(CreateImageCommand request, CancellationToken cancellationToken) {
        var id = Guid.NewGuid();
        var response = await _imagesService.UploadImageAsync(id, request.file);
        return id;
        // TODO jak obsłużyć błędy?
    }
}
