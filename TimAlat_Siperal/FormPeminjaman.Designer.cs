namespace TimAlat_Siperal
{
    partial class FormPeminjaman
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPeminjaman));
            this.label1 = new System.Windows.Forms.Label();
            this.lblNIKResult = new System.Windows.Forms.Label();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtCariNama = new System.Windows.Forms.TextBox();
            this.lblNamaPeminjam = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelTransaksi = new System.Windows.Forms.GroupBox();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.btnSimpan = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblStok = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAlat = new System.Windows.Forms.ComboBox();
            this.dgvPeminjaman = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSearchTrans = new System.Windows.Forms.Button();
            this.txtSearchTrans = new System.Windows.Forms.TextBox();
            this.btnKembalikan = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dBPeminjamanAlatDataSet1 = new TimAlat_Siperal.DBPeminjamanAlatDataSet1();
            this.peminjamanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.peminjamanTableAdapter = new TimAlat_Siperal.DBPeminjamanAlatDataSet1TableAdapters.PeminjamanTableAdapter();
            this.peminjamBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.peminjamTableAdapter = new TimAlat_Siperal.DBPeminjamanAlatDataSet1TableAdapters.PeminjamTableAdapter();
            this.groupBox1.SuspendLayout();
            this.panelTransaksi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeminjaman)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dBPeminjamanAlatDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peminjamanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.peminjamBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(20, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cari Nama :";
            // 
            // lblNIKResult
            // 
            this.lblNIKResult.AutoSize = true;
            this.lblNIKResult.BackColor = System.Drawing.Color.Transparent;
            this.lblNIKResult.Location = new System.Drawing.Point(20, 67);
            this.lblNIKResult.Name = "lblNIKResult";
            this.lblNIKResult.Size = new System.Drawing.Size(34, 16);
            this.lblNIKResult.TabIndex = 2;
            this.lblNIKResult.Text = "NIK :";
            // 
            // lblAlamat
            // 
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.BackColor = System.Drawing.Color.Transparent;
            this.lblAlamat.Location = new System.Drawing.Point(121, 104);
            this.lblAlamat.Name = "lblAlamat";
            this.lblAlamat.Size = new System.Drawing.Size(11, 16);
            this.lblAlamat.TabIndex = 2;
            this.lblAlamat.Text = "-";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(265, 140);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(99, 28);
            this.btnCari.TabIndex = 1;
            this.btnCari.Text = "Cari Warga";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtCariNama
            // 
            this.txtCariNama.Location = new System.Drawing.Point(125, 37);
            this.txtCariNama.Name = "txtCariNama";
            this.txtCariNama.Size = new System.Drawing.Size(239, 22);
            this.txtCariNama.TabIndex = 0;
            // 
            // lblNamaPeminjam
            // 
            this.lblNamaPeminjam.AutoSize = true;
            this.lblNamaPeminjam.BackColor = System.Drawing.Color.Transparent;
            this.lblNamaPeminjam.Location = new System.Drawing.Point(121, 67);
            this.lblNamaPeminjam.Name = "lblNamaPeminjam";
            this.lblNamaPeminjam.Size = new System.Drawing.Size(11, 16);
            this.lblNamaPeminjam.TabIndex = 4;
            this.lblNamaPeminjam.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(20, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = " Alamat:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblNamaPeminjam);
            this.groupBox1.Controls.Add(this.txtCariNama);
            this.groupBox1.Controls.Add(this.btnCari);
            this.groupBox1.Controls.Add(this.lblAlamat);
            this.groupBox1.Controls.Add(this.lblNIKResult);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(384, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cari data peminjam";
            // 
            // panelTransaksi
            // 
            this.panelTransaksi.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelTransaksi.Controls.Add(this.txtJumlah);
            this.panelTransaksi.Controls.Add(this.btnSimpan);
            this.panelTransaksi.Controls.Add(this.label3);
            this.panelTransaksi.Controls.Add(this.lblStok);
            this.panelTransaksi.Controls.Add(this.label4);
            this.panelTransaksi.Controls.Add(this.cbAlat);
            this.panelTransaksi.Location = new System.Drawing.Point(20, 230);
            this.panelTransaksi.Name = "panelTransaksi";
            this.panelTransaksi.Size = new System.Drawing.Size(384, 185);
            this.panelTransaksi.TabIndex = 1;
            this.panelTransaksi.TabStop = false;
            this.panelTransaksi.Text = "Detail Peminjaman.";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(140, 120);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(100, 22);
            this.txtJumlah.TabIndex = 5;
            // 
            // btnSimpan
            // 
            this.btnSimpan.Location = new System.Drawing.Point(275, 145);
            this.btnSimpan.Name = "btnSimpan";
            this.btnSimpan.Size = new System.Drawing.Size(90, 28);
            this.btnSimpan.TabIndex = 2;
            this.btnSimpan.Text = "Simpan";
            this.btnSimpan.UseVisualStyleBackColor = true;
            this.btnSimpan.Click += new System.EventHandler(this.btnSimpan_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(20, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Jumlah Pinjam :";
            // 
            // lblStok
            // 
            this.lblStok.AutoSize = true;
            this.lblStok.BackColor = System.Drawing.Color.Transparent;
            this.lblStok.Location = new System.Drawing.Point(140, 85);
            this.lblStok.Name = "lblStok";
            this.lblStok.Size = new System.Drawing.Size(14, 16);
            this.lblStok.TabIndex = 3;
            this.lblStok.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(20, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Stok Tersedia :";
            // 
            // cbAlat
            // 
            this.cbAlat.FormattingEnabled = true;
            this.cbAlat.Location = new System.Drawing.Point(23, 40);
            this.cbAlat.Name = "cbAlat";
            this.cbAlat.Size = new System.Drawing.Size(217, 24);
            this.cbAlat.TabIndex = 0;
            this.cbAlat.Text = "Pilih Alat :";
            this.cbAlat.SelectedIndexChanged += new System.EventHandler(this.cbAlat_SelectedIndexChanged);
            // 
            // dgvPeminjaman
            // 
            this.dgvPeminjaman.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvPeminjaman.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeminjaman.Location = new System.Drawing.Point(430, 35);
            this.dgvPeminjaman.Name = "dgvPeminjaman";
            this.dgvPeminjaman.RowHeadersWidth = 51;
            this.dgvPeminjaman.Size = new System.Drawing.Size(1147, 380);
            this.dgvPeminjaman.TabIndex = 2;
            this.dgvPeminjaman.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPeminjaman_CellClick);
            this.dgvPeminjaman.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPeminjaman_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnSearchTrans);
            this.panel1.Controls.Add(this.txtSearchTrans);
            this.panel1.Controls.Add(this.btnKembalikan);
            this.panel1.Controls.Add(this.btnHapus);
            this.panel1.Controls.Add(this.panelTransaksi);
            this.panel1.Controls.Add(this.dgvPeminjaman);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(411, 128);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1843, 520);
            this.panel1.TabIndex = 3;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btnSearchTrans
            // 
            this.btnSearchTrans.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSearchTrans.Location = new System.Drawing.Point(1396, 0);
            this.btnSearchTrans.Name = "btnSearchTrans";
            this.btnSearchTrans.Size = new System.Drawing.Size(105, 25);
            this.btnSearchTrans.TabIndex = 9;
            this.btnSearchTrans.Text = "CARI DATA";
            this.btnSearchTrans.UseVisualStyleBackColor = false;
            this.btnSearchTrans.Click += new System.EventHandler(this.btnSearchTrans_Click);
            // 
            // txtSearchTrans
            // 
            this.txtSearchTrans.Location = new System.Drawing.Point(827, 1);
            this.txtSearchTrans.Name = "txtSearchTrans";
            this.txtSearchTrans.Size = new System.Drawing.Size(540, 22);
            this.txtSearchTrans.TabIndex = 8;
            // 
            // btnKembalikan
            // 
            this.btnKembalikan.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnKembalikan.FlatAppearance.BorderSize = 0;
            this.btnKembalikan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKembalikan.Location = new System.Drawing.Point(430, 430);
            this.btnKembalikan.Name = "btnKembalikan";
            this.btnKembalikan.Size = new System.Drawing.Size(140, 32);
            this.btnKembalikan.TabIndex = 6;
            this.btnKembalikan.Text = "Barang Kembali";
            this.btnKembalikan.UseVisualStyleBackColor = false;
            this.btnKembalikan.Click += new System.EventHandler(this.btnKembalikan_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Location = new System.Drawing.Point(630, 430);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(120, 32);
            this.btnHapus.TabIndex = 7;
            this.btnHapus.Text = "Hapus Riwayat";
            this.btnHapus.UseVisualStyleBackColor = false;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(841, 98);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(302, 27);
            this.bindingNavigator1.TabIndex = 4;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 24);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            this.bindingNavigatorPositionItem.Click += new System.EventHandler(this.bindingNavigatorPositionItem_Click);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            this.bindingNavigatorMoveNextItem.Click += new System.EventHandler(this.bindingNavigatorMoveNextItem_Click);
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // dBPeminjamanAlatDataSet1
            // 
            this.dBPeminjamanAlatDataSet1.DataSetName = "DBPeminjamanAlatDataSet1";
            this.dBPeminjamanAlatDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // peminjamanBindingSource
            // 
            this.peminjamanBindingSource.DataMember = "Peminjaman";
            this.peminjamanBindingSource.DataSource = this.dBPeminjamanAlatDataSet1;
            // 
            // peminjamanTableAdapter
            // 
            this.peminjamanTableAdapter.ClearBeforeFill = true;
            // 
            // peminjamBindingSource
            // 
            this.peminjamBindingSource.DataMember = "Peminjam";
            this.peminjamBindingSource.DataSource = this.dBPeminjamanAlatDataSet1;
            // 
            // peminjamTableAdapter
            // 
            this.peminjamTableAdapter.ClearBeforeFill = true;
            // 
            // FormPeminjaman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 650);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.panel1);
            this.Name = "FormPeminjaman";
            this.Text = "Manajemen Transaksi Peminjaman";
            this.Load += new System.EventHandler(this.FormPeminjaman_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelTransaksi.ResumeLayout(false);
            this.panelTransaksi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeminjaman)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dBPeminjamanAlatDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peminjamanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.peminjamBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNIKResult;
        private System.Windows.Forms.Label lblAlamat;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtCariNama;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnKembalikan;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Button btnSearchTrans;
        private System.Windows.Forms.TextBox txtSearchTrans;
        private DBPeminjamanAlatDataSet1 dBPeminjamanAlatDataSet1;
        private System.Windows.Forms.BindingSource peminjamanBindingSource;
        private DBPeminjamanAlatDataSet1TableAdapters.PeminjamanTableAdapter peminjamanTableAdapter;
        private System.Windows.Forms.BindingSource peminjamBindingSource;
        private DBPeminjamanAlatDataSet1TableAdapters.PeminjamTableAdapter peminjamTableAdapter;
    }
}