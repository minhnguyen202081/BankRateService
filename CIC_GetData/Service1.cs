using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.Threading;
using System.Configuration;
using BR.Models;
using BR.BankRuleLogic;
using Library;
using BR.BankLogic;
using BR.RateHistoryLogic;
using System.Data.SqlClient;

namespace BankRateService
{
    public partial class BankRateService : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private System.Timers.Timer timer;
        
        public BankRateService()
        {
           InitializeComponent();
		if (!System.Diagnostics.EventLog.SourceExists("BankRateService")) 
		{         
				System.Diagnostics.EventLog.CreateEventSource(
                    "BankRateService", "");
		}
        eventLog1.Source = "BankRateService";
        eventLog1.Log = "";
        }
      public static DateTime  LastOCBRun
        {
            get;
            set;

            
        }
        protected override void OnStart(string[] args)
        {
          
            int Interval = Convert.ToInt32(ConfigurationManager.AppSettings["Interval"].ToString());
            this.timer = new System.Timers.Timer(Interval);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            LastOCBRun = DateTime.Now;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();

         
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            BankRateService.Main(eventLog1); // my separate static method for do work
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
        }
      public static void Main(EventLog e)
        {
            try
            {
                
                
                BankRate_Thread brThread = new BankRate_Thread();
                string QueryCode = "";
                ThreadPool.QueueUserWorkItem(new WaitCallback(brThread.BankRateThread), QueryCode);
                DeleteSQL();

             //   ThreadPool.QueueUserWorkItem(new WaitCallback(brThread.BankRateThread), QueryCode);


            }
            catch (Exception ex)
            {
                e.WriteEntry(ex.Message);
            }
        }

        public static void DeleteSQL()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BRContext"].ConnectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_auto_deletelog", conn);
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;

               

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
        public static double ConvertMillisecondsToMinutes(double milliseconds)
      {
          return TimeSpan.FromMilliseconds(milliseconds).TotalMinutes;
      }
    }
}
