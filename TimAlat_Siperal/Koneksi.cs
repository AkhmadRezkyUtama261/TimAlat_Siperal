using System;
using System.Data.SqlClient;

namespace TimAlat_Siperal
{
    class Koneksi
    {
        public SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection();

            // Menggunakan IP Address (192.168.94.188) agar bisa diakses Client
            conn.ConnectionString = @"Data Source=192.168.94.188\ANDHIKA1;Initial Catalog=DBPeminjamanAlat;User ID=sa;Password=Purworejo123;Encrypt=False";

            return conn;
        }
    }
}