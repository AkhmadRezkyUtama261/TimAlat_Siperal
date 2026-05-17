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

            dgvActivity.Location = new Point(posisiX1, 280);
            dgvActivity.Width = (lebarKartu * 3) + (jarakAntarKartu * 2);
            dgvActivity.Height = 450;
        }

        private void TarikDataDariDatabase()
        {
            using (SqlConnection conn = new SqlConnection(koneksi))
            {
                try
                {
                    conn.Open();
                    string queryTabel = "SELECT TOP 5 * FROM vm_MenampilkanDaftarPeminjaman ORDER BY Tanggal_Pinjam DESC";
                    SqlDataAdapter da = new SqlDataAdapter(queryTabel, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvActivity.Columns.Clear();
                    dgvActivity.DataSource = dt;

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
                catch (Exception ex) { MessageBox.Show("Gagal muat tabel dashboard: " + ex.Message); }
            }
        }

        private void PolesTabelModern()
        {
            dgvActivity.BackgroundColor = Color.White;
            dgvActivity.BorderStyle = BorderStyle.None;
            dgvActivity.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvActivity.EnableHeadersVisualStyles = false;
            dgvActivity.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
            dgvActivity.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvActivity.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvActivity.ColumnHeadersHeight = 45;
            dgvActivity.RowTemplate.Height = 40;
            dgvActivity.RowHeadersVisible = false;
            dgvActivity.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvActivity.ReadOnly = true;
        }
    }
}