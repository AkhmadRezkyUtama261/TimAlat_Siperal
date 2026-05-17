using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class Dasboard : Form
    {
        // ================= KONEKSI DATABASE =================
        private readonly string koneksi = @"Data Source=LAPTOP-7SOCNODM\ANDHIKA1;Initial Catalog=DBPeminjamanAlat;Integrated Security=True";

        private Form formAktif = null;

        // PROPERTY ROLE (ADMIN / PETUGAS)
        public string RoleLogin { get; set; } = "ADMIN";

        public Dasboard()
        {
            InitializeComponent();
        }

        // ================= SIDEBAR POLOS (TEMA WEBSITE UMY PUTIH BERSIH) =================
        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {
            // Set background panel menjadi putih bersih polos
            panelSidebar.BackColor = Color.White;

            // Menggambar garis batas (border) abu-abu tipis di sebelah kanan sidebar sesuai prinsip HCI Visual Closure
            Graphics g = e.Graphics;
            using (Pen pen = new Pen(Color.FromArgb(230, 233, 237), 1))
            {
                g.DrawLine(pen, panelSidebar.Width - 1, 0, panelSidebar.Width - 1, panelSidebar.Height);
            }
        }

        // ================= AUTO-LAYOUT, LOAD DATA & ROLE CONTROL =================
        private void Dasboard_Load(object sender, EventArgs e)
        {
            // Latar belakang aplikasi abu-abu sangat muda (Light Gray) agar area kerja terasa luas dan bersih
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.WindowState = FormWindowState.Maximized;
            panelSidebar.Dock = DockStyle.Left;

            // Judul Aplikasi disesuaikan warnanya menjadi Abu-Abu Gelap agar kontras di atas background putih
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.FromArgb(44, 53, 64);
            label1.Location = new Point(20, 30);
            label1.Text = "SIPERAL";
            label1.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.DarkGray;
            label2.Location = new Point(20, 65);
            label2.Text = "MANAGEMENT SYSTEM";

            // ================= REVISI: GENERATE LABEL INDIKATOR ROLE USER SECARA DINAMIS =================
            Label lblRoleInfo = new Label();
            lblRoleInfo.BackColor = Color.Transparent;
            lblRoleInfo.Location = new Point(20, 90); // Posisi vertikal ideal di bawah label2
            lblRoleInfo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblRoleInfo.AutoSize = true;

            if (RoleLogin == "ADMIN")
            {
                lblRoleInfo.Text = "Role: ADMIN";
                lblRoleInfo.ForeColor = Color.FromArgb(141, 19, 36); // Warna Maroon Khas UMY
            }
            else
            {
                lblRoleInfo.Text = "Role: PETUGAS";
                lblRoleInfo.ForeColor = Color.FromArgb(0, 150, 136); // Warna Teal Hijau Muhammadiyah
            }

            // Daftarkan label baru ke kontrol panel sidebar kiri
            if (panelSidebar != null)
            {
                panelSidebar.Controls.Add(lblRoleInfo);
            }

            // Hubungkan tombol utama untuk kembali ke tampilan dasar dashboard
            btnDashboardUtama.Click += new EventHandler(KembaliKeDashboardAwal);

            // Susun Ulang Posisi & Ubah Gaya Tombol Navigasi Umum (Warna Teks Abu-Abu Gelap, Hover Maroon UMY)
            RapikanTombolUMY(btnDashboardUtama, 130);
            RapikanTombolUMY(btnDataPengguna, 195);
            RapikanTombolUMY(btnKeDataAlat, 260);
            RapikanTombolUMY(btnTransaksi, 325);

            // FIX: Menggunakan fungsi terpisah khusus Logout agar tidak tabrakan warna (HCI Error Prevention)
            if (btnLogout != null)
            {
                RapikanTombolLogoutHCI(btnLogout, panelSidebar.Height - 80);
                btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            }

            // ================= LOGIKA HAK AKSES ROLE (BUKA AKSES PENGGUNA UNTUK PETUGAS) =================
            if (RoleLogin == "PETUGAS")
            {
                // Tombol Data Pengguna tetap AKTIF/TRUE agar petugas bisa mengelola data warga secara pop-up aman
                if (btnDataPengguna != null) btnDataPengguna.Visible = true;

                // Data Alat murni dimatikan karena hak akses logistik ada di Admin
                if (btnKeDataAlat != null) btnKeDataAlat.Visible = false;

                // Geser posisi menu Transaksi ke atas agar tata letak sidebar rapat dan konsisten (Hukum Gestalt)
                if (btnTransaksi != null) btnTransaksi.Location = new Point(20, 260);
            }

            // Tarik data riwayat real-time dari SQL Server
            TarikDataDariDatabase();

            int padding = 40;
            int lebarKartu = 280;
            int jarakAntarKartu = 20;

            int posisiX1 = panelSidebar.Width + padding;
            int posisiX2 = posisiX1 + lebarKartu + jarakAntarKartu;
            int posisiX3 = posisiX2 + lebarKartu + jarakAntarKartu;

            card1.Location = new Point(posisiX1, 120);
            card2.Location = new Point(posisiX2, 120);
            card3.Location = new Point(posisiX3, 120);

            dgvActivity.Location = new Point(posisiX1, 280);
            dgvActivity.Width = (lebarKartu * 3) + (jarakAntarKartu * 2);
            dgvActivity.Height = 450;
        }

        // ================= JURUS SPA (SINGLE PAGE APPLICATION) =================
        private void BukaHalaman(Form formBawahan)
        {
            if (formAktif != null) { formAktif.Close(); }
            formAktif = formBawahan;

            card1.Visible = false;
            card2.Visible = false;
            card3.Visible = false;
            dgvActivity.Visible = false;

            formBawahan.TopLevel = false;
            formBawahan.FormBorderStyle = FormBorderStyle.None;
            formBawahan.Dock = DockStyle.Fill;

            this.Controls.Add(formBawahan);
            this.Tag = formBawahan;

            formBawahan.BringToFront();
            panelSidebar.BringToFront();

            formBawahan.Show();
        }

        private void KembaliKeDashboardAwal(object sender, EventArgs e)
        {
            if (formAktif != null) { formAktif.Close(); formAktif = null; }
            card1.Visible = true; card2.Visible = true; card3.Visible = true; dgvActivity.Visible = true;
            TarikDataDariDatabase();
        }

        // ================= NAVIGASI BAR SIDEBAR (SINKRONISASI PARAMETER ROLE) =================
        private void btnDataPengguna_Click(object sender, EventArgs e)
        {
            FormPengguna frmOrang = new FormPengguna();

            // Mengirimkan data identitas hak akses yang aktif ke FormPengguna
            frmOrang.StatusAkses = this.RoleLogin;

            BukaHalaman(frmOrang);
        }

        private void btnTransaksi_Click(object sender, EventArgs e)
        {
            BukaHalaman(new FormPeminjaman());
        }

        private void btnKeDataAlat_Click(object sender, EventArgs e)
        {
            BukaHalaman(new FormAlat());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new FormLogin().Show();
            this.Hide();
        }

        // ================= FUNGSI ADO.NET =================
        private void TarikDataDariDatabase()
        {
            using (SqlConnection conn = new SqlConnection(koneksi))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT ISNULL(SUM(Stok), 0) FROM Alat", conn);
                    PolesKartu(card1, "Total Alat RT", cmd1.ExecuteScalar().ToString(), Color.FromArgb(141, 19, 36)); // Aksen Maroon UMY

                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Peminjaman WHERE Status='DIPINJAM'", conn);
                    PolesKartu(card2, "Peminjaman Active", cmd2.ExecuteScalar().ToString(), Color.FromArgb(0, 150, 136)); // Aksen Teal Hijau Muhammadiyah

                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Peminjam", conn);
                    PolesKartu(card3, "Total Pengguna", cmd3.ExecuteScalar().ToString(), Color.FromArgb(52, 73, 94)); // Aksen Slate Gray

                    // FIX AMAN: Kita panggil pakai SELECT * TOP 5 saja dari View 
                    // Nama Header di DataGridView akan kita poles manual lewat kode di bawah agar tidak crash DataMember!
                    string queryTabel = "SELECT TOP 5 * FROM vm_MenampilkanDaftarPeminjaman ORDER BY [Tanggal Pinjam] DESC";

                    // NB: Jika SQL mendeteksi error nama kolom Tanggal Pinjam, otomatis fallback ke query alternatif tanpa spasi:
                    try
                    {
                        SqlDataAdapter daCheck = new SqlDataAdapter(queryTabel, conn);
                        DataTable dtCheck = new DataTable();
                        daCheck.Fill(dtCheck);
                    }
                    catch
                    {
                        queryTabel = "SELECT TOP 5 * FROM vm_MenampilkanDaftarPeminjaman ORDER BY Tanggal_Pinjam DESC";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(queryTabel, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvActivity.Columns.Clear();
                    dgvActivity.DataSource = dt;

                    // Poles judul header secara dinamis berdasarkan indeks urutan kolom (Bebas Error Spasi/Underscore)
                    if (dgvActivity.Columns.Count >= 7)
                    {
                        dgvActivity.Columns[0].HeaderText = "ID";
                        dgvActivity.Columns[1].HeaderText = "PETUGAS";
                        dgvActivity.Columns[2].HeaderText = "NAMA PEMINJAM";
                        dgvActivity.Columns[3].HeaderText = "ALAT";
                        dgvActivity.Columns[4].HeaderText = "TANGGAL PINJAM";
                        dgvActivity.Columns[5].HeaderText = "JUMLAH";
                        dgvActivity.Columns[6].HeaderText = "STATUS";
                    }

                    PolesTabelModern();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data dashboard!\n" + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================= FUNGSI POLES KARTU & TABEL =================
        private void PolesKartu(Panel pnl, string judul, string angka, Color aksen)
        {
            pnl.Controls.Clear();
            pnl.BackColor = Color.White;
            pnl.Size = new Size(280, 130);
            pnl.BorderStyle = BorderStyle.None;

            pnl.Controls.Add(new Label { Text = judul, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(20, 20), AutoSize = true });
            pnl.Controls.Add(new Label { Text = angka, Font = new Font("Segoe UI", 32, FontStyle.Bold), ForeColor = Color.FromArgb(44, 53, 64), Location = new Point(15, 45), AutoSize = true });
            pnl.Controls.Add(new Panel { BackColor = aksen, Size = new Size(pnl.Width, 5), Dock = DockStyle.Bottom });
        }

        private void PolesTabelModern()
        {
            dgvActivity.BackgroundColor = Color.White;
            dgvActivity.BorderStyle = BorderStyle.None;
            dgvActivity.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvActivity.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvActivity.EnableHeadersVisualStyles = false;
            dgvActivity.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
            dgvActivity.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvActivity.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvActivity.ColumnHeadersHeight = 45;
            dgvActivity.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 244, 248);
            dgvActivity.DefaultCellStyle.SelectionForeColor = Color.FromArgb(141, 19, 36); // Highlight baris terseleksi sewarna Maroon UMY
            dgvActivity.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvActivity.RowTemplate.Height = 40;
            dgvActivity.RowHeadersVisible = false;
            dgvActivity.AllowUserToResizeRows = false;
            dgvActivity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActivity.ReadOnly = true;
        }

        // ================= STYLE TOMBOL NAVIGASI BERDASARKAN TEMA KAMPUS UMY =================
        private void RapikanTombolUMY(Button btn, int posisiY)
        {
            if (btn == null) return;
            btn.Size = new Size(210, 50);
            btn.Location = new Point(20, posisiY);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.FromArgb(64, 74, 88); // Teks warna gelap agar terbaca jelas di atas putih
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            // JURUS HOVER SAKTI: Saat kursor masuk, background menjadi Maroon UMY dan teks menjadi Putih murni!
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(141, 19, 36);
            btn.MouseEnter += (s, e) => { btn.ForeColor = Color.White; };
            btn.MouseLeave += (s, e) => { btn.ForeColor = Color.FromArgb(64, 74, 88); };
        }

        // ================= STYLE KHUSUS TOMBOL LOGOUT (HCI USABILITY) =================
        private void RapikanTombolLogoutHCI(Button btn, int posisiY)
        {
            btn.Size = new Size(210, 50);
            btn.Location = new Point(20, posisiY);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.FromArgb(231, 76, 60); // Warna teks murni merah tegas (Awal)
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            // EFEK HOVER RESPONSIF: Pas disorot background jadi Merah Solid, teks berubah jadi Putih Bersih!
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            btn.MouseEnter += (s, e) => { btn.ForeColor = Color.White; };
            btn.MouseLeave += (s, e) => { btn.ForeColor = Color.FromArgb(231, 76, 60); };
        }
    }
}