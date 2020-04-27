using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BR.BankLogic;
using BR.Models;
using BR.TenorLogic;
using BR.BankRuleLogic;
using BR.RateHistoryLogic;

namespace Library
{
   public class AutoExtractInterest
    {
       public static void mainProcess()
       {
            string bankrule = "";
           try
           {
               BankService bs = new BankService();
               TenorService ts = new TenorService();
               BankRuleService brs = new BankRuleService();

               List<BANK> bankList = bs.findAll();
               string groupCode = Guid.NewGuid().ToString();



               List<TENOR> tenorList = ts.findAll();
               foreach (BANK b in bankList)
               {
                   if (DataExtractorFactory.getDataExtractor(b) != null)
                   {
                       string bankData = DataExtractorFactory.getDataExtractor(b).extractData(b);
                       foreach (TENOR t in tenorList)
                       {
                           BANK_RULES br = brs.find(b.BankCode, t.TenorCode);
                            bankrule = br.BankRule;
                           if (br != null)
                           {
                               string rule = br.BankRule;

                               string result = RuleProcessor.process(bankData, rule,b.BankCode);
                               RATE_HISTORY rateHistory = new RATE_HISTORY();
                               RateHistoryService rhs = new RateHistoryService();

                               rateHistory.BankCode = b.BankCode;
                               rateHistory.TenorCode = t.TenorCode;
                               rateHistory.InsRate = result;
                               rateHistory.GroupCode = groupCode;
                               rateHistory.Timestamp = DateTime.Now;

                               rhs.addRateHistory(rateHistory);

                           }
                       }

                   }
               }
           }
           catch(Exception ex)
           {
               ErrorUtil.logError(ex, bankrule);
           }
                    
       }
      
    





    }
}
