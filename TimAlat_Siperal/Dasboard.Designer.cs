using System;
using System.Windows.Forms;

namespace TimAlat_Siperal
{
    partial class Dasboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnKeDataAlat = new System.Windows.Forms.Button();
            this.btnTransaksi = new System.Windows.Forms.Button();
            this.btnDataPengguna = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnDashboardUtama = new System.Windows.Forms.Button();
            this.card1 = new System.Windows.Forms.Panel();
            this.card2 = new System.Windows.Forms.Panel();
            this.card3 = new System.Windows.Forms.Panel();
            this.dgvActivity = new System.Windows.Forms.DataGridView();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Black;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogout.Location = new System.Drawing.Point(54, 625);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(137, 40);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnKeDataAlat
            // 
            this.btnKeDataAlat.BackColor = System.Drawing.Color.Black;
            this.btnKeDataAlat.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeDataAlat.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnKeDataAlat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKeDataAlat.Location = new System.Drawing.Point(50, 250);
            this.btnKeDataAlat.Name = "btnKeDataAlat";
            this.btnKeDataAlat.Size = new System.Drawing.Size(250, 50);
            this.btnKeDataAlat.TabIndex = 3;
            this.btnKeDataAlat.Text = "Data Alat";
            this.btnKeDataAlat.UseVisualStyleBackColor = false;
            this.btnKeDataAlat.Click += new System.EventHandler(this.btnKeDataAlat_Click);
            // 
            // btnTransaksi
            // 
            this.btnTransaksi.BackColor = System.Drawing.Color.Black;
            this.btnTransaksi.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransaksi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnTransaksi.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnTransaksi.Location = new System.Drawing.Point(50, 336);
            this.btnTransaksi.Name = "btnTransaksi";
            this.btnTransaksi.Size = new System.Drawing.Size(250, 50);
            this.btnTransaksi.TabIndex = 1;
            this.btnTransaksi.Text = "Transaksi Peminjaman";
            this.btnTransaksi.UseVisualStyleBackColor = false;
            this.btnTransaksi.Click += new System.EventHandler(this.btnTransaksi_Click);
            // 
            // btnDataPengguna
            // 
            this.btnDataPengguna.BackColor = System.Drawing.Color.Black;
            this.btnDataPengguna.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDataPengguna.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataPengguna.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDataPengguna.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDataPengguna.Location = new System.Drawing.Point(50, 169);
            this.btnDataPengguna.Name = "btnDataPengguna";
            this.btnDataPengguna.Size = new System.Drawing.Size(250, 50);
            this.btnDataPengguna.TabIndex = 0;
            this.btnDataPengguna.Text = "Data Pengguna";
            this.btnDataPengguna.UseVisualStyleBackColor = false;
            this.btnDataPengguna.Click += new System.EventHandler(this.btnDataPengguna_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(26, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "SIPERAL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(26, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "MANAGEMENT SYSTEM";
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(30)))), ((int)(((byte)(43)))));
            this.panelSidebar.Controls.Add(this.button1);
            this.panelSidebar.Controls.Add(this.btnDashboardUtama);
            this.panelSidebar.Controls.Add(this.label2);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnTransaksi);
            this.panelSidebar.Controls.Add(this.label1);
            this.panelSidebar.Controls.Add(this.btnKeDataAlat);
            this.panelSidebar.Controls.Add(this.btnDataPengguna);
            this.panelSidebar.Location = new System.Drawing.Point(-5, -3);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(394, 724);
            this.panelSidebar.TabIndex = 6;
            this.panelSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSidebar_Paint);
            // 
            // btnDashboardUtama
            // 
            this.btnDashboardUtama.BackColor = System.Drawing.Color.Black;
            this.btnDashboardUtama.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnDashboardUtama.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboardUtama.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDashboardUtama.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnDashboardUtama.Location = new System.Drawing.Point(50, 81);
            this.btnDashboardUtama.Name = "btnDashboardUtama";
            this.btnDashboardUtama.Size = new System.Drawing.Size(250, 50);
            this.btnDashboardUtama.TabIndex = 6;
            this.btnDashboardUtama.Text = "Dasboard Pengguna";
            this.btnDashboardUtama.UseVisualStyleBackColor = false;
            this.btnDashboardUtama.Click += new System.EventHandler(this.btnDataPengguna_Click);
            // 
            // card1
            // 
            this.card1.Location = new System.Drawing.Point(438, 192);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(210, 124);
            this.card1.TabIndex = 7;
            // 
            // card2
            // 
            this.card2.Location = new System.Drawing.Point(695, 192);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(223, 124);
            this.card2.TabIndex = 8;
            // 
            // card3
            // 
            this.card3.Location = new System.Drawing.Point(958, 192);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(200, 124);
            this.card3.TabIndex = 9;
            // 
            // dgvActivity
            // 
            this.dgvActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivity.Location = new System.Drawing.Point(438, 388);
            this.dgvActivity.Name = "dgvActivity";
            this.dgvActivity.RowHeadersWidth = 51;
            this.dgvActivity.RowTemplate.Height = 24;
            this.dgvActivity.Size = new System.Drawing.Size(720, 261);
            this.dgvActivity.TabIndex = 10;
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(423, 9);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(252, 169);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea5.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chart2.Legends.Add(legend5);
            this.chart2.Location = new System.Drawing.Point(695, 12);
            this.chart2.Name = "chart2";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chart2.Series.Add(series5);
            this.chart2.Size = new System.Drawing.Size(237, 163);
            this.chart2.TabIndex = 12;
            this.chart2.Text = "chart2";
            // 
            // chart3
            // 
            chartArea6.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart3.Legends.Add(legend6);
            this.chart3.Location = new System.Drawing.Point(938, -3);
            this.chart3.Name = "chart3";
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chart3.Series.Add(series6);
            this.chart3.Size = new System.Drawing.Size(232, 181);
            this.chart3.TabIndex = 13;
            this.chart3.Text = "chart3";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(54, 549);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "Report";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Dasboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1182, 703);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.dgvActivity);
            this.Controls.Add(this.card3);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.card1);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Dasboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormUtama";
            this.Load += new System.EventHandler(this.Dasboard_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnKeDataAlat;
        private System.Windows.Forms.Button btnTransaksi;
        private System.Windows.Forms.Button btnDataPengguna;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel card1;
        private System.Windows.Forms.Panel card2;
        private System.Windows.Forms.Panel card3;
        private System.Windows.Forms.DataGridView dgvActivity;
        private Button btnDashboardUtama;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private Button button1;
    }
}