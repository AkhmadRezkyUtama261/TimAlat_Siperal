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
        string koneksi = @"Data Source=EKYYY\REZKY;Initial Catalog= DBPeminjamanAlat;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Cek input kosong
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Silakan isi Username dan Password terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Menggunakan database DBPeminjamanAlat
            string koneksi = @"Data Source=EKYYY\REZKY;Initial Catalog= DBPeminjamanAlat;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(koneksi))
            {
                try
                {
                    conn.Open();
                    // Sesuaikan query dengan nama tabel dan kolom di database baru kamu
                    string query = "SELECT COUNT(*) FROM Admin WHERE username=@user AND password=@pass";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        MessageBox.Show("Login Berhasil! Selamat datang di Sistem Peminjaman Alat.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Lanjutkan ke Form Utama setelah login sukses
                    }
                    else
                    {
                        MessageBox.Show("Username atau Password salah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal terhubung ke database: " + ex.Message, "Error Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
