using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using QuanLiThucTap_SV.GIAODIEN_UI;

namespace QuanLiThucTap_SV
{
    public partial class CompanyForm : Form
    {
        // Khai báo BLL và các biến cần thiết
        private UserBLL userBLL = new UserBLL();
        private CongTyBLL ctBLL = new CongTyBLL();
        private DataTable dtStudents;
        private string currentMaCT = string.Empty;

        public CompanyForm()
        {
            InitializeComponent();
            this.Text = "Quản lý Thực Tập - Công Ty";

            LoadCompanyInfo();
            LoadStudentDataGrid();
        }

        // ===============================================
        // TẢI THÔNG TIN CÔNG TY 
        // ===============================================
        private void LoadCompanyInfo()
        {
            int maUser = Session.MaUser;
            if (maUser > 0)
            {
                DataTable dt = userBLL.GetCongTyInfoByMaUser(maUser);
                if (dt.Rows.Count > 0)
                {
                   
                    string maCT_Value = dt.Rows[0]["MaCT"].ToString();
                    currentMaCT = maCT_Value;

                    lblMaCT.Text = maCT_Value; 
                    lblTenCT.Text = dt.Rows[0]["TenCT"].ToString();
                    lblDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                    lblNguoiLienHe.Text = dt.Rows[0]["NguoiLienHe"].ToString();
                    lblContact.Text = dt.Rows[0]["SoDienThoai"].ToString();
                }
                else
                {
                    lblTenCT.Text = "Không tìm thấy!";
                    MessageBox.Show("Không tìm thấy thông tin Công ty tương ứng. Vui lòng kiểm tra lại liên kết USER-CONGTY.", "Lỗi hệ thống");
                }
            }
        }

        // ===============================================
        // TẢI DANH SÁCH SINH VIÊN
        // ===============================================
        private void LoadStudentDataGrid()
        {
            // Không tải nếu chưa có MaCT hợp lệ
            if (string.IsNullOrEmpty(currentMaCT)) return;

            dtStudents = ctBLL.GetStudentsByCompany(currentMaCT);
            dgvStudents.DataSource = dtStudents;

            // Cấu hình DGV
            dgvStudents.ReadOnly = false;

            if (dgvStudents.Columns.Contains("MaCT")) dgvStudents.Columns["MaCT"].Visible = false;
            if (dgvStudents.Columns.Contains("MaGVGS")) dgvStudents.Columns["MaGVGS"].Visible = true;
            if (dgvStudents.Columns.Contains("MaSV")) dgvStudents.Columns["MaSV"].ReadOnly = true;
            if (dgvStudents.Columns.Contains("TenSV")) dgvStudents.Columns["TenSV"].ReadOnly = true;
            if (dgvStudents.Columns.Contains("NgayBatDauTT")) dgvStudents.Columns["NgayBatDauTT"].ReadOnly = true;

            // Cột Điểm cho phép sửa
            if (dgvStudents.Columns.Contains("DiemCongTy"))
            {
                dgvStudents.Columns["DiemCongTy"].ReadOnly = false;
                dgvStudents.Columns["DiemCongTy"].DefaultCellStyle.Format = "N2"; // Định dạng số với 2 chữ số thập phân
            }
        }

        // ===============================================
        // NÚT SỬA VÀ LƯU ĐIỂM 
        // ===============================================
        private void btnSua_Click(object sender, EventArgs e)
        {
     
            dgvStudents.EndEdit(); // Kết thúc việc chỉnh sửa hiện tại

            DataTable changedTable = dtStudents.GetChanges(DataRowState.Modified); // Lấy các hàng đã thay đổi

            if (changedTable == null || changedTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có thay đổi nào được thực hiện để lưu.", "Thông báo");
                return;
            }

            int successCount = 0;
            string maCT_Str = currentMaCT;

            // Lặp qua các hàng đã thay đổi và gọi BLL để lưu
            foreach (DataRow row in changedTable.Rows)
            {
                try
                {
                    string maSV = row["MaSV"].ToString();
                    string maGVGS = row["MaGVGS"].ToString();

                    bool diemChanged = false;

                    // Kiểm tra xem cột DiemCongTy có thay đổi không
                    if (row.Table.Columns.Contains("DiemCongTy") &&
                        row["DiemCongTy", DataRowVersion.Current].ToString() != row["DiemCongTy", DataRowVersion.Original].ToString())
                    {
                        string diemStr = row["DiemCongTy", DataRowVersion.Current].ToString();

                        if (decimal.TryParse(diemStr,
                                             NumberStyles.Any,
                                             CultureInfo.InvariantCulture,
                                             out decimal diem))
                        {
                            if (diem >= 0 && diem <= 10)
                            {
                                // Gọi hàm BLL để cập nhật DiemCongTy
                                int result = ctBLL.UpdateDiemCongTySimple(maSV, maCT_Str, maGVGS, diem);
                                if (result > 0)
                                {
                                    diemChanged = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Lỗi: Điểm Công ty của sinh viên {row["TenSV"]} phải từ 0 đến 10.", "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                LoadStudentDataGrid();
                                return;
                            }
                        }
                        else if (!string.IsNullOrEmpty(diemStr))
                        {
                            MessageBox.Show($"Lỗi: Điểm Công ty của sinh viên {row["TenSV"]} không hợp lệ (phải là số, *sử dụng dấu chấm*).", "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LoadStudentDataGrid();
                            return;
                        }
                    }

                    if (diemChanged) successCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật sinh viên {row["TenSV"]}: {ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LoadStudentDataGrid();
                    return;
                }
            }

            // 4. Thông báo kết quả và chấp nhận thay đổi
            if (successCount > 0)
            {
                MessageBox.Show($"Đã lưu thành công {successCount} thay đổi Điểm Công ty.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtStudents.AcceptChanges(); // Reset trạng thái Modified
            }
            else if (changedTable.Rows.Count > 0)
            {
                MessageBox.Show("Không có Điểm Công ty nào được lưu thành công vào cơ sở dữ liệu.", "Thông báo");
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
        private void btnDangXuat_Click(object sender, EventArgs e)
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

    }
}