using Host;
using System;
using System.IO;

namespace PdfWriter.Host
{
    public class DocumentConverter : IDocumentConverter
    {
        private readonly IDocumentWriterFactory _documentWriterFactory;

        public DocumentConverter(IDocumentWriterFactory documentWriterFactory)
        {
            _documentWriterFactory = documentWriterFactory ?? throw new ArgumentNullException(nameof(documentWriterFactory));
        }

        public void Convert(Stream input, Stream output)
        {
            var writer = _documentWriterFactory.Create(output);

            using (var reader = new StreamReader(input, leaveOpen: true))
            {
                while (!reader.EndOfStream)
                {
                    var nextLine = reader.ReadLine();
                    ProcessLine(writer, nextLine);
                }
            }
        }

        private void ProcessLine(IDocumentWriter writer, string nextLine)
        {
            if (nextLine.StartsWith("."))
            {
                // This is a control statement. Process it appropriately.
            }
            else
            {
                // This is just text - write it.
                writer.Write(nextLine);
            }
        }
    }
}
