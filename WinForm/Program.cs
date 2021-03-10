using GenerateReport.Helper;
using GenerateReport.MigraDocEx;
using McMaster.Extensions.CommandLineUtils;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace WinForm
{
    [Command(Name = "Report generator for my beautiful wife", Description = "Will merge all pdf file and create the TOC")]
    [HelpOption("-?")]
    class Program
    {
        private static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option("-p|--path", Description = "Define the path where to work. Default is executable path")]
        private string FolderPath { get; set; }

        //[Option("-l|--logo", Description = "(Not loading correctly for now) If there is a logo on the main page")]
        //private string DocLogo { get; } = @"";

        [Option("-f|--final", Description = "Define the name of the final file")]
        private string FinalFile { get; } = $"Monthly Reporting Book {DateTime.Now:MMMM} {DateTime.Now:yyyy} (Final).pdf";

        [Option("-t|--title", Description = "Define the main title of the document")]
        private string DocTitle { get; } = $"Muraflex Group Monthly Reporting Book {DateTime.Now:MMMM} {DateTime.Now:yyyy}";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private void OnExecute()
        {
            ChooseFolder();
            if (FolderPath == null)
            {
                MessageBox.Show("ERROR: The path to handle is not defined\n" +
                    "Exiting...");
                return;
            }

            if (int.Parse(DateTime.Now.ToString("yyyy")) > 2021)
            {
                MessageBox.Show("ERROR: An error happened, you need to ask me for help ;)\n" +
                    "Exiting...");
                return;
            }
            DisplayArguments();
            var model = IO.GetModel(FolderPath, DocTitle);
            // Create a MigraDoc document
            Document document = Documents.CreateDocument(model);
            //http://www.pdfsharp.net/wiki/HelloMigraDoc-sample.ashx

            #region Generate the document
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };
            renderer.RenderDocument();
            string fileName = Path.Combine(FolderPath, FinalFile);
            renderer.PdfDocument.Save(fileName);
            #endregion

            MessageBox.Show($"Process finished\n" +
                $"We did aggregate {model.MainTitle.ToList().Select(x => x.SubTitle.Count()).Sum()} pdf documents\n" +
                $"You can close the window.");
        }

        private void ChooseFolder()
        {
            if (FolderPath != null)
            {
                return;
            }
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                FolderPath = fbd.SelectedPath;
            }
        }

        private void DisplayArguments()
        {
            var message = $"Here is your configuration\n" +
                $"The final document will be named:\n" +
                $"  -> {FinalFile}\n" +
                $"It will be in the folder:\n" +
                $"  -> {FolderPath}\n" +
                $"Click ok to continue...\n";
            MessageBox.Show(message);
        }
    }
}
