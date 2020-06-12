using System.IO;

namespace PdfWriter.Host
{
    public class PdfDocumentWriterFactory : IDocumentWriterFactory
    {
        public IDocumentWriter Create(Stream output)
        {
            return new PdfDocumentWriter(output);
        }
    }
}
