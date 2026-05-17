using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPengguna : Form
    {
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();
        int idPeminjam = 0;
        public string StatusAkses { get; set; } = "ADMIN";

        public FormPengguna()
        {
            InitializeComponent();
            TampilData();
        }

        private void FormPengguna_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 246, 250);
            dgvPengguna.BackgroundColor = Color.White;
            dgvPengguna.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Tampil Data: " + ex.Message);
                }
            }
        }

        void Bersihkan() { }
        private void btnSimpan_Click(object sender, EventArgs e) { }
        private void btnUbah_Click(object sender, EventArgs e) { }
        private void btnHapus_Click(object sender, EventArgs e) { }
        private void dgvPengguna_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void btnSearch_Click_1(object sender, EventArgs e) { }
    }
}

private void FormPengguna_Load(object sender, EventArgs e)
{
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