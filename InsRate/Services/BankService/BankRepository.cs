using BR.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BR.BankModel
{
    public class BankRepository
    {
          private BRContext db;

          public BankRepository(BRContext db)
        {
            this.db = db;
        }

        public void insert(BANK bank)
        {
            db.BANKS.Add(bank);
            
        }

        public void update(BANK bank)
        {
            db.Entry(bank).State = EntityState.Modified;
           
        }

        public void delete(BANK bank)
        {
            db.Entry(bank).State = EntityState.Deleted;
          
        }

        public List<BANK> selectAll()
        {
            return db.BANKS.ToList();
        }

        public BANK select(string bankCode)
        {
            return db.BANKS.Where(uc => uc.BankCode == bankCode).FirstOrDefault();
        }

        public List<BANK> selectLike(string bankCode)
        {
            return db.BANKS.Where(uc => uc.BankCode.Contains(bankCode)).ToList();
        }

        public BANK select(Guid BankID)
        {
            return db.BANKS.Find(BankID);
        }
    }
}
