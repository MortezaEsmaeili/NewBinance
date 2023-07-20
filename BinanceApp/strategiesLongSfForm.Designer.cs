
namespace BinanceApp
{
    partial class strategiesLongSfForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(strategiesLongSfForm));
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.label1 = new System.Windows.Forms.Label();
            this.chCoinList = new System.Windows.Forms.CheckedListBox();
            this.radDropDownList4 = new Telerik.WinControls.UI.RadDropDownList();
            this.cb_Selection = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView1
            // 
            this.radChartView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radChartView1.AreaDesign = cartesianArea1;
            this.radChartView1.Location = new System.Drawing.Point(177, 12);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowGrid = false;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(938, 520);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 36;
            this.label1.Text = "Interval:";
            // 
            // chCoinList
            // 
            this.chCoinList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chCoinList.FormattingEnabled = true;
            this.chCoinList.Location = new System.Drawing.Point(13, 103);
            this.chCoinList.Name = "chCoinList";
            this.chCoinList.Size = new System.Drawing.Size(158, 429);
            this.chCoinList.TabIndex = 35;
            this.chCoinList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chCoinList_ItemCheck);
            // 
            // radDropDownList4
            // 
            this.radDropDownList4.DefaultItemsCountInDropDown = 7;
            this.radDropDownList4.DropDownAnimationEnabled = true;
            this.radDropDownList4.Location = new System.Drawing.Point(13, 39);
            this.radDropDownList4.Name = "radDropDownList4";
            this.radDropDownList4.Size = new System.Drawing.Size(157, 28);
            this.radDropDownList4.TabIndex = 34;
            this.radDropDownList4.Text = "radDropDownList4";
            // 
            // cb_Selection
            // 
            this.cb_Selection.AutoSize = true;
            this.cb_Selection.Checked = true;
            this.cb_Selection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Selection.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.cb_Selection.ForeColor = System.Drawing.Color.White;
            this.cb_Selection.Location = new System.Drawing.Point(16, 81);
            this.cb_Selection.Name = "cb_Selection";
            this.cb_Selection.Size = new System.Drawing.Size(18, 17);
            this.cb_Selection.TabIndex = 37;
            this.cb_Selection.UseVisualStyleBackColor = true;
            this.cb_Selection.CheckedChanged += new System.EventHandler(this.cb_Selection_CheckedChanged);
            // 
            // strategiesLongSfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1127, 549);
            this.Controls.Add(this.cb_Selection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radDropDownList4);
            this.Controls.Add(this.chCoinList);
            this.Controls.Add(this.radChartView1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "strategiesLongSfForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "All Long Term";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.strategiesLongSfForm_FormClosed);
            this.Load += new System.EventHandler(this.strategiesLongSfForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadChartView radChartView1;
        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chCoinList;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList4;
        private System.Windows.Forms.CheckBox cb_Selection;
    }
}

