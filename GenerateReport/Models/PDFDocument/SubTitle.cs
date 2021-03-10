using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GenerateReport.Models.PDFDocument
{
    public class SubTitle
    {
        public SubTitle(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            Title = GetTitle(file.Name);
            if (!string.IsNullOrEmpty(Title) && char.IsDigit(Title[0]))
            {
                Title = Title[1..];
            }
            FilePath = file.FullName;
            Id = Guid.NewGuid().ToString("N");
        }

        private static string GetTitle(string fileName)
        {
            string res = Path.GetFileNameWithoutExtension(fileName);
            res = Regex.Replace(res, @"\s+", " ");
            res = res.Replace($"- {DateTime.Now:MMMM} {DateTime.Now:yyyy}", string.Empty);
            return res.Trim();
        }

        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Id { get; set; }
    }
}
