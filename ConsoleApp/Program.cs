using GenerateReport.Models;
using McMaster.Extensions.CommandLineUtils;

namespace ConsoleApp
{
    [Command(Name = "PDF report generator", Description = "Merge pdf documents and create cover and toc pages")]
    [HelpOption("-?")]
    class Program
    {
        private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option("-p|--path", Description = "Working path")]
        private string FolderPath { get; }

        [Option("-l|--logo", Description = "Logo file to add to the cover page")]
        private string DocLogo { get; }

        [Option("-f|--final", Description = "Filename to be created")]
        private string FinalFile { get; }

        [Option("-t|--title", Description = "Title on the cover page")]
        private string DocTitle { get; }

        [Option("-c|--toc", Description = "Table of content")]
        private bool TOC { get; }

        [Option("-a|--author", Description = "Name of the author")]
        private string Author { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private void OnExecute()
        {
            var parameters = new EntryParameters()
            {
                FileName = FinalFile,
                TitleDocument = DocTitle,
                WorkPath = FolderPath,
                Logo = DocLogo,
                TOC = TOC,
                Author = Author
            };
            GenerateReport.Entry.Generate(parameters);
        }
    }
}
