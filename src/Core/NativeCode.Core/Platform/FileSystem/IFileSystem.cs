namespace NativeCode.Core.Platform.FileSystem
{
    /// <summary>
    /// Provides a contract to manage files.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Gets the text of the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>Returns the file contents as text.</returns>
        string GetText(string filename);

        /// <summary>
        /// Checks if filename exists.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns><c>true</c> if exists, <c>false</c> otherwise.</returns>
        bool Exists(string filename);

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="text">The text.</param>
        void SetText(string filename, string text);
    }
}