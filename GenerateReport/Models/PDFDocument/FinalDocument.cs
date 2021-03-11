using GenerateReport.Helper;
using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GenerateReport.Models.PDFDocument
{
    public class FinalDocument
    {
        private string _logo;

        public FinalDocument(EntryParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (string.IsNullOrEmpty(parameters.WorkPath))
            {
                throw new ArgumentNullException(nameof(parameters), "The FolderPath is null");
            }

            Title = parameters.TitleDocument;
            Logo = parameters.Logo;
            MainTitle = GetMainTitle(parameters.WorkPath);
            TOC = parameters.TOC;
            Author = parameters.Author;
        }

        private static IEnumerable<MainTitle> GetMainTitle(string path)
        {
            if (!Directory.Exists(path))
            {
                throw new ArgumentException("The folder path given in parameter is incorrect");
            }
            List<MainTitle> lMainTitle = new List<MainTitle>();
            var dirs = IO.GetDirectory(path);
            dirs.ToList().ForEach(x =>
            {
                var el = new MainTitle(x);
                if (el != null)
                {
                    lMainTitle.Add(el);
                }
            });
            return lMainTitle;
        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Logo
        {
            get { return _logo; }
            set
            {
                if (File.Exists(value))
                {
                    _logo = value;
                }
            }
        }
        public IEnumerable<MainTitle> MainTitle { get; set; }
        public Document Document { get; set; }
        public bool TOC { get; set; }
    }
}
