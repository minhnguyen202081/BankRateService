using BR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace BR.TenorModel
{
    public class HomeRepository
    {
        private BRContext db;

        public HomeRepository(BRContext db)
        {
            this.db = db;
        }

        public List<HOME> selectAll()
        {
            
            return db.Database.SqlQuery<HOME> ("sp_ViewAll").ToList();
        }
        public DataTable display(string bankCode)
        {
            DataTable dt = new DataTable();

            dt = getData(bankCode);
            
            return dt;
        }
        public DataTable getData(string bankCode)
        {
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BRContext"].ConnectionString))
            using (var cmd = new SqlCommand("sp_ViewAll", con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BankCode", SqlDbType.NVarChar, 2000).Value = bankCode;
                da.Fill(table);
                
            }
            return table;
        }



    }
}
