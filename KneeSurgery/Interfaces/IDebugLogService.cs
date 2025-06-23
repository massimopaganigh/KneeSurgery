namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for managing debug logs functionality.
    /// Provides functionality to manage debug log files, monitoring and directory operations.
    /// </summary>
    public interface IDebugLogService
    {
        /// <summary>
        /// Releases the resources used by the service.
        /// </summary>
        public void Dispose();

        /// <summary>
        /// Opens the debug log file for viewing.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// - bool: true if opening succeeded, false otherwise
        /// - string: result message or error message of the operation
        /// </returns>
        public (bool, string) Open();

        /// <summary>
        /// Opens the debug log directory in the file explorer.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// - bool: true if opening succeeded, false otherwise
        /// - string: result message or error message of the operation
        /// </returns>
        public (bool, string) OpenDirectory();

        /// <summary>
        /// Starts monitoring debug log changes.
        /// </summary>
        public void StartMonitoring();

        /// <summary>
        /// Stops monitoring debug log changes.
        /// </summary>
        public void StopMonitoring();
    }
}