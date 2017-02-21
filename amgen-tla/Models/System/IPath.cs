namespace TLA.Models.System
{
    public interface IPath
    {
        bool IsPathRooted(string path);
        string Combine(string path1, string path2);
        string GetDirectoryName(string path);
        string GetFileNameWithoutExtension(string path);
    }
}