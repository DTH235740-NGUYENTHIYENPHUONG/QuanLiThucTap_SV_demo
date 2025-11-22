using QuanLiThucTap_SV.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiThucTap_SV.GIAODIEN_UI
{
    public partial class frmDoiMatKhau : Form
    {
        UserBLL userBLL = new UserBLL(); // Tạo đối tượng UserBLL để gọi phương thức đổi mật khẩu
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPass.Text;
            string newPass = txtNewPass.Text;
            string confirmPass = txtConfirmPass.Text;

            // 1. KIỂM TRA RÀNG BUỘC (Validation)
            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các trường.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu mới và Nhập lại mật khẩu không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. GỌI BLL ĐỂ ĐỔI MẬT KHẨU
            int maUser = Session.MaUser; // Lấy ID người dùng hiện tại từ Session

            if (maUser == -1)
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng đang đăng nhập.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            bool success = userBLL.ChangePasscode(maUser, oldPass, newPass);

            if (success)
            {
                MessageBox.Show("Đổi mật khẩu thành công! Bạn cần đăng nhập lại với mật khẩu mới.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Đóng form đổi mật khẩu và form chính
                this.Close();
                Application.Exit(); // Đóng toàn bộ ứng dụng để yêu cầu đăng nhập lại
                // Hoặc: Mở lại LoginForm nếu bạn quản lý nhiều Form
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
