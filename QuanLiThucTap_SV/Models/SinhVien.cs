using System;

namespace QuanLiThucTap_SV.Models
{
    public class SinhVien
    {
        public string MaSV { get; set; } // Khóa chính
        public string HoTen { get; set; } //
        public DateTime NgaySinh { get; set; } //
        public string GioiTinh { get; set; } //
        public string SoDienThoai { get; set; } //
        public string MaLop { get; set; } //
    }
}