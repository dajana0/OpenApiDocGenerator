using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenApiDocGenerator.Services
{
    public interface ICommandParser
    {
        Task<int> ExecuteCommandAsync(string[] args);
    }

    internal class CommandParser : ICommandParser
    {
        private readonly Command _rootCommand;
        private readonly IDocGeneratorService _docGeneratorService;

        public CommandParser(IDocGeneratorService docGeneratorService)
        {
            _docGeneratorService = docGeneratorService;
            _rootCommand = BuildCommand();
        }
        public Task<int> ExecuteCommandAsync(string[] args)
        {
            return _rootCommand.InvokeAsync(args);
        }

        private Command BuildCommand()
        {

            var fileOption = new Option<FileInfo>(
                name: "--input",
                description: "File path OpenAPI JSON/YAML");
            var outputOption = new Option<string>(
                name: "--output",
                description: "Output path for pdf");

            var rootCommand = new RootCommand("Pdf OpenAPI Documentation generator")
            {
                fileOption,
                outputOption,
            };

            rootCommand.SetHandler(async (FileInfo input, string output) =>
            {
                try
                {
                    await _docGeneratorService.GenerateAsync(input, output);
                    Console.WriteLine("Documentation geneated!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }, fileOption, outputOption);

            return rootCommand;
        }
    }
}
