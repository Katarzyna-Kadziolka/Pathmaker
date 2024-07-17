using Microsoft.AspNetCore.Http;
using Pathmaker.Application.Requests.Files.Commands.CreateFile;
using Pathmaker.IntegrationTests.Extensions;
using Pathmaker.Tests.Shared.Assets;

namespace Pathmaker.IntegrationTests.Controllers;

public class FilesControllerTests : BaseTest {
    private const string Route = "api/files";

    [Test]
    public async Task Post_WhenDataIsCorrect_ShouldBeOk() {
        // Arrange
        var formFile = AssetsManeger.GetImage();
        using var form = new MultipartFormDataContent();
        form.Add(formFile, nameof(CreateFileCommand.File));
        // Act
        var response = await HttpClient.PostAsync(Route, form);
        // Assert
        response.Should().Be200Ok();
        var result = await response.Content.DeserializeContentAsync<CreateFileResponse>();
        result.Should().NotBeNull();
        result!.FileId.Should().NotBeEmpty();
    }
    
    [Test]
    public async Task Post_WhenFileNameHasSpecialCharacters_ShouldBeOk() {
        // Arrange
        var defaultFormFile = AssetsManeger.GetImage();
        var specialName = "ąęółżźćśń!@#$%^&*()-=_+~`?/";
        var formFile = new FormFile(defaultFormFile.OpenReadStream(), 0, defaultFormFile.Length, "File", specialName)  {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg",
            ContentDisposition = "form-data"
        };
        using var form = new MultipartFormDataContent();
        form.Add(formFile, nameof(CreateFileCommand.File));
        // Act
        var response = await HttpClient.PostAsync(Route, form);
        // Assert
        response.Should().Be200Ok();
        var result = await response.Content.DeserializeContentAsync<CreateFileResponse>();
        result.Should().NotBeNull();
        result!.FileId.Should().NotBeEmpty();
    }
}
