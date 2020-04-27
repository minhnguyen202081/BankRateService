using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Generic;
using System.ComponentModel;

namespace DatabaseComponent
{
    [DataObject]
	public class UserScreenDB
	{
		private string connectionString;

		public UserScreenDB()
		{
			// Get connection string from web.config.
			connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
		}
        public UserScreenDB(string connectionString)
		{
			this.connectionString = connectionString;
		}

        

   
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteUserRole(string userID, string roleID)
		{
			SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DeleteUserRole", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_UserID", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserID"].Value = userID;

            cmd.Parameters.Add(new SqlParameter("@p_RoleID", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_RoleID"].Value = roleID;
            				
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();
			}
			catch (SqlException err) 
			{
				// Replace the error with something less specific.
				// You could also log the error now.
                throw new ApplicationException(err.Message.ToString());
			}
			finally 
			{
				con.Close();			
			}
		}

      

		public DataTable GetUserScreenByRole(string roleID)
		{
			SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetScreenByRole", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.VarChar, 50));
            cmd.Parameters["@RoleID"].Value = roleID;

        try
			{
				con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dt = new DataTable();
                dt.Load(dr);
                return dt;
			}

          
			catch (SqlException err) 
			{
				// Replace the error with something less specific.
				// You could also log the error now.
                throw new ApplicationException(err.Message.ToString());
			}
			finally 
			{
				con.Close();			
			}
		}
				
		
	}
}
