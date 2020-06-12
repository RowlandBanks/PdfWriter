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
            }
        }

        [Fact]
        public void ConverterSetsFontSizeLarge()
        {
            var input = ".large";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).SetFontSize(FontSize.Large);
            }
        }

        [Fact]
        public void ConverterSetsFontSizeNormal()
        {
            var input = ".normal";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).SetFontSize(FontSize.Normal);
            }
        }

        [Fact]
        public void ConverterSetsBold()
        {
            var input = ".bold";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).SetFontStyle(FontStyle.Bold);
            }
        }

        [Fact]
        public void ConverterSetsItalics()
        {
            var input = ".italics";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).SetFontStyle(FontStyle.Italic);
            }
        }

        [Fact]
        public void ConverterSetsRegular()
        {
            var input = ".regular";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).SetFontStyle(FontStyle.NoStyle);
            }
        }

        [Fact]
        public void ConverterSetsParagraphAlignmentJustified()
        {
            var input = ".fill";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).SetParagraphAlignment(ParagraphAlignment.Justified);
            }
        }

        [Fact]
        public void ConverterSetsParagraphAlignmentLeftAlign()
        {
            var input = ".nofill";

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).SetParagraphAlignment(ParagraphAlignment.LeftAlign);
            }
        }

        [Fact]
        public void ConverterThrowsOnUnrecognizedControlStatement()
        {
            var input = ".not_real";

            var exception = Throws<Exception>(() => Act(input, null));
            Equal("Unrecognized control statement '.not_real'", exception.Message);
        }

        [Theory]
        [InlineData("+0", 0)]
        [InlineData("0", 0)]
        [InlineData("-0", 0)]
        [InlineData("1", 1)]
        [InlineData("+1", 1)]
        [InlineData("-1", -1)]
        [InlineData(" +1", 1)]
        public void ConverterSetsIndent(string indent, int expected)
        {
            var input = ".indent " + indent;

            Act(input, Assert);

            void Assert(IDocumentWriter writer)
            {
                // Prove we received exactly 1 call, to Write.
                Single(writer.ReceivedCalls());
                writer.Received(1).ChangeIndent(expected);
            }
        }

        [Fact]
        public void ConverterThrowsOnInvalidIndentValue()
        {
            var input = ".indent invalid";

            var exception = Throws<Exception>(() => Act(input, null));

            Equal("Invalid indent value 'invalid'", exception.Message);
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
