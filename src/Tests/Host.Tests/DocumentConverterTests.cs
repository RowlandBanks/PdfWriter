using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NSubstitute;
using Xunit;
using static Xunit.Assert;

namespace PdfWriter.Host.Tests
{
    public class DocumentConverterTests
    {
        [Fact]
        public void UsesDocumentWriterFactory()
        {
            // Background: Proves that the converter uses the factory
            // Arrange
            var factory = Substitute.For<IDocumentWriterFactory>();
            var converter = new DocumentConverter(factory);
            var input = new MemoryStream();
            var output = new MemoryStream();

            // Act
            converter.Convert(input, output);

            // Assert
            factory.Received(1).Create(output);
        }

        [Fact]
        public void ConverterWritesText()
        {
            var input = "Hello world";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).Write(input);
                writer.ReceivedWithAnyArgs(1).Write(default);
            }
        }

        private void Act(string input, Action<IDocumentWriter> assert)
        {
            var factory = Substitute.For<IDocumentWriterFactory>();
            var writer = Substitute.For<IDocumentWriter>();
            var converter = new DocumentConverter(factory);
            var inputStream = new MemoryStream();
            var outputStream = new MemoryStream();

            factory.Create(outputStream).Returns(writer);

            using (var streamWriter = new StreamWriter(inputStream, leaveOpen: true))
            {
                streamWriter.Write(input);
            }

            inputStream.Position = 0;

            converter.Convert(inputStream, outputStream);

            assert(writer);
        }
    }
}
