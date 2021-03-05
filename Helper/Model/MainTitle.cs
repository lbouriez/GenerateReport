using System;
using System.Collections.Generic;
using System.IO;

namespace Helper.Model
{
    public class MainTitle
    {
        public MainTitle(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }
            Title = Directory.GetParent(fileInfo.FullName).Name;
        }
        public MainTitle()
        {

        }
        public string Title { get; set; }
        public int Order { get; set; }
        public IEnumerable<SubTitle> SubTitle { get; set; }
    }
}
