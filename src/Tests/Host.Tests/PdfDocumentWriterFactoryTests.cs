using System.IO;
using Xunit;

namespace PdfWriter.Host.Tests
{
    public class PdfDocumentWriterFactoryTests
    {
        [Fact]
        public void CreateReturnsPdfDocumentWriter()
        {
            // Background: Proves that the factory returns an instance of the correct type.
            // Arrange
            var factory = new PdfDocumentWriterFactory();

            // Act
            var writer = factory.Create(new MemoryStream());

            // Assert
            Assert.IsType<PdfDocumentWriter>(writer);
        }
    }
}
