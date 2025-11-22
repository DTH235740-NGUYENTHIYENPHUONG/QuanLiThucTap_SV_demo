using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.BLL;
using System;
using System.Data;
using System.Windows.Forms;
// Cần thêm using cho các form Admin/CRUD nếu bạn mở form đó từ đây.

namespace QuanLiThucTap_SV
{
    public partial class LecturerForm : Form
    {
        private GiangVienBLL gvBLL = new GiangVienBLL();
        private PhanCongBLL pcBLL = new PhanCongBLL();
        private UserBLL userBLL = new UserBLL();
        private string currentMaGV = string.Empty; // Biến lưu MaGV hiện tại

        public LecturerForm()
        {
            InitializeComponent();
            LoadGiangVienInfo();
            LoadSinhVienPhanCong();
        }

        // ===============================================
        // A. LOAD THÔNG TIN GIẢNG VIÊN (User: (tên người dùng))
        // ===============================================
        private void LoadGiangVienInfo()
        {
            // Lấy MaUser đã lưu khi đăng nhập thành công
            int maUser = Session.MaUser;

            if (maUser > 0)
            {
                DataTable dt = userBLL.GetGiangVienInfoByMaUser(maUser);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    currentMaGV = row["MaGV"].ToString();

                    // Hiển thị thông tin lên panel bên trái
                    lblMaGV.Text = currentMaGV;
                    lblHoTen.Text = row["HoTen"].ToString();
                    lblMaKhoa.Text = row["MaKhoa"].ToString();
                    lblEmail.Text = row["Email"].ToString();

                    // 💡 Cập nhật tên Form
                    this.Text = "Quản lý sinh viên - Giảng viên: " + row["HoTen"].ToString();
                }
            }
        }

        // ===============================================
        // B. LOAD DANH SÁCH SINH VIÊN ĐƯỢC PHÂN CÔNG
        // ===============================================
        private void LoadSinhVienPhanCong()
        {
            if (string.IsNullOrEmpty(currentMaGV)) return;

            // 🔑 Cập nhật truy vấn để lấy đủ khóa chính cho việc Xóa/Sửa
            string query = @"
                SELECT 
                    pc.MaSV, pc.MaCT, pc.MaGVGS, 
                    sv.HoTen AS TenSV, ct.TenCT, pc.NgayBatDauTT, pc.TrangThai
                FROM phancong pc
                JOIN sinhvien sv ON pc.MaSV = sv.MaSV
                JOIN congty ct ON pc.MaCT = ct.MaCT
                WHERE pc.MaGVGS = @MaGVGS"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaGVGS", currentMaGV)
            };

            DataTable dt = DAL.DBHelper.GetData(query, parameters);
            dgvSinhVien.DataSource = dt;

            // 💡 Ẩn các cột khóa chính không cần thiết cho người dùng
            dgvSinhVien.Columns["MaCT"].Visible = false;
            dgvSinhVien.Columns["MaGVGS"].Visible = false;
            // Đặt cột TrangThai là ComboBox để dễ chỉnh sửa
            // Cần chuyển cột TrangThai thành DataGridViewComboBoxColumn trong thiết kế DGV
        }


        // ===============================================
        // C. CHỨC NĂNG NGHIỆP VỤ (Thêm, Xóa, Sửa - Dành cho Admin)
        // Nếu Giảng viên chỉ được quản lý sinh viên của mình:
        // Cần Form/Control để Giảng viên thêm/xóa/sửa thông tin PHÂN CÔNG, KHÔNG phải thêm/xóa/sửa thông tin gốc SV.
        // ===============================================


        private void btnThem_Click(object sender, EventArgs e)
        {

            
        }



        // ===============================================
        // D. CẬP NHẬT ĐIỂM
        // ===============================================
        private void btnCapNhatDiem_Click(object sender, EventArgs e)
        {
            // Giảng viên sẽ cập nhật điểm giám sát (`DiemGVGS`) và nhận xét chung vào bảng `ketqua_thuctap`

            // 1. Lấy MaSV, MaCT từ dòng được chọn trên dgvSinhVien
            // 2. Mở Form nhập điểm và nhận xét (frmNhapDiem)
            // 3. Trong frmNhapDiem, gọi hàm trong BLL để UPDATE DiemGVGS

            // Example BLL function:
            /*
            public int UpdateDiemGVGS(string maSV, int maCT, string maGVGS, decimal diem, string nhanxet)
            {
                string query = "UPDATE ketqua_thuctap SET DiemGVGS = @Diem, NhanXetChung = @NhanXet WHERE MaSV = @MaSV AND MaCT = @MaCT AND MaGVGS = @MaGVGS";
                // ... (Parameters và ExecuteNonQuery)
            }
            */
        }

        // ===============================================
        // E. ĐỔI MẬT KHẨU & ĐĂNG XUẤT
        // ===============================================
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            // Mở Form Đổi Mật Khẩu (frmDoiMatKhau)
            // Sử dụng Session.MaUser để xác định tài khoản cần đổi.
        }

        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa khỏi danh sách giám sát.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này khỏi danh sách giám sát? Thao tác này sẽ xóa bản ghi Phân công và Kết quả!",
                                                   "Xác nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Lấy dữ liệu từ hàng được chọn
                DataGridViewRow selectedRow = dgvSinhVien.SelectedRows[0];
                try
                {
                    string maSV = selectedRow.Cells["MaSV"].Value.ToString();
                    int maCT = Convert.ToInt32(selectedRow.Cells["MaCT"].Value);
                    string maGVGS = selectedRow.Cells["MaGVGS"].Value.ToString();

                    // Gọi BLL để xóa bản ghi Phân Công
                    int result = pcBLL.DeletePhanCong(maSV, maCT, maGVGS);

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa phân công thành công!", "Thành công");
                        LoadSinhVienPhanCong(); // Tải lại dữ liệu
                    }
                    else if (result == 0)
                    {
                        MessageBox.Show("Không tìm thấy bản ghi phân công để xóa.", "Lỗi");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi");
                }
            }
        }

        // Nút sửa trạng thái phân công
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Lấy dữ liệu từ hàng được chọn
            DataGridViewRow selectedRow = dgvSinhVien.SelectedRows[0];
            try
            {
                string maSV = selectedRow.Cells["MaSV"].Value.ToString();
                int maCT = Convert.ToInt32(selectedRow.Cells["MaCT"].Value);
                string maGVGS = selectedRow.Cells["MaGVGS"].Value.ToString();
                string trangThai = selectedRow.Cells["TrangThai"].Value.ToString();
                // Gọi BLL để cập nhật trạng thái phân công
                int result = pcBLL.UpdatePhanCongStatus(maSV, maCT, maGVGS, trangThai);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật trạng thái phân công thành công!", "Thành công");
                    LoadSinhVienPhanCong(); // Tải lại dữ liệu
                }
                else if (result == 0)
                {
                    MessageBox.Show("Không tìm thấy bản ghi phân công để cập nhật.", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi");
            }

        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
          
            Session.Logout(); // Xóa thông tin Session
            this.Close();    // Đóng form Giảng viên
            LoginForm login = new LoginForm();
            login.Show();    // Mở lại Form Đăng nhập
        }
    }
    
}