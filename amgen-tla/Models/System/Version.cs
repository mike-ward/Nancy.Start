using System;
using System.Diagnostics;
using System.Reflection;

namespace TLA.Models.System
{
    public static class Version
    {
        public static string Current()
        {
            var assembly = Assembly.GetExecutingAssembly();
            if (string.IsNullOrWhiteSpace(assembly.Location)) throw new Exception("Unable to read application version");
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }
    }
}