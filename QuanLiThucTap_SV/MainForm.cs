using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThucTap_SV
{
    public partial class MainForm : Form
    {

        private string loggedInUserRole;

        // 1. Hàm khởi tạo nhận Role (Sẽ gọi từ LoginForm)
        public MainForm(string role)
        {
            InitializeComponent();
            this.loggedInUserRole = role;
            this.Text = $"Hệ Thống Quản Lý Thực Tập - Quyền: {role}";

            // Gọi hàm Phân quyền
            ApplyPermissions(role);
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void ApplyPermissions(string role)
        {
            // Mặc định ẩn các chức năng nhạy cảm hoặc không dùng chung
            tsmiQLNguoiDung.Visible = false;
            tsmiCongTy.Visible = false;
            tsmiPhanCongTT.Visible = false;

            switch (role)
            {
                case "Admin":
                    // Admin được xem và quản lý tất cả
                    tsmiQLNguoiDung.Visible = true;
                    tsmiCongTy.Visible = true;
                    tsmiPhanCongTT.Visible = true;
                    tsmiDanhMuc.Visible = true; // Hiện menu cha
                    tsmiNghiepVu.Visible = true;
                    break;

                case "CanBo": // Cán bộ Quản lý Thực tập
                    // Cán bộ quản lý danh mục và phân công
                    tsmiCongTy.Visible = true;
                    tsmiPhanCongTT.Visible = true;
                    tsmiDanhMuc.Visible = true;
                    tsmiNghiepVu.Visible = true;
                    break;

                case "GiangVien": // Giáo viên Giám sát
                    // Giảng viên chỉ cần xem danh sách sinh viên, không quản lý CSDL
                    tsmiDanhMuc.Visible = true;
                    tsmiSinhVien.Visible = true;
                    tsmiCongTy.Visible = false; // Không được quản lý/thêm Công ty
                    tsmiNghiepVu.Visible = false; // Không được Phân công

                    // Nếu bạn có Menu "Báo cáo điểm", thì giữ lại
                    // tsmiBaoCaoDiem.Visible = true;
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void tsmiDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Mở lại Form Đăng nhập
                LoginForm login = new LoginForm();
                login.Show();

                // Đóng Form Main
                this.Close();
            }
        }

        private void tsmiSinhVien_Click(object sender, EventArgs e)
        {
            // Kiểm tra Form đã mở chưa
            // MDI container thường chỉ cho phép mở một instance của mỗi form con
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is FrmSinhVien)
                {
                    frm.Activate(); // Kích hoạt form nếu đã mở
                    return;
                }
            }

            // Form chưa mở, tạo mới và hiển thị
            FrmSinhVien frmSV = new FrmSinhVien();
            frmSV.MdiParent = this; // Đặt MainForm làm cha 
            frmSV.Show();
        }
    }
}
