using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace BinanceApp
{
    public partial class LoginForm : Telerik.WinControls.UI.RadForm
    {
        public string UserName { get; set; }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (tb_username.Text.ToLower() == "guest" && tb_password.Text == "Sabafam1400")
            {
                this.DialogResult = DialogResult.OK;
                UserName = "Guest";
                this.Close();
                return;
            }
            if(tb_username.Text.ToLower()== "admin" || tb_username.Text.ToLower()=="administrator")
            {
                if(tb_password.Text=="Sabafam@2021")
                {
                    this.DialogResult = DialogResult.OK;
                    UserName = "Admin";
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Username or password is incorrect.", "Warning", MessageBoxButtons.OK);
            return;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tb_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_ok_Click(sender, e);
        }
    }
}
