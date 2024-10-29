using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.Extensions.DependencyInjection;
using OpenApiDocGenerator.Services;
using OpenApiDocGenerator.Helper;

namespace OpenApiDocGenerator;

public class Program
{
    static async Task<int> Main(string[] args)
    {
        var context = new CustomAssemblyLoadContext();
        context.Load();

        var serviceProvider = new ServiceCollection()
             .AddSingleton<ICommandParser, CommandParser>()
             .AddSingleton<IHtmlGenerator, HtmlGenerator>()
             .AddSingleton<IOpenApiService, OpenApiService>()
             .AddSingleton<IPdfGenerator, PdfGenerator>()
             .AddSingleton<IDocGeneratorService, DocGeneratorService>()
             .AddSingleton(typeof(IConverter), new BasicConverter(new PdfTools()))
             .BuildServiceProvider();

        var parser = serviceProvider.GetRequiredService<ICommandParser>();
        return await parser.ExecuteCommandAsync(args);
    }

}