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

        // Constructor mới nhận MaGV
         public LecturerForm(string maGV)
         {
             InitializeComponent();
             _maGVGS = maGV;
             this.Text = $"Giảng viên: {_maGVGS}"; // Đặt tiêu đề Form

             // Tải sinh viên ngay khi Form được mở
         }

         private string _maGVGS; // Biến lưu MaGV của giảng viên đang đăng nhập
        /*private void LoadStudentsData()
        {
            // Dùng hàm vừa tạo trong Database
            DataTable dt = Database.GetStudentsByLecturer(_maGVGS);
            dgvSinhVien.DataSource = dt;

            // Tùy chỉnh hiển thị cột DGV (tùy chọn)
            if (dgvSinhVien.Columns.Contains("MaSV")) dgvSinhVien.Columns["MaSV"].HeaderText = "Mã SV";
            if (dgvSinhVien.Columns.Contains("HoTen")) dgvSinhVien.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvSinhVien.Columns.Contains("NgaySinh")) dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            if (dgvSinhVien.Columns.Contains("GioiTinh")) dgvSinhVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvSinhVien.Columns.Contains("Email")) dgvSinhVien.Columns["Email"].HeaderText = "Email";
            if (dgvSinhVien.Columns.Contains("SDT")) dgvSinhVien.Columns["SDT"].HeaderText = "SĐT";
            if (dgvSinhVien.Columns.Contains("MaLop")) dgvSinhVien.Columns["MaLop"].HeaderText = "Mã Lớp";
        }


        private void btnThem_Click(object sender, EventArgs e)
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
        }*/


    }
}
