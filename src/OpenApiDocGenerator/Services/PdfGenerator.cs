using DinkToPdf;
using DinkToPdf.Contracts;

namespace OpenApiDocGenerator.Services;

public interface IPdfGenerator
{
    void GetDocument(string html, string output);
}

public class PdfGenerator(IConverter converter) : IPdfGenerator
{
    private readonly IConverter _converter = converter;

    public void GetDocument(string html, string output)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = 
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4Plus,
                Out = output
            },
            Objects = 
            {
                new ObjectSettings() {
                    PagesCount = true,
                    UseLocalLinks = true,
                    UseExternalLinks = true,
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                }
            },
           
        };

        _converter.Convert(doc);
    }
}
