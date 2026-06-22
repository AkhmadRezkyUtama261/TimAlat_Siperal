using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb; // Tambahan untuk Import Excel
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormAlat : Form
    {
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();
        string idAlat = "";

        private System.ComponentModel.IContainer components;
        private DataGridView dgvAlat;
        private Button btnTambah;
        private Button btnUpdate;
        private Button btnDelete;
        private TextBox txtNama;
        private Button btnTampil;
        private TextBox txtStok;
        private Label label2;
        private Label label1;
        private Label label3;
        private Panel panel1;
        private Panel panel2;
        private Button btnSearch;
        private Button btnResetSearch; // Komponen tombol reset pencarian baru
        private Label label4;
        private TextBox txtKodeAlat;
        private TextBox txtSearch;
        private ToolStripButton bindingNavigatorAddNewItem;
        private BindingNavigator bindingNavigator1;
        private ToolStripButton bindingNavigatorAddNewItem1;
        private ToolStripLabel bindingNavigatorCountItem;
        private ToolStripButton bindingNavigatorDeleteItem1;
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox bindingNavigatorPositionItem;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private TextBox txtMerek;
        private Label label5;
        private DBPeminjamanAlatDataSet dBPeminjamanAlatDataSet;
        private BindingSource alatBindingSource;
        private DBPeminjamanAlatDataSetTableAdapters.AlatTableAdapter alatTableAdapter;
        private ToolStripButton bindingNavigatorDeleteItem;
        private Button btnImportExcel; // Tambahan Tombol Import Excel

        public FormAlat()
        {
            InitializeComponent();
            
            // Inisialisasi Tombol Import Excel
            btnImportExcel = new Button();
            btnImportExcel.Text = "Import Excel";
            btnImportExcel.Size = new Size(140, 30);
            btnImportExcel.Location = new Point(450, 398); // Di sebelah tombol Refresh Data
            btnImportExcel.BackColor = Color.FromArgb(46, 204, 113); // Warna Hijau ala Excel
            btnImportExcel.ForeColor = Color.White;
            btnImportExcel.FlatStyle = FlatStyle.Flat;
            btnImportExcel.FlatAppearance.BorderSize = 0;
            btnImportExcel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnImportExcel.Cursor = Cursors.Hand;
            btnImportExcel.Click += new EventHandler(btnImportExcel_Click); // Event click import
            
            // Tambahkan ke panel1 agar tidak tertutup
            if (this.panel1 != null) {
                this.panel1.Controls.Add(btnImportExcel);
                btnImportExcel.BringToFront();
            } else {
                this.Controls.Add(btnImportExcel);
                btnImportExcel.BringToFront();
            }

            TampilData();
        }

        // ================= POIN 3: IMPORT EXCEL (Tahap 1) =================
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            ofd.Title = "Pilih File Excel Data Alat";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                BacaDataExcel(filePath);
            }
        }

        // ================= POIN 3: IMPORT EXCEL (Tahap 2) =================
        private void BacaDataExcel(string filePath)
        {
            try
            {
                string excelConnString = "";
                if (filePath.EndsWith(".xls"))
                    excelConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";
                else if (filePath.EndsWith(".xlsx"))
                    excelConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";

                using (OleDbConnection excelConn = new OleDbConnection(excelConnString))
                {
                    excelConn.Open();
                    DataTable dtSchema = excelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();

                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "]", excelConn);
                    DataTable dtExcel = new DataTable();
                    da.Fill(dtExcel);

                    MessageBox.Show("Berhasil membaca " + dtExcel.Rows.Count + " baris data dari Excel!\nMenyimpan ke database...", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    int berhasil = 0;
                    int gagal = 0;

                    using (SqlConnection conn = konn.GetConn())
                    {
                        conn.Open();
                        foreach (DataRow row in dtExcel.Rows)
                        {
                            try
                            {
                                // Pastikan urutan kolom Excel: AlatID | Nama_Alat | Stok | Merek
                                string id = row[0].ToString();
                                string nama = row[1].ToString();
                                int stok = Convert.ToInt32(row[2]);
                                string merek = row[3].ToString();

                                SqlCommand cmd = new SqlCommand("sp_TambahAlat", conn);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@AlatID", id);
                                cmd.Parameters.AddWithValue("@Nama", nama);
                                cmd.Parameters.AddWithValue("@stok", stok);
                                cmd.Parameters.AddWithValue("@Merek", merek);
                                cmd.ExecuteNonQuery();

                                berhasil++;
                            }
                            catch (Exception)
                            {
                                gagal++;
                            }
                        }
                    }

                    MessageBox.Show("Import Selesai!\nBerhasil: " + berhasil + "\nGagal/Duplikat: " + gagal, "Laporan Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TampilData(); // Refresh Datagrid
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membaca Excel: Pastikan file tidak sedang dibuka dan format kolomnya benar (AlatID, Nama_Alat, Stok, Merek).\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTampil_Click(object sender, EventArgs e)
        {
            TampilData();
        }

        // ================= POIN 2, 4, & 5: SINKRONISASI DATA VIEW & BINDING SOURCE =================
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

                    // ================= DATA BINDING SAKTI TEXTBOX =================
                    // Dihapus agar text box tidak auto-fill saat anak panah (Navigator) diklik,
                    // melainkan hanya bergeser baris biru di tabel saja. Pengisian murni dari CellClick.
                    txtKodeAlat.DataBindings.Clear();
                    txtNama.DataBindings.Clear();
                    txtStok.DataBindings.Clear();
                    txtMerek.DataBindings.Clear();

                    if (dgvAlat.Columns["AdminID"] != null)
                        dgvAlat.Columns["AdminID"].Visible = false;

                    // Buat DataGrid Alat Fill
                    dgvAlat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Tampil Data: " + ex.Message);
                }
            }
            HitungTotalStok();
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

        // ================= POIN 1: STORED PROCEDURE INSERT =================
        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKodeAlat.Text) || string.IsNullOrWhiteSpace(txtNama.Text) || string.IsNullOrWhiteSpace(txtStok.Text) || string.IsNullOrWhiteSpace(txtMerek.Text))
            {
                MessageBox.Show("Semua kolom termasuk Merek tidak boleh kosong!");
                return;
            }

            if (!int.TryParse(txtStok.Text, out int stokValid))
            {
                MessageBox.Show("Stok harus berupa angka!");
                return;
            }

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
                    TampilData();
                    Bersihkan();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("Oops! Kode Alat tersebut sudah ada di database. Silakan gunakan Kode Alat yang lain.", "Peringatan: Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan database:\n" + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
                else
                {
                    Bersihkan();
                }
            }
        }

        // ================= POIN 1: STORED PROCEDURE UPDATE =================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (idAlat == "")
            {
                MessageBox.Show("Pilih data yang mau diubah dulu!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMerek.Text))
            {
                MessageBox.Show("Kolom Merek tidak boleh kosong saat melakukan update!");
                return;
            }

            if (!int.TryParse(txtStok.Text, out int stokValid))
            {
                MessageBox.Show("Stok harus angka!");
                return;
            }

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
                    TampilData();
                    Bersihkan();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("Oops! Kode Alat tersebut sudah ada di database. Silakan gunakan Kode Alat yang lain.", "Peringatan: Data Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan database:\n" + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ================= POIN 1: STORED PROCEDURE DELETE =================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (idAlat == "")
            {
                MessageBox.Show("Pilih data yang mau dihapus!");
                return;
            }

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
                        TampilData();
                        Bersihkan();
                    }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Gagal Menghapus! Data alat ini sedang digunakan di tabel Transaksi Peminjaman.\nPastikan alat ini sudah dikembalikan sebelum dihapus.", "Peringatan: Data Sedang Dipakai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Terjadi kesalahan database:\n" + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                }
            }
        }

        // ================= REVISI POIN 3: FITUR CARI VULNERABLE + GIMMICK HACKED ALL KOLOM =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // DETEKSI DEMO: Jika textbox mengandung karakter injeksi seperti kutip tunggal (') atau double minus (--)
            string searchLower = txtSearch.Text.ToLower();
            if (txtSearch.Text.Contains("'") || txtSearch.Text.Contains("--") || searchLower.Contains(" or ") || searchLower == "or" || searchLower.StartsWith("or "))
            {
                MessageBox.Show("🚨 WARNING: SYSTEM HACKED! 🚨\n\nSQL Injection Bypass Execution Succeeded!",
                                "Security Breach Identified", MessageBoxButtons.OK, MessageBoxIcon.Error);

                using (SqlConnection conn = konn.GetConn())
                {
                    try
                    {
                        conn.Open();
                        // JURUS MANIPULASI: Paksa SQL Server mereturn baris buatan dengan nilai teks "HACKED" di semua kolom tabel
                        string queryJebol = "SELECT 'HACKED' AS alatID, 'SYSTEM HACKED' AS Nama_Alat, 'VULNERABLE' AS Merek, 0 AS Stok, 1 AS AdminID";

                        SqlDataAdapter da = new SqlDataAdapter(queryJebol, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Putus binding textbox sementara agar teks bertuliskan 'HACKED' tidak merusak memori tipe data stok (integer)
                        txtKodeAlat.DataBindings.Clear();
                        txtNama.DataBindings.Clear();
                        txtStok.DataBindings.Clear();
                        txtMerek.DataBindings.Clear();

                        bs.DataSource = dt;
                        dgvAlat.DataSource = bs;
                        return; // Langsung potong alur keluar dari fungsi
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal memunculkan efek manipulasi: " + ex.Message);
                    }
                }
            }

            // KONDISI PENCARIAN ALAT NORMAL (Tanpa Payload Injeksi)
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    string queryBocor = "SELECT * FROM vw_Alat WHERE Nama_Alat LIKE '%" + txtSearch.Text + "%'";

                    SqlDataAdapter da = new SqlDataAdapter(queryBocor, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bs.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SQL Execution Alert (Template Base): " + ex.Message, "Database Log");
                }
            }
        }

        // ================= TOMBOL PENAWAR: RESET FILTER PENCARI DAN NORMALISASI KONDISI TABEL =================
        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            TampilData(); // Memulihkan data murni view serta mengikat kembali DataBinding TextBox kiri
            MessageBox.Show("Sistem berhasil dipulihkan dari eksploitasi serangan siber.", "System Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================= RESET FORM FORMALAT =================
        private void Bersihkan()
        {
            txtKodeAlat.Text = "";
            txtKodeAlat.Enabled = true;
            txtNama.Text = "";
            txtStok.Text = "";
            txtMerek.Text = "";
            txtSearch.Text = "";
            idAlat = "";
            TampilData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void txtStok_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void dgvAlat_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // ================= BLOK DESIGNER COMPONENT =================
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlat));
            this.dgvAlat = new System.Windows.Forms.DataGridView();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.alatBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBPeminjamanAlatDataSet = new TimAlat_Siperal.DBPeminjamanAlatDataSet();
            this.btnTampil = new System.Windows.Forms.Button();
            this.txtStok = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnResetSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtMerek = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKodeAlat = new System.Windows.Forms.TextBox();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.alatTableAdapter = new TimAlat_Siperal.DBPeminjamanAlatDataSetTableAdapters.AlatTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alatBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBPeminjamanAlatDataSet)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAlat
            // 
            this.dgvAlat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAlat.BackgroundColor = System.Drawing.Color.White;
            this.dgvAlat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlat.Location = new System.Drawing.Point(670, 135);
            this.dgvAlat.Name = "dgvAlat";
            this.dgvAlat.RowHeadersWidth = 51;
            this.dgvAlat.Size = new System.Drawing.Size(650, 343);
            this.dgvAlat.TabIndex = 0;
            this.dgvAlat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dgvAlat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAlat_CellContentClick);
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(244, 231);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 33);
            this.btnTambah.TabIndex = 6;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(126, 231);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(85, 33);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(23, 231);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 33);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtNama
            // 
            this.txtNama.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alatBindingSource, "Nama_Alat", true));
            this.txtNama.Location = new System.Drawing.Point(100, 119);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(210, 22);
            this.txtNama.TabIndex = 3;
            // 
            // alatBindingSource
            // 
            this.alatBindingSource.DataMember = "Alat";
            this.alatBindingSource.DataSource = this.dBPeminjamanAlatDataSet;
            // 
            // dBPeminjamanAlatDataSet
            // 
            this.dBPeminjamanAlatDataSet.DataSetName = "DBPeminjamanAlatDataSet";
            this.dBPeminjamanAlatDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnTampil
            // 
            this.btnTampil.BackColor = System.Drawing.Color.DarkGray;
            this.btnTampil.FlatAppearance.BorderSize = 0;
            this.btnTampil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTampil.ForeColor = System.Drawing.Color.White;
            this.btnTampil.Location = new System.Drawing.Point(300, 398);
            this.btnTampil.Name = "btnTampil";
            this.btnTampil.Size = new System.Drawing.Size(140, 30);
            this.btnTampil.TabIndex = 5;
            this.btnTampil.Text = "Refresh Data";
            this.btnTampil.UseVisualStyleBackColor = false;
            this.btnTampil.Click += new System.EventHandler(this.btnTampil_Click);
            // 
            // txtStok
            // 
            this.txtStok.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alatBindingSource, "Stok", true));
            this.txtStok.Location = new System.Drawing.Point(100, 165);
            this.txtStok.Name = "txtStok";
            this.txtStok.Size = new System.Drawing.Size(210, 22);
            this.txtStok.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Stok";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nama Alat";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(459, 405);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "Total Stok: 0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.bindingNavigator1);
            this.panel1.Controls.Add(this.dgvAlat);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnResetSearch);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnTampil);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 700);
            this.panel1.TabIndex = 9;
            this.panel1.Tag = "s";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem1;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alatBindingSource, "alatID", true));
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem1;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem1,
            this.bindingNavigatorDeleteItem1});
            this.bindingNavigator1.Location = new System.Drawing.Point(670, 69);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(302, 27);
            this.bindingNavigator1.TabIndex = 14;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem1
            // 
            this.bindingNavigatorAddNewItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem1.Image")));
            this.bindingNavigatorAddNewItem1.Name = "bindingNavigatorAddNewItem1";
            this.bindingNavigatorAddNewItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem1.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorAddNewItem1.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem1
            // 
            this.bindingNavigatorDeleteItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem1.Image")));
            this.bindingNavigatorDeleteItem1.Name = "bindingNavigatorDeleteItem1";
            this.bindingNavigatorDeleteItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem1.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorDeleteItem1.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1030, 98);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "CARI DATA";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnResetSearch
            // 
            this.btnResetSearch.Location = new System.Drawing.Point(1140, 98);
            this.btnResetSearch.Name = "btnResetSearch";
            this.btnResetSearch.Size = new System.Drawing.Size(100, 26);
            this.btnResetSearch.TabIndex = 15;
            this.btnResetSearch.Text = "RESET DATA";
            this.btnResetSearch.UseVisualStyleBackColor = true;
            this.btnResetSearch.Click += new System.EventHandler(this.btnResetSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(670, 100);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(350, 22);
            this.txtSearch.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.txtMerek);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtKodeAlat);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtNama);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtStok);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnTambah);
            this.panel2.Location = new System.Drawing.Point(300, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 302);
            this.panel2.TabIndex = 11;
            // 
            // txtMerek
            // 
            this.txtMerek.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alatBindingSource, "Merek", true));
            this.txtMerek.Location = new System.Drawing.Point(100, 81);
            this.txtMerek.Name = "txtMerek";
            this.txtMerek.Size = new System.Drawing.Size(210, 22);
            this.txtMerek.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Merek";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Kode Alat";
            // 
            // txtKodeAlat
            // 
            this.txtKodeAlat.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.alatBindingSource, "alatID", true));
            this.txtKodeAlat.Location = new System.Drawing.Point(100, 30);
            this.txtKodeAlat.Name = "txtKodeAlat";
            this.txtKodeAlat.Size = new System.Drawing.Size(210, 22);
            this.txtKodeAlat.TabIndex = 9;
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 23);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 23);
            // 
            // alatTableAdapter
            // 
            this.alatTableAdapter.ClearBeforeFill = true;
            // 
            // FormAlat
            // 
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.panel1);
            this.Name = "FormAlat";
            this.Load += new System.EventHandler(this.FormAlat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alatBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBPeminjamanAlatDataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void FormAlat_Load(object sender, EventArgs e)
        {
            // Sengaja dimatikan agar tidak bentrok dengan TampilData() manual dan menyebabkan tabel null
            // this.alatTableAdapter.Fill(this.dBPeminjamanAlatDataSet.Alat);
        }
    }
}