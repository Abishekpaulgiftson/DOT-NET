using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_management_system
{
    public class Data
    {
        public SqlDataReader dr;
        public DataSet ds = new DataSet();
        public SqlConnection con()
        {
            SqlConnection con = new SqlConnection("server=Abishek;database=Shop management system;uid=sa;password=password123");
            con.Open();
            return con;
        }
        public void exec(string str)
        {
            SqlCommand cmd = new SqlCommand(str, con());
            cmd.ExecuteNonQuery();
        }
        public int exec1(string str)
        {
            SqlCommand cmd = new SqlCommand(str, con());
            return cmd.ExecuteNonQuery();
        }
        public SqlDataReader ret_dr(string str)
        {
            SqlCommand cmd = new SqlCommand(str, con());
            return cmd.ExecuteReader();
        }
        public DataSet ret_ds(string str)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter sqlda = new SqlDataAdapter(str, con());
            sqlda.Fill(ds);
            return ds;
        }

        public string GetScalar(string query0)
        {
            string result = null;
            {
                SqlCommand cmd = new SqlCommand(query0, con());

                object scalarResult = cmd.ExecuteScalar();
                result = scalarResult != null ? scalarResult.ToString() : null;
            }
            return result;
        }

        
    }
}
