using FluentAssertions;
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

        var fileService = Substitute.For<IFileService>();
        fileService.UploadImageAsync(formFile).Returns(new FileServiceResult {
            FileId = Guid.NewGuid(),
            IsSuccess = true
        });
        var sut = new CreateFileCommandHandler(fileService);

        // Act
        var result = await sut.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CreateFileResponse>();
        result.FileId.Should().NotBeEmpty();
    }

    [Test]
    public async Task Handle_InValidImage_ShouldReturnCreateFileResponse() {
        // Arrange
        var formFile = AssetsManeger.GetImage();
        var command = new CreateFileCommand {
            File = formFile
        };

        var fileService = Substitute.For<IFileService>();
        fileService.UploadImageAsync(formFile).Returns(new FileServiceResult {
            IsSuccess = false
        });
        var sut = new CreateFileCommandHandler(fileService);

        // Act
        var act = async () => await sut.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ExternalServiceFailureException>();
    }
}
