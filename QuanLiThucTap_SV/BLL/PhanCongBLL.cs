using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.Models;
using System.Data;
using System;

namespace QuanLiThucTap_SV.BLL
{
    public class PhanCongBLL
    {
        // 1. HÀM THÊM PHÂN CÔNG (Create)
        public int AddPhanCong(PhanCong phanCong)
        {
            string query = "INSERT INTO phancong (MaSV, MaCT, MaGVGS, NgayBatDauTT, TrangThai) " +
                           "VALUES (@MaSV, @MaCT, @MaGVGS, @NgayBatDau, @TrangThai)"; //
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV", phanCong.MaSV),
                new MySqlParameter("@MaCT", phanCong.MaCT),
                new MySqlParameter("@MaGVGS", phanCong.MaGVGS),
                new MySqlParameter("@NgayBatDauTT", phanCong.NgayBatDauTT),
                new MySqlParameter("@TrangThai", phanCong.TrangThai)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }


        // 2. HÀM XÓA PHÂN CÔNG (Delete)
        public int DeletePhanCong(string maSV, int maCT, string maGVGS)
        {
            string query = "DELETE FROM phancong WHERE MaSV = @MaSV AND MaCT = @MaCT AND MaGVGS = @MaGVGS"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV", maSV),
                new MySqlParameter("@MaCT", maCT),
                new MySqlParameter("@MaGVGS", maGVGS)
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // 3. HÀM CẬP NHẬT TRẠNG THÁI PHÂN CÔNG (Update)
        // Giảng viên thường chỉ có quyền cập nhật trạng thái hoặc ngày bắt đầu
        public int UpdatePhanCongStatus(string maSV, int maCT, string maGVGS, string newTrangThai)
        {
            string query = "UPDATE phancong SET TrangThai = @NewStatus WHERE MaSV = @MaSV AND MaCT = @MaCT AND MaGVGS = @MaGVGS"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@NewStatus", newTrangThai),
                new MySqlParameter("@MaSV", maSV),
                new MySqlParameter("@MaCT", maCT),
                new MySqlParameter("@MaGVGS", maGVGS)
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }
    }
}