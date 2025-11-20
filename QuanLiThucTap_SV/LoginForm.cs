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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); 
            string password = txtPassword.Text.Trim(); 
            string userRole; // Biến để lưu quyền người dùng

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Kiểm tra Đăng nhập
            if (Database.CheckLogin(username, password, out userRole))
            {
                // 2. Thành công: Mở Form Trang Chủ
                MessageBox.Show($"Đăng nhập thành công với quyền: {userRole}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tạo và hiển thị Form Trang Chủ (Giả sử bạn đặt tên là MainForm)
                MainForm mainForm = new MainForm(userRole); // Truyền userRole vào MainForm
                mainForm.Show();

                // Ẩn Form Đăng nhập hiện tại
                this.Hide();

                // **TODO:** Bạn nên truyền thông tin userRole vào MainForm để xử lý phân quyền MenuStrip
            }
            else
            {
                // 3. Thất bại
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear(); // Xóa mật khẩu để người dùng nhập lại
                txtUsername.Focus(); // Đặt con trỏ vào ô Tên đăng nhập
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
