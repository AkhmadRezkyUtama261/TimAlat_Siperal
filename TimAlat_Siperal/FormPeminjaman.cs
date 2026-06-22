using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPeminjaman : Form
    {
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();

        string idTerpilih = "";
        int idPeminjamTerpilih = 0;

        public FormPeminjaman()
        {
            InitializeComponent();
            LoadPeminjamUnik();
            LoadAlatUnik();
            ShowData();

            if (panelTransaksi != null) panelTransaksi.Enabled = false;
            if (btnCari != null) btnCari.Visible = false; // Tombol pencarian disembunyikan
        }

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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Memuat Tabel Peminjaman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadPeminjamUnik()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT PeminjamID, Nama_Peminjam FROM Peminjam", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (txtCariNama != null)
                    {
                        txtCariNama.DataSource = dt;
                        txtCariNama.DisplayMember = "Nama_Peminjam";
                        txtCariNama.ValueMember = "PeminjamID";
                        txtCariNama.SelectedIndex = -1;
                        txtCariNama.Text = "-- Pilih Peminjam --";
                    }
                }
                catch (Exception) { }
            }
        }

        void LoadAlatUnik()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT alatID, Nama_Alat, Merek FROM vw_Alat WHERE Stok > 0", conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (cbAlat != null)
                    {
                        cbAlat.Items.Clear();
                        cbAlat.Items.Add("--- Pilih Alat ---");

                        while (dr.Read())
                        {
                            string itemTampil = $"{dr["Nama_Alat"]} {dr["Merek"]} ({dr["alatID"]})";
                            cbAlat.Items.Add(itemTampil);
                        }
                        cbAlat.SelectedIndex = 0;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void txtCariNama_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCariNama.SelectedIndex != -1 && txtCariNama.SelectedValue != null && txtCariNama.SelectedValue is int)
            {
                idPeminjamTerpilih = (int)txtCariNama.SelectedValue;
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT NIK, Alamat FROM Peminjam WHERE PeminjamID = @ID", conn);
                        cmd.Parameters.AddWithValue("@ID", idPeminjamTerpilih);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            if (lblNamaPeminjam != null) lblNamaPeminjam.Text = dr["NIK"].ToString();
                            if (lblAlamat != null) lblAlamat.Text = dr["Alamat"].ToString();
                            
                            if (panelTransaksi != null) panelTransaksi.Enabled = true;
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        private void cbAlat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAlat == null || cbAlat.SelectedIndex <= 0 || cbAlat.Text.StartsWith("---")) return;

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
                    SqlCommand cmd = new SqlCommand("SELECT Stok FROM vw_Alat WHERE alatID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", idAlatTerpilih);
                    object res = cmd.ExecuteScalar();

                    if (lblStok != null) lblStok.Text = (res != null) ? res.ToString() : "0";
                }
                catch { if (lblStok != null) lblStok.Text = "0"; }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cbAlat.SelectedIndex <= 0 || cbAlat.Text.StartsWith("---")) { MessageBox.Show("Silakan pilih alat!"); return; }
            if (txtJumlah == null || !int.TryParse(txtJumlah.Text, out int jmlPinjam) || jmlPinjam <= 0) { MessageBox.Show("Jumlah pinjam tidak valid!"); return; }

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

                    cmdSP.Parameters.AddWithValue("@petugasID", 1);
                    cmdSP.Parameters.AddWithValue("@peminjamID", idPeminjamTerpilih);
                    cmdSP.Parameters.AddWithValue("@alatID", idAlatAsli);
                    cmdSP.Parameters.AddWithValue("@jumlah_Pinjam", jmlPinjam);

                    cmdSP.ExecuteNonQuery();

                    MessageBox.Show("Transaksi Peminjaman Berhasil Disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowData();
                    LoadAlatUnik();
                    Bersihkan();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Peringatan Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCariNama.Text)) { MessageBox.Show("Ketikkan Nama Warga terlebih dahulu!"); return; }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    SqlCommand cmdCari = new SqlCommand("sp_SearchPeminjam", conn);
                    cmdCari.CommandType = CommandType.StoredProcedure;
                    cmdCari.Parameters.AddWithValue("@Search", txtCariNama.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmdCari);
                    DataTable dtWarga = new DataTable();
                    da.Fill(dtWarga);

                    if (dtWarga.Rows.Count == 0)
                    {
                        MessageBox.Show("Warga tidak ditemukan!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Bersihkan();
                    }
                    else if (dtWarga.Rows.Count == 1)
                    {
                        DataRow dr = dtWarga.Rows[0];
                        idPeminjamTerpilih = Convert.ToInt32(dr["PeminjamID"]);

                        if (lblNamaPeminjam != null) lblNamaPeminjam.Text = dr["NIK"].ToString(); // REVISI
                        if (lblAlamat != null) lblAlamat.Text = dr["Alamat"].ToString();
                        txtCariNama.Text = dr["Nama_Peminjam"].ToString();

                        if (panelTransaksi != null) panelTransaksi.Enabled = true;
                        MessageBox.Show("Data Warga Berhasil Ditemukan!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (dgvPeminjaman != null)
                        {
                            dgvPeminjaman.DataSource = dtWarga;
                            if (dgvPeminjaman.Columns["PeminjamID"] != null)
                                dgvPeminjaman.Columns["PeminjamID"].Visible = false;
                        }

                        MessageBox.Show($"Ditemukan {dtWarga.Rows.Count} warga yang mirip!\nSilakan klik baris di tabel.", "Pilihan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (panelTransaksi != null) panelTransaksi.Enabled = false;
                    }
                }
                catch (SqlException ex) { MessageBox.Show("Error SP: " + ex.Message); }
            }
        }

        private void dgvPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPeminjaman == null || e.RowIndex < 0) return;

            DataGridViewRow row = dgvPeminjaman.Rows[e.RowIndex];

            if (dgvPeminjaman.Columns["NIK"] != null && dgvPeminjaman.DataSource != bs)
            {
                if (row.Cells["PeminjamID"].Value != null && row.Cells["PeminjamID"].Value != DBNull.Value)
                {
                    idPeminjamTerpilih = Convert.ToInt32(row.Cells["PeminjamID"].Value);

                    if (lblNamaPeminjam != null) lblNamaPeminjam.Text = row.Cells["NIK"].Value.ToString(); // REVISI
                    if (lblAlamat != null) lblAlamat.Text = row.Cells["Alamat"].Value.ToString();
                    txtCariNama.Text = row.Cells["Nama_Peminjam"].ToString();

                    if (panelTransaksi != null) panelTransaksi.Enabled = true;
                    MessageBox.Show($"Warga '{txtCariNama.Text}' Berhasil Dipilih!", "Sukses Memilih", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ShowData();
                }
            }
            else
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value != DBNull.Value)
                {
                    idTerpilih = row.Cells[0].Value.ToString();
                    try
                    {
                        int colNama = dgvPeminjaman.Columns.Contains("Nama Peminjam") ? dgvPeminjaman.Columns["Nama Peminjam"].Index : 
                                      (dgvPeminjaman.Columns.Contains("Nama_Peminjam") ? dgvPeminjaman.Columns["Nama_Peminjam"].Index : 1);
                        if (row.Cells[colNama].Value != null)
                        {
                            int idx = txtCariNama.FindStringExact(row.Cells[colNama].Value.ToString());
                            if (idx >= 0) txtCariNama.SelectedIndex = idx;
                        }
                        
                        int colAlat = dgvPeminjaman.Columns.Contains("Nama Alat") ? dgvPeminjaman.Columns["Nama Alat"].Index : 
                                      (dgvPeminjaman.Columns.Contains("Nama_Alat") ? dgvPeminjaman.Columns["Nama_Alat"].Index : 2);
                        if (row.Cells[colAlat].Value != null)
                        {
                            string namaAlat = row.Cells[colAlat].Value.ToString();
                            for (int i = 0; i < cbAlat.Items.Count; i++)
                            {
                                if (cbAlat.Items[i].ToString().Contains(namaAlat))
                                {
                                    cbAlat.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                        
                        int colJumlah = dgvPeminjaman.Columns.Contains("Jumlah Pinjam") ? dgvPeminjaman.Columns["Jumlah Pinjam"].Index : 
                                        (dgvPeminjaman.Columns.Contains("Jumlah_Pinjam") ? dgvPeminjaman.Columns["Jumlah_Pinjam"].Index : 4);
                        if (row.Cells[colJumlah].Value != null && txtJumlah != null)
                        {
                            txtJumlah.Text = row.Cells[colJumlah].Value.ToString();
                        }
                    }
                    catch { }
                }
            }
        }

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

                        conn.InfoMessage += (sInfo, ev) =>
                        {
                            MessageBox.Show(ev.Message, "Pesan dari Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        };

                        SqlCommand cmd = new SqlCommand("sp_UpdatePeminjaman", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@peminjamanID", Convert.ToInt32(idTerpilih));
                        cmd.ExecuteNonQuery();

                        ShowData();
                        LoadAlatUnik();
                        idTerpilih = "";
                    }
                    catch (SqlException ex) { MessageBox.Show("Error Database: " + ex.Message); }
                }
            }
        }

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

        private void btnSearchTrans_Click(object sender, EventArgs e)
        {
            if (txtSearchTrans != null && string.IsNullOrWhiteSpace(txtSearchTrans.Text))
            {
                ShowData();
                return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    SqlCommand cmdCariLog = new SqlCommand("Sp_SearchPeminjaman", conn);
                    cmdCariLog.CommandType = CommandType.StoredProcedure;
                    cmdCariLog.Parameters.AddWithValue("@Search", txtSearchTrans.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmdCariLog);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bs.DataSource = dt;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error Pencarian Transaksi: " + ex.Message);
                }
            }
        }

        void Bersihkan()
        {
            if (txtCariNama != null) { txtCariNama.SelectedIndex = -1; txtCariNama.Text = "-- Pilih Peminjam --"; }
            if (txtSearchTrans != null) txtSearchTrans.Clear();
            if (lblNamaPeminjam != null) lblNamaPeminjam.Text = "-"; // REVISI
            if (lblNIKResult != null) lblNIKResult.Text = "NIK :"; // REVISI
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
        private void FormPeminjaman_Load(object sender, EventArgs e)
        {
            if (this.panel1 != null) {
                this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
            // Sengaja dimatikan agar tidak bentrok dengan ShowData() manual
            // this.peminjamTableAdapter.Fill(this.dBPeminjamanAlatDataSet1.Peminjam);
            // this.peminjamanTableAdapter.Fill(this.dBPeminjamanAlatDataSet1.Peminjaman);
        }
        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e) { }
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e) { }
        private void dgvPeminjaman_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}