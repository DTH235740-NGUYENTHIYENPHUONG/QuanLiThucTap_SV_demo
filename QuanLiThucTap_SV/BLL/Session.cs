namespace QuanLiThucTap_SV
{

    // Lớp quản lý Session người dùng

    public static class Session
    {
        // Lưu Mã User (MaUser) sau khi đăng nhập thành công
        public static int MaUser { get; set; } = -1;

        // Lưu Vai trò (Role) sau khi đăng nhập thành công: Admin, GiangVien, CongTy, SinhVien
        public static string Role { get; set; } = string.Empty; // Giá trị mặc định là chuỗi rỗng (empty)

        // Hàm xóa Session khi đăng xuất
        public static void Logout()
        {
            MaUser = -1;
            Role = string.Empty;
        }
    }
}