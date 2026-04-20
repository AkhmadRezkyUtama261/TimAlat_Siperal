using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Form_Alat
{
    public partial class Form1 : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString = "Data Source=DEVALLDINOZAIN\\DEV;Initial Catalog= DBPeminjamanAlat;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void btnMenampilkanDataAlat_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();


                dataGridView1.Columns.Add("Nama_Alat", "Nama Alat");
                dataGridView1.Columns.Add("Stok", "Stok");
                

                string query = "SELECT * FROM Alat";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    dataGridView1.Rows.Add(
                        reader["Nama_Alat"].ToString(),
                        reader["stok"].ToString()
                        
                     );
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan data: " + ex.Message);
            }
            
        }

        private void btnTambahAlat_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (TXTNamaAlat.Text == "")
                {
                    MessageBox.Show("Nama Alat Wajib Diisyi");
                    TXTNamaAlat.Focus();
                    return;
                }

                if (TXTStok.Text == "")
                {
                    MessageBox.Show("Stok Wajib Diisyi");
                    TXTStok.Focus();
                    return;
                }

                string query = @"INSERT INTO Alat
                                (Nama_Alat, Stok) VALUES (@Nama_Alat, @Stok)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Nama_Alat", TXTNamaAlat.Text);
                cmd.Parameters.AddWithValue("@Stok", TXTStok.Text);
                

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data Alat berhasil ditambahkyan");
                    ClearForm();
                    btnMenampilkanDataAlat.PerformClick();
                }
                else
                {
                    MessageBox.Show("Data gagal ditambahkyan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesyalahan: " + ex.Message);
            }
        }

        private void btnUpdateAlat_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = @"UPDATE Alat 
                                SET Nama_Alat = @Nama_Alat, Stok = @Stok
                                WHERE Nama_Alat = @Nama_Alat";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Nama_Alat", TXTNamaAlat.Text);
                cmd.Parameters.AddWithValue("@Stok", TXTStok.Text);
             
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    MessageBox.Show("Data  berhasil diUpdate");
                    ClearForm();
                    btnMenampilkanDataAlat.PerformClick();
                }
                else
                {
                    MessageBox.Show("Data tidak ditemukyan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan " + ex.Message);
            }
        }


        private void btnHapusAlat_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                DialogResult resultConfirm = MessageBox.Show(
                    "Kenpa di Hapus Sihh Syebell huhh",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultConfirm == DialogResult.Yes)
                {
                    string query = "DELETE FROM Alat WHERE Nama_Alat = @Nama_Alat";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Nama_Alat", TXTNamaAlat.Text);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Data  berhasil diHapyus");
                        ClearForm();
                        btnMenampilkanDataAlat.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Data tidak ditemukyan");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                TXTNamaAlat.Text = row.Cells["Nama_Alat"].Value.ToString();
                TXTStok.Text = row.Cells["Stok"].Value.ToString();

            }
        }
        private void ClearForm()
        {
            TXTNamaAlat.Clear();
            TXTStok.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.CellClick += dataGridView1_CellClick;

        }
    }
    
}
