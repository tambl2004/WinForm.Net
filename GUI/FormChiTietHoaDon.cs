using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using DTO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using System.Linq;
using PdfSharp;

namespace GUI
{
    public partial class FormChiTietHoaDon : Form
    {
        private string maHD;
        private DTO.HoaDon hoaDon;

        public FormChiTietHoaDon(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
            SetupDataGridView();

            // Cài đặt sự kiện
            this.Load += FormChiTietHoaDon_Load;
            btnCapNhat.Click += BtnCapNhat_Click;
            btnIn.Click += BtnIn_Click;
            btnDong.Click += BtnDong_Click;
        }

        private void SetupDataGridView()
        {
            dgvChiTietHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTietHoaDon.MultiSelect = false;
            dgvChiTietHoaDon.ReadOnly = true;
            dgvChiTietHoaDon.AllowUserToAddRows = false;
            dgvChiTietHoaDon.AllowUserToDeleteRows = false;
            dgvChiTietHoaDon.AllowUserToResizeRows = false;
            dgvChiTietHoaDon.RowHeadersVisible = false;
            dgvChiTietHoaDon.EnableHeadersVisualStyles = false;
            dgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTietHoaDon.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 232, 255);
            dgvChiTietHoaDon.BorderStyle = BorderStyle.None;
            dgvChiTietHoaDon.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvChiTietHoaDon.GridColor = SystemColors.ButtonShadow;
        }

        private void FormChiTietHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin hóa đơn
                hoaDon = HoaDonBUS.Instance.GetHoaDonByMa(maHD);
                if (hoaDon == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin hóa đơn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Hiển thị thông tin hóa đơn
                lblMaHD.Text = hoaDon.MaHD;
                dtpNgayBan.Value = hoaDon.NgayBan;
                txtGiamGia.Text = hoaDon.GiamGia.ToString("N0");
                txtGhiChu.Text = hoaDon.GhiChu;

                // Lấy thông tin khách hàng
                DataTable dtKhachHang = KhachHangBUS.Instance.GetAll();
                cboKhachHang.DataSource = dtKhachHang;
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";
                cboKhachHang.SelectedValue = hoaDon.MaKH;

                // Lấy thông tin nhân viên
                DataTable dtNhanVien = NhanVienBUS.Instance.GetAllNhanVien();
                cboNhanVien.DataSource = dtNhanVien;
                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "MaNV";
                cboNhanVien.SelectedValue = hoaDon.MaNV;

                // Lấy chi tiết hóa đơn
                DataTable dtChiTiet = HoaDonBUS.Instance.GetChiTietHoaDon(maHD);
                dgvChiTietHoaDon.DataSource = dtChiTiet;

                // Định dạng hiển thị
                FormatDataGridView();

                // Hiển thị tổng tiền
                lblTongTien.Text = $"Tổng tiền: {hoaDon.TongTien:N0} VNĐ";

                // Trạng thái hóa đơn
                chkTrangThai.Checked = hoaDon.TrangThai;

                // Hiển thị thanh toán
                lblThanhToan.Text = $"Thanh toán: {(hoaDon.TongTien - hoaDon.GiamGia):N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ form
                string maKH = cboKhachHang.SelectedValue?.ToString();
                string maNV = cboNhanVien.SelectedValue?.ToString();
                DateTime ngayBan = dtpNgayBan.Value;
                decimal giamGia = 0;
                decimal.TryParse(txtGiamGia.Text.Replace(",", ""), out giamGia);
                string ghiChu = txtGhiChu.Text;
                bool trangThai = chkTrangThai.Checked;

                // Kiểm tra dữ liệu
                if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng và nhân viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tính ThanhToan (Tổng tiền - Giảm giá)
                decimal thanhToan = hoaDon.TongTien - giamGia;
                if (thanhToan < 0) thanhToan = 0;

                // Cập nhật hóa đơn
                bool result = HoaDonBUS.Instance.CapNhatHoaDon(maHD, maKH, maNV, ngayBan, giamGia, ghiChu, trangThai);

                if (result)
                {
                    // Cập nhật lại đối tượng hoaDon
                    hoaDon = HoaDonBUS.Instance.GetHoaDonByMa(maHD);
                    if (hoaDon != null)
                    {
                        hoaDon.ThanhToan = thanhToan; // Cập nhật giá trị ThanhToan
                        lblTongTien.Text = $"Tổng tiền: {hoaDon.TongTien:N0} VNĐ";
                        lblThanhToan.Text = $"Thanh toán: {hoaDon.ThanhToan:N0} VNĐ";
                    }
                    MessageBox.Show("Cập nhật hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Làm mới thông tin
                    FormChiTietHoaDon_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Cập nhật hóa đơn không thành công", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnIn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu trước khi tạo PDF
                if (hoaDon == null || string.IsNullOrEmpty(cboKhachHang.Text) || string.IsNullOrEmpty(cboNhanVien.Text))
                {
                    MessageBox.Show("Dữ liệu hóa đơn không hợp lệ, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở dialog để chọn nơi lưu file PDF
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                    sfd.FileName = $"HoaDon_{maHD}.pdf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GenerateInvoicePDF(sfd.FileName);
                        DialogResult result = MessageBox.Show("Hóa đơn đã được xuất ra file PDF thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                {
                                    FileName = sfd.FileName,
                                    UseShellExecute = true
                                });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Không thể mở file PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi in hóa đơn: {ex.Message}\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatDataGridView()
        {
            if (dgvChiTietHoaDon.Columns.Count > 0)
            {
                // Đặt tên hiển thị và chiều rộng cho các cột
                if (dgvChiTietHoaDon.Columns.Contains("MaXe"))
                {
                    dgvChiTietHoaDon.Columns["MaXe"].HeaderText = "Mã Xe";
                    dgvChiTietHoaDon.Columns["MaXe"].Width = 80;
                }
                if (dgvChiTietHoaDon.Columns.Contains("TenXe"))
                {
                    dgvChiTietHoaDon.Columns["TenXe"].HeaderText = "Tên Xe";
                    dgvChiTietHoaDon.Columns["TenXe"].Width = 150;
                }
                if (dgvChiTietHoaDon.Columns.Contains("HangXe"))
                {
                    dgvChiTietHoaDon.Columns["HangXe"].HeaderText = "Hãng Xe";
                    dgvChiTietHoaDon.Columns["HangXe"].Width = 100;
                }
                if (dgvChiTietHoaDon.Columns.Contains("MauSac"))
                {
                    dgvChiTietHoaDon.Columns["MauSac"].HeaderText = "Màu Sắc";
                    dgvChiTietHoaDon.Columns["MauSac"].Width = 80;
                }
                if (dgvChiTietHoaDon.Columns.Contains("SoLuong"))
                {
                    dgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số Lượng";
                    dgvChiTietHoaDon.Columns["SoLuong"].Width = 80;
                    dgvChiTietHoaDon.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvChiTietHoaDon.Columns.Contains("GiaBan"))
                {
                    dgvChiTietHoaDon.Columns["GiaBan"].HeaderText = "Giá Bán";
                    dgvChiTietHoaDon.Columns["GiaBan"].Width = 100;
                    dgvChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                    dgvChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvChiTietHoaDon.Columns.Contains("GiamGia"))
                {
                    dgvChiTietHoaDon.Columns["GiamGia"].HeaderText = "Giảm Giá";
                    dgvChiTietHoaDon.Columns["GiamGia"].Width = 100;
                    dgvChiTietHoaDon.Columns["GiamGia"].DefaultCellStyle.Format = "N0";
                    dgvChiTietHoaDon.Columns["GiamGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvChiTietHoaDon.Columns.Contains("ThanhTien"))
                {
                    dgvChiTietHoaDon.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                    dgvChiTietHoaDon.Columns["ThanhTien"].Width = 100;
                    dgvChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                    dgvChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvChiTietHoaDon.Columns.Contains("GhiChu"))
                {
                    dgvChiTietHoaDon.Columns["GhiChu"].Visible = false;
                }
            }
        }

        private void GenerateInvoicePDF(string filePath)
        {
            // Khởi tạo tài liệu PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Hóa Đơn Bán Hàng";
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

            // Tiêu đề
            gfx.DrawString("HÓA ĐƠN BÁN HÀNG", titleFont, XBrushes.Black,
                new XRect(0, yPos, pageWidth, 30), XStringFormats.Center);
            yPos += 80;

            // --- THÔNG TIN HÓA ĐƠN (bên trái) ---
            double leftColY = yPos;
            gfx.DrawString("Mã hóa đơn: " + hoaDon.MaHD, boldFont, XBrushes.Black, xPos, leftColY);
            leftColY += 15;
            gfx.DrawString("Ngày bán: " + hoaDon.NgayBan.ToString("dd/MM/yyyy HH:mm"), normalFont, XBrushes.Black, xPos, leftColY);
            leftColY += 15;
            gfx.DrawString("Trạng thái: " + (hoaDon.TrangThai ? "Đã thanh toán" : "Chưa thanh toán"), normalFont, XBrushes.Black, xPos, leftColY);

            // --- THÔNG TIN KHÁCH HÀNG (bên phải) ---
            double rightColX = pageWidth - margin - 250; // Canh phải
            double rightColY = yPos; // Cùng hàng với thông tin hóa đơn
            gfx.DrawString("THÔNG TIN KHÁCH HÀNG", headerFont, XBrushes.Black, rightColX, rightColY);
            rightColY += 20;
            gfx.DrawString($"Khách hàng: {cboKhachHang.Text}", normalFont, XBrushes.Black, rightColX, rightColY);
            rightColY += 15;
            gfx.DrawString($"Nhân viên bán hàng: {cboNhanVien.Text}", normalFont, XBrushes.Black, rightColX, rightColY);
            rightColY += 15;

            if (!string.IsNullOrEmpty(hoaDon.GhiChu))
            {
                gfx.DrawString($"Ghi chú: {hoaDon.GhiChu}", normalFont, XBrushes.Black, rightColX, rightColY);
                rightColY += 15;
            }

            // Cập nhật yPos dựa trên chiều cao lớn nhất của hai cột
            yPos = Math.Max(leftColY, rightColY) + 20;

            // --- BẢNG CHI TIẾT HÓA ĐƠN ---
            yPos += 20; // Tạo khoảng cách với phần trên
            gfx.DrawString("CHI TIẾT HÓA ĐƠN", headerFont, XBrushes.Black, xPos, yPos);
            yPos += 20;

            // Định nghĩa cột với tỷ lệ hợp lý hơn
            string[] headers = { "Mã Xe", "Tên Xe", "Hãng Xe", "Màu Sắc", "Số Lượng", "Giá Bán", "Giảm Giá", "Thành Tiền" };
            double[] widthFactors = { 0.10, 0.20, 0.10, 0.10, 0.12, 0.15, 0.11, 0.15 }; // Tỷ lệ cho các cột

            double tableWidth = contentWidth;
            double[] colWidths = new double[headers.Length];
            for (int i = 0; i < headers.Length; i++)
            {
                colWidths[i] = tableWidth * widthFactors[i];
            }

            // Vẽ header của bảng
            double tableX = xPos;
            double rowHeight = 25;

            // Vẽ nền header (đảm bảo full chiều rộng bảng)
            double totalTableWidth = 0;
            foreach (double width in colWidths)
            {
                totalTableWidth += width;
            }
            XRect headerRect = new XRect(tableX, yPos, totalTableWidth, rowHeight);
            gfx.DrawRectangle(XBrushes.LightGray, headerRect);

            // Vẽ tiêu đề cột với khoảng cách hợp lý
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
            DataTable dtChiTiet = HoaDonBUS.Instance.GetChiTietHoaDon(hoaDon.MaHD);

            foreach (DataRow row in dtChiTiet.Rows)
            {
                cellX = tableX;

                // Vẽ nền hàng
                XRect rowRect = new XRect(tableX, yPos, totalTableWidth, rowHeight);

                // Vẽ từng ô
                for (int i = 0; i < headers.Length; i++)
                {
                    XRect cellRect = new XRect(cellX, yPos, colWidths[i], rowHeight);
                    gfx.DrawRectangle(XPens.Black, cellRect);

                    string value = row[i].ToString();
                    if (i >= 5) // Định dạng số tiền từ cột 6 trở đi
                    {
                        value = Convert.ToDecimal(row[i]).ToString("N0");
                    }

                    // Canh lề cho số
                    XStringFormat format = i >= 4 ? XStringFormats.CenterRight : XStringFormats.CenterLeft;
                    gfx.DrawString(value, normalFont, XBrushes.Black,
                        new XRect(cellX + 5, yPos + 2, colWidths[i] - 10, rowHeight - 4),
                        format);

                    cellX += colWidths[i];
                }

                yPos += rowHeight;

                // Kiểm tra nếu hết trang thì tạo trang mới
                if (yPos > pageHeight - margin - 150)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    yPos = margin;
                }
            }

            // --- THÔNG TIN THANH TOÁN ---
            yPos += 20;

            // Vẽ đường kẻ phân cách
            gfx.DrawLine(XPens.Black, tableX, yPos, tableX + totalTableWidth, yPos);
            yPos += 10;

            // Thông tin tổng tiền (canh phải)
            double paymentInfoX = tableX + totalTableWidth - 250;

            // Cột label
            gfx.DrawString("Tổng tiền hàng:", boldFont, XBrushes.Black,
                new XRect(paymentInfoX, yPos, 150, 20), XStringFormats.TopLeft);
            // Cột giá trị
            gfx.DrawString($"{hoaDon.TongTien:N0} VNĐ", boldFont, XBrushes.Black,
                new XRect(paymentInfoX + 150, yPos, 100, 20), XStringFormats.TopRight);
            yPos += 20;

            gfx.DrawString("Giảm giá:", boldFont, XBrushes.Black,
                new XRect(paymentInfoX, yPos, 150, 20), XStringFormats.TopLeft);
            gfx.DrawString($"{hoaDon.GiamGia:N0} VNĐ", boldFont, XBrushes.Black,
                new XRect(paymentInfoX + 150, yPos, 100, 20), XStringFormats.TopRight);
            yPos += 20;

            // Vẽ đường kẻ trước tổng thanh toán
            gfx.DrawLine(XPens.Black, paymentInfoX, yPos, paymentInfoX + 250, yPos);
            yPos += 10;

            gfx.DrawString("Tổng thanh toán:", new XFont("Times New Roman", 14, XFontStyleEx.Bold), XBrushes.Black,
                new XRect(paymentInfoX, yPos, 150, 25), XStringFormats.TopLeft);
            gfx.DrawString($"{(hoaDon.TongTien - hoaDon.GiamGia):N0} VNĐ",
                new XFont("Times New Roman", 14, XFontStyleEx.Bold), XBrushes.Black,
                new XRect(paymentInfoX + 150, yPos, 100, 25), XStringFormats.TopRight);

            // --- CHỮ KÝ ---
            yPos += 50;

            // Khách hàng (bên trái)
            double signWidth = contentWidth / 3;
            gfx.DrawString("KHÁCH HÀNG", boldFont, XBrushes.Black,
                new XRect(xPos, yPos, signWidth, 20), XStringFormats.Center);
            yPos += 15;
            gfx.DrawString("(Ký, ghi rõ họ tên)", smallFont, XBrushes.Black,
                new XRect(xPos, yPos, signWidth, 20), XStringFormats.Center);

            // Nhân viên bán hàng (giữa)
            gfx.DrawString("NHÂN VIÊN BÁN HÀNG", boldFont, XBrushes.Black,
                new XRect(xPos + signWidth, yPos - 15, signWidth, 20), XStringFormats.Center);
            gfx.DrawString("(Ký, ghi rõ họ tên)", smallFont, XBrushes.Black,
                new XRect(xPos + signWidth, yPos, signWidth, 20), XStringFormats.Center);

            // Người lập hóa đơn (bên phải)
            gfx.DrawString("NGƯỜI LẬP HÓA ĐƠN", boldFont, XBrushes.Black,
                new XRect(xPos + 2 * signWidth, yPos - 15, signWidth, 20), XStringFormats.Center);
            gfx.DrawString("(Ký, ghi rõ họ tên)", smallFont, XBrushes.Black,
                new XRect(xPos + 2 * signWidth, yPos, signWidth, 20), XStringFormats.Center);

            // --- FOOTER ---
            double footerY = pageHeight - margin;
            gfx.DrawString("Cảm ơn quý khách đã mua hàng!",
                new XFont("Times New Roman", 10, XFontStyleEx.Italic), XBrushes.Black,
                new XRect(0, footerY - 20, pageWidth, 20), XStringFormats.Center);

            gfx.DrawString($"In ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", smallFont, XBrushes.Black,
                new XRect(margin, footerY - 15, contentWidth, 15), XStringFormats.TopLeft);

            gfx.DrawString("Trang 1", smallFont, XBrushes.Black,
                new XRect(margin, footerY - 15, contentWidth, 15), XStringFormats.TopRight);

            // Lưu file PDF
            document.Save(filePath);
        }
    }
}