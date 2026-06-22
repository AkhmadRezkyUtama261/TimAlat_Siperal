using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    public static class UIHelper
    {
        public static void MakeRounded(Control control, int radius)
        {
            if (control.Width == 0 || control.Height == 0) return;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            control.Region = new Region(path);
        }

        public static void ApplyModernTheme(Form form)
        {
            // Background form keseluruhan
            form.BackColor = Color.FromArgb(244, 247, 254);

            foreach (Control c in form.Controls)
            {
                ApplyToControl(c);
            }
        }

        private static void ApplyToControl(Control c)
        {
            if (c is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btn.ForeColor = Color.White;
                btn.Cursor = Cursors.Hand;
                
                string text = btn.Text.ToLower();
                if (text.Contains("tambah") || text.Contains("simpan") || text.Contains("insert"))
                    btn.BackColor = Color.FromArgb(16, 185, 129); // Emerald Green
                else if (text.Contains("ubah") || text.Contains("update") || text.Contains("edit"))
                    btn.BackColor = Color.FromArgb(245, 158, 11); // Amber Yellow
                else if (text.Contains("hapus") || text.Contains("delete"))
                    btn.BackColor = Color.FromArgb(239, 68, 68); // Red
                else if (text.Contains("cari") || text.Contains("search"))
                    btn.BackColor = Color.FromArgb(59, 130, 246); // Light Blue
                else if (text.Contains("refresh") || text.Contains("reset"))
                    btn.BackColor = Color.FromArgb(100, 116, 139); // Slate Gray
                else
                    btn.BackColor = Color.FromArgb(37, 99, 235); // Primary Blue

                // Membuat ujung tombol melengkung
                MakeRounded(btn, 10);
                btn.Resize += (s, e) => MakeRounded((Control)s, 10);
            }
            else if (c is Panel pnl)
            {
                // Jika panel berwarna putih, anggap itu sebuah "Card"
                if (pnl.BackColor == Color.White)
                {
                    MakeRounded(pnl, 15);
                    pnl.Resize += (s, e) => MakeRounded((Control)s, 15);
                }
                
                foreach (Control subC in pnl.Controls)
                {
                    ApplyToControl(subC);
                }
            }
            else if (c is DataGridView dgv)
            {
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.None;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgv.ColumnHeadersHeight = 45;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 232, 240);
                dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                dgv.RowTemplate.Height = 40;
                dgv.RowHeadersVisible = false;
                dgv.AllowUserToResizeRows = false;
                MakeRounded(dgv, 15);
                dgv.Resize += (s, e) => MakeRounded((Control)s, 15);
            }
        }
    }
}
