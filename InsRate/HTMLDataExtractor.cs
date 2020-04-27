using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BR.Models;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Library
{
    public class HTMLDataExtractor: DataExtractor
    {
        public string extractData(BANK bank)
        {
            string result = "";
            try
            {
            string replacestring =    ConfigurationManager.AppSettings["ReplaceString"];
                string bankLink = bank.BankLink;
                string str = "";
               if(bank.BankLink.Contains(replacestring))
                {
                    str = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString();
                }
                bankLink = bankLink.Replace(replacestring, str);
                if (bank.BankCode == "OCB")
                {
                    OCBHttpClient httpOCBclient = new OCBHttpClient();
                    result = httpOCBclient.getData(@"https://www.ocb.com.vn/");
                    result = Utilities.RemoveWhitespace(Regex.Replace(result, @"\t|\n|\r", "").Replace("            ", ""));
                }
                else
                {
                    HttpClient httpclient = new HttpClient();
                    result = httpclient.getData(bankLink);
                    result = Utilities.RemoveWhitespace(Regex.Replace(result, @"\t|\n|\r", "").Replace("            ", ""));
                }
            }
            catch (Exception ex)
            {
                ErrorUtil.logError( ex,bank.BankCode );
            }
            return result;
        }
    }
}
