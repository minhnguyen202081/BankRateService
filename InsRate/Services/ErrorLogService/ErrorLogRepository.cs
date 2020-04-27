using BR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BR.ErrorLogModel
{
    public class ErrorLogRepository
    {
           private BRContext db;

           public ErrorLogRepository(BRContext db)
        {
            this.db = db;
        }

        public void insert(ERROR_LOG errorLog)
        {
            db.ERROR_LOG.Add(errorLog);
            
        }

        public void update(ERROR_LOG errorLog)
        {
            db.Entry(errorLog).State = EntityState.Modified;
           
        }

        public void delete(ERROR_LOG errorLog)
        {
            db.Entry(errorLog).State = EntityState.Deleted;
          
        }

        public List<ERROR_LOG> selectAll()
        {
            return db.ERROR_LOG.ToList();
        }

       

   
    }
}
