namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for code injection into the target application.
    /// Manages the injection process and provides information about the operation result.
    /// </summary>
    public interface IInjectionService
    {
        /// <summary>
        /// Executes the code injection operation.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// - bool: true if injection succeeded, false otherwise
        /// - string: result message or error message of the operation
        /// </returns>
        public (bool, string) Inject();
    }
}