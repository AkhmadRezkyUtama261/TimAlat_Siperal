using System;
using System.Data.SqlClient;

namespace TimAlat_Siperal
{
    class Koneksi
    {
  
        public SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=LAPTOP-7SOCNODM\ANDHIKA1;Initial Catalog=DBPeminjamanAlat;Integrated Security=True";
            return conn;
        }
    }
}