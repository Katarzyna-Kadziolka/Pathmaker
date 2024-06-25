using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pathmaker.Application.Requests.Images.Commands.CreateImage;

namespace Pathmaker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/images")]
public class ImagesController : ControllerBase {
    private readonly IMediator _mediator;
    
    public ImagesController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost("images/{id:guid}")]
    public async Task<Guid> Create(CreateImageCommand command) {
        return await _mediator.Send(command);
    }
    // TODO po co zwracać IActionResult?
}
