using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPeminjaman : Form
    {
        // 1. KONEKSI & BINDING SOURCE
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();

        // 2. VARIABEL PENAMPUNG ID DATA
        string idTerpilih = ""; // Menyimpan PeminjamanID dari DataGridView
        int idPeminjamTerpilih = 0; // Menyimpan PeminjamID asli hasil pencarian nama

        public FormPeminjaman()
        {
            InitializeComponent();
            LoadAlatUnik(); // Menggunakan pemanggilan combo teranyar
            ShowData();

            // Kunci panel transaksi sebelum nama warga berhasil dicari demi keamanan sistem
            if (panelTransaksi != null) panelTransaksi.Enabled = false;
        }

        // ================= POIN 2 & 4: SELECT DATA VIEW & MANUAL DATA CONTROL =================
        void ShowData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vm_MenampilkanDaftarPeminjaman", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bs.DataSource = dt;

                    if (dgvPeminjaman != null)
                    {
                        dgvPeminjaman.DataSource = bs;
                        dgvPeminjaman.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }

                    if (bindingNavigator1 != null)
                    {
                        bindingNavigator1.BindingSource = bs;
                    }

                    // REVISI TOTAL: Hapus baris .DataBindings.Add() lama agar label kiri 
                    // TIDAK otomatis mencuri/mengintip data baris pertama tabel saat form dibuka!
                    if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                    if (lblAlamat != null) lblAlamat.DataBindings.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Memuat Tabel Peminjaman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================= PENYEMPURNAAN: LOAD COMBO BOX ALAT MULTI-INFORMASI =================
        void LoadAlatUnik()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    // Mengambil data lengkap Alat yang stoknya tersedia di gudang RT
                    SqlCommand cmd = new SqlCommand("SELECT alatID, Nama_Alat, Merek FROM Alat WHERE Stok > 0", conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (cbAlat != null)
                    {
                        cbAlat.Items.Clear();
                        cbAlat.Items.Add("--- Pilih Alat ---");

                        while (dr.Read())
                        {
                            // Menggabungkan Atribut Data Menjadi String Unik Eksklusif
                            string itemTampil = $"{dr["Nama_Alat"]} {dr["Merek"]} ({dr["alatID"]})";
                            cbAlat.Items.Add(itemTampil);
                        }
                        cbAlat.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Memuat Daftar Alat Unik: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================= PENYESUAIAN DETEKSI INDEX CHANGED COMBO UNIK =================
        private void cbAlat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAlat == null || cbAlat.SelectedIndex <= 0 || cbAlat.Text.StartsWith("---")) return;

            // Trik Pemisah: Ambil ID Alat asli yang berada di dalam kurung paling ujung string
            string selectedText = cbAlat.SelectedItem.ToString();
            int indexBukaKurung = selectedText.LastIndexOf('(');
            int indexTutupKurung = selectedText.LastIndexOf(')');

            if (indexBukaKurung == -1 || indexTutupKurung == -1) return;

            string idAlatTerpilih = selectedText.Substring(indexBukaKurung + 1, indexTutupKurung - indexBukaKurung - 1);

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    // Cari jumlah stok berdasarkan ID Alat murni database agar akurat
                    SqlCommand cmd = new SqlCommand("SELECT Stok FROM Alat WHERE alatID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", idAlatTerpilih);
                    object res = cmd.ExecuteScalar();

                    if (lblStok != null) lblStok.Text = (res != null) ? res.ToString() : "0";
                }
                catch { if (lblStok != null) lblStok.Text = "0"; }
            }
        }

        // ================= POIN 1: STORED PROCEDURE (INSERT) TRANSAKSI AMAN =================
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cbAlat.SelectedIndex <= 0 || cbAlat.Text.StartsWith("---"))
            {
                MessageBox.Show("Silakan pilih alat yang valid terlebih dahulu!"); return;
            }

            if (txtJumlah == null || !int.TryParse(txtJumlah.Text, out int jmlPinjam) || jmlPinjam <= 0)
            {
                MessageBox.Show("Jumlah pinjam harus berupa angka bulat dan minimal 1!"); return;
            }

            int stokTersedia = int.Parse(lblStok.Text);
            if (jmlPinjam > stokTersedia)
            {
                MessageBox.Show("Stok alat tidak mencukupi untuk dipinjam!", "Stok Kurang", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            // Trik Pemisah ID: Ekstrak kembali ID Alat asli untuk dilempar ke Stored Procedure
            string selectedText = cbAlat.SelectedItem.ToString();
            int indexBukaKurung = selectedText.LastIndexOf('(');
            int indexTutupKurung = selectedText.LastIndexOf(')');
            string idAlatAsli = selectedText.Substring(indexBukaKurung + 1, indexTutupKurung - indexBukaKurung - 1);

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();

                    SqlCommand cmdSP = new SqlCommand("sp_TambahPinjam", conn);
                    cmdSP.CommandType = CommandType.StoredProcedure;

                    cmdSP.Parameters.AddWithValue("@petugasID", 1); // Default Petugas ID operasional
                    cmdSP.Parameters.AddWithValue("@peminjamID", idPeminjamTerpilih);
                    cmdSP.Parameters.AddWithValue("@alatID", idAlatAsli); // Mengirim ID bersih murni database
                    cmdSP.Parameters.AddWithValue("@jumlah_Pinjam", jmlPinjam);

                    cmdSP.ExecuteNonQuery();

                    MessageBox.Show("Transaksi Peminjaman Berhasil Disimpan via Stored Procedure!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    LoadAlatUnik(); // Memperbarui item combo box setelah stok berubah
                    Bersihkan();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menyimpan transaksi: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        // ================= FITUR CARI WARGA MULTI-HASIL DENGAN COUPLING AMAN =================
        private void btnCari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCariNama.Text))
            {
                MessageBox.Show("Ketikkan Nama Warga yang ingin dicari terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();

                    string queryCari = "SELECT PeminjamID, NIK, Nama_Peminjam, Alamat, NomorHP FROM Peminjam WHERE Nama_Peminjam LIKE @nama";
                    SqlDataAdapter da = new SqlDataAdapter(queryCari, conn);
                    da.SelectCommand.Parameters.AddWithValue("@nama", "%" + txtCariNama.Text.Trim() + "%");

                    DataTable dtWarga = new DataTable();
                    da.Fill(dtWarga);

                    if (dtWarga.Rows.Count == 0)
                    {
                        MessageBox.Show("Warga dengan nama tersebut tidak ditemukan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Bersihkan();
                    }
                    else if (dtWarga.Rows.Count == 1)
                    {
                        DataRow dr = dtWarga.Rows[0];
                        idPeminjamTerpilih = Convert.ToInt32(dr["PeminjamID"]);

                        if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                        if (lblAlamat != null) lblAlamat.DataBindings.Clear();

                        // Set text label kiri murni berdasarkan data warga hasil pencarian
                        if (lblNamaPeminjam != null) lblNamaPeminjam.Text = dr["NIK"].ToString();
                        if (lblAlamat != null) lblAlamat.Text = dr["Alamat"].ToString();
                        txtCariNama.Text = dr["Nama_Peminjam"].ToString();

                        if (panelTransaksi != null) panelTransaksi.Enabled = true;
                        MessageBox.Show("Data Warga Berhasil Ditemukan! Menu transaksi dibuka.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                        if (lblAlamat != null) lblAlamat.DataBindings.Clear();

                        if (dgvPeminjaman != null)
                        {
                            dgvPeminjaman.DataSource = dtWarga;
                            if (dgvPeminjaman.Columns["PeminjamID"] != null)
                                dgvPeminjaman.Columns["PeminjamID"].Visible = false;
                        }

                        MessageBox.Show($"Ditemukan {dtWarga.Rows.Count} warga yang mirip!\n\nSilakan KLIK BARIS WARGA yang benar pada TABEL DI SEBELAH KANAN.", "Pilihan Warga Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (panelTransaksi != null) panelTransaksi.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat mencari data warga: " + ex.Message);
                }
            }
        }

        // ================= REVISI CELL CLICK: PILIH WARGA KEMBAR VS VIEW DETAIL TRANSAKSI =================
        private void dgvPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPeminjaman == null || e.RowIndex < 0) return;

            DataGridViewRow row = dgvPeminjaman.Rows[e.RowIndex];

            // KONDISI A: Jika tabel sedang menampilkan daftar pencarian nama warga kembar (punya kolom NIK bawaan tabel Peminjam)
            if (dgvPeminjaman.Columns["NIK"] != null && dgvPeminjaman.DataSource != bs)
            {
                if (row.Cells["PeminjamID"].Value != null && row.Cells["PeminjamID"].Value != DBNull.Value)
                {
                    idPeminjamTerpilih = Convert.ToInt32(row.Cells["PeminjamID"].Value);

                    if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                    if (lblAlamat != null) lblAlamat.DataBindings.Clear();

                    if (lblNamaPeminjam != null) lblNamaPeminjam.Text = row.Cells["NIK"].Value.ToString();
                    if (lblAlamat != null) lblAlamat.Text = row.Cells["Alamat"].Value.ToString();
                    txtCariNama.Text = row.Cells["Nama_Peminjam"].Value.ToString();

                    if (panelTransaksi != null) panelTransaksi.Enabled = true;
                    MessageBox.Show($"Warga '{txtCariNama.Text}' Berhasil Dipilih! Menu transaksi dibuka.", "Sukses Memilih", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowData(); // Kembalikan tabel ke daftar riwayat utama
                }
            }
            // KONDISI B: Jika tabel dalam keadaan normal (User klik baris riwayat transaksi untuk melihat rincian detailnya)
            else
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value != DBNull.Value)
                {
                    idTerpilih = row.Cells[0].Value.ToString();

                    // Tampilkan rincian data baris riwayat transaksi yang diklik ke label kiri secara responsif
                    if (lblNamaPeminjam != null && row.Cells["NamaPeminjam"].Value != null)
                        lblNamaPeminjam.Text = row.Cells["NamaPeminjam"].Value.ToString(); // Menampilkan nama peminjam dari riwayat

                    if (lblAlamat != null && row.Cells["NamaAlat"].Value != null)
                        lblAlamat.Text = row.Cells["NamaAlat"].Value.ToString(); // Menampilkan nama alat dari riwayat
                }
            }
        }

        // ================= POIN 1: STORED PROCEDURE (UPDATE PENGEMBALIAN & CEK DENDA) =================
        private void btnKembalikan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTerpilih)) { MessageBox.Show("Klik data transaksi di tabel terlebih dahulu!"); return; }

            if (MessageBox.Show("Kembalikan alat ini?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();

                        string queryIntip = "SELECT Tanggal_Pinjam FROM Peminjaman WHERE peminjamanID = @id";
                        SqlCommand cmdIntip = new SqlCommand(queryIntip, conn);
                        cmdIntip.Parameters.AddWithValue("@id", idTerpilih);
                        DateTime tanggalPinjam = Convert.ToDateTime(cmdIntip.ExecuteScalar());

                        int totalHariMulaiPinjam = (DateTime.Now.Date - tanggalPinjam.Date).Days;
                        int hariTelat = totalHariMulaiPinjam - 5;

                        string pesanSukses = "Barang berhasil dikembalikan! Status: Tepat Waktu.";

                        if (hariTelat > 0)
                        {
                            int totalDenda = hariTelat * 5000;
                            pesanSukses = $"Barang berhasil dikembalikan!\n⚠️ Warga terlambat {hariTelat} hari.\n💵 Sanksi Denda yang Harus Dibayar: Rp {totalDenda:N0}";
                        }

                        SqlCommand cmd = new SqlCommand("sp_UpdatePeminjaman", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@peminjamanID", Convert.ToInt32(idTerpilih));
                        cmd.ExecuteNonQuery();

                        MessageBox.Show(pesanSukses, "Informasi Pengembalian", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ShowData();
                        LoadAlatUnik();
                        idTerpilih = "";
                    }
                    catch (Exception ex) { MessageBox.Show("Error Database: " + ex.Message); }
                }
            }
        }

        // ================= POIN 1: STORED PROCEDURE (DELETE) =================
        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTerpilih)) { MessageBox.Show("Pilih data riwayat transaksi di tabel yang ingin dihapus!"); return; }

            if (MessageBox.Show("Apakah Anda yakin ingin menghapus data riwayat transaksi ini?", "Hapus Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("sp_HapusPeminjaman", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@PeminjamanID", Convert.ToInt32(idTerpilih));
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Riwayat Transaksi Berhasil Dihapus!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ShowData();
                        idTerpilih = "";
                    }
                    catch (Exception ex) { MessageBox.Show("Gagal menghapus data: " + ex.Message); }
                }
            }
        }

        // ================= REVISI RESET FORM SINKRON =================
        void Bersihkan()
        {
            if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
            if (lblAlamat != null) lblAlamat.DataBindings.Clear();

            if (txtCariNama != null) txtCariNama.Clear();
            if (lblNamaPeminjam != null) lblNamaPeminjam.Text = "-";
            if (lblAlamat != null) lblAlamat.Text = "-";
            if (cbAlat != null) { cbAlat.SelectedIndex = 0; }
            if (lblStok != null) lblStok.Text = "0";
            if (txtJumlah != null) txtJumlah.Clear();
            if (panelTransaksi != null) panelTransaksi.Enabled = false;

            idTerpilih = "";
            idPeminjamTerpilih = 0;

            ShowData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void FormPeminjaman_Load(object sender, EventArgs e) { }
        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e) { }
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e) { }
    }
}