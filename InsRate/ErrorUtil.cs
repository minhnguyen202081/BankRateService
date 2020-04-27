using BR.ErrorLogLogic;
using BR.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
   public static class ErrorUtil
    {
       private static readonly ILog logger = LogManager.GetLogger(typeof(ErrorUtil));
                    public  static void logError(Exception exception,string error)
                    {
                        ErrorLogService ers = new ErrorLogService();
                        ERROR_LOG erl = new ERROR_LOG();
            erl.ID = Guid.NewGuid();
                        erl.Timestamp = DateTime.Now;
                        erl.ErrorMessage = exception.Message + "Error:" + error;
                        erl.StackTrace = exception.StackTrace;
                        erl.InnerException =  exception.InnerException == null ? "" : exception.InnerException.ToString();
                        
                        ers.addErrorLog(erl);
                    }
                public static void logInfo(string info)
                {
                    logger.Info("Info :"+ info);
                }

    }
}
