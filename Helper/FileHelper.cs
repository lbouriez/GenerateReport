using Helper.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Helper
{
    public static class FileHelper
    {
        public static IEnumerable<MainTitle> GetFileList(string path = "")
        {
            string[] allfiles = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in allfiles)
            {
                FileInfo info = new FileInfo(file);

                var t = new MainTitle(info);
                // Do something with the Folder or just add them to a list via nameoflist.add();
            }
            return null;
        }
    }
}
