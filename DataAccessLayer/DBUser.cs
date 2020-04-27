using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Net.NetworkInformation;
using System.IO;
using System.Web.Configuration;
using System.Configuration;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DataAccessLayer
{
    public class DbUser : DBConnection
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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


        public DbUser(string pUserName, string pPassword, string pTNSName, string pPort, string pIPAddress)
        {
            this.UserName = pUserName;
            this.Password = pPassword;
            this.TNSName = pTNSName;
            this.BranchID = pPort;
            this.IPAddress = pIPAddress;
        }

        #endregion

        #region Main Method & Function

        public bool TestConnectionByIPAddress(string strIPAdd)
        {

            Ping ping = new Ping();
            PingReply reply;
            try
            {
                reply = ping.Send(strIPAdd, 1000);
                if (reply.Status != IPStatus.Success)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TestConnnectionUpdate()
        {

            OracleCommand cmd = new OracleCommand();
            string strSQL = "SELECT VARNAME FROM SYSVAR where varname ='BUSDATE'";
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            string strOraConnect = GetConnectionString(this.UserName, this.Password, this.TNSName);
            OracleConnection OraConnect = new OracleConnection(strOraConnect);
            try
            {
                OraConnect.Open();
                cmd.Connection = OraConnect;
                int i = cmd.ExecuteNonQuery();
                cmd.Dispose();
                ClosedConnection();
                return (true);
            }
            catch
            {
                //throw ex;
                return (false);
            }
        }

        public bool TestConnnection()
        {

            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleConnection oraConn = new OracleConnection(strConnection);
            try
            {
                if (oraConn.State == ConnectionState.Closed) oraConn.Open();
                if (oraConn.State == ConnectionState.Open) oraConn.Close();
                return (true);
            }
            catch
            {
                return (false);
            }

        }

        public bool TestConnnectionCOLL()
        {

            string strConnection = GetConnectionStringCOLL(this.UserName, this.Password, this.TNSName);
            OracleConnection oraConn = new OracleConnection(strConnection);
            try
            {
                if (oraConn.State == ConnectionState.Closed) oraConn.Open();
                if (oraConn.State == ConnectionState.Open) oraConn.Close();
                return (true);
            }
            catch
            {
                return (false);
            }

        }

        public void ExecuteSQLNonQuery(string strSQL)
        {

            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            OracleConnection oraConn = new OracleConnection(strConnection);

            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                cmd.Connection.BeginTransaction();

                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
                cmd.Parameters.Clear();

            }
            catch
            {
                cmd.Transaction.Rollback();
            }
            finally
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
        }

        public DataSet ExecuteSQLReturnDataset(string strSQL, string table)
        {

            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            DataSet ds = new DataSet();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            OracleConnection oraConn = new OracleConnection(strConnection);
            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                adapter.Fill(ds, table);
                cmd.Parameters.Clear();
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
                return ds;
            }
            catch
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
                return (ds = null);

            }
        }

        public string ExecuteSQLReturnOneValue(string strSQL)
        {
            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            OracleConnection oraConn = new OracleConnection(strConnection);
            string v_result = string.Empty;

            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                OracleDataReader v_dr = cmd.ExecuteReader();
                while (v_dr.Read())
                {
                    v_result = v_dr.GetString(0);
                }
                return v_result;
            }
            catch (Exception ex)
            {
                v_result = ex.ToString();
                return v_result;
            }
            finally
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
        }

        public DataTable ExecuteSQLReturnDataTable(string strSQL)
        {
            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            OracleConnection oraConn = new OracleConnection(strConnection);
            DataTable dtb = new DataTable();
            OracleDataReader oraDR;
            
            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
               // OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                oraDR = cmd.ExecuteReader();
               // adapter.Fill(dtb);
                dtb.Load(oraDR);
                cmd.Parameters.Clear();
                return dtb;
            }
            catch (Exception ex)
            {
                log.Info("Error: ", ex);
               // LogFile(ex.Message.ToString() + ex.ToString());
                return (dtb = null);
            }
            finally
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
        }


        public OracleDataReader ExecuteSQLReturnOracleDataReader(string strSQL)
        {
            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            OracleConnection oraConn = new OracleConnection(strConnection);
            DataTable dtb = new DataTable();
            OracleDataReader oraDR;

            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                // OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                oraDR = cmd.ExecuteReader();
                // adapter.Fill(dtb);
                //dtb.Load(oraDR);
                //cmd.Parameters.Clear();
                return oraDR;
            }
            catch (Exception ex)
            {
                LogFile(ex.Message.ToString() + ex.ToString());
                return (oraDR = null);
            }
            finally
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
        }
        public DataTable ExecuteSQLReturnDataTableCOLL(string strSQL)
        {
            string strConnection = GetConnectionStringCOLL(this.UserName, this.Password, this.TNSName);
            OracleCommand cmd = new OracleCommand();
            cmd.CreateParameter();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            OracleConnection oraConn = new OracleConnection(strConnection);
            DataTable dtb = new DataTable();

            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
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
                oraConn.Close();
                oraConn.Dispose();
            }
        }

        public void ExecuteStoreProcedure(string StoreName)
        {
            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoreName;
            OracleConnection oraConn = new OracleConnection(strConnection);
            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
        }

        private OracleCommand AttachParameter_StoreProcedure(OracleCommand cmd, DBOracleParameter[] DbPara)
        {
            if (DbPara.Length > 0)
            {
                for (int i = 0; i <= DbPara.Length - 1; i++)
                {
                    if (DbPara[i] != null)
                    {
                        OracleParameter p = new OracleParameter();
                        p.ParameterName = DbPara[i].ParaName;
                        p.OracleDbType = OracleDbType.Varchar2;
                        p.Value = DbPara[i].ParaValue;
                        if (DbPara[i].isOutput == true) // Kiểm tra Input hay Output
                        {
                            p.Direction = ParameterDirection.Output;
                            p.Size = DbPara[i].Size;
                        }
                        else p.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(p);
                    }
                }
            }
            return cmd;
        }

        public void ExecuteStoreProcedure(string StoreName, DBOracleParameter[] DbPara)
        {

            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoreName;
            OracleConnection oraConn = new OracleConnection(strConnection);
            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                cmd = AttachParameter_StoreProcedure(cmd, DbPara);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
        }

        public void ExecuteStoreProcedureReturnValue(string StoreName, ref DBOracleParameter[] lstOutput, DBOracleParameter[] DbPara)
        {

            string strConnection = GetConnectionString(this.UserName, this.Password, this.TNSName, this.BranchID, this.IPAddress);
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = StoreName;
            OracleConnection oraConn = new OracleConnection(strConnection);
            try
            {
                oraConn.Open();
                cmd.Connection = oraConn;
                cmd = AttachParameter_StoreProcedure(cmd, lstOutput);
                cmd = AttachParameter_StoreProcedure(cmd, DbPara);
                cmd.ExecuteNonQuery();
                // Lấy lại danh sách lỗi
                for (int i = 0; i < cmd.Parameters.Count; i++)
                {
                    if (cmd.Parameters[i].Direction.ToString() == "Output")
                        lstOutput[i].ParaValue = cmd.Parameters[i].Value.ToString();
                }
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
            catch
            {
                cmd.Dispose();
                oraConn.Close();
                oraConn.Dispose();
            }
        }


        private void LogFile(String str)
            {
                // Create a writer and open the file:
               ///string logfile = WebConfigurationManager.AppSettings["SystemLog"].ToString() ;
                StreamWriter log;
                if (!File.Exists( "logfile.txt"))
                {
                    log = new StreamWriter( "logfile.txt");
                }
                else
                {
                    log = File.AppendText( "logfile.txt");
                }
                // Write to the file:
                log.WriteLine(DateTime.Now);
                log.WriteLine(str);
                log.WriteLine();
                // Close the stream:
                log.Close();
                //end of write             
            }

        #endregion
    }
}
