using BR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BR.RateHistoryModel
{
    public class RateHistoryRepository
    {
         private BRContext db;

         public RateHistoryRepository(BRContext db)
        {
            this.db = db;
        }

        public void insert(RATE_HISTORY rateHistory)
        {
            db.RATE_HISTORY.Add(rateHistory);
            
        }

        public void update(RATE_HISTORY rateHistory)
        {
            db.Entry(rateHistory).State = EntityState.Modified;
           
        }

        public void delete(RATE_HISTORY rateHistory)
        {
            db.Entry(rateHistory).State = EntityState.Deleted;
          
        }

        public List<RATE_HISTORY> selectAll()
        {
            return db.RATE_HISTORY.ToList();
        }

        public RATE_HISTORY select(string bankCode)
        {
            return db.RATE_HISTORY.Find(bankCode);
        }

        public List<RATE_HISTORY> selectLike(string bankCode)
        {
            return db.RATE_HISTORY.Where(uc => uc.BankCode.Contains(bankCode)).ToList();
        }

        public RATE_HISTORY Get(string bankCode, string tenorCode)
        {
            return db.RATE_HISTORY.Where(uc => uc.BankCode==bankCode && uc.TenorCode==tenorCode).FirstOrDefault();
        }
    }
}
