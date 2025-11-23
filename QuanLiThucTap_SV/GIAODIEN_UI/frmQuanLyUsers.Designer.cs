namespace QuanLiThucTap_SV.GIAODIEN_UI
{
    partial class frmQuanLyUsers
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
            this.btnXoaUser = new System.Windows.Forms.Button();
            this.btnCapNhatUser = new System.Windows.Forms.Button();
            this.btnThemUser = new System.Windows.Forms.Button();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXoaUser
            // 
            this.btnXoaUser.Location = new System.Drawing.Point(643, 372);
            this.btnXoaUser.Name = "btnXoaUser";
            this.btnXoaUser.Size = new System.Drawing.Size(145, 44);
            this.btnXoaUser.TabIndex = 1;
            this.btnXoaUser.Text = "Xóa";
            this.btnXoaUser.UseVisualStyleBackColor = true;
            this.btnXoaUser.Click += new System.EventHandler(this.btnXoaUser_Click);
            // 
            // btnCapNhatUser
            // 
            this.btnCapNhatUser.Location = new System.Drawing.Point(643, 214);
            this.btnCapNhatUser.Name = "btnCapNhatUser";
            this.btnCapNhatUser.Size = new System.Drawing.Size(145, 44);
            this.btnCapNhatUser.TabIndex = 2;
            this.btnCapNhatUser.Text = "Cập Nhật";
            this.btnCapNhatUser.UseVisualStyleBackColor = true;
            this.btnCapNhatUser.Click += new System.EventHandler(this.btnCapNhatUser_Click);
            // 
            // btnThemUser
            // 
            this.btnThemUser.Location = new System.Drawing.Point(643, 46);
            this.btnThemUser.Name = "btnThemUser";
            this.btnThemUser.Size = new System.Drawing.Size(145, 44);
            this.btnThemUser.TabIndex = 3;
            this.btnThemUser.Text = "Thêm";
            this.btnThemUser.UseVisualStyleBackColor = true;
            this.btnThemUser.Click += new System.EventHandler(this.btnThemUser_Click);
            // 
            // dgvUser
            // 
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Location = new System.Drawing.Point(12, 12);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersWidth = 62;
            this.dgvUser.RowTemplate.Height = 28;
            this.dgvUser.Size = new System.Drawing.Size(625, 413);
            this.dgvUser.TabIndex = 4;
            // 
            // frmQuanLyUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.btnThemUser);
            this.Controls.Add(this.btnCapNhatUser);
            this.Controls.Add(this.btnXoaUser);
            this.Name = "frmQuanLyUsers";
            this.Text = "frmQuanLyUsers";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnXoaUser;
        private System.Windows.Forms.Button btnCapNhatUser;
        private System.Windows.Forms.Button btnThemUser;
        private System.Windows.Forms.DataGridView dgvUser;
    }
}