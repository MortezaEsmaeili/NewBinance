
namespace BinanceApp
{
    partial class strategies01Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(strategies01Form));
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
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lb_trade = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_PercentageMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView1
            // 
            this.radChartView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radChartView1.AreaDesign = cartesianArea1;
            this.radChartView1.Location = new System.Drawing.Point(243, 78);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowGrid = false;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(862, 461);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Location = new System.Drawing.Point(116, 24);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(121, 28);
            this.radDropDownList1.TabIndex = 14;
            this.radDropDownList1.Text = "radDropDownList3";
            // 
            // radDropDownList4
            // 
            this.radDropDownList4.DropDownAnimationEnabled = true;
            this.radDropDownList4.Location = new System.Drawing.Point(335, 24);
            this.radDropDownList4.Name = "radDropDownList4";
            this.radDropDownList4.Size = new System.Drawing.Size(122, 28);
            this.radDropDownList4.TabIndex = 18;
            this.radDropDownList4.Text = "radDropDownList4";
            // 
            // price_LB
            // 
            this.price_LB.AutoSize = true;
            this.price_LB.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.price_LB.ForeColor = System.Drawing.Color.Lime;
            this.price_LB.Location = new System.Drawing.Point(19, 78);
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
            this.BTN_Buy.Location = new System.Drawing.Point(1009, 15);
            this.BTN_Buy.Name = "BTN_Buy";
            this.BTN_Buy.Size = new System.Drawing.Size(44, 23);
            this.BTN_Buy.TabIndex = 29;
            this.BTN_Buy.UseVisualStyleBackColor = false;
            // 
            // BTN_Sell
            // 
            this.BTN_Sell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_Sell.BackColor = System.Drawing.Color.Gray;
            this.BTN_Sell.Location = new System.Drawing.Point(1009, 41);
            this.BTN_Sell.Name = "BTN_Sell";
            this.BTN_Sell.Size = new System.Drawing.Size(44, 23);
            this.BTN_Sell.TabIndex = 30;
            this.BTN_Sell.UseVisualStyleBackColor = false;
            // 
            // NUD_PercentageMargin
            // 
            this.NUD_PercentageMargin.Font = new System.Drawing.Font("Segoe UI", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUD_PercentageMargin.Location = new System.Drawing.Point(580, 22);
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
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(963, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 19);
            this.label7.TabIndex = 55;
            this.label7.Text = "Sell:";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(963, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 19);
            this.label8.TabIndex = 54;
            this.label8.Text = "Buy:";
            // 
            // lb_trade
            // 
            this.lb_trade.AutoSize = true;
            this.lb_trade.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_trade.ForeColor = System.Drawing.Color.Lime;
            this.lb_trade.Location = new System.Drawing.Point(715, 24);
            this.lb_trade.Name = "lb_trade";
            this.lb_trade.Size = new System.Drawing.Size(121, 28);
            this.lb_trade.TabIndex = 56;
            this.lb_trade.Text = "Trade State";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(490, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 57;
            this.label1.Text = "Percentage:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(257, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 19);
            this.label2.TabIndex = 60;
            this.label2.Text = "Interval:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 61;
            this.label3.Text = "Coin Name:";
            // 
            // strategies01Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1127, 549);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_trade);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
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
            this.Name = "strategies01Form";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Strategy 01";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.strategies01Form_FormClosed);
            this.Load += new System.EventHandler(this.strategies01Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_PercentageMargin)).EndInit();
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
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_trade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

