using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.BLL;
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
        private AdminBLL adminBLL = new AdminBLL(); // BLL để thao tác với dữ liệu người dùng
        private DataTable dtUsers; // DataTable để theo dõi sự thay đổi (rất quan trọng)

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Quản Lý Hệ Thống - Administrator";
            LoadUserData();
        }

        // ===============================================
        // I. LOAD DỮ LIỆU
        // ===============================================

        private void LoadUserData()
        {
            try
            {
                // Lấy dữ liệu và gán cho DataTable và DGV
                dtUsers = adminBLL.GetAllUsers();
                dgvUser.DataSource = dtUsers;
                SetupDGVColumns();

                // Sau khi tải, chấp nhận tất cả thay đổi để reset trạng thái theo dõi của DataTable
                dtUsers.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDGVColumns()
        {
            // Thiết lập tên hiển thị và ẩn các cột cần thiết
            dgvUser.Columns["MaUser"].Visible = false; // Ẩn Mã User
            dgvUser.Columns["Username"].HeaderText = "Tên đăng nhập";
            dgvUser.Columns["HoTen"].HeaderText = "Họ và Tên";
            dgvUser.Columns["Role"].HeaderText = "Vai trò";
            dgvUser.Columns["TrangThai"].HeaderText = "Trạng thái (1:Mở)";

            // Cho phép Admin sửa HoTen, Role, TrangThai trực tiếp trên DGV
            dgvUser.ReadOnly = false;
            dgvUser.AllowUserToAddRows = false;

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


        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tsmiHeThong_Click(object sender, EventArgs e)
        {

        }

        private void tsmiThem_Click(object sender, EventArgs e)
        {
            // Kết thúc việc chỉnh sửa ở dòng hiện tại (nếu có)
            dgvUser.EndEdit();

            // Thêm một dòng trống vào DataTable
            DataRow newRow = dtUsers.NewRow();
            dtUsers.Rows.Add(newRow);

            // Cuộn DGV xuống dòng cuối cùng vừa thêm
            dgvUser.FirstDisplayedScrollingRowIndex = dgvUser.Rows.Count - 1;
        }

        private void tsmiCapNhat_Click(object sender, EventArgs e)
        {
            dgvUser.EndEdit(); // Kết thúc chỉnh sửa ở ô hiện tại
            int successfulChanges = 0;

            // 1. Xử lý THÊM MỚI (Các dòng chưa có MaUser)
            // Lặp qua các hàng đang ở trạng thái 'Added' trong DataTable (Dòng mới thêm)
            foreach (DataRow row in dtUsers.Select(null, null, DataViewRowState.Added))
            {
                // Lấy giá trị từ dòng mới
                string username = row["Username"]?.ToString().Trim();
                string hoTen = row["HoTen"]?.ToString().Trim();
                string role = row["Role"]?.ToString();
                string password = "123456"; // Mật khẩu mặc định

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
                {
                    MessageBox.Show($"Dòng mới ({hoTen}) thiếu Tên đăng nhập hoặc Vai trò. Không thể lưu.", "Lỗi nhập liệu");
                    continue;
                }

                try
                {
                    if (adminBLL.AddUser(username, password, hoTen, role) > 0)
                    {
                        successfulChanges++;
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    MessageBox.Show($"Lỗi: Tên đăng nhập '{username}' đã tồn tại.", "Lỗi trùng lặp");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi thêm tài khoản {username}: {ex.Message}", "Lỗi hệ thống");
                }
            }

            // 2. Xử lý SỬA (Các dòng đã bị thay đổi)
            // Lấy các hàng ở trạng thái 'Modified'
            DataTable modifiedRows = dtUsers.GetChanges(DataRowState.Modified);
            if (modifiedRows != null)
            {
                foreach (DataRow row in modifiedRows.Rows)
                {
                    // Lấy các giá trị mới (Current)
                    int maUser = Convert.ToInt32(row["MaUser", DataRowVersion.Current]);
                    string hoTen = row["HoTen", DataRowVersion.Current].ToString().Trim();
                    string role = row["Role", DataRowVersion.Current].ToString();
                    bool trangThai = Convert.ToBoolean(row["TrangThai", DataRowVersion.Current]);

                    if (adminBLL.UpdateUser(maUser, hoTen, role, trangThai) > 0)
                    {
                        successfulChanges++;
                    }
                }
            }

            if (successfulChanges > 0)
            {
                MessageBox.Show($"Đã lưu thành công {successfulChanges} thay đổi/thêm mới.", "Thành công");
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào cần lưu.", "Thông báo");
            }

            LoadUserData(); // Tải lại để đồng bộ DGV với CSDL (reset trạng thái DataTable)

        }


        private void tsmiXoa_Click(object sender, EventArgs e)
        {
            if (dgvUser.SelectedRows.Count == 0 || dgvUser.SelectedRows[0].IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản hợp lệ để xóa.", "Cảnh báo");
                return;
            }

            DataGridViewRow selectedRow = dgvUser.SelectedRows[0];
            int maUser = Convert.ToInt32(selectedRow.Cells["MaUser"].Value);
            string username = selectedRow.Cells["Username"].Value.ToString();

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa vĩnh viễn tài khoản '{username}' không? (Thao tác này có thể gây lỗi khóa ngoại với các bảng liên quan!)",
                                                   "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    if (adminBLL.DeleteUser(maUser) > 0)
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo");
                        // Xóa thành công, tải lại dữ liệu để loại bỏ dòng đã xóa khỏi DGV
                        LoadUserData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản. Kiểm tra lại BLL hoặc các ràng buộc khóa ngoại.", "Lỗi Xóa");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Lỗi CSDL: Không thể xóa do ràng buộc Khóa ngoại. Hãy dùng chức năng Khóa (TrangThai=0). Chi tiết: {ex.Message}", "Lỗi Xóa");
                }
            }
        }
    }
}
