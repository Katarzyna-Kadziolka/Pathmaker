using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pathmaker.Infrastructure.Services.Images;

namespace Pathmaker.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/images")]
public class ImagesController : ControllerBase {
    private readonly IMediator _mediator;
    private readonly IImagesService _imagesService;
    // TODO dodać cały CQRS, wyrzucić stad imageService
    
    public ImagesController(IMediator mediator, IImagesService imagesService) {
        _mediator = mediator;
        _imagesService = imagesService;
    }

    [HttpPost("images/{id:guid}")]
    public async Task<IActionResult> Upload([FromRoute] Guid id, [FromForm] IFormFile file) {
        var response = await _imagesService.UploadImageAsync(id, file);
        if (response.HttpStatusCode == HttpStatusCode.OK) {
            return Ok();
        }

        return BadRequest();
    }
}
