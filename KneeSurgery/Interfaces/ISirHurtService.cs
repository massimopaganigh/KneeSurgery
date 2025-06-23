//-----------------------------------------------------------------------
// <copyright file="ISirHurtService.cs" company="SirHurt">
//     Author: massimopaganigh
//     Copyright (c) 2025. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for managing SirHurt application operations.
    /// Provides functionality to handle SirHurt user authentication and session management.
    /// </summary>
    public interface ISirHurtService
    {
        /// <summary>
        /// Gets the current version of the SirHurt application asynchronously.
        /// </summary>
        /// <returns>A string containing the SirHurt version.</returns>
        public Task<string> GetVersion();

        /// <summary>
        /// Logs out the user from SirHurt by removing authentication files.
        /// Deletes the authentication data files (a.dat and p.dat) from the SirHurt directory.
        /// </summary>
        /// <returns>
        /// A tuple containing:
        /// - bool: true if logout operation completed, false if an error occurred
        /// - string: result message or error message of the operation
        /// </returns>
        public (bool, string) Logout();
    }
}