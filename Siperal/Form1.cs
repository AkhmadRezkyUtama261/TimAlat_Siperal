using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Siperal
{
    public partial class Form1 : Form
    {
        string koneksi = @"Data Source=EKYYY\REZKY;Initial Catalog=Siperal;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Cek apakah kolom kosong
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Isi dulu username dan password-nya!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pakai nama server laptop kamu yang tadi
            string koneksi = @"Data Source=EKYYY\REZKY;Initial Catalog=Siperal;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(koneksi))
            {
                try
                {
                    conn.Open();
                    // Query untuk mencocokkan data Admin_1
                    string query = "SELECT COUNT(*) FROM Admin WHERE username=@user AND password=@pass";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        MessageBox.Show("Login Berhasil! Selamat datang Admin.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Di sini nanti kita panggil Form Utama
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password salah! Coba cek lagi.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ada masalah koneksi: " + ex.Message);
                }
            }
        }
    }
}
