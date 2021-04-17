﻿using GenerateReport.Models.PDFDocument;
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
            model.MainTitle.ToList().ForEach(x =>
            {
                bool isTitleAdded = false;

                // For each subtitle
                x.SubTitle.ToList().ForEach(u =>
                {
                    bool isSubTitleAdded = false;

                    PdfDocument inputDocument = PdfReader.Open(u.FilePath, PdfDocumentOpenMode.Import);
                    // Iterate pages
                    int count = inputDocument.PageCount;
                    for (int idx = 0; idx < count; idx++)
                    {
                        #region Define the orientation
                        section = model.Document.AddSection();

                        if (inputDocument.Pages[idx].Orientation == PdfSharpCore.PageOrientation.Landscape)
                        {
                            section.PageSetup.PageHeight = inputDocument.Pages[idx].Width.Value;
                            section.PageSetup.PageWidth = inputDocument.Pages[idx].Height.Value;
                            section.PageSetup.Orientation = Orientation.Landscape;
                        }
                        else
                        {
                            section.PageSetup.PageHeight = inputDocument.Pages[idx].Height.Value;
                            section.PageSetup.PageWidth = inputDocument.Pages[idx].Width.Value;

                            section.PageSetup.Orientation = Orientation.Portrait;
                        }
                        #endregion
                        // First page, we add the title and create
                        if (!isTitleAdded)
                        {
                            Paragraph paragraph = section.AddParagraph(x.Title);
                            paragraph.Format.Font.Size = 0.01;
                            paragraph.Format.Font.Color = Colors.White;
                            paragraph.Format.OutlineLevel = OutlineLevel.Level1;
                            paragraph.AddBookmark(u.Id);
                            isTitleAdded = true;
                        }
                        if (!isSubTitleAdded)
                        {
                            Paragraph paragraph = section.AddParagraph(u.Title);
                            paragraph.Format.Font.Size = 0.01;
                            paragraph.Format.Font.Color = Colors.White;
                            paragraph.Format.OutlineLevel = OutlineLevel.Level2;
                            paragraph.AddBookmark(u.Id);
                            isSubTitleAdded = true;
                        }


                        #region Add the page
                        Image image = section.AddImage($"{u.FilePath}#{idx + 1}");
                        image.LockAspectRatio = true;
                        image.RelativeHorizontal = RelativeHorizontal.Page;
                        image.RelativeVertical = RelativeVertical.Page;

                        image.WrapFormat.Style = WrapStyle.Through;
                        #endregion
                    }
                });
            });
        }
    }
}
