namespace App.Models.Utilities
{
    public interface IConfiguration
    {
        string[] ActiveDirectoryUserGroups();
        string[] ActiveDirectoryAdminGroups();
        string UserRepositoryPath();
    }
}