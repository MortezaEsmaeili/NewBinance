
namespace BinanceApp
{
    partial class CoinAlertForm
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
            this.lv_alerts = new System.Windows.Forms.ListView();
            this.Coin_Interval = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LocalTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CloseTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ML = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DML = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DMH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ST_SF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LT_SF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Signal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lv_alerts
            // 
            this.lv_alerts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Coin_Interval,
            this.LocalTime,
            this.CloseTime,
            this.Price,
            this.ML,
            this.MH,
            this.DML,
            this.DMH,
            this.ST_SF,
            this.LT_SF,
            this.Signal});
            this.lv_alerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_alerts.HideSelection = false;
            this.lv_alerts.Location = new System.Drawing.Point(0, 0);
            this.lv_alerts.Name = "lv_alerts";
            this.lv_alerts.Size = new System.Drawing.Size(885, 231);
            this.lv_alerts.TabIndex = 0;
            this.lv_alerts.UseCompatibleStateImageBehavior = false;
            this.lv_alerts.View = System.Windows.Forms.View.Details;
            // 
            // Coin_Interval
            // 
            this.Coin_Interval.Text = "Coin_Interval";
            this.Coin_Interval.Width = 100;
            // 
            // LocalTime
            // 
            this.LocalTime.Text = "LocalTime";
            this.LocalTime.Width = 150;
            // 
            // CloseTime
            // 
            this.CloseTime.Text = "CloseTime";
            this.CloseTime.Width = 150;
            // 
            // Price
            // 
            this.Price.Text = "Price";
            // 
            // ML
            // 
            this.ML.Text = "ML";
            // 
            // MH
            // 
            this.MH.Text = "MH";
            // 
            // DML
            // 
            this.DML.Text = "DML";
            // 
            // DMH
            // 
            this.DMH.Text = "DMH";
            // 
            // ST_SF
            // 
            this.ST_SF.Text = "ST_SF";
            // 
            // LT_SF
            // 
            this.LT_SF.Text = "LT_SF";
            // 
            // Signal
            // 
            this.Signal.Text = "Signal";
            // 
            // CoinAlertForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 231);
            this.ControlBox = false;
            this.Controls.Add(this.lv_alerts);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CoinAlertForm";
            this.Opacity = 0.7D;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.Text = "CoinAlertForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CoinAlertForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_alerts;
        private System.Windows.Forms.ColumnHeader Coin_Interval;
        private System.Windows.Forms.ColumnHeader LocalTime;
        private System.Windows.Forms.ColumnHeader CloseTime;
        private System.Windows.Forms.ColumnHeader Price;
        private System.Windows.Forms.ColumnHeader ML;
        private System.Windows.Forms.ColumnHeader MH;
        private System.Windows.Forms.ColumnHeader DML;
        private System.Windows.Forms.ColumnHeader DMH;
        private System.Windows.Forms.ColumnHeader ST_SF;
        private System.Windows.Forms.ColumnHeader LT_SF;
        private System.Windows.Forms.ColumnHeader Signal;
    }
}