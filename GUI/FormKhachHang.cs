using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Drawing;
using System.Linq;
using DAO;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2022.M03.Main;

namespace GUI
{
    public partial class FormKhachHang : Form
    {
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalRecords;
        private int totalPages;
        private string currentUserRole = FormDangNhap.LoggedInChucVu;

        public FormKhachHang()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadData();
            RegisterEvents();
            CheckUserRole();
        }
        private void CheckUserRole()
        {
            bool isAdmin = currentUserRole == "Admin";
            btnThem.Enabled = isAdmin;
            btnSua.Enabled = isAdmin;
            btnXoa.Enabled = isAdmin;
            btnClear.Enabled = isAdmin;

            if (!isAdmin)
            {
                gb_ThaoTac.Text = "Chỉ dành cho Admin!";
            }
        }

        private void LoadData()
        {
            try
            {
                totalRecords = KhachHangBUS.Instance.GetTotalCount();
                totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                if (totalPages == 0) totalPages = 1; // Ensure at least 1 page
                lblPageInfo.Text = $"{currentPage} / {totalPages}";
                dataGridViewKhachHang.DataSource = KhachHangBUS.Instance.GetAll(currentPage, pageSize);

                // Hide TrangThai column if not needed
                if (dataGridViewKhachHang.Columns["TrangThai"] != null)
                    dataGridViewKhachHang.Columns["TrangThai"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterEvents()
        {
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnClear.Click += BtnClear_Click;
            btnFirstPage.Click += (s, e) => { currentPage = 1; LoadData(); };
            btnPreviousPage.Click += (s, e) => { if (currentPage > 1) { currentPage--; LoadData(); } };
            btnNextPage.Click += (s, e) => { if (currentPage < totalPages) { currentPage++; LoadData(); } };
            btnLastPage.Click += (s, e) => { currentPage = totalPages; LoadData(); };
            dataGridViewKhachHang.SelectionChanged += DataGridViewKhachHang_SelectionChanged;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboGioiTinh.Text))
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGioiTinh.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var kh = new KhachHang
                {
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text.Trim(),
                    DienThoai = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    CMND = txtCMND.Text.Trim()
                };

                // Kiểm tra nếu mã khách hàng đã tồn tại
                if (!string.IsNullOrEmpty(txtMaKH.Text) && KhachHangBUS.Instance.IsMaKHExists(txtMaKH.Text))
                {
                    MessageBox.Show("Mã Khách Hàng đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Generate MaKH
                kh.MaKH = KhachHangDAO.Instance.GenerateNewMaKH();

                if (KhachHangBUS.Instance.Add(kh))
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng thất bại! Kiểm tra dữ liệu hoặc kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                var kh = new KhachHang
                {
                    MaKH = txtMaKH.Text,
                    HoTen = txtHoTen.Text.Trim(),
                    GioiTinh = cboGioiTinh.Text,
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text.Trim(),
                    DienThoai = txtSDT.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    CMND = txtCMND.Text.Trim()
                };

                if (KhachHangBUS.Instance.Update(kh))
                {
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật khách hàng thất bại! Kiểm tra dữ liệu hoặc kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                if (KhachHangBUS.Instance.Delete(txtMaKH.Text))
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Xóa khách hàng thất bại! Kiểm tra dữ liệu hoặc kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string kw = txtTimKiem.Text.Trim();
                if (string.IsNullOrEmpty(kw))
                {
                    LoadData();
                }
                else
                {
                    DataTable searchResult = KhachHangBUS.Instance.Search(kw);
                    dataGridViewKhachHang.DataSource = searchResult;

                    // Recalculate total pages based on search results
                    totalRecords = searchResult.Rows.Count;
                    totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
                    if (totalPages == 0) totalPages = 1; // Ensure at least 1 page
                    currentPage = 1; // Reset to first page on search
                    lblPageInfo.Text = $"{currentPage} / {totalPages}";

                    // Paginate search results
                    if (totalRecords > pageSize)
                    {
                        DataTable pagedData = searchResult.AsEnumerable()
                            .Skip((currentPage - 1) * pageSize)
                            .Take(pageSize)
                            .CopyToDataTable();
                        dataGridViewKhachHang.DataSource = pagedData;
                    }

                    // Hide TrangThai column if not needed
                    if (dataGridViewKhachHang.Columns["TrangThai"] != null)
                        dataGridViewKhachHang.Columns["TrangThai"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadData(); // Reload full data after clearing
        }

        private void SetupDataGridView()
        {
            // Basic setup
            dataGridViewKhachHang.AutoGenerateColumns = false;
            dataGridViewKhachHang.Columns.Clear();
            dataGridViewKhachHang.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "MaKH", HeaderText = "Mã KH", DataPropertyName = "MaKH", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "Họ Tên", DataPropertyName = "HoTen", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "GioiTinh", HeaderText = "Giới Tính", DataPropertyName = "GioiTinh", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "NgaySinh", HeaderText = "Ngày Sinh", DataPropertyName = "NgaySinh", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "DiaChi", HeaderText = "Địa Chỉ", DataPropertyName = "DiaChi", Width = 250 },
                new DataGridViewTextBoxColumn { Name = "DienThoai", HeaderText = "Số Điện Thoại", DataPropertyName = "DienThoai", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "CMND", HeaderText = "CMND", DataPropertyName = "CMND", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", Width = 120 }
            );

            // Format columns
            dataGridViewKhachHang.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Hide TrangThai column if not needed
            if (dataGridViewKhachHang.Columns["TrangThai"] != null)
                dataGridViewKhachHang.Columns["TrangThai"].Visible = false;

            // Customize appearance
            dataGridViewKhachHang.CellFormatting += DataGridViewKhachHang_CellFormatting;
        }

        private void DataGridViewKhachHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewKhachHang.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = value ? "Hoạt động" : "Không hoạt động";
                    e.FormattingApplied = true;
                }
            }
        }

        private void DataGridViewKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewKhachHang.CurrentRow != null && dataGridViewKhachHang.CurrentRow.Index >= 0)
            {
                try
                {
                    var row = dataGridViewKhachHang.CurrentRow;
                    txtMaKH.Text = row.Cells["MaKH"].Value?.ToString() ?? string.Empty;
                    txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? string.Empty;
                    cboGioiTinh.Text = row.Cells["GioiTinh"].Value?.ToString() ?? string.Empty;
                    dtpNgaySinh.Value = row.Cells["NgaySinh"].Value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                    txtSDT.Text = row.Cells["DienThoai"].Value?.ToString() ?? string.Empty;
                    txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;
                    txtCMND.Text = row.Cells["CMND"].Value?.ToString() ?? string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chọn dòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResetForm()
        {
            txtMaKH.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            cboGioiTinh.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Now;
            txtDiaChi.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtCMND.Text = string.Empty;
        }
    }
}