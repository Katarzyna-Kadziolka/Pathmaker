using Microsoft.AspNetCore.Http;

namespace Pathmaker.Tests.Shared.Assets;

public class AssetsManeger {
    public static IFormFile GetImage() {
        var path = "Assets/test.jpg";
        var memoryStream = new MemoryStream(File.ReadAllBytes(path));
        return new FormFile(memoryStream,
            0,
            memoryStream.Length,
            Path.GetFileName(path),
            Path.GetFileName(path)) {
            Headers = new HeaderDictionary(),
            ContentType = "image/jpeg",
            ContentDisposition = "form-data"
        };
    }
}
