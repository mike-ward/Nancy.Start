namespace TLA.Models
{
    public interface IConfiguration
    {
        string[] ActiveDirectoryUserGroups();
        string[] ActiveDirectoryAdminGroups();
        string UserRepositoryPath();
    }
}