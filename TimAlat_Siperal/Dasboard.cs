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

            label1.Text = "SIPERAL";
            label1.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(44, 53, 64);

            label2.Text = "MANAGEMENT SYSTEM";
            label2.ForeColor = Color.DarkGray;

            RapikanTombolUMY(btnDashboardUtama, 130);
            RapikanTombolUMY(btnDataPengguna, 195);
            RapikanTombolUMY(btnKeDataAlat, 260);
            RapikanTombolUMY(btnTransaksi, 325);

            if (btnLogout != null)
            {
                RapikanTombolLogoutHCI(btnLogout, panelSidebar.Height - 80);
                btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            }
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

            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            btn.MouseEnter += (s, e) => { btn.ForeColor = Color.White; };
            btn.MouseLeave += (s, e) => { btn.ForeColor = Color.FromArgb(231, 76, 60); };
        }
    }
}