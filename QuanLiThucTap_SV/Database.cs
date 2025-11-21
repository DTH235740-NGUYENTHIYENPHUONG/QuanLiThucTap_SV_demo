using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
// Đã xóa using System.Security.Cryptography; và using System.Text;

namespace QuanLiThucTap_SV
{
    internal class Database
    {
        // Cập nhật thông tin kết nối CSDL 
        private static string connectionString = "Server=localhost;Database=quanlithuctap;Uid=root;Pwd=123456;";


        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool CheckLogin(string username, string passcode, out string role)
        {
            role = string.Empty;
       
            string query = "SELECT Role FROM USERS WHERE Username = @user AND Passcode = @pass AND TrangThai = 1";

            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user", username);
                    command.Parameters.AddWithValue("@pass", passcode);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            role = result.ToString();
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        // Hiển thị chi tiết lỗi kết nối để dễ debug (Ví dụ: Sai mật khẩu MySQL)
                        MessageBox.Show("Lỗi kết nối CSDL hoặc truy vấn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        public static string GetCodeByUsername(string username)
        {
            // Giả định: Username của Giảng viên là MaGV. 
            // Nếu username của Giảng viên được lưu là MaGV, thì chỉ cần trả về username.
            // Nếu bạn muốn lấy HoTen hay MaGV từ bảng GIANGVIEN dựa vào Username, cần phải JOIN.

            // Tạm thời trả về Username (vì MaGV là varchar(10) và Username cũng là string)
            string query = "SELECT Username FROM USERS WHERE Username = @username AND Role = 'GiangVien'";

            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString(); // Trả về MaGV (chính là Username)
                        }
                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tra cứu mã người dùng: " + ex.Message, "Lỗi");
                        return string.Empty;
                    }
                }
            }

        }

        // Lấy danh sách sinh viên được phân công cho giảng viên hướng dẫn cụ thể
        public static DataTable GetStudentsByLecturer(string maGVGS)
        {
            DataTable dt = new DataTable();
            // JOIN giữa SINHVIEN và PHANCONG để lấy danh sách sinh viên được phân công cho MaGVGS
            string query = @"
        SELECT 
            SV.MaSV, SV.HoTen, SV.NgaySinh, SV.GioiTinh, SV.Email, SV.SoDienThoai, SV.MaLop,
            PC.MaCT, PC.TrangThai
        FROM SINHVIEN SV
        INNER JOIN PHANCONG PC ON SV.MaSV = PC.MaSV
        WHERE PC.MaGVGS = @maGVGS";

            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maGVGS", maGVGS);

                    try
                    {
                        connection.Open();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi tải danh sách sinh viên: " + ex.Message, "Lỗi CSDL");
                    }
                }
            }
            return dt;
        }

        // Thêm sinh viên mới và phân công giảng viên hướng dẫn 
        public static bool InsertNewStudent(string maSV, string hoTen, DateTime ngaySinh, string gioiTinh, string email, string sdt, string maLop, string maGVGS)
        {
            MySqlConnection connection = GetConnection();
            MySqlTransaction transaction = null; // Khởi tạo Transaction

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                // 1. INSERT vào bảng SINHVIEN
                string insertSVQuery = @"
            INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, MaLop)
            VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @SDT, @MaLop)";

                using (MySqlCommand cmdSV = new MySqlCommand(insertSVQuery, connection, transaction))
                {
                    cmdSV.Parameters.AddWithValue("@MaSV", maSV);
                    cmdSV.Parameters.AddWithValue("@HoTen", hoTen);
                    cmdSV.Parameters.AddWithValue("@NgaySinh", ngaySinh.ToString("yyyy-MM-dd"));
                    cmdSV.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmdSV.Parameters.AddWithValue("@Email", email);
                    cmdSV.Parameters.AddWithValue("@SDT", sdt);
                    cmdSV.Parameters.AddWithValue("@MaLop", maLop);
                    cmdSV.ExecuteNonQuery();
                }

                // 2. INSERT vào bảng PHANCONG (MaCT giả định là 1 nếu chưa chọn công ty)
                // Bạn cần thay thế MaCT_Default nếu có dữ liệu công ty
                int MaCT_Default = 1;

                string insertPCQuery = @"
            INSERT INTO PHANCONG (MaSV, MaCT, MaGVGS, NgayBatDauTT, TrangThai)
            VALUES (@MaSV_PC, @MaCT, @MaGVGS, CURDATE(), 'Cho Duyet')";

                using (MySqlCommand cmdPC = new MySqlCommand(insertPCQuery, connection, transaction))
                {
                    cmdPC.Parameters.AddWithValue("@MaSV_PC", maSV);
                    cmdPC.Parameters.AddWithValue("@MaCT", MaCT_Default);
                    cmdPC.Parameters.AddWithValue("@MaGVGS", maGVGS);
                    cmdPC.ExecuteNonQuery();
                }

                // Commit (Xác nhận) cả hai thao tác thành công
                transaction.Commit();
                return true;
            }
            catch (MySqlException sqlEx)
            {
                // Rollback (Hủy bỏ) nếu có lỗi xảy ra
                transaction?.Rollback();
                if (sqlEx.Number == 1062) // Lỗi trùng khóa chính (MaSV)
                {
                    MessageBox.Show("Mã sinh viên này đã tồn tại trong hệ thống.", "Lỗi Trùng Mã");
                }
                else
                {
                    MessageBox.Show("Lỗi CSDL khi thêm sinh viên: " + sqlEx.Message, "Lỗi");
                }
                return false;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi");
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}