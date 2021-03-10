using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;

namespace MuraflexBible
{
    [Command(Name = "Report generator for my beautiful wife", Description = "Will merge all pdf file and create the TOC")]
    [HelpOption("-?")]
    class Program
    {
        private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option("-p|--path", Description = "Define the path where to work. Default is executable path")]
        private string FolderPath { get; } = Directory.GetCurrentDirectory();

        [Option("-l|--logo", Description = "(Not working yet) If there is a logo on the main page")]
        private string DocLogo { get; } = "";

        [Option("-f|--final", Description = "Define the name of the final file")]
        private string FinalFile { get; } = $"Monthly Reporting Book {DateTime.Now:MMMM} {DateTime.Now:yyyy} (Final).pdf";

        [Option("-t|--title", Description = "Define the main title of the document")]
        private string DocTitle { get; } = $"Muraflex Group Monthly Reporting Book {DateTime.Now:MMMM} {DateTime.Now:yyyy}";

        [Option("-c|--toc", Description = "Define if the table of content should be written or not")]
        private bool TOC { get; } = true;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private void OnExecute()
        {
            if (int.Parse(DateTime.Now.ToString("yyyy")) > 2021)
            {
                Console.WriteLine("ERROR: An error happened, you need to ask me for help ;)\n" +
                    "Exiting...");
                return;
            }
            GenerateReport.Entry.Generate(FolderPath, DocLogo, FinalFile, DocTitle, TOC);
        }
    }
}
