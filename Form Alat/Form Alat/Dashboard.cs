using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_Alat
{
    public partial class Dashboard : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString = "Data Source=DEVALLDINOZAIN\\DEV;Initial Catalog= DBPeminjamanAlat;Integrated Security=True";
        public Dashboard()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            dataGridView1_CellContentClick(null,null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                
                string query = @"SELECT 
                                        Peminjam.Nama_Peminjam, 
                                        Alat.Nama_Alat, 
                                        Peminjaman.Jumlah_Pinjam,
                                        Peminjaman.Tanggal_Pinjam
                                    FROM Peminjaman
                                    JOIN Peminjam ON Peminjaman.NIK = Peminjam.NIK
                                    JOIN Alat ON Peminjaman.alatID = Alat.alatID";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

              
                dataGridView1.Columns[0].HeaderText = "Nama Peminjam";
                dataGridView1.Columns[1].HeaderText = "Nama Alat";
                dataGridView1.Columns[2].HeaderText = "Jumalah Pinjam";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal Load: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnAlat_Click(object sender, EventArgs e)
        {
            FormAlat frm = new FormAlat();
            frm.Show();
        }
    }
}
