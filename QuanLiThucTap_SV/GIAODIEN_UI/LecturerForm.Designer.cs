using System.Windows.Forms;

namespace QuanLiThucTap_SV
{
    partial class LecturerForm
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
            this.dgvSinhVien = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMaKhoa = new System.Windows.Forms.Label();
            this.lblMaGV = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSinhVien
            // 
            this.dgvSinhVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSinhVien.ColumnHeadersHeight = 34;
            this.dgvSinhVien.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgvSinhVien.Location = new System.Drawing.Point(314, 0);
            this.dgvSinhVien.Name = "dgvSinhVien";
            this.dgvSinhVien.ReadOnly = true;
            this.dgvSinhVien.RowHeadersWidth = 62;
            this.dgvSinhVien.Size = new System.Drawing.Size(730, 641);
            this.dgvSinhVien.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMaKhoa);
            this.groupBox1.Controls.Add(this.lblMaGV);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblEmail);
            this.groupBox1.Controls.Add(this.lblHoTen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 325);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin người dùng";
            // 
            // lblMaKhoa
            // 
            this.lblMaKhoa.AutoSize = true;
            this.lblMaKhoa.Location = new System.Drawing.Point(103, 190);
            this.lblMaKhoa.Name = "lblMaKhoa";
            this.lblMaKhoa.Size = new System.Drawing.Size(80, 20);
            this.lblMaKhoa.TabIndex = 13;
            this.lblMaKhoa.Text = "(Mã khoa)";
            // 
            // lblMaGV
            // 
            this.lblMaGV.AutoSize = true;
            this.lblMaGV.Location = new System.Drawing.Point(103, 37);
            this.lblMaGV.Name = "lblMaGV";
            this.lblMaGV.Size = new System.Drawing.Size(107, 20);
            this.lblMaGV.TabIndex = 12;
            this.lblMaGV.Text = "(Mã giáo viên)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "MaGV";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "MaKhoa";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(103, 265);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(131, 20);
            this.lblEmail.TabIndex = 9;
            this.lblEmail.Text = "(thông tin liên hệ)";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(103, 117);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(125, 20);
            this.lblHoTen.TabIndex = 8;
            this.lblHoTen.Text = "(tên người dùng)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 265);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Contact:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "User:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnXoa);
            this.groupBox2.Controls.Add(this.btnDangXuat);
            this.groupBox2.Controls.Add(this.btnDoiMatKhau);
            this.groupBox2.Controls.Add(this.btnSua);
            this.groupBox2.Location = new System.Drawing.Point(12, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 286);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Công cụ";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(0, 95);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(284, 34);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Location = new System.Drawing.Point(0, 240);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(284, 34);
            this.btnDangXuat.TabIndex = 4;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.Location = new System.Drawing.Point(0, 200);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(284, 34);
            this.btnDoiMatKhau.TabIndex = 3;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = true;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click_1);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(0, 55);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(284, 34);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(314, 641);
            this.panel1.TabIndex = 4;
            // 
            // LecturerForm
            // 
            this.ClientSize = new System.Drawing.Size(1044, 641);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvSinhVien);
            this.Name = "LecturerForm";
            this.Text = "Quản lý sinh viên - Giảng viên";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSinhVien)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSinhVien;
        private GroupBox groupBox1;
        private Label lblEmail;
        private Label lblHoTen;
        private Label label3;
        private Label label2;
        private GroupBox groupBox2;
        private Button btnDoiMatKhau;
        private Button btnSua;
        private Button btnDangXuat;
        private Panel panel1;
        private Label lblMaKhoa;
        private Label lblMaGV;
        private Label label6;
        private Label label1;
        private Button btnXoa;
    }
}