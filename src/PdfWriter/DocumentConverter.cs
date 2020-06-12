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

                writer.Complete();
            }
        }

        private void ProcessLine(IDocumentWriter writer, string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            if (line.StartsWith("."))
            {
                // This is a control statement. Process it appropriately.
                if (line.StartsWith(".bold"))
                {
                    writer.SetFontStyle(FontStyle.Bold);
                }
                else if (line.StartsWith(".italic"))
                {
                    writer.SetFontStyle(FontStyle.Italic);
                }
                else if (line.StartsWith(".regular"))
                {
                    writer.SetFontStyle(FontStyle.NoStyle);
                }
                else if (line.StartsWith(".large"))
                {
                    writer.SetFontSize(FontSize.Large);
                }
                else if (line.StartsWith(".normal"))
                {
                    writer.SetFontSize(FontSize.Normal);
                }
                else if (line.StartsWith(".fill"))
                {
                    writer.SetParagraphAlignment(ParagraphAlignment.Justified);
                }
                else if (line.StartsWith(".nofill"))
                {
                    writer.SetParagraphAlignment(ParagraphAlignment.LeftAlign);
                }
                else if (line.StartsWith(".indent"))
                {
                    // Parse out the indent value.
                    var indentValue = line.Substring(7).Trim();
                    if (int.TryParse(indentValue, out var indent))
                    {
                        writer.ChangeIndent(indent);
                    }
                    else
                    {
                        throw new Exception($"Invalid indent value '{indentValue}'");
                    }
                }
                else
                {
                    throw new Exception($"Unrecognized control statement '{line}'");
                }
            }
            else
            {
                // This is just text - write it.
                writer.Write(line);
            }
        }
    }
}
