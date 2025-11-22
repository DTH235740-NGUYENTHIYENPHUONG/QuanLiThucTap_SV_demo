using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.DAL;
using QuanLiThucTap_SV.Models;
using System.Data;

namespace QuanLiThucTap_SV.BLL
{
    public class SinhVienBLL
    {
        // 1. LẤY TẤT CẢ SINH VIÊN
        public DataTable GetAllSinhVien()
        {
            string query = "SELECT * FROM sinhvien"; //
            return DBHelper.GetData(query);
        }

        // 2. THÊM SINH VIÊN (Create)
        public int InsertSinhVien(SinhVien sv)
        {
            string query = "INSERT INTO sinhvien (MaSV, HoTen, NgaySinh, GioiTinh, SoDienThoai, MaLop) " +
                           "VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @MaLop)"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV", sv.MaSV),
                new MySqlParameter("@HoTen", sv.HoTen),
                new MySqlParameter("@NgaySinh", sv.NgaySinh.ToString("yyyy-MM-dd")),
                new MySqlParameter("@GioiTinh", sv.GioiTinh),
                new MySqlParameter("@SoDienThoai", sv.SoDienThoai),
                new MySqlParameter("@MaLop", sv.MaLop)
            };

            return DBHelper.ExecuteNonQuery(query, parameters);
        }

        // 3. SỬA SINH VIÊN (Update)
        public int UpdateSinhVien(SinhVien sv)
        {
            string query = "UPDATE sinhvien SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, " +
                           "SoDienThoai = @SoDienThoai, MaLop = @MaLop WHERE MaSV = @MaSV"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@HoTen", sv.HoTen),
                new MySqlParameter("@NgaySinh", sv.NgaySinh.ToString("yyyy-MM-dd")),
                new MySqlParameter("@GioiTinh", sv.GioiTinh),
                new MySqlParameter("@SoDienThoai", sv.SoDienThoai),
                new MySqlParameter("@MaLop", sv.MaLop),
                new MySqlParameter("@MaSV", sv.MaSV) // Where clause
            };

            return DBHelper.ExecuteNonQuery(query, parameters);
        }

        // 4. XÓA SINH VIÊN (Delete)
        public int DeleteSinhVien(string maSV)
        {
            string query = "DELETE FROM sinhvien WHERE MaSV = @MaSV"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV", maSV)
            };

            return DBHelper.ExecuteNonQuery(query, parameters);
        }
    }
}