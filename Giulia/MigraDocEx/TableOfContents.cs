using GenerateReport.Model;
using MigraDoc.DocumentObjectModel;
using System.Linq;

namespace GenerateReport.MigraDocEx
{
    public class TableOfContents
    {
        /// <summary>
        /// Defines the cover page.
        /// </summary>
        public static void DefineTableOfContents(Doc model)
        {
            Section section = model.Document.LastSection;
            section.AddPageBreak();
            Paragraph paragraph = section.AddParagraph("Contents");
            paragraph.Format.Font.Size = 14;
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
