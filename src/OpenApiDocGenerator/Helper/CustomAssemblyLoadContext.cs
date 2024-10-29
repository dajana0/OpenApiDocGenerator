using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace OpenApiDocGenerator.Helper;

public class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public void Load()
    {
        LoadUnmanagedDllFromPath(GetWkHtmlToPdfExeName());
    }

    private static string GetWkHtmlToPdfExeName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Path.Combine(AppContext.BaseDirectory, "libwkhtmltox", "linux", "libwkhtmltox.so");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Path.Combine(AppContext.BaseDirectory, "libwkhtmltox", "macOS", "libwkhtmltox.dylib");
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Path.Combine(AppContext.BaseDirectory, "libwkhtmltox", "win-x64", "libwkhtmltox.dll");
        }

        throw new NotSupportedException("Operating system not supported by wkhtmltopdf");
    }
}
