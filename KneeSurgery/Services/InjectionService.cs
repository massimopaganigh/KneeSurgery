namespace KneeSurgery.Services
{
    public class InjectionService(Directories directories, Statuses statuses) : IInjectionService
    {
        private readonly Directories _directories = directories;
        private readonly Statuses _statuses = statuses;

        public (bool, string) Inject()
        {
            try
            {
                ProcessStartInfo sirHurtStartInfo = new()
                {
                    CreateNoWindow = true,
                    FileName = Path.Combine(_directories.CurrentDirectory, Constants.SirHurtExe),
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                using Process sirHurt = new()
                {
                    StartInfo = sirHurtStartInfo
                };

                sirHurt.Start();
                sirHurt.WaitForExit();

                int exitCode = sirHurt.ExitCode;
                string output = sirHurt.StandardOutput.ReadToEnd();

                _statuses.InjectionStatus = exitCode == 0 ? InjectionStatus.Injected : InjectionStatus.NotInjected;

                Log.Debug("[{0}] SirHurt Exit Code: {1}.", nameof(Inject), exitCode);
                Log.Debug("[{0}] SirHurt Output: {1}.", nameof(Inject), output);
                Log.Debug("[{0}] Injection Status: {1}.", nameof(Inject), _statuses.InjectionStatus);

                return (_statuses.InjectionStatus == InjectionStatus.Injected, output);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(Inject));

                return (false, ex.Message);
            }
        }
    }
}