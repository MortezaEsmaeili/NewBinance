
namespace BinanceApp
{
    partial class macdPmaMpriceForm
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
            Telerik.WinControls.UI.CartesianArea cartesianArea1 = new Telerik.WinControls.UI.CartesianArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(macdPmaMpriceForm));
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_tmf_1min = new System.Windows.Forms.CheckBox();
            this.chb_tmf_5min = new System.Windows.Forms.CheckBox();
            this.chb_tmf_30min = new System.Windows.Forms.CheckBox();
            this.chb_tmf_15min = new System.Windows.Forms.CheckBox();
            this.chb_tmf_2H = new System.Windows.Forms.CheckBox();
            this.chb_tmf_1H = new System.Windows.Forms.CheckBox();
            this.chb_tmf_1D = new System.Windows.Forms.CheckBox();
            this.chb_tmf_4H = new System.Windows.Forms.CheckBox();
            this.chb_tmf_1W = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chb_tmf_Z1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView1
            // 
            this.radChartView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radChartView1.AreaDesign = cartesianArea1;
            this.radChartView1.Location = new System.Drawing.Point(13, 44);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowGrid = false;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(971, 529);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Location = new System.Drawing.Point(105, 10);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(200, 24);
            this.radDropDownList1.TabIndex = 14;
            this.radDropDownList1.Text = "radDropDownList3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 21;
            this.label2.Text = "Coin Name:";
            // 
            // chb_tmf_1min
            // 
            this.chb_tmf_1min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_1min.AutoSize = true;
            this.chb_tmf_1min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_1min.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_1min.Location = new System.Drawing.Point(47, 34);
            this.chb_tmf_1min.Name = "chb_tmf_1min";
            this.chb_tmf_1min.Size = new System.Drawing.Size(64, 23);
            this.chb_tmf_1min.TabIndex = 22;
            this.chb_tmf_1min.Text = "1Min";
            this.chb_tmf_1min.UseVisualStyleBackColor = true;
            this.chb_tmf_1min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_5min
            // 
            this.chb_tmf_5min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_5min.AutoSize = true;
            this.chb_tmf_5min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_5min.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_5min.Location = new System.Drawing.Point(47, 86);
            this.chb_tmf_5min.Name = "chb_tmf_5min";
            this.chb_tmf_5min.Size = new System.Drawing.Size(64, 23);
            this.chb_tmf_5min.TabIndex = 23;
            this.chb_tmf_5min.Text = "5Min";
            this.chb_tmf_5min.UseVisualStyleBackColor = true;
            this.chb_tmf_5min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_30min
            // 
            this.chb_tmf_30min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_30min.AutoSize = true;
            this.chb_tmf_30min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_30min.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_30min.Location = new System.Drawing.Point(47, 190);
            this.chb_tmf_30min.Name = "chb_tmf_30min";
            this.chb_tmf_30min.Size = new System.Drawing.Size(72, 23);
            this.chb_tmf_30min.TabIndex = 25;
            this.chb_tmf_30min.Text = "30Min";
            this.chb_tmf_30min.UseVisualStyleBackColor = true;
            this.chb_tmf_30min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_15min
            // 
            this.chb_tmf_15min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_15min.AutoSize = true;
            this.chb_tmf_15min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_15min.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_15min.Location = new System.Drawing.Point(47, 138);
            this.chb_tmf_15min.Name = "chb_tmf_15min";
            this.chb_tmf_15min.Size = new System.Drawing.Size(72, 23);
            this.chb_tmf_15min.TabIndex = 24;
            this.chb_tmf_15min.Text = "15Min";
            this.chb_tmf_15min.UseVisualStyleBackColor = true;
            this.chb_tmf_15min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_2H
            // 
            this.chb_tmf_2H.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_2H.AutoSize = true;
            this.chb_tmf_2H.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_2H.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_2H.Location = new System.Drawing.Point(47, 294);
            this.chb_tmf_2H.Name = "chb_tmf_2H";
            this.chb_tmf_2H.Size = new System.Drawing.Size(50, 23);
            this.chb_tmf_2H.TabIndex = 27;
            this.chb_tmf_2H.Text = "2H";
            this.chb_tmf_2H.UseVisualStyleBackColor = true;
            this.chb_tmf_2H.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_1H
            // 
            this.chb_tmf_1H.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_1H.AutoSize = true;
            this.chb_tmf_1H.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_1H.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_1H.Location = new System.Drawing.Point(47, 242);
            this.chb_tmf_1H.Name = "chb_tmf_1H";
            this.chb_tmf_1H.Size = new System.Drawing.Size(50, 23);
            this.chb_tmf_1H.TabIndex = 26;
            this.chb_tmf_1H.Text = "1H";
            this.chb_tmf_1H.UseVisualStyleBackColor = true;
            this.chb_tmf_1H.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_1D
            // 
            this.chb_tmf_1D.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_1D.AutoSize = true;
            this.chb_tmf_1D.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_1D.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_1D.Location = new System.Drawing.Point(47, 398);
            this.chb_tmf_1D.Name = "chb_tmf_1D";
            this.chb_tmf_1D.Size = new System.Drawing.Size(49, 23);
            this.chb_tmf_1D.TabIndex = 29;
            this.chb_tmf_1D.Text = "1D";
            this.chb_tmf_1D.UseVisualStyleBackColor = true;
            this.chb_tmf_1D.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_4H
            // 
            this.chb_tmf_4H.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_4H.AutoSize = true;
            this.chb_tmf_4H.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_4H.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_4H.Location = new System.Drawing.Point(47, 346);
            this.chb_tmf_4H.Name = "chb_tmf_4H";
            this.chb_tmf_4H.Size = new System.Drawing.Size(50, 23);
            this.chb_tmf_4H.TabIndex = 28;
            this.chb_tmf_4H.Text = "4H";
            this.chb_tmf_4H.UseVisualStyleBackColor = true;
            this.chb_tmf_4H.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_tmf_1W
            // 
            this.chb_tmf_1W.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_1W.AutoSize = true;
            this.chb_tmf_1W.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_1W.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_1W.Location = new System.Drawing.Point(47, 450);
            this.chb_tmf_1W.Name = "chb_tmf_1W";
            this.chb_tmf_1W.Size = new System.Drawing.Size(53, 23);
            this.chb_tmf_1W.TabIndex = 30;
            this.chb_tmf_1W.Text = "1W";
            this.chb_tmf_1W.UseVisualStyleBackColor = true;
            this.chb_tmf_1W.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chb_tmf_Z1);
            this.groupBox1.Controls.Add(this.chb_tmf_1min);
            this.groupBox1.Controls.Add(this.chb_tmf_5min);
            this.groupBox1.Controls.Add(this.chb_tmf_1W);
            this.groupBox1.Controls.Add(this.chb_tmf_15min);
            this.groupBox1.Controls.Add(this.chb_tmf_1D);
            this.groupBox1.Controls.Add(this.chb_tmf_30min);
            this.groupBox1.Controls.Add(this.chb_tmf_4H);
            this.groupBox1.Controls.Add(this.chb_tmf_1H);
            this.groupBox1.Controls.Add(this.chb_tmf_2H);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(989, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 533);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time Frame";
            // 
            // chb_tmf_Z1
            // 
            this.chb_tmf_Z1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_tmf_Z1.AutoSize = true;
            this.chb_tmf_Z1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_tmf_Z1.ForeColor = System.Drawing.Color.White;
            this.chb_tmf_Z1.Location = new System.Drawing.Point(47, 494);
            this.chb_tmf_Z1.Name = "chb_tmf_Z1";
            this.chb_tmf_Z1.Size = new System.Drawing.Size(48, 23);
            this.chb_tmf_Z1.TabIndex = 31;
            this.chb_tmf_Z1.Text = "Z1";
            this.chb_tmf_Z1.UseVisualStyleBackColor = true;
            this.chb_tmf_Z1.CheckStateChanged += new System.EventHandler(this.chb_tmf_Z1_CheckStateChanged);
            // 
            // macdPmaMpriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1127, 583);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radChartView1);
            this.Controls.Add(this.radDropDownList1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "macdPmaMpriceForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "MACD+MA-Price";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.strategiesSfForm_FormClosed);
            this.Load += new System.EventHandler(this.strategiesSfMaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadChartView radChartView1;
        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_tmf_1min;
        private System.Windows.Forms.CheckBox chb_tmf_5min;
        private System.Windows.Forms.CheckBox chb_tmf_30min;
        private System.Windows.Forms.CheckBox chb_tmf_15min;
        private System.Windows.Forms.CheckBox chb_tmf_2H;
        private System.Windows.Forms.CheckBox chb_tmf_1H;
        private System.Windows.Forms.CheckBox chb_tmf_1D;
        private System.Windows.Forms.CheckBox chb_tmf_4H;
        private System.Windows.Forms.CheckBox chb_tmf_1W;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chb_tmf_Z1;
    }
}

