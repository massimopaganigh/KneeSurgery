//-----------------------------------------------------------------------
// <copyright file="IBootstrapService.cs" company="SirHurt">
//     Author: massimopaganigh
//     Copyright (c) 2025. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for managing bootstrap operations and application initialization.
    /// Provides functionality to extract and prepare necessary application components.
    /// </summary>
    public interface IBootstrapService
    {
        /// <summary>
        /// Extracts the required files and components for application initialization asynchronously.
        /// This operation prepares the application environment by extracting necessary resources.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// - bool: true if extraction succeeded, false if an error occurred
        /// - string: result message or error message of the extraction operation
        /// </returns>
        public Task<(bool, string)> ExtractAsync();
    }
}