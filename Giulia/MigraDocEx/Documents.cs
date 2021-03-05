using GenerateReport.MigraDocExEx;
using GenerateReport.Model;
using MigraDoc.DocumentObjectModel;

namespace GenerateReport.MigraDocEx
{
    public class Documents
    {
        public static Document CreateDocument(Doc model)
        {
            model.Document = new Document();
            var document = model.Document;

            document.Info.Title = model.Title;
            document.Info.Subject = model.Title;
            document.Info.Author = "Giulia Ippolito";

            // Define the style of the document
            Styles.DefineStyles(model);

            // Define the cover
            Cover.DefineCover(model);
            TableOfContents.DefineTableOfContents(model);
            External.MergeFiles(model);

            return document;
        }
    }
}
