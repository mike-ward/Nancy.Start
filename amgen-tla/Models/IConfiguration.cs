namespace App.Models
{
    public interface IConfiguration
    {
        string[] ActiveDirectoryUserGroups();
        string[] ActiveDirectoryAdminGroups();
        string UserRepositoryPath();
    }
}