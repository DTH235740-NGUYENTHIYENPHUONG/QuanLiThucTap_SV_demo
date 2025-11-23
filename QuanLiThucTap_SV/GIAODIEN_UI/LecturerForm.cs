using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.BLL;
using QuanLiThucTap_SV.GIAODIEN_UI;
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

            // 🔑 Truy vấn mới: LEFT JOIN để lấy DiemGVGS
            string query = @"
            SELECT 
                pc.MaSV, pc.MaCT, pc.MaGVGS, 
                sv.HoTen AS TenSV, 
                ct.TenCT, 
                pc.NgayBatDauTT, 
                pc.TrangThai,
                kq.DiemGVGS 
            FROM phancong pc
            JOIN sinhvien sv ON pc.MaSV = sv.MaSV
            JOIN congty ct ON pc.MaCT = ct.MaCT
            LEFT JOIN ketqua_thuctap kq ON pc.MaSV = kq.MaSV AND pc.MaCT = kq.MaCT AND pc.MaGVGS = kq.MaGVGS
            WHERE pc.MaGVGS = @MaGVGS"; //

            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@MaGVGS", currentMaGV) };

            dtPhanCong = DAL.DBHelper.GetData(query, parameters);
            dgvSinhVien.DataSource = dtPhanCong;

            // (Ẩn các cột khóa chính)
            dgvSinhVien.Columns["MaCT"].Visible = false;
            dgvSinhVien.Columns["MaGVGS"].Visible = false;

            // Đặt DGV ở chế độ cho phép sửa
            dgvSinhVien.ReadOnly = false;
            if (dgvSinhVien.Columns.Contains("DiemGVGS"))
            {
                dgvSinhVien.Columns["DiemGVGS"].ReadOnly = false;
                // Định dạng cột điểm để hiển thị số thập phân
                dgvSinhVien.Columns["DiemGVGS"].DefaultCellStyle.Format = "N2";
            }
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
                    string maCT = selectedRow.Cells["MaCT"].Value.ToString();
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

            int successCountPhanCong = 0;
            int successCountDiem = 0;

            // Lưu trữ các hàng lỗi để xử lý sau
            DataRowCollection originalRows = changedTable.Rows;
            int totalChangedRows = originalRows.Count;

            // 2. Lặp qua các hàng đã thay đổi và gọi BLL để lưu
            for (int i = totalChangedRows - 1; i >= 0; i--)
            {
                DataRow row = originalRows[i];

                try
                {
                    string maSV = row["MaSV"].ToString();
                    string maCT = row["MaCT"].ToString();
                    string maGVGS = row["MaGVGS"].ToString();

                    bool phanCongChanged = false;
                    bool diemChanged = false;


                    // ===============================================
                    // 2A. XỬ LÝ LƯU THAY ĐỔI TRONG PHÂN CÔNG (Trạng thái, Ngày)
                    // ===============================================
                    string newTrangThai = row["TrangThai", DataRowVersion.Current].ToString();
                    DateTime newNgayBatDau = Convert.ToDateTime(row["NgayBatDauTT", DataRowVersion.Current]);

                    // So sánh với giá trị gốc để chỉ lưu khi thực sự thay đổi

                    if (newTrangThai != row["TrangThai", DataRowVersion.Original].ToString() ||
                        newNgayBatDau != Convert.ToDateTime(row["NgayBatDauTT", DataRowVersion.Original]))
                    {
                        int result = pcBLL.UpdatePhanCong(maSV, maCT, maGVGS, newTrangThai, newNgayBatDau);

                        if (result > 0)
                        {
                            successCountPhanCong++;
                            phanCongChanged = true;
                        }
                    }


                    // ===============================================
                    // 2B. XỬ LÝ LƯU ĐIỂM GIÁM SÁT (DiemGVGS)
                    // ===============================================
                    // Kiểm tra xem cột DiemGVGS có thay đổi không
                    if (row.Table.Columns.Contains("DiemGVGS") &&
                        row["DiemGVGS", DataRowVersion.Current].ToString() != row["DiemGVGS", DataRowVersion.Original].ToString())
                    {
                        string diemStr = row["DiemGVGS", DataRowVersion.Current].ToString();

                        if (decimal.TryParse(diemStr, out decimal diem))
                        {
                            // Validation: Điểm phải hợp lệ [0, 10]
                            if (diem >= 0 && diem <= 10)
                            {
                                int diemUpdate = gvBLL.UpdateDiemGVGSSimple(maSV, maCT, maGVGS, diem);
                                if (diemUpdate > 0)
                                {
                                    successCountDiem++;
                                    diemChanged = true;
                                }
                            }
                            else
                            {
                                // Xử lý lỗi: Điểm không hợp lệ
                                MessageBox.Show($"Lỗi: Điểm Giám sát của sinh viên {row["TenSV"]} phải từ 0 đến 10.", "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // Quay lại hàm Load để hủy bỏ thay đổi sai
                                LoadSinhVienPhanCong();
                                return;
                            }
                        }
                        else if (!string.IsNullOrEmpty(diemStr))
                        {
                            // Xử lý lỗi: Điểm không phải là số
                            MessageBox.Show($"Lỗi: Điểm Giám sát của sinh viên {row["TenSV"]} không hợp lệ (phải là số).", "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LoadSinhVienPhanCong();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi CSDL chung
                    MessageBox.Show($"Lỗi khi cập nhật sinh viên {row["TenSV"]}: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadSinhVienPhanCong();
                    return;
                }
            }

            // 3. THÔNG BÁO KẾT QUẢ VÀ CẬP NHẬT TRẠNG THÁI DATATABLE
            if (successCountPhanCong > 0 || successCountDiem > 0)
            {
                string msg = "Lưu thành công: ";
                if (successCountPhanCong > 0) msg += $"{successCountPhanCong} thay đổi Phân Công. ";
                if (successCountDiem > 0) msg += $"{successCountDiem} thay đổi Điểm Giám sát.";

                MessageBox.Show(msg, "Lưu thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Chấp nhận thay đổi trong DataTable để reset trạng thái Modified
                dtPhanCong.AcceptChanges();
                LoadSinhVienPhanCong(); // Load lại để đảm bảo dữ liệu mới nhất (nếu có tính toán Điểm tổng kết)
            }
            else
            {
                MessageBox.Show("Không có thay đổi nào được lưu vào cơ sở dữ liệu.", "Thông báo");
            }
        }


        // ===============================================
        // D. ĐỔI MẬT KHẨU & ĐĂNG XUẤT
        // ===============================================
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
          
            Session.Logout(); // Xóa thông tin Session
            this.Close();    // Đóng form Giảng viên
            LoginForm login = new LoginForm();
            login.Show();    // Mở lại Form Đăng nhập
        }

        private void btnDoiMatKhau_Click_1(object sender, EventArgs e)
        {
            frmDoiMatKhau doiMatKhauForm = new frmDoiMatKhau();
            doiMatKhauForm.ShowDialog();
        }

        
    }
    
}