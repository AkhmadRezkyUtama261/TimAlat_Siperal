using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;

namespace TimAlat_Siperal
{
    public partial class FormPengguna : Form
    {
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();
        int idPeminjam = 0;
        
        Button btnImportExcel;

        public string StatusAkses { get; set; } = "ADMIN";

        public FormPengguna()
        {
            InitializeComponent();
            
            // Inisialisasi Tombol Import Excel
            btnImportExcel = new Button();
            btnImportExcel.Text = "Import Excel";
            btnImportExcel.Size = new Size(140, 30);
            btnImportExcel.Location = new Point(416, 210); // Menempatkan di atas datagridview atau dekat form pencarian
            btnImportExcel.BackColor = Color.FromArgb(46, 204, 113); // Warna Hijau ala Excel
            btnImportExcel.ForeColor = Color.White;
            btnImportExcel.FlatStyle = FlatStyle.Flat;
            btnImportExcel.FlatAppearance.BorderSize = 0;
            btnImportExcel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnImportExcel.Cursor = Cursors.Hand;
            btnImportExcel.Click += new EventHandler(btnImportExcel_Click); // Event click import
            
            // Tambahkan ke panel2 (kotak putih tempat input) biar pasti muncul dan rapi
            if (this.panel2 != null) {
                // btnHapus posisinya di (3, 320). Kita taruh di (3, 380) biar ada jarak.
                btnImportExcel.Location = new Point(3, 380);
                this.panel2.Controls.Add(btnImportExcel);
                btnImportExcel.BringToFront();
            } else {
                this.Controls.Add(btnImportExcel);
                btnImportExcel.BringToFront();
            }

            TampilData();
        }

        private void FormPengguna_Load(object sender, EventArgs e)
        {
            // Dimatikan agar tidak bentrok dengan TampilData()
            // this.peminjamTableAdapter.Fill(this.dBPeminjamanAlatDataSet1.Peminjam);
            this.BackColor = Color.FromArgb(245, 246, 250);
            dgvPengguna.BackgroundColor = Color.White;
            dgvPengguna.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (StatusAkses == "PETUGAS")
            {
                if (btnHapus != null) btnHapus.Visible = false;
                if (btnSimpan != null) btnSimpan.Visible = true;
                if (btnUbah != null) btnUbah.Visible = true;

                this.Text = "Kelola Warga Cepat (Mode Operasional Petugas)";
            }
        }

        void TampilData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vw_Peminjam", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bs.DataSource = dt;
                    dgvPengguna.DataSource = bs;
                    bindingNavigator1.BindingSource = bs;

                    if (dgvPengguna.Columns["PeminjamID"] != null)
                    {
                        dgvPengguna.Columns["PeminjamID"].Visible = false;
                    }

                    // Tambahkan Binding agar sinkron dengan tombol next-next (Navigator)
                    txtNIK.DataBindings.Clear();
                    txtNama.DataBindings.Clear();
                    txtAlamat.DataBindings.Clear();
                    txtTelp.DataBindings.Clear();

                    txtNIK.DataBindings.Add("Text", bs, "NIK", true, DataSourceUpdateMode.Never);
                    txtNama.DataBindings.Add("Text", bs, "Nama_Peminjam", true, DataSourceUpdateMode.Never);
                    txtAlamat.DataBindings.Add("Text", bs, "Alamat", true, DataSourceUpdateMode.Never);
                    txtTelp.DataBindings.Add("Text", bs, "NomorHP", true, DataSourceUpdateMode.Never);
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

            idPeminjam = 0;
            txtNIK.Enabled = true;
            txtNIK.Focus();
        }

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

        private void dgvPengguna_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvPengguna.Rows[e.RowIndex];

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

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Pilih File Excel Data Pengguna";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    UseHeaderRow = true
                                }
                            });

                            DataTable dtExcel = result.Tables[0];
                            int suksesCount = 0;
                            int gagalCount = 0;

                            foreach (DataRow row in dtExcel.Rows)
                            {
                                string nik = row[0].ToString();
                                string nama = row[1].ToString();
                                string alamat = row[2].ToString();
                                string nohp = row[3].ToString();

                                if (string.IsNullOrWhiteSpace(nik) || string.IsNullOrWhiteSpace(nama)) continue;

                                using (SqlConnection conn = konn.GetConn())
                                {
                                    try
                                    {
                                        conn.Open();
                                        SqlCommand cmd = new SqlCommand("sp_TambahPeminjam", conn);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@NIK", nik);
                                        cmd.Parameters.AddWithValue("@NAMA", nama);
                                        cmd.Parameters.AddWithValue("@alamat", alamat);
                                        cmd.Parameters.AddWithValue("@NoHp", nohp);
                                        cmd.ExecuteNonQuery();
                                        suksesCount++;
                                    }
                                    catch (Exception)
                                    {
                                        gagalCount++;
                                    }
                                }
                            }
                            
                            MessageBox.Show($"Import Selesai!\nBerhasil: {suksesCount}\nGagal/Duplikat: {gagalCount}", "Laporan Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TampilData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat membaca file Excel. Pastikan file tidak sedang dibuka di aplikasi lain.\n\nDetail: " + ex.Message, "Error Membaca Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            TampilData();
        }
    }
}