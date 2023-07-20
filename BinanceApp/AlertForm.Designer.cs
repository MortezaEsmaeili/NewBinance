
namespace BinanceApp
{
    partial class AlertForm
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
            this.AlertView = new System.Windows.Forms.Integration.ElementHost();
            this.alertCollection1 = new BinanceApp.AlertCoinCollection();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // AlertView
            // 
            this.AlertView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AlertView.Location = new System.Drawing.Point(0, 0);
            this.AlertView.Name = "AlertView";
            this.AlertView.Size = new System.Drawing.Size(350, 416);
            this.AlertView.TabIndex = 1;
            this.AlertView.Text = "elementHost2";
            this.AlertView.Child = this.alertCollection1;
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(350, 416);
            this.ControlBox = false;
            this.Controls.Add(this.AlertView);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(100, 50);
            this.Name = "AlertForm";
            this.Opacity = 0.8D;
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MaxSize = new System.Drawing.Size(800, 600);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowItemToolTips = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AlertForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AlertForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Integration.ElementHost AlertView;
        private AlertCoinCollection alertCollection1;
    }
}