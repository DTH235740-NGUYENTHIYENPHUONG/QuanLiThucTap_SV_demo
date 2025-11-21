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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            SetupFlatStyle();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            // Khai báo biến để nhận kết quả từ Database
            string userRole;
            int userID;

            // Gọi hàm CheckLogin đã nâng cấp (nhận 4 tham số)
            if (Database.CheckLogin(username, password, out userRole, out userID))
            {
                // 1. LƯU THÔNG TIN VÀO SESSION
                Session.MaUser = userID;
                Session.Role = userRole;

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide(); // Ẩn form Login

                // 2. PHÂN LUỒNG MỞ FORM
                switch (userRole)
                {
                    case "Admin":
                        // Admin thường không cần ID cụ thể, chỉ cần quyền
                        MainForm adminMain = new MainForm();
                        adminMain.Show();
                        break;

                    case "GiangVien":
                        // Form giảng viên sẽ tự dùng Session.MaUser để tìm thông tin của mình
                        LecturerForm gvForm = new LecturerForm();
                        gvForm.Show();
                        break;

                    case "CongTy":
                        CompanyForm ctForm = new CompanyForm();
                        ctForm.Show();
                        break;

                    case "SinhVien":
                        // Form SV sẽ tự dùng Session.MaUser để load điểm
                        StudentPortalForm svPortal = new StudentPortalForm();
                        svPortal.Show();
                        break;

                    default:
                        MessageBox.Show("Vai trò không hợp lệ.");
                        this.Show(); // Hiện lại form login nếu lỗi
                        break;
                }
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát khỏi ứng dụng?", "Xác Nhận Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
