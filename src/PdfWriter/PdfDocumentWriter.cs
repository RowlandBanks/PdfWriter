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
        private static readonly int LargeFontSize = 28;
        private static readonly int NormalFontSize = 12;
        private readonly Stream _output;
        private readonly Document _document;
        private readonly Section _section;
        private Paragraph _paragraph;
        private TextFormat _currentTextFormat;
        private int _currentFontSize = NormalFontSize;
        private Unit _currentIndent = new Unit(0);

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
            _currentIndent += new Unit(delta, UnitType.Centimeter);
            _paragraph.Format.LeftIndent = _currentIndent;
        }

        public void SetFontSize(FontSize fontSize)
        {
            _currentFontSize = fontSize == FontSize.Large ?
                    LargeFontSize :
                    NormalFontSize;

            _paragraph.Format.Font.Size = _currentFontSize;
        }

        public void SetFontStyle(FontStyle fontStyle)
        {
            if (fontStyle.HasFlag(FontStyle.Bold))
            {
                _currentTextFormat = _currentTextFormat | TextFormat.Bold;
            }
            else if (fontStyle.HasFlag(FontStyle.Italic))
            {
                _currentTextFormat = _currentTextFormat | TextFormat.Italic;
            }
            else if (fontStyle == FontStyle.NoStyle)
            {
                _currentTextFormat = 0;
            }
        }

        public void SetParagraphAlignment(ParagraphAlignment paragraphAlignment)
        {
            _paragraph.Format.Alignment = GetParagraphAlignment(paragraphAlignment);
        }

        public void StartNewParagraph()
        {
            _paragraph = _section.AddParagraph();

            _paragraph.Format.SpaceAfter = new Unit(1, UnitType.Centimeter);
            _paragraph.Format.Font.Size = _currentFontSize;
            _paragraph.Format.LeftIndent += _currentIndent;
        }

        public void Write(string text)
        {
            _paragraph.AddFormattedText(text, _currentTextFormat);
        }

        public void Complete()
        {
            var pdfRenderer = new PdfDocumentRenderer(unicode: true);
            pdfRenderer.Document = _document;
            pdfRenderer.RenderDocument();

            // Save the document...
            pdfRenderer.PdfDocument.Save(_output);
        }

        private MigraDoc.DocumentObjectModel.ParagraphAlignment GetParagraphAlignment(ParagraphAlignment paragraphAlignment)
        {
            if (paragraphAlignment == ParagraphAlignment.Justified)
            {
                return MigraDoc.DocumentObjectModel.ParagraphAlignment.Justify;
            }
            else
            {
                return MigraDoc.DocumentObjectModel.ParagraphAlignment.Left;
            }
        }
    }
}
