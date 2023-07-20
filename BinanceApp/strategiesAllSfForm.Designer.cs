
namespace BinanceApp
{
    partial class strategiesAllSfForm
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
            Telerik.WinControls.UI.CartesianArea cartesianArea2 = new Telerik.WinControls.UI.CartesianArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(strategiesAllSfForm));
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.radDropDownList4 = new Telerik.WinControls.UI.RadDropDownList();
            this.cb_ST = new System.Windows.Forms.CheckBox();
            this.cb_LT = new System.Windows.Forms.CheckBox();
            this.chCoinList = new System.Windows.Forms.CheckedListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.radChartView2 = new Telerik.WinControls.UI.RadChartView();
            this.cb_Selection = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView1
            // 
            this.radChartView1.AreaDesign = cartesianArea1;
            this.radChartView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView1.Location = new System.Drawing.Point(0, 0);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowGrid = false;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(945, 360);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.DoubleClick += new System.EventHandler(this.radChartView1_DoubleClick);
            // 
            // radDropDownList4
            // 
            this.radDropDownList4.DefaultItemsCountInDropDown = 7;
            this.radDropDownList4.DropDownAnimationEnabled = true;
            this.radDropDownList4.Location = new System.Drawing.Point(13, 40);
            this.radDropDownList4.Name = "radDropDownList4";
            this.radDropDownList4.Size = new System.Drawing.Size(157, 28);
            this.radDropDownList4.TabIndex = 18;
            this.radDropDownList4.Text = "radDropDownList4";
            // 
            // cb_ST
            // 
            this.cb_ST.AutoSize = true;
            this.cb_ST.Checked = true;
            this.cb_ST.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_ST.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.cb_ST.ForeColor = System.Drawing.Color.White;
            this.cb_ST.Location = new System.Drawing.Point(12, 70);
            this.cb_ST.Name = "cb_ST";
            this.cb_ST.Size = new System.Drawing.Size(105, 23);
            this.cb_ST.TabIndex = 0;
            this.cb_ST.Text = "Short Term";
            this.cb_ST.UseVisualStyleBackColor = true;
            this.cb_ST.CheckedChanged += new System.EventHandler(this.cb_ST_CheckedChanged);
            // 
            // cb_LT
            // 
            this.cb_LT.AutoSize = true;
            this.cb_LT.Checked = true;
            this.cb_LT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_LT.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.cb_LT.ForeColor = System.Drawing.Color.White;
            this.cb_LT.Location = new System.Drawing.Point(13, 99);
            this.cb_LT.Name = "cb_LT";
            this.cb_LT.Size = new System.Drawing.Size(102, 23);
            this.cb_LT.TabIndex = 29;
            this.cb_LT.Text = "Long Term";
            this.cb_LT.UseVisualStyleBackColor = true;
            this.cb_LT.CheckedChanged += new System.EventHandler(this.cb_LT_CheckedChanged);
            // 
            // chCoinList
            // 
            this.chCoinList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chCoinList.FormattingEnabled = true;
            this.chCoinList.Location = new System.Drawing.Point(12, 162);
            this.chCoinList.Name = "chCoinList";
            this.chCoinList.Size = new System.Drawing.Size(158, 382);
            this.chCoinList.TabIndex = 30;
            this.chCoinList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chCoinList_ItemCheck);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(176, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.radChartView1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.radChartView2);
            this.splitContainer1.Size = new System.Drawing.Size(945, 532);
            this.splitContainer1.SplitterDistance = 360;
            this.splitContainer1.TabIndex = 31;
            // 
            // radChartView2
            // 
            this.radChartView2.AreaDesign = cartesianArea2;
            this.radChartView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView2.Location = new System.Drawing.Point(0, 0);
            this.radChartView2.Name = "radChartView2";
            this.radChartView2.ShowGrid = false;
            this.radChartView2.Size = new System.Drawing.Size(945, 168);
            this.radChartView2.TabIndex = 0;
            this.radChartView2.DoubleClick += new System.EventHandler(this.radChartView2_DoubleClick);
            // 
            // cb_Selection
            // 
            this.cb_Selection.AutoSize = true;
            this.cb_Selection.Checked = true;
            this.cb_Selection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Selection.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.cb_Selection.ForeColor = System.Drawing.Color.White;
            this.cb_Selection.Location = new System.Drawing.Point(15, 140);
            this.cb_Selection.Name = "cb_Selection";
            this.cb_Selection.Size = new System.Drawing.Size(18, 17);
            this.cb_Selection.TabIndex = 32;
            this.cb_Selection.UseVisualStyleBackColor = true;
            this.cb_Selection.CheckedChanged += new System.EventHandler(this.cb_Selection_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 19);
            this.label1.TabIndex = 33;
            this.label1.Text = "Interval:";
            // 
            // strategiesAllSfForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1127, 549);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_Selection);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.chCoinList);
            this.Controls.Add(this.cb_LT);
            this.Controls.Add(this.cb_ST);
            this.Controls.Add(this.radDropDownList4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "strategiesAllSfForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Strategy 04";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.strategiesAllSfForm_FormClosed);
            this.Load += new System.EventHandler(this.strategiesAllSfForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList4)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadChartView radChartView1;
        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList4;
        private System.Windows.Forms.CheckBox cb_ST;
        private System.Windows.Forms.CheckBox cb_LT;
        private System.Windows.Forms.CheckedListBox chCoinList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadChartView radChartView2;
        private System.Windows.Forms.CheckBox cb_Selection;
        private System.Windows.Forms.Label label1;
    }
}

