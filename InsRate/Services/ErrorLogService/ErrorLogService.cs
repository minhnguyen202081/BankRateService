using BR.ConstantLogic;
using BR.ErrorLogModel;
using BR.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.ErrorLogLogic
{
    class ErrorLogService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ErrorLogService));


        public void saveChanges(BRContext db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                logger.Error("saveChanges", e);
                throw new InvalidOperationException(MessageConstantLogic.ERROR_MODEL_DB_CONTEXT);
            }
        }
        public void addErrorLog(ERROR_LOG errorLog)
        {
            logger.Info("addErrorLog: " + errorLog.ID + " start!!!");
            using (BRContext db = new BRContext())
            {
                addErrorLog(errorLog, db);
                saveChanges(db);
            }
            logger.Info("addBank: " + errorLog.ID + " end!!!");
        }
        public void addErrorLog(ERROR_LOG errorLog, BRContext db)
        {
            ErrorLogRepository ur = new ErrorLogRepository(db);

            ur.insert(errorLog);
          
        }

      

        public List<ERROR_LOG> findAll(BRContext db)
        {
            ErrorLogRepository ur = new ErrorLogRepository(db);
            return ur.selectAll();
        }

        public List<ERROR_LOG> findAll()
        {
            logger.Info("findAll");
            using (BRContext db = new BRContext())
            {
                return findAll(db);
            }
        }


    
    }
}
