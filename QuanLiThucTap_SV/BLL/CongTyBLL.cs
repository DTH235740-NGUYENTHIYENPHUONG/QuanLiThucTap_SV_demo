using MySql.Data.MySqlClient;
using QuanLiThucTap_SV.DAL;
using System.Data;
using System;
using System.Windows.Forms; // Dùng cho MessageBox nếu cần báo lỗi CSDL

namespace QuanLiThucTap_SV.BLL
{
    public class CongTyBLL
    {
        // ====================================================
        // 1. LẤY DANH SÁCH SINH VIÊN ĐANG THỰC TẬP TẠI CÔNG TY
        // ====================================================
        public DataTable GetStudentsByCompany(string maCT)
        {
            // Truy vấn lấy chi tiết sinh viên, giảng viên giám sát và Điểm Công Ty hiện tại
            string query = @"
                SELECT 
                    pc.MaSV,             -- Khóa chính
                    pc.MaCT,             -- Khóa chính (Ẩn trên DGV)
                    pc.MaGVGS,           -- Khóa chính (Ẩn trên DGV)
                    sv.HoTen AS TenSV, 
                    pc.NgayBatDauTT, 
                    pc.TrangThai, 
                    kq.DiemCongTy        -- Điểm Công ty (Cột để nhập liệu)
                FROM phancong pc
                JOIN sinhvien sv ON pc.MaSV = sv.MaSV
                LEFT JOIN ketqua_thuctap kq ON 
                    pc.MaSV = kq.MaSV AND pc.MaCT = kq.MaCT AND pc.MaGVGS = kq.MaGVGS
                WHERE pc.MaCT = @MaCT"; //

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaCT", maCT)
            };

            // Giả định bạn có một lớp Database.cs với hàm GetData(string query, MySqlParameter[] parameters)
            return DAL.DBHelper.GetData(query, parameters);
        }

        // ====================================================
        // 2. CẬP NHẬT ĐIỂM CÔNG TY ĐƠN GIẢN (TỪ DGV)
        // ====================================================
        public int UpdateDiemCongTySimple(string maSV, string maCT, string maGVGS, decimal diemCongTy)
        {
            // 1. KIỂM TRA bản ghi đã tồn tại trong KETQUA_THUCTAP chưa
            // MaCT phải khớp với MaCT trong KETQUA_THUCTAP (đã giả định là VARCHAR)
            string checkQuery = "SELECT COUNT(*) FROM ketqua_thuctap WHERE MaSV = @MaSV AND MaCT = @MaCT_Str AND MaGVGS = @MaGVGS";

            MySqlParameter[] commonParams = new MySqlParameter[]
            {
            new MySqlParameter("@MaSV", maSV),
            new MySqlParameter("@MaCT_Str", maCT), // Tham số MaCT là string
            new MySqlParameter("@MaGVGS", maGVGS)
            };

            DataTable dt = DAL.DBHelper.GetData(checkQuery, commonParams);
            int count = (dt != null && dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0][0]) : 0;

            string sqlQuery;
            MySqlParameter[] updateParams;

            // Định nghĩa các tham số
            MySqlParameter paramDiem = new MySqlParameter("@DiemCongTy", diemCongTy);
            MySqlParameter paramMaSV = new MySqlParameter("@MaSV", maSV);
            MySqlParameter paramMaCT_Str = new MySqlParameter("@MaCT_Str", maCT);
            MySqlParameter paramMaGVGS = new MySqlParameter("@MaGVGS", maGVGS);

            if (count > 0)
            {
                // UPDATE
                sqlQuery = "UPDATE ketqua_thuctap SET DiemCongTy = @DiemCongTy WHERE MaSV = @MaSV AND MaCT = @MaCT_Str AND MaGVGS = @MaGVGS";
                updateParams = new MySqlParameter[] { paramDiem, paramMaSV, paramMaCT_Str, paramMaGVGS };
            }
            else
            {
                // INSERT
                sqlQuery = "INSERT INTO ketqua_thuctap (MaSV, MaCT, MaGVGS, DiemCongTy) VALUES (@MaSV, @MaCT_Str, @MaGVGS, @DiemCongTy)";
                updateParams = new MySqlParameter[] { paramMaSV, paramMaCT_Str, paramMaGVGS, paramDiem };
            }

            return DAL.DBHelper.ExecuteNonQuery(sqlQuery, updateParams);
        }
    }
}