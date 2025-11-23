using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.BLL;
using QuanLiThucTap_SV.GIAODIEN_UI; // Dùng cho frmDoiMatKhau
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLiThucTap_SV
{
    public partial class StudentPortalForm : Form
    {
        private SinhVienBLL svBLL = new SinhVienBLL();
        private DataTable dtStudentInfo; // Biến lưu trữ DataTable cho DGV
        private string currentMaSV = string.Empty; // Biến lưu trữ MaSV
        private string currentReportPath = string.Empty; // Biến lưu trữ đường dẫn báo cáo hiện tại

        // Các Label/Controls cần thiết trên Form (theo ảnh giao diện)
        // Giả định: dgvStudentInfo, lblMaSV, lblUser, btnSuaThongTin, btnDoiMatKhau, btnDangXuat

        public StudentPortalForm()
        {
            InitializeComponent();
            this.Text = "Sinh viên - Thông tin thực tập";

            // Đổi tên nút Lưu cho rõ ràng
            btnSua.Text = "Lưu Thông Tin Cá Nhân";

            LoadStudentData();
        }

        // ===============================================
        // A. TẢI THÔNG TIN CÁ NHÂN VÀ ĐIỂM VÀO DGV
        // ===============================================
        private void LoadStudentData()
        {
            int maUser = Session.MaUser;
            if (maUser <= 0) return;

            dtStudentInfo = svBLL.GetStudentInfoAndScoresByMaUser(maUser);

            if (dtStudentInfo != null && dtStudentInfo.Rows.Count > 0)
            {
                // Thông tin sinh viên là duy nhất nên DGV chỉ có 1 hàng
                currentMaSV = dtStudentInfo.Rows[0]["MaSV"].ToString();

                // Gán DataTable cho DataGridView
                dgvStudentInfo.DataSource = dtStudentInfo;

                // Cấu hình DGV
                ConfigureStudentDGV();

                // Hiển thị thông tin chung lên Label/Control khác
                lblMaSV.Text =  currentMaSV;
                lblUser.Text =  dtStudentInfo.Rows[0]["HoTen"].ToString();
                lblNgaySinh.Text = Convert.ToDateTime(dtStudentInfo.Rows[0]["NgaySinh"]).ToString("yyyy-MM-dd");
                lblGioiTinh.Text = dtStudentInfo.Rows[0]["GioiTinh"].ToString();
                lblContact.Text =  dtStudentInfo.Rows[0]["Contact"].ToString();
                lblMaLop.Text =  dtStudentInfo.Rows[0]["MaLop"].ToString();
                lblDiemGVGS.Text = dtStudentInfo.Rows[0]["DiemGVGS"].ToString();
                lblDiemCongTy.Text = dtStudentInfo.Rows[0]["DiemCongTy"].ToString();
                lblDiemTongKet.Text = dtStudentInfo.Rows[0]["DiemTongKet"].ToString();
                lblTrangThai.Text =  dtStudentInfo.Rows[0]["TrangThai"].ToString();
            }
            else
            {
                lblUser.Text = "(Không tìm thấy thông tin)";
                MessageBox.Show("Không tìm thấy thông tin Sinh viên tương ứng. Vui lòng kiểm tra lại liên kết USER-SINHVIEN.", "Lỗi hệ thống");
            }
        }

        // ===============================================
        // B. CẤU HÌNH DGV (Cột nào sửa được, cột nào không)
        // ===============================================
        private void ConfigureStudentDGV()
        {
            dgvStudentInfo.ReadOnly = false; // Cho phép sửa chung

            // 1. Cột KHÔNG ĐƯỢC PHÉP SỬA (Điểm và Khóa chính)
            string[] readOnlyColumns = { "MaSV", "DiemGVGS", "DiemCongTy", "DiemTongKet", "TrangThai" };

            foreach (string col in readOnlyColumns)
            {
                if (dgvStudentInfo.Columns.Contains(col))
                {
                    dgvStudentInfo.Columns[col].ReadOnly = true;
                    // Tô màu cột điểm để phân biệt (tùy chọn)
                    if (col.StartsWith("Diem") || col == "TrangThai")
                    {
                        dgvStudentInfo.Columns[col].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }
            }

            // 2. Cột ĐƯỢC PHÉP SỬA (Thông tin cá nhân)
            string[] editableColumns = { "HoTen", "NgaySinh", "GioiTinh", "Contact", "MaLop" };

            foreach (string col in editableColumns)
            {
                if (dgvStudentInfo.Columns.Contains(col))
                {
                    dgvStudentInfo.Columns[col].ReadOnly = false;
                }
            }

            // Cấu hình hiển thị ngày tháng
            if (dgvStudentInfo.Columns.Contains("NgaySinh"))
            {
                dgvStudentInfo.Columns["NgaySinh"].DefaultCellStyle.Format = "yyyy-MM-dd"; // Sử dụng format DB/Invariant
            }
            // Cấu hình format điểm
            if (dgvStudentInfo.Columns.Contains("DiemTongKet"))
            {
                dgvStudentInfo.Columns["DiemTongKet"].DefaultCellStyle.Format = "N2";
            }
        }

        // ===============================================
        // C. NÚT LƯU THÔNG TIN CÁ NHÂN (btnSuaThongTin_Click)
        // ===============================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kết thúc chỉnh sửa cell hiện tại
            dgvStudentInfo.EndEdit();

            if (string.IsNullOrEmpty(currentMaSV)) return;

            // Lấy tất cả các thay đổi
            DataTable changedTable = dtStudentInfo.GetChanges(DataRowState.Modified);

            if (changedTable == null || changedTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào được thực hiện để lưu.", "Thông báo");
                return;
            }

            // Chỉ lấy hàng duy nhất đã sửa
            DataRow row = changedTable.Rows[0];

            try
            {
                // 1. Lấy dữ liệu mới từ hàng đã chỉnh sửa
                string hoTen = row["HoTen", DataRowVersion.Current].ToString().Trim();
                string ngaySinhStr = row["NgaySinh", DataRowVersion.Current].ToString();
                string gioiTinh = row["GioiTinh", DataRowVersion.Current].ToString();
                string sdt = row["Contact", DataRowVersion.Current].ToString().Trim();
                string maLop = row["MaLop", DataRowVersion.Current].ToString().Trim();

                // Kiểm tra dữ liệu bắt buộc
                if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(sdt))
                {
                    MessageBox.Show("Họ tên và Số điện thoại không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadStudentData();
                    return;
                }

                DateTime ngaySinh;
                // Parse ngày sinh, dùng TryParse với CultureInfo.InvariantCulture để chấp nhận định dạng ngày chuẩn
                if (!DateTime.TryParse(ngaySinhStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh))
                {
                    MessageBox.Show("Lỗi: Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng YYYY-MM-DD (Ví dụ: 2001-10-10).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadStudentData();
                    return;
                }

                // 2. Gọi BLL để lưu
                int result = svBLL.UpdateStudentInfo(currentMaSV, hoTen, ngaySinh, gioiTinh, sdt, maLop);

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật thông tin cá nhân thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtStudentInfo.AcceptChanges(); // Chấp nhận thay đổi trên DataTable
                }
                else
                {
                    MessageBox.Show("Không có thay đổi nào được lưu hoặc có lỗi CSDL.", "Thông báo");
                    LoadStudentData(); // Tải lại nếu không thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadStudentData(); // Tải lại để loại bỏ dữ liệu lỗi
            }
        }

        // ===============================================
        // D. NÚT ĐỔI MẬT KHẨU
        // ===============================================
        private void btnDoiMatKhau_Click_1(object sender, EventArgs e)
        {
            frmDoiMatKhau doiMatKhauForm = new frmDoiMatKhau();
            doiMatKhauForm.ShowDialog();
        }

        // ===============================================
        // E. NÚT ĐĂNG XUẤT
        // ===============================================
        private void btnDangXuat_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Session.MaUser = -1;
                Session.Role = string.Empty;
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }
        // ===============================================
        // F. CHỨC NĂNG BÁO CÁO (Tạm thời để trống)
        // ===============================================
        private void btnThemBaoCao_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(currentMaSV))
            {
                MessageBox.Show("Không tìm thấy Mã sinh viên để nộp báo cáo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmThemBaoCao formThemBC = new frmThemBaoCao(currentMaSV);
            if (formThemBC.ShowDialog() == DialogResult.OK)
            {
                LoadStudentData();
            }
        }

        private void btnXoaBaoCao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentReportPath))
            {
                MessageBox.Show("Không có báo cáo nào để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa báo cáo '{Path.GetFileName(currentReportPath)}' này? Thao tác này sẽ xóa cả file vật lý.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // 1. Xóa trong CSDL (set NULL)
                    int result = svBLL.DeleteReportPath(currentMaSV);

                    if (result > 0)
                    {
                        // 2. Xóa file vật lý trên đĩa
                        if (File.Exists(currentReportPath))
                        {
                            File.Delete(currentReportPath);
                        }

                        MessageBox.Show("Xóa báo cáo thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStudentData(); // Tải lại dữ liệu
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa đường dẫn báo cáo trong CSDL.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa file vật lý: {ex.Message}", "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        



    }
}