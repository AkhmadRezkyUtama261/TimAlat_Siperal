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
            this.BackgroundImageLayout = ImageLayout.Stretch;

            panel1.Size = new Size(800, 450);
            panel1.BackColor = Color.FromArgb(80, 255, 255, 255);

            panel2.Size = new Size(320, 360);
            panel2.BackColor = Color.FromArgb(240, 255, 255, 255);
            panel1.Controls.Add(panel2);

            panel2.Location = new Point((panel1.Width - panel2.Width) / 2, (panel1.Height - panel2.Height) / 2);

            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(textBox2);

            pictureBox1.Size = new Size(100, 100);
            pictureBox1.Location = new Point((panel2.Width - pictureBox1.Width) / 2, 20);

            label1.Text = "Username";
            label1.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            label1.Location = new Point(40, 140);

            textBox1.Size = new Size(240, 30);
            textBox1.Location = new Point(40, 160);
            textBox1.Font = new Font("Segoe UI", 11, FontStyle.Regular);

            label2.Text = "Password";
            label2.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            label2.Location = new Point(40, 200);

            textBox2.Size = new Size(240, 30);
            textBox2.Location = new Point(40, 220);
            textBox2.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            textBox2.UseSystemPasswordChar = true;

            Button btnLoginBaru = new Button();
            btnLoginBaru.Text = "MASUK";
            btnLoginBaru.Size = new Size(240, 40);
            btnLoginBaru.Location = new Point(40, 280);
            btnLoginBaru.BackColor = Color.DodgerBlue;
            btnLoginBaru.ForeColor = Color.White;
            btnLoginBaru.FlatStyle = FlatStyle.Flat;
            btnLoginBaru.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLoginBaru.Cursor = Cursors.Hand;

            panel2.Controls.Add(btnLoginBaru);
        }
    }
}