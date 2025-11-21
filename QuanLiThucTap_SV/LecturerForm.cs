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
    public partial class LecturerForm : Form
    {

        private string _maGVGS; // Biến lưu MaGV của giảng viên đang đăng nhập

        // Constructor mới nhận MaGV
        public LecturerForm(string maGV)
        {
            InitializeComponent();
            _maGVGS = maGV;
            this.Text = $"Giảng viên: {_maGVGS}"; // Đặt tiêu đề Form

            // Tải sinh viên ngay khi Form được mở
            LoadStudentsData();
        }

        private void LoadStudentsData()
        {
            // Dùng hàm vừa tạo trong Database
            DataTable dt = Database.GetStudentsByLecturer(_maGVGS);
            dgvDSSV.DataSource = dt;

            // Tùy chỉnh hiển thị cột DGV (tùy chọn)
            if (dgvDSSV.Columns.Contains("MaSV")) dgvDSSV.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgvDSSV.Columns.Contains("HoTen")) dgvDSSV.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvDSSV.Columns.Contains("NgaySinh")) dgvDSSV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            if (dgvDSSV.Columns.Contains("GioiTinh")) dgvDSSV.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvDSSV.Columns.Contains("Email")) dgvDSSV.Columns["Email"].HeaderText = "Email";
            if (dgvDSSV.Columns.Contains("SDT")) dgvDSSV.Columns["SDT"].HeaderText = "SĐT";
            if (dgvDSSV.Columns.Contains("MaLop")) dgvDSSV.Columns["MaLop"].HeaderText = "Mã Lớp";
        }

        public LecturerForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvDSSV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThemSinhVien_Click(object sender, EventArgs e)
        {
            string maSV = txtMaSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value.Date;
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString();
            string email = txtEmail.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string maLop = txtMaLop.Text.Trim();

            // Kiểm tra dữ liệu đầu vào cơ bản
            if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã SV, Họ Tên và Giới Tính.", "Cảnh báo");
                return;
            }

            // Gọi hàm thêm sinh viên từ Database
            if (Database.InsertNewStudent(maSV, hoTen, ngaySinh, gioiTinh, email, sdt, maLop, _maGVGS))
            {
                MessageBox.Show("Thêm sinh viên thành công và đã phân công cho Giảng viên.", "Thành công");

                // Sau khi thêm thành công, làm mới DataGridView
                LoadStudentsData();

                // Xóa dữ liệu cũ trên các ô nhập
                ClearStudentInputs();
            }
        }

        private void ClearStudentInputs()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            txtEmail.Clear();
            txtSDT.Clear();
            txtMaLop.Clear();
        }
    }
}
