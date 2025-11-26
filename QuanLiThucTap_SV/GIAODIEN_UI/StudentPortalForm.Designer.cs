using System;
using System.IO;
using System.Windows.Forms;

namespace QuanLiThucTap_SV
{
    partial class StudentPortalForm
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
        private Label lblInfo;
        private Label lblDiem;
        private Button btnUpload;
        private string reportsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");

        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblDiem = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDoiMatKhau = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnThemBaoCao = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvStudentInfo = new System.Windows.Forms.DataGridView();
            this.lblDiemGVGS = new System.Windows.Forms.Label();
            this.lblDiemCongTy = new System.Windows.Forms.Label();
            this.lblDiemTongKet = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMaLop = new System.Windows.Forms.Label();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSua = new System.Windows.Forms.Button();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Controls.Add(this.lblInfo);
            this.panel.Controls.Add(this.lblDiem);
            this.panel.Controls.Add(this.btnUpload);
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 100);
            this.panel.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(100, 23);
            this.lblInfo.TabIndex = 0;
            // 
            // lblDiem
            // 
            this.lblDiem.Location = new System.Drawing.Point(0, 0);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(100, 23);
            this.lblDiem.TabIndex = 1;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(0, 0);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1302, 734);
            this.panel1.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.btnThemBaoCao);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 448);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1302, 286);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Công cụ";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDoiMatKhau);
            this.panel2.Controls.Add(this.btnDangXuat);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(955, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 217);
            this.panel2.TabIndex = 5;
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(0, 124);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(344, 46);
            this.btnDoiMatKhau.TabIndex = 3;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.UseVisualStyleBackColor = true;
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click_1);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 170);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(344, 47);
            this.btnDangXuat.TabIndex = 4;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click_1);
            // 
            // btnThemBaoCao
            // 
            this.btnThemBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThemBaoCao.Location = new System.Drawing.Point(3, 22);
            this.btnThemBaoCao.Name = "btnThemBaoCao";
            this.btnThemBaoCao.Size = new System.Drawing.Size(1296, 44);
            this.btnThemBaoCao.TabIndex = 0;
            this.btnThemBaoCao.Text = "Thêm báo cáo";
            this.btnThemBaoCao.UseVisualStyleBackColor = true;
            this.btnThemBaoCao.Click += new System.EventHandler(this.btnThemBaoCao_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvStudentInfo);
            this.groupBox1.Controls.Add(this.lblDiemGVGS);
            this.groupBox1.Controls.Add(this.lblDiemCongTy);
            this.groupBox1.Controls.Add(this.lblDiemTongKet);
            this.groupBox1.Controls.Add(this.lblTrangThai);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblMaLop);
            this.groupBox1.Controls.Add(this.lblGioiTinh);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblNgaySinh);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnSua);
            this.groupBox1.Controls.Add(this.lblMaSV);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblContact);
            this.groupBox1.Controls.Add(this.lblUser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1302, 418);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin người dùng";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dgvStudentInfo
            // 
            this.dgvStudentInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudentInfo.ColumnHeadersHeight = 34;
            this.dgvStudentInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvStudentInfo.Location = new System.Drawing.Point(3, 276);
            this.dgvStudentInfo.Name = "dgvStudentInfo";
            this.dgvStudentInfo.ReadOnly = true;
            this.dgvStudentInfo.RowHeadersWidth = 62;
            this.dgvStudentInfo.Size = new System.Drawing.Size(1296, 90);
            this.dgvStudentInfo.TabIndex = 5;
            this.dgvStudentInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentInfo_CellContentClick);
            // 
            // lblDiemGVGS
            // 
            this.lblDiemGVGS.AutoSize = true;
            this.lblDiemGVGS.Location = new System.Drawing.Point(910, 78);
            this.lblDiemGVGS.Name = "lblDiemGVGS";
            this.lblDiemGVGS.Size = new System.Drawing.Size(139, 20);
            this.lblDiemGVGS.TabIndex = 26;
            this.lblDiemGVGS.Text = "(Điểm Giảng Viên)";
            // 
            // lblDiemCongTy
            // 
            this.lblDiemCongTy.AutoSize = true;
            this.lblDiemCongTy.Location = new System.Drawing.Point(910, 123);
            this.lblDiemCongTy.Name = "lblDiemCongTy";
            this.lblDiemCongTy.Size = new System.Drawing.Size(111, 20);
            this.lblDiemCongTy.TabIndex = 25;
            this.lblDiemCongTy.Text = "(Điểm công ty)";
            // 
            // lblDiemTongKet
            // 
            this.lblDiemTongKet.AutoSize = true;
            this.lblDiemTongKet.Location = new System.Drawing.Point(910, 168);
            this.lblDiemTongKet.Name = "lblDiemTongKet";
            this.lblDiemTongKet.Size = new System.Drawing.Size(124, 20);
            this.lblDiemTongKet.TabIndex = 24;
            this.lblDiemTongKet.Text = "(Điểm Tổng Kết)";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(910, 213);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(90, 20);
            this.lblTrangThai.TabIndex = 23;
            this.lblTrangThai.Text = "(Trạng thái)";
            this.lblTrangThai.Click += new System.EventHandler(this.lblTrangThai_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(809, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 20);
            this.label8.TabIndex = 22;
            this.label8.Text = "Trạng Thái:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(824, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Điểm TK:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(823, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Điểm CT:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(795, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Điểm GVGS:";
            // 
            // lblMaLop
            // 
            this.lblMaLop.AutoSize = true;
            this.lblMaLop.Location = new System.Drawing.Point(910, 33);
            this.lblMaLop.Name = "lblMaLop";
            this.lblMaLop.Size = new System.Drawing.Size(66, 20);
            this.lblMaLop.TabIndex = 18;
            this.lblMaLop.Text = "(mã lớp)";
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Location = new System.Drawing.Point(361, 168);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(73, 20);
            this.lblGioiTinh.TabIndex = 17;
            this.lblGioiTinh.Text = "(giới tính)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(837, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "Mã lớp:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(276, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 20);
            this.label9.TabIndex = 14;
            this.label9.Text = "Contact:";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new System.Drawing.Point(361, 123);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(86, 20);
            this.lblNgaySinh.TabIndex = 13;
            this.lblNgaySinh.Text = "(ngày sinh)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(263, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Ngày sinh:";
            // 
            // btnSua
            // 
            this.btnSua.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSua.Location = new System.Drawing.Point(3, 366);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(1296, 49);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa thông tin";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // lblMaSV
            // 
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Location = new System.Drawing.Point(361, 33);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(106, 20);
            this.lblMaSV.TabIndex = 11;
            this.lblMaSV.Text = "(mã sinh viên)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(284, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Mã SV:";
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(361, 213);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(131, 20);
            this.lblContact.TabIndex = 9;
            this.lblContact.Text = "(thông tin liên hệ)";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(361, 78);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(125, 20);
            this.lblUser.TabIndex = 8;
            this.lblUser.Text = "(tên người dùng)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(278, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Giới tính:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "User:";
            // 
            // StudentPortalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1302, 734);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel);
            this.Name = "StudentPortalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sinh viên - Thông tin thực tập";
            this.panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel;
        private Panel panel1;
        private GroupBox groupBox2;
        private Button btnDangXuat;
        private Button btnDoiMatKhau;
        private Button btnSua;
        private Button btnThemBaoCao;
        private GroupBox groupBox1;
        private Label lblContact;
        private Label lblUser;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label lblMaSV;
        private Label lblNgaySinh;
        private Label label7;
        private DataGridView dgvStudentInfo;
        private Label label10;
        private Label label9;
        private Label lblGioiTinh;
        private Label lblMaLop;
        private Label label8;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label lblDiemGVGS;
        private Label lblDiemCongTy;
        private Label lblDiemTongKet;
        private Label lblTrangThai;
        private Panel panel2;
    }
}