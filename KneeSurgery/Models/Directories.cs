namespace KneeSurgery.Models
{
    public class Directories
    {
        private readonly string _applicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private readonly string _currentDirectory = Environment.CurrentDirectory;
        private readonly string _localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public string CurrentDirectory => _currentDirectory;

        public string KneeSurgeryDirectory => Path.Combine(_applicationData, "KneeSurgery");

        //public string RobloxDirectory => Path.Combine(_localApplicationData, "Roblox"); We do not need it atm

        public string SirHurtDirectory => Path.Combine(_applicationData, "SirHurt");
    }
}