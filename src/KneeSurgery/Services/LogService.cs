namespace KneeSurgery.Services
{
    public class LogService(Directories directories) : ILogService
    {
        private readonly Directories _directories = directories;

        public async Task StartLoggingAsync()
        {
            try
            {
                if (!Directory.Exists(_directories.KneeSurgeryDirectory))
                    Directory.CreateDirectory(_directories.KneeSurgeryDirectory);

                Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File(Path.Combine(_directories.KneeSurgeryDirectory, Constants.KneeSurgeryLog), fileSizeLimitBytes: 5 * 1024 * 1024, rollOnFileSizeLimit: true, retainedFileCountLimit: 10).WriteTo.LastLogEventSink().CreateLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{nameof(StartLoggingAsync)}] Exception: {ex}.");
            }
            finally
            {
                await StopLoggingAsync();
            }
        }

        public async Task StopLoggingAsync()
        {
            try
            {
                await Log.CloseAndFlushAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{nameof(StopLoggingAsync)}] Exception: {ex}.");
            }
        }
    }
}