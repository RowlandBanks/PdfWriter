using System.IO;

namespace Host
{
    /// <summary>
    /// Converts an input document into an output document.
    /// </summary>
    interface IDocumentConverter
    {
        /// <summary>
        /// Converts <paramref name="input"/> into a new document.
        /// </summary>
        /// <remarks>
        /// Note that the caller is responsible for closing the <paramref name="input"/> and
        /// <paramref name="output"/> streams.
        /// </remarks>
        /// <param name="input">The input stream</param>
        /// <param name="output">The output stream</param>
        /// <returns>A stream representing the output document.</returns>
        void Convert(Stream input, Stream output);
    }
}
