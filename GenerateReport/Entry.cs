using GenerateReport.Helper;
using GenerateReport.MigraDocEx;
using GenerateReport.Models;
using GenerateReport.Models.PDFDocument;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace GenerateReport
{
    public static class Entry
    {
        public static void Generate(EntryParameters parameters)
        {
            try
            {
                DisplayArguments(parameters);

                var model = new FinalDocument(parameters);

                // Create the document
                Documents.CreateDocument(model);

                // Define the styles
                DocumentStyles.DefineStyles(model);

                // Create the cover page
                Cover.DefineCover(model);

                // Create the TOC
                TableOfContents.DefineTableOfContents(model);

                // Merge the files
                External.MergeFiles(model);

                // Generate the document
                GenerateFile(model.Document, parameters.WorkPath, parameters.FileName);

                Logger.Log($"Process ended correctly.\n" +
                    $"{model.MainTitle.ToList().Select(x => x.SubTitle.Count()).Sum()} files were merged.\n" +
                    $"You can find everything inside => {parameters.WorkPath}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                Logger.Log("Process finished, you can close the window...");
            }
        }

        private static void DisplayArguments(EntryParameters parameters)
        {
            Logger.Warning($"Here is your configuration\n" +
                $"The final document will be named:\n" +
                $"  -> {parameters.FileName}\n" +
                $"It will be in the folder:\n" +
                $"  -> {parameters.WorkPath}\n" +
                $"Process running, wait...\n");
        }

        private static void GenerateFile(Document doc, string WorkFolder, string FileName)
        {
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(doc, "MigraDoc.mdddl");
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = doc
            };
            renderer.RenderDocument();
            string fileName = Path.Combine(WorkFolder, FileName);
            renderer.PdfDocument.Save(fileName);
        }
    }
}
