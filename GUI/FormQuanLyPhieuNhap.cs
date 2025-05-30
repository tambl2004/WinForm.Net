using System;
using System.Data;
using System.Windows.Forms;
using BUS;
using ClosedXML.Excel;
using DTO;

namespace GUI
{
    public partial class FormQuanLyPhieuNhap : Form
    {
        private int currentPage = 1;
        private int pageSize = 12;
        private int totalPage = 1;

        public FormQuanLyPhieuNhap()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadData();
            RegisterEvents();
        }

        private void SetupDataGridView()
        {
            // Thiết lập Dock properties để DataGridView lấp đầy không gian
            // Thêm vào phương thức SetupDataGridView
            dgvPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Setup dgvPhieuNhap
            dgvPhieuNhap.AutoGenerateColumns = false;

            // Add columns với DataPropertyName để map đúng với dữ liệu từ SQL
            DataGridViewTextBoxColumn colMaPN = new DataGridViewTextBoxColumn();
            colMaPN.Name = "MaPN";
            colMaPN.HeaderText = "Mã phiếu nhập";
            colMaPN.DataPropertyName = "MaPN"; // Map với cột MaPN từ SQL
            dgvPhieuNhap.Columns.Add(colMaPN);

            DataGridViewTextBoxColumn colNgayNhap = new DataGridViewTextBoxColumn();
            colNgayNhap.Name = "NgayNhap";
            colNgayNhap.HeaderText = "Ngày nhập";
            colNgayNhap.DataPropertyName = "NgayNhap"; // Map với cột NgayNhap từ SQL
            colNgayNhap.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvPhieuNhap.Columns.Add(colNgayNhap);

            DataGridViewTextBoxColumn colNCC = new DataGridViewTextBoxColumn();
            colNCC.Name = "TenNCC";
            colNCC.HeaderText = "Nhà cung cấp";
            colNCC.DataPropertyName = "TenNCC"; // Map với cột TenNCC từ SQL
            dgvPhieuNhap.Columns.Add(colNCC);

            DataGridViewTextBoxColumn colNV = new DataGridViewTextBoxColumn();
            colNV.Name = "NhanVien";
            colNV.HeaderText = "Nhân viên";
            colNV.DataPropertyName = "NhanVien"; // Map với cột NhanVien từ SQL
            dgvPhieuNhap.Columns.Add(colNV);

            DataGridViewTextBoxColumn colTien = new DataGridViewTextBoxColumn();
            colTien.Name = "TongTien";
            colTien.HeaderText = "Tổng tiền";
            colTien.DataPropertyName = "TongTien"; // Map với cột TongTien từ SQL
            colTien.DefaultCellStyle.Format = "N0";
            colTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPhieuNhap.Columns.Add(colTien);


            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            colTrangThai.Name = "TrangThai";
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.DataPropertyName = "TrangThai";
            dgvPhieuNhap.Columns.Add(colTrangThai);
            // Thêm sự kiện CellFormatting để format giá trị của cột TrangThai
            dgvPhieuNhap.CellFormatting += DgvPhieuNhap_CellFormatting;


            // Add columns
            DataGridViewTextBoxColumn colMaXe = new DataGridViewTextBoxColumn();
            colMaXe.Name = "MaXe";
            colMaXe.HeaderText = "Mã xe";
            colMaXe.DataPropertyName = "MaXe"; // Map với cột MaXe từ SQL
          

            DataGridViewTextBoxColumn colTenXe = new DataGridViewTextBoxColumn();
            colTenXe.Name = "TenXe";
            colTenXe.HeaderText = "Tên xe";
            colTenXe.DataPropertyName = "TenXe"; // Map với cột TenXe từ SQL
           

            DataGridViewTextBoxColumn colHangXe = new DataGridViewTextBoxColumn();
            colHangXe.Name = "HangXe";
            colHangXe.HeaderText = "Hãng xe";
            colHangXe.DataPropertyName = "HangXe"; // Map với cột HangXe từ SQL
    
            DataGridViewTextBoxColumn colMauSac = new DataGridViewTextBoxColumn();
            colMauSac.Name = "MauSac";
            colMauSac.HeaderText = "Màu sắc";
            colMauSac.DataPropertyName = "MauSac"; // Map với cột MauSac từ SQL
         

            DataGridViewTextBoxColumn colSoLuong = new DataGridViewTextBoxColumn();
            colSoLuong.Name = "SoLuong";
            colSoLuong.HeaderText = "Số lượng";
            colSoLuong.DataPropertyName = "SoLuong"; // Map với cột SoLuong từ SQL
            colSoLuong.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        

            DataGridViewTextBoxColumn colGiaNhap = new DataGridViewTextBoxColumn();
            colGiaNhap.Name = "GiaNhap";
            colGiaNhap.HeaderText = "Giá nhập";
            colGiaNhap.DataPropertyName = "GiaNhap"; // Map với cột GiaNhap từ SQL
            colGiaNhap.DefaultCellStyle.Format = "N0";
            colGiaNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          

            DataGridViewTextBoxColumn colThanhTien = new DataGridViewTextBoxColumn();
            colThanhTien.Name = "ThanhTien";
            colThanhTien.HeaderText = "Thành tiền";
            colThanhTien.DataPropertyName = "ThanhTien"; // Map với cột ThanhTien từ SQL
            colThanhTien.DefaultCellStyle.Format = "N0";
            colThanhTien.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
        }

        private void LoadData()
        {
            try
            {
                int total = NhapHangBUS.Instance.GetTotalCount();
                totalPage = (int)Math.Ceiling(total / (double)pageSize);
                lblPageInfo.Text = $"{currentPage} / {totalPage}";

                DataTable dt = NhapHangBUS.Instance.GetAllPhieuNhap(currentPage, pageSize);
                dgvPhieuNhap.DataSource = dt;

                // Clear detail
            
                btnCapNhat.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterEvents()
        {
            // Pagination
            btnFirstPage.Click += (s, e) => { currentPage = 1; LoadData(); };
            btnPreviousPage.Click += (s, e) => { if (currentPage > 1) { currentPage--; LoadData(); } };
            btnNextPage.Click += (s, e) => { if (currentPage < totalPage) { currentPage++; LoadData(); } };
            btnLastPage.Click += (s, e) => { currentPage = totalPage; LoadData(); };
            btn_XuatExcel.Click += Btn_XuatExcel_Click;
            // Search
            btnTimKiem.Click += BtnTimKiem_Click;

            // Selection changed
            dgvPhieuNhap.SelectionChanged += DgvPhieuNhap_SelectionChanged;

            // Update
            btnCapNhat.Click += BtnCapNhat_Click;

            // Date check state changed
            chkNgay.CheckedChanged += chkNgay_CheckedChanged;

            // Placeholder effect for search textbox
            txtTimKiem.Enter += TxtTimKiem_Enter;
            txtTimKiem.Leave += TxtTimKiem_Leave;
        }
        // Thêm method xử lý sự kiện CellFormatting
        private void DgvPhieuNhap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvPhieuNhap.Columns["TrangThai"].Index && e.Value != null)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = value ? "Nhập Hàng Thành Công" : "Chưa Hoàn Thành";
                    e.FormattingApplied = true;
                }
            }
        }
        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            DateTime? fromDate = null;
            DateTime? toDate = null;

            if (chkNgay.Checked)
            {
                fromDate = dtpTuNgay.Value.Date;
                toDate = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
            }

            DataTable dt = NhapHangBUS.Instance.SearchPhieuNhap(keyword, fromDate, toDate);
            dgvPhieuNhap.DataSource = dt;
            lblPageInfo.Text = "Tìm kiếm";
        }

        private void DgvPhieuNhap_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvPhieuNhap.CurrentRow != null && dgvPhieuNhap.CurrentRow.Index >= 0)
                {
                    if (dgvPhieuNhap.Columns.Contains("MaPN") && dgvPhieuNhap.CurrentRow.Cells["MaPN"].Value != null)
                    {
                        string maPN = dgvPhieuNhap.CurrentRow.Cells["MaPN"].Value.ToString();
                        DataTable dt = NhapHangBUS.Instance.GetChiTietPhieuNhap(maPN);
                       

                        // Enable update button
                        btnCapNhat.Enabled = true;
                    }
                    else
                    {
                      
                        btnCapNhat.Enabled = false;
                    }
                }
                else
                {
                 
                    btnCapNhat.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
                btnCapNhat.Enabled = false;
            }
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.CurrentRow == null) return;

            string maPN = dgvPhieuNhap.CurrentRow.Cells["MaPN"].Value.ToString();
            PhieuNhap phieuNhap = NhapHangBUS.Instance.GetPhieuNhap(maPN);

            if (phieuNhap == null) return;

            // Show form to update
            using (FormChiTietPhieuNhap frm = new FormChiTietPhieuNhap(phieuNhap))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Refresh data
                    LoadData();
                }
            }
        }

        private void chkNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkNgay.Checked;
            dtpDenNgay.Enabled = chkNgay.Checked;
        }

        private void TxtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập mã phiếu nhập, tên nhà cung cấp hoặc tên nhân viên để tìm kiếm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void TxtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Nhập mã phiếu nhập, tên nhà cung cấp hoặc tên nhân viên để tìm kiếm...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void Btn_XuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    Title = "Xuất danh sách phiếu nhập",
                    FileName = $"DanhSachPhieuNhap_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("DanhSachPhieuNhap");

                        // Tiêu đề
                        worksheet.Cell(1, 1).Value = "Danh Sách Phiếu Nhập";
                        worksheet.Cell(1, 1).Style.Font.Bold = true;
                        worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                        worksheet.Range("A1:G1").Merge();

                        // Tiêu đề cột
                        for (int i = 0; i < dgvPhieuNhap.Columns.Count; i++)
                        {
                            worksheet.Cell(3, i + 1).Value = dgvPhieuNhap.Columns[i].HeaderText;
                            worksheet.Cell(3, i + 1).Style.Font.Bold = true;
                            worksheet.Cell(3, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                        }

                        // Dữ liệu
                        for (int i = 0; i < dgvPhieuNhap.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvPhieuNhap.Columns.Count; j++)
                            {
                                var cellValue = dgvPhieuNhap.Rows[i].Cells[j].Value?.ToString();
                                worksheet.Cell(i + 4, j + 1).Value = cellValue;

                                // Định dạng số tiền
                                if (dgvPhieuNhap.Columns[j].Name == "TongTien")
                                {
                                    worksheet.Cell(i + 4, j + 1).Style.NumberFormat.Format = "#,##0";
                                }
                                // Định dạng ngày
                                else if (dgvPhieuNhap.Columns[j].Name == "NgayNhap")
                                {
                                    worksheet.Cell(i + 4, j + 1).Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
                                }
                            }
                        }

                        // Điều chỉnh độ rộng cột
                        worksheet.Columns().AdjustToContents();

                        // Lưu file
                        workbook.SaveAs(saveDialog.FileName);
                        MessageBox.Show($"Đã xuất danh sách phiếu nhập thành công đến {saveDialog.FileName}",
                                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất danh sách phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}