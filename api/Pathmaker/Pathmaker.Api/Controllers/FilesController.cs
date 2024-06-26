using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pathmaker.Application.Requests.Files.Commands.CreateFile;

namespace Pathmaker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/images")]
public class FilesController : ControllerBase {
    private readonly IMediator _mediator;
    
    public FilesController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateFileResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateFileResponse>> Create(CreateFileCommand command) {
        return await _mediator.Send(command);
    }
}
