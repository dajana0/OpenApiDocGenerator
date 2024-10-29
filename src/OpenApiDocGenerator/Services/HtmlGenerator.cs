using RazorLight;

namespace OpenApiDocGenerator.Services
{
    public interface IHtmlGenerator
    {
        Task<string> RenderDocumentAsync(string template, object model);
    }

    public class HtmlGenerator : IHtmlGenerator
    {
        private readonly IRazorLightEngine _renderingEngine;

        public HtmlGenerator()
        {
            _renderingEngine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(Program))
                .UseMemoryCachingProvider()
                .Build();
        }

        public Task<string> RenderDocumentAsync(string template, object model)
        {
            return _renderingEngine.CompileRenderAsync(template, model);
        }
    }
}
