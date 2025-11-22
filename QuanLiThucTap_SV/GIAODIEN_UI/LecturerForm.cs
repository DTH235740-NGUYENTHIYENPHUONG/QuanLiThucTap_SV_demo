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
        private DataTable dtPhanCong; // Biến lưu DataTable phân công cấp Class
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

            string query = @"
                SELECT 
                    pc.MaSV, pc.MaCT, pc.MaGVGS, 
                    sv.HoTen AS TenSV, ct.TenCT, pc.NgayBatDauTT, pc.TrangThai
                FROM phancong pc
                JOIN sinhvien sv ON pc.MaSV = sv.MaSV
                JOIN congty ct ON pc.MaCT = ct.MaCT
                WHERE pc.MaGVGS = @MaGVGS"; //

            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@MaGVGS", currentMaGV) };

            // Gán kết quả vào biến cấp Class
            dtPhanCong = DAL.DBHelper.GetData(query, parameters);
            dgvSinhVien.DataSource = dtPhanCong;

            // (Ẩn các cột khóa chính)
            dgvSinhVien.Columns["MaCT"].Visible = false;
            dgvSinhVien.Columns["MaGVGS"].Visible = false;

            // Đặt DGV ở chế độ cho phép sửa
            dgvSinhVien.ReadOnly = false;
            // Chỉ cho phép sửa cột trạng thái và ngày (các cột khác ReadOnly = true)
            if (dgvSinhVien.Columns.Contains("TenSV")) dgvSinhVien.Columns["TenSV"].ReadOnly = true;
            if (dgvSinhVien.Columns.Contains("TrangThai")) dgvSinhVien.Columns["TrangThai"].ReadOnly = false;
            if (dgvSinhVien.Columns.Contains("NgayBatDauTT")) dgvSinhVien.Columns["NgayBatDauTT"].ReadOnly = false;
        }


        // ===============================================
        // C. CHỨC NĂNG NGHIỆP VỤ (Thêm, Xóa, Sửa - Dành cho Admin)
        // Nếu Giảng viên chỉ được quản lý sinh viên của mình:
        // Cần Form/Control để Giảng viên thêm/xóa/sửa thông tin PHÂN CÔNG, KHÔNG phải thêm/xóa/sửa thông tin gốc SV.
        // ===============================================



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
            // Kết thúc chỉnh sửa cell hiện tại để đảm bảo DataRowState được cập nhật
            dgvSinhVien.EndEdit();

            // 1. Lấy tất cả các hàng đã được chỉnh sửa
            DataTable changedTable = dtPhanCong.GetChanges(DataRowState.Modified);

            if (changedTable == null || changedTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào được thực hiện để lưu.", "Thông báo");
                return;
            }

            int successCount = 0;

            // 2. Lặp qua các hàng đã thay đổi và gọi BLL để lưu
            foreach (DataRow row in changedTable.Rows)
            {
                try
                {
                    string maSV = row["MaSV"].ToString();
                    int maCT = Convert.ToInt32(row["MaCT"]);
                    string maGVGS = row["MaGVGS"].ToString();

                    // Lấy giá trị mới từ hàng
                    string newTrangThai = row["TrangThai", DataRowVersion.Current].ToString();
                    DateTime newNgayBatDau = Convert.ToDateTime(row["NgayBatDauTT", DataRowVersion.Current]);

                    // Gọi hàm BLL để cập nhật
                    int result = pcBLL.UpdatePhanCong(maSV, maCT, maGVGS, newTrangThai, newNgayBatDau);

                    if (result > 0)
                    {
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật sinh viên {row["TenSV"]}: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
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
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
          
            Session.Logout(); // Xóa thông tin Session
            this.Close();    // Đóng form Giảng viên
            LoginForm login = new LoginForm();
            login.Show();    // Mở lại Form Đăng nhập
        }
    }
    
}