namespace KneeSurgery.Services
{
    public class RobloxService(HttpClient httpClient) : IRobloxService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<string> GetVersionAsync()
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(await _httpClient.GetStringAsync(Constants.SirHurtApi));

                if (document.RootElement.EnumerateArray().FirstOrDefault().TryGetProperty("SirHurt V5", out JsonElement sirHurt))
                {
                    if (sirHurt.TryGetProperty("roblox_version", out JsonElement version))
                        return version.GetString() ?? string.Empty;

                    Log.Error("[{0}] roblox_version field not found in JSON response.", nameof(GetVersionAsync));

                    return string.Empty;
                }

                Log.Error("[{0}] SirHurt V5 field not found in JSON response.", nameof(GetVersionAsync));

                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(GetVersionAsync));

                return string.Empty;
            }
        }

        public (bool, string) Kill()
        {
            try
            {
                Process[] roblox = Process.GetProcessesByName(Constants.RobloxPlayerBeta);
                List<string> killedRoblox = [];
                int totalKilled = 0;

                if (roblox.Length > 0)
                {
                    Log.Debug("[{0}] Found {1} active Roblox processes.", nameof(Kill), roblox.Length);

                    foreach (Process process in roblox)
                    {
                        try
                        {
                            string processInfo = $"{process.ProcessName} (PID: {process.Id})";

                            process.Kill(true);
                            process.WaitForExit(5000);
                            killedRoblox.Add(processInfo);

                            totalKilled++;

                            Log.Debug("[{0}] Active Roblox process terminated: {1}.", nameof(Kill), processInfo);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, nameof(Kill));

                            throw;
                        }
                        finally
                        {
                            process.Dispose();
                        }
                    }
                }

                string result = totalKilled > 0 ? $"Successfully terminated {totalKilled} active Roblox processes: {string.Join(", ", killedRoblox)}" : "No active Roblox processes found running.";

                Log.Debug("[{0}] {1}", nameof(Kill), result);

                return (true, result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(Kill));

                return (false, ex.Message);
            }
        }
    }
}