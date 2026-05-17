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

        private void Dasboard_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.WindowState = FormWindowState.Maximized;
            panelSidebar.Dock = DockStyle.Left;

            TarikDataDariDatabase();

            int padding = 40; int lebarKartu = 280; int jarakAntarKartu = 20;
            int posisiX1 = panelSidebar.Width + padding;
            int posisiX2 = posisiX1 + lebarKartu + jarakAntarKartu;
            int posisiX3 = posisiX2 + lebarKartu + jarakAntarKartu;

            card1.Location = new Point(posisiX1, 120);
            card2.Location = new Point(posisiX2, 120);
            card3.Location = new Point(posisiX3, 120);
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
                }
                catch (Exception ex) { MessageBox.Show("Gagal hitung kartu dashboard: " + ex.Message); }
            }
        }

        private void PolesKartu(Panel pnl, string judul, string angka, Color aksen)
        {
            pnl.Controls.Clear();
            pnl.BackColor = Color.White;
            pnl.Size = new Size(280, 130); pnl.BorderStyle = BorderStyle.None;
            pnl.Controls.Add(new Label { Text = judul, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(20, 20), AutoSize = true });
            pnl.Controls.Add(new Label { Text = angka, Font = new Font("Segoe UI", 32, FontStyle.Bold), ForeColor = Color.FromArgb(44, 53, 64), Location = new Point(15, 45), AutoSize = true });
            pnl.Controls.Add(new Panel { BackColor = aksen, Size = new Size(pnl.Width, 5), Dock = DockStyle.Bottom });
        }
    }
}