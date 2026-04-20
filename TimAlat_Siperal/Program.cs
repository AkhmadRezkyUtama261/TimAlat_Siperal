using System;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    internal static class Program
    {
        /// <summary>
        /// Pintu masuk utama aplikasi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ===================================================================
            // CARA PAKAI: 
            // Pastikan hanya ada SATU baris "Application.Run" yang tidak berwarna hijau.
            // ===================================================================

            // 1. Aktifkan baris ini kalau mau buka Form Peminjaman (Transaksi)
            Application.Run(new FormPeminjaman());

            // 2. Aktifkan baris ini kalau mau buka Form Pengguna (Data Member)
           //Application.Run(new FormPengguna());

            
        }
    }
}