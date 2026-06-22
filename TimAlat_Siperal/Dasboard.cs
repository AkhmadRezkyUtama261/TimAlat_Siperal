using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class Dasboard : Form
    {
        private readonly string koneksi = @"Data Source=LAPTOP-7SOCNODM\ANDHIKA1;Initial Catalog=DBPeminjamanAlat;Integrated Security=True";
        private Form formAktif = null;

        public string RoleLogin { get; set; } = "ADMIN";

        public Dasboard()
        {
            InitializeComponent();
        }

        private void panelSidebar_Paint(object sender, PaintEventArgs e)
        {
            panelSidebar.BackColor = Color.White;
            Graphics g = e.Graphics;
            using (Pen pen = new Pen(Color.FromArgb(230, 233, 237), 1))
            {
                g.DrawLine(pen, panelSidebar.Width - 1, 0, panelSidebar.Width - 1, panelSidebar.Height);
            }
        }

        private void Dasboard_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.WindowState = FormWindowState.Maximized;
            panelSidebar.Dock = DockStyle.Left;

            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.FromArgb(44, 53, 64);
            label1.Location = new Point(20, 30);
            label1.Text = "SIPERAL";
            label1.Font = new Font("Segoe UI", 16, FontStyle.Bold);

            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.DarkGray;
            label2.Location = new Point(20, 65);
            label2.Text = "MANAGEMENT SYSTEM";

            Label lblRoleInfo = new Label();
            lblRoleInfo.BackColor = Color.Transparent;
            lblRoleInfo.Location = new Point(20, 90);
            lblRoleInfo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblRoleInfo.AutoSize = true;

            if (RoleLogin == "ADMIN")
            {
                lblRoleInfo.Text = "Role: ADMIN";
                lblRoleInfo.ForeColor = Color.FromArgb(141, 19, 36);
            }
            else
            {
                lblRoleInfo.Text = "Role: PETUGAS";
                lblRoleInfo.ForeColor = Color.FromArgb(0, 150, 136);
            }

            if (panelSidebar != null)
            {
                panelSidebar.Controls.Add(lblRoleInfo);
            }

            btnDashboardUtama.Click += new EventHandler(KembaliKeDashboardAwal);

            RapikanTombolUMY(btnDashboardUtama, 130);
            RapikanTombolUMY(btnDataPengguna, 195);
            RapikanTombolUMY(btnKeDataAlat, 260);
            RapikanTombolUMY(btnTransaksi, 325);

            if (button1 != null)
            {
                RapikanTombolUMY(button1, 390); // Default ADMIN
                button1.Text = "Cetak Laporan";
                button1.Size = new Size(210, 50); // Paksa ukuran
                button1.Anchor = AnchorStyles.Top | AnchorStyles.Left; // Reset anchor
                button1.BringToFront();
            }

            if (btnLogout != null)
            {
                RapikanTombolLogoutHCI(btnLogout, panelSidebar.Height - 80);
                btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            }

            if (RoleLogin == "PETUGAS")
            {
                if (btnDataPengguna != null) btnDataPengguna.Visible = true;
                if (btnKeDataAlat != null) btnKeDataAlat.Visible = false;
                if (btnTransaksi != null) btnTransaksi.Location = new Point(20, 260);
                if (button1 != null) button1.Location = new Point(20, 325); // Naik di PETUGAS
            }

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
            dgvActivity.Width = (lebarKartu * 2) + jarakAntarKartu;
            dgvActivity.Height = 250; // Diperpendek agar muat untuk chart di bawahnya

            Control[] foundCharts1 = this.Controls.Find("chart1", true);
            if (foundCharts1.Length > 0 && foundCharts1[0] is System.Windows.Forms.DataVisualization.Charting.Chart chart1)
            {
                chart1.Location = new Point(posisiX3, 280);
                chart1.Size = new Size(lebarKartu + 40, 250);
                chart1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            Control[] foundCharts2 = this.Controls.Find("chart2", true);
            if (foundCharts2.Length > 0 && foundCharts2[0] is System.Windows.Forms.DataVisualization.Charting.Chart chart2)
            {
                chart2.Location = new Point(posisiX1, 550);
                chart2.Size = new Size((lebarKartu * 2) + jarakAntarKartu, 250);
                chart2.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            Control[] foundCharts3 = this.Controls.Find("chart3", true);
            if (foundCharts3.Length > 0 && foundCharts3[0] is System.Windows.Forms.DataVisualization.Charting.Chart chart3)
            {
                chart3.Location = new Point(posisiX3, 550);
                chart3.Size = new Size(lebarKartu + 40, 250);
                chart3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        private void BukaHalaman(Form formBawahan)
        {
            if (formAktif != null) { formAktif.Close(); }
            formAktif = formBawahan;

            card1.Visible = false;
            card2.Visible = false;
            card3.Visible = false;
            dgvActivity.Visible = false;
            Control[] foundCharts1 = this.Controls.Find("chart1", true);
            if (foundCharts1.Length > 0) foundCharts1[0].Visible = false;

            Control[] foundCharts2 = this.Controls.Find("chart2", true);
            if (foundCharts2.Length > 0) foundCharts2[0].Visible = false;

            Control[] foundCharts3 = this.Controls.Find("chart3", true);
            if (foundCharts3.Length > 0) foundCharts3[0].Visible = false;

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
            Control[] foundCharts1 = this.Controls.Find("chart1", true);
            if (foundCharts1.Length > 0) foundCharts1[0].Visible = true;

            Control[] foundCharts2 = this.Controls.Find("chart2", true);
            if (foundCharts2.Length > 0) foundCharts2[0].Visible = true;

            Control[] foundCharts3 = this.Controls.Find("chart3", true);
            if (foundCharts3.Length > 0) foundCharts3[0].Visible = true;
            TarikDataDariDatabase();
        }

        private void btnDataPengguna_Click(object sender, EventArgs e)
        {
            FormPengguna frmOrang = new FormPengguna();
            frmOrang.StatusAkses = this.RoleLogin;
            BukaHalaman(frmOrang);
        }

        private void btnTransaksi_Click(object sender, EventArgs e)
        {
            BukaHalaman(new FormPeminjaman());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLaporan fl = new FormLaporan();
            fl.Show();
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

        private void TarikDataDariDatabase()
        {
            using (SqlConnection conn = new SqlConnection(koneksi))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT ISNULL(SUM(Stok), 0) FROM Alat", conn);
                    PolesKartu(card1, "Total Alat RT", cmd1.ExecuteScalar().ToString(), Color.FromArgb(141, 19, 36));

                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Peminjaman WHERE Status='DIPINJAM'", conn);
                    PolesKartu(card2, "Peminjaman Active", cmd2.ExecuteScalar().ToString(), Color.FromArgb(0, 150, 136));

                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Peminjam", conn);
                    PolesKartu(card3, "Total Pengguna", cmd3.ExecuteScalar().ToString(), Color.FromArgb(52, 73, 94));

                    string queryTabel = "SELECT TOP 5 * FROM vm_MenampilkanDaftarPeminjaman ORDER BY [Tanggal Pinjam] DESC";

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

                    // REVISI TOTAL: Menyesuaikan 8 kolom dari View SQL Server terbaru
                    if (dgvActivity.Columns.Count >= 8)
                    {
                        dgvActivity.Columns[0].HeaderText = "ID";
                        dgvActivity.Columns[1].HeaderText = "PETUGAS"; // Isinya 'ADMIN' atau 'PETUGAS'
                        dgvActivity.Columns[2].HeaderText = "NIK PEMINJAM"; // Kolom baru yang masuk
                        dgvActivity.Columns[3].HeaderText = "NAMA PEMINJAM";
                        dgvActivity.Columns[4].HeaderText = "ALAT";
                        dgvActivity.Columns[5].HeaderText = "TANGGAL PINJAM";
                        dgvActivity.Columns[6].HeaderText = "JUMLAH";
                        dgvActivity.Columns[7].HeaderText = "STATUS";

                        dgvActivity.Columns["Jumlah"].Width = 100;
                        dgvActivity.Columns["Status"].Width = 120;
                    }

                    // --- ISI DATA CHART 1 (GRAFIK PEMINJAMAN) ---
                    Control[] foundCharts = this.Controls.Find("chart1", true);
                    if (foundCharts.Length > 0 && foundCharts[0] is System.Windows.Forms.DataVisualization.Charting.Chart chart1)
                    {
                        string queryChart = @"
                            SELECT TOP 5 Nama_Alat, SUM(Jumlah_Pinjam) as TotalPinjam 
                            FROM vm_MenampilkanDaftarPeminjaman 
                            GROUP BY Nama_Alat 
                            ORDER BY TotalPinjam DESC";

                        SqlDataAdapter daChart = new SqlDataAdapter(queryChart, conn);
                        DataTable dtChart = new DataTable();
                        
                        try {
                            daChart.Fill(dtChart);
                        } catch {
                            // Coba pakai spasi jika pakai spasi di DB
                            queryChart = @"
                            SELECT TOP 5 [Nama Alat], SUM([Jumlah Pinjam]) as TotalPinjam 
                            FROM vm_MenampilkanDaftarPeminjaman 
                            GROUP BY [Nama Alat] 
                            ORDER BY TotalPinjam DESC";
                            daChart = new SqlDataAdapter(queryChart, conn);
                            dtChart = new DataTable();
                            daChart.Fill(dtChart);
                        }

                        chart1.Series.Clear();
                        var series = chart1.Series.Add("Peminjaman");
                        series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
                        series.IsValueShownAsLabel = true;

                        chart1.Titles.Clear();
                        chart1.Titles.Add("5 Alat Sering Dipinjam");
                        chart1.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);

                        foreach (DataRow row in dtChart.Rows)
                        {
                            series.Points.AddXY(row[0].ToString(), Convert.ToInt32(row[1]));
                        }
                    }

                    // --- ISI DATA CHART 2 (COLUMN: Peminjaman per Hari) ---
                    Control[] foundChart2 = this.Controls.Find("chart2", true);
                    if (foundChart2.Length > 0 && foundChart2[0] is System.Windows.Forms.DataVisualization.Charting.Chart chart2)
                    {
                        string queryChart2 = @"
                            SELECT TOP 7 CONVERT(VARCHAR, Tanggal_Pinjam, 103) as Tanggal, COUNT(*) as TotalTransaksi
                            FROM Peminjaman 
                            GROUP BY CONVERT(VARCHAR, Tanggal_Pinjam, 103)
                            ORDER BY MIN(Tanggal_Pinjam) ASC";

                        SqlDataAdapter daChart2 = new SqlDataAdapter(queryChart2, conn);
                        DataTable dtChart2 = new DataTable();
                        try { daChart2.Fill(dtChart2); } catch { }

                        chart2.Series.Clear();
                        var series2 = chart2.Series.Add("Peminjaman Harian");
                        series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                        series2.IsValueShownAsLabel = true;
                        series2.Color = Color.FromArgb(16, 185, 129); // Hijau

                        chart2.Titles.Clear();
                        chart2.Titles.Add("Jumlah Peminjaman (7 Hari Terakhir)");
                        chart2.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);

                        foreach (DataRow row in dtChart2.Rows)
                        {
                            series2.Points.AddXY(row[0].ToString(), Convert.ToInt32(row[1]));
                        }
                    }

                    // --- ISI DATA CHART 3 (PIE: Status Peminjaman) ---
                    Control[] foundChart3 = this.Controls.Find("chart3", true);
                    if (foundChart3.Length > 0 && foundChart3[0] is System.Windows.Forms.DataVisualization.Charting.Chart chart3)
                    {
                        string queryChart3 = @"
                            SELECT Status, COUNT(*) as Total 
                            FROM Peminjaman 
                            GROUP BY Status";

                        SqlDataAdapter daChart3 = new SqlDataAdapter(queryChart3, conn);
                        DataTable dtChart3 = new DataTable();
                        try { daChart3.Fill(dtChart3); } catch { }

                        chart3.Series.Clear();
                        var series3 = chart3.Series.Add("Status");
                        series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                        series3.IsValueShownAsLabel = true;
                        
                        chart3.Titles.Clear();
                        chart3.Titles.Add("Perbandingan Status Alat");
                        chart3.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Bold);

                        foreach (DataRow row in dtChart3.Rows)
                        {
                            series3.Points.AddXY(row[0].ToString(), Convert.ToInt32(row[1]));
                        }
                    }

                    PolesTabelModern();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data dashboard!\n" + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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
            dgvActivity.DefaultCellStyle.SelectionForeColor = Color.FromArgb(141, 19, 36);
            dgvActivity.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            dgvActivity.RowTemplate.Height = 40;
            dgvActivity.RowHeadersVisible = false;
            dgvActivity.AllowUserToResizeRows = false;
            dgvActivity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActivity.ReadOnly = true;
        }

        private void RapikanTombolUMY(Button btn, int posisiY)
        {
            if (btn == null) return;
            btn.Size = new Size(210, 50);
            btn.Location = new Point(20, posisiY);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.FromArgb(64, 74, 88);
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(141, 19, 36);
            btn.MouseEnter += (s, e) => { btn.ForeColor = Color.White; };
            btn.MouseLeave += (s, e) => { btn.ForeColor = Color.FromArgb(64, 74, 88); };
        }

        private void RapikanTombolLogoutHCI(Button btn, int posisiY)
        {
            btn.Size = new Size(210, 50);
            btn.Location = new Point(20, posisiY);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.FromArgb(231, 76, 60);
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;

            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            btn.MouseEnter += (s, e) => { btn.ForeColor = Color.White; };
            btn.MouseLeave += (s, e) => { btn.ForeColor = Color.FromArgb(231, 76, 60); };
        }
    }
}