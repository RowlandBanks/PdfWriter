using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NSubstitute;
using Xunit;

namespace PdfWriter.Host.Tests
{
    public class DocumentConverterTests
    {
        [Fact]
        public void UsesDocumentWriterFactory()
        {
            // Background: Proves that the factory returns an instance of the correct type.
            // Arrange
            var factory = Substitute.For<IDocumentWriterFactory>();
            var converter = new DocumentConverter(factory);
            var input = new MemoryStream();
            var output = new MemoryStream();

            // Act
            converter.Convert(input, output);

            // Assert
            factory.Create(output).Received(1);
        }
    }
}
