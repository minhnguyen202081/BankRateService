using BR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BR.TenorModel
{
    public class TenorRepository
    {
          private BRContext db;

          public TenorRepository(BRContext db)
        {
            this.db = db;
        }

        public void insert(TENOR tenor)
        {
            db.TENORS.Add(tenor);
            
        }

        public void update(TENOR tenor)
        {
            db.Entry(tenor).State = EntityState.Modified;
           
        }

        public void delete(TENOR tenor)
        {
            db.Entry(tenor).State = EntityState.Deleted;
          
        }

        public List<TENOR> selectAll()
        {
            return db.TENORS.ToList();
        }

        public TENOR select(string tenorCode)
        {
            return db.TENORS.Where(uc => uc.TenorCode == tenorCode).FirstOrDefault();
        }

        public List<TENOR> selectLike(string tenorCode)
        {
            return db.TENORS.Where(uc => uc.TenorCode.Contains(tenorCode)).ToList();
        }

        public TENOR select(Guid TenorID)
        {
            return db.TENORS.Find(TenorID);
        }
    }
}
