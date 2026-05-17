using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public partial class FormAlat : Form
    {
        Koneksi konn = new Koneksi();
        BindingSource bs = new BindingSource();
        string idAlat = "";

        public FormAlat()
        {
            InitializeComponent();
            TampilData();
        }

        private void btnTampil_Click(object sender, EventArgs e)
        {
            TampilData();
        }

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

                    txtKodeAlat.DataBindings.Clear();
                    txtNama.DataBindings.Clear();
                    txtStok.DataBindings.Clear();
                    txtMerek.DataBindings.Clear();

                    txtKodeAlat.DataBindings.Add("Text", bs, "alatID", true, DataSourceUpdateMode.Never);
                    txtNama.DataBindings.Add("Text", bs, "Nama_Alat", true, DataSourceUpdateMode.Never);
                    txtStok.DataBindings.Add("Text", bs, "Stok", true, DataSourceUpdateMode.Never);
                    txtMerek.DataBindings.Add("Text", bs, "Merek", true, DataSourceUpdateMode.Never);

                    if (dgvAlat.Columns["AdminID"] != null)
                        dgvAlat.Columns["AdminID"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Tampil Data: " + ex.Message);
                }
            }
        }
    }
}