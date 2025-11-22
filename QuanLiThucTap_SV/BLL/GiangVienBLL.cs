using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.Models;
using System.Data;
using QuanLiThucTap_SV.DAL;

namespace QuanLiThucTap_SV.BLL
{
    public class GiangVienBLL
    {
        // 1. LẤY TẤT CẢ GIẢNG VIÊN (Read)
        public DataTable GetAllGiangVien()
        {
            string query = "SELECT MaGV, HoTen, MaKhoa, Email FROM giangvien"; //
            return DAL.DBHelper.GetData(query);
        }

        // 2. THÊM GIẢNG VIÊN (Create)
        public int InsertGiangVien(GiangVien gv)
        {
            string query = "INSERT INTO giangvien (MaGV, HoTen, MaKhoa, Email) VALUES (@MaGV, @HoTen, @MaKhoa, @Email)";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaGV", gv.MaGV),
                new MySqlParameter("@HoTen", gv.HoTen),
                new MySqlParameter("@MaKhoa", gv.MaKhoa),
                new MySqlParameter("@Email", gv.Email)
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // 3. SỬA GIẢNG VIÊN (Update)
        public int UpdateGiangVien(GiangVien gv)
        {
            string query = "UPDATE giangvien SET HoTen = @HoTen, MaKhoa = @MaKhoa, Email = @Email WHERE MaGV = @MaGV";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@HoTen", gv.HoTen),
                new MySqlParameter("@MaKhoa", gv.MaKhoa),
                new MySqlParameter("@Email", gv.Email),
                new MySqlParameter("@MaGV", gv.MaGV) // Điều kiện WHERE
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // 4. XÓA GIẢNG VIÊN (Delete)
        public int DeleteGiangVien(string maGV)
        {
            string query = "DELETE FROM giangvien WHERE MaGV = @MaGV";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaGV", maGV)
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // 5. LẤY THÔNG TIN GIẢNG VIÊN THEO MaUser
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

            return DAL.DBHelper.GetData(query, parameters);
        }
    }
}