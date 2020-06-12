using System;
using System.IO;
using System.Text;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace PdfWriter.Host
{
    /// <summary>
    /// Implements a <see cref="IDocumentWriter" /> that uses PdfSharp to write the document.
    /// </summary>
    public class PdfDocumentWriter : IDocumentWriter
    {
        private readonly Stream _output;
        private readonly Document _document;
        private readonly Section _section;
        private Paragraph _paragraph;

        static PdfDocumentWriter()
        {
            // See https://stackoverflow.com/a/49701230/15393
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public PdfDocumentWriter(Stream output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            _document = new Document();
            _section = _document.AddSection();
            StartNewParagraph();
        }

        public void ChangeIndent(int delta)
        {
            throw new NotImplementedException();
        }

        public void SetFontSize(FontSize fontSize)
        {
            throw new NotImplementedException();
        }

        public void SetFontStyle(FontStyle fontStyle)
        {
            throw new NotImplementedException();
        }

        public void SetParagraphAlignment(ParagraphAlignment paragraphAlignment)
        {
            throw new NotImplementedException();
        }

        public void StartNewParagraph()
        {
            _paragraph = _section.AddParagraph();
        }

        public void Write(string text)
        {
            _paragraph.AddFormattedText(text);
        }

        public void Complete()
        {
            var pdfRenderer = new PdfDocumentRenderer(unicode: true);
            pdfRenderer.Document = _document;
            pdfRenderer.RenderDocument();

            // Save the document...
            pdfRenderer.PdfDocument.Save(_output);
        }
    }
}
