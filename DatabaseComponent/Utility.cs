using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace DatabaseComponent
{
    public static class Utility
    {
        public static string CheckUserPrivilege(string userName, string ScreenID)
        {
           string connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_CheckUserPrivilege", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 200));
            cmd.Parameters["@UserName"].Value = userName;

            cmd.Parameters.Add(new SqlParameter("@ScreenID", SqlDbType.NVarChar, 200));
            cmd.Parameters["@ScreenID"].Value = ScreenID;
            try
            {
                con.Open();
				SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
				
				// Get the first row.
				reader.Read();
                return reader["Result"].ToString();
				
            }
            catch (SqlException err)
            {
                // Replace the error with something less specific.
                // You could also log the error now.
                throw new ApplicationException("Data error.");
            }
            finally
            {
                con.Close();
            }
        }
    }

}
