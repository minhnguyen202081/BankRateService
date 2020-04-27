using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using System.IO;

namespace DataAccessLayer
{
    public class DBConnection
    {
        private OracleConnection OraConnect;
        private string strOraConnect;
        private string strConnectionProperty = "Connection Lifetime=120;Connection Timeout=160;Max Pool Size=200;Incr Pool Size=5; Decr Pool Size=2";
        //Min Pool Size=10;


        public string GetConnectionString(string pUsername, string pPassword, string pTnsName)
        {
            string strConn = "USER ID=" + pUsername + ";PASSWORD=" + pPassword + ";DATA Source=" + pTnsName + ";" + strConnectionProperty;
            strConn = "User ID=SGVFDB03;Password=SGVFDB03;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=176.1.100.135)(PORT=1580)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=DFSP02)));";
            return strConn;
        }

        public string GetConnectionString(string pUsername, string pPassword, string pTnsName, string pPort, string pHost)
        {
            string strConn = "User ID=" + pUsername + ";Password=" + pPassword + ";Connection Lifetime=120;Connection Timeout=160;Max Pool Size=200;;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + pHost + ")(PORT=" + pPort + ")))(CONNECT_DATA=(SERVER=DEDICATED)(SID=" + pTnsName + ")));";
            return strConn;
        }

        public string GetConnectionStringCOLL(string pUsername, string pPassword, string pTnsName)
        {
            string strConn = "USER ID=" + pUsername + ";PASSWORD=" + pPassword + ";DATA Source=" + pTnsName + ";" + strConnectionProperty;
            strConn = "User ID=SGVFDB03;Password=SGVFDB03;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=176.1.100.135)(PORT=1570)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=DFSP01)));";
            return strConn;
        }
        
        public DBConnection()
        {
            try
            {
                OraConnect = new OracleConnection();
            
            }

            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected OracleConnection GetConnection(string pUsername, string pPassword, string pTnsName)
        {
            try
            {
                if (OraConnect.State != ConnectionState.Closed) OraConnect.Close();
                //Make a connection by Username & Password & TnsName
                strOraConnect = "USER ID=" + pUsername + ";PASSWORD=" + pPassword + ";DATA Source=" + pTnsName + ";" + strConnectionProperty;

                string str = "User ID=SGVFDB03;Password=SGVFDB03;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=176.1.183.115)(PORT=1591)))(CONNECT_DATA=(SERVER=DEDICATED)(SID=DFSP02)));";

                OraConnect = new OracleConnection(str);
                OraConnect.Open();
                return OraConnect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ClosedConnection()
        {
            if (OraConnect.State == ConnectionState.Open)
                OraConnect.Close();
        }


    }

}
