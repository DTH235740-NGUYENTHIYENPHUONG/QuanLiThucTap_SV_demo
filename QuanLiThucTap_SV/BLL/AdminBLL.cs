using MySql.Data.MySqlClient;
using System.Data;
using QuanLiThucTap_SV.DAL; // Giả định DAL.Database nằm ở đây
using System;
using System.Security.Cryptography; // Cần cho mã hóa nếu sử dụng

namespace QuanLiThucTap_SV.BLL
{
    public class AdminBLL
    {
        // ====================================================
        // I. QUẢN LÝ TÀI KHOẢN (USERS)
        // ====================================================

        public DataTable GetAllUsers()
        {
            string query = "SELECT MaUser, Username, HoTen, Role, TrangThai FROM USERS ORDER BY Role, Username";
            return DAL.DBHelper.GetData(query);
        }

        public int AddUser(string username, string password, string hoTen, string role)
        {
            // LƯU Ý: NÊN MÃ HÓA MẬT KHẨU TRƯỚC KHI LƯU
            // Hiện tại dùng mật khẩu thô như trong SQL đã upload
            string query = "INSERT INTO USERS (Username, Passcode, HoTen, Role, TrangThai) VALUES (@User, @Pass, @HoTen, @Role, 1)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@User", username),
                new MySqlParameter("@Pass", password), // Thường là mật khẩu đã mã hóa
                new MySqlParameter("@HoTen", hoTen),
                new MySqlParameter("@Role", role)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        public int UpdateUser(int maUser, string hoTen, string role, bool trangThai)
        {
            string query = "UPDATE USERS SET HoTen = @HoTen, Role = @Role, TrangThai = @TrangThai WHERE MaUser = @MaUser";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@HoTen", hoTen),
                new MySqlParameter("@Role", role),
                new MySqlParameter("@TrangThai", trangThai ? 1 : 0),
                new MySqlParameter("@MaUser", maUser)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        public int ResetPassword(int maUser, string newPassword)
        {
            // LƯU Ý: NÊN MÃ HÓA MẬT KHẨU TRƯỚC KHI LƯU
            string query = "UPDATE USERS SET Passcode = @NewPass WHERE MaUser = @MaUser";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@NewPass", newPassword), // Mật khẩu mới đã mã hóa
                new MySqlParameter("@MaUser", maUser)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // Lưu ý: Việc xóa User có thể bị cản trở bởi khóa ngoại.
        public int DeleteUser(int maUser)
        {
            // Nên dùng câu lệnh này nếu bạn muốn xóa đồng thời cả hồ sơ liên quan (SINHVIEN/GIANGVIEN/CONGTY) 
            // nhưng cần phải kiểm tra khóa ngoại trước. Nếu không, chỉ nên CẬP NHẬT TrangThai = 0 (Khóa)
            string query = "DELETE FROM USERS WHERE MaUser = @MaUser";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                 new MySqlParameter("@MaUser", maUser)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

    }
}
