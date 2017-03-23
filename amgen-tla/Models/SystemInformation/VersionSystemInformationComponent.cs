namespace TLA.Models.SystemInformation
{
    public class VersionSystemInformationComponent : ISystemInformationComponent
    {
        public string Title()
        {
            return "Version";
        }

        public string Html()
        {
            return $"Version: {Version.Current()}";
        }
    }
}