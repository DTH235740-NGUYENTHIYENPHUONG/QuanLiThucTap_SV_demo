using MySql.Data.MySqlClient;
using System.Data;
using QuanLiThucTap_SV.DAL; 
using System;
using System.Security.Cryptography; 

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
           
            string query = "INSERT INTO USERS (Username, Passcode, HoTen, Role, TrangThai) VALUES (@User, @Pass, @HoTen, @Role, 1)";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@User", username),
                new MySqlParameter("@Pass", password), 
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

        /*public int ResetPassword(int maUser, string newPassword)
        {
            // LƯU Ý: NÊN MÃ HÓA MẬT KHẨU TRƯỚC KHI LƯU
            string query = "UPDATE USERS SET Passcode = @NewPass WHERE MaUser = @MaUser";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@NewPass", newPassword), // Mật khẩu mới đã mã hóa
                new MySqlParameter("@MaUser", maUser)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }*/

        
        public int DeleteUser(int maUser)
        {
            string query = "DELETE FROM USERS WHERE MaUser = @MaUser";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                 new MySqlParameter("@MaUser", maUser)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

    }
}
