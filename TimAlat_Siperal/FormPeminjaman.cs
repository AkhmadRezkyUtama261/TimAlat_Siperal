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
            ShowData();
            if (panelTransaksi != null) panelTransaksi.Enabled = false;
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
                    if (bindingNavigator1 != null) bindingNavigator1.BindingSource = bs;

                    if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                    if (lblAlamat != null) lblAlamat.DataBindings.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Memuat Tabel Peminjaman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void LoadAlatUnik() { }
        private void cbAlat_SelectedIndexChanged(object sender, EventArgs e) { }
        private void btnSimpan_Click(object sender, EventArgs e) { }
        private void btnCari_Click(object sender, EventArgs e) { }
        private void dgvPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void btnKembalikan_Click(object sender, EventArgs e) { }
        private void btnHapus_Click(object sender, EventArgs e) { }
        void Bersihkan() { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void FormPeminjaman_Load(object sender, EventArgs e) { }
        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e) { }
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e) { }
    }
}

void LoadAlatUnik()
{
    using (SqlConnection conn = konn.GetConn())
    {
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT alatID, Nama_Alat, Merek FROM Alat WHERE Stok > 0", conn);
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
        catch (Exception ex)
        {
            MessageBox.Show("Gagal Memuat Daftar Alat Unik: " + ex.Message);
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
            SqlCommand cmd = new SqlCommand("SELECT Stok FROM Alat WHERE alatID = @id", conn);
            cmd.Parameters.AddWithValue("@id", idAlatTerpilih);
            object res = cmd.ExecuteScalar();
            if (lblStok != null) lblStok.Text = (res != null) ? res.ToString() : "0";
        }
        catch { if (lblStok != null) lblStok.Text = "0"; }
    }
}

private void btnCari_Click(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtCariNama.Text))
    {
        MessageBox.Show("Ketikkan Nama Warga yang ingin dicari terlebih dahulu!"); return;
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
                MessageBox.Show("Warga dengan nama tersebut tidak ditemukan!"); Bersihkan();
            }
            else if (dtWarga.Rows.Count == 1)
            {
                DataRow dr = dtWarga.Rows[0];
                idPeminjamTerpilih = Convert.ToInt32(dr["PeminjamID"]);
                if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                if (lblAlamat != null) lblAlamat.DataBindings.Clear();

                if (lblNamaPeminjam != null) lblNamaPeminjam.Text = dr["NIK"].ToString();
                if (lblAlamat != null) lblAlamat.Text = dr["Alamat"].ToString();
                txtCariNama.Text = dr["Nama_Peminjam"].ToString();

                if (panelTransaksi != null) panelTransaksi.Enabled = true;
            }
            else
            {
                if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                if (lblAlamat != null) lblAlamat.DataBindings.Clear();
                if (dgvPeminjaman != null)
                {
                    dgvPeminjaman.DataSource = dtWarga;
                    if (dgvPeminjaman.Columns["PeminjamID"] != null) dgvPeminjaman.Columns["PeminjamID"].Visible = false;
                }
                MessageBox.Show($"Ditemukan {dtWarga.Rows.Count} warga yang mirip! Silakan klik baris warga pada tabel.");
                if (panelTransaksi != null) panelTransaksi.Enabled = false;
            }
        }
        catch (Exception ex) { MessageBox.Show("Error saat mencari data warga: " + ex.Message); }
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
            if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
            if (lblAlamat != null) lblAlamat.DataBindings.Clear();

            if (lblNamaPeminjam != null) lblNamaPeminjam.Text = row.Cells["NIK"].Value.ToString();
            if (lblAlamat != null) lblAlamat.Text = row.Cells["Alamat"].Value.ToString();
            txtCariNama.Text = row.Cells["Nama_Peminjam"].Value.ToString();

            if (panelTransaksi != null) panelTransaksi.Enabled = true;
            ShowData();
        }
    }
    else
    {
        if (row.Cells[0].Value != null && row.Cells[0].Value != DBNull.Value)
        {
            idTerpilih = row.Cells[0].Value.ToString();
            if (lblNamaPeminjam != null && row.Cells["NamaPeminjam"].Value != null)
                lblNamaPeminjam.Text = row.Cells["NamaPeminjam"].Value.ToString();
            if (lblAlamat != null && row.Cells["NamaAlat"].Value != null)
                lblAlamat.Text = row.Cells["NamaAlat"].Value.ToString();
        }
    }
}