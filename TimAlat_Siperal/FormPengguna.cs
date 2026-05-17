using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPengguna : Form
    {
        Koneksi konn = new Koneksi();

        // BINDING SOURCE (Untuk Poin 4 & 5 UCP 2)
        BindingSource bs = new BindingSource();

        // VARIABEL KRUSIAL: Menyimpan PeminjamID dari SQL
        int idPeminjam = 0;

        // ================= KUNCI REKAT MULTI-FORM =================
        // Menampung teks role akses yang di-passing dari Dashboard / Form Peminjaman
        public string StatusAkses { get; set; } = "ADMIN";

        public FormPengguna()
        {
            InitializeComponent();
            TampilData();
        }

        private void FormPengguna_Load(object sender, EventArgs e)
        {
            // Styling Dasar Bawaan kelompokmu
            this.BackColor = Color.FromArgb(245, 246, 250);
            dgvPengguna.BackgroundColor = Color.White;
            dgvPengguna.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ================= JURUS AMAN INTERAKTIF: EMBARGO HAK AKSES =================
            // Jika form dibuka secara cepat melalui operasional akun PETUGAS
            if (StatusAkses == "PETUGAS")
            {
                // 1. EMBARGO DATA DESTRUCTION: Sembunyikan tombol hapus secara permanen!
                if (btnHapus != null) btnHapus.Visible = false;

                // 2. AKSES OPERASIONAL: Tombol simpan baru dan ubah (koreksi typo) tetap menyala
                if (btnSimpan != null) btnSimpan.Visible = true;
                if (btnUbah != null) btnUbah.Visible = true;

                // 3. Ubah teks judul window form agar informatif
                this.Text = "Kelola Warga Cepat (Mode Operasional Petugas)";
            }
        }

        // ================= POIN 2, 4, & 5: VIEW & BINDING =================
        void TampilData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    // POIN 2: MENGGUNAKAN VIEW
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_Peminjam", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // POIN 4 & 5: MENGIKAT DATA KE BINDING SOURCE & NAVIGATOR
                    bs.DataSource = dt;
                    dgvPengguna.DataSource = bs;
                    bindingNavigator1.BindingSource = bs;

                    // Sembunyikan kolom PeminjamID biar UI tetap rapi
                    if (dgvPengguna.Columns["PeminjamID"] != null)
                    {
                        dgvPengguna.Columns["PeminjamID"].Visible = false;
                    }
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
            if (txtSearch != null) txtSearch.Clear();

            idPeminjam = 0; // Reset ID
            txtNIK.Enabled = true;
            txtNIK.Focus();
        }

        // ================= POIN 1: SP INSERT =================
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
                    SqlCommand cmd = new SqlCommand("sp_TambahPeminjam", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NIK", txtNIK.Text.Trim());
                    cmd.Parameters.AddWithValue("@NAMA", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoHp", txtTelp.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Disimpan!");
                    TampilData();
                    Bersihkan();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Peringatan Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // ================= POIN 1: SP UPDATE =================
        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (idPeminjam == 0)
            {
                MessageBox.Show("Pilih data yang mau diubah dulu dari tabel!");
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_UpdatePeminjam", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@peminjamID", idPeminjam);
                    cmd.Parameters.AddWithValue("@NIK", txtNIK.Text.Trim());
                    cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text.Trim());
                    cmd.Parameters.AddWithValue("@NoHP", txtTelp.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diubah!");
                    TampilData();
                    Bersihkan();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Peringatan Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // ================= POIN 1: SP DELETE =================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (idPeminjam == 0)
            {
                MessageBox.Show("Pilih dulu data yang mau dihapus dari tabel!");
                return;
            }

            if (MessageBox.Show("Yakin hapus data peminjam ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_HapusPeminjam", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PeminjamID", idPeminjam);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Dihapus!");
                        TampilData();
                        Bersihkan();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Peringatan Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        // ================= MENANGKAP ID SAAT TABEL DIKLIK (VERSI ANTI-CRASH DBNull) =================
        private void dgvPengguna_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvPengguna.Rows[e.RowIndex];

                // Cegah Crash akibat DBNull
                if (row.Cells["PeminjamID"].Value != DBNull.Value && row.Cells["PeminjamID"].Value != null)
                {
                    idPeminjam = Convert.ToInt32(row.Cells["PeminjamID"].Value);
                }
                else
                {
                    idPeminjam = 0;
                }

                txtNIK.Text = row.Cells["NIK"].Value != DBNull.Value ? row.Cells["NIK"].Value.ToString() : "";
                txtNama.Text = row.Cells["Nama_Peminjam"].Value != DBNull.Value ? row.Cells["Nama_Peminjam"].Value.ToString() : "";
                txtAlamat.Text = row.Cells["Alamat"].Value != DBNull.Value ? row.Cells["Alamat"].Value.ToString() : "";
                txtTelp.Text = row.Cells["NomorHP"].Value != DBNull.Value ? row.Cells["NomorHP"].Value.ToString() : "";
            }
        }

        // ================= POIN 3: SQL INJECTION (DI TOMBOL CARI DATA) =================
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    // Celah sengaja dibuka untuk demo SQLi ke dosen menggunakan LIKE
                    string queryBocor = "SELECT * FROM vw_Peminjam WHERE Nama_Peminjam LIKE '%" + txtSearch.Text + "%'";

                    SqlDataAdapter da = new SqlDataAdapter(queryBocor, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bs.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Pencarian: " + ex.Message);
                }
            }
        }
    } // Kurung kurawal penutup Class
} // Kurung kurawal penutup Namespace