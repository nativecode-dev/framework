namespace NativeCode.Core.Logging
{
    public enum LogMessageType
    {
        /// <summary>
        /// Specifies a default message type.
        /// </summary>
        Default = 0, 

        /// <summary>
        /// Specifies a debug message type.
        /// </summary>
        Debug = 1, 

        /// <summary>
        /// Specifies an error message type.
        /// </summary>
        Error = 2, 

        /// <summary>
        /// Specifies an exception message type.
        /// </summary>
        Exception = 3, 

        /// <summary>
        /// Specifies an informational message type.
        /// </summary>
        Informational = Default, 

        /// <summary>
        /// Specifies a warning message type.
        /// </summary>
        Warning = 4
    }
}