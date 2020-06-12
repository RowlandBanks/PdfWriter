using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfWriter.Host
{
    /// <summary>
    /// Responsible for writing to an output document.
    /// </summary>
    public interface IDocumentWriter
    {
        /// <summary>
        /// Writes <paramref name="text"/> to the output stream.
        /// </summary>
        /// <param name="text">The text to write.</param>
        void Write(string text);

        /// <summary>
        /// Sets the font-size for subsequent calls to <see cref="Write(string)"/>
        /// </summary>
        /// <param name="fontSize"></param>
        void SetFontSize(FontSize fontSize);

        /// <summary>
        /// Sets the font-style for subsequent calls to <see cref="Write(string)"/>
        /// </summary>
        /// <param name="fontStyle"></param>
        void SetFontStyle(FontStyle fontStyle);

        /// <summary>
        /// Sets the current paragraph alignment.
        /// </summary>
        /// <param name="paragraphAlignment">The alignment</param>
        void SetParagraphAlignment(ParagraphAlignment paragraphAlignment);

        /// <summary>
        /// Starts a new paragraph.
        /// </summary>
        void StartNewParagraph();

        /// <summary>
        /// Changes the current indentation level by <paramref name="delta"/>.
        /// </summary>
        /// <remarks>
        /// Supply a negative number to reduce the indentation level.
        /// </remarks>
        /// <param name="delta"></param>
        void ChangeIndent(int delta);
    }
}
