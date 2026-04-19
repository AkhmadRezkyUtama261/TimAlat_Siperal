using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Mengarahkan aplikasi untuk membuka Form Peminjaman saat pertama kali dijalankan
            Application.Run(new FormPeminjaman());
        }
    }
}



