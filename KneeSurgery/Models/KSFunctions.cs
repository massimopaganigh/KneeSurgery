namespace KneeSurgery.Models
{
    public class KSFunctions
    {
        public Dictionary<string, string> Functions = new()
        {
            { "GetKneeSurgeryVersion", $"print(\"Knee Surgery Version: {Constants.KneeSurgeryVersion}\")" }
        };
    }
}