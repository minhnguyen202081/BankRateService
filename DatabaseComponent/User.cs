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
	public class UserDB
	{
		private string connectionString;

		public UserDB()
		{
			// Get connection string from web.config.
			connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
		}
        public UserDB(string connectionString)
		{
			this.connectionString = connectionString;
		}

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
		public int InsertAWB(AWBDB awb)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("InsertAWB", con);
			cmd.CommandType = CommandType.StoredProcedure;
    
            //cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 10));
            //cmd.Parameters["@FirstName"].Value = emp.FirstName;
            //cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 20));
            //cmd.Parameters["@LastName"].Value = emp.LastName;
            //cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", SqlDbType.NVarChar, 25));
            //cmd.Parameters["@TitleOfCourtesy"].Value = emp.TitleOfCourtesy;
            //cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
            //cmd.Parameters["@EmployeeID"].Direction = ParameterDirection.Output;
		
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();
				return (int)cmd.Parameters["@EmployeeID"].Value;
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

        [DataObjectMethod(DataObjectMethodType.Update, true)]
		public void UpdateEmployee(EmployeeDetails emp)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 10));
			cmd.Parameters["@FirstName"].Value = emp.FirstName;
			cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.NVarChar, 20));
			cmd.Parameters["@LastName"].Value = emp.LastName;
			cmd.Parameters.Add(new SqlParameter("@TitleOfCourtesy", SqlDbType.NVarChar, 25));
			cmd.Parameters["@TitleOfCourtesy"].Value = emp.TitleOfCourtesy;
			cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
			cmd.Parameters["@EmployeeID"].Value = emp.EmployeeID;

			try
			{
				con.Open();
				cmd.ExecuteNonQuery();
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

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateUser1(string userID, string updUser, string offToday)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("UpdateUser", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@p_UserID", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserID"].Value = userID;

            cmd.Parameters.Add(new SqlParameter("@p_OffToday", SqlDbType.VarChar, 3));
            cmd.Parameters["@p_OffToday"].Value = offToday;

            cmd.Parameters.Add(new SqlParameter("@p_LastUpdUser", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_LastUpdUser"].Value = updUser;

                                   
            
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

        // Variant used for testing ObjectDataSourceUpdate2.aspx.
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateUser(string ID, string user, string offToday)
        {
            // Just send the call to our standard method.
            UpdateUser1(ID, user, offToday);
        }
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
		public void DeleteEmployee(int employeeID)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("DeleteEmployee", con);
			cmd.CommandType = CommandType.StoredProcedure;
    
			cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
			cmd.Parameters["@EmployeeID"].Value = employeeID;
				
			try 
			{
				con.Open();
				cmd.ExecuteNonQuery();
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

        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void DeleteEmployee(EmployeeDetails emp)
        {
            DeleteEmployee(emp.EmployeeID);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
		public EmployeeDetails GetEmployee(int employeeID)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("GetEmployee", con);
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 4));
			cmd.Parameters["@EmployeeID"].Value = employeeID;
    				
			try 
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
				
				// Get the first row.
				reader.Read();
				EmployeeDetails emp = new EmployeeDetails(
					(int)reader["EmployeeID"], (string)reader["FirstName"],
					(string)reader["LastName"], (string)reader["TitleOfCourtesy"]);
				reader.Close();
				return emp;
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

        [DataObjectMethod(DataObjectMethodType.Select, true)]
		public List<UserDetails> GetUsers(string userName, string offToday)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("GetUserByCond", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_UserName", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserName"].Value = userName;

            cmd.Parameters.Add(new SqlParameter("@p_OffToday", SqlDbType.VarChar, 3));
            cmd.Parameters["@p_OffToday"].Value = offToday;
                            				
			// Create a collection for all the employee records.
            List<UserDetails> users = new List<UserDetails>();

			try 
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
                    UserDetails user = new UserDetails(
                        (string)reader["UserID"],
                        (string)reader["UserName"],
                        (string)reader["FullName"],
                        (string)((String.IsNullOrEmpty(reader["OffToday"].ToString())) ? "" : reader["OffToday"])
                        );
                    users.Add(user);
				}

				reader.Close();

                return users;
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
				
		public int CountEmployees()
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("CountEmployees", con);
			cmd.CommandType = CommandType.StoredProcedure;
    				
			try 
			{
				con.Open();
				return (int)cmd.ExecuteScalar();
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
