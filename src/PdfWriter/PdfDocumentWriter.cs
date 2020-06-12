using System;
using System.IO;

namespace PdfWriter.Host
{
    /// <summary>
    /// Implements a <see cref="IDocumentWriter" /> that uses PdfSharp to write the document.
    /// </summary>
    public class PdfDocumentWriter : IDocumentWriter
    {
        private readonly Stream _output;

        public PdfDocumentWriter(Stream output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
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
            throw new NotImplementedException();
        }

        public void Write(string text)
        {
            throw new NotImplementedException();
        }
    }
}
