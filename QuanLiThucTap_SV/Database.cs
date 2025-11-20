using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
// Đã xóa using System.Security.Cryptography; và using System.Text;

namespace QuanLiThucTap_SV
{
    internal class Database
    {
        // Cập nhật thông tin kết nối CSDL 
        private static string connectionString = "Server=localhost;Database=qltt;Uid=root;Pwd=123456;";


        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool CheckLogin(string username, string password, out string role)
        {
            role = string.Empty;
       
            string query = "SELECT Role FROM USERS WHERE Username = @user AND Password = @pass AND TrangThai = 1";

            using (MySqlConnection connection = GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user", username);
                    command.Parameters.AddWithValue("@pass", password);

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
    }
}