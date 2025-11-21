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
            string userRole; // Biến để lưu quyền người dùng

            if(Database.CheckLogin(username, password, out userRole))
{
                this.Hide(); // Ẩn form Login

                // PHÂN LUỒNG MỞ FORM THEO VAI TRÒ
                switch (userRole)
                {
                    case "Admin":
                        MainForm adminMain = new MainForm("Admin"); // MainForm chứa MenuStrip QL toàn hệ thống
                        adminMain.Show();
                        break;

                    case "GiangVien":
                        LecturerForm gvForm = new LecturerForm("GiangVien"); // Form quản lý SV Giám sát
                        gvForm.Show();
                        break;

                    case "CongTy":
                        CompanyForm ctForm = new CompanyForm(); // Form quản lý SV Thực tập
                        ctForm.Show();
                        break;

                    case "SinhVien":
                        StudentPortalForm svPortal = new StudentPortalForm(); // Form thông tin SV
                        svPortal.Show();
                        break;

                    default:
                        MessageBox.Show("Vai trò không xác định. Vui lòng liên hệ quản trị viên.");
                        new LoginForm().Show(); // Mở lại Login
                        break;
                }
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
