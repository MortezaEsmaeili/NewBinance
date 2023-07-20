namespace BinanceApp
{
    partial class TradeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeForm));
            this.label9 = new System.Windows.Forms.Label();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.price_LB = new System.Windows.Forms.Label();
            this.btn_Position = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_stopLoss = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_takeProfit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.userName_TB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Trade = new System.Windows.Forms.Button();
            this.btn_getReport = new System.Windows.Forms.Button();
            this.listBox_Report = new System.Windows.Forms.ListBox();
            this.usersTrades1 = new BinanceApp.UsersTrades();
            this.report1 = new FastReport.Report();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersTrades1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.report1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(17, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 25);
            this.label9.TabIndex = 59;
            this.label9.Text = "Coin Name:";
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.DropDownAnimationEnabled = true;
            this.radDropDownList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDropDownList1.Location = new System.Drawing.Point(139, 33);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(139, 28);
            this.radDropDownList1.TabIndex = 57;
            this.radDropDownList1.Text = "radDropDownList3";
            // 
            // price_LB
            // 
            this.price_LB.AutoSize = true;
            this.price_LB.Font = new System.Drawing.Font("Segoe UI", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.price_LB.ForeColor = System.Drawing.Color.Lime;
            this.price_LB.Location = new System.Drawing.Point(219, 169);
            this.price_LB.Name = "price_LB";
            this.price_LB.Size = new System.Drawing.Size(71, 32);
            this.price_LB.TabIndex = 61;
            this.price_LB.Text = "Price";
            this.price_LB.Click += new System.EventHandler(this.price_LB_Click);
            // 
            // btn_Position
            // 
            this.btn_Position.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_Position.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Position.ForeColor = System.Drawing.Color.Black;
            this.btn_Position.Location = new System.Drawing.Point(44, 220);
            this.btn_Position.Name = "btn_Position";
            this.btn_Position.Size = new System.Drawing.Size(139, 41);
            this.btn_Position.TabIndex = 64;
            this.btn_Position.Text = "Position";
            this.btn_Position.UseVisualStyleBackColor = false;
            this.btn_Position.Click += new System.EventHandler(this.btn_trend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_stopLoss);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_takeProfit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.userName_TB);
            this.groupBox1.Controls.Add(this.radDropDownList1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 130);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // tb_stopLoss
            // 
            this.tb_stopLoss.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_stopLoss.Location = new System.Drawing.Point(427, 77);
            this.tb_stopLoss.Name = "tb_stopLoss";
            this.tb_stopLoss.Size = new System.Drawing.Size(136, 34);
            this.tb_stopLoss.TabIndex = 67;
            this.tb_stopLoss.Text = "0.01";
            this.tb_stopLoss.Leave += new System.EventHandler(this.tb_stopLoss_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(305, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 25);
            this.label3.TabIndex = 66;
            this.label3.Text = "SP %:";
            // 
            // tb_takeProfit
            // 
            this.tb_takeProfit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_takeProfit.Location = new System.Drawing.Point(139, 77);
            this.tb_takeProfit.Name = "tb_takeProfit";
            this.tb_takeProfit.Size = new System.Drawing.Size(136, 34);
            this.tb_takeProfit.TabIndex = 65;
            this.tb_takeProfit.Text = "0.01";
            this.tb_takeProfit.Leave += new System.EventHandler(this.tb_stopLoss_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 25);
            this.label2.TabIndex = 64;
            this.label2.Text = "TP %:";
            // 
            // userName_TB
            // 
            this.userName_TB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName_TB.Location = new System.Drawing.Point(427, 30);
            this.userName_TB.Name = "userName_TB";
            this.userName_TB.Size = new System.Drawing.Size(139, 34);
            this.userName_TB.TabIndex = 63;
            this.userName_TB.Leave += new System.EventHandler(this.userName_TB_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(305, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 25);
            this.label1.TabIndex = 62;
            this.label1.Text = "User Name:";
            // 
            // btn_Trade
            // 
            this.btn_Trade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_Trade.Enabled = false;
            this.btn_Trade.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Trade.Location = new System.Drawing.Point(234, 220);
            this.btn_Trade.Name = "btn_Trade";
            this.btn_Trade.Size = new System.Drawing.Size(139, 41);
            this.btn_Trade.TabIndex = 66;
            this.btn_Trade.Text = "Open Position";
            this.btn_Trade.UseVisualStyleBackColor = false;
            this.btn_Trade.Click += new System.EventHandler(this.btn_Trade_Click);
            // 
            // btn_getReport
            // 
            this.btn_getReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_getReport.Location = new System.Drawing.Point(424, 220);
            this.btn_getReport.Name = "btn_getReport";
            this.btn_getReport.Size = new System.Drawing.Size(139, 41);
            this.btn_getReport.TabIndex = 67;
            this.btn_getReport.Text = "Get Report";
            this.btn_getReport.UseVisualStyleBackColor = false;
            this.btn_getReport.Click += new System.EventHandler(this.btn_getReport_Click);
            // 
            // listBox_Report
            // 
            this.listBox_Report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.listBox_Report.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_Report.ForeColor = System.Drawing.SystemColors.Info;
            this.listBox_Report.FormattingEnabled = true;
            this.listBox_Report.ItemHeight = 31;
            this.listBox_Report.Location = new System.Drawing.Point(9, 281);
            this.listBox_Report.Name = "listBox_Report";
            this.listBox_Report.Size = new System.Drawing.Size(582, 190);
            this.listBox_Report.TabIndex = 68;
            // 
            // usersTrades1
            // 
            this.usersTrades1.DataSetName = "UsersTrades";
            this.usersTrades1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // report1
            // 
            this.report1.NeedRefresh = false;
            this.report1.ReportResourceString = resources.GetString("report1.ReportResourceString");
            this.report1.Tag = null;
            this.report1.RegisterData(this.usersTrades1, "usersTrades1");
            // 
            // TradeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(606, 482);
            this.ControlBox = false;
            this.Controls.Add(this.listBox_Report);
            this.Controls.Add(this.btn_getReport);
            this.Controls.Add(this.btn_Trade);
            this.Controls.Add(this.btn_Position);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.price_LB);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TradeForm";
            this.Opacity = 0.9D;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trade Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.TradeForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TradeForm_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usersTrades1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.report1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private System.Windows.Forms.Label price_LB;
        private System.Windows.Forms.Button btn_Position;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox userName_TB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Trade;
        private System.Windows.Forms.Button btn_getReport;
        private System.Windows.Forms.ListBox listBox_Report;
        private System.Windows.Forms.TextBox tb_stopLoss;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_takeProfit;
        private System.Windows.Forms.Label label2;
        private UsersTrades usersTrades1;
        private FastReport.Report report1;
    }
}
