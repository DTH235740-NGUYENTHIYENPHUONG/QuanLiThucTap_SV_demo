namespace QuanLiThucTap_SV
{
    public static class Session
    {
        // Lưu Mã User (MaUser) sau khi đăng nhập thành công
        public static int MaUser { get; set; } = -1;

        // Lưu Vai trò (Role) sau khi đăng nhập thành công: Admin, GiangVien, CongTy, SinhVien
        public static string Role { get; set; } = string.Empty;

        // Hàm xóa Session khi đăng xuất
        public static void Logout()
        {
            MaUser = -1;
            Role = string.Empty;
        }
    }
}