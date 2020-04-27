using BR.ConstantLogic;
using BR.Models;
using BR.RateHistoryModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.RateHistoryLogic
{
   public class RateHistoryService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RateHistoryService));


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
        public void addRateHistory(RATE_HISTORY rateHistory)
        {
            logger.Info("addRateHistory: " + rateHistory.BankCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                addRateHistory(rateHistory, db);
                saveChanges(db);
            }
            logger.Info("addTenor: " + rateHistory.BankCode + " end!!!");
        }
        public void addRateHistory(RATE_HISTORY rateHistory, BRContext db)
        {
            RateHistoryRepository ur = new RateHistoryRepository(db);
            //if (ur.select(rateHistory.TenorCode) == null)
            //{
            ur.insert(rateHistory);
            //}
            //else
            //{
            //    logger.Warn("addTenor: " + MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ": " + tenor.TenorCode);
            //    throw new ArgumentException(MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ": " + tenor.TenorCode);
            //}
        }

        public void editRateHistory(RATE_HISTORY rateHistory, BRContext db)
        {
            RateHistoryRepository ur = new RateHistoryRepository(db);
            ur.update(rateHistory);
        }

        public void editRateHistory(RATE_HISTORY rateHistory)
        {
            logger.Info("editRateHistory: " + rateHistory.BankCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                editRateHistory(rateHistory, db);
                saveChanges(db);
            }
            logger.Info("editTenor: " + rateHistory.BankCode + " end!!!");
        }
        public void deleteRateHistory(RATE_HISTORY rateHistory, BRContext db)
        {
            RateHistoryRepository ur = new RateHistoryRepository(db);
            ur.delete(rateHistory);
        }

        public void deleteRateHistory(RATE_HISTORY rateHistory)
        {
            logger.Info("DeletRateHistory: " + rateHistory.BankCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                deleteRateHistory(rateHistory, db);
                saveChanges(db);
            }
            logger.Info("DeletRateHistory: " + rateHistory.BankCode + " end!!!");
        }

        public void deleteTenor(RATE_HISTORY rateHistory)
        {
            logger.Info("deleteRateHistory: " + rateHistory.BankCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                deleteRateHistory(rateHistory, db);
                saveChanges(db);
            }
            logger.Info("deleteRateHistory: " + rateHistory.BankCode + " end!!!");
        }

        public List<RATE_HISTORY> findAll(BRContext db)
        {
            RateHistoryRepository ur = new RateHistoryRepository(db);
            return ur.selectAll();
        }

        public List<RATE_HISTORY> findAll()
        {
            logger.Info("findAll");
            using (BRContext db = new BRContext())
            {
                return findAll(db);
            }
        }


        public RATE_HISTORY find(string bankCode, BRContext db)
        {
            RATE_HISTORY rateHistory = new RATE_HISTORY();
            RateHistoryRepository ur = new RateHistoryRepository(db);
            rateHistory = ur.select(bankCode);

            return rateHistory;
        }

        public RATE_HISTORY find(string bankCode)
        {
            logger.Info("find: " + bankCode);
            RATE_HISTORY rateHistory = new RATE_HISTORY();
           
                using (BRContext db = new BRContext())
                {
                    rateHistory = find(bankCode, db);
                }
         
            return rateHistory;
        }

        public RATE_HISTORY get(string bankCode, string tenorCode, BRContext db)
        {
            RATE_HISTORY rateHistory = new RATE_HISTORY();
            RateHistoryRepository ur = new RateHistoryRepository(db);
            rateHistory = ur.Get(bankCode,tenorCode);

            return rateHistory;
        }
        public RATE_HISTORY get(string bankCode, string tenorCode)
        {
            logger.Info("get: " + bankCode);
            RATE_HISTORY rateHistory = new RATE_HISTORY();
           
                using (BRContext db = new BRContext())
                {
                    rateHistory = get(bankCode,tenorCode, db);
                }
           
            return rateHistory;
        }

    }
}
