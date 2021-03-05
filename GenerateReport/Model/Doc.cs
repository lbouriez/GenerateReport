using Helper.Model;
using MigraDoc.DocumentObjectModel;
using System.Collections.Generic;

namespace GenerateReport.Model
{
    public class Doc
    {
        public Doc(string title)
        {
            Title = title;

            //if (File.Exists(logo))
            //{
            //    Logo = logo;
            //}
        }
        public string Title { get; set; }
        //public string Logo { get; set; }
        public IEnumerable<MainTitle> MainTitle { get; set; }
        public Document Document { get; set; }
        public PageFormat PageFormat { get; set; } = PageFormat.A4;
    }
}
