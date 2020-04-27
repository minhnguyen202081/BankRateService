using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DataAccessLayer;
using DocumentFormat.OpenXml;
using System.Security.Cryptography;
using System.IO;
using Library;
using DataConnection;
namespace DataConnection
{
    public class ReportInfo
    {
        public string ReportName;
        public string[] ParametersList;
        public string QueryPath;
        public string ExcelPath;
        public string FtpPath;
        public string System;
        public string Password;
        public bool IsZipped;
        public string Type;
        public string Output;
        public string RowNum;
        //public int Option;
        //public string FromDate;
        //public string ToDate;

        #region Constructors
        public ReportInfo()
        {
            ReportName = string.Empty;
            ParametersList = null;
            QueryPath = string.Empty;
            ExcelPath = string.Empty;
            FtpPath = string.Empty;
            System = string.Empty;
            Password = string.Empty;
            IsZipped = true;
            Type = string.Empty;
            Output = string.Empty;
            RowNum = string.Empty;
            //Option = 0;
            //FromDate = string.Empty;
            //ToDate = string.Empty;
        }

        public ReportInfo(string sReportName, string[] aParametersList, string sQueryPath, string sExcelPath
                        , string sFtpPath, string sSystem, string sPassword, bool bIsZipped, string stype, string soutput, string rowNum
            //, int iOption, string sFromDate, string sToDate
                        )
        {
            ReportName = sReportName;
            ParametersList = aParametersList;
            QueryPath = sQueryPath;
            ExcelPath = sExcelPath;
            FtpPath = sFtpPath;
            System = sSystem;
            Password = sPassword;
            IsZipped = bIsZipped;
            Type = stype;
            Output = soutput;
            RowNum = rowNum;
            //Option = iOption;
            //FromDate = sFromDate;
            //ToDate = sToDate;
        }

        public ReportInfo(int iSequenceNumber)
        {
            ReportName = ConfigurationManager.AppSettings["ReportName" + iSequenceNumber.ToString()].ToString();
            string paramList = ConfigurationManager.AppSettings["OtherParam" + iSequenceNumber.ToString()].ToString();
            ParametersList = paramList.Split(new char[] { ';' });
            QueryPath = ConfigurationManager.AppSettings["QueryPath" + iSequenceNumber.ToString()].ToString();
            ExcelPath = ConfigurationManager.AppSettings["ExcelPath" + iSequenceNumber.ToString()].ToString();
            FtpPath = ConfigurationManager.AppSettings["FtpPath" + iSequenceNumber.ToString()].ToString();
            System = ConfigurationManager.AppSettings["System" + iSequenceNumber.ToString()].ToString();
            Password = ConfigurationManager.AppSettings["Password" + iSequenceNumber.ToString()].ToString();
            Output = ConfigurationManager.AppSettings["Output" + iSequenceNumber.ToString()].ToString();
            if (!Password.Equals(string.Empty))
                Password = Library.LibraryFuncs.DecryptString(Password, "encryptpasswordSGVF");
            Type = ConfigurationManager.AppSettings["Type" + iSequenceNumber.ToString()].ToString();
            RowNum = ConfigurationManager.AppSettings["RowNum" + iSequenceNumber.ToString()].ToString();
            string sZipped = ConfigurationManager.AppSettings["IsZipped" + iSequenceNumber.ToString()].ToString();
            if (sZipped.Equals("Y") || sZipped.Equals("y")) IsZipped = true;
            else IsZipped = false;
            /*
             Option = Convert.ToInt32(ConfigurationManager.AppSettings["Option" + iSequenceNumber.ToString()].ToString());
            FromDate = ConfigurationManager.AppSettings["FromDate" + iSequenceNumber.ToString()].ToString();
            ToDate = ConfigurationManager.AppSettings["ToDate" + iSequenceNumber.ToString()].ToString();
            if (Option.Equals(0))
            {
                FromDate = DateTime.Now.AddDays(Convert.ToInt32(FromDate)).ToString("dd-MM-yyyy");
                ToDate = DateTime.Now.AddDays(Convert.ToInt32(ToDate)).ToString("dd-MM-yyyy");
            }
             */
        }
        #endregion
    }

    public class DatabaseInfo
    {
        public string Alias;
        private string UserName;
        private string Password;
        private string TNSName;
        private string Port;
        private string IPAddress;
        public DbUser dbUser;

        #region Constructors


        public DatabaseInfo(string sAlias, string sUserName, string sPassword, string sTNSName, string sPort, string sIPAddress)
        {
            Alias = sAlias;
            UserName = sUserName;
            Password = sPassword;
            TNSName = sTNSName;
            Port = sPort;
            IPAddress = sIPAddress;
            dbUser = new DbUser(UserName, Password, TNSName, Port, IPAddress);
        }

        public DatabaseInfo() // Collection | Reporting | LOSLMS
        {
            Alias = ConfigurationManager.AppSettings["DBAlias"].ToString();
            UserName = ConfigurationManager.AppSettings["DBUserName"].ToString();
            Password = ConfigurationManager.AppSettings["DBPassword"].ToString();
            if (!Password.Equals(string.Empty))
                Password = Library.LibraryFuncs.DecryptString(Password, "encryptpasswordSGVF");

            TNSName = ConfigurationManager.AppSettings["DBTNSName"].ToString();
            Port = ConfigurationManager.AppSettings["DBPort"].ToString();
            IPAddress = ConfigurationManager.AppSettings["DBIPAddress"].ToString();

            dbUser = new DbUser(UserName, Password, TNSName, Port, IPAddress);
        }
        #endregion
    }

}