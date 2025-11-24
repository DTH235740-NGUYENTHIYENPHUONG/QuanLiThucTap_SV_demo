using System;
using System.IO;
using System.Windows.Forms;
using QuanLiThucTap_SV.BLL;
using System.Drawing;


namespace QuanLiThucTap_SV.GIAODIEN_UI 
{
    public partial class frmThemBaoCao : Form
    {
        private string sourceFilePath = string.Empty;
        private string maSV;
        private SinhVienBLL svBLL = new SinhVienBLL();

        // Tên thư mục gốc để lưu file (nằm cùng cấp với file .exe)
        private const string REPORT_FOLDER = "BaoCaoSV";

        public frmThemBaoCao(string studentID)
        {
            InitializeComponent();
            maSV = studentID;
            this.Text = "Nộp Báo Cáo Thực Tập cho SV: " + maSV;

            // Kích hoạt tính năng Drag and Drop
            pnlDropZone.AllowDrop = true;
            pnlDropZone.DragEnter += pnlDropZone_DragEnter;
            pnlDropZone.DragDrop += pnlDropZone_DragDrop;

            txtFileName.ReadOnly = true;
            pnlDropZone.BackColor = Color.LightGray;
        }

        // ===============================================
        // KÉO THẢ (DRAG & DROP)
        // ===============================================

        private void pnlDropZone_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pnlDropZone_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files != null && files.Length > 0)
            {
                sourceFilePath = files[0];
                txtFileName.Text = Path.GetFileName(sourceFilePath);
                pnlDropZone.BackColor = Color.LightGreen;
                MessageBox.Show($"File đã chọn: {Path.GetFileName(sourceFilePath)}.\nBấm 'Upload' để ghi đè báo cáo cũ (nếu có).", "Thông báo");
            }
        }

        // ===============================================
        // B. NÚT UPLOAD/GHI ĐÈ
        // ===============================================

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(sourceFilePath) || string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng kéo thả file báo cáo trước.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // XÁC NHẬN GHI ĐÈ
            DialogResult confirm = MessageBox.Show("Thao tác này sẽ GHI ĐÈ báo cáo hiện tại của bạn. Bạn có chắc chắn muốn tiếp tục?", "Xác nhận Ghi Đè", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;


            try
            {
                string fileName = Path.GetFileName(sourceFilePath);

                // 1. Tạo thư mục lưu trữ 
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string destDirectory = Path.Combine(appDirectory, REPORT_FOLDER, maSV);

                if (!Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }

                // 2. Tạo tên file duy nhất 
                string uniqueFileName = $"{maSV}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_{fileName}";
                string destFilePath = Path.Combine(destDirectory, uniqueFileName);

                // 3. COPY file
                File.Copy(sourceFilePath, destFilePath, true);

                // 4. LƯU đường dẫn mới vào CSDL (Sử dụng hàm UPDATE)
                int result = svBLL.UpdateReportPath(maSV, destFilePath);

                if (result > 0)
                {
                    MessageBox.Show("Nộp báo cáo thành công! (Báo cáo cũ đã được ghi đè nếu tồn tại)", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Xóa file vật lý vừa copy nếu không lưu được vào CSDL
                    if (File.Exists(destFilePath)) File.Delete(destFilePath);
                    MessageBox.Show("Lỗi CSDL: Không thể lưu thông tin báo cáo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi nộp báo cáo: " + ex.Message, "Lỗi Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
