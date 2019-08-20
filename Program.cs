using ceTe.DynamicPDF;
using ceTe.DynamicPDF.Cryptography;
using ceTe.DynamicPDF.Merger;
using System;
using System.Text.RegularExpressions;

namespace example_password_protect_pdf_dotnet
{
    // This example shows how to create a PDF with password protection and password protect an existing PDF.
    // It references the ceTe.DynamicPDF.CoreSuite.NET NuGet package.
    class Program
    {
        static void Main(string[] args)
        {
            PasswordProtectNewPDF();

            AddPasswordToExistingPDF();
        }

        // Create a password protected PDF from scratch.
        // This code uses DynamicPDF Generator for .NET product.
        // Use the ceTe.DynamicPDF namespace for the Document and Page classes.
        // Use the ceTe.DynamicPDF.Cryptography namespace for the Aes256Security class.
        private static void PasswordProtectNewPDF()
        {
            //Create a Document object
            Document document = new Document();

            //Create a Page object and add it to the Document
            Page page = new Page();
            document.Pages.Add(page);

            //Create a Security class object with passwords and set it to the Document
            Aes256Security security = new Aes256Security("owner", "user");
            document.Security = security;

            document.Draw("output-new-pdf.pdf");
        }

        // Add password protection for an existing PDF.
        // This code uses DynamicPDF Merger for .NET product.
        // Use the ceTe.DynamicPDF.Merger namespace for the MergeDocument class.
        private static void AddPasswordToExistingPDF()
        {
            //Create PdfDocument object with the existing PDF file and create MergeDocument using PdfDocument
            PdfDocument pdf = new PdfDocument(GetResourcePath("doc-a.pdf"));
            MergeDocument document = new MergeDocument(pdf);

            //Create Security object with passwords and set it to the Document
            Aes256Security security = new Aes256Security("owner", "user");
            document.Security = security;

            //Save the Document
            document.Draw("output-existing-pdf.pdf");
        }

        // This is a helper function to get the full path to a file in the Resources folder.
        public static string GetResourcePath(string inputFileName)
        {
            var exePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return System.IO.Path.Combine(appRoot, "Resources", inputFileName);
        }
    }
}
