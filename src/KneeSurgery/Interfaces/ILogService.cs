//-----------------------------------------------------------------------
// <copyright file="ILogService.cs" company="SirHurt">
//     Author: massimopaganigh
//     Copyright (c) 2025. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for managing logging functionality.
    /// Provides functionality to start and stop logging operations within the application.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Starts the logging operation asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous logging start operation.</returns>
        public Task StartLoggingAsync();

        /// <summary>
        /// Stops the logging operation asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous logging stop operation.</returns>
        public Task StopLoggingAsync();
    }
}