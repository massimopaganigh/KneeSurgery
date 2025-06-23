//-----------------------------------------------------------------------
// <copyright file="IExecutionService.cs" company="SirHurt">
//     Author: massimopaganigh
//     Copyright (c) 2025. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for executing scripts within the KneeSurgery application.
    /// Provides functionality to execute scripts and return the operation result.
    /// </summary>
    public interface IExecutionService
    {
        /// <summary>
        /// Executes a specified script asynchronously.
        /// </summary>
        /// <param name="script">The script to execute.</param>
        /// <returns>
        /// A tuple containing:
        /// - bool: true if execution succeeded, false otherwise
        /// - string: result message or error message
        /// </returns>
        public Task<(bool, string)> Execute(string script);
    }
}