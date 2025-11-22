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

            // 1. Viết Hàm trong BLL/GiangVienBLL để lấy danh sách sinh viên được phân công
            // (Bạn cần tạo hàm này trong BLL)
            // Lấy thông tin chi tiết SV, Công ty và TrangThai

            string query = @"
                SELECT 
                    sv.MaSV, sv.HoTen AS TenSV, ct.TenCT, pc.NgayBatDauTT, pc.TrangThai
                FROM phancong pc
                JOIN sinhvien sv ON pc.MaSV = sv.MaSV
                JOIN congty ct ON pc.MaCT = ct.MaCT
                WHERE pc.MaGVGS = @MaGVGS"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaGVGS", currentMaGV)
            };

            // Giả sử bạn tạo hàm GetSinhVienPhanCong trong GiangVienBLL hoặc Data Access
            DataTable dt = DAL.DBHelper.GetData(query, parameters);

            // dgvSinhVien được hiển thị trên Panel lớn bên phải
            dgvSinhVien.DataSource = dt;
        }


        // ===============================================
        // C. CHỨC NĂNG NGHIỆP VỤ (Thêm, Xóa, Sửa - Dành cho Admin)
        // Nếu Giảng viên chỉ được quản lý sinh viên của mình:
        // Cần Form/Control để Giảng viên thêm/xóa/sửa thông tin PHÂN CÔNG, KHÔNG phải thêm/xóa/sửa thông tin gốc SV.
        // ===============================================

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Mở Form CRUD Phân Công và truyền MaGVGS hiện tại vào đó
            // Ví dụ: frmThemPhanCong form = new frmThemPhanCong(currentMaGV);
            // form.ShowDialog();
            // LoadSinhVienPhanCong();
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