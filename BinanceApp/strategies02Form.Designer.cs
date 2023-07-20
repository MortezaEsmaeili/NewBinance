
namespace BinanceApp
{
    partial class strategies02Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(strategies02Form));
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.radDropDownList4 = new Telerik.WinControls.UI.RadDropDownList();
            this.price_LB = new System.Windows.Forms.Label();
            this.ResetTrading_BT = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.BTN_Buy = new System.Windows.Forms.Button();
            this.BTN_Sell = new System.Windows.Forms.Button();
            this.NUD_PercentageMargin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_ma1 = new System.Windows.Forms.TextBox();
            this.tb_ma2 = new System.Windows.Forms.TextBox();
            this.tb_ma3 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_slowPeriod = new System.Windows.Forms.TextBox();
            this.tb_signalPeriod = new System.Windows.Forms.TextBox();
            this.tb_fastPeriod = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lb_trade = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_PercentageMargin)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView1
            // 
            this.radChartView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radChartView1.AreaDesign = cartesianArea1;
            this.radChartView1.Location = new System.Drawing.Point(243, 122);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowGrid = false;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(872, 417);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Location = new System.Drawing.Point(116, 13);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(200, 28);
            this.radDropDownList1.TabIndex = 14;
            this.radDropDownList1.Text = "radDropDownList3";
            // 
            // radDropDownList4
            // 
            this.radDropDownList4.DropDownAnimationEnabled = true;
            this.radDropDownList4.Location = new System.Drawing.Point(117, 48);
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
            this.price_LB.Location = new System.Drawing.Point(785, 44);
            this.price_LB.Name = "price_LB";
            this.price_LB.Size = new System.Drawing.Size(58, 28);
            this.price_LB.TabIndex = 19;
            this.price_LB.Text = "Price";
            // 
            // ResetTrading_BT
            // 
            this.ResetTrading_BT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResetTrading_BT.Location = new System.Drawing.Point(49, 480);
            this.ResetTrading_BT.Name = "ResetTrading_BT";
            this.ResetTrading_BT.Size = new System.Drawing.Size(118, 37);
            this.ResetTrading_BT.TabIndex = 27;
            this.ResetTrading_BT.Text = "Reset Trading";
            this.ResetTrading_BT.UseVisualStyleBackColor = true;
            this.ResetTrading_BT.Click += new System.EventHandler(this.ResetTrading_BT_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.Color.Lime;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 23;
            this.listBox1.Location = new System.Drawing.Point(12, 122);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(225, 349);
            this.listBox1.TabIndex = 28;
            this.listBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseClick);
            // 
            // BTN_Buy
            // 
            this.BTN_Buy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Buy.BackColor = System.Drawing.Color.Gray;
            this.BTN_Buy.Location = new System.Drawing.Point(996, 46);
            this.BTN_Buy.Name = "BTN_Buy";
            this.BTN_Buy.Size = new System.Drawing.Size(44, 23);
            this.BTN_Buy.TabIndex = 29;
            this.BTN_Buy.UseVisualStyleBackColor = false;
            // 
            // BTN_Sell
            // 
            this.BTN_Sell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Sell.BackColor = System.Drawing.Color.Gray;
            this.BTN_Sell.Location = new System.Drawing.Point(996, 72);
            this.BTN_Sell.Name = "BTN_Sell";
            this.BTN_Sell.Size = new System.Drawing.Size(44, 23);
            this.BTN_Sell.TabIndex = 30;
            this.BTN_Sell.UseVisualStyleBackColor = false;
            // 
            // NUD_PercentageMargin
            // 
            this.NUD_PercentageMargin.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUD_PercentageMargin.Location = new System.Drawing.Point(118, 82);
            this.NUD_PercentageMargin.Name = "NUD_PercentageMargin";
            this.NUD_PercentageMargin.Size = new System.Drawing.Size(47, 30);
            this.NUD_PercentageMargin.TabIndex = 31;
            this.NUD_PercentageMargin.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NUD_PercentageMargin.ValueChanged += new System.EventHandler(this.NUD_PercentageMargin_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 19);
            this.label1.TabIndex = 32;
            this.label1.Text = "MA1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 19);
            this.label2.TabIndex = 33;
            this.label2.Text = "MA2:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 19);
            this.label3.TabIndex = 34;
            this.label3.Text = "MA3:";
            // 
            // tb_ma1
            // 
            this.tb_ma1.Location = new System.Drawing.Point(88, 21);
            this.tb_ma1.Name = "tb_ma1";
            this.tb_ma1.Size = new System.Drawing.Size(53, 22);
            this.tb_ma1.TabIndex = 35;
            this.tb_ma1.Text = "20";
            this.tb_ma1.TextChanged += new System.EventHandler(this.tb_ma1_TextChanged);
            // 
            // tb_ma2
            // 
            this.tb_ma2.Location = new System.Drawing.Point(88, 47);
            this.tb_ma2.Name = "tb_ma2";
            this.tb_ma2.Size = new System.Drawing.Size(53, 22);
            this.tb_ma2.TabIndex = 36;
            this.tb_ma2.Text = "200";
            this.tb_ma2.TextChanged += new System.EventHandler(this.tb_ma2_TextChanged);
            // 
            // tb_ma3
            // 
            this.tb_ma3.Location = new System.Drawing.Point(88, 73);
            this.tb_ma3.Name = "tb_ma3";
            this.tb_ma3.Size = new System.Drawing.Size(53, 22);
            this.tb_ma3.TabIndex = 37;
            this.tb_ma3.Text = "950";
            this.tb_ma3.TextChanged += new System.EventHandler(this.tb_ma3_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_slowPeriod);
            this.groupBox1.Controls.Add(this.tb_signalPeriod);
            this.groupBox1.Controls.Add(this.tb_fastPeriod);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Location = new System.Drawing.Point(489, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 106);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MACD";
            // 
            // tb_slowPeriod
            // 
            this.tb_slowPeriod.Location = new System.Drawing.Point(145, 47);
            this.tb_slowPeriod.Name = "tb_slowPeriod";
            this.tb_slowPeriod.Size = new System.Drawing.Size(53, 22);
            this.tb_slowPeriod.TabIndex = 43;
            this.tb_slowPeriod.Text = "26";
            this.tb_slowPeriod.TextChanged += new System.EventHandler(this.tb_macd_TextChanged);
            // 
            // tb_signalPeriod
            // 
            this.tb_signalPeriod.Location = new System.Drawing.Point(145, 73);
            this.tb_signalPeriod.Name = "tb_signalPeriod";
            this.tb_signalPeriod.Size = new System.Drawing.Size(53, 22);
            this.tb_signalPeriod.TabIndex = 44;
            this.tb_signalPeriod.Text = "9";
            this.tb_signalPeriod.TextChanged += new System.EventHandler(this.tb_macd_TextChanged);
            // 
            // tb_fastPeriod
            // 
            this.tb_fastPeriod.Location = new System.Drawing.Point(145, 21);
            this.tb_fastPeriod.Name = "tb_fastPeriod";
            this.tb_fastPeriod.Size = new System.Drawing.Size(53, 22);
            this.tb_fastPeriod.TabIndex = 42;
            this.tb_fastPeriod.Text = "12";
            this.tb_fastPeriod.TextChanged += new System.EventHandler(this.tb_macd_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Location = new System.Drawing.Point(16, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 19);
            this.label6.TabIndex = 41;
            this.label6.Text = "Signal Period:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(16, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 19);
            this.label5.TabIndex = 40;
            this.label5.Text = "Slow Period:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(16, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 19);
            this.label4.TabIndex = 39;
            this.label4.Text = "Fast Period:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tb_ma1);
            this.groupBox2.Controls.Add(this.tb_ma3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tb_ma2);
            this.groupBox2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox2.Location = new System.Drawing.Point(322, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 107);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Moving Average";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(941, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 19);
            this.label7.TabIndex = 53;
            this.label7.Text = "Sell:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(941, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 19);
            this.label8.TabIndex = 52;
            this.label8.Text = "Buy:";
            // 
            // lb_trade
            // 
            this.lb_trade.AutoSize = true;
            this.lb_trade.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_trade.ForeColor = System.Drawing.Color.Lime;
            this.lb_trade.Location = new System.Drawing.Point(919, 13);
            this.lb_trade.Name = "lb_trade";
            this.lb_trade.Size = new System.Drawing.Size(121, 28);
            this.lb_trade.TabIndex = 54;
            this.lb_trade.Text = "Trade State";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(25, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 19);
            this.label9.TabIndex = 55;
            this.label9.Text = "Coin Name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(25, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 19);
            this.label10.TabIndex = 56;
            this.label10.Text = "Interval:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(25, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 19);
            this.label11.TabIndex = 57;
            this.label11.Text = "Percentage:";
            // 
            // strategies02Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1127, 549);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lb_trade);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.NUD_PercentageMargin);
            this.Controls.Add(this.BTN_Sell);
            this.Controls.Add(this.BTN_Buy);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ResetTrading_BT);
            this.Controls.Add(this.price_LB);
            this.Controls.Add(this.radDropDownList4);
            this.Controls.Add(this.radChartView1);
            this.Controls.Add(this.radDropDownList1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "strategies02Form";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Coin Name:";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.strategies02Form_FormClosed);
            this.Load += new System.EventHandler(this.strategies02Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_PercentageMargin)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.Button ResetTrading_BT;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button BTN_Buy;
        private System.Windows.Forms.Button BTN_Sell;
        private System.Windows.Forms.NumericUpDown NUD_PercentageMargin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_ma1;
        private System.Windows.Forms.TextBox tb_ma2;
        private System.Windows.Forms.TextBox tb_ma3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_slowPeriod;
        private System.Windows.Forms.TextBox tb_signalPeriod;
        private System.Windows.Forms.TextBox tb_fastPeriod;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_trade;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}

