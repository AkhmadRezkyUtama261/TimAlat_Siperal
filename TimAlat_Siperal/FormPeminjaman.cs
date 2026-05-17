using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormPeminjaman : Form
    {
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();
        string idTerpilih = "";
        int idPeminjamTerpilih = 0;

        public FormPeminjaman()
        {
            InitializeComponent();
            ShowData();
            if (panelTransaksi != null) panelTransaksi.Enabled = false;
        }

        void ShowData()
        {
            using (SqlConnection conn = konn.GetConn())
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vm_MenampilkanDaftarPeminjaman", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    bs.DataSource = dt;
                    if (dgvPeminjaman != null)
                    {
                        dgvPeminjaman.DataSource = bs;
                        dgvPeminjaman.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    if (bindingNavigator1 != null) bindingNavigator1.BindingSource = bs;

                    if (lblNamaPeminjam != null) lblNamaPeminjam.DataBindings.Clear();
                    if (lblAlamat != null) lblAlamat.DataBindings.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Memuat Tabel Peminjaman: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void LoadAlatUnik() { }
        private void cbAlat_SelectedIndexChanged(object sender, EventArgs e) { }
        private void btnSimpan_Click(object sender, EventArgs e) { }
        private void btnCari_Click(object sender, EventArgs e) { }
        private void dgvPeminjaman_CellClick(object sender, DataGridViewCellEventArgs e) { }
        private void btnKembalikan_Click(object sender, EventArgs e) { }
        private void btnHapus_Click(object sender, EventArgs e) { }
        void Bersihkan() { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void FormPeminjaman_Load(object sender, EventArgs e) { }
        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e) { }
        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e) { }
    }
}