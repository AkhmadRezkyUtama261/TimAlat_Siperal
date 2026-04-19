using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPengguna : Form
    {
        Koneksi konn = new Koneksi();
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;

       
        string nikLama = "";

        public FormPengguna()
        {
            InitializeComponent();
            TampilData();
        }

        void TampilData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    da = new SqlDataAdapter("SELECT * FROM Peminjam", conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    dgvPengguna.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Tampil Data: " + ex.Message);
                }
            }
        }

        void Bersihkan()
        {
            txtNIK.Clear();
            txtNama.Clear();
            txtAlamat.Clear();
            txtTelp.Clear();

            nikLama = ""; 
            txtNIK.Enabled = true; 
            txtNIK.Focus();
        }

        // 1. Tombol SIMPAN 
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtNIK.Text.Trim() == "" || txtNama.Text.Trim() == "")
            {
                MessageBox.Show("NIK dan Nama wajib diisi!");
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Peminjam (NIK, Nama_Peminjam, Alamat, NomorHP) " +
                                   "VALUES ('" + txtNIK.Text.Trim() + "', '" + txtNama.Text.Trim() + "', '" + txtAlamat.Text.Trim() + "', '" + txtTelp.Text.Trim() + "')";

                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Disimpan!");
                    TampilData();
                    Bersihkan();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Simpan: " + ex.Message);
                }
            }
        }
