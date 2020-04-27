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
	public class MRCDB
	{
		private string connectionString;

		public MRCDB()
		{
			// Get connection string from web.config.
			connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
		}
        public MRCDB(string connectionString)
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
        public void UpdateMRC1(string mrcID, string userName, string status, string mrcquantity)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("UpdateMRC", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_MRCID", SqlDbType.VarChar, 20));
            cmd.Parameters["@p_MRCID"].Value = mrcID;

            cmd.Parameters.Add(new SqlParameter("@p_UserName", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserName"].Value = userName;            
            
            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_Status"].Value = status;

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
        public void UpdateMRC(string ID, string user, string status, string mrcquantity)
        {
            // Just send the call to our standard method.
            UpdateMRC1(ID, user, status, "");
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
        public List<MRCDetails> GetMRCs(string sipCode, string status)
		{
			SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetMRCByCond", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_SipCode", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_SipCode"].Value = sipCode;
            
            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_Status"].Value = status;
            
            cmd.Parameters.Add(new SqlParameter("@p_User", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_User"].Value = System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1].ToString();
                				
			// Create a collection for all the employee records.
            List<MRCDetails> MRCs = new List<MRCDetails>();

			try 
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
                    MRCDetails MRC = new MRCDetails(

                        (string)((String.IsNullOrEmpty(reader["MrcID"].ToString())) ? "" : reader["MrcID"]),
                        (string)((String.IsNullOrEmpty(reader["EnvelopeDeID"].ToString())) ? "" : reader["EnvelopeDeID"]),
                        (string)((String.IsNullOrEmpty(reader["ContractNo"].ToString())) ? "" : reader["ContractNo"]),
                        (string)((String.IsNullOrEmpty(reader["ReceptionDate"].ToString())) ? "" : reader["ReceptionDate"]),
                        (string)((String.IsNullOrEmpty(reader["PreviousUser"].ToString())) ? "" : reader["PreviousUser"]),
                        (string)((String.IsNullOrEmpty(reader["SipCode"].ToString())) ? "" : reader["SipCode"]),
                        (string)((String.IsNullOrEmpty(reader["AWB"].ToString())) ? "" : reader["AWB"]),
                        (string)((String.IsNullOrEmpty(reader["Status"].ToString())) ? "" : reader["Status"]),                        
                        (string)((String.IsNullOrEmpty(reader["MrcQuantity"].ToString())) ? "" : reader["MrcQuantity"]),
                        (string)((String.IsNullOrEmpty(reader["EnteredQuantity"].ToString())) ? "" : reader["EnteredQuantity"])
                        
                        );
                    MRCs.Add(MRC);
				}

				reader.Close();

                return MRCs;
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
