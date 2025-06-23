//-----------------------------------------------------------------------
// <copyright file="LastLogEvenSinkService.cs" company="SirHurt">
//     Author: massimopaganigh
//     Copyright (c) 2025. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KneeSurgery.Services
{
    /// <summary>
    /// Service providing extension methods for configuring custom log event sinks.
    /// Contains extensions for Serilog logger configuration to add LastLogEventSink functionality.
    /// </summary>
    public static class LastLogEvenSinkService
    {
        /// <summary>
        /// Configures the logger to use the LastLogEventSink for capturing the most recent log events.
        /// </summary>
        /// <param name="configuration">The Serilog sink configuration to extend.</param>
        /// <returns>The configured LoggerConfiguration with LastLogEventSink added.</returns>
        public static LoggerConfiguration LastLogEventSink(this LoggerSinkConfiguration configuration) => configuration.Sink(new LastLogEventSink());
    }
}