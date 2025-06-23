//-----------------------------------------------------------------------
// <copyright file="IStatusesService.cs" company="SirHurt">
//     Author: massimopaganigh
//     Copyright (c) 2025. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KneeSurgery.Interfaces
{
    /// <summary>
    /// Service for monitoring and managing application states.
    /// Provides functionality to monitor Roblox and manage the service lifecycle.
    /// </summary>
    public interface IStatusesService
    {
        /// <summary>
        /// Releases the resources used by the service.
        /// </summary>
        public void Dispose();

        /// <summary>
        /// Starts monitoring the Roblox application.
        /// </summary>
        public void StartMonitoring();

        /// <summary>
        /// Stops monitoring the Roblox application.
        /// </summary>
        public void StopMonitoring();
    }
}