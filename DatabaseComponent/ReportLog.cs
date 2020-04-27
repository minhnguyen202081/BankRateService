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
    public class REPORTLOGDB
	{
		private string connectionString;

		public REPORTLOGDB()
		{
			// Get connection string from web.config.
            connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
		}
        public REPORTLOGDB(string connectionString)
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
        public List<REPORTLOGDetails> GetREPORTLOGs(string ReportCode)
		{
			SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetREPORTLOGCond", con);
			cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_UserName", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserName"].Value = System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1].ToString();

            cmd.Parameters.Add(new SqlParameter("@p_ReportCode", SqlDbType.VarChar, 1000));
            cmd.Parameters["@p_ReportCode"].Value = ReportCode;
              	
			// Create a collection for all the employee records.
            List<REPORTLOGDetails> REPORTLOGs = new List<REPORTLOGDetails>();

			try 
			{
				con.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
                    REPORTLOGDetails REPORTLOG = new REPORTLOGDetails(

                        (string)((String.IsNullOrEmpty(reader["ReportLogID"].ToString())) ? "" : reader["ReportLogID"]),
                        (string)((String.IsNullOrEmpty(reader["ReportCode"].ToString())) ? "" : reader["ReportCode"]),
                        (string)((String.IsNullOrEmpty(reader["ReportName"].ToString())) ? "" : reader["ReportName"]),
                        (string)((String.IsNullOrEmpty(reader["ExtractedDate"].ToString())) ? "" : reader["ExtractedDate"]),
                        (string)((String.IsNullOrEmpty(reader["UserName"].ToString())) ? "" : reader["UserName"]),
                        (string)((String.IsNullOrEmpty(reader["FileName"].ToString())) ? "" : reader["FileName"]),
                        (string)((String.IsNullOrEmpty(reader["Status"].ToString())) ? "" : reader["Status"]),
                        (string)((String.IsNullOrEmpty(reader["FileLocation"].ToString())) ? "" : reader["FileLocation"]),
                        (string)((String.IsNullOrEmpty(reader["Duration"].ToString())) ? "" : reader["Duration"])
                        );
                    REPORTLOGs.Add(REPORTLOG);
				}

				reader.Close();

                return REPORTLOGs;
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
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

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
