
using BR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public class DataExtractorFactory
    {
        public static DataExtractor getDataExtractor(BANK bank)
        {
            DataExtractor obj = null;
            try
            {
                Type t = Type.GetType("Library." + bank.DataExtractor.Trim());
                if (t != null)
                {
                    obj = (DataExtractor)Activator.CreateInstance(t);

                }
            }
            catch (Exception ex)
            {
                ErrorUtil.logError(ex,"");
            }
            return obj;


        }
    }
}
