namespace KneeSurgery.Services
{
    public class DebugLogService(DebugLog debugLog, Directories directories) : IDebugLogService
    {
        private readonly DebugLog _debugLog = debugLog;
        private readonly System.Timers.Timer _debugLogMonitorTimer = new(5000);
        private readonly Directories _directories = directories;
        private bool _isMonitoring = false;

        private void OnMonitorTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                string sirHDebugLogDat = Path.Combine(_directories.SirHurtDirectory, Constants.SirHui, Constants.SirHDebugLogDat);

                if (!File.Exists(sirHDebugLogDat))
                {
                    Log.Debug("[{0}] Debug log file not found at path: {1}.", nameof(OnMonitorTimerElapsed), sirHDebugLogDat);

                    return;
                }

                string content = File.ReadAllText(sirHDebugLogDat);

                _debugLog.Content = content;

                Log.Debug("[{0}] Debug log content updated successfully. Content length: {1} characters.", nameof(OnMonitorTimerElapsed), content.Length);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(OnMonitorTimerElapsed));
            }
        }

        public void Dispose()
        {
            StopMonitoring();

            _debugLogMonitorTimer?.Dispose();
        }

        public (bool, string) Open()
        {
            try
            {
                string sirHDebugLogDat = Path.Combine(_directories.SirHurtDirectory, Constants.SirHui, Constants.SirHDebugLogDat);

                if (!File.Exists(sirHDebugLogDat))
                {
                    string result = $"[{nameof(Open)}] Debug log file not found at path: {sirHDebugLogDat}.";

                    Log.Debug(result);

                    return (false, result);
                }

                ProcessStartInfo notepadStartInfo = new()
                {
                    FileName = "notepad.exe",
                    Arguments = sirHDebugLogDat,
                    UseShellExecute = true
                };

                using Process notepad = new()
                {
                    StartInfo = notepadStartInfo
                };

                notepad.Start();

                if (notepad == null)
                {
                    string result = $"[{nameof(Open)}] Failed to start notepad process to open debug log.";

                    Log.Debug(result);

                    return (false, result);
                }

                string res = $"[{nameof(Open)}] Debug log opened successfully: {sirHDebugLogDat}.";

                Log.Debug(res);

                return (true, res);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(Open));

                return (false, ex.Message);
            }
        }

        public (bool, string) OpenDirectory()
        {
            try
            {
                string sirHuiDirectory = Path.Combine(_directories.SirHurtDirectory, Constants.SirHui);

                if (!Directory.Exists(sirHuiDirectory))
                {
                    string result = $"[{nameof(OpenDirectory)}] Debug log directory not found at: {sirHuiDirectory}.";

                    Log.Debug(result);

                    return (false, result);
                }

                ProcessStartInfo explorerStartInfo = new()
                {
                    FileName = "explorer.exe",
                    Arguments = sirHuiDirectory,
                    UseShellExecute = true
                };

                using Process explorer = new()
                {
                    StartInfo = explorerStartInfo
                };

                explorer.Start();

                if (explorer == null)
                {
                    string result = $"[{nameof(OpenDirectory)}] Failed to start explorer process to open debug log directory.";

                    Log.Debug(result);

                    return (false, result);
                }

                string res = $"[{nameof(OpenDirectory)}] Debug log directory opened successfully: {sirHuiDirectory}.";

                Log.Debug(res);

                return (true, res);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(OpenDirectory));

                return (false, ex.Message);
            }
        }

        public void StartMonitoring()
        {
            try
            {
                if (_isMonitoring)
                {
                    Log.Debug("[{0}] Debug log monitoring is already active.", nameof(StartMonitoring));

                    return;
                }

                _debugLogMonitorTimer.Elapsed += OnMonitorTimerElapsed;
                _debugLogMonitorTimer.AutoReset = true;
                _debugLogMonitorTimer.Enabled = true;
                _isMonitoring = true;

                OnMonitorTimerElapsed(null, null!);

                Log.Debug("[{0}] Debug log monitoring started. Checking every 5 seconds.", nameof(StartMonitoring));
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(StartMonitoring));
            }
        }

        public void StopMonitoring()
        {
            try
            {
                if (!_isMonitoring)
                {
                    Log.Debug("[{0}] Debug log monitoring is not active.", nameof(StopMonitoring));

                    return;
                }

                _debugLogMonitorTimer.Enabled = false;
                _debugLogMonitorTimer.Elapsed -= OnMonitorTimerElapsed;
                _isMonitoring = false;

                Log.Debug("[{0}] Debug log monitoring stopped.", nameof(StopMonitoring));
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(StopMonitoring));
            }
        }
    }
}