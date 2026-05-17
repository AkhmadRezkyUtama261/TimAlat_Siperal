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
        }

        private void BukaHalaman(Form formBawahan)
        {
            if (formAktif != null) { formAktif.Close(); }
            formAktif = formBawahan;

            formBawahan.TopLevel = false;
            formBawahan.FormBorderStyle = FormBorderStyle.None;
            formBawahan.Dock = DockStyle.Fill;

            this.Controls.Add(formBawahan);
            this.Tag = formBawahan;

            formBawahan.BringToFront();
            panelSidebar.BringToFront();
            formBawahan.Show();
        }
    }
}