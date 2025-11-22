using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.DAL;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLiThucTap_SV.BLL // Lớp nghiệp vụ
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

        
        public DataTable GetGiangVienInfoByMaUser(int maUser)
        {
            // B1: Lấy MaGV từ bảng USERS (cần thêm cột MaLienKet vào USERS hoặc liên kết qua 1 bảng khác)
            // 💡 GIẢ ĐỊNH: Trong bảng USERS, cột Username có giá trị trùng với MaGV (vd: 'gv001')

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
            return DBHelper.GetData(query, parameters);

        }
    }
}