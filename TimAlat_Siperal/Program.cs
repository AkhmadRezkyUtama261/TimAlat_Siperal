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

            // UBAH BARIS DI BAWAH INI:
            // Dari Form1() menjadi FormPengguna()
            Application.Run(new FormPengguna());
        }
    }
}