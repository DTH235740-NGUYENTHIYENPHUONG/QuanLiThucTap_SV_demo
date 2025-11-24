using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLiThucTap_SV.BLL 
{
    public class UserBLL
    {
        // ====================================================
        // KIỂM TRA ĐĂNG NHẬP
        // ====================================================
        public bool CheckLogin(string username, string passcode, out string role, out int maUser)
        {
            role = string.Empty;
            maUser = -1;

            string query = "SELECT MaUser, Role FROM USERS WHERE Username = @user AND Passcode = @pass AND TrangThai = 1"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@user", username),
                new MySqlParameter("@pass", passcode)
            };

            // Gọi DBHelper để lấy dữ liệu
            DataTable dt = DBHelper.GetData(query, parameters);

            if (dt.Rows.Count > 0)
            {
                maUser = Convert.ToInt32(dt.Rows[0]["MaUser"]); //
                role = dt.Rows[0]["Role"].ToString(); //
                return true;
            }

            return false;
        }

        // Lấy thông tin giảng viên dựa trên MaUser
        public DataTable GetGiangVienInfoByMaUser(int maUser)
        {

            string query = @"
        SELECT 
            gv.MaGV, gv.HoTen, gv.MaKhoa, gv.Email
        FROM USERS u
        JOIN giangvien gv ON u.Username = gv.MaGV
        WHERE u.MaUser = @MaUser";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
        new MySqlParameter("@MaUser", maUser)
            };
            
            return DBHelper.GetData(query, parameters); //DAL.Database.GetData là hàm lấy dữ liệu từ CSDL

        }

        // Lấy thông tin công ty dựa trên MaUser
        public DataTable GetCongTyInfoByMaUser(int maUser)
        {
            
            string query = @"
        SELECT 
            ct.MaCT, ct.TenCT, ct.DiaChi, ct.NguoiLienHe, ct.SoDienThoai
        FROM USERS u
        JOIN congty ct ON u.Username = ct.MaCT 
        WHERE u.MaUser = @MaUser AND u.Role = 'CongTy'";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
        new MySqlParameter("@MaUser", maUser)
            };

            
            return DAL.DBHelper.GetData(query, parameters);
        }

        // Hàm kiểm tra và thay đổi mật khẩu
        public bool ChangePasscode(int maUser, string oldPass, string newPass)
        {
            // KIỂM TRA MẬT KHẨU CŨ CÓ ĐÚNG KHÔNG
            string checkQuery = "SELECT COUNT(*) FROM USERS WHERE MaUser = @MaUser AND Passcode = @OldPass"; //

            MySqlParameter[] checkParams = new MySqlParameter[]
            {
                new MySqlParameter("@MaUser", maUser),
                new MySqlParameter("@OldPass", oldPass)
            };

            //GetData để kiểm tra COUNT, sau đó lấy giá trị scalar
            DataTable dt = DAL.DBHelper.GetData(checkQuery, checkParams); //Parameters để tránh SQL Injection

            // GetData trả về DataTable,kiểm tra kết quả trả về là 1 row, 1 column
            if (dt == null || dt.Rows.Count == 0 || Convert.ToInt32(dt.Rows[0][0]) == 0)
            {
                // Mật khẩu cũ không đúng hoặc không tìm thấy người dùng
                return false;
            }

            //CẬP NHẬT MẬT KHẨU MỚI
            string updateQuery = "UPDATE USERS SET Passcode = @NewPass WHERE MaUser = @MaUser"; //

            MySqlParameter[] updateParams = new MySqlParameter[]
            {
                new MySqlParameter("@NewPass", newPass),
                new MySqlParameter("@MaUser", maUser)
            };

            int rowsAffected = DAL.DBHelper.ExecuteNonQuery(updateQuery, updateParams);

            return rowsAffected > 0; 
        }
    }
}