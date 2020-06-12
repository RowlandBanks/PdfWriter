using System.IO;

namespace PdfWriter.Host
{
    public interface IDocumentWriterFactory
    {
        /// <summary>
        /// Creates a new document writer.
        /// </summary>
        /// <param name="output">The stream the writer should write to.</param>
        /// <returns></returns>
        IDocumentWriter Create(Stream output);
    }
}
