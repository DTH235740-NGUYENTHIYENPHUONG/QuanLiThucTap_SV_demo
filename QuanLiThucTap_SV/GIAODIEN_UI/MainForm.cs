using QuanLiThucTap_SV.GIAODIEN_UI;
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


        public MainForm()
        {
            InitializeComponent();
            this.Text = "Quản Lý Hệ Thống - Administrator";
        }

        
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        // CHỨC NĂNG HỆ THỐNG
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.Logout(); // Xóa thông tin Session
            this.Close();    // Đóng form 
            LoginForm login = new LoginForm();
            login.Show();    // Mở lại Form Đăng nhập
        }

        private void thoátHệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.Logout();
            this.Close();
        }

        private void quảnLíNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau doiMatKhauForm = new frmDoiMatKhau();
            doiMatKhauForm.ShowDialog();
        }

        // CHỨC NĂNG QUẢN LÝ

        private void tsmiTaiKhoan_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra nếu Form đã mở, thì kích hoạt nó
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmQuanLyUsers)
                {
                    form.Activate();
                    return;
                }
            }

            // 2. Nếu Form chưa mở, tạo và mở Form mới
            frmQuanLyUsers userForm = new frmQuanLyUsers();
            userForm.MdiParent = this; // Đặt Form cha
            userForm.Show();
        }

        private void tsmiPhanCongThucTap_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra nếu Form đã mở, thì kích hoạt nó
            foreach (Form form in this.MdiChildren)
            {
                if (form is frmQuanLyPhanCong)
                {
                    form.Activate();
                    return;
                }
            }
            // 2. Nếu Form chưa mở, tạo và mở Form mới
            frmQuanLyPhanCong pcForm = new frmQuanLyPhanCong();
            pcForm.MdiParent = this; // Đặt Form cha
            pcForm.Show();
        }
    }
}
