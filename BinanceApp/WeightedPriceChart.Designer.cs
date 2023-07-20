
namespace BinanceApp
{
    partial class WeightedPriceChart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeightedPriceChart));
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_WP5 = new System.Windows.Forms.CheckBox();
            this.chk_WP50 = new System.Windows.Forms.CheckBox();
            this.chk_WP20 = new System.Windows.Forms.CheckBox();
            this.chk_WP10 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView1
            // 
            this.radChartView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radChartView1.AreaDesign = cartesianArea1;
            this.radChartView1.Location = new System.Drawing.Point(152, 12);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowGrid = false;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(820, 525);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Location = new System.Drawing.Point(7, 40);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(134, 28);
            this.radDropDownList1.TabIndex = 14;
            this.radDropDownList1.Text = "radDropDownList3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 63;
            this.label3.Text = "Coin Name:";
            // 
            // chk_WP5
            // 
            this.chk_WP5.AutoSize = true;
            this.chk_WP5.ForeColor = System.Drawing.Color.White;
            this.chk_WP5.Location = new System.Drawing.Point(7, 94);
            this.chk_WP5.Name = "chk_WP5";
            this.chk_WP5.Size = new System.Drawing.Size(134, 23);
            this.chk_WP5.TabIndex = 64;
            this.chk_WP5.Text = "Weighted Price 5";
            this.chk_WP5.UseVisualStyleBackColor = true;
            // 
            // chk_WP50
            // 
            this.chk_WP50.AutoSize = true;
            this.chk_WP50.ForeColor = System.Drawing.Color.White;
            this.chk_WP50.Location = new System.Drawing.Point(7, 181);
            this.chk_WP50.Name = "chk_WP50";
            this.chk_WP50.Size = new System.Drawing.Size(142, 23);
            this.chk_WP50.TabIndex = 65;
            this.chk_WP50.Text = "Weighted Price 50";
            this.chk_WP50.UseVisualStyleBackColor = true;
            // 
            // chk_WP20
            // 
            this.chk_WP20.AutoSize = true;
            this.chk_WP20.ForeColor = System.Drawing.Color.White;
            this.chk_WP20.Location = new System.Drawing.Point(7, 152);
            this.chk_WP20.Name = "chk_WP20";
            this.chk_WP20.Size = new System.Drawing.Size(142, 23);
            this.chk_WP20.TabIndex = 66;
            this.chk_WP20.Text = "Weighted Price 20";
            this.chk_WP20.UseVisualStyleBackColor = true;
            // 
            // chk_WP10
            // 
            this.chk_WP10.AutoSize = true;
            this.chk_WP10.ForeColor = System.Drawing.Color.White;
            this.chk_WP10.Location = new System.Drawing.Point(7, 123);
            this.chk_WP10.Name = "chk_WP10";
            this.chk_WP10.Size = new System.Drawing.Size(142, 23);
            this.chk_WP10.TabIndex = 67;
            this.chk_WP10.Text = "Weighted Price 10";
            this.chk_WP10.UseVisualStyleBackColor = true;
            // 
            // WeightedPriceChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(994, 549);
            this.Controls.Add(this.chk_WP10);
            this.Controls.Add(this.chk_WP20);
            this.Controls.Add(this.chk_WP50);
            this.Controls.Add(this.chk_WP5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radChartView1);
            this.Controls.Add(this.radDropDownList1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WeightedPriceChart";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Saba Fam - Binance";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WeightedPriceChart_FormClosed);
            this.Load += new System.EventHandler(this.WeightedPriceChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadChartView radChartView1;
        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_WP5;
        private System.Windows.Forms.CheckBox chk_WP50;
        private System.Windows.Forms.CheckBox chk_WP20;
        private System.Windows.Forms.CheckBox chk_WP10;
    }
}

