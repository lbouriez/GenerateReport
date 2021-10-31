using GenerateReport.Models;
using System;
using System.IO;

namespace MuraflexBible
{
    static class Program
    {
        private static void Main()
        {
            var parameters = new EntryParameters()
            {
                FileName = $"{DateTime.Now:yyyy}-{DateTime.Now.AddMonths(-1):MM}-{DateTime.Now:dd} - Monthly Reporting Book-Bible.pdf",
                TitleDocument = $"Muraflex Group Monthly Reporting Book {DateTime.Now.AddMonths(-1):MMMM} {DateTime.Now:yyyy}",
                FinalPath = @"F:\Executive Monthly Reporting Book_Bible\2021",
                WorkPath = @"F:\Executive Monthly Reporting Book_Bible\Next Bible",
                Logo = null,
                TOC = true,
                Author = "Giulia Ippolito"
            };

            GenerateReport.Entry.Generate(parameters);
            CleanForMuraflex(parameters);
        }

        private static void CleanForMuraflex(EntryParameters parameters)
        {
            // Move all the documents used into the personal folder for backup
            var backupFolder = $@"C:\Users\gippolito\OneDrive - Muraflex inc\+Monthly Executive Book- The Bible\{DateTime.Now:yyyy}\{DateTime.Now.AddMonths(-1):MM} - Executive Reporting Book";

            if (!Directory.Exists(parameters.WorkPath))
            {
                return;
            }

            // Create directories
            foreach (string dirPath in Directory.GetDirectories(parameters.WorkPath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(parameters.WorkPath, backupFolder));

            // Copy all the files
            foreach (string newPath in Directory.GetFiles(parameters.WorkPath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(parameters.WorkPath, backupFolder), true);

            // Clean server folder
            ConsoleKey response;
            do
            {
                Console.Write("Amore, should I clean the files in ther server, I did backup them into your onedrive (default=yes)??? [y/n]");
                response = Console.ReadKey(false).Key;
                if (response == ConsoleKey.Enter)
                {
                    response = ConsoleKey.Y;
                }

            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            bool confirmed = response == ConsoleKey.Y;
            if (confirmed)
            {
                foreach (string newPath in Directory.GetFiles(parameters.WorkPath, "*.*",
                SearchOption.AllDirectories))
                    File.Delete(newPath);
            }
        }
    }
}
