using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.Models;
using System.Text.RegularExpressions;
using System.Net;
using System.Configuration;
using PdfToText;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Library
{
    public class PDFDataExtractor : DataExtractor
    {
        public string extractData(BANK bank)
        {
            try
            {
                DeleteAllFile(ConfigurationManager.AppSettings["downloadLocation"]);
            }
            catch (Exception ex)
            {
            }
            string result = "";
            result= DownloadFile(bank.BankLink);
           
            
            try
            {
                //HttpClient httpclient = new HttpClient();
                //result = httpclient.getData(bank.BankLink);
                result = Regex.Replace(result, @"\t|\n|\r", "").Replace("            ", "");
            }
            catch (Exception ex)
            {
                ErrorUtil.logError(ex,bank.BankCode);
            }
            return result;
        }

        public static string DownloadFile(string url)
        {
            string fileLocation = ConfigurationManager.AppSettings["downloadLocation"];
            string fileName = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string fullFileName = fileLocation + @"\" + fileName + ".pdf";
            string result = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, fullFileName);
                }



                PDFParser pdfParser = new PDFParser();
                pdfParser.ExtractText(fullFileName, System.IO.Path.GetFileNameWithoutExtension(fullFileName) + ".txt");
                try
            {
                    //using (StreamReader sr = new StreamReader(System.IO.Path.GetFileNameWithoutExtension(fullFileName) + ".txt"))
                    //{
                    //    // Read the stream to a string, and write the string to the console.
                    //    result = sr.ReadToEnd();

                    //}
                    //string output=    System.IO.Path.GetFileNameWithoutExtension(fullFileName) + ".txt";
                    //    var bytes = File.ReadAllBytes(fullFileName);
                    //    File.WriteAllText(output, ConvertToText(bytes), Encoding.UTF8);
                    var bytes = File.ReadAllBytes(fullFileName);
                    result = ConvertToText(bytes);

                }
            catch (Exception ex)
            {
                ErrorUtil.logError(ex,"");
            }
           
            }
            catch (Exception ex)
            {
                ErrorUtil.logError(ex,"");
            }
         
            return result;
        }

        public static void DeleteAllFile(string path)
        {


            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }


        }
        private static string ConvertToText(byte[] bytes)
        {
            var sb = new StringBuilder();

            try
            {
                var reader = new PdfReader(bytes);
                var numberOfPages = reader.NumberOfPages;

                for (var currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    sb.Append(PdfTextExtractor.GetTextFromPage(reader, currentPageIndex,strategy));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return sb.ToString();
        }

    }
}
