namespace KneeSurgery.Services
{
    public class ExecutionService(Directories directories, IKSFunctionsService kSFunctionsService, Statuses statuses) : IExecutionService
    {
        private readonly Directories _directories = directories;
        private readonly IKSFunctionsService _ksFunctionsService = kSFunctionsService;
        private readonly Statuses _statuses = statuses;

        public async Task<(bool, string)> Execute(string script)
        {
            try
            {
                if (_statuses.InjectionStatus != InjectionStatus.Injected)
                {
                    string result = $"[{nameof(Execute)}] Current status: {_statuses.InjectionStatus}. No execution needed.";

                    Log.Debug(result);

                    return (false, result);
                }

                string sirHurtDat = Path.Combine(_directories.SirHurtDirectory, Constants.SirHui, Constants.SirHurtDat);
                string sirHuiDirectory = Path.GetDirectoryName(sirHurtDat)!;

                if (!Directory.Exists(sirHuiDirectory))
                {
                    Directory.CreateDirectory(sirHuiDirectory);

                    Log.Debug("[{0}] Created directory: {1}.", nameof(Execute), sirHuiDirectory);
                }

                script = _ksFunctionsService.ProcessKSFunctions(script);

                await File.WriteAllTextAsync(sirHurtDat, script);

                Log.Debug("[{0}] Script written to: {1}.", nameof(Execute), sirHurtDat);
                Log.Debug("[{0}] Script: {1}.", nameof(Execute), script);

                return (_statuses.InjectionStatus == InjectionStatus.Injected, script);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(Execute));

                return (false, ex.Message);
            }
        }
    }
}