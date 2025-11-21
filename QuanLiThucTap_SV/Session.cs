using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Session lưu thông tin người dùng sau khi đăng nhập (Nhận biết ai đang đăng nhập)

namespace QuanLiThucTap_SV
{
    // static: Để truy cập từ bất kỳ Form nào mà không cần "new"
    public static class Session
    {
        public static int MaUser { get; set; } = -1;    // ID tài khoản (bảng USERS)
        public static string Role { get; set; } = "";   // Vai trò
        public static string HoTen { get; set; } = "";  // Tên hiển thị (nếu muốn)

        // Hàm xóa session khi Đăng xuất
        public static void Clear()
        {
            MaUser = -1;
            Role = "";
            HoTen = "";
        }
    }
}
