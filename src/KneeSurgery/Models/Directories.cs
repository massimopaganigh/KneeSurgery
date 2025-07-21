namespace KneeSurgery.Models
{
    public class Directories
    {
        private readonly string _applicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private readonly string _currentDirectory = Environment.CurrentDirectory;

        public string CurrentDirectory => _currentDirectory;

        public string KneeSurgeryDirectory => Path.Combine(_applicationData, "KneeSurgery");

        public string SirHurtDirectory => Path.Combine(_applicationData, "SirHurt");
    }
}