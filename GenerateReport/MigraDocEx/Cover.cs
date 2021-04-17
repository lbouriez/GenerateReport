using GenerateReport.Models.PDFDocument;
using MigraDoc.DocumentObjectModel;

namespace GenerateReport.MigraDocEx
{
    public static class Cover
    {
        public static void DefineCover(FinalDocument model)
        {
            if (string.IsNullOrEmpty(model.Title) && string.IsNullOrEmpty(model.Logo))
            {
                return;
            }
            Section section = model.Document.AddSection();

            Paragraph paragraph;

            paragraph = section.AddParagraph(model.Title);
            paragraph.Format.Font.Size = 32;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.SpaceBefore = "10cm";
            paragraph.Format.SpaceAfter = "12cm";
        }
    }
}
