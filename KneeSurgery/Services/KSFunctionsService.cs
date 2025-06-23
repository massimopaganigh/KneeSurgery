namespace KneeSurgery.Services
{
    public class KSFunctionsService(KSFunctions kSFunctions) : IKSFunctionsService
    {
        private readonly KSFunctions _ksFunctions = kSFunctions;

        public string ProcessKSFunctions(string script)
        {
            try
            {
                if (string.IsNullOrEmpty(script))
                    return script;

                foreach (var function in _ksFunctions.Functions)
                    script = script.Replace($"ksf.{function.Key}", function.Value);

                Log.Debug("[{0}] Script processed. KSFunction substitutions applied.", nameof(ProcessKSFunctions));

                return script;
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(ProcessKSFunctions));

                return script;
            }
        }
    }
}