using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using DataAccessLayer;
using ExportToExcel;
using FTPLib;
using Library;
using System.IO;
using DataConnection;
using ExportToExcel;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace DatabaseComponent
{
    [DataObject]
    public class REPORTTHREADDB
	{
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private string connectionString;

		public REPORTTHREADDB()
		{
			// Get connection string from web.config.
            connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
		}
        public REPORTTHREADDB(string connectionString)
		{
			this.connectionString = connectionString;
		}


        private static List<DataTable> SplitTableIntoMultiTables(DataTable dt, int rows)
        {

            int i, currentRow = 0;
            List<DataTable> dts = new List<DataTable>();
            while (currentRow + rows < dt.Rows.Count)
            {
                DataTable otherTable = new DataTable();
                otherTable = dt.Clone();
                if (currentRow + rows <= dt.Rows.Count)
                {
                    for (i = currentRow; i < currentRow + rows; i++)
                    {
                        otherTable.ImportRow(dt.Rows[i]); //Imports (copies) the row from the original table to the new one
                        dt.Rows[i].Delete(); //Marks row for deletion
                    }
                    currentRow = i + 1;
                    dt.AcceptChanges();
                    dts.Add(otherTable);
                }
            }
            DataTable otherTable1 = dt.Clone();
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                otherTable1.ImportRow(dt.Rows[i]); //Imports (copies) the row from the original table to the new one
                dt.Rows[i].Delete(); //Marks row for deletion
            }

            dt.AcceptChanges();
            dts.Add(otherTable1);
            return dts;


        }

        //public void ReportThread(string result, string strRptFolder)
        public void ReportThread(Object objCode)
        {
            try
            {
           

                string strCode = (string)objCode;

                string[] code = strCode.Split(';');

                string strRepCode = code[0];

                string strUserName = code[1];

                string strFromDate = code[2];

                string strToDate = code[3];



                string strRptFolder = ConfigurationManager.AppSettings["ReportFolder"].ToString();

                //Insert Report log

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BO"].ConnectionString);
                conn.Open();

                SqlCommand cmd = new SqlCommand("InsertReport_Log", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@p_ReportCode", SqlDbType.VarChar, 50).Value = strRepCode;
                cmd.Parameters.Add("@p_UserName", SqlDbType.VarChar, 50).Value = strUserName;
                cmd.Parameters.Add("@p_FileLocation", SqlDbType.VarChar, 50).Value = strRptFolder;
                if (strFromDate != "" && strToDate != "")
                {
                    cmd.Parameters.Add("@p_Duration", SqlDbType.VarChar, 50).Value = strFromDate + " to " + strToDate;
                }
                else
                {
                    if (strFromDate != "")
                    {
                        cmd.Parameters.Add("@p_Duration", SqlDbType.VarChar, 50).Value = strFromDate;
                    }
                    else
                    {
                        cmd.Parameters.Add("@p_Duration", SqlDbType.VarChar, 50).Value = "";
                    }
                }

                  
                string result = (System.String)cmd.ExecuteScalar();

                string[] parts = result.Split(';');
                string strQuery = parts[0];

                string strOriFName = parts[1];

                string strGenFileName = strRptFolder + parts[1] + ".csv";

                string strReportLogID = parts[2];


                for (int i = 2; i <= code.Length - 1; i++)
                {
                    strQuery = strQuery.Replace("''", "'").Replace("@Param" + (i - 1).ToString(), code[i]);
                }

                //strQuery = strQuery.Replace("''", "'").Replace("@Param1",strFromDate).Replace("@Param2",strToDate).Replace("@dq","''");
                DatabaseInfo rp = new DatabaseInfo();
                DbUser objDB = rp.dbUser;
                DataTable dt = new DataTable();
                dt = objDB.ExecuteSQLReturnDataTable(strQuery);
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        string dilimiter = ConfigurationManager.AppSettings["Delimiter"].ToString();
                        CreateExcelFile.CreateCSVFile(dt, strGenFileName, dilimiter);
                        //cmd = new SqlCommand(strProcedure, conn);

                        //cmd.Parameters.Add("@p_DocTypeID", SqlDbType.VarChar, 20).Value = strRepCode;

                        //cmd.Parameters.Add("@p_FromDate", SqlDbType.VarChar, 50).Value = strFromDate;
                        //cmd.Parameters.Add("@p_ToDate", SqlDbType.VarChar, 50).Value = strToDate;


                        //cmd.CommandType = CommandType.StoredProcedure;
                        //SqlDataAdapter adp = new SqlDataAdapter(cmd);

                        //DataTable dt = new DataTable();
                        //adp.Fill(dt);

                        //DataSet ds = new DataSet("table");
                        //ds.Tables.Add(dt);
                        //Minh removes for testing
                        // CreateExcelFile.CreateExcelDocument(dt, strGenFileName);

                        //update status of file in Database
                        //Minh remove for testing
                        FileInfo f = new FileInfo(strGenFileName);
                        if (f.Length > 0)
                        {

                            cmd = new SqlCommand("UpdateREPORT_LOG", conn);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@p_ReportLogID", SqlDbType.VarChar, 20));
                            cmd.Parameters["@p_ReportLogID"].Value = strReportLogID;

                            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 20));
                            cmd.Parameters["@p_Status"].Value = "Completed";

                            cmd.Parameters.Add(new SqlParameter("@p_FileName", SqlDbType.VarChar, 100));
                            cmd.Parameters["@p_FileName"].Value = strOriFName;

                            cmd.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        cmd = new SqlCommand("UpdateREPORT_LOG", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@p_ReportLogID", SqlDbType.VarChar, 20));
                        cmd.Parameters["@p_ReportLogID"].Value = strReportLogID;

                        cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 100));
                        cmd.Parameters["@p_Status"].Value = "Completed with no rows";

                        cmd.Parameters.Add(new SqlParameter("@p_FileName", SqlDbType.VarChar, 100));
                        cmd.Parameters["@p_FileName"].Value = strOriFName;

                        cmd.ExecuteNonQuery();
                    }

                }
                else
                {
                    cmd = new SqlCommand("UpdateREPORT_LOG", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@p_ReportLogID", SqlDbType.VarChar, 20));
                    cmd.Parameters["@p_ReportLogID"].Value = strReportLogID;

                    cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 100));
                    cmd.Parameters["@p_Status"].Value = "Failed";

                    cmd.Parameters.Add(new SqlParameter("@p_FileName", SqlDbType.VarChar, 100));
                    cmd.Parameters["@p_FileName"].Value = strOriFName;

                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                log.Info("Error: ", ex);
            }

            }

      
	}
}
