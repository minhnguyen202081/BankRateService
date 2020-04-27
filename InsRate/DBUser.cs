using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net.NetworkInformation;
using System.Data.SqlClient;

namespace EnvironmentConnectionCheck
{
    public class DbUser : DBConnection
    {
        #region Properties

        private string _UserName;
        private string _Password;
        private string _TNSName;
        private string _IPAddress;
        private string _BranchID;
        private bool _IsConnecting = false;

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        public string TNSName
        {
            get
            {
                return _TNSName;
            }
            set
            {
                _TNSName = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return _IPAddress;
            }
            set
            {
                _IPAddress = value;
            }
        }

        public string BranchID
        {
            get
            {
                return _BranchID;
            }
            set
            {
                _BranchID = value;
            }
        }

        public bool IsConnecting
        {
            get
            {
                return _IsConnecting;

            }
            set
            {
                _IsConnecting = value;
            }
        }

        #endregion

        #region Constructor

        public DbUser()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.TNSName = string.Empty;
            this.BranchID = string.Empty;
            this.IPAddress = string.Empty;
        }

        public DbUser(string pUserName, string pPassword, string pTNSName)
        {
            this.UserName = pUserName;
            this.Password = pPassword;
            this.TNSName = pTNSName;
            this.BranchID = string.Empty;
            this.IPAddress = string.Empty;
        }

        public DbUser(string pUserName, string pPassword, string pTNSName, string pBrid)
        {
            this.UserName = pUserName;
            this.Password = pPassword;
            this.TNSName = pTNSName;
            this.BranchID = pBrid;
            this.IPAddress = string.Empty;
        }


        public DbUser(string pUserName, string pPassword, string pTNSName, string pBrid, string pIPAddress)
        {
            this.UserName = pUserName;
            this.Password = pPassword;
            this.TNSName = pTNSName;
            this.BranchID = pBrid;
            this.IPAddress = pIPAddress;
        }

        #endregion

        #region Main Method & Function



        public DataTable ExecuteSQLReturnDataTable(string strSQL)
        {
            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName);
            //OracleCommand cmd = new OracleCommand();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            SqlConnection sqlconn = new SqlConnection(strConnection);
            //OracleConnection oraConn = new OracleConnection(strConnection);
            DataTable dtb = new DataTable();

            try
            {
                sqlconn.Open();
                cmd.Connection = sqlconn;
                //OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtb);
                cmd.Parameters.Clear();
                return dtb;
            }
            catch
            {
                return (dtb = null);
            }
            finally
            {
                cmd.Dispose();
                sqlconn.Close();
                sqlconn.Dispose();
            }
        }
        #endregion
    }
}
