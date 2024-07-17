using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Pathmaker.IntegrationTests.Extensions;

public static class MultipartFormDataContentExtensions {
    public static void Add(this MultipartFormDataContent form, IFormFile file, string name) {
        var content = new StreamContent(file.OpenReadStream()) {
            Headers = {
                ContentLength = file.Length,
                ContentType = new MediaTypeHeaderValue(file.ContentType)
            }
        };
        form.Add(content, name, file.FileName);
    } 
}
