
using NSubstitute;
using Pathmaker.Application.Services.Files;

namespace Pathmaker.UnitTests.Requests.Files.Commands.CreateFile;

public class CreateFileCommandHandlerTests {
    [Test]
    public async Task Handle_ShouldReturnCreateFileResponse() {
        // Arrange
        var service = Substitute.For<IFileService>();
    }
    
}
