//-----------------------------------------------------------------------
// <copyright file="IRobloxService.cs" company="SirHurt">
//     Author: massimopaganigh
//     Copyright (c) 2025. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for managing and interacting with the Roblox application.
    /// Provides functionality to get Roblox information and control its execution.
    /// </summary>
    public interface IRobloxService
    {
        /// <summary>
        /// Gets the current version of the Roblox application asynchronously.
        /// </summary>
        /// <returns>A string containing the Roblox version.</returns>
        public Task<string> GetVersionAsync();

        /// <summary>
        /// Forcefully terminates the Roblox application execution.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// - bool: true if termination succeeded, false otherwise
        /// - string: result message or error message of the operation
        /// </returns>
        public (bool, string) Kill();
    }
}