using BR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    public class VTBDataExtractor : DataExtractor
    {
        public string extractData(BANK bank)
        {
            string result = "";
            try
            {
                HttpClient httpclient = new HttpClient();
                result = httpclient.getData(bank.BankLink);
                result = Regex.Replace(result, @"\t|\n|\r", "").Replace("            ", "");
            }
            catch (Exception ex)
            {
                ErrorUtil.logError(ex,"");
            }
            return result;
        }
    }
}

