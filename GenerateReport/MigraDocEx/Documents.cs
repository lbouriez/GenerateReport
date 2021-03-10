using GenerateReport.Models.PDFDocument;
using MigraDoc.DocumentObjectModel;

namespace GenerateReport.MigraDocEx
{
    public class Documents
    {
        public static void CreateDocument(FinalDocument model)
        {
            model.Document = new Document();
            var document = model.Document;

            document.Info.Title = model.Title;
            document.Info.Subject = model.Title;
            document.Info.Author = "Giulia Ippolito";
        }
    }
}
