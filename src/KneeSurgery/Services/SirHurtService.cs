﻿namespace KneeSurgery.Services
{
    public class SirHurtService(Directories directories, HttpClient httpClient) : ISirHurtService
    {
        private readonly Directories _directories = directories;
        private readonly HttpClient _httpClient = httpClient;

        public async Task<string> GetVersionAsync()
        {
            try
            {
                using JsonDocument document = JsonDocument.Parse(await _httpClient.GetStringAsync(Constants.SirHurtApi));

                if (document.RootElement.EnumerateArray().FirstOrDefault().TryGetProperty("SirHurt V5", out JsonElement sirHurt))
                {
                    if (sirHurt.TryGetProperty("exploit_version", out JsonElement version))
                        return version.GetString() ?? string.Empty;

                    Log.Error("[{0}] exploit_version field not found in JSON response.", nameof(GetVersionAsync));

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

        public (bool, string) Login() => throw new NotImplementedException(nameof(Login));

        public (bool, string) Logout()
        {
            try
            {
                string sirHuiDirectory = Path.Combine(_directories.SirHurtDirectory, Constants.SirHui);
                string sirHurtADat = Path.Combine(sirHuiDirectory, Constants.SirHurtADat);
                string sirHurtPDat = Path.Combine(sirHuiDirectory, Constants.SirHurtPDat);
                List<string> deletedFiles = [];
                List<string> notFoundFiles = [];

                if (File.Exists(sirHurtADat))
                {
                    File.Delete(sirHurtADat);

                    deletedFiles.Add(Constants.SirHurtADat);

                    Log.Debug("[{0}] Deleted file: {1}.", nameof(Logout), sirHurtADat);
                }
                else
                {
                    notFoundFiles.Add(Constants.SirHurtADat);

                    Log.Debug("[{0}] File not found: {1}.", nameof(Logout), sirHurtADat);
                }

                if (File.Exists(sirHurtPDat))
                {
                    File.Delete(sirHurtPDat);

                    deletedFiles.Add(Constants.SirHurtPDat);

                    Log.Debug("[{0}] Deleted file: {1}.", nameof(Logout), sirHurtPDat);
                }
                else
                {
                    notFoundFiles.Add(Constants.SirHurtPDat);

                    Log.Debug("[{0}] File not found: {1}.", nameof(Logout), sirHurtPDat);
                }

                string result;

                if (deletedFiles.Count > 0)
                {
                    result = $"[{nameof(Logout)}] Logout completed successfully. Deleted files: {string.Join(", ", deletedFiles)}.";

                    if (notFoundFiles.Count > 0)
                        result += $" Files not found: {string.Join(", ", notFoundFiles)}.";
                }
                else
                    result = $"[{nameof(Logout)}] No files were deleted. Files not found: {string.Join(", ", notFoundFiles)}.";

                Log.Debug(result);

                return (true, result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(Logout));

                return (false, ex.Message);
            }
        }
    }
}