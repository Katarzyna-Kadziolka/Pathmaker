using MediatR;
using Microsoft.AspNetCore.Http;

namespace Pathmaker.Application.Requests.Images.Commands.CreateImage;

public class CreateImageCommand : IRequest<Guid> {
    public IFormFile file { get; set; }
}
