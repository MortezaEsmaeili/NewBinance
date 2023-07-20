
namespace BinanceApp
{
    partial class strategiesEma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(strategiesEma));
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.price_LB = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_ema15m = new System.Windows.Forms.CheckBox();
            this.tb_Wema15m = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Wema30m = new System.Windows.Forms.TextBox();
            this.chk_ema30m = new System.Windows.Forms.CheckBox();
            this.tb_Wema1h = new System.Windows.Forms.TextBox();
            this.chk_ema1h = new System.Windows.Forms.CheckBox();
            this.tb_Wema2h = new System.Windows.Forms.TextBox();
            this.chk_ema2h = new System.Windows.Forms.CheckBox();
            this.tb_Wema4h = new System.Windows.Forms.TextBox();
            this.chk_ema4h = new System.Windows.Forms.CheckBox();
            this.tb_Wema1D = new System.Windows.Forms.TextBox();
            this.chk_ema1D = new System.Windows.Forms.CheckBox();
            this.tb_Wema1W = new System.Windows.Forms.TextBox();
            this.chk_ema1W = new System.Windows.Forms.CheckBox();
            this.tb_percent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_NumofClasses = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radio_percent = new System.Windows.Forms.RadioButton();
            this.radio_jenksFisher = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_Wema5m = new System.Windows.Forms.TextBox();
            this.chk_ema5m = new System.Windows.Forms.CheckBox();
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
            this.radChartView1.Location = new System.Drawing.Point(218, 22);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowGrid = false;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(897, 517);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Location = new System.Drawing.Point(12, 86);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(200, 28);
            this.radDropDownList1.TabIndex = 14;
            this.radDropDownList1.Text = "radDropDownList3";
            // 
            // price_LB
            // 
            this.price_LB.AutoSize = true;
            this.price_LB.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.price_LB.ForeColor = System.Drawing.Color.Lime;
            this.price_LB.Location = new System.Drawing.Point(12, 22);
            this.price_LB.Name = "price_LB";
            this.price_LB.Size = new System.Drawing.Size(58, 28);
            this.price_LB.TabIndex = 19;
            this.price_LB.Text = "Price";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 47;
            this.label2.Text = "Coin Name:";
            // 
            // chk_ema15m
            // 
            this.chk_ema15m.AutoSize = true;
            this.chk_ema15m.ForeColor = System.Drawing.Color.White;
            this.chk_ema15m.Location = new System.Drawing.Point(12, 180);
            this.chk_ema15m.Name = "chk_ema15m";
            this.chk_ema15m.Size = new System.Drawing.Size(124, 23);
            this.chk_ema15m.TabIndex = 48;
            this.chk_ema15m.Text = "EMA20_TF15m";
            this.chk_ema15m.UseVisualStyleBackColor = true;
            this.chk_ema15m.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // tb_Wema15m
            // 
            this.tb_Wema15m.Location = new System.Drawing.Point(142, 181);
            this.tb_Wema15m.Name = "tb_Wema15m";
            this.tb_Wema15m.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema15m.TabIndex = 49;
            this.tb_Wema15m.Text = "1";
            this.tb_Wema15m.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema15m.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(148, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 19);
            this.label1.TabIndex = 50;
            this.label1.Text = "Weight";
            // 
            // tb_Wema30m
            // 
            this.tb_Wema30m.Location = new System.Drawing.Point(142, 210);
            this.tb_Wema30m.Name = "tb_Wema30m";
            this.tb_Wema30m.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema30m.TabIndex = 52;
            this.tb_Wema30m.Text = "1.5";
            this.tb_Wema30m.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema30m.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // chk_ema30m
            // 
            this.chk_ema30m.AutoSize = true;
            this.chk_ema30m.ForeColor = System.Drawing.Color.White;
            this.chk_ema30m.Location = new System.Drawing.Point(12, 209);
            this.chk_ema30m.Name = "chk_ema30m";
            this.chk_ema30m.Size = new System.Drawing.Size(124, 23);
            this.chk_ema30m.TabIndex = 51;
            this.chk_ema30m.Text = "EMA20_TF30m";
            this.chk_ema30m.UseVisualStyleBackColor = true;
            this.chk_ema30m.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // tb_Wema1h
            // 
            this.tb_Wema1h.Location = new System.Drawing.Point(142, 239);
            this.tb_Wema1h.Name = "tb_Wema1h";
            this.tb_Wema1h.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema1h.TabIndex = 54;
            this.tb_Wema1h.Text = "2";
            this.tb_Wema1h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema1h.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // chk_ema1h
            // 
            this.chk_ema1h.AutoSize = true;
            this.chk_ema1h.ForeColor = System.Drawing.Color.White;
            this.chk_ema1h.Location = new System.Drawing.Point(12, 238);
            this.chk_ema1h.Name = "chk_ema1h";
            this.chk_ema1h.Size = new System.Drawing.Size(112, 23);
            this.chk_ema1h.TabIndex = 53;
            this.chk_ema1h.Text = "EMA20_TF1h";
            this.chk_ema1h.UseVisualStyleBackColor = true;
            this.chk_ema1h.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // tb_Wema2h
            // 
            this.tb_Wema2h.Location = new System.Drawing.Point(142, 268);
            this.tb_Wema2h.Name = "tb_Wema2h";
            this.tb_Wema2h.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema2h.TabIndex = 56;
            this.tb_Wema2h.Text = "3";
            this.tb_Wema2h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema2h.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // chk_ema2h
            // 
            this.chk_ema2h.AutoSize = true;
            this.chk_ema2h.ForeColor = System.Drawing.Color.White;
            this.chk_ema2h.Location = new System.Drawing.Point(12, 267);
            this.chk_ema2h.Name = "chk_ema2h";
            this.chk_ema2h.Size = new System.Drawing.Size(112, 23);
            this.chk_ema2h.TabIndex = 55;
            this.chk_ema2h.Text = "EMA20_TF2h";
            this.chk_ema2h.UseVisualStyleBackColor = true;
            this.chk_ema2h.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // tb_Wema4h
            // 
            this.tb_Wema4h.Location = new System.Drawing.Point(142, 297);
            this.tb_Wema4h.Name = "tb_Wema4h";
            this.tb_Wema4h.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema4h.TabIndex = 58;
            this.tb_Wema4h.Text = "5";
            this.tb_Wema4h.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema4h.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // chk_ema4h
            // 
            this.chk_ema4h.AutoSize = true;
            this.chk_ema4h.ForeColor = System.Drawing.Color.White;
            this.chk_ema4h.Location = new System.Drawing.Point(12, 296);
            this.chk_ema4h.Name = "chk_ema4h";
            this.chk_ema4h.Size = new System.Drawing.Size(112, 23);
            this.chk_ema4h.TabIndex = 57;
            this.chk_ema4h.Text = "EMA20_TF4h";
            this.chk_ema4h.UseVisualStyleBackColor = true;
            this.chk_ema4h.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // tb_Wema1D
            // 
            this.tb_Wema1D.Location = new System.Drawing.Point(142, 326);
            this.tb_Wema1D.Name = "tb_Wema1D";
            this.tb_Wema1D.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema1D.TabIndex = 60;
            this.tb_Wema1D.Text = "7";
            this.tb_Wema1D.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema1D.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // chk_ema1D
            // 
            this.chk_ema1D.AutoSize = true;
            this.chk_ema1D.ForeColor = System.Drawing.Color.White;
            this.chk_ema1D.Location = new System.Drawing.Point(12, 325);
            this.chk_ema1D.Name = "chk_ema1D";
            this.chk_ema1D.Size = new System.Drawing.Size(114, 23);
            this.chk_ema1D.TabIndex = 59;
            this.chk_ema1D.Text = "EMA20_TF1D";
            this.chk_ema1D.UseVisualStyleBackColor = true;
            this.chk_ema1D.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // tb_Wema1W
            // 
            this.tb_Wema1W.Location = new System.Drawing.Point(142, 355);
            this.tb_Wema1W.Name = "tb_Wema1W";
            this.tb_Wema1W.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema1W.TabIndex = 62;
            this.tb_Wema1W.Text = "9";
            this.tb_Wema1W.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema1W.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // chk_ema1W
            // 
            this.chk_ema1W.AutoSize = true;
            this.chk_ema1W.ForeColor = System.Drawing.Color.White;
            this.chk_ema1W.Location = new System.Drawing.Point(12, 354);
            this.chk_ema1W.Name = "chk_ema1W";
            this.chk_ema1W.Size = new System.Drawing.Size(117, 23);
            this.chk_ema1W.TabIndex = 61;
            this.chk_ema1W.Text = "EMA20_TF1W";
            this.chk_ema1W.UseVisualStyleBackColor = true;
            this.chk_ema1W.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // tb_percent
            // 
            this.tb_percent.Location = new System.Drawing.Point(157, 45);
            this.tb_percent.Name = "tb_percent";
            this.tb_percent.Size = new System.Drawing.Size(38, 22);
            this.tb_percent.TabIndex = 66;
            this.tb_percent.Text = "10";
            this.tb_percent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_percent.TextChanged += new System.EventHandler(this.tb_breaks_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(31, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 19);
            this.label3.TabIndex = 51;
            this.label3.Text = "Differential Percent:";
            // 
            // tb_NumofClasses
            // 
            this.tb_NumofClasses.Location = new System.Drawing.Point(158, 96);
            this.tb_NumofClasses.Name = "tb_NumofClasses";
            this.tb_NumofClasses.Size = new System.Drawing.Size(40, 22);
            this.tb_NumofClasses.TabIndex = 66;
            this.tb_NumofClasses.Text = "4";
            this.tb_NumofClasses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_NumofClasses.TextChanged += new System.EventHandler(this.tb_NumofClasses_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(26, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 19);
            this.label4.TabIndex = 51;
            this.label4.Text = "Number of Classses:";
            // 
            // radio_percent
            // 
            this.radio_percent.AutoSize = true;
            this.radio_percent.Checked = true;
            this.radio_percent.ForeColor = System.Drawing.Color.White;
            this.radio_percent.Location = new System.Drawing.Point(5, 22);
            this.radio_percent.Name = "radio_percent";
            this.radio_percent.Size = new System.Drawing.Size(132, 23);
            this.radio_percent.TabIndex = 68;
            this.radio_percent.TabStop = true;
            this.radio_percent.Text = "Percent Classifier";
            this.radio_percent.UseVisualStyleBackColor = true;
            // 
            // radio_jenksFisher
            // 
            this.radio_jenksFisher.AutoSize = true;
            this.radio_jenksFisher.ForeColor = System.Drawing.Color.White;
            this.radio_jenksFisher.Location = new System.Drawing.Point(6, 70);
            this.radio_jenksFisher.Name = "radio_jenksFisher";
            this.radio_jenksFisher.Size = new System.Drawing.Size(154, 23);
            this.radio_jenksFisher.TabIndex = 69;
            this.radio_jenksFisher.Text = "Jenks Fisher Clasifier";
            this.radio_jenksFisher.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_NumofClasses);
            this.groupBox1.Controls.Add(this.radio_percent);
            this.groupBox1.Controls.Add(this.radio_jenksFisher);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_percent);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(8, 383);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 130);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Classifier";
            // 
            // tb_Wema5m
            // 
            this.tb_Wema5m.Location = new System.Drawing.Point(143, 152);
            this.tb_Wema5m.Name = "tb_Wema5m";
            this.tb_Wema5m.Size = new System.Drawing.Size(61, 22);
            this.tb_Wema5m.TabIndex = 72;
            this.tb_Wema5m.Text = "1";
            this.tb_Wema5m.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Wema5m.TextChanged += new System.EventHandler(this.WeightChanged);
            // 
            // chk_ema5m
            // 
            this.chk_ema5m.AutoSize = true;
            this.chk_ema5m.ForeColor = System.Drawing.Color.White;
            this.chk_ema5m.Location = new System.Drawing.Point(13, 151);
            this.chk_ema5m.Name = "chk_ema5m";
            this.chk_ema5m.Size = new System.Drawing.Size(116, 23);
            this.chk_ema5m.TabIndex = 71;
            this.chk_ema5m.Text = "EMA20_TF5m";
            this.chk_ema5m.UseVisualStyleBackColor = true;
            this.chk_ema5m.CheckedChanged += new System.EventHandler(this.Toggle_Checkbox);
            // 
            // strategiesEma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1127, 549);
            this.Controls.Add(this.tb_Wema5m);
            this.Controls.Add(this.chk_ema5m);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tb_Wema1W);
            this.Controls.Add(this.chk_ema1W);
            this.Controls.Add(this.tb_Wema1D);
            this.Controls.Add(this.chk_ema1D);
            this.Controls.Add(this.tb_Wema4h);
            this.Controls.Add(this.chk_ema4h);
            this.Controls.Add(this.tb_Wema2h);
            this.Controls.Add(this.chk_ema2h);
            this.Controls.Add(this.tb_Wema1h);
            this.Controls.Add(this.chk_ema1h);
            this.Controls.Add(this.tb_Wema30m);
            this.Controls.Add(this.chk_ema30m);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Wema15m);
            this.Controls.Add(this.chk_ema15m);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.price_LB);
            this.Controls.Add(this.radChartView1);
            this.Controls.Add(this.radDropDownList1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "strategiesEma";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Strategy EMA Classification";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.strategiesEma_FormClosed);
            this.Load += new System.EventHandler(this.strategiesEma_Load);
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
        private System.Windows.Forms.Label price_LB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_ema15m;
        private System.Windows.Forms.TextBox tb_Wema15m;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Wema30m;
        private System.Windows.Forms.CheckBox chk_ema30m;
        private System.Windows.Forms.TextBox tb_Wema1h;
        private System.Windows.Forms.CheckBox chk_ema1h;
        private System.Windows.Forms.TextBox tb_Wema2h;
        private System.Windows.Forms.CheckBox chk_ema2h;
        private System.Windows.Forms.TextBox tb_Wema4h;
        private System.Windows.Forms.CheckBox chk_ema4h;
        private System.Windows.Forms.TextBox tb_Wema1D;
        private System.Windows.Forms.CheckBox chk_ema1D;
        private System.Windows.Forms.TextBox tb_Wema1W;
        private System.Windows.Forms.CheckBox chk_ema1W;
        private System.Windows.Forms.TextBox tb_percent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_NumofClasses;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radio_percent;
        private System.Windows.Forms.RadioButton radio_jenksFisher;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_Wema5m;
        private System.Windows.Forms.CheckBox chk_ema5m;
    }
}

