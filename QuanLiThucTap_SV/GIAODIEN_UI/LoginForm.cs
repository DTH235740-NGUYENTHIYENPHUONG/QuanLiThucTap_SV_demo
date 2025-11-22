using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLiThucTap_SV.BLL;
using System.Windows.Forms;

namespace QuanLiThucTap_SV
{
    public partial class LoginForm : Form
    {
        private UserBLL userBLL = new UserBLL(); // Khai báo UserBLL để sử dụng

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

            string userRole;
            int userID;

            // 🔑 GỌI HÀM TỪ BLL
            if (userBLL.CheckLogin(username, password, out userRole, out userID)) // Thay thế Database.CheckLogin
            {
                // 1. LƯU THÔNG TIN VÀO SESSION
                Session.MaUser = userID;
                Session.Role = userRole;

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();

                // 2. PHÂN LUỒNG MỞ FORM (Giữ nguyên logic switch case của bạn)
                switch (userRole)
                {
                    case "Admin":
                        MainForm adminMain = new MainForm();
                        adminMain.Show();
                        break;
                    case "GiangVien":
                        LecturerForm gvPortal = new LecturerForm();
                        gvPortal.Show();
                        break;
                    case "CongTy":
                        CompanyForm ctPortal = new CompanyForm();
                        ctPortal.Show();
                        break;
                    case "SinhVien":
                        StudentPortalForm svPortal = new StudentPortalForm();
                        svPortal.Show();
                        break;
                    default:
                        MessageBox.Show("Vai trò không hợp lệ.");
                        this.Show();
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
