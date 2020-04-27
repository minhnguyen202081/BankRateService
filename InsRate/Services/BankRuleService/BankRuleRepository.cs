using BR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace  BR.BankRuleModel
{
    public class BankRuleRepository
    {
             private BRContext db;

        public BankRuleRepository(BRContext db)
        {
            this.db = db;
        }

        public void insert(BANK_RULES bankRule)
        {
            db.BANK_RULES.Add(bankRule);
            
        }

        public void update(BANK_RULES bankRule)
        {
            db.Entry(bankRule).State = EntityState.Modified;
           
        }

        public void delete(BANK_RULES bankRule)
        {
            db.Entry(bankRule).State = EntityState.Deleted;
          
        }

        public List<BANK_RULES> selectAll()
        {
            return db.BANK_RULES.ToList();
        }

        public BANK_RULES select(string bankCode, string tenorCode)
        {
            return db.BANK_RULES.SingleOrDefault(p => p.BankCode == bankCode && p.TenorCode == tenorCode);
        }

        public List<BANK_RULES> selectLike(string bankRule)
        {
            return db.BANK_RULES.Where(uc => uc.BankCode.Contains(bankRule)).ToList();
        }
    }
}
