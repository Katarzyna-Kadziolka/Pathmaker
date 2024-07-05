
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Pathmaker.Application.Behaviour.Exceptions;
using Pathmaker.Application.Requests.Files.Commands.CreateFile;
using Pathmaker.Application.Services.Files;

namespace Pathmaker.UnitTests.Requests.Files.Commands.CreateFile;

public class CreateFileCommandHandlerTests {
    private IFormFile CreateFormFile()
    {
        var relativePath = @"..\..\..\..\Pathmaker.Tests.Shared\Assets\test.jpg";
        var filePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));
        var fileInfo = new FileInfo(filePath);
        var memoryStream = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            stream.CopyTo(memoryStream);
        }
        memoryStream.Position = 0;

        var formFile = Substitute.For<IFormFile>();
        formFile.OpenReadStream().Returns(memoryStream);
        formFile.FileName.Returns(fileInfo.Name);
        formFile.Length.Returns(fileInfo.Length);
        formFile.ContentType.Returns("image/jpg");

        return formFile;
    }
    [Test]
    public async Task Handle_ValidImage_ShouldReturnCreateFileResponse() {
        // Arrange
        var formFile = CreateFormFile();
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
        var formFile = CreateFormFile();
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
