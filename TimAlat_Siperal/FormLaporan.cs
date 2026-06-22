using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace TimAlat_Siperal
{
    public partial class FormLaporan : Form
    {
        Koneksi konn = new Koneksi();

        public FormLaporan()
        {
            InitializeComponent();
        }

        private void FormLaporan_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = konn.GetConn())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vm_MenampilkanDaftarPeminjaman", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    CrystalReport1 rpt = new CrystalReport1();
                    rpt.SetDataSource(dt);
                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat laporan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }
    }
}
