using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TimAlat_Siperal
{
    public partial class FormLogin : Form
    {
        // Kunci 2-Tier: Koneksi ke database
        private readonly string koneksi = @"Data Source=LAPTOP-7SOCNODM\ANDHIKA1;Initial Catalog=DBPeminjamanAlat;Integrated Security=True";

        public FormLogin()
        {
            InitializeComponent();
            CekKoneksiOtomatis();
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
            label1.BackColor = Color.Transparent;

            textBox1.Size = new Size(240, 30);
            textBox1.Location = new Point(40, 160);
            textBox1.Font = new Font("Segoe UI", 11, FontStyle.Regular);

            label2.Text = "Password";
            label2.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            label2.Location = new Point(40, 200);
            label2.BackColor = Color.Transparent;

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
            btnLoginBaru.FlatAppearance.BorderSize = 0;
            btnLoginBaru.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLoginBaru.Cursor = Cursors.Hand;

            btnLoginBaru.Click += new EventHandler(btnLogin_Click);
            panel2.Controls.Add(btnLoginBaru);
        }

        private void CekKoneksiOtomatis()
        {
            using (SqlConnection conn = new SqlConnection(koneksi))
            {
                try { conn.Open(); }
                catch (Exception ex)
                {
                    MessageBox.Show("Koneksi Database Gagal!\n\nError: " + ex.Message, "Kesalahan Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ================= MULTI-ROLE LOGIN DATABASE (FIXED) =================
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Silakan isi Username dan Password terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(koneksi))
            {
                try
                {
                    conn.Open();

                    // JURUS UNION: Memeriksa dua tabel sekaligus (Admin & Petugas) dalam satu ketukan query
                    string queryMultiRole = @"
                        SELECT 'ADMIN' AS Role FROM Admin WHERE username = @user AND password = @pass
                        UNION
                        SELECT 'PETUGAS' AS Role FROM Petugas WHERE username = @user AND password = @pass";

                    using (SqlCommand cmd = new SqlCommand(queryMultiRole, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", textBox2.Text.Trim());

                        object objResult = cmd.ExecuteScalar();

                        if (objResult != null)
                        {
                            string roleTerdeteksi = objResult.ToString();

                            MessageBox.Show($"Login Berhasil sebagai {roleTerdeteksi}!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Lempar status Role Login murni ke Dashboard utama kelompokmu
                            Dasboard utama = new Dasboard();
                            utama.RoleLogin = roleTerdeteksi;

                            utama.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Username atau Password salah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal terhubung ke database saat login: " + ex.Message, "Error Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}