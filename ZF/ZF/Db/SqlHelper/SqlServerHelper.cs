using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Db.SqlHelper
{
    public class SqlServerHelper
    {
        public static DataTable Get(string conn, string sql, params SqlParameter[] paramter)
        {
            using (SqlConnection dbconn = new SqlConnection(conn))
            {
                dbconn.Open();
                using (SqlCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(paramter);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dbconn.Close();
                    return dt;
                }
            }
        }

        public static void ExecuteNonQuery(string conn, string sql, params SqlParameter[] paramter)
        {
            using (SqlConnection dbconn = new SqlConnection(conn))
            {
                dbconn.Open();
                using (SqlCommand cmd = dbconn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(paramter);
                    cmd.ExecuteNonQuery();
                    dbconn.Close();
                }
            }
        }
    }
}
