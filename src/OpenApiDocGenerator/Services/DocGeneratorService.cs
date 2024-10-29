namespace OpenApiDocGenerator.Services;

public interface IDocGeneratorService
{
    Task GenerateAsync(FileInfo input, string output);
}
public class DocGeneratorService(
    IOpenApiService _openApiService,
    IPdfGenerator _pdfGenerator,
    IHtmlGenerator _htmlGenerator) : IDocGeneratorService
{
    public async Task GenerateAsync(FileInfo input, string output)
    {
        var apiDefinition = await _openApiService.LoadOpenApiDocument(input);
        var html = await _htmlGenerator.RenderDocumentAsync("Templates.Documentation2", apiDefinition);
        _pdfGenerator.GetDocument(html, output);
    }
}
