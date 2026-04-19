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

       