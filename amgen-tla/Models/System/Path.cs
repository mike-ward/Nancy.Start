namespace TLA.Models.System
{
    public class Path : IPath
    {
        public string Combine(string path1, string path2)
        {
            return global::System.IO.Path.Combine(path1, path2);
        }

        public string GetDirectoryName(string path)
        {
            return global::System.IO.Path.GetDirectoryName(path);
        }

        public string GetFileNameWithoutExtension(string path)
        {
            return global::System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public bool IsPathRooted(string path)
        {
            return global::System.IO.Path.IsPathRooted(path);
        }
    }
}