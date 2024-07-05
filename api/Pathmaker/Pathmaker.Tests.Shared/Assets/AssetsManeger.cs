using Microsoft.AspNetCore.Http;

namespace Pathmaker.Tests.Shared.Assets;

public class AssetsManeger {
    public static IFormFile GetImage() {
        var path = "Assets/test.jpg";
        using var stream = new FileStream(path, FileMode.Open);
        return new FormFile(stream,
            0,
            stream.Length,
            Path.GetFileName(path),
            Path.GetFileName(path));
    } 
}
