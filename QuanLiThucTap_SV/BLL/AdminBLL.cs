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

        // ====================================================
        // II. QUẢN LÝ PHÂN CÔNG (PHANCONG)
        // ====================================================

        // Lấy tất cả phân công (Hiển thị tên thay vì mã)
        public DataTable GetAllAssignments()
        {
            string query = @"
                SELECT 
                    pc.MaSV, sv.HoTen AS TenSinhVien, 
                    pc.MaCT, ct.TenCT AS TenCongTy,
                    pc.MaGVGS, gv.HoTen AS TenGiangVien,
                    pc.NgayBatDau, pc.TrangThai
                FROM PHANCONG pc
                JOIN SINHVIEN sv ON pc.MaSV = sv.MaSV
                JOIN CONGTY ct ON pc.MaCT = ct.MaCT
                JOIN GIANGVIEN gv ON pc.MaGVGS = gv.MaGV
                ORDER BY pc.TrangThai, pc.NgayBatDau DESC";
            return DAL.DBHelper.GetData(query);
        }

        // Lấy danh sách SV chưa được phân công (dùng cho ComboBox)
        public DataTable GetUnassignedStudents()
        {
            // Giả định SV đã có tài khoản user
            string query = @"
                SELECT MaSV, HoTen 
                FROM SINHVIEN 
                WHERE MaSV NOT IN (SELECT MaSV FROM PHANCONG)";
            return DAL.DBHelper.GetData(query);
        }

        // Lấy danh sách Công ty (dùng cho ComboBox)
        public DataTable GetAllCompanies()
        {
            string query = "SELECT MaCT, TenCT FROM CONGTY";
            return DAL.DBHelper.GetData(query);
        }

        // Lấy danh sách Giảng viên (dùng cho ComboBox)
        public DataTable GetAllLecturers()
        {
            string query = "SELECT MaGV, HoTen FROM GIANGVIEN";
            return DAL.DBHelper.GetData(query);
        }

        public int AddAssignment(string maSV, int maCT, string maGVGS, DateTime ngayBatDau)
        {
            // Mặc định trạng thái ban đầu là 'Cho Duyet'
            string query = @"
                INSERT INTO PHANCONG (MaSV, MaCT, MaGVGS, NgayBatDau, TrangThai) 
                VALUES (@MaSV, @MaCT, @MaGVGS, @NgayBD, 'Cho Duyet');
                
                -- Tạo bản ghi KETQUA_THUCTAP tương ứng
                INSERT INTO KETQUA_THUCTAP (MaSV, MaCT, MaGVGS)
                VALUES (@MaSV, @MaCT, @MaGVGS)";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV", maSV),
                new MySqlParameter("@MaCT", maCT),
                new MySqlParameter("@MaGVGS", maGVGS),
                new MySqlParameter("@NgayBD", ngayBatDau)
            };

            // Chạy cả 2 lệnh INSERT trong cùng 1 transaction nếu cần (để đảm bảo tính toàn vẹn)
            // Tuy nhiên, nếu DAL.Database không hỗ trợ transaction, cứ chạy bình thường.
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // Cập nhật trạng thái phân công
        public int UpdateAssignmentStatus(string maSV, int maCT, string maGVGS, string newStatus)
        {
            string query = @"
                UPDATE PHANCONG 
                SET TrangThai = @NewStatus 
                WHERE MaSV = @MaSV AND MaCT = @MaCT AND MaGVGS = @MaGVGS";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@NewStatus", newStatus),
                new MySqlParameter("@MaSV", maSV),
                new MySqlParameter("@MaCT", maCT),
                new MySqlParameter("@MaGVGS", maGVGS)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }

        // Xóa phân công (sẽ tự động xóa KETQUA_THUCTAP do khóa ngoại CASCADE)
        public int DeleteAssignment(string maSV, int maCT, string maGVGS)
        {
            string query = "DELETE FROM PHANCONG WHERE MaSV = @MaSV AND MaCT = @MaCT AND MaGVGS = @MaGVGS";
            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@MaSV", maSV),
                new MySqlParameter("@MaCT", maCT),
                new MySqlParameter("@MaGVGS", maGVGS)
            };
            return DAL.DBHelper.ExecuteNonQuery(query, parameters);
        }
    }
}