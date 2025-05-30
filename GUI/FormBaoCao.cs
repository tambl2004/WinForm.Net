using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ClosedXML.Excel;

namespace GUI
{
    public partial class FormBaoCao : Form
    {
        private int currentPage = 1;
        private int pageSize = 10;
        private DataTable currentDataTable;
        private int selectedReportType = -1;

        public FormBaoCao()
        {
            InitializeComponent();
            RegisterEvents();
            ConfigureDefaultSettings();
        }

        private void ConfigureDefaultSettings()
        {
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-30);
            dateTimePickerTo.Value = DateTime.Now;
            lblPageInfo.Text = "1 / 1";
        }

        private void RegisterEvents()
        {
            btn_DoanhThuNgay.Click += (s, e) => GenerateReport(0);
            btn_DoanhThuThang.Click += (s, e) => GenerateReport(1);
            btn_SanPhamBanChay.Click += (s, e) => GenerateReport(2);
            btn_BaoCaoTonKho.Click += (s, e) => GenerateReport(3);
            btn_BaoCaoNhapHang.Click += (s, e) => GenerateReport(4);
            btn_BaoCaoXuatHang.Click += (s, e) => GenerateReport(5);
            btn_xuatExel.Click += BtnExportExcel_Click;
            btn_Reset.Click += BtnReset_Click;
            btn_LocNgay.Click += BtnLocNgay_Click;

            btnFirstPage.Click += (s, e) => NavigateToPage(1);
            btnPreviousPage.Click += (s, e) => NavigateToPage(currentPage - 1);
            btnNextPage.Click += (s, e) => NavigateToPage(currentPage + 1);
            btnLastPage.Click += (s, e) => NavigateToLastPage();
        }

        private void BtnLocNgay_Click(object sender, EventArgs e)
        {
            if (selectedReportType != 0)
            {
                MessageBox.Show("Bạn cần chọn Doanh Thu Theo Ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dateTimePickerFrom.Value > dateTimePickerTo.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GenerateReport(0);
        }

        private void GenerateReport(int reportType)
        {
            try
            {
                selectedReportType = reportType;
                dataGridViewBaoCao.DataSource = null;
                currentDataTable = null;
                lblReportSummary.Text = "Báo cáo: Đang tải...";

                switch (reportType)
                {
                    case 0:
                        currentDataTable = BaoCaoBUS.Instance.BaoCaoDoanhThu(dateTimePickerFrom.Value, dateTimePickerTo.Value);
                        UpdateReportSummary("Doanh thu theo ngày", "DoanhThuThuc");
                        break;
                    case 1:
                        int namDoanhThu = DateTime.Now.Year;
                        currentDataTable = BaoCaoBUS.Instance.BaoCaoDoanhThuTheoThang(namDoanhThu);
                        UpdateReportSummary("Doanh thu theo tháng", "DoanhThuThuc");
                        break;
                    case 2:
                        currentDataTable = BaoCaoBUS.Instance.BaoCaoXeBanChay(dateTimePickerFrom.Value, dateTimePickerTo.Value, 20);
                        UpdateReportSummary("Sản phẩm bán chạy", "TongDoanhThu");
                        break;
                    case 3:
                        currentDataTable = BaoCaoBUS.Instance.BaoCaoTonKho();
                        UpdateReportSummary("Tồn kho", "SoLuongTon", isInventory: true);
                        break;
                    case 4:
                        currentDataTable = BaoCaoBUS.Instance.BaoCaoNhapHang(dateTimePickerFrom.Value, dateTimePickerTo.Value);
                        UpdateReportSummary("Nhập hàng", "TongTien");
                        break;
                    case 5:
                        currentDataTable = BaoCaoBUS.Instance.BaoCaoXuatHang(dateTimePickerFrom.Value, dateTimePickerTo.Value);
                        UpdateReportSummary("Xuất hàng", "TongDoanhThu");
                        break;
                }

                if (currentDataTable != null && currentDataTable.Rows.Count > 0)
                {
                    UpdateDataGridView();
                    ConfigureColumns(reportType); // Đặt lại headerText sau khi gán DataSource
                    UpdatePagingInfo();
                }
                else
                {
                    lblReportSummary.Text = $"Báo cáo: {GetReportName(reportType)} - Không có dữ liệu";
                    MessageBox.Show("Không có dữ liệu báo cáo cho thời gian đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                lblReportSummary.Text = "Báo cáo: Lỗi";
                MessageBox.Show($"Lỗi khi tạo báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureColumns(int reportType)
        {
            switch (reportType)
            {
                case 0: ConfigureDoanhThuColumns(); break;
                case 1: ConfigureDoanhThuTheoThangColumns(); break;
                case 2: ConfigureXeBanChayColumns(); break;
                case 3: ConfigureTonKhoColumns(); break;
                case 4: ConfigureNhapHangColumns(); break;
                case 5: ConfigureXuatHangColumns(); break;
            }
        }

        private string GetReportName(int reportType)
        {
            switch (reportType)
            {
                case 0: return "Doanh thu theo ngày";
                case 1: return "Doanh thu theo tháng";
                case 2: return "Sản phẩm bán chạy";
                case 3: return "Tồn kho";
                case 4: return "Nhập hàng";
                case 5: return "Xuất hàng";
                default: return "Chưa chọn";
            }
        }

        private void UpdateReportSummary(string reportName, string sumColumn, bool isInventory = false)
        {
            if (currentDataTable == null || currentDataTable.Rows.Count == 0)
            {
                lblReportSummary.Text = $"Báo cáo: {reportName} - Không có dữ liệu";
                return;
            }

            decimal total = 0;
            foreach (DataRow row in currentDataTable.Rows)
            {
                if (row[sumColumn] != DBNull.Value)
                {
                    total += Convert.ToDecimal(row[sumColumn]);
                }
            }

            if (isInventory)
            {
                lblReportSummary.Text = $"Báo cáo: {reportName} - Tổng số lượng tồn: {total:N0}";
            }
            else
            {
                lblReportSummary.Text = $"Báo cáo: {reportName} - Tổng doanh thu: {total:N0} VNĐ";
            }
        }

        private void UpdateDataGridView()
        {
            DataTable pageData = currentDataTable.Clone();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, currentDataTable.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                pageData.ImportRow(currentDataTable.Rows[i]);
            }

            dataGridViewBaoCao.DataSource = pageData;
        }

        private void UpdatePagingInfo()
        {
            if (currentDataTable == null || currentDataTable.Rows.Count == 0)
            {
                lblPageInfo.Text = "1 / 1";
                return;
            }

            int totalPages = (int)Math.Ceiling((double)currentDataTable.Rows.Count / pageSize);
            lblPageInfo.Text = $"{currentPage} / {totalPages}";
            btnPreviousPage.Enabled = true;
            btnNextPage.Enabled = true;
            btnFirstPage.Enabled = true;
            btnLastPage.Enabled = true;
        }

        private void NavigateToPage(int page)
        {
            if (currentDataTable == null) return;

            int totalPages = (int)Math.Ceiling((double)currentDataTable.Rows.Count / pageSize);
            if (page >= 1 && page <= totalPages)
            {
                currentPage = page;
                UpdateDataGridView();
                ConfigureColumns(selectedReportType); // Đặt lại headerText sau khi gán DataSource
                UpdatePagingInfo();
            }
        }

        private void NavigateToLastPage()
        {
            if (currentDataTable == null) return;
            int totalPages = (int)Math.Ceiling((double)currentDataTable.Rows.Count / pageSize);
            NavigateToPage(totalPages);
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewBaoCao.DataSource == null || dataGridViewBaoCao.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Xuất báo cáo",
                    FileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("BaoCao");

                        worksheet.Cell(1, 1).Value = "Báo Cáo và Thống Kê";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range("A1:" + (char)('A' + dataGridViewBaoCao.Columns.Count - 1) + "1").Merge();

                        for (int i = 0; i < dataGridViewBaoCao.Columns.Count; i++)
                        {
                            worksheet.Cell(3, i + 1).Value = dataGridViewBaoCao.Columns[i].HeaderText;
                            worksheet.Cell(3, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(3, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                        }

                        for (int i = 0; i < dataGridViewBaoCao.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridViewBaoCao.Columns.Count; j++)
                            {
                                var cellValue = dataGridViewBaoCao.Rows[i].Cells[j].Value?.ToString();
                                worksheet.Cell(i + 4, j + 1).Value = cellValue;

                                if (dataGridViewBaoCao.Columns[j].Name == "TongDoanhThu" ||
                                    dataGridViewBaoCao.Columns[j].Name == "TongGiamGia" ||
                                    dataGridViewBaoCao.Columns[j].Name == "DoanhThuThuc" ||
                                    dataGridViewBaoCao.Columns[j].Name == "GiaBanTrungBinh" ||
                                    dataGridViewBaoCao.Columns[j].Name == "GiaNhap" ||
                                    dataGridViewBaoCao.Columns[j].Name == "GiaBan" ||
                                    dataGridViewBaoCao.Columns[j].Name == "GiaTriTon" ||
                                    dataGridViewBaoCao.Columns[j].Name == "TongTien")
                                {
                                    worksheet.Cell(i + 4, j + 1).Style.NumberFormat.Format = "#,##0";
                                }
                                else if (dataGridViewBaoCao.Columns[j].Name == "TongSoLuong")
                                {
                                    worksheet.Cell(i + 4, j + 1).Style.NumberFormat.Format = "N0";
                                }
                                else if (dataGridViewBaoCao.Columns[j].Name == "NgayBan" ||
                                         dataGridViewBaoCao.Columns[j].Name == "NgayNhap")
                                {
                                    worksheet.Cell(i + 4, j + 1).Style.DateFormat.Format = "dd/MM/yyyy";
                                }
                            }
                        }

                        worksheet.Columns().AdjustToContents();
                        workbook.SaveAs(saveDialog.FileName);

                        if (MessageBox.Show($"Đã xuất báo cáo thành công đến {saveDialog.FileName}. Bạn có muốn mở file không?",
                                            "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-30);
            dateTimePickerTo.Value = DateTime.Now;
            dataGridViewBaoCao.DataSource = null;
            currentDataTable = null;
            currentPage = 1;
            selectedReportType = -1;
            lblPageInfo.Text = "1 / 1";
            btnPreviousPage.Enabled = false;
            btnNextPage.Enabled = false;
            btnFirstPage.Enabled = false;
            btnLastPage.Enabled = false;
            lblReportSummary.Text = "Báo cáo: Chưa chọn";
        }

        #region Column Configuration Methods
        private void ConfigureDoanhThuColumns()
        {
            if (dataGridViewBaoCao.Columns.Count > 0)
            {
                dataGridViewBaoCao.Columns["NgayBan"].HeaderText = "Ngày bán";
                dataGridViewBaoCao.Columns["SoHoaDon"].HeaderText = "Số hóa đơn";
                dataGridViewBaoCao.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                dataGridViewBaoCao.Columns["TongGiamGia"].HeaderText = "Tổng giảm giá";
                dataGridViewBaoCao.Columns["DoanhThuThuc"].HeaderText = "Doanh thu thực";

                dataGridViewBaoCao.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["TongGiamGia"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["DoanhThuThuc"].DefaultCellStyle.Format = "N0";
            }
        }

        private void ConfigureDoanhThuTheoThangColumns()
        {
            if (dataGridViewBaoCao.Columns.Count > 0)
            {
                dataGridViewBaoCao.Columns["Thang"].HeaderText = "Tháng";
                dataGridViewBaoCao.Columns["SoHoaDon"].HeaderText = "Số hóa đơn";
                dataGridViewBaoCao.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                dataGridViewBaoCao.Columns["TongGiamGia"].HeaderText = "Tổng giảm giá";
                dataGridViewBaoCao.Columns["DoanhThuThuc"].HeaderText = "Doanh thu thực";

                dataGridViewBaoCao.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["TongGiamGia"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["DoanhThuThuc"].DefaultCellStyle.Format = "N0";
            }
        }

        private void ConfigureXeBanChayColumns()
        {
            if (dataGridViewBaoCao.Columns.Count > 0)
            {
                dataGridViewBaoCao.Columns["MaXe"].HeaderText = "Mã xe";
                dataGridViewBaoCao.Columns["TenXe"].HeaderText = "Tên xe";
                dataGridViewBaoCao.Columns["HangXe"].HeaderText = "Hãng xe";
                dataGridViewBaoCao.Columns["DongXe"].HeaderText = "Dòng xe";
                dataGridViewBaoCao.Columns["TongSoLuongBan"].HeaderText = "Tổng số lượng bán";
                dataGridViewBaoCao.Columns["GiaBanTrungBinh"].HeaderText = "Giá bán trung bình";
                dataGridViewBaoCao.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";

                dataGridViewBaoCao.Columns["GiaBanTrungBinh"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
            }
        }

        private void ConfigureTonKhoColumns()
        {
            if (dataGridViewBaoCao.Columns.Count > 0)
            {
                dataGridViewBaoCao.Columns["MaXe"].HeaderText = "Mã xe";
                dataGridViewBaoCao.Columns["TenXe"].HeaderText = "Tên xe";
                dataGridViewBaoCao.Columns["HangXe"].HeaderText = "Hãng xe";
                dataGridViewBaoCao.Columns["DongXe"].HeaderText = "Dòng xe";
                dataGridViewBaoCao.Columns["MauSac"].HeaderText = "Màu sắc";
                dataGridViewBaoCao.Columns["NamSX"].HeaderText = "Năm sản xuất";
                dataGridViewBaoCao.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
                dataGridViewBaoCao.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dataGridViewBaoCao.Columns["GiaBan"].HeaderText = "Giá bán";
                dataGridViewBaoCao.Columns["GiaTriTon"].HeaderText = "Giá trị tồn";

                dataGridViewBaoCao.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["GiaTriTon"].DefaultCellStyle.Format = "N0";
            }
        }
        private void ConfigureNhapHangColumns()
        {
            if (dataGridViewBaoCao.Columns.Count > 0)
            {
                dataGridViewBaoCao.Columns["NgayNhap"].HeaderText = "Ngày nhập";
                dataGridViewBaoCao.Columns["SoPhieuNhap"].HeaderText = "Số phiếu nhập";
                dataGridViewBaoCao.Columns["TongSoLuong"].HeaderText = "Tổng số lượng";
                dataGridViewBaoCao.Columns["TongTien"].HeaderText = "Tổng tiền";

                dataGridViewBaoCao.Columns["TongSoLuong"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
        }

        private void ConfigureXuatHangColumns()
        {
            if (dataGridViewBaoCao.Columns.Count > 0)
            {
                dataGridViewBaoCao.Columns["NgayBan"].HeaderText = "Ngày xuất";
                dataGridViewBaoCao.Columns["SoHoaDon"].HeaderText = "Số hóa đơn";
                dataGridViewBaoCao.Columns["TongSoLuong"].HeaderText = "Tổng số lượng";
                dataGridViewBaoCao.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";

                dataGridViewBaoCao.Columns["TongSoLuong"].DefaultCellStyle.Format = "N0";
                dataGridViewBaoCao.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
            }
        }
        #endregion
    }
}
