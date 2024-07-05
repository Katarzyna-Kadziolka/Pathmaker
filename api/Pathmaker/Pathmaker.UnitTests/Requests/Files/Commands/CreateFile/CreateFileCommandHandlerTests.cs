using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Pathmaker.Application.Behaviour.Exceptions;
using Pathmaker.Application.Requests.Files.Commands.CreateFile;
using Pathmaker.Application.Services.Files;
using Pathmaker.Tests.Shared.Assets;

namespace Pathmaker.UnitTests.Requests.Files.Commands.CreateFile;

public class CreateFileCommandHandlerTests {

    [Test]
    public async Task Handle_ValidImage_ShouldReturnCreateFileResponse() {
        // Arrange
        var formFile = AssetsManeger.GetImage();
        var command = new CreateFileCommand {
            File = formFile
        };
        var fileId = Guid.NewGuid();

        var fileService = Substitute.For<IFileService>();
        fileService.UploadImageAsync(formFile).Returns(new FileServiceResult {
            FileId = fileId,
            IsSuccess = true
        });
        var sut = new CreateFileCommandHandler(fileService);

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.FileId.Should().Be(fileId);
    }

    [Test]
    public async Task Handle_InvalidImage_ShouldReturnCreateFileResponse() {
        // Arrange
        var command = new CreateFileCommand();
        var fileService = Substitute.For<IFileService>();
        fileService.UploadImageAsync(Arg.Any<IFormFile>()).Returns(new FileServiceResult {
            IsSuccess = false
        });
        var sut = new CreateFileCommandHandler(fileService);

        // Act
        var act = async () => await sut.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ExternalServiceFailureException>();
    }
}
