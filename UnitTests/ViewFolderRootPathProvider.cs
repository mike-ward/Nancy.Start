﻿using System;
using System.IO;
using System.Linq;
using Nancy;

namespace UnitTests
{
    public class ViewFolderRootPathProvider : IRootPathProvider
    {
        private static string _cachedRootPath;

        public string GetRootPath()
        {
            return _cachedRootPath ?? (_cachedRootPath = RootPath());
        }

        private static string RootPath()
        {
            var currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);

            while (currentDirectory != null)
            {
                var directoriesContainingViewFolder = currentDirectory.GetDirectories("Views", SearchOption.AllDirectories);
                if (directoriesContainingViewFolder.Any()) return directoriesContainingViewFolder.First().FullName;
                currentDirectory = currentDirectory.Parent;
            }

            throw new DirectoryNotFoundException();
        }
    }
}