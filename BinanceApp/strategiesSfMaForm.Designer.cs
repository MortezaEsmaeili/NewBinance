
namespace BinanceApp
{
    partial class strategiesSfMaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(strategiesSfMaForm));
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.radDropDownList4 = new Telerik.WinControls.UI.RadDropDownList();
            this.price_LB = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_MA5_1min = new System.Windows.Forms.CheckBox();
            this.chb_MA20_1min = new System.Windows.Forms.CheckBox();
            this.chb_Ma20_5min = new System.Windows.Forms.CheckBox();
            this.chb_Ma5_5min = new System.Windows.Forms.CheckBox();
            this.chb_Ma20_15min = new System.Windows.Forms.CheckBox();
            this.chb_Ma5_15min = new System.Windows.Forms.CheckBox();
            this.chb_Ma20_30min = new System.Windows.Forms.CheckBox();
            this.chb_Ma5_30min = new System.Windows.Forms.CheckBox();
            this.chb_Ma20_1h = new System.Windows.Forms.CheckBox();
            this.chb_Ma5_1hour = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).BeginInit();
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
            this.radChartView1.Size = new System.Drawing.Size(971, 495);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Location = new System.Drawing.Point(105, 10);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(200, 28);
            this.radDropDownList1.TabIndex = 14;
            this.radDropDownList1.Text = "radDropDownList3";
            // 
            // radDropDownList4
            // 
            this.radDropDownList4.DefaultItemsCountInDropDown = 7;
            this.radDropDownList4.DropDownAnimationEnabled = true;
            this.radDropDownList4.Location = new System.Drawing.Point(424, 10);
            this.radDropDownList4.Name = "radDropDownList4";
            this.radDropDownList4.Size = new System.Drawing.Size(199, 28);
            this.radDropDownList4.TabIndex = 18;
            this.radDropDownList4.Text = "radDropDownList4";
            // 
            // price_LB
            // 
            this.price_LB.AutoSize = true;
            this.price_LB.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.price_LB.ForeColor = System.Drawing.Color.Lime;
            this.price_LB.Location = new System.Drawing.Point(663, 7);
            this.price_LB.Name = "price_LB";
            this.price_LB.Size = new System.Drawing.Size(58, 28);
            this.price_LB.TabIndex = 19;
            this.price_LB.Text = "Price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(356, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 20;
            this.label1.Text = "Interval:";
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
            // chb_MA5_1min
            // 
            this.chb_MA5_1min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_MA5_1min.AutoSize = true;
            this.chb_MA5_1min.Checked = true;
            this.chb_MA5_1min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_MA5_1min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_MA5_1min.ForeColor = System.Drawing.Color.White;
            this.chb_MA5_1min.Location = new System.Drawing.Point(12, 30);
            this.chb_MA5_1min.Name = "chb_MA5_1min";
            this.chb_MA5_1min.Size = new System.Drawing.Size(107, 23);
            this.chb_MA5_1min.TabIndex = 22;
            this.chb_MA5_1min.Text = "MA50 1Min";
            this.chb_MA5_1min.UseVisualStyleBackColor = true;
            this.chb_MA5_1min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_MA20_1min
            // 
            this.chb_MA20_1min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_MA20_1min.AutoSize = true;
            this.chb_MA20_1min.Checked = true;
            this.chb_MA20_1min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_MA20_1min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_MA20_1min.ForeColor = System.Drawing.Color.White;
            this.chb_MA20_1min.Location = new System.Drawing.Point(12, 59);
            this.chb_MA20_1min.Name = "chb_MA20_1min";
            this.chb_MA20_1min.Size = new System.Drawing.Size(107, 23);
            this.chb_MA20_1min.TabIndex = 23;
            this.chb_MA20_1min.Text = "MA20 1Min";
            this.chb_MA20_1min.UseVisualStyleBackColor = true;
            this.chb_MA20_1min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma20_5min
            // 
            this.chb_Ma20_5min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma20_5min.AutoSize = true;
            this.chb_Ma20_5min.Checked = true;
            this.chb_Ma20_5min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma20_5min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma20_5min.ForeColor = System.Drawing.Color.White;
            this.chb_Ma20_5min.Location = new System.Drawing.Point(12, 117);
            this.chb_Ma20_5min.Name = "chb_Ma20_5min";
            this.chb_Ma20_5min.Size = new System.Drawing.Size(107, 23);
            this.chb_Ma20_5min.TabIndex = 25;
            this.chb_Ma20_5min.Text = "MA20 5Min";
            this.chb_Ma20_5min.UseVisualStyleBackColor = true;
            this.chb_Ma20_5min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma5_5min
            // 
            this.chb_Ma5_5min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma5_5min.AutoSize = true;
            this.chb_Ma5_5min.Checked = true;
            this.chb_Ma5_5min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma5_5min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma5_5min.ForeColor = System.Drawing.Color.White;
            this.chb_Ma5_5min.Location = new System.Drawing.Point(12, 88);
            this.chb_Ma5_5min.Name = "chb_Ma5_5min";
            this.chb_Ma5_5min.Size = new System.Drawing.Size(107, 23);
            this.chb_Ma5_5min.TabIndex = 24;
            this.chb_Ma5_5min.Text = "MA50 5Min";
            this.chb_Ma5_5min.UseVisualStyleBackColor = true;
            this.chb_Ma5_5min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma20_15min
            // 
            this.chb_Ma20_15min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma20_15min.AutoSize = true;
            this.chb_Ma20_15min.Checked = true;
            this.chb_Ma20_15min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma20_15min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma20_15min.ForeColor = System.Drawing.Color.White;
            this.chb_Ma20_15min.Location = new System.Drawing.Point(12, 175);
            this.chb_Ma20_15min.Name = "chb_Ma20_15min";
            this.chb_Ma20_15min.Size = new System.Drawing.Size(115, 23);
            this.chb_Ma20_15min.TabIndex = 27;
            this.chb_Ma20_15min.Text = "MA20 15Min";
            this.chb_Ma20_15min.UseVisualStyleBackColor = true;
            this.chb_Ma20_15min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma5_15min
            // 
            this.chb_Ma5_15min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma5_15min.AutoSize = true;
            this.chb_Ma5_15min.Checked = true;
            this.chb_Ma5_15min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma5_15min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma5_15min.ForeColor = System.Drawing.Color.White;
            this.chb_Ma5_15min.Location = new System.Drawing.Point(12, 146);
            this.chb_Ma5_15min.Name = "chb_Ma5_15min";
            this.chb_Ma5_15min.Size = new System.Drawing.Size(115, 23);
            this.chb_Ma5_15min.TabIndex = 26;
            this.chb_Ma5_15min.Text = "MA50 15Min";
            this.chb_Ma5_15min.UseVisualStyleBackColor = true;
            this.chb_Ma5_15min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma20_30min
            // 
            this.chb_Ma20_30min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma20_30min.AutoSize = true;
            this.chb_Ma20_30min.Checked = true;
            this.chb_Ma20_30min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma20_30min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma20_30min.ForeColor = System.Drawing.Color.White;
            this.chb_Ma20_30min.Location = new System.Drawing.Point(12, 233);
            this.chb_Ma20_30min.Name = "chb_Ma20_30min";
            this.chb_Ma20_30min.Size = new System.Drawing.Size(115, 23);
            this.chb_Ma20_30min.TabIndex = 29;
            this.chb_Ma20_30min.Text = "MA20 30Min";
            this.chb_Ma20_30min.UseVisualStyleBackColor = true;
            this.chb_Ma20_30min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma5_30min
            // 
            this.chb_Ma5_30min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma5_30min.AutoSize = true;
            this.chb_Ma5_30min.Checked = true;
            this.chb_Ma5_30min.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma5_30min.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma5_30min.ForeColor = System.Drawing.Color.White;
            this.chb_Ma5_30min.Location = new System.Drawing.Point(12, 204);
            this.chb_Ma5_30min.Name = "chb_Ma5_30min";
            this.chb_Ma5_30min.Size = new System.Drawing.Size(115, 23);
            this.chb_Ma5_30min.TabIndex = 28;
            this.chb_Ma5_30min.Text = "MA50 30Min";
            this.chb_Ma5_30min.UseVisualStyleBackColor = true;
            this.chb_Ma5_30min.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma20_1h
            // 
            this.chb_Ma20_1h.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma20_1h.AutoSize = true;
            this.chb_Ma20_1h.Checked = true;
            this.chb_Ma20_1h.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma20_1h.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma20_1h.ForeColor = System.Drawing.Color.White;
            this.chb_Ma20_1h.Location = new System.Drawing.Point(12, 291);
            this.chb_Ma20_1h.Name = "chb_Ma20_1h";
            this.chb_Ma20_1h.Size = new System.Drawing.Size(116, 23);
            this.chb_Ma20_1h.TabIndex = 31;
            this.chb_Ma20_1h.Text = "MA20 1Hour";
            this.chb_Ma20_1h.UseVisualStyleBackColor = true;
            this.chb_Ma20_1h.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // chb_Ma5_1hour
            // 
            this.chb_Ma5_1hour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chb_Ma5_1hour.AutoSize = true;
            this.chb_Ma5_1hour.Checked = true;
            this.chb_Ma5_1hour.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_Ma5_1hour.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.chb_Ma5_1hour.ForeColor = System.Drawing.Color.White;
            this.chb_Ma5_1hour.Location = new System.Drawing.Point(12, 262);
            this.chb_Ma5_1hour.Name = "chb_Ma5_1hour";
            this.chb_Ma5_1hour.Size = new System.Drawing.Size(116, 23);
            this.chb_Ma5_1hour.TabIndex = 30;
            this.chb_Ma5_1hour.Text = "MA50 1Hour";
            this.chb_Ma5_1hour.UseVisualStyleBackColor = true;
            this.chb_Ma5_1hour.CheckedChanged += new System.EventHandler(this.chb_MA_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chb_MA5_1min);
            this.groupBox1.Controls.Add(this.chb_Ma20_1h);
            this.groupBox1.Controls.Add(this.chb_MA20_1min);
            this.groupBox1.Controls.Add(this.chb_Ma5_1hour);
            this.groupBox1.Controls.Add(this.chb_Ma5_5min);
            this.groupBox1.Controls.Add(this.chb_Ma20_30min);
            this.groupBox1.Controls.Add(this.chb_Ma20_5min);
            this.groupBox1.Controls.Add(this.chb_Ma5_30min);
            this.groupBox1.Controls.Add(this.chb_Ma5_15min);
            this.groupBox1.Controls.Add(this.chb_Ma20_15min);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(989, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 339);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MA";
            // 
            // strategiesSfMaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1127, 549);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.price_LB);
            this.Controls.Add(this.radDropDownList4);
            this.Controls.Add(this.radChartView1);
            this.Controls.Add(this.radDropDownList1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "strategiesSfMaForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SF Chart";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.strategiesSfForm_FormClosed);
            this.Load += new System.EventHandler(this.strategiesSfMaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).EndInit();
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
        private Telerik.WinControls.UI.RadDropDownList radDropDownList4;
        private System.Windows.Forms.Label price_LB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_MA5_1min;
        private System.Windows.Forms.CheckBox chb_MA20_1min;
        private System.Windows.Forms.CheckBox chb_Ma20_5min;
        private System.Windows.Forms.CheckBox chb_Ma5_5min;
        private System.Windows.Forms.CheckBox chb_Ma20_15min;
        private System.Windows.Forms.CheckBox chb_Ma5_15min;
        private System.Windows.Forms.CheckBox chb_Ma20_30min;
        private System.Windows.Forms.CheckBox chb_Ma5_30min;
        private System.Windows.Forms.CheckBox chb_Ma20_1h;
        private System.Windows.Forms.CheckBox chb_Ma5_1hour;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

