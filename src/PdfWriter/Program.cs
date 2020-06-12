using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Host;
using Microsoft.Extensions.DependencyInjection;
using PdfWriter.Host;
using static System.Console;

namespace PdfWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Welcome to the PdfWriter!");

            if (args.Length != 1)
            {
                Fail("This application accepts only a single argument - a file path");
            }

            var inputFile = args[0];

            if (!File.Exists(inputFile))
            {
                Fail($"File '{inputFile}' does not exist.");
            }

            var services = new ServiceCollection()
                .AddTransient<IDocumentWriterFactory, PdfDocumentWriterFactory>()
                .AddTransient<IDocumentConverter, DocumentConverter>()
                .BuildServiceProvider();

            var converter = services.GetRequiredService<IDocumentConverter>();

            var outputFile = Path.ChangeExtension(inputFile, "pdf");
            using (var inputStream = File.OpenRead(inputFile))
            using (var outputStream = File.OpenWrite(outputFile))
            {
                converter.Convert(inputStream, outputStream);
            }

            WriteLine($"Document conversion complete. Opening '{outputFile}'");

            var psi = new ProcessStartInfo
            {
                // Execute with the shell to ensure the PDF file opens in
                // its default application.
                UseShellExecute = true,
                FileName = outputFile
            };

            Process.Start(psi);
        }

        private static void Fail(string reason)
        {
            WriteLine(reason);
            Environment.Exit(-1);
        }
    }
}
