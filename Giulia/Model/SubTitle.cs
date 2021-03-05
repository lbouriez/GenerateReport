using System;
using System.IO;

namespace Helper.Model
{
    public class SubTitle
    {
        public SubTitle(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            Title = Path.GetFileNameWithoutExtension(file.Name);
            if (!string.IsNullOrEmpty(Title) && char.IsDigit(Title[0]))
            {
                Title = Title[1..];
            }
            FilePath = file.FullName;
            Id = Guid.NewGuid().ToString("N");
        }

        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Id { get; set; }
    }
}
