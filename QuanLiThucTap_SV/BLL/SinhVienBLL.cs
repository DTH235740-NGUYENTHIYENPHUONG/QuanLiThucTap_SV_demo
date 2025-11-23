using MySql.Data.MySqlClient;
using System.Data;
using System;
using QuanLiThucTap_SV.DAL;
using System.Globalization; // Cần thiết cho việc parse NgaySinh

namespace QuanLiThucTap_SV.BLL
{
    public class SinhVienBLL
    {
        // ====================================================
        // 1. HÀM LẤY THÔNG TIN CÁ NHÂN VÀ ĐIỂM (CHO DGV)
        // ====================================================
        public DataTable GetStudentInfoAndScoresByMaUser(int maUser)
        {
            string maSV = string.Empty;
            // 1. Lấy MaSV từ USERS trước (vì Form dùng MaUser)
            string getMaSVQuery = "SELECT Username FROM USERS WHERE MaUser = @MaUser AND Role = 'SinhVien'";
            DataTable dtUser = DAL.DBHelper.GetData(getMaSVQuery, new MySqlParameter[] { new MySqlParameter("@MaUser", maUser) });

            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                maSV = dtUser.Rows[0]["Username"].ToString();
            }
            else
            {
                return null;
            }

            // 2. JOIN SINHVIEN và KETQUA_THUCTAP/PHANCONG để lấy tất cả thông tin
            string query = @"
                SELECT 
                    sv.MaSV,
                    sv.HoTen,
                    sv.NgaySinh,
                    sv.GioiTinh,
                    sv.SoDienThoai AS Contact,
                    sv.MaLop,
                    kq.DiemGVGS,
                    kq.DiemCongTy,
                    kq.DiemTongKet,
                    pc.TrangThai
                FROM SINHVIEN sv
                LEFT JOIN PHANCONG pc ON sv.MaSV = pc.MaSV
                LEFT JOIN KETQUA_THUCTAP kq ON sv.MaSV = kq.MaSV
                WHERE sv.MaSV = @MaSV_Param";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV_Param", maSV)
            };

            return DAL.DBHelper.GetData(query, parameters);
        }

        // ====================================================
        // 2. HÀM CẬP NHẬT THÔNG TIN CÁ NHÂN (Dùng cho nút Lưu)
        // ====================================================
        public int UpdateStudentInfo(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string sdt, string maLop)
        {
            string query = @"
                UPDATE SINHVIEN 
                SET 
                    HoTen = @HoTen, 
                    NgaySinh = @NgaySinh, 
                    GioiTinh = @GioiTinh, 
                    SoDienThoai = @SoDienThoai,
                    MaLop = @MaLop
                WHERE MaSV = @MaSV";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@HoTen", hoTen),
                new MySqlParameter("@NgaySinh", ngaySinh),
                new MySqlParameter("@GioiTinh", gioiTinh),
                new MySqlParameter("@SoDienThoai", sdt),
                new MySqlParameter("@MaLop", maLop),
                new MySqlParameter("@MaSV", maSV)
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }
    }
}