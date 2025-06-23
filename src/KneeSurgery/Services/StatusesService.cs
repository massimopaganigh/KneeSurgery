namespace KneeSurgery.Services
{
    public class StatusesService(Statuses statuses) : IStatusesService
    {
        private bool _isMonitoring = false;
        private readonly System.Timers.Timer _robloxMonitorTimer = new(Constants.RobloxMonitorTimerPollingInterval);
        private readonly Statuses _statuses = statuses;

        private void OnMonitorTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                if (_statuses.InjectionStatus != InjectionStatus.Injected)
                {
                    Log.Debug("[{0}] Current status: {1}. No check needed.", nameof(OnMonitorTimerElapsed), _statuses.InjectionStatus);

                    return;
                }

                Process[] roblox = Process.GetProcessesByName(Constants.RobloxPlayerBeta);

                if (roblox.Length == 0)
                {
                    _statuses.InjectionStatus = InjectionStatus.NotInjected;

                    Log.Debug("[{0}] No active Roblox processes found. Status changed to NotInjected.", nameof(OnMonitorTimerElapsed));
                }
                else
                    Log.Debug("[{0}] Found {1} active Roblox processes. Status remains Injected.", nameof(OnMonitorTimerElapsed), roblox.Length);

                foreach (Process process in roblox)
                    process?.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(OnMonitorTimerElapsed));
            }
        }

        public void Dispose()
        {
            StopMonitoring();

            _robloxMonitorTimer?.Dispose();
        }

        public void StartMonitoring()
        {
            try
            {
                if (_isMonitoring)
                {
                    Log.Debug("[{0}] Roblox process monitoring is already active.", nameof(StartMonitoring));

                    return;
                }

                _robloxMonitorTimer.Elapsed += OnMonitorTimerElapsed;
                _robloxMonitorTimer.AutoReset = true;
                _robloxMonitorTimer.Enabled = true;
                _isMonitoring = true;

                Log.Debug("[{0}] Roblox process monitoring started. Checking every 5 seconds.", nameof(StartMonitoring));
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
                    Log.Debug("[{0}] Roblox process monitoring is not active.", nameof(StartMonitoring));

                    return;
                }

                _robloxMonitorTimer.Enabled = false;
                _robloxMonitorTimer.Elapsed -= OnMonitorTimerElapsed;
                _isMonitoring = false;

                Log.Debug("[{0}] Roblox process monitoring stopped.", nameof(StartMonitoring));
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(StopMonitoring));
            }
        }
    }
}