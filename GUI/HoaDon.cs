using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ClosedXML.Excel;

namespace GUI
{
    public partial class HoaDon : Form
    {
        private int currentPage = 1;
        private int pageSize = 11; // Đổi thành 12 hóa đơn mỗi trang
        private int totalPages = 0;

        public HoaDon()
        {
            InitializeComponent();
            SetupDataGridView();
            this.Load += FormLichSuBanHang_Load;
            btnSearch.Click += BtnSearch_Click;
            btnExport.Click += BtnExport_Click;
            dgvHoaDon.CellDoubleClick += DgvHoaDon_CellDoubleClick;
            button3.Click += BtnPrevious_Click; // Trang trước
            button2.Click += BtnNext_Click;     // Trang sau
            button4.Click += BtnFirst_Click;    // Trang đầu
            button1.Click += BtnLast_Click;     // Trang cuối
        }

        private void SetupDataGridView()
        {
            dgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHoaDon.MultiSelect = false;
            dgvHoaDon.ReadOnly = true;
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.AllowUserToDeleteRows = false;
            dgvHoaDon.AllowUserToResizeRows = false;
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 232, 255);
            dgvHoaDon.BorderStyle = BorderStyle.None;
            dgvHoaDon.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgvHoaDon.GridColor = SystemColors.ButtonShadow;

        }

        private void FormLichSuBanHang_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                int totalCount = HoaDonBUS.Instance.GetTotalCount();
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                if (totalPages == 0) totalPages = 1;

                DataTable dt = HoaDonBUS.Instance.GetDanhSachHoaDon(currentPage, pageSize);
                dgvHoaDon.DataSource = dt;

                FormatDataGridView();
                label9.Text = $"{currentPage} / {totalPages}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                DateTime? fromDate = chkDateFilter.Checked ? dtpFrom.Value.Date : (DateTime?)null;
                DateTime? toDate = chkDateFilter.Checked ? dtpTo.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

                DataTable dt = HoaDonBUS.Instance.TimKiemHoaDon(keyword, fromDate, toDate);
                dgvHoaDon.DataSource = dt;

                FormatDataGridView();
                currentPage = 1; // Reset to first page after search
                int totalCount = dt.Rows.Count;
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                label9.Text = $"{currentPage} / {totalPages}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Xuất báo cáo hóa đơn",
                    FileName = $"DanhSachHoaDon_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("DanhSachHoaDon");

                        // Tiêu đề
                        worksheet.Cell(1, 1).Value = "Danh Sách Hóa Đơn";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range("A1:I1").Merge();

                        // Tiêu đề cột
                        for (int i = 0; i < dgvHoaDon.Columns.Count; i++)
                        {
                            worksheet.Cell(3, i + 1).Value = dgvHoaDon.Columns[i].HeaderText;
                            worksheet.Cell(3, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(3, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                        }

                        // Dữ liệu
                        for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvHoaDon.Columns.Count; j++)
                            {
                                var cellValue = dgvHoaDon.Rows[i].Cells[j].Value?.ToString();
                                worksheet.Cell(i + 4, j + 1).Value = cellValue;

                                // Định dạng số tiền
                                if (dgvHoaDon.Columns[j].Name == "TongTien" ||
                                    dgvHoaDon.Columns[j].Name == "GiamGia" ||
                                    dgvHoaDon.Columns[j].Name == "ThanhToan")
                                {
                                    worksheet.Cell(i + 4, j + 1).Style.NumberFormat.Format = "#,##0";
                                }
                                // Định dạng ngày
                                else if (dgvHoaDon.Columns[j].Name == "NgayBan")
                                {
                                    worksheet.Cell(i + 4, j + 1).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                                }
                            }
                        }

                        // Điều chỉnh độ rộng cột
                        worksheet.Columns().AdjustToContents();

                        // Lưu file
                        workbook.SaveAs(saveDialog.FileName);
                        MessageBox.Show($"Đã xuất báo cáo thành công đến {saveDialog.FileName}",
                                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage = 1;
                LoadData();
            }
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage = totalPages;
                LoadData();
            }
        }

        private void DgvHoaDon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maHD = dgvHoaDon.Rows[e.RowIndex].Cells["MaHD"].Value.ToString();
                FormChiTietHoaDon formChiTiet = new FormChiTietHoaDon(maHD);
                formChiTiet.FormClosed += (s, args) => LoadData(); // Làm mới danh sách sau khi đóng form chi tiết
                formChiTiet.ShowDialog();
            }
        }

        private void FormatDataGridView()
        {
            if (dgvHoaDon.Columns.Count > 0)
            {
                dgvHoaDon.Columns["MaHD"].HeaderText = "Mã Hóa Đơn";
                dgvHoaDon.Columns["MaHD"].Width = 100;
                dgvHoaDon.Columns["NgayBan"].HeaderText = "Ngày Bán";
                dgvHoaDon.Columns["NgayBan"].Width = 120;
                dgvHoaDon.Columns["NgayBan"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvHoaDon.Columns["KhachHang"].HeaderText = "Khách Hàng";
                dgvHoaDon.Columns["KhachHang"].Width = 150;
                dgvHoaDon.Columns["NhanVien"].HeaderText = "Nhân Viên";
                dgvHoaDon.Columns["NhanVien"].Width = 140;
                dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvHoaDon.Columns["TongTien"].Width = 100;
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvHoaDon.Columns["GiamGia"].HeaderText = "Giảm Giá";
                dgvHoaDon.Columns["GiamGia"].Width = 100;
                dgvHoaDon.Columns["GiamGia"].DefaultCellStyle.Format = "N0";
                dgvHoaDon.Columns["ThanhToan"].HeaderText = "Thanh Toán";
                dgvHoaDon.Columns["ThanhToan"].Width = 100;
                dgvHoaDon.Columns["ThanhToan"].DefaultCellStyle.Format = "N0";
                dgvHoaDon.Columns["TrangThai"].Width = 120;

              
            }
        }
    }
}