
namespace BinanceApp
{
    partial class DmhDmlForm
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
            this.dg_DML = new System.Windows.Forms.DataGridView();
            this.coinNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dML1MinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dML5MinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dML15MinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dML30MinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dML60MinDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dML4HourDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dML1DayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dg_DMH = new System.Windows.Forms.DataGridView();
            this.bindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.coinNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg_DML)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_DMH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_DML
            // 
            this.dg_DML.AllowUserToAddRows = false;
            this.dg_DML.AllowUserToDeleteRows = false;
            this.dg_DML.AllowUserToOrderColumns = true;
            this.dg_DML.AutoGenerateColumns = false;
            this.dg_DML.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_DML.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coinNameDataGridViewTextBoxColumn,
            this.dML1MinDataGridViewTextBoxColumn,
            this.dML5MinDataGridViewTextBoxColumn,
            this.dML15MinDataGridViewTextBoxColumn,
            this.dML30MinDataGridViewTextBoxColumn,
            this.dML60MinDataGridViewTextBoxColumn,
            this.dML4HourDataGridViewTextBoxColumn,
            this.dML1DayDataGridViewTextBoxColumn});
            this.dg_DML.DataSource = this.bindingSource1;
            this.dg_DML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_DML.Location = new System.Drawing.Point(0, 0);
            this.dg_DML.Name = "dg_DML";
            this.dg_DML.ReadOnly = true;
            this.dg_DML.RowHeadersWidth = 51;
            this.dg_DML.RowTemplate.Height = 24;
            this.dg_DML.Size = new System.Drawing.Size(1419, 248);
            this.dg_DML.TabIndex = 0;
            // 
            // coinNameDataGridViewTextBoxColumn
            // 
            this.coinNameDataGridViewTextBoxColumn.DataPropertyName = "CoinName";
            this.coinNameDataGridViewTextBoxColumn.HeaderText = "CoinName";
            this.coinNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.coinNameDataGridViewTextBoxColumn.Name = "coinNameDataGridViewTextBoxColumn";
            this.coinNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.coinNameDataGridViewTextBoxColumn.Width = 125;
            // 
            // dML1MinDataGridViewTextBoxColumn
            // 
            this.dML1MinDataGridViewTextBoxColumn.DataPropertyName = "DML_1Min";
            this.dML1MinDataGridViewTextBoxColumn.HeaderText = "DML_1Min";
            this.dML1MinDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dML1MinDataGridViewTextBoxColumn.Name = "dML1MinDataGridViewTextBoxColumn";
            this.dML1MinDataGridViewTextBoxColumn.ReadOnly = true;
            this.dML1MinDataGridViewTextBoxColumn.Width = 125;
            // 
            // dML5MinDataGridViewTextBoxColumn
            // 
            this.dML5MinDataGridViewTextBoxColumn.DataPropertyName = "DML_5Min";
            this.dML5MinDataGridViewTextBoxColumn.HeaderText = "DML_5Min";
            this.dML5MinDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dML5MinDataGridViewTextBoxColumn.Name = "dML5MinDataGridViewTextBoxColumn";
            this.dML5MinDataGridViewTextBoxColumn.ReadOnly = true;
            this.dML5MinDataGridViewTextBoxColumn.Width = 125;
            // 
            // dML15MinDataGridViewTextBoxColumn
            // 
            this.dML15MinDataGridViewTextBoxColumn.DataPropertyName = "DML_15Min";
            this.dML15MinDataGridViewTextBoxColumn.HeaderText = "DML_15Min";
            this.dML15MinDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dML15MinDataGridViewTextBoxColumn.Name = "dML15MinDataGridViewTextBoxColumn";
            this.dML15MinDataGridViewTextBoxColumn.ReadOnly = true;
            this.dML15MinDataGridViewTextBoxColumn.Width = 125;
            // 
            // dML30MinDataGridViewTextBoxColumn
            // 
            this.dML30MinDataGridViewTextBoxColumn.DataPropertyName = "DML_30Min";
            this.dML30MinDataGridViewTextBoxColumn.HeaderText = "DML_30Min";
            this.dML30MinDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dML30MinDataGridViewTextBoxColumn.Name = "dML30MinDataGridViewTextBoxColumn";
            this.dML30MinDataGridViewTextBoxColumn.ReadOnly = true;
            this.dML30MinDataGridViewTextBoxColumn.Width = 125;
            // 
            // dML60MinDataGridViewTextBoxColumn
            // 
            this.dML60MinDataGridViewTextBoxColumn.DataPropertyName = "DML_60Min";
            this.dML60MinDataGridViewTextBoxColumn.HeaderText = "DML_60Min";
            this.dML60MinDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dML60MinDataGridViewTextBoxColumn.Name = "dML60MinDataGridViewTextBoxColumn";
            this.dML60MinDataGridViewTextBoxColumn.ReadOnly = true;
            this.dML60MinDataGridViewTextBoxColumn.Width = 125;
            // 
            // dML4HourDataGridViewTextBoxColumn
            // 
            this.dML4HourDataGridViewTextBoxColumn.DataPropertyName = "DML_4Hour";
            this.dML4HourDataGridViewTextBoxColumn.HeaderText = "DML_4Hour";
            this.dML4HourDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dML4HourDataGridViewTextBoxColumn.Name = "dML4HourDataGridViewTextBoxColumn";
            this.dML4HourDataGridViewTextBoxColumn.ReadOnly = true;
            this.dML4HourDataGridViewTextBoxColumn.Width = 125;
            // 
            // dML1DayDataGridViewTextBoxColumn
            // 
            this.dML1DayDataGridViewTextBoxColumn.DataPropertyName = "DML_1Day";
            this.dML1DayDataGridViewTextBoxColumn.HeaderText = "DML_1Day";
            this.dML1DayDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.dML1DayDataGridViewTextBoxColumn.Name = "dML1DayDataGridViewTextBoxColumn";
            this.dML1DayDataGridViewTextBoxColumn.ReadOnly = true;
            this.dML1DayDataGridViewTextBoxColumn.Width = 125;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(BinanceApp.DataModel.CoinDML);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dg_DML);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dg_DMH);
            this.splitContainer1.Size = new System.Drawing.Size(1419, 475);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 1;
            // 
            // dg_DMH
            // 
            this.dg_DMH.AllowUserToAddRows = false;
            this.dg_DMH.AllowUserToDeleteRows = false;
            this.dg_DMH.AutoGenerateColumns = false;
            this.dg_DMH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_DMH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coinNameDataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.dg_DMH.DataSource = this.bindingSource2;
            this.dg_DMH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_DMH.Location = new System.Drawing.Point(0, 0);
            this.dg_DMH.Name = "dg_DMH";
            this.dg_DMH.ReadOnly = true;
            this.dg_DMH.RowHeadersWidth = 51;
            this.dg_DMH.RowTemplate.Height = 24;
            this.dg_DMH.Size = new System.Drawing.Size(1419, 223);
            this.dg_DMH.TabIndex = 0;
            // 
            // bindingSource2
            // 
            this.bindingSource2.DataSource = typeof(BinanceApp.DataModel.CoinDMH);
            // 
            // coinNameDataGridViewTextBoxColumn1
            // 
            this.coinNameDataGridViewTextBoxColumn1.DataPropertyName = "CoinName";
            this.coinNameDataGridViewTextBoxColumn1.HeaderText = "CoinName";
            this.coinNameDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.coinNameDataGridViewTextBoxColumn1.Name = "coinNameDataGridViewTextBoxColumn1";
            this.coinNameDataGridViewTextBoxColumn1.ReadOnly = true;
            this.coinNameDataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "DMH_1Min";
            this.dataGridViewTextBoxColumn1.HeaderText = "DMH_1Min";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DMH_5Min";
            this.dataGridViewTextBoxColumn2.HeaderText = "DMH_5Min";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DMH_15Min";
            this.dataGridViewTextBoxColumn3.HeaderText = "DMH_15Min";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DMH_30Min";
            this.dataGridViewTextBoxColumn4.HeaderText = "DMH_30Min";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DMH_60Min";
            this.dataGridViewTextBoxColumn5.HeaderText = "DMH_60Min";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "DMH_4Hour";
            this.dataGridViewTextBoxColumn6.HeaderText = "DMH_4Hour";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 125;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "DMH_1Day";
            this.dataGridViewTextBoxColumn7.HeaderText = "DMH_1Day";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 125;
            // 
            // DmhDmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1419, 475);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DmhDmlForm";
            this.Text = "DMH-DML";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DmhDmlForm_FormClosed);
            this.Load += new System.EventHandler(this.DmhDmlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_DML)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_DMH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_DML;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dML1MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMH1MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dML5MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMH5MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dML15MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMH15MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dML30MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMH30MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dML60MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMH60MinDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dML4HourDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMH4HourDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dML1DayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dMH1DayDataGridViewTextBoxColumn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dg_DMH;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}