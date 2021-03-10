using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace GenerateReport.Helper
{
    public static class IO
    {
        public static IEnumerable<DirectoryInfo> GetDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path), "The folder path is mandatory");
            }
            if (!Directory.Exists(path))
            {
                throw new ArgumentException("The folder path given in parameter is incorrect");
            }
            var dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            // We may be in the case of zip file, trying...
            if (dirs.Length == 0)
            {
                var zipFiles = GetFiles(path, "zip");
                zipFiles.ToList().ForEach(x =>
                {
                    ZipFile.ExtractToDirectory(x.FullName, path);
                    //x.Delete();
                });
                dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            }
            return dirs.Select(x => new DirectoryInfo(x));
        }

        public static IEnumerable<FileInfo> GetFiles(string path = "", string ext = "pdf")
        {
            var files = Directory.GetFiles(path, $"*.{ext}*", SearchOption.AllDirectories);
            return files.Select(x => new FileInfo(x));
        }
    }
}
