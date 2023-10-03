
namespace BinanceApp
{
    partial class TradeBotForm
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
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.CartesianArea cartesianArea1 = new Telerik.WinControls.UI.CartesianArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TradeBotForm));
            this.radChartView2 = new Telerik.WinControls.UI.RadChartView();
            this.office2007BlackTheme1 = new Telerik.WinControls.Themes.Office2007BlackTheme();
            this.radDropDownList3 = new Telerik.WinControls.UI.RadDropDownList();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_stop_buy = new System.Windows.Forms.Button();
            this.bt_start_buy = new System.Windows.Forms.Button();
            this.cb_show_chart_buy = new System.Windows.Forms.CheckBox();
            this.tx_buy_available = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tx_buy_stop_loss = new System.Windows.Forms.TextBox();
            this.tx_buy_profit_limit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tx_buy_low_price = new System.Windows.Forms.TextBox();
            this.tx_buy_up_price = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_buy_save = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_stop_sell = new System.Windows.Forms.Button();
            this.bt_start_sell = new System.Windows.Forms.Button();
            this.cb_show_chart_sell = new System.Windows.Forms.CheckBox();
            this.tx_sell_low_price = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tx_sell_available = new System.Windows.Forms.TextBox();
            this.tx_sell_up_price = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tx_sell_stop_loss = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tx_sell_profit_limit = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgTragingData = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTragingData)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView2
            // 
            this.radChartView2.AreaDesign = cartesianArea1;
            this.radChartView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView2.Location = new System.Drawing.Point(3, 3);
            this.radChartView2.Name = "radChartView2";
            this.radChartView2.ShowGrid = false;
            this.radChartView2.Size = new System.Drawing.Size(493, 496);
            this.radChartView2.TabIndex = 2;
            this.radChartView2.ThemeName = "Office2007Black";
            this.radChartView2.DoubleClick += new System.EventHandler(this.radChartView2_DoubleClick);
            // 
            // radDropDownList3
            // 
            this.radDropDownList3.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDropDownList3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.radDropDownList3.Location = new System.Drawing.Point(221, 13);
            this.radDropDownList3.Name = "radDropDownList3";
            this.radDropDownList3.Size = new System.Drawing.Size(166, 23);
            this.radDropDownList3.TabIndex = 14;
            this.radDropDownList3.Text = "Coin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(130, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 62;
            this.label3.Text = "Coin Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(123)))), ((int)(((byte)(249)))));
            this.groupBox1.Controls.Add(this.bt_stop_buy);
            this.groupBox1.Controls.Add(this.bt_start_buy);
            this.groupBox1.Controls.Add(this.cb_show_chart_buy);
            this.groupBox1.Controls.Add(this.tx_buy_available);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tx_buy_stop_loss);
            this.groupBox1.Controls.Add(this.tx_buy_profit_limit);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tx_buy_low_price);
            this.groupBox1.Controls.Add(this.tx_buy_up_price);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(16, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 369);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buy";
            // 
            // bt_stop_buy
            // 
            this.bt_stop_buy.BackColor = System.Drawing.Color.Fuchsia;
            this.bt_stop_buy.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_stop_buy.Location = new System.Drawing.Point(141, 268);
            this.bt_stop_buy.Name = "bt_stop_buy";
            this.bt_stop_buy.Size = new System.Drawing.Size(89, 35);
            this.bt_stop_buy.TabIndex = 67;
            this.bt_stop_buy.Text = "Stop";
            this.bt_stop_buy.UseVisualStyleBackColor = false;
            this.bt_stop_buy.Click += new System.EventHandler(this.bt_stop_buy_Click);
            // 
            // bt_start_buy
            // 
            this.bt_start_buy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bt_start_buy.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_start_buy.Location = new System.Drawing.Point(12, 268);
            this.bt_start_buy.Name = "bt_start_buy";
            this.bt_start_buy.Size = new System.Drawing.Size(89, 35);
            this.bt_start_buy.TabIndex = 66;
            this.bt_start_buy.Text = "Start";
            this.bt_start_buy.UseVisualStyleBackColor = false;
            this.bt_start_buy.Click += new System.EventHandler(this.bt_start_buy_Click);
            // 
            // cb_show_chart_buy
            // 
            this.cb_show_chart_buy.AutoSize = true;
            this.cb_show_chart_buy.Location = new System.Drawing.Point(47, 320);
            this.cb_show_chart_buy.Name = "cb_show_chart_buy";
            this.cb_show_chart_buy.Size = new System.Drawing.Size(131, 24);
            this.cb_show_chart_buy.TabIndex = 12;
            this.cb_show_chart_buy.Text = "Show on chart";
            this.cb_show_chart_buy.UseVisualStyleBackColor = true;
            this.cb_show_chart_buy.CheckedChanged += new System.EventHandler(this.cb_show_chart_buy_CheckedChanged);
            // 
            // tx_buy_available
            // 
            this.tx_buy_available.Location = new System.Drawing.Point(107, 215);
            this.tx_buy_available.Name = "tx_buy_available";
            this.tx_buy_available.Size = new System.Drawing.Size(123, 27);
            this.tx_buy_available.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 218);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Available:";
            // 
            // tx_buy_stop_loss
            // 
            this.tx_buy_stop_loss.Location = new System.Drawing.Point(107, 171);
            this.tx_buy_stop_loss.Name = "tx_buy_stop_loss";
            this.tx_buy_stop_loss.Size = new System.Drawing.Size(123, 27);
            this.tx_buy_stop_loss.TabIndex = 7;
            // 
            // tx_buy_profit_limit
            // 
            this.tx_buy_profit_limit.Location = new System.Drawing.Point(107, 127);
            this.tx_buy_profit_limit.Name = "tx_buy_profit_limit";
            this.tx_buy_profit_limit.Size = new System.Drawing.Size(123, 27);
            this.tx_buy_profit_limit.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Stop Loss:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Profit Limit:";
            // 
            // tx_buy_low_price
            // 
            this.tx_buy_low_price.Location = new System.Drawing.Point(107, 83);
            this.tx_buy_low_price.Name = "tx_buy_low_price";
            this.tx_buy_low_price.Size = new System.Drawing.Size(123, 27);
            this.tx_buy_low_price.TabIndex = 3;
            // 
            // tx_buy_up_price
            // 
            this.tx_buy_up_price.Location = new System.Drawing.Point(107, 39);
            this.tx_buy_up_price.Name = "tx_buy_up_price";
            this.tx_buy_up_price.Size = new System.Drawing.Size(123, 27);
            this.tx_buy_up_price.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Low Price:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Up Price:";
            // 
            // btn_buy_save
            // 
            this.btn_buy_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btn_buy_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_buy_save.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_buy_save.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_buy_save.Location = new System.Drawing.Point(230, 466);
            this.btn_buy_save.Name = "btn_buy_save";
            this.btn_buy_save.Size = new System.Drawing.Size(99, 45);
            this.btn_buy_save.TabIndex = 11;
            this.btn_buy_save.Text = "Save";
            this.btn_buy_save.UseVisualStyleBackColor = false;
            this.btn_buy_save.Click += new System.EventHandler(this.btn_buy_save_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(58)))), ((int)(((byte)(86)))));
            this.groupBox2.Controls.Add(this.bt_stop_sell);
            this.groupBox2.Controls.Add(this.bt_start_sell);
            this.groupBox2.Controls.Add(this.cb_show_chart_sell);
            this.groupBox2.Controls.Add(this.tx_sell_low_price);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tx_sell_available);
            this.groupBox2.Controls.Add(this.tx_sell_up_price);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tx_sell_stop_loss);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tx_sell_profit_limit);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(296, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(249, 369);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sell";
            // 
            // bt_stop_sell
            // 
            this.bt_stop_sell.BackColor = System.Drawing.Color.Fuchsia;
            this.bt_stop_sell.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_stop_sell.Location = new System.Drawing.Point(144, 268);
            this.bt_stop_sell.Name = "bt_stop_sell";
            this.bt_stop_sell.Size = new System.Drawing.Size(89, 35);
            this.bt_stop_sell.TabIndex = 69;
            this.bt_stop_sell.Text = "Stop";
            this.bt_stop_sell.UseVisualStyleBackColor = false;
            this.bt_stop_sell.Click += new System.EventHandler(this.bt_stop_sell_Click);
            // 
            // bt_start_sell
            // 
            this.bt_start_sell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bt_start_sell.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.bt_start_sell.Location = new System.Drawing.Point(15, 268);
            this.bt_start_sell.Name = "bt_start_sell";
            this.bt_start_sell.Size = new System.Drawing.Size(89, 35);
            this.bt_start_sell.TabIndex = 68;
            this.bt_start_sell.Text = "Start";
            this.bt_start_sell.UseVisualStyleBackColor = false;
            this.bt_start_sell.Click += new System.EventHandler(this.bt_start_sell_Click);
            // 
            // cb_show_chart_sell
            // 
            this.cb_show_chart_sell.AutoSize = true;
            this.cb_show_chart_sell.Location = new System.Drawing.Point(51, 321);
            this.cb_show_chart_sell.Name = "cb_show_chart_sell";
            this.cb_show_chart_sell.Size = new System.Drawing.Size(131, 24);
            this.cb_show_chart_sell.TabIndex = 21;
            this.cb_show_chart_sell.Text = "Show on chart";
            this.cb_show_chart_sell.UseVisualStyleBackColor = true;
            this.cb_show_chart_sell.CheckedChanged += new System.EventHandler(this.cb_show_chart_buy_CheckedChanged);
            // 
            // tx_sell_low_price
            // 
            this.tx_sell_low_price.Location = new System.Drawing.Point(110, 85);
            this.tx_sell_low_price.Name = "tx_sell_low_price";
            this.tx_sell_low_price.Size = new System.Drawing.Size(123, 27);
            this.tx_sell_low_price.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Low Price:";
            // 
            // tx_sell_available
            // 
            this.tx_sell_available.Location = new System.Drawing.Point(111, 216);
            this.tx_sell_available.Name = "tx_sell_available";
            this.tx_sell_available.Size = new System.Drawing.Size(123, 27);
            this.tx_sell_available.TabIndex = 18;
            // 
            // tx_sell_up_price
            // 
            this.tx_sell_up_price.Location = new System.Drawing.Point(110, 39);
            this.tx_sell_up_price.Name = "tx_sell_up_price";
            this.tx_sell_up_price.Size = new System.Drawing.Size(123, 27);
            this.tx_sell_up_price.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 219);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Available:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Up Price:";
            // 
            // tx_sell_stop_loss
            // 
            this.tx_sell_stop_loss.Location = new System.Drawing.Point(111, 172);
            this.tx_sell_stop_loss.Name = "tx_sell_stop_loss";
            this.tx_sell_stop_loss.Size = new System.Drawing.Size(123, 27);
            this.tx_sell_stop_loss.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 20);
            this.label11.TabIndex = 13;
            this.label11.Text = "Profit Limit:";
            // 
            // tx_sell_profit_limit
            // 
            this.tx_sell_profit_limit.Location = new System.Drawing.Point(111, 128);
            this.tx_sell_profit_limit.Name = "tx_sell_profit_limit";
            this.tx_sell_profit_limit.Size = new System.Drawing.Size(123, 27);
            this.tx_sell_profit_limit.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "Stop Loss:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(574, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(507, 535);
            this.tabControl1.TabIndex = 65;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.radChartView2);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(499, 502);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chart";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnExport);
            this.tabPage2.Controls.Add(this.dgTragingData);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(499, 502);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(23, 465);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 27);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dgTragingData
            // 
            this.dgTragingData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTragingData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTragingData.Location = new System.Drawing.Point(3, 3);
            this.dgTragingData.Name = "dgTragingData";
            this.dgTragingData.RowHeadersWidth = 51;
            this.dgTragingData.RowTemplate.Height = 24;
            this.dgTragingData.Size = new System.Drawing.Size(493, 456);
            this.dgTragingData.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(499, 502);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Open Positions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Green;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 23;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(493, 496);
            this.listBox1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TradeBotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1086, 549);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_buy_save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radDropDownList3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TradeBotForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Saba Fam - Binance";
            this.ThemeName = "Office2007Black";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTragingData)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Telerik.WinControls.UI.RadChartView radChartView2;
        private Telerik.WinControls.Themes.Office2007BlackTheme office2007BlackTheme1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tx_buy_up_price;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tx_buy_stop_loss;
        private System.Windows.Forms.TextBox tx_buy_profit_limit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tx_buy_low_price;
        private System.Windows.Forms.TextBox tx_sell_low_price;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tx_sell_up_price;
        private System.Windows.Forms.Button btn_buy_save;
        private System.Windows.Forms.TextBox tx_buy_available;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cb_show_chart_buy;
        private System.Windows.Forms.CheckBox cb_show_chart_sell;
        private System.Windows.Forms.TextBox tx_sell_available;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tx_sell_stop_loss;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tx_sell_profit_limit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgTragingData;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button bt_stop_buy;
        private System.Windows.Forms.Button bt_start_buy;
        private System.Windows.Forms.Button bt_stop_sell;
        private System.Windows.Forms.Button bt_start_sell;
    }
}

