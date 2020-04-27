using BR.ConstantLogic;
using BR.Models;
using BR.TenorModel;
using log4net;
using ModelLib.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.TenorLogic
{
    public class TenorService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TenorService));


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
        public void addTenor(TENOR tenor)
        {
            logger.Info("addTenor: " + tenor.TenorCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                addTenor(tenor, db);
                saveChanges(db);
            }
            logger.Info("addTenor: " + tenor.TenorCode + " end!!!");
        }
        public void addTenor(TENOR tenor, BRContext db)
        {
            TenorRepository ur = new TenorRepository(db);
            if (ur.select(tenor.TenorCode) == null)
            {
                ur.insert(tenor);
            }
            else
            {
                logger.Warn("addTenor: " + MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ": " + tenor.TenorCode);
                throw new ArgumentException(MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ": " + tenor.TenorCode);
            }
        }

        public void editTenor(TENOR tenor, BRContext db)
        {
            TenorRepository ur = new TenorRepository(db);
            ur.update(tenor);
        }

        public void editTenor(TENOR tenor)
        {
            logger.Info("editTenor: " + tenor.TenorCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                editTenor(tenor, db);
                saveChanges(db);
            }
            logger.Info("editTenor: " + tenor.TenorCode + " end!!!");
        }
        public void deleteTenor(TENOR tenor, BRContext db)
        {
            TenorRepository ur = new TenorRepository(db);
            ur.delete(tenor);
        }

        public void deleteTenor(TENOR tenor)
        {
            logger.Info("deleteTenor: " + tenor.TenorCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                deleteTenor(tenor, db);
                saveChanges(db);
            }
            logger.Info("deleteTenor: " + tenor.TenorCode + " end!!!");
        }

        public List<TENOR> findAll(BRContext db)
        {
            TenorRepository ur = new TenorRepository(db);
            return ur.selectAll();
        }

        public List<TENOR> findAll()
        {
            logger.Info("findAll");
            using (BRContext db = new BRContext())
            {
                return findAll(db);
            }
        }


        public TENOR find(string tenorCode, BRContext db)
        {
            TENOR tenor = new TENOR();
            TenorRepository ur = new TenorRepository(db);
            tenor = ur.select(tenorCode);

            return tenor;
        }

        public TENOR find(string tenorCode)
        {
            logger.Info("find: " + tenorCode);
            TENOR tenor = new TENOR();
            
                using (BRContext db = new BRContext())
                {
                    tenor = find(tenorCode, db);
                }
            
            return tenor;
        }

        public List<TenorView> findLikeView(string tenorCode)
        {
            List<TENOR> tenors = findLike(tenorCode);
            if (tenors == null)
            {
                return new List<TenorView>();
            }
            else
            {
                return convertToModelView(tenors);
            }
        }
        public List<TENOR> findLike(string tenorCode)
        {
            logger.Info("findLike: " + tenorCode);
            using (BRContext db = new BRContext())
            {
                return findLike(tenorCode, db);
            }
        }
        public List<TENOR> findLike(string tenorCode, BRContext db)
        {
            TenorRepository br = new TenorRepository(db);
            return br.selectLike(tenorCode);
        }

        public  List<TenorView> convertToModelView(List<TENOR> tenors)
        {
            List<TenorView> tenorViews = new List<TenorView>();
            foreach (TENOR tenor in tenors)
            {
                tenorViews.Add(convertToModelView(tenor));
            }
            return tenorViews;
        }
        public static TenorView convertToModelView(TENOR tenor)
        {
            TenorView tenorView = new TenorView();
            tenorView.TenorId = tenor.TenorID;
            tenorView.TenorCode = tenor.TenorCode;
            tenorView.TenorDesc = tenor.TenorDesc;
            tenorView.TenorIndex = tenor.TenorIndex;

            return tenorView;
        }

        public TenorView findView(Guid TenorID)
        {
            TENOR tenor = find(TenorID);
            if (tenor == null)
            {
                return null;
            }
            else
            {
                return convertToModelView(tenor);
            }
        }
        public TENOR find(Guid TenorID)
        {
            logger.Info("find: " + TenorID);
            TENOR tenor = new TENOR();
            using (BRContext db = new BRContext())
            {
                tenor = find(TenorID, db);
            }

            return tenor;
        }
        public TENOR find(Guid tenorID, BRContext db)
        {
            TENOR tenor = new TENOR();

            TenorRepository br = new TenorRepository(db);
            tenor = br.select(tenorID);

            return tenor;
        }

        public void addTenorView(TenorView tenorView)
        {
            TENOR tenor = convertToModel(tenorView);
            addTenor(tenor);
        }

        public static TENOR convertToModel(TenorView tenorView)
        {
            TENOR tenor = new TENOR();
            tenor.TenorID = tenorView.TenorId;
            tenor.TenorCode = tenorView.TenorCode;
            tenor.TenorDesc = tenorView.TenorDesc;
            tenor.TenorIndex = tenorView.TenorIndex;

            return tenor;
        }
        public void editTenorView(TenorView tenorView)
        {
            TENOR tenor = convertToModel(tenorView);
            editTenor(tenor);
        }

        public void deleteTenorView(TenorView tenorView)
        {
            TENOR tenor = convertToModel(tenorView);
            deleteTenor(tenor);
        }
  
    }
}
