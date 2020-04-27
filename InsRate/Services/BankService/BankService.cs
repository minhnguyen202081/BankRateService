using BR.BankModel;
using BR.ConstantLogic;
using BR.Models;
using log4net;
using ModelLib.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BankLogic
{
    public class BankService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BankService));


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
        public void addBank(BANK bank)
        {
            logger.Info("addBan: " + bank.BankCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                addBank(bank, db);
                saveChanges(db);
            }
            logger.Info("addBank: " + bank.BankCode + " end!!!");
        }
        public void addBank(BANK bank, BRContext db)
        {
            BankRepository ur = new BankRepository(db);
            if (ur.select(bank.BankCode) == null)
            {
                ur.insert(bank);
            }
            else
            {
                logger.Warn("addBank: " + MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ": " + bank.BankCode);
                throw new ArgumentException(MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ": " + bank.BankCode);
            }
        }

        public void editBank(BANK bank, BRContext db)
        {
            BankRepository ur = new BankRepository(db);
            ur.update(bank);
        }

        public void editBank(BANK bank)
        {
            logger.Info("editBank: " + bank.BankCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                editBank(bank, db);
                saveChanges(db);
            }
            logger.Info("editBank: " + bank.BankCode + " end!!!");
        }
        public void deleteBank(BANK bank, BRContext db)
        {
            BankRepository ur = new BankRepository(db);
            ur.delete(bank);
        }

        public void deleteBank(BANK bank)
        {
            logger.Info("deleteBank: " + bank.BankCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                deleteBank(bank, db);
                saveChanges(db);
            }
            logger.Info("deleteBank: " + bank.BankCode + " end!!!");
        }

        public List<BANK> findAll(BRContext db)
        {
            BankRepository ur = new BankRepository(db);
            return ur.selectAll();
        }

        public List<BANK> findAll()
        {
            logger.Info("findAll");
            using (BRContext db = new BRContext())
            {
                return findAll(db);
            }
        }


        public BANK find(string bankCode, BRContext db)
        {
            BANK bank = new BANK();
            BankRepository ur = new BankRepository(db);
            bank = ur.select(bankCode);

            return bank;
        }

        public BANK find(string bankCode)
        {
            logger.Info("find: " + bankCode);
            BANK bank = new BANK();
        
                using (BRContext db = new BRContext())
                {
                    bank = find(bankCode, db);
                }

            return bank;
        }
        public List<BankView> findLikeView(string bankCode)
        {
            List<BANK> banks = findLike(bankCode);
            if (banks == null)
            {
                return new List<BankView>();
            }
            else
            {
                return convertToModelView(banks);
            }
        }
        public List<BANK> findLike(string bankCode)
        {
            logger.Info("findLike: " + bankCode);
            using (BRContext db = new BRContext())
            {
                return findLike(bankCode, db);
            }
        }
        public List<BANK> findLike(string bankCode, BRContext db)
        {
            BankRepository br = new BankRepository(db);
            return br.selectLike(bankCode);
        }
        public  List<BankView> convertToModelView(List<BANK> banks)
        {
            List<BankView> bankViews = new List<BankView>();
            foreach (BANK bank in banks)
            {
                bankViews.Add(convertToModelView(bank));
            }
            return bankViews;
        }
        public static BankView convertToModelView(BANK bank)
        {
            BankView bankView = new BankView();
            bankView.BankId = bank.BankID;
            bankView.BankCode = bank.BankCode;
            bankView.BankName = bank.BankName;
            bankView.URL = bank.BankLink;
            bankView.DataExtractor = bank.DataExtractor;

            return bankView;
        }
        public BankView findView(Guid BankID)
        {
            BANK bank = find(BankID);
            if (bank == null)
            {
                return null;
            }
            else
            {
                return convertToModelView(bank);
            }
        }

        public BANK find(Guid bankID)
        {
            logger.Info("find: " + bankID);
            BANK bank = new BANK();
            using (BRContext db = new BRContext())
                {
                    bank = find(bankID, db);
                }

            return bank;
        }
        public BANK find(Guid bankID, BRContext db)
        {
            BANK bank = new BANK();

            BankRepository br = new BankRepository(db);
            bank = br.select(bankID);

            return bank;
        }

        public void addBankView(BankView bankView)
        {
            BANK bank = convertToModel(bankView);
            addBank(bank);
        }

        public static BANK convertToModel(BankView bankView)
        {
            BANK bank = new BANK();
            bank.BankID = bankView.BankId;
            bank.BankCode = bankView.BankCode;
            bank.BankName = bankView.BankName;
            bank.BankLink = bankView.URL;
            bank.DataExtractor = bankView.DataExtractor;
            return bank;
        }

        public void editBankView(BankView bankView)
        {
            BANK bank = convertToModel(bankView);
            editBank(bank);
        }

        public void deleteBankView(BankView bankView)
        {
            BANK bank = convertToModel(bankView);
            deleteBank(bank);
        }
    }
}
