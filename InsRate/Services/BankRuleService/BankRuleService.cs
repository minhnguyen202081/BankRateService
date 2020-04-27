using BR.BankLogic;
using BR.BankRuleModel;
using BR.ConstantLogic;
using BR.Models;
using BR.TenorLogic;
using Library;
using log4net;
using ModelLib.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BR.BankRuleLogic
{
   public class BankRuleService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BankRuleService));


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
        public void addBankRule(BANK_RULES bankRule)
        {
            logger.Info("addBankRule: BankCode " + bankRule.BankCode +" TenorCode "+ bankRule.TenorCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                addBankRule(bankRule, db);
                saveChanges(db);
            }
            logger.Info("addBankRule: BankCode " + bankRule.BankCode + " TenorCode " + bankRule.TenorCode + " end!!!");
        }
        public void addBankRule(BANK_RULES bankRule, BRContext db)
        {
            BankRuleRepository ur = new BankRuleRepository(db);
            if (ur.select(bankRule.BankCode,bankRule.TenorCode) == null)
            {
                ur.insert(bankRule);
            }
            else
            {
                logger.Warn("addBankRule: " + MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ":  BankCode " + bankRule.BankCode + " TenorCode " +bankRule.TenorCode) ;
                throw new ArgumentException(MessageConstantLogic.ERROR_RECORD_ALREADY_EXISTED + ":  BankCode " + bankRule.BankCode + " TenorCode " + bankRule.TenorCode);
            }
        }

        public void editBankRule(BANK_RULES bankRule, BRContext db)
        {
            BankRuleRepository ur = new BankRuleRepository(db);
            ur.update(bankRule);
        }

        public void editBankRule(BANK_RULES bankRule)
        {
            logger.Info("editBank:bankCode " + bankRule.BankCode +" tenorCode "+ bankRule.TenorCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                editBankRule(bankRule, db);
                saveChanges(db);
            }
            logger.Info("editBank:bankCode " + bankRule.BankCode + " tenorCode " + bankRule.TenorCode + " end!!!");
        }
        public void deleteBankRule(BANK_RULES bankRule, BRContext db)
        {
            BankRuleRepository ur = new BankRuleRepository(db);
            ur.delete(bankRule);
        }

        public void deleteBankRule(BANK_RULES bankRule)
        {
            logger.Info("deleteBankRule:  bankCode " + bankRule.BankCode + " tenorCode: "+ bankRule.TenorCode + " start!!!");
            using (BRContext db = new BRContext())
            {
                deleteBankRule(bankRule, db);
                saveChanges(db);
            }
            logger.Info("deleteBankRule:  bankCode " + bankRule.BankCode + " tenorCode: " + bankRule.TenorCode + " end!!!");
        }

        public List<BANK_RULES> findAll(BRContext db)
        {
            BankRuleRepository ur = new BankRuleRepository(db);
            return ur.selectAll();
        }

        public List<BANK_RULES> findAll()
        {
            logger.Info("findAll");
            using (BRContext db = new BRContext())
            {
                return findAll(db);
            }
        }


        public BANK_RULES find(string bankCode, string tenorCode, BRContext db)
        {
            BANK_RULES bankRule = new BANK_RULES();
            BankRuleRepository ur = new BankRuleRepository(db);
            bankRule = ur.select(bankCode,tenorCode);

            return bankRule;
        }

        public BANK_RULES find(string bankCode, string tenorCode)
        {
            logger.Info("find: bankCode " + bankCode + " tenorCode " + tenorCode );
            BANK_RULES bankRule = new BANK_RULES();
           
                using (BRContext db = new BRContext())
                {
                    bankRule = find(bankCode,tenorCode, db);
                }
           
            return bankRule;
        }

        public static BankRuleView convertToModelView(BANK_RULES bankrule)
        {
            BankService bs = new BankService();
            TenorService ts = new TenorService();
            BankRuleView bankRuleView = new BankRuleView();
            bankRuleView.BankName = bs.find( bankrule.BankCode).BankName;
            bankRuleView.TenorDesc = ts.find( bankrule.TenorCode).TenorDesc;
            bankRuleView.TenorIndex = ts.find(bankrule.TenorCode).TenorIndex;
            bankRuleView.BankRule = bankrule.BankRule;
            bankRuleView.BankCode = bankrule.BankCode;
            bankRuleView.TenorCode = bankrule.TenorCode;
           
            return bankRuleView;
        }

        public  List<BankRuleView> convertToModelView(List<BANK_RULES> bankrules)
        {
            List<BankRuleView> bankRuleViews = new List<BankRuleView>();
            foreach (BANK_RULES bankRule in bankrules)
            {
                bankRuleViews.Add(convertToModelView(bankRule));
            }
            return bankRuleViews;
        }

        public BankRuleView findBankRuleView(string bankCode, string tenorCode)
        {
            logger.Info("find: bankCode " + bankCode + " tenorCode " + tenorCode);
            BANK_RULES bankRule = new BANK_RULES();
            BankRuleView bankRuleView = new BankRuleView();
            using (BRContext db = new BRContext())
            {
                bankRuleView = convertToModelView( find(bankCode, tenorCode, db));
            }

            return bankRuleView;
        }

        public void editBankRuleView(BankRuleView bankruleView)
        {
            BANK_RULES bank_rule = convertToModel(bankruleView);
            editBankRule(bank_rule);
        }

        public static BANK_RULES convertToModel(BankRuleView bankruleView)
        {
            BANK_RULES bank_rule = new BANK_RULES();
            bank_rule.BankCode = bankruleView.BankCode;
            bank_rule.TenorCode = bankruleView.TenorCode;
            bank_rule.BankRule = bankruleView.BankRule;


            return bank_rule;
        }

        public void addBankRuleView(BankRuleView bankRuleView)
        {
            BANK_RULES bank_rule = convertToModel(bankRuleView);
            addBankRule(bank_rule);
        }

        public void deleteBankRuleView(BankRuleView bankRuleView)
        {
            BANK_RULES bank_rule = convertToModel(bankRuleView);
            deleteBankRule(bank_rule);
        }
    }
}
