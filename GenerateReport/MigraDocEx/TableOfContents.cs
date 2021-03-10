using GenerateReport.Models.PDFDocument;
using MigraDoc.DocumentObjectModel;
using System.Linq;

namespace GenerateReport.MigraDocEx
{
    public class TableOfContents
    {
        public static void DefineTableOfContents(FinalDocument model)
        {
            if (!model.TOC)
            {
                return;
            }
            Section section = model.Document.AddSection();
            section.PageSetup.LeftMargin = 40;
            section.PageSetup.RightMargin = 40;
            section.AddPageBreak();
            Paragraph paragraph = section.AddParagraph("Contents");
            paragraph.Format.Font.Size = 20;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.SpaceAfter = 24;
            paragraph.Format.OutlineLevel = OutlineLevel.Level1;
            paragraph.Format.SpaceAfter = "2cm";

            // Main title
            model.MainTitle.ToList().ForEach(x =>
            {
                paragraph = section.AddParagraph();
                paragraph.Style = "TOC 1";
                Hyperlink hyperlinkLvl1 = null;

                // Sub title
                x.SubTitle.ToList().ForEach(u =>
                {
                    if (hyperlinkLvl1 == null)
                    {
                        hyperlinkLvl1 = paragraph.AddHyperlink(u.Id);
                        hyperlinkLvl1.AddText($"{x.Title}\t");
                        hyperlinkLvl1.AddPageRefField(u.Id);
                        paragraph.Format.SpaceAfter = "0.25cm";
                    }
                    paragraph = section.AddParagraph();
                    paragraph.Style = "TOC 2";
                    Hyperlink hyperlinkLvl2 = paragraph.AddHyperlink(u.Id);
                    hyperlinkLvl2.AddText($"{(char)160}{(char)160}{(char)160}{(char)160}{u.Title}\t");
                    hyperlinkLvl2.AddPageRefField(u.Id);
                });

                paragraph.Format.SpaceAfter = "1cm";
            });
        }
    }
}
