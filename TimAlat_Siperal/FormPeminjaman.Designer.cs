namespace TimAlat_Siperal
{
    partial class FormPeminjaman
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.n = new System.Windows.Forms.Label();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtNIK = new System.Windows.Forms.TextBox();
            this.lblNamaPeminjam = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelTransaksi = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStok = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAlat = new System.Windows.Forms.ComboBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.dgvPeminjaman = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.panelTransaksi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeminjaman)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Masukkan NIK :";
            // 
            // n
            // 
            this.n.AutoSize = true;
            this.n.Location = new System.Drawing.Point(20, 67);
            this.n.Name = "n";
            this.n.Size = new System.Drawing.Size(50, 16);
            this.n.TabIndex = 2;
            this.n.Text = "Nama :";
            // 
            // lblAlamat
            // 
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.Location = new System.Drawing.Point(121, 104);
            this.lblAlamat.Name = "lblAlamat";
            this.lblAlamat.Size = new System.Drawing.Size(11, 16);
            this.lblAlamat.TabIndex = 2;
            this.lblAlamat.Text = "-";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(239, 161);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(99, 23);
            this.btnCari.TabIndex = 1;
            this.btnCari.Text = "Cari Warga";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtNIK
            // 
            this.txtNIK.Location = new System.Drawing.Point(125, 37);
            this.txtNIK.Name = "txtNIK";
            this.txtNIK.Size = new System.Drawing.Size(213, 22);
            this.txtNIK.TabIndex = 0;
            // 
            // lblNamaPeminjam
            // 
            this.lblNamaPeminjam.AutoSize = true;
            this.lblNamaPeminjam.Location = new System.Drawing.Point(121, 67);
            this.lblNamaPeminjam.Name = "lblNamaPeminjam";
            this.lblNamaPeminjam.Size = new System.Drawing.Size(11, 16);
            this.lblNamaPeminjam.TabIndex = 4;
            this.lblNamaPeminjam.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = " Alamat:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblNamaPeminjam);
            this.groupBox1.Controls.Add(this.txtNIK);
            this.groupBox1.Controls.Add(this.btnCari);
            this.groupBox1.Controls.Add(this.lblAlamat);
            this.groupBox1.Controls.Add(this.n);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(84, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 190);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cari data peminjam";
            // 
            // panelTransaksi
            // 
            this.panelTransaksi.Controls.Add(this.txtJumlah);
            this.panelTransaksi.Controls.Add(this.btnSimpan);
            this.panelTransaksi.Controls.Add(this.label3);
            this.panelTransaksi.Controls.Add(this.lblStok);
            this.panelTransaksi.Controls.Add(this.label4);
            this.panelTransaksi.Controls.Add(this.cbAlat);
            this.panelTransaksi.Location = new System.Drawing.Point(84, 257);
            this.panelTransaksi.Name = "panelTransaksi";
            this.panelTransaksi.Size = new System.Drawing.Size(378, 181);
            this.panelTransaksi.TabIndex = 1;
            this.panelTransaksi.TabStop = false;
            this.panelTransaksi.Text = "Detail Peminjaman.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Jumlah Pinjam :";
            // 
            // lblStok
            // 
            this.lblStok.AutoSize = true;
            this.lblStok.Location = new System.Drawing.Point(161, 89);
            this.lblStok.Name = "lblStok";
            this.lblStok.Size = new System.Drawing.Size(14, 16);
            this.lblStok.TabIndex = 3;
            this.lblStok.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Stok Tersedia :";
            // 
            // cbAlat
            // 
            this.cbAlat.FormattingEnabled = true;
            this.cbAlat.Location = new System.Drawing.Point(23, 48);
            this.cbAlat.Name = "cbAlat";
            this.cbAlat.Size = new System.Drawing.Size(121, 24);
            this.cbAlat.TabIndex = 0;
            this.cbAlat.Text = "Pilih Alat :";
            this.cbAlat.SelectedIndexChanged += new System.EventHandler(this.cbAlat_SelectedIndexChanged);
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(295, 152);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(75, 23);
            this.btnSimpan.TabIndex = 2;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(183, 120);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(100, 22);
            this.txtJumlah.TabIndex = 5;
            // 
            // dgvPeminjaman
            // 
            this.dgvPeminjaman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeminjaman.Location = new System.Drawing.Point(494, 93);
            this.dgvPeminjaman.Name = "dgvPeminjaman";
            this.dgvPeminjaman.RowHeadersWidth = 51;
            this.dgvPeminjaman.RowTemplate.Height = 24;
            this.dgvPeminjaman.Size = new System.Drawing.Size(650, 236);
            this.dgvPeminjaman.TabIndex = 2;
            // 
            // FormPeminjaman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 450);
            this.Controls.Add(this.dgvPeminjaman);
            this.Controls.Add(this.panelTransaksi);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormPeminjaman";
            this.Text = "FormPeminjaman";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelTransaksi.ResumeLayout(false);
            this.panelTransaksi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeminjaman)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label n;
        private System.Windows.Forms.Label lblAlamat;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtNIK;
        private System.Windows.Forms.Label lblNamaPeminjam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox panelTransaksi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStok;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbAlat;
        private System.Windows.Forms.Button btnSimpan;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.DataGridView dgvPeminjaman;
    }
}