using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TimAlat_Siperal
{
    public partial class FormLogin : Form
    {
        private readonly string koneksi = @"Data Source=LAPTOP-7SOCNODM\ANDHIKA1;Initial Catalog=DBPeminjamanAlat;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AturDesainUIOtomatis();
            TengahkanPanel();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            TengahkanPanel();
        }

        private void TengahkanPanel()
        {
            if (panel1 != null)
            {
                panel1.Location = new Point((this.ClientSize.Width - panel1.Width) / 2,
                                            (this.ClientSize.Height - panel1.Height) / 2);
            }
        }

        private void AturDesainUIOtomatis()
        {
            panel1.Anchor = AnchorStyles.None;
            this.Size = new Size(1000, 600);
            panel1.Size = new Size(800, 450);
            panel1.BackColor = Color.FromArgb(80, 255, 255, 255);
        }
    }
}