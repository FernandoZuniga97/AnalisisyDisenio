using System;
using System.Data;
using System.Data.SqlClient;

namespace MyWinFormsApp.Database
{
    public class DataAccess
    {
        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection conn = DbConfig.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    return dt;
                }
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (SqlConnection conn = DbConfig.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
