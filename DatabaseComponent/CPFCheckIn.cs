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
	public class CPFCheckIn
	{
		private string connectionString;

		public CPFCheckIn()
		{
			// Get connection string from web.config.
			connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
		}
        public CPFCheckIn(string connectionString)
		{
			this.connectionString = connectionString;
		}

       

       
      
        // Modified By Minh 11-June-13
     


      

        // end
        // Variant used for testing ObjectDataSourceUpdate2.aspx.
        

       
      

     

      

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<CPFCheckInDetails> GetCPFDetails(string contractNo)
		{
			SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetCPFDetail", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_ContractNo", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_ContractNo"].Value = contractNo;
            
           
			// Create a collection for all the employee records.
            List<CPFCheckInDetails> CPFs = new List<CPFCheckInDetails>();

			try 
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
                    CPFCheckInDetails CPF = new CPFCheckInDetails(

                        (string)((String.IsNullOrEmpty(reader["CpfID"].ToString())) ? "" : reader["CpfID"]),
                        (string)((String.IsNullOrEmpty(reader["CpfDetailID"].ToString())) ? "" : reader["CpfDetailID"]),

                        (string)((String.IsNullOrEmpty(reader["ContractNo"].ToString())) ? "" : reader["ContractNo"]),
                        (string)((String.IsNullOrEmpty(reader["Doc"].ToString())) ? "" : reader["Doc"]),
                        (string)((String.IsNullOrEmpty(reader["Status"].ToString())) ? "" : reader["Status"]),

                        (string)((String.IsNullOrEmpty(reader["Mistake"].ToString())) ? "" : reader["Mistake"]),
                        (string)((String.IsNullOrEmpty(reader["MistakeType"].ToString())) ? "" : reader["MistakeType"]),
                        (string)((String.IsNullOrEmpty(reader["Description"].ToString())) ? "" : reader["Description"]),
                        (string)((String.IsNullOrEmpty(reader["DocID"].ToString())) ? "" : reader["DocID"]),
                        (string)((String.IsNullOrEmpty(reader["Category"].ToString())) ? "" : reader["Category"])
                  
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
