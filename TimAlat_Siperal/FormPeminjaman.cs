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

        private void cbAlat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAlat.SelectedIndex == -1) return;
            using (SqlConnection conn = konn.GetConn())
            {
                conn.Open();
                cmd = new SqlCommand("SELECT Stok FROM Alat WHERE Nama_Alat = @nama", conn);
                cmd.Parameters.AddWithValue("@nama", cbAlat.SelectedItem.ToString());
                object res = cmd.ExecuteScalar();
                lblStok.Text = (res != null) ? res.ToString() : "0";
            }
        }


        private void btnSimpan_Click(object sender, EventArgs e)
        {
            int jmlPinjam;
            if (!int.TryParse(txtJumlah.Text, out jmlPinjam))
            {
                MessageBox.Show("Jumlah harus angka!"); return;
            }

            // Validasi: Nggak boleh 0 atau minus
            if (jmlPinjam <= 0)
            {
                MessageBox.Show("Jumlah pinjam minimal 1, input yang benar!"); return;
            }

            int stokTersedia = int.Parse(lblStok.Text);
            if (jmlPinjam > stokTersedia)
            {
                MessageBox.Show("Stok tidak mencukupi!"); return;
            }

            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT alatID FROM Alat WHERE Nama_Alat = @nama", conn);
                    cmd.Parameters.AddWithValue("@nama", cbAlat.SelectedItem.ToString());
                    int idAlat = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand("INSERT INTO Peminjaman (petugasID, NIK, alatID, Status, Jumlah_Pinjam) VALUES (1, @nik, @id, 'DIPINJAM', @jml)", conn);
                    cmd.Parameters.AddWithValue("@nik", txtNIK.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", idAlat);
                    cmd.Parameters.AddWithValue("@jml", jmlPinjam);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("UPDATE Alat SET Stok = Stok - @jml WHERE alatID = @id", conn);
                    cmd.Parameters.AddWithValue("@jml", jmlPinjam);
                    cmd.Parameters.AddWithValue("@id", idAlat);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Berhasil Dipinjam!");
                    ShowData(); Bersihkan();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void dgvPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPeminjaman.Rows[e.RowIndex];
                idTerpilih = row.Cells["ID"].Value.ToString();
            }
        }

        private void btnKembalikan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTerpilih)) { MessageBox.Show("Klik data di tabel!"); return; }
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("SELECT alatID, Jumlah_Pinjam, Status FROM Peminjaman WHERE peminjamanID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", idTerpilih);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr["Status"].ToString() == "TERSEDIA")
                        {
                            MessageBox.Show("Sudah dikembalikan!"); dr.Close(); return;
                        }
                        int idAlat = (int)dr["alatID"];
                        int jml = Convert.ToInt32(dr["Jumlah_Pinjam"]);
                        dr.Close();

                        cmd = new SqlCommand("UPDATE Peminjaman SET Status = 'TERSEDIA' WHERE peminjamanID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", idTerpilih);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("UPDATE Alat SET Stok = Stok + @jml WHERE alatID = @id", conn);
                        cmd.Parameters.AddWithValue("@jml", jml);
                        cmd.Parameters.AddWithValue("@id", idAlat);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Barang Kembali!");
                        ShowData(); idTerpilih = "";
                    }
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTerpilih)) { MessageBox.Show("Pilih data dulu!"); return; }
            if (MessageBox.Show("Hapus data ini?", "Hapus", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM Peminjaman WHERE peminjamanID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", idTerpilih);
                    cmd.ExecuteNonQuery();
                    ShowData(); idTerpilih = "";
                }
            }
        }

        void Bersihkan()
        {
            txtNIK.Clear(); lblNamaPeminjam.Text = "-"; lblAlamat.Text = "-";
            cbAlat.SelectedIndex = -1; lblStok.Text = "0"; txtJumlah.Clear();
            panelTransaksi.Enabled = false; idTerpilih = "";
        }
    }
}
