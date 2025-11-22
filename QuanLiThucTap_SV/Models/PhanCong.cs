using System;

namespace QuanLiThucTap_SV.Models
{
    // Đại diện cho bảng phancong
    public class PhanCong
    {
        // Khóa chính
        public string MaSV { get; set; }
        public int MaCT { get; set; }
        public string MaGVGS { get; set; }

        public DateTime NgayBatDauTT { get; set; }
        public string TrangThai { get; set; } // Enum: 'Dang TT', 'Hoan Thanh', 'Huy', 'Cho Duyet'
    }
}