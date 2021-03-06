using GenerateReport.Model;
using Helper.Model;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace GenerateReport.Helper
{
    public static class IO
    {
        public static Doc GetModel(string path = "", string title = "")
        {
            var doc = new Doc(title);
            List<MainTitle> lMainTitle = new List<MainTitle>();
            var dirs = GetDirectory(path);
            dirs.ToList().ForEach(x =>
            {
                var el = new MainTitle(x);
                if (el != null)
                {
                    lMainTitle.Add(el);
                }
            });
            doc.MainTitle = lMainTitle;
            return doc;
        }

        public static IEnumerable<DirectoryInfo> GetDirectory(string path = "")
        {
            var dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            // We may be in the case of zip file, trying...
            if (dirs.Length == 0)
            {
                var zipFiles = GetFiles(path, "zip");
                zipFiles.ToList().ForEach(x => {
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
