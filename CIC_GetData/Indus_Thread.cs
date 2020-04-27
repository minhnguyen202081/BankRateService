using System;
using System.Collections.Generic;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;


using BR.Models;
using BR.RateHistoryLogic;
using Library;
using BR.BankRuleLogic;
using BR.BankLogic;
using System.Threading;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace BankRateService
{

    public class BankRate_Thread
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
          private string connectionString;

        public BankRate_Thread()
        {
            // Get connection string from web.config.
           // connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
        }
        public BankRate_Thread(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public bool OCB
        {
            get;
            set;
        }


        //public void ReportThread(string result, string strRptFolder)
        public void BankRateThread(object QueryCode)
        {
           
         



                List<BANK_RULES> lbr = new List<BANK_RULES>();
                BankRuleService brs = new BankRuleService();

                lbr = brs.findAll();
                foreach (BANK_RULES BR in lbr)
                {
                try
                {
                    string result="";
                    BankService bs = new BankService();
                    BANK b = new BANK();
                    b = bs.find(BR.BankCode);
                    if (b != null)
                    {
                        string groupCode = Guid.NewGuid().ToString();
                        RATE_HISTORY rateHistory = new RATE_HISTORY();
                        RateHistoryService rhs = new RateHistoryService();
                        if(b.BankCode=="OCB")
                        {
                           
                            Thread.Sleep(180000);
                            if(BankRateService.LastOCBRun.AddMinutes(5)<DateTime.Now)
                            {
                                BankRateService.LastOCBRun = DateTime.Now;
                                string bankData = DataExtractorFactory.getDataExtractor(b).extractData(b);

                                 result = RuleProcessor.process(bankData, BR.BankRule, b.BankCode);
                                
                            }
                        }
                        else
                        {
                            string bankData = DataExtractorFactory.getDataExtractor(b).extractData(b);

                            result = RuleProcessor.process(bankData, BR.BankRule, b.BankCode);

                        }
                    
                        if (result != "")
                        {
                            rateHistory = rhs.get(b.BankCode, BR.TenorCode);
                            float finalResult;
                            if (rateHistory != null)
                            {
                                if (float.TryParse(result, out finalResult))
                                {
                                    rateHistory.InsRate = result;
                                    rateHistory.GroupCode = groupCode;
                                    rateHistory.Timestamp = DateTime.Now;
                                    rhs.editRateHistory(rateHistory);
                                }
                            }
                            else
                            {
                                if (float.TryParse(result, out finalResult))
                                {
                                    rateHistory = new RATE_HISTORY();
                                    rateHistory.BankCode = b.BankCode;
                                    rateHistory.TenorCode = BR.TenorCode;

                                    rateHistory.InsRate = result;
                                    rateHistory.GroupCode = groupCode;
                                    rateHistory.Timestamp = DateTime.Now;
                                    rhs.addRateHistory(rateHistory);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Info("Error: ", ex);
                }


            }
          

          
             
        }
        public static void InsertDataToSQL(string nationalID, string ContractNo, string PartyType, string IndusStatus)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CIC"].ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("CICCheck_ins", conn);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@NationalID", SqlDbType.NVarChar, 50).Value = nationalID;
                cmd.Parameters.Add("@ContractNo", SqlDbType.NVarChar, 50).Value = ContractNo;
                cmd.Parameters.Add("@PartyType", SqlDbType.NVarChar, 50).Value = PartyType;
                cmd.Parameters.Add("@IndusStatus", SqlDbType.NVarChar, 500).Value = IndusStatus;

                cmd.ExecuteNonQuery();
            
            }
            catch (Exception ex)
            {
                log.Info("Error: ", ex);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
        }
        public static void UpdateIndusStatus( string ContractNo)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CIC"].ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_UpdateIndusStatus", conn);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

           
                cmd.Parameters.Add("@ContractNo", SqlDbType.NVarChar, 40).Value = ContractNo;
             

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
        }

        public static string GetQueryFromDB(string QueryCode)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CIC"].ConnectionString);
            conn.Open();
            string result = "";
            SqlCommand cmd = new SqlCommand("sp_SQLQuery_Get", conn);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@QueryCode", SqlDbType.NVarChar, 50).Value = QueryCode;



                 result = (System.String)cmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            return result;
        }

        private static DataTable ListAllApprovalLevel1Contract(DateTime insertedDate)
        {
           
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CIC"].ConnectionString);

            SqlDataReader sqlDataReader;
            DataTable dtb = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_ListAllContractsAtLevel1", conn);
            try
            {


                conn.Open();


                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.Add("@insertedDate", SqlDbType.DateTime).Value = insertedDate;

                sqlDataReader = cmd.ExecuteReader();
                dtb.Load(sqlDataReader);


            }
            catch (Exception ex)
            {
                log.Error("Error: ", ex);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return dtb;
        }
   
    }
}
