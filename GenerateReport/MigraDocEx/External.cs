using GenerateReport.Models.PDFDocument;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using System.Linq;

namespace GenerateReport.MigraDocEx
{
    public static class External
    {
        public static void MergeFiles(FinalDocument model)
        {
            Section section = model.Document.LastSection;
            // For each title
            model.MainTitle.ToList().ForEach(mainTitleElm =>
            {
                bool isTitleAdded = false;

                // For each subtitle
                mainTitleElm.SubTitle.ToList().ForEach(subtitleElm =>
                {
                    bool isSubTitleAdded = false;

                    PdfDocument inputDocument = PdfReader.Open(subtitleElm.FilePath, PdfDocumentOpenMode.Import);
                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        section = model.Document.AddSection();
                        #region Define the orientation
                        bool isLandscape = inputDocument.Pages[idx].Orientation == PdfSharpCore.PageOrientation.Landscape;
                        section.PageSetup.PageHeight = isLandscape ? inputDocument.Pages[idx].Width.Value : inputDocument.Pages[idx].Height.Value;
                        section.PageSetup.PageWidth = isLandscape ? inputDocument.Pages[idx].Height.Value : inputDocument.Pages[idx].Width.Value;
                        section.PageSetup.Orientation = isLandscape ? Orientation.Landscape : Orientation.Portrait;
                        #endregion
                        isTitleAdded = AddTitle(isTitleAdded, section, mainTitleElm, subtitleElm);
                        isSubTitleAdded = AddSubtitle(isSubTitleAdded, section, subtitleElm);

                        #region Add the page
                        Image image = section.AddImage($"{subtitleElm.FilePath}#{idx + 1}");
                        image.LockAspectRatio = true;
                        image.RelativeHorizontal = RelativeHorizontal.Page;
                        image.RelativeVertical = RelativeVertical.Page;

                        image.WrapFormat.Style = WrapStyle.Through;
                        #endregion
                    }
                });
            });
        }

        private static bool AddTitle(bool isTitleAdded, Section section, MainTitle mainTitleElm, SubTitle subtitleElm)
        {
            if (!isTitleAdded)
            {
                Paragraph paragraph = section.AddParagraph(mainTitleElm.Title);
                paragraph.Format.Font.Size = 0.01;
                paragraph.Format.Font.Color = Colors.White;
                paragraph.Format.OutlineLevel = OutlineLevel.Level1;
                paragraph.AddBookmark(subtitleElm.Id);
                isTitleAdded = true;
            }
            return isTitleAdded;
        }

        private static bool AddSubtitle(bool isSubTitleAdded, Section section, SubTitle subtitleElm)
        {
            if (!isSubTitleAdded)
            {
                Paragraph paragraph = section.AddParagraph(subtitleElm.Title);
                paragraph.Format.Font.Size = 0.01;
                paragraph.Format.Font.Color = Colors.White;
                paragraph.Format.OutlineLevel = OutlineLevel.Level2;
                paragraph.AddBookmark(subtitleElm.Id);
                isSubTitleAdded = true;
            }
            return isSubTitleAdded;
        }
    }
}
