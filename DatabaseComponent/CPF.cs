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
	public class CpfDB
	{
		private string connectionString;

		public CpfDB()
		{
			// Get connection string from web.config.
			connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
		}
        public CpfDB(string connectionString)
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
        public void UpdateCPF1(string cpfID, string userName, string qualified, string status, string nextUser)
		{
			SqlConnection con = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand("UpdateCPF", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_CpfID", SqlDbType.VarChar, 20));
            cmd.Parameters["@p_CpfID"].Value = cpfID;

            cmd.Parameters.Add(new SqlParameter("@p_UserName", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserName"].Value = userName;
            
            cmd.Parameters.Add(new SqlParameter("@p_Qualified", SqlDbType.VarChar, 10));
            cmd.Parameters["@p_Qualified"].Value = qualified;

            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_Status"].Value = status;

			cmd.Parameters.Add(new SqlParameter("@p_NextUser", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_NextUser"].Value = nextUser;
                    
            
            
            
            
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
        // Modified By Minh 11-June-13
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCPFDisb1(string cpfID, string userName, string DisbStatus, string DisbRemark)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UpdateCPFOFDisb", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_CpfID", SqlDbType.VarChar, 20));
            cmd.Parameters["@p_CpfID"].Value = cpfID;

            cmd.Parameters.Add(new SqlParameter("@p_UserName", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserName"].Value = userName;

            cmd.Parameters.Add(new SqlParameter("@p_DisbStatus", SqlDbType.VarChar, 10));
            cmd.Parameters["@p_DisbStatus"].Value = DisbStatus;

            cmd.Parameters.Add(new SqlParameter("@p_DisbRemark", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_DisbRemark"].Value = DisbRemark;

       





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


        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCPFStamp(string ID, string user, string StampingStatus, string StampingRemark)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UpdateCPFOFStamp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_CpfID", SqlDbType.VarChar, 20));
            cmd.Parameters["@p_CpfID"].Value = ID;

            cmd.Parameters.Add(new SqlParameter("@p_UserName", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserName"].Value = user;

            cmd.Parameters.Add(new SqlParameter("@p_StampStatus", SqlDbType.VarChar, 10));
            cmd.Parameters["@p_StampStatus"].Value = StampingStatus;

            cmd.Parameters.Add(new SqlParameter("@p_StampRemark", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_StampRemark"].Value = StampingRemark;







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

        // end
        // Variant used for testing ObjectDataSourceUpdate2.aspx.
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCPF(string ID, string user, string qualified, string status, string nextUser)
        {
            // Just send the call to our standard method.
            UpdateCPF1(ID, user, qualified, status, nextUser);
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void UpdateCPFDisb(string ID, string user, string DisbursementStatus, string DisbursementRemark)
        {
            // Just send the call to our standard method.
            UpdateCPFDisb1(ID, user, DisbursementStatus, DisbursementRemark);
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
        public List<CPFDetails> GetCPFs(string contractNo, string status)
		{
			SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetCPFByCond", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_ContractNo", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_ContractNo"].Value = contractNo;
            
            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_Status"].Value = status;
            
            cmd.Parameters.Add(new SqlParameter("@p_User", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_User"].Value = System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1].ToString();
                				
			// Create a collection for all the employee records.
            List<CPFDetails> CPFs = new List<CPFDetails>();

			try 
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
                    CPFDetails CPF = new CPFDetails(

                        (string)((String.IsNullOrEmpty(reader["CpfID"].ToString())) ? "" : reader["CpfID"]),
                        (string)((String.IsNullOrEmpty(reader["EnvelopeDeID"].ToString())) ? "" : reader["EnvelopeDeID"]),

                        (string)((String.IsNullOrEmpty(reader["ContractNo"].ToString())) ? "" : reader["ContractNo"]),
                        (string)((String.IsNullOrEmpty(reader["Qualified"].ToString())) ? "" : reader["Qualified"]),
                        (string)((String.IsNullOrEmpty(reader["Status"].ToString())) ? "" : reader["Status"]),

                        (string)((String.IsNullOrEmpty(reader["ReceptionDate"].ToString())) ? "" : reader["ReceptionDate"]),
                        (string)((String.IsNullOrEmpty(reader["PreviousUser"].ToString())) ? "" : reader["PreviousUser"]),
                        (string)((String.IsNullOrEmpty(reader["NextUser"].ToString())) ? "" : reader["NextUser"]),
                        (string)((String.IsNullOrEmpty(reader["SipCode"].ToString())) ? "" : reader["SipCode"]),
                        (string)((String.IsNullOrEmpty(reader["AWB"].ToString())) ? "" : reader["AWB"]),
                        (string)((String.IsNullOrEmpty(reader["ChannelName"].ToString())) ? "" : reader["ChannelName"]),

                        (string)((String.IsNullOrEmpty(reader["DisbursementStatus"].ToString())) ? "" : reader["DisbursementStatus"]),
                         (string)((String.IsNullOrEmpty(reader["DisbursementRemark"].ToString())) ? "" : reader["DisbursementRemark"]),

                           (string)((String.IsNullOrEmpty(reader["StampingStatus"].ToString())) ? "" : reader["StampingStatus"]),
                         (string)((String.IsNullOrEmpty(reader["StampingRemark"].ToString())) ? "" : reader["StampingRemark"])
                                            
                        
                        

                        
                        
                        
                        
                        );
                    CPFs.Add(CPF);
				}

				reader.Close();

                return CPFs;
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

        public List<CPFDetails> GetCPFsforDisbursement(string contractNo, string status)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetCPFForDisbursement", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_ContractNo", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_ContractNo"].Value = contractNo;

            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_Status"].Value = status;

            cmd.Parameters.Add(new SqlParameter("@p_User", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_User"].Value = System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1].ToString();

            // Create a collection for all the employee records.
            List<CPFDetails> CPFs = new List<CPFDetails>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CPFDetails CPF = new CPFDetails(

                        (string)((String.IsNullOrEmpty(reader["CpfID"].ToString())) ? "" : reader["CpfID"]),
                        (string)((String.IsNullOrEmpty(reader["EnvelopeDeID"].ToString())) ? "" : reader["EnvelopeDeID"]),

                        (string)((String.IsNullOrEmpty(reader["ContractNo"].ToString())) ? "" : reader["ContractNo"]),
                        (string)((String.IsNullOrEmpty(reader["Qualified"].ToString())) ? "" : reader["Qualified"]),
                        (string)((String.IsNullOrEmpty(reader["Status"].ToString())) ? "" : reader["Status"]),

                        (string)((String.IsNullOrEmpty(reader["ReceptionDate"].ToString())) ? "" : reader["ReceptionDate"]),
                        (string)((String.IsNullOrEmpty(reader["PreviousUser"].ToString())) ? "" : reader["PreviousUser"]),
                        (string)((String.IsNullOrEmpty(reader["NextUser"].ToString())) ? "" : reader["NextUser"]),
                        (string)((String.IsNullOrEmpty(reader["SipCode"].ToString())) ? "" : reader["SipCode"]),
                        (string)((String.IsNullOrEmpty(reader["AWB"].ToString())) ? "" : reader["AWB"]),
                        (string)((String.IsNullOrEmpty(reader["ChannelName"].ToString())) ? "" : reader["ChannelName"]),

                        (string)((String.IsNullOrEmpty(reader["DisbursementStatus"].ToString())) ? "" : reader["DisbursementStatus"]),
                         (string)((String.IsNullOrEmpty(reader["DisbursementRemark"].ToString())) ? "" : reader["DisbursementRemark"]),

                           (string)((String.IsNullOrEmpty(reader["StampingStatus"].ToString())) ? "" : reader["StampingStatus"]),
                         (string)((String.IsNullOrEmpty(reader["StampingRemark"].ToString())) ? "" : reader["StampingRemark"])







                        );
                    CPFs.Add(CPF);
                }

                reader.Close();

                return CPFs;
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

        public List<CPFDetails> GetCPFsConStamp(string contractNo, string status)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetCPFsConStamp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_ContractNo", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_ContractNo"].Value = contractNo;

            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_Status"].Value = status;

            cmd.Parameters.Add(new SqlParameter("@p_User", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_User"].Value = System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1].ToString();

            // Create a collection for all the employee records.
            List<CPFDetails> CPFs = new List<CPFDetails>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CPFDetails CPF = new CPFDetails(

                        (string)((String.IsNullOrEmpty(reader["CpfID"].ToString())) ? "" : reader["CpfID"]),
                        (string)((String.IsNullOrEmpty(reader["EnvelopeDeID"].ToString())) ? "" : reader["EnvelopeDeID"]),

                        (string)((String.IsNullOrEmpty(reader["ContractNo"].ToString())) ? "" : reader["ContractNo"]),
                        (string)((String.IsNullOrEmpty(reader["Qualified"].ToString())) ? "" : reader["Qualified"]),
                        (string)((String.IsNullOrEmpty(reader["Status"].ToString())) ? "" : reader["Status"]),

                        (string)((String.IsNullOrEmpty(reader["ReceptionDate"].ToString())) ? "" : reader["ReceptionDate"]),
                        (string)((String.IsNullOrEmpty(reader["PreviousUser"].ToString())) ? "" : reader["PreviousUser"]),
                        (string)((String.IsNullOrEmpty(reader["NextUser"].ToString())) ? "" : reader["NextUser"]),
                        (string)((String.IsNullOrEmpty(reader["SipCode"].ToString())) ? "" : reader["SipCode"]),
                        (string)((String.IsNullOrEmpty(reader["AWB"].ToString())) ? "" : reader["AWB"]),
                        (string)((String.IsNullOrEmpty(reader["ChannelName"].ToString())) ? "" : reader["ChannelName"]),

                        (string)((String.IsNullOrEmpty(reader["DisbursementStatus"].ToString())) ? "" : reader["DisbursementStatus"]),
                         (string)((String.IsNullOrEmpty(reader["DisbursementRemark"].ToString())) ? "" : reader["DisbursementRemark"]),

                           (string)((String.IsNullOrEmpty(reader["StampingStatus"].ToString())) ? "" : reader["StampingStatus"]),
                         (string)((String.IsNullOrEmpty(reader["StampingRemark"].ToString())) ? "" : reader["StampingRemark"])







                        );
                    CPFs.Add(CPF);
                }

                reader.Close();

                return CPFs;
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
