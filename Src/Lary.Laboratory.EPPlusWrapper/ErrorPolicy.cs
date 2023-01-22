namespace Lary.Laboratory.EPPlusWrapper
{
    /// <summary>
    /// Defines how to behave when error occurred.
    /// </summary>
    public enum ErrorPolicy
    {
        /// <summary>
        /// Terminates the whole action.
        /// </summary>
        Terminate,

        /// <summary>
        /// Processes data that before the first error.
        /// </summary>
        Truncate,

        /// <summary>
        /// Ignores errors and processes all correct data.
        /// </summary>
        Ignore
    }
}
