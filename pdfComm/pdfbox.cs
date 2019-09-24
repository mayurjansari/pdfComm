using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pdfComm
{
    public class pdfbox
    {
        public static String PDFText(String PDFFilePath)
        {
            PDDocument doc = PDDocument.load(PDFFilePath);
            PDFTextStripper stripper = new PDFTextStripper();
            return stripper.getText(doc);
        }

        public static java.util.List SplitPDFFile(string SourcePath, int splitPageQty = 1)
        {
            var doc = PDDocument.load(SourcePath);
            var splitter = new Splitter();
            splitter.setSplitAtPage(splitPageQty);

            return (java.util.List)splitter.split(doc);
        }

        public static object[] SplitPDFFileImprove(string SourcePath, int splitPageQty = 1)
        {
            var doc = PDDocument.load(SourcePath);
            var splitter = new Splitter();
            splitter.setSplitAtPage(splitPageQty);

            return (object[])splitter.split(doc).toArray();
        }
        public static void ExtractToSingleFile(int[] PageNumbers, string sourceFilePath, string outputFilePath)
        {
            var originalDocument = PDDocument.load(sourceFilePath);
            var originalCatalog = originalDocument.getDocumentCatalog();
            java.util.List sourceDocumentPages = originalCatalog.getAllPages();
            var newDocument = new PDDocument();

            foreach (var pageNumber in PageNumbers)
            {
                // Page numbers are 1-based, but PDPages are contained in a zero-based array:
                int pageIndex = pageNumber - 1;
                newDocument.addPage((PDPage)sourceDocumentPages.get(pageIndex));
            }
            newDocument.save(outputFilePath);
        }

        public void ExtractAndMergePages()
        {
            string sourcePath = @"C:\SomeDirectory\YourFile.pdf";
            string outputPath = @"C:\SomeDirectory\YourNewFile.pdf";
            int[] pageNumbers = { 1, 6, 7 };
            PDFFileSplitter.ExtractToSingleFile(pageNumbers, sourcePath, outputPath);
        }
    }
}
