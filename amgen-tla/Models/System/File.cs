namespace TLA.Models.System
{
    public class File : IFile
    {
        public bool Exists(string path)
        {
            return global::System.IO.File.Exists(path);
        }

        public void Delete(string path)
        {
            global::System.IO.File.Delete(path);
        }

        public void Move(string source, string dest)
        {
            global::System.IO.File.Move(source, dest);
        }

        public string ReadAllText(string path)
        {
            return global::System.IO.File.ReadAllText(path);
        }

        public void WriteAllText(string path, string contents)
        {
            global::System.IO.File.WriteAllText(path, contents);
        }
    }
}