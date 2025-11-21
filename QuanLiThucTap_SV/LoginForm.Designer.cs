using System.Windows.Forms;

namespace QuanLiThucTap_SV
{
    partial class LoginForm
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
            this.Text = "Đăng nhập - Internship Manager";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(420, 220);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            lblTitle = new Label() { Text = "Đăng nhập", Left = 20, Top = 10, AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 14) };
            var lblUser = new Label() { Text = "Tài khoản", Left = 20, Top = 50, AutoSize = true };
            txtUser = new TextBox() { Left = 120, Top = 45, Width = 260 };
            var lblPass = new Label() { Text = "Mật khẩu", Left = 20, Top = 90, AutoSize = true };
            txtPass = new TextBox() { Left = 120, Top = 85, Width = 260, PasswordChar = '●' };

            btnLogin = new Button() { Text = "Đăng nhập", Left = 120, Top = 130, Width = 120, Height = 35 };
            btnLogin.Click += btnLogin_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUser);
            this.Controls.Add(txtUser);
            this.Controls.Add(lblPass);
            this.Controls.Add(txtPass);
            this.Controls.Add(btnLogin);
        }

        private void SetupFlatStyle()
        {
            this.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.BackColor = System.Drawing.Color.FromArgb(32, 150, 243);
            btnLogin.ForeColor = System.Drawing.Color.White;
        }

        #endregion

        private TextBox txtUser;
        private TextBox txtPass;
        private Button btnLogin;
        private Label lblTitle;
    }
}

