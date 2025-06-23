namespace KneeSurgery.Services
{
    public class BootstrapService(Directories directories) : IBootstrapService
    {
        private readonly Directories _directories = directories;

        public async Task<(bool, string)> ExtractAsync()
        {
            try
            {
                string resource = "KneeSurgery.Assets.Zips.SirHurt.zip";
                using Stream? resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

                if (resourceStream == null)
                {
                    string result = $"[{nameof(ExtractAsync)}] Embedded resource not found: {resource}.";

                    Log.Error(result);

                    return (false, result);
                }

                Log.Debug("[{0}] Starting file extraction from embedded resource: {1}.", nameof(ExtractAsync), resource);

                List<string> extractedFiles = [];
                using ZipArchive zipArchive = new(resourceStream, ZipArchiveMode.Read);

                foreach (ZipArchiveEntry entry in zipArchive.Entries)
                {
                    if (string.IsNullOrEmpty(entry.Name))
                        continue;

                    string entryFilePath = Path.Combine(_directories.CurrentDirectory, entry.FullName);
                    string? entryFileDirectory = Path.GetDirectoryName(entryFilePath);

                    if (!string.IsNullOrEmpty(entryFileDirectory)
                        && !Directory.Exists(entryFileDirectory))
                    {
                        Directory.CreateDirectory(entryFileDirectory);

                        Log.Debug("[{0}] Created directory: {1}.", nameof(ExtractAsync), entryFileDirectory);
                    }

                    using Stream entryStream = entry.Open();
                    using FileStream fileStream = File.Create(entryFilePath);

                    await entryStream.CopyToAsync(fileStream);

                    extractedFiles.Add(entry.FullName);

                    Log.Debug("[{0}] Extracted file: {1}.", nameof(ExtractAsync), entryFilePath);
                }

                string res = $"[{nameof(ExtractAsync)}] Extraction completed successfully. Files extracted: {extractedFiles.Count}. Path: {_directories.CurrentDirectory}.";

                Log.Debug(res);

                return (true, res);
            }
            catch (Exception ex)
            {
                Log.Error(ex, nameof(ExtractAsync));

                return (false, ex.Message);
            }
        }
    }
}