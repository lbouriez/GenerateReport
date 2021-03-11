using GenerateReport.Models.PDFDocument;
using MigraDoc.DocumentObjectModel;

namespace GenerateReport.MigraDocEx
{
    public class Cover
    {
        public static void DefineCover(FinalDocument model)
        {
            if (string.IsNullOrEmpty(model.Title) && string.IsNullOrEmpty(model.Logo))
            {
                return;
            }
            Section section = model.Document.AddSection();

            Paragraph paragraph;
            //if (!string.IsNullOrEmpty(model?.Logo))
            //{
            //    paragraph = section.AddParagraph();
            //    paragraph.Format.SpaceAfter = "3cm";
            //    Image image = section.AddImage(model.Logo);
            //    image.Width = "10cm";
            //}

            paragraph = section.AddParagraph(model.Title);
            paragraph.Format.Font.Size = 32;
            paragraph.Format.Font.Color = Colors.Black;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.SpaceBefore = "10cm";
            paragraph.Format.SpaceAfter = "12cm";

            // Add a footer
            //section.PageSetup.DifferentFirstPageHeaderFooter = true;
            //paragraph = section.Footers.FirstPage.AddParagraph("Rendering date: ");
            //paragraph.AddDateField();
        }
    }
}
