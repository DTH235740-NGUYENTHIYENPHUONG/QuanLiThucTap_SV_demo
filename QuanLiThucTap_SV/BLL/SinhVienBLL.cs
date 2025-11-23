using MySql.Data.MySqlClient;
using System.Data;
using System;
using QuanLiThucTap_SV.DAL; // Giả định DAL.Database nằm trong namespace này
using System.Globalization;

namespace QuanLiThucTap_SV.BLL
{
    public class SinhVienBLL
    {
        // ====================================================
        // 1. LẤY THÔNG TIN CÁ NHÂN, ĐIỂM VÀ ĐƯỜNG DẪN BÁO CÁO
        // ====================================================
        public DataTable GetStudentInfoAndScoresByMaUser(int maUser)
        {
            string maSV = string.Empty;
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

            string query = @"
                SELECT 
                    sv.MaSV,
                    sv.HoTen,
                    sv.NgaySinh,
                    sv.GioiTinh,
                    sv.SoDienThoai AS Contact,
                    sv.MaLop,
                    sv.DuongDanBaoCao, -- <--- LẤY CỘT ĐƯỜNG DẪN TỪ SINHVIEN
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
        // 2. CẬP NHẬT THÔNG TIN CÁ NHÂN (TỪ DGV)
        // ====================================================
        public int UpdateStudentInfo(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string sdt, string maLop)
        {
            // (Giữ nguyên) Hàm này chỉ cập nhật thông tin cá nhân
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

        // ====================================================
        // 3. THÊM/CẬP NHẬT (GHI ĐÈ) ĐƯỜNG DẪN BÁO CÁO DUY NHẤT
        // ====================================================
        public int UpdateReportPath(string maSV, string duongDan)
        {
            string query = "UPDATE SINHVIEN SET DuongDanBaoCao = @DuongDan WHERE MaSV = @MaSV";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@DuongDan", duongDan),
                new MySqlParameter("@MaSV", maSV)
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // ====================================================
        // 4. XÓA BÁO CÁO (SET DuongDanBaoCao VỀ NULL)
        // ====================================================
        public int DeleteReportPath(string maSV)
        {
            string query = "UPDATE SINHVIEN SET DuongDanBaoCao = NULL WHERE MaSV = @MaSV";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV", maSV)
            };

            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }
    }
}