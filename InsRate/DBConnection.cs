using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvironmentConnectionCheck
{
    public class DBConnection
    {

        private string strOraConnect;
        private string strConnectionProperty = "Connection Lifetime=120;Connection Timeout=60;Incr Pool Size=5; Decr Pool Size=2";
        //Min Pool Size=10;


        public string GetConnectionString(string pUsername, string pPassword, string pTnsName)
        {
            string strConn = "USER ID=" + pUsername + ";PASSWORD=" + pPassword + ";DATA Source=" + pTnsName + ";" + strConnectionProperty;
            //strConn = "User ID=SGVFDB02;Password=SGVFDB02;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=176.1.100.135)(PORT=1580)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=DFSP02)));";
            strConn = "Data Source=192.168.50.8; Initial Catalog=TEST;" + "Persist Security Info=True;User ID=sa;Password=HoangDuocSu123123";
            return strConn;
        }
    }
}
