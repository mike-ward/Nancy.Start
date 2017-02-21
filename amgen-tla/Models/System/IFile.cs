namespace TLA.Models.System
{
    public interface IFile
    {
        bool Exists(string path);
        void Delete(string path);
        void Move(string source, string dest);
        string ReadAllText(string path);
        void WriteAllText(string path, string contents);
    }
}