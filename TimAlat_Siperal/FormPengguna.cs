using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPengguna : Form
    {
        // KONEKSI SESUAI REVISI ARSITEKTUR KELAS 3 TIER
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

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtNIK.Text.Trim() == "" || txtNama.Text.Trim() == "")
            {
                MessageBox.Show("NIK dan Nama wajib diisi!"); return;
            }
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Peminjam (NIK, Nama_Peminjam, Alamat, NomorHP) VALUES ('" + txtNIK.Text.Trim() + "', '" + txtNama.Text.Trim() + "', '" + txtAlamat.Text.Trim() + "', '" + txtTelp.Text.Trim() + "')";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Disimpan!");
                    TampilData(); Bersihkan();
                }
                catch (Exception ex) { MessageBox.Show("Gagal Simpan: " + ex.Message); }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (txtNIK.Text.Trim() == "" || nikLama == "") return;
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Peminjam SET NIK='" + txtNIK.Text.Trim() + "', Nama_Peminjam='" + txtNama.Text.Trim() + "', Alamat='" + txtAlamat.Text.Trim() + "', NomorHP='" + txtTelp.Text.Trim() + "' WHERE NIK='" + nikLama + "'";
                    cmd = new SqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diubah!");
                    TampilData(); Bersihkan();
                }
                catch (Exception ex) { MessageBox.Show("Gagal Ubah: " + ex.Message); }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (txtNIK.Text.Trim() == "") return;
            if (MessageBox.Show("Yakin hapus data?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Peminjam WHERE NIK='" + nikLama + "'";
                        cmd = new SqlCommand(query, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Berhasil Dihapus!");
                        TampilData(); Bersihkan();
                    }
                    catch (Exception ex) { MessageBox.Show("Gagal Hapus: " + ex.Message); }
                }
            }
        }

        private void dgvPengguna_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvPengguna.Rows[e.RowIndex];
                txtNIK.Text = row.Cells["NIK"].Value.ToString();
                txtNama.Text = row.Cells["Nama_Peminjam"].Value.ToString();
                txtAlamat.Text = row.Cells["Alamat"].Value.ToString();
                txtTelp.Text = row.Cells["NomorHP"].Value.ToString();
                nikLama = row.Cells["NIK"].Value.ToString();
            }
        }

        private void FormPengguna_Load(object sender, EventArgs e) { }
    }
}