using GenerateReport.Models;
using System;
using System.IO;

namespace MuraflexBible
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (int.Parse(DateTime.Now.ToString("yyyy")) > 2021)
            {
                Console.WriteLine("ERROR: An error happened, you need to ask me for help ;)\n" +
                    "Exiting...");
                return;
            }

            var parameters = new EntryParameters()
            {
                FileName = $"Monthly Reporting Book {DateTime.Now:MMMM} {DateTime.Now:yyyy} (Final).pdf",
                TitleDocument = $"Muraflex Group Monthly Reporting Book {DateTime.Now:MMMM} {DateTime.Now:yyyy}",
                WorkPath = Directory.GetCurrentDirectory(),
                Logo = null,
                TOC = true,
                Author = "Giulia Ippolito"
            };

            GenerateReport.Entry.Generate(parameters);
        }
    }
}
