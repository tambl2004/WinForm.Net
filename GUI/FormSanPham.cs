using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BUS;
using DAO;
using DTO;

namespace GUI
{

    public partial class FormSanPham : Form
    {
        private byte[] imageData;
        private int currentPage = 1;
        private int recordsPerPage = 9; // 5 products per page
        private int totalRecords;
        private int totalPages;
        private string currentUserRole = FormDangNhap.LoggedInChucVu;
        private DataTable dtXeMay;

        public FormSanPham()
        {
            InitializeComponent();
            InitializeForm();
            CheckUserRole();
        }

        #region Initialization
        private void CheckUserRole()
        {
            bool isAdmin = currentUserRole == "Admin";
            btnAdd.Enabled = isAdmin;
            btnUpdate.Enabled = isAdmin;
            btnDelete.Enabled = isAdmin;
            btnClear.Enabled = isAdmin;
            btnUploadImage.Enabled = isAdmin;
            btnUpdateInventory.Enabled = isAdmin;

            if (!isAdmin)
            {
                gbActions.Text = "Chức năng chỉ dành cho Admin!";
            }
        }
        private void InitializeForm()
        {
            try
            {
                // Wire up all event handlers programmatically
                btnAdd.Click += btnAdd_Click;
                btnUpdate.Click += btnUpdate_Click;
                btnDelete.Click += btnDelete_Click;
                btnSearch.Click += btnSearch_Click;
                btnUploadImage.Click += btnUploadImage_Click;
                btnUpdateInventory.Click += btnUpdateInventory_Click;
                btnClear.Click += btnClear_Click;
                btnFirstPage.Click += btnFirstPage_Click;
                btnPreviousPage.Click += btnPreviousPage_Click;
                btnNextPage.Click += btnNextPage_Click;
                btnLastPage.Click += btnLastPage_Click;
                dgvSanPham.CellClick += dgvSanPham_CellClick;
               
                LoadData();
                SetupDataGridView();
                LoadComboBoxes();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                dtXeMay = XeMayBUS.Instance.GetXeMayDataTable();
                if (dtXeMay == null || dtXeMay.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu sản phẩm để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtXeMay = new DataTable();
                    dtXeMay.Columns.Add("MaXe", typeof(string));
                    dtXeMay.Columns.Add("TenXe", typeof(string));
                    dtXeMay.Columns.Add("HangXe", typeof(string));
                    dtXeMay.Columns.Add("DongXe", typeof(string));
                    dtXeMay.Columns.Add("MauSac", typeof(string));
                    dtXeMay.Columns.Add("NamSX", typeof(int));
                    dtXeMay.Columns.Add("GiaNhap", typeof(decimal));
                    dtXeMay.Columns.Add("GiaBan", typeof(decimal));
                    dtXeMay.Columns.Add("SoLuongTon", typeof(int));
                    dtXeMay.Columns.Add("TrangThai", typeof(bool));
                }

                totalRecords = dtXeMay.Rows.Count;
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
                if (totalPages == 0) totalPages = 1; // Ensure at least 1 page
                currentPage = 1;
                DisplayPage(currentPage);
                UpdatePagingInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.Columns.Clear();
            dgvSanPham.Columns.Add("MaXe", "Mã Xe");
            dgvSanPham.Columns.Add("TenXe", "Tên Xe");
            dgvSanPham.Columns.Add("HangXe", "Hãng Xe");
            dgvSanPham.Columns.Add("DongXe", "Dòng Xe");
            dgvSanPham.Columns.Add("MauSac", "Màu Sắc");
            dgvSanPham.Columns.Add("NamSX", "Năm SX");
            dgvSanPham.Columns.Add("GiaNhap", "Giá Nhập");
            dgvSanPham.Columns.Add("GiaBan", "Giá Bán");
            dgvSanPham.Columns.Add("SoLuongTon", "Số Lượng Tồn");
            dgvSanPham.Columns.Add("TrangThai", "Trạng Thái");

            dgvSanPham.Columns["MaXe"].DataPropertyName = "MaXe";
            dgvSanPham.Columns["TenXe"].DataPropertyName = "TenXe";
            dgvSanPham.Columns["HangXe"].DataPropertyName = "HangXe";
            dgvSanPham.Columns["DongXe"].DataPropertyName = "DongXe";
            dgvSanPham.Columns["MauSac"].DataPropertyName = "MauSac";
            dgvSanPham.Columns["NamSX"].DataPropertyName = "NamSX";
            dgvSanPham.Columns["GiaNhap"].DataPropertyName = "GiaNhap";
            dgvSanPham.Columns["GiaBan"].DataPropertyName = "GiaBan";
            dgvSanPham.Columns["SoLuongTon"].DataPropertyName = "SoLuongTon";
            dgvSanPham.Columns["TrangThai"].DataPropertyName = "TrangThai";

            // Format columns
            dgvSanPham.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "N0";

            // Set dynamic column widths using Fill mode
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.Columns["MaXe"].FillWeight = 10;
            dgvSanPham.Columns["TenXe"].FillWeight = 17;
            dgvSanPham.Columns["HangXe"].FillWeight = 15;
            dgvSanPham.Columns["DongXe"].FillWeight = 15;
            dgvSanPham.Columns["MauSac"].FillWeight = 10;
            dgvSanPham.Columns["NamSX"].FillWeight = 8;
            dgvSanPham.Columns["GiaNhap"].FillWeight = 12;
            dgvSanPham.Columns["GiaBan"].FillWeight = 12;
            dgvSanPham.Columns["SoLuongTon"].FillWeight = 10;
            dgvSanPham.Columns["TrangThai"].FillWeight = 15;

            // Disable resizing of columns and rows
            dgvSanPham.AllowUserToResizeColumns = false;
            dgvSanPham.AllowUserToResizeRows = false;

            // Customize appearance
            dgvSanPham.CellFormatting += DgvSanPham_CellFormatting;
        }

        private void DgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSanPham.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = value ? "Còn kinh doanh" : "Ngừng kinh doanh";
                    e.FormattingApplied = true;
                }
            }
        }

        private void LoadComboBoxes()
        {
            cbNCC.Items.Clear();
            cbNCC.Items.AddRange(new[] { "Honda", "Yamaha", "Suzuki", "SYM", "Piaggio", "Kawasaki", "Ducati", "BMW" });
            cboLoaiXe.Items.Clear();
            cboLoaiXe.Items.AddRange(new[] { "Xe số", "Xe tay ga", "Xe côn tay", "Xe phân khối lớn" });
            cboMauSac.Items.Clear();
            cboMauSac.Items.AddRange(new[] { "Đỏ", "Đen", "Trắng", "Xanh", "Vàng", "Xám", "Bạc", "Cam", "Tím" });
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.AddRange(new[] { "Còn kinh doanh", "Ngừng kinh doanh" });
            cbTrangThai.SelectedIndex = 0;
        }

        private void ResetForm()
        {
            try
            {
                txtMaXe.Text = XeMayBUS.Instance.GenerateNewMaXe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo mã xe mới: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaXe.Text = "XM" + DateTime.Now.Ticks; // Fallback
            }
            txtTenXe.Text = string.Empty;
            cbNCC.SelectedIndex = -1;
            cboLoaiXe.SelectedIndex = -1;
            cboMauSac.SelectedIndex = -1;
            txtGiaNhap.Text = string.Empty;
            txtGiaBan.Text = string.Empty;
            txtTonKho.Text = "0";
            txtSearch.Text = string.Empty;
            pbProfileImage.Image = null;
            imageData = null;
            cbTrangThai.SelectedIndex = 0;
        }

        #endregion

        #region Data Management

        private void DisplayPage(int page)
        {
            try
            {
                if (dtXeMay == null || dtXeMay.Rows.Count == 0)
                {
                    dgvSanPham.DataSource = null;
                    return;
                }

                DataTable pageData = dtXeMay.Clone();
                int startIndex = (page - 1) * recordsPerPage;
                int endIndex = Math.Min(startIndex + recordsPerPage, totalRecords);

                for (int i = startIndex; i < endIndex; i++)
                {
                    pageData.ImportRow(dtXeMay.Rows[i]);
                }

                dgvSanPham.DataSource = null;
                dgvSanPham.DataSource = pageData;
                dgvSanPham.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị trang: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePagingInfo()
        {
            lblPageInfo.Text = totalPages > 0 ? $"{currentPage} / {totalPages}" : "0 / 0";
            btnFirstPage.Enabled = true;
            btnPreviousPage.Enabled = true;
            btnNextPage.Enabled = true;
            btnLastPage.Enabled = true;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenXe.Text))
            {
                MessageBox.Show("Vui lòng nhập tên xe!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenXe.Focus();
                return false;
            }

            if (cbNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn hãng xe!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbNCC.Focus();
                return false;
            }

            if (cboLoaiXe.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn dòng xe!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiXe.Focus();
                return false;
            }

            if (cboMauSac.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn màu sắc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMauSac.Focus();
                return false;
            }

            if (!decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap) || giaNhap <= 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return false;
            }

            if (!decimal.TryParse(txtGiaBan.Text, out decimal giaBan) || giaBan <= 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return false;
            }

            if (giaBan < giaNhap)
            {
                MessageBox.Show("Giá bán phải lớn hơn giá nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Event Handlers

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                string maXe = txtMaXe.Text.Trim();
                // Check if MaXe already exists
                DataTable dtCheck = DatabaseAcces.Instance.ExecuteQuery(
                    "SELECT COUNT(*) FROM XeMay WHERE MaXe = @MaXe",
                    new[] { new SqlParameter("@MaXe", maXe) }
                );
                if (dtCheck.Rows[0].Field<int>(0) > 0)
                {
                    MessageBox.Show("Mã số xe đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tenXe = txtTenXe.Text.Trim();
                string hangXe = cbNCC.Text;
                string dongXe = cboLoaiXe.Text;
                string mauSac = cboMauSac.Text;
                int namSX = DateTime.Now.Year;
                string soKhung = $"SK{DateTime.Now:yyyyMMddHHmmss}";
                string soMay = $"SM{DateTime.Now:yyyyMMddHHmmss}";
                decimal giaNhap = decimal.Parse(txtGiaNhap.Text);
                decimal giaBan = decimal.Parse(txtGiaBan.Text);
                int soLuongTon = int.Parse(txtTonKho.Text);
                bool trangThai = cbTrangThai.SelectedIndex == 0;

                bool result = XeMayBUS.Instance.AddXeMay(
                    maXe, tenXe, hangXe, dongXe, mauSac, namSX,
                    soKhung, soMay, giaNhap, giaBan,
                    soLuongTon, imageData, string.Empty, trangThai
                );

                if (result)
                {
                    MessageBox.Show("Thêm xe máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Thêm xe máy thất bại! Kiểm tra dữ liệu hoặc kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm xe máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                string maXe = txtMaXe.Text;
                DataTable dt = DatabaseAcces.Instance.ExecuteQuery(
                    "SELECT SoKhung, SoMay FROM XeMay WHERE MaXe = @MaXe",
                    new[] { new SqlParameter("@MaXe", maXe) }
                );

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy xe máy!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool result = XeMayBUS.Instance.UpdateXeMay(
                    maXe, txtTenXe.Text.Trim(), cbNCC.Text, cboLoaiXe.Text, cboMauSac.Text,
                    DateTime.Now.Year, dt.Rows[0]["SoKhung"].ToString(), dt.Rows[0]["SoMay"].ToString(),
                    decimal.Parse(txtGiaNhap.Text), decimal.Parse(txtGiaBan.Text),
                    int.Parse(txtTonKho.Text), imageData, string.Empty, cbTrangThai.SelectedIndex == 0
                );

                if (result)
                {
                    MessageBox.Show("Cập nhật xe máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật xe máy thất bại! Kiểm tra dữ liệu hoặc kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật xe máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaXe.Text))
            {
                MessageBox.Show("Vui lòng chọn xe máy cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa xe máy này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bool result = XeMayBUS.Instance.DeleteXeMay(txtMaXe.Text);
                if (result)
                {
                    MessageBox.Show("Xóa xe máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Xóa xe máy thất bại! Kiểm tra dữ liệu hoặc kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa xe máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dtXeMay = XeMayBUS.Instance.SearchXeMay(txtSearch.Text.Trim());
                if (dtXeMay == null || dtXeMay.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtXeMay = new DataTable();
                    dtXeMay.Columns.Add("MaXe", typeof(string));
                    dtXeMay.Columns.Add("TenXe", typeof(string));
                    dtXeMay.Columns.Add("HangXe", typeof(string));
                    dtXeMay.Columns.Add("DongXe", typeof(string));
                    dtXeMay.Columns.Add("MauSac", typeof(string));
                    dtXeMay.Columns.Add("NamSX", typeof(int));
                    dtXeMay.Columns.Add("GiaNhap", typeof(decimal));
                    dtXeMay.Columns.Add("GiaBan", typeof(decimal));
                    dtXeMay.Columns.Add("SoLuongTon", typeof(int));
                    dtXeMay.Columns.Add("TrangThai", typeof(bool));
                }

                totalRecords = dtXeMay.Rows.Count;
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);
                if (totalPages == 0) totalPages = 1; // Ensure at least 1 page
                currentPage = 1;
                DisplayPage(currentPage);
                UpdatePagingInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                    Title = "Chọn ảnh xe"
                })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        imageData = File.ReadAllBytes(ofd.FileName);
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            pbProfileImage.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                imageData = null;
                pbProfileImage.Image = null;
            }
        }

        private void btnUpdateInventory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaXe.Text))
            {
                MessageBox.Show("Vui lòng chọn xe máy!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtTonKho.Text, out int soLuongTon) || soLuongTon < 0)
            {
                MessageBox.Show("Số lượng tồn kho không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool result = XeMayBUS.Instance.UpdateSoLuongTon(txtMaXe.Text, soLuongTon);
                if (result)
                {
                    MessageBox.Show("Cập nhật tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật tồn kho thất bại! Vui lòng chọn sản phẩm cần cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật tồn kho: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvSanPham.Rows.Count) return;

            try
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                txtMaXe.Text = row.Cells["MaXe"].Value?.ToString() ?? string.Empty;
                txtTenXe.Text = row.Cells["TenXe"].Value?.ToString() ?? string.Empty;
                cbNCC.Text = row.Cells["HangXe"].Value?.ToString() ?? string.Empty;
                cboLoaiXe.Text = row.Cells["DongXe"].Value?.ToString() ?? string.Empty;
                cboMauSac.Text = row.Cells["MauSac"].Value?.ToString() ?? string.Empty;
                txtGiaNhap.Text = row.Cells["GiaNhap"].Value?.ToString() ?? string.Empty;
                txtGiaBan.Text = row.Cells["GiaBan"].Value?.ToString() ?? string.Empty;
                txtTonKho.Text = row.Cells["SoLuongTon"].Value?.ToString() ?? "0";

                bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                cbTrangThai.SelectedIndex = trangThai ? 0 : 1;

                byte[] hinhAnh = XeMayBUS.Instance.GetHinhAnhByMaXe(txtMaXe.Text);
                if (hinhAnh != null && hinhAnh.Length > 0)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(hinhAnh))
                        {
                            pbProfileImage.Image = Image.FromStream(ms);
                        }
                        imageData = hinhAnh;
                    }
                    catch
                    {
                        pbProfileImage.Image = null;
                        imageData = null;
                    }
                }
                else
                {
                    pbProfileImage.Image = null;
                    imageData = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn dòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayPage(currentPage);
            UpdatePagingInfo();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage(currentPage);
                UpdatePagingInfo();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayPage(currentPage);
                UpdatePagingInfo();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            DisplayPage(currentPage);
            UpdatePagingInfo();
        }

        #endregion
    }
}