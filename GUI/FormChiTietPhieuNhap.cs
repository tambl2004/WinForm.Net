using System;
using System.Data;
using System.Windows.Forms;
using BUS;
using DTO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace GUI
{
    public partial class FormChiTietPhieuNhap : Form
    {
        private PhieuNhap phieuNhap;

        public FormChiTietPhieuNhap(PhieuNhap phieuNhap)
        {
            InitializeComponent();
            this.phieuNhap = phieuNhap;
            LoadNhaCungCap();
            LoadNhanVien();
            LoadData();
            RegisterEvents();
        }

        private void LoadNhaCungCap()
        {
            DataTable dt = NhapHangBUS.Instance.GetAllNhaCungCap();
            cmbNhaCungCap.DataSource = dt;
            cmbNhaCungCap.DisplayMember = "TenNCC";
            cmbNhaCungCap.ValueMember = "MaNCC";
        }

        private void LoadNhanVien()
        {
            DataTable dt = NhapHangBUS.Instance.GetAllNhanVien();
            cmbNhanVien.DataSource = dt;
            cmbNhanVien.DisplayMember = "HoTen";
            cmbNhanVien.ValueMember = "MaNV";
        }

        private void LoadData()
        {
            // Load PhieuNhap info
            txtMaPN.Text = phieuNhap.MaPN;
            dtpNgayNhap.Value = phieuNhap.NgayNhap;
            cmbNhaCungCap.SelectedValue = phieuNhap.MaNCC;
            cmbNhanVien.SelectedValue = phieuNhap.MaNV;
            chkTrangThai.Checked = phieuNhap.TrangThai;

            // Load ChiTietPhieuNhap
            DataTable dt = NhapHangBUS.Instance.GetChiTietPhieuNhap(phieuNhap.MaPN);
            dgvChiTiet.DataSource = dt;

            // Format columns
            if (dgvChiTiet.Columns.Count > 0)
            {
                dgvChiTiet.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
        }

        private void RegisterEvents()
        {
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += BtnHuy_Click;
            btnIn.Click += btnIn_Click;
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update PhieuNhap
            phieuNhap.NgayNhap = dtpNgayNhap.Value;
            phieuNhap.MaNCC = cmbNhaCungCap.SelectedValue.ToString();
            phieuNhap.MaNV = cmbNhanVien.SelectedValue.ToString();
            phieuNhap.TrangThai = chkTrangThai.Checked;

            bool success = NhapHangBUS.Instance.UpdatePhieuNhap(phieuNhap);

            if (success)
            {
                MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Cập nhật phiếu nhập thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                Title = "Lưu hóa đơn PDF",
                FileName = $"PhieuNhap_{phieuNhap.MaPN}.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                GenerateInvoicePDF(saveFileDialog.FileName);
                DialogResult result = MessageBox.Show("Tạo file PDF thành công! Bạn có muốn mở file không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
        }

        private void GenerateInvoicePDF(string filePath)
        {
            // Khởi tạo tài liệu PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Phiếu Nhập Hàng";
            PdfPage page = document.AddPage();
            page.Size = PageSize.A4;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Định nghĩa font
            XFont titleFont = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
            XFont headerFont = new XFont("Times New Roman", 14, XFontStyleEx.Bold);
            XFont normalFont = new XFont("Times New Roman", 12, XFontStyleEx.Regular);
            XFont boldFont = new XFont("Times New Roman", 12, XFontStyleEx.Bold);
            XFont smallFont = new XFont("Times New Roman", 10, XFontStyleEx.Regular);

            // Kích thước và canh lề
            double margin = 50;
            double pageWidth = page.Width.Point;
            double pageHeight = page.Height.Point;
            double contentWidth = pageWidth - 2 * margin;

            // Vị trí bắt đầu
            double xPos = margin;
            double yPos = margin;

            // --- HEADER SECTION ---
            gfx.DrawString("PHIẾU NHẬP HÀNG", titleFont, XBrushes.Black,
                new XRect(0, yPos, pageWidth, 30), XStringFormats.Center);
            yPos += 80; // Giảm khoảng cách từ 80 xuống 40 để gần hơn

            // --- THÔNG TIN CHUNG (Mã phiếu nhập và Ngày nhập/Trạng thái) ---
            gfx.DrawString($"Mã phiếu nhập: {phieuNhap.MaPN}", boldFont, XBrushes.Black, xPos, yPos);
            yPos += 20;
            gfx.DrawString($"Ngày nhập: {phieuNhap.NgayNhap:dd/MM/yyyy HH:mm}", normalFont, XBrushes.Black, xPos, yPos);
            yPos += 15;
            gfx.DrawString($"Trạng thái: {(phieuNhap.TrangThai ? "Hoàn tất" : "Chưa hoàn tất")}", normalFont, XBrushes.Black, xPos, yPos);
            yPos += 15;

            // --- THÔNG TIN PHIẾU NHẬP ---
            double rightColX = pageWidth - margin - 250; // Canh phải
            double rightColY = yPos; // Cùng hàng với thông tin hóa đơn
            yPos += 20;
            gfx.DrawString("THÔNG TIN PHIẾU NHẬP", headerFont, XBrushes.Black, xPos, yPos);
            yPos += 20;
            gfx.DrawString($"Nhà cung cấp: {cmbNhaCungCap.Text}", normalFont, XBrushes.Black, xPos, yPos);
            yPos += 15;
            gfx.DrawString($"Nhân viên nhập: {cmbNhanVien.Text}", normalFont, XBrushes.Black, xPos, yPos);
            yPos += 15;

            if (!string.IsNullOrEmpty(phieuNhap.GhiChu))
            {
                gfx.DrawString($"Ghi chú: {phieuNhap.GhiChu}", normalFont, XBrushes.Black, xPos, yPos);
                yPos += 15;
            }

            // --- BẢNG CHI TIẾT PHIẾU NHẬP ---
            yPos += 20; // Thêm khoảng cách trước khi vẽ bảng
            gfx.DrawString("CHI TIẾT PHIẾU NHẬP", headerFont, XBrushes.Black, xPos, yPos);
            yPos += 20;

            // Định nghĩa cột
            string[] headers = { "Mã Xe", "Tên Xe", "Hãng Xe", "Màu Sắc", "Số Lượng", "Giá Nhập", "Thành Tiền" };
            double[] widthFactors = { 0.1, 0.20, 0.15, 0.15, 0.12, 0.15, 0.15 }; // Tỷ lệ chiều rộng

            double tableWidth = contentWidth;
            double[] colWidths = new double[headers.Length];
            for (int i = 0; i < headers.Length; i++)
            {
                colWidths[i] = tableWidth * widthFactors[i];
            }

            // Vẽ header của bảng với nền full width
            double tableX = xPos;
            double rowHeight = 25;

            XRect headerRect = new XRect(tableX, yPos, pageWidth - 2 * margin, rowHeight); // Full width
            gfx.DrawRectangle(XBrushes.LightGray, headerRect);

            double cellX = tableX;
            for (int i = 0; i < headers.Length; i++)
            {
                XRect cellRect = new XRect(cellX, yPos, colWidths[i], rowHeight);
                gfx.DrawRectangle(XPens.Black, cellRect);
                gfx.DrawString(headers[i], boldFont, XBrushes.Black,
                    new XRect(cellX + 5, yPos + 2, colWidths[i] - 10, rowHeight - 4),
                    XStringFormats.Center);
                cellX += colWidths[i];
            }
            yPos += rowHeight;

            // Dữ liệu chi tiết
            DataTable dtChiTiet = NhapHangBUS.Instance.GetChiTietPhieuNhap(phieuNhap.MaPN);
            decimal totalAmount = 0;

            foreach (DataRow row in dtChiTiet.Rows)
            {
                cellX = tableX;

                XRect rowRect = new XRect(tableX, yPos, tableWidth, rowHeight);

                for (int i = 0; i < headers.Length; i++)
                {
                    XRect cellRect = new XRect(cellX, yPos, colWidths[i], rowHeight);
                    gfx.DrawRectangle(XPens.Black, cellRect);

                    string value = row[i].ToString();
                    if (i >= 4) // Định dạng số từ cột Số Lượng, Giá Nhập, Thành Tiền
                    {
                        value = Convert.ToDecimal(row[i]).ToString("N0");
                    }

                    XStringFormat format = i >= 4 ? XStringFormats.CenterRight : XStringFormats.CenterLeft;
                    gfx.DrawString(value, normalFont, XBrushes.Black,
                        new XRect(cellX + 5, yPos + 2, colWidths[i] - 10, rowHeight - 4),
                        format);

                    cellX += colWidths[i];
                }

                totalAmount += Convert.ToDecimal(row["ThanhTien"]);
                yPos += rowHeight;

                // Kiểm tra nếu hết trang thì tạo trang mới
                if (yPos > pageHeight - margin - 100)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = margin;
                }
            }

            // --- TỔNG KẾT ---
            yPos += 20;
            gfx.DrawString($"Tổng tiền: {totalAmount:N0} VNĐ", boldFont, XBrushes.Black,
                new XRect(xPos, yPos, contentWidth, 20), XStringFormats.TopRight);

            // Lưu file
            document.Save(filePath);
            document.Close();
        }
    }
}