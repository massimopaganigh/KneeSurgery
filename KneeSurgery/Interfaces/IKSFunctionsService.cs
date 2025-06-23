namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for processing KneeSurgery-specific functions.
    /// Provides functionality to process and transform scripts containing custom KS functions.
    /// </summary>
    public interface IKSFunctionsService
    {
        /// <summary>
        /// Processes and transforms a script containing custom KneeSurgery functions.
        /// </summary>
        /// <param name="script">The script containing KS functions to process.</param>
        /// <returns>The processed script with transformed KS functions.</returns>
        public string ProcessKSFunctions(string script);
    }
}