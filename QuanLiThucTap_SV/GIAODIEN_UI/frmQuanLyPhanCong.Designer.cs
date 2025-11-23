namespace QuanLiThucTap_SV.GIAODIEN_UI
{
    partial class frmQuanLyPhanCong
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtfindPhanCong = new System.Windows.Forms.TextBox();
            this.dgvPhanCong = new System.Windows.Forms.DataGridView();
            this.btnThemPhanCong = new System.Windows.Forms.Button();
            this.btnCapNhatPhanCong = new System.Windows.Forms.Button();
            this.btnXoaPhanCong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanCong)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tìm kiếm phân công";
            // 
            // txtfindPhanCong
            // 
            this.txtfindPhanCong.Location = new System.Drawing.Point(232, 18);
            this.txtfindPhanCong.Name = "txtfindPhanCong";
            this.txtfindPhanCong.Size = new System.Drawing.Size(511, 26);
            this.txtfindPhanCong.TabIndex = 11;
            // 
            // dgvPhanCong
            // 
            this.dgvPhanCong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhanCong.Location = new System.Drawing.Point(12, 84);
            this.dgvPhanCong.Name = "dgvPhanCong";
            this.dgvPhanCong.RowHeadersWidth = 62;
            this.dgvPhanCong.RowTemplate.Height = 28;
            this.dgvPhanCong.Size = new System.Drawing.Size(591, 348);
            this.dgvPhanCong.TabIndex = 10;
            // 
            // btnThemPhanCong
            // 
            this.btnThemPhanCong.Location = new System.Drawing.Point(643, 106);
            this.btnThemPhanCong.Name = "btnThemPhanCong";
            this.btnThemPhanCong.Size = new System.Drawing.Size(145, 44);
            this.btnThemPhanCong.TabIndex = 9;
            this.btnThemPhanCong.Text = "Thêm";
            this.btnThemPhanCong.UseVisualStyleBackColor = true;
            // 
            // btnCapNhatPhanCong
            // 
            this.btnCapNhatPhanCong.Location = new System.Drawing.Point(643, 242);
            this.btnCapNhatPhanCong.Name = "btnCapNhatPhanCong";
            this.btnCapNhatPhanCong.Size = new System.Drawing.Size(145, 44);
            this.btnCapNhatPhanCong.TabIndex = 8;
            this.btnCapNhatPhanCong.Text = "Cập Nhật";
            this.btnCapNhatPhanCong.UseVisualStyleBackColor = true;
            // 
            // btnXoaPhanCong
            // 
            this.btnXoaPhanCong.Location = new System.Drawing.Point(643, 379);
            this.btnXoaPhanCong.Name = "btnXoaPhanCong";
            this.btnXoaPhanCong.Size = new System.Drawing.Size(145, 44);
            this.btnXoaPhanCong.TabIndex = 7;
            this.btnXoaPhanCong.Text = "Xóa";
            this.btnXoaPhanCong.UseVisualStyleBackColor = true;
            // 
            // frmQuanLyPhanCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtfindPhanCong);
            this.Controls.Add(this.dgvPhanCong);
            this.Controls.Add(this.btnThemPhanCong);
            this.Controls.Add(this.btnCapNhatPhanCong);
            this.Controls.Add(this.btnXoaPhanCong);
            this.Name = "frmQuanLyPhanCong";
            this.Text = "frmQuanLyPhanCong";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanCong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtfindPhanCong;
        private System.Windows.Forms.DataGridView dgvPhanCong;
        private System.Windows.Forms.Button btnThemPhanCong;
        private System.Windows.Forms.Button btnCapNhatPhanCong;
        private System.Windows.Forms.Button btnXoaPhanCong;
    }
}