using System;
using System.IO;

namespace PdfWriter.Host
{
    /// <summary>
    /// Implements a <see cref="IDocumentWriter" /> that uses 
    /// </summary>
    public class PdfDocumentWriter : IDocumentWriter
    {
        public void ChangeIndent(int delta)
        {
            throw new NotImplementedException();
        }

        public void Initialize(Stream output)
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
