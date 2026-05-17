using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormAlat : Form
    {
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();
        string idAlat = "";

        public FormAlat()
        {
            InitializeComponent();
            TampilData();
        }

        private void btnTampil_Click(object sender, EventArgs e)
        {
            TampilData();
        }

        private void TampilData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_Alat", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bs.DataSource = dt;
                    dgvAlat.DataSource = bs;

                    if (bindingNavigator1 != null)
                        bindingNavigator1.BindingSource = bs;

                    txtKodeAlat.DataBindings.Clear();
                    txtNama.DataBindings.Clear();
                    txtStok.DataBindings.Clear();
                    txtMerek.DataBindings.Clear();

                    txtKodeAlat.DataBindings.Add("Text", bs, "alatID", true, DataSourceUpdateMode.Never);
                    txtNama.DataBindings.Add("Text", bs, "Nama_Alat", true, DataSourceUpdateMode.Never);
                    txtStok.DataBindings.Add("Text", bs, "Stok", true, DataSourceUpdateMode.Never);
                    txtMerek.DataBindings.Add("Text", bs, "Merek", true, DataSourceUpdateMode.Never);

                    if (dgvAlat.Columns["AdminID"] != null)
                        dgvAlat.Columns["AdminID"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Tampil Data: " + ex.Message);
                }
            }
        }
    }
}

private void HitungTotalStok()
{
    using (SqlConnection conn = konn.GetConn())
    {
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT SUM(Stok) FROM Alat", conn);
            object result = cmd.ExecuteScalar();

            if (label3 != null)
                label3.Text = "Total Stok: " + (result != DBNull.Value ? result.ToString() : "0");
        }
        catch (Exception)
        {
            if (label3 != null) label3.Text = "Total Stok: 0";
        }
    }
}

private void btnTambah_Click(object sender, EventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtKodeAlat.Text) || string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtStok.Text) || string.IsNullOrWhiteSpace(txtMerek.Text))
    {
        MessageBox.Show("Semua kolom termasuk Merek tidak boleh kosong!"); return;
    }
    if (!int.TryParse(txtStok.Text, out int stokValid)) { MessageBox.Show("Stok harus berupa angka!"); return; }

    using (SqlConnection conn = konn.GetConn())
    {
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_TambahAlat", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AlatID", txtKodeAlat.Text.Trim());
            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
            cmd.Parameters.AddWithValue("@stok", stokValid);
            cmd.Parameters.AddWithValue("@Merek", txtMerek.Text.Trim());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data berhasil ditambahkan via SP!");
            TampilData(); Bersihkan();
        }
        catch (SqlException ex) { MessageBox.Show(ex.Message, "Peringatan Database", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}

private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex >= 0)
    {
        DataGridViewRow row = dgvAlat.Rows[e.RowIndex];
        if (row.Cells["alatID"].Value != null && row.Cells["alatID"].Value != DBNull.Value)
        {
            idAlat = row.Cells["alatID"].Value.ToString();
            txtKodeAlat.Enabled = false;
        }
        else { Bersihkan(); }
    }
}

private void btnUpdate_Click(object sender, EventArgs e)
{
    if (idAlat == "") { MessageBox.Show("Pilih data yang mau diubah dulu!"); return; }
    if (string.IsNullOrWhiteSpace(txtMerek.Text)) { MessageBox.Show("Kolom Merek tidak boleh kosong saat melakukan update!"); return; }
    if (!int.TryParse(txtStok.Text, out int stokValid)) { MessageBox.Show("Stok harus angka!"); return; }

    using (SqlConnection conn = konn.GetConn())
    {
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateAlat", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AlatID", idAlat);
            cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
            cmd.Parameters.AddWithValue("@StokBaru", stokValid);
            cmd.Parameters.AddWithValue("@Merek", txtMerek.Text.Trim());
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data berhasil diubah via SP!");
            TampilData(); Bersihkan();
        }
        catch (SqlException ex) { MessageBox.Show(ex.Message, "Peringatan Database", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }
}

private void btnDelete_Click(object sender, EventArgs e)
{
    if (idAlat == "") { MessageBox.Show("Pilih data yang mau dihapus!"); return; }
    if (MessageBox.Show("Yakin hapus?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
    {
        using (SqlConnection conn = konn.GetConn())
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_HapusAlat", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AlatID", idAlat);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dihapus via SP!");
                TampilData(); Bersihkan();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "Peringatan Database", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
    }
}