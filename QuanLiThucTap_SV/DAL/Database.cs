using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLiThucTap_SV.DAL 
{
    // Lớp hỗ trợ truy cập cơ sở dữ liệu (DAL)
    internal static class DBHelper
    {
        
        private static string connectionString = "Server=localhost;Database=qlitt;Uid=root;Pwd=123456;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // ====================================================
        // HÀM LẤY DỮ LIỆU (SELECT)
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
                        // Hiển thị lỗi ngay tại đây nếu có vấn đề về kết nối/truy vấn
                        MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return dt;
        }

        // ====================================================
        // HÀM THỰC THI LỆNH (INSERT, UPDATE, DELETE)
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
                        MessageBox.Show("Lỗi thao tác dữ liệu: " + ex.Message, "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
            return rowsAffected;
        }
    }
}