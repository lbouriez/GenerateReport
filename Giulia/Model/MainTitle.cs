using GenerateReport.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Helper.Model
{
    public class MainTitle
    {
        public MainTitle(DirectoryInfo dir)
        {
            if (dir == null)
            {
                throw new ArgumentNullException(nameof(dir));
            }
            Title = dir.Name;
            SubTitle = GetSubtitle(dir);
            Id = Guid.NewGuid().ToString("N");
        }

        private static IEnumerable<SubTitle> GetSubtitle(DirectoryInfo dir)
        {
            List<SubTitle> subList = new List<SubTitle>();
            IO.GetFiles(dir.FullName).ToList().ForEach(x =>
            {
                var t = new SubTitle(x);
                if (subList != null)
                {
                    subList.Add(t);
                }
            });
            return subList;
        }

        public string Title { get; set; }
        public IEnumerable<SubTitle> SubTitle { get; set; }
        public string Id { get; set; }
    }
}
