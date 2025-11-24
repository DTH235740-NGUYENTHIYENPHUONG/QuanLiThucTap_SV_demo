namespace QuanLiThucTap_SV
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLíNgườiDùngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátHệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvUser = new System.Windows.Forms.DataGridView();
            this.tsmiThem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCapNhat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXoa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHeThong,
            this.tsmiDanhMuc});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1193, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // tsmiHeThong
            // 
            this.tsmiHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLíNgườiDùngToolStripMenuItem,
            this.đăngXuấtToolStripMenuItem,
            this.thoátHệThốngToolStripMenuItem});
            this.tsmiHeThong.Name = "tsmiHeThong";
            this.tsmiHeThong.Size = new System.Drawing.Size(106, 29);
            this.tsmiHeThong.Text = "Hệ Thống";
            this.tsmiHeThong.Click += new System.EventHandler(this.tsmiHeThong_Click);
            // 
            // quảnLíNgườiDùngToolStripMenuItem
            // 
            this.quảnLíNgườiDùngToolStripMenuItem.Name = "quảnLíNgườiDùngToolStripMenuItem";
            this.quảnLíNgườiDùngToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.quảnLíNgườiDùngToolStripMenuItem.Text = "Đổi Mật Khẩu";
            this.quảnLíNgườiDùngToolStripMenuItem.Click += new System.EventHandler(this.quảnLíNgườiDùngToolStripMenuItem_Click);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng Xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // thoátHệThốngToolStripMenuItem
            // 
            this.thoátHệThốngToolStripMenuItem.Name = "thoátHệThốngToolStripMenuItem";
            this.thoátHệThốngToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.thoátHệThốngToolStripMenuItem.Text = "Thoát Hệ Thống";
            this.thoátHệThốngToolStripMenuItem.Click += new System.EventHandler(this.thoátHệThốngToolStripMenuItem_Click);
            // 
            // tsmiDanhMuc
            // 
            this.tsmiDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiThem,
            this.tsmiCapNhat,
            this.tsmiXoa});
            this.tsmiDanhMuc.Name = "tsmiDanhMuc";
            this.tsmiDanhMuc.Size = new System.Drawing.Size(88, 29);
            this.tsmiDanhMuc.Text = "Quản Lí";
            // 
            // dgvUser
            // 
            this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUser.Location = new System.Drawing.Point(0, 33);
            this.dgvUser.Name = "dgvUser";
            this.dgvUser.RowHeadersWidth = 62;
            this.dgvUser.RowTemplate.Height = 28;
            this.dgvUser.Size = new System.Drawing.Size(1193, 648);
            this.dgvUser.TabIndex = 8;
            // 
            // tsmiThem
            // 
            this.tsmiThem.Name = "tsmiThem";
            this.tsmiThem.Size = new System.Drawing.Size(270, 34);
            this.tsmiThem.Text = "Thêm";
            this.tsmiThem.Click += new System.EventHandler(this.tsmiThem_Click);
            // 
            // tsmiCapNhat
            // 
            this.tsmiCapNhat.Name = "tsmiCapNhat";
            this.tsmiCapNhat.Size = new System.Drawing.Size(270, 34);
            this.tsmiCapNhat.Text = "Cập Nhật";
            this.tsmiCapNhat.Click += new System.EventHandler(this.tsmiCapNhat_Click);
            // 
            // tsmiXoa
            // 
            this.tsmiXoa.Name = "tsmiXoa";
            this.tsmiXoa.Size = new System.Drawing.Size(270, 34);
            this.tsmiXoa.Text = "Xóa";
            this.tsmiXoa.Click += new System.EventHandler(this.tsmiXoa_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 681);
            this.Controls.Add(this.dgvUser);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lí Thực Tập Sinh Viên";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiHeThong;
        private System.Windows.Forms.ToolStripMenuItem tsmiDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem quảnLíNgườiDùngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátHệThốngToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvUser;
        private System.Windows.Forms.ToolStripMenuItem tsmiThem;
        private System.Windows.Forms.ToolStripMenuItem tsmiCapNhat;
        private System.Windows.Forms.ToolStripMenuItem tsmiXoa;
    }
}