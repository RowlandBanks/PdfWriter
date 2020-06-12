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
        /// <param name="input"></param>
        /// <returns>A stream representing the output document.</returns>
        Stream Write(Stream input);
    }
}
