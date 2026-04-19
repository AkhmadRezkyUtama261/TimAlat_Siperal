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
        // 2. Tombol UBAH (Update pakai memori nikLama)
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (txtNIK.Text.Trim() == "" || nikLama == "")
            {
                MessageBox.Show("Pilih data yang mau diubah dulu dari tabel!");
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    // Query diubah: Kita ganti juga kolom NIK-nya, dan mencari berdasarkan nikLama
                    string query = "UPDATE Peminjam SET NIK='" + txtNIK.Text.Trim() + "', Nama_Peminjam='" + txtNama.Text.Trim() + "', Alamat='" + txtAlamat.Text.Trim() + "', NomorHP='" + txtTelp.Text.Trim() + "' WHERE NIK='" + nikLama + "'";
                    cmd = new SqlCommand(query, conn);

                    int hasil = cmd.ExecuteNonQuery();
                    if (hasil > 0)
                    {
                        MessageBox.Show("Data Berhasil Diubah!");
                    }
                    else
                    {
                        MessageBox.Show("Gagal! Data tidak ditemukan di database.");
                    }

                    TampilData();
                    Bersihkan();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Ubah: " + ex.Message);
                }
            }
        }

        // 3. Tombol HAPUS 
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtNIK.Text.Trim() == "")
            {
                MessageBox.Show("Pilih dulu data yang mau dihapus dari tabel!");
                return;
            }

            if (MessageBox.Show("Yakin hapus data NIK: " + txtNIK.Text.Trim() + "?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        // Hapus menggunakan nikLama biar lebih akurat
                        string query = "DELETE FROM Peminjam WHERE NIK='" + nikLama + "'";
                        cmd = new SqlCommand(query, conn);

                        int hasil = cmd.ExecuteNonQuery();

                        if (hasil > 0)
                        {
                            MessageBox.Show("Data Berhasil Dihapus!");
                        }
                        else
                        {
                            MessageBox.Show("Gagal! Data tidak ditemukan di database.");
                        }

                        TampilData();
                        Bersihkan();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal Hapus: " + ex.Message);
                    }
                }
            }
        }
