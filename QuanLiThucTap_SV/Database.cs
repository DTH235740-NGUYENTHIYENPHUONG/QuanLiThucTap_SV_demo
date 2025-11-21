using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;


namespace QuanLiThucTap_SV
{
    internal class Database
    {
        // Connection String
        private static string connectionString = "Server=localhost;Database=qltt;Uid=root;Pwd=123456;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // ====================================================
        // 1. KIỂM TRA ĐĂNG NHẬP 
        // ====================================================
        // Thêm tham số 'out int maUser' để lấy ID người dùng lưu vào Session
        public static bool CheckLogin(string username, string passcode, out string role, out int maUser)
        {
            role = string.Empty;
            maUser = -1;

            // Lấy cả Role và MaUser
            string query = "SELECT MaUser, Role FROM USERS WHERE Username = @user AND Passcode = @pass AND TrangThai = 1";

            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user", username);
                    command.Parameters.AddWithValue("@pass", passcode);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                maUser = reader.GetInt32("MaUser"); // Lấy ID
                                role = reader.GetString("Role");    // Lấy Quyền
                                return true;
                            }
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
        }

        // ====================================================
        // 2. HÀM LẤY DỮ LIỆU (Dùng cho SELECT - Hiển thị lên DataGridView)
        // ====================================================
        public static DataTable GetData(string sql, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        connection.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
                    }
                }
            }
            return dt;
        }

        // ====================================================
        // 3. HÀM THỰC THI LỆNH (Dùng cho INSERT, UPDATE, DELETE)
        // ====================================================
        public static int ExecuteNonQuery(string sql, MySqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi thao tác dữ liệu: " + ex.Message);
                        return -1; // Trả về -1 nếu lỗi
                    }
                }
            }
            return rowsAffected;
        }
    }
}