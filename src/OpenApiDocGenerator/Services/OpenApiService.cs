using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace OpenApiDocGenerator.Services
{
    public interface IOpenApiService
    {
        Task<OpenApiDocument> LoadOpenApiDocument(FileInfo input);
        Task<OpenApiDocument> LoadOpenApiDocument(string baseAddres, string? requestUri);
    }

    public class OpenApiService : IOpenApiService
    {
        public async Task<OpenApiDocument> LoadOpenApiDocument(FileInfo input)
        {
            using var stream = File.OpenRead(input.FullName);
            var reader = new OpenApiStreamReader();
            var result = await reader.ReadAsync(stream);
            var a = result.OpenApiDocument;
           
            return result.OpenApiDocument;
        }

        public async Task<OpenApiDocument> LoadOpenApiDocument(string baseAddres, string? requestUri)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddres)
            };
            var stream =  await httpClient.GetStreamAsync(requestUri);
            var reader = new OpenApiStreamReader();
            var result = await reader.ReadAsync(stream);

            return result.OpenApiDocument;
        }
    }
}
