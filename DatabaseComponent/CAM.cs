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
    public class CamDB
    {
        private string connectionString;

        public CamDB()
        {
            // Get connection string from web.config.
            connectionString = WebConfigurationManager.ConnectionStrings["BO"].ConnectionString;
        }
        public CamDB(string connectionString)
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
        public void UpdateCAM1(string mistakeID, string userName, string mistakeTypeID, string qualified, string status, string nextUser, string comment)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UpdateCAM", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_MistakeID", SqlDbType.VarChar, 20));
            cmd.Parameters["@p_MistakeID"].Value = mistakeID;

            cmd.Parameters.Add(new SqlParameter("@p_UserName", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_UserName"].Value = userName;

            cmd.Parameters.Add(new SqlParameter("@p_MistakeTypeID", SqlDbType.NVarChar, 50));
            cmd.Parameters["@p_MistakeTypeID"].Value = mistakeTypeID;

            cmd.Parameters.Add(new SqlParameter("@p_Qualified", SqlDbType.VarChar, 10));
            cmd.Parameters["@p_Qualified"].Value = qualified;

            cmd.Parameters.Add(new SqlParameter("@p_Status", SqlDbType.VarChar, 20));
            cmd.Parameters["@p_Status"].Value = status;

            cmd.Parameters.Add(new SqlParameter("@p_NextUser", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_NextUser"].Value = nextUser;

            cmd.Parameters.Add(new SqlParameter("@p_Comment", SqlDbType.NVarChar, 255));
            cmd.Parameters["@p_Comment"].Value = comment;



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
        public void UpdateCAM(string ID, string user, string mistakeTypeID, string qualified, string status, string nextUser, string comment)
        {
            // Just send the call to our standard method.
            UpdateCAM1(ID, user, mistakeTypeID, qualified, status, nextUser, comment);
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
        public List<CAMDetails> GetCAMs(string contractNo, string sipCode)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("GetCAMByCond", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_ContractNo", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_ContractNo"].Value = contractNo;

            cmd.Parameters.Add(new SqlParameter("@p_SipCode", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_SipCode"].Value = sipCode;

            cmd.Parameters.Add(new SqlParameter("@p_User", SqlDbType.VarChar, 50));
            cmd.Parameters["@p_User"].Value = System.Web.HttpContext.Current.User.Identity.Name.Split('\\')[1].ToString();

            // Create a collection for all the employee records.
            List<CAMDetails> CAMs = new List<CAMDetails>();

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CAMDetails CAM = new CAMDetails(

                        (string)((String.IsNullOrEmpty(reader["MistakeID"].ToString())) ? "" : reader["MistakeID"]),
                        (string)((String.IsNullOrEmpty(reader["EnvelopeDeID"].ToString())) ? "" : reader["EnvelopeDeID"]),

                        (string)((String.IsNullOrEmpty(reader["MistakeTypeID"].ToString())) ? "" : reader["MistakeTypeID"]),
                        (string)((String.IsNullOrEmpty(reader["MistakeTypeName"].ToString())) ? "" : reader["MistakeTypeName"]),                        

                        (string)((String.IsNullOrEmpty(reader["ContractNo"].ToString())) ? "" : reader["ContractNo"]),
                        (string)((String.IsNullOrEmpty(reader["ReceptionDate"].ToString())) ? "" : reader["ReceptionDate"]),
                        (string)((String.IsNullOrEmpty(reader["Qualified"].ToString())) ? "" : reader["Qualified"]),

                        (string)((String.IsNullOrEmpty(reader["SipCode"].ToString())) ? "" : reader["SipCode"]),
                        (string)((String.IsNullOrEmpty(reader["Status"].ToString())) ? "" : reader["Status"]),
                        (string)((String.IsNullOrEmpty(reader["PreviousUser"].ToString())) ? "" : reader["PreviousUser"]),

                        (string)((String.IsNullOrEmpty(reader["NextUser"].ToString())) ? "" : reader["NextUser"]),
                        (string)((String.IsNullOrEmpty(reader["Comment"].ToString())) ? "" : reader["Comment"])

                        );

                    CAMs.Add(CAM);
                }

                reader.Close();

                return CAMs;
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
