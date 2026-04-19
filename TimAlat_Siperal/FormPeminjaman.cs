using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPeminjaman : Form
    {
        Koneksi konn = new Koneksi();
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataTable dt;
        string idTerpilih = ""; 

        public FormPeminjaman()
        {
            InitializeComponent();
            LoadAlat();
            ShowData();
            panelTransaksi.Enabled = false;
        }

        void ShowData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    string sql = @"SELECT p.peminjamanID AS [ID], m.Nama_Peminjam AS [Nama Warga], 
                                   a.Nama_Alat AS [Alat], p.Jumlah_Pinjam AS [Jumlah], 
                                   p.Tanggal_Pinjam AS [Tanggal], p.Status AS [Status]
                                   FROM Peminjaman p
                                   JOIN Peminjam m ON p.NIK = m.NIK
                                   JOIN Alat a ON p.alatID = a.alatID
                                   ORDER BY p.peminjamanID DESC";
                    da = new SqlDataAdapter(sql, conn);
                    dt = new DataTable();
                    da.Fill(dt);
                    dgvPeminjaman.DataSource = dt;
                    dgvPeminjaman.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex) { MessageBox.Show("Gagal Load Tabel: " + ex.Message); }
            }
        }

        void LoadAlat()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT Nama_Alat FROM Alat", conn);
                    dr = cmd.ExecuteReader();
                    cbAlat.Items.Clear();
                    while (dr.Read()) { cbAlat.Items.Add(dr["Nama_Alat"].ToString()); }
                }
                catch (Exception ex) { MessageBox.Show("Gagal Load Alat: " + ex.Message); }
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNIK.Text)) { MessageBox.Show("Masukkan NIK!"); return; }
            using (SqlConnection conn = konn.GetConn())
            {
                conn.Open();
                cmd = new SqlCommand("SELECT Nama_Peminjam, Alamat FROM Peminjam WHERE NIK = @nik", conn);
                cmd.Parameters.AddWithValue("@nik", txtNIK.Text.Trim());
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lblNamaPeminjam.Text = dr["Nama_Peminjam"].ToString();
                    lblAlamat.Text = dr["Alamat"].ToString();
                    panelTransaksi.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Warga tidak ditemukan!");
                    panelTransaksi.Enabled = false;
                }
            }
        }
