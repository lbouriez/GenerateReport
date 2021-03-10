using McMaster.Extensions.CommandLineUtils;

namespace ConsoleApp
{
    [Command(Name = "Report generator for my beautiful wife", Description = "Will merge all pdf file and create the TOC")]
    [HelpOption("-?")]
    class Program
    {
        private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option("-p|--path", Description = "Define the path where to work. Default is executable path")]
        private string FolderPath { get; }

        [Option("-l|--logo", Description = "Add a logo on the cover page")]
        private string DocLogo { get; }

        [Option("-f|--final", Description = "Define the name of the final file")]
        private string FinalFile { get; }

        [Option("-t|--title", Description = "Define the main title of the document")]
        private string DocTitle { get; }

        [Option("-c|--toc", Description = "Define if the table of content should be written or not")]
        private bool TOC { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private void OnExecute()
        {
            GenerateReport.Entry.Generate(FolderPath, DocLogo, FinalFile, DocTitle, TOC);
        }
    }
}
