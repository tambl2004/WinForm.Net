using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class FormNhaCungCap : Form
    {
        private DataTable dtNhaCungCap;
        private int currentPage = 1;
        private int recordsPerPage = 10;
        private int totalRecords = 0;
        private int totalPages = 0;

        public FormNhaCungCap()
        {
            InitializeComponent();
            LoadData();
            SetupDataGridView();
            InitializeEvents();
        }

        private void LoadData()
        {
            try
            {
                // Load data from database
                dtNhaCungCap = BUS.NhaCungCapBUS.Instance.GetAllNhaCungCap();
                totalRecords = dtNhaCungCap.Rows.Count;
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);

                // Initialize textbox MaNCC with new generated ID
                txtMaNCC.Text = BUS.NhaCungCapBUS.Instance.GenerateNewMaNCC();

                // Display data
                DisplayDataAtPage(currentPage);
                UpdatePagingInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            // Setup columns for DataGridView
            dataGridViewNCC.AutoGenerateColumns = false;

            dataGridViewNCC.Columns.Clear();
            dataGridViewNCC.Columns.Add("MaNCC", "Mã NCC");
            dataGridViewNCC.Columns.Add("TenNCC", "Tên NCC");
            dataGridViewNCC.Columns.Add("DiaChi", "Địa chỉ");
            dataGridViewNCC.Columns.Add("DienThoai", "Điện thoại");
            dataGridViewNCC.Columns.Add("Email", "Email");
            dataGridViewNCC.Columns.Add("NguoiLienHe", "Người liên hệ");
            dataGridViewNCC.Columns.Add("TrangThai", "Trạng thái");

            dataGridViewNCC.Columns["MaNCC"].DataPropertyName = "MaNCC";
            dataGridViewNCC.Columns["TenNCC"].DataPropertyName = "TenNCC";
            dataGridViewNCC.Columns["DiaChi"].DataPropertyName = "DiaChi";
            dataGridViewNCC.Columns["DienThoai"].DataPropertyName = "DienThoai";
            dataGridViewNCC.Columns["Email"].DataPropertyName = "Email";
            dataGridViewNCC.Columns["NguoiLienHe"].DataPropertyName = "NguoiLienHe";
            dataGridViewNCC.Columns["TrangThai"].DataPropertyName = "TrangThai";

            // Format TrangThai column
            dataGridViewNCC.Columns["TrangThai"].DefaultCellStyle.Format = "Hoạt động;Ngừng Hoạt Động";

            // Set column widths
            dataGridViewNCC.Columns["MaNCC"].Width = 100;
            dataGridViewNCC.Columns["TenNCC"].Width = 180;
            dataGridViewNCC.Columns["DiaChi"].Width = 180;
            dataGridViewNCC.Columns["DienThoai"].Width = 120;
            dataGridViewNCC.Columns["Email"].Width = 180;
            dataGridViewNCC.Columns["NguoiLienHe"].Width = 200;
            dataGridViewNCC.Columns["TrangThai"].Width = 120;

            // Customize appearance
            dataGridViewNCC.CellFormatting += DataGridViewNCC_CellFormatting;
        }

        private void DataGridViewNCC_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewNCC.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = value ? "Hoạt động" : "Ngừng Hoạt Động";
                    e.FormattingApplied = true;
                }
            }
        }

        private void InitializeEvents()
        {
            // Button events
            btnThem.Click += BtnThem_Click;
            btnSua.Click += BtnSua_Click;
            btnXoa.Click += BtnXoa_Click;
            btnTimKiem.Click += BtnTimKiem_Click;
            btnClear.Click += BtnClear_Click;

            // Navigation buttons
            button4.Click += BtnFirstPage_Click; // Trang đầu
            button3.Click += BtnPreviousPage_Click; // Trang trước
            button2.Click += BtnNextPage_Click; // Trang sau
            button1.Click += BtnLastPage_Click; // Trang cuối

            // DataGridView events
            dataGridViewNCC.SelectionChanged += DataGridViewNCC_SelectionChanged;

            // KeyPress events for text boxes
            txtSoDienThoai.KeyPress += TxtSoDienThoai_KeyPress;
        }

        private void TxtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers and control characters (like backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DisplayDataAtPage(int page)
        {
            if (dtNhaCungCap.Rows.Count == 0) return;

            // Ensure page is within valid range
            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;
            currentPage = page;

            // Calculate start and end records for the current page
            int startIndex = (currentPage - 1) * recordsPerPage;
            int endIndex = Math.Min(startIndex + recordsPerPage - 1, totalRecords - 1);

            // Create a new DataTable for the current page
            DataTable dtPage = dtNhaCungCap.Clone();
            for (int i = startIndex; i <= endIndex; i++)
            {
                dtPage.ImportRow(dtNhaCungCap.Rows[i]);
            }

            // Bind the data to the DataGridView
            dataGridViewNCC.DataSource = dtPage;
        }

        private void UpdatePagingInfo()
        {
            label9.Text = $"{currentPage} / {totalPages}";
            button4.Enabled = true; // Trang đầu
            button3.Enabled = true; // Trang trước
            button2.Enabled = true; // Trang sau
            button1.Enabled = true; // Trang cuối
        }

        private void ClearInputFields()
        {
            txtMaNCC.Text = BUS.NhaCungCapBUS.Instance.GenerateNewMaNCC();
            txtTenNCC.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNguoiLienHe.Text = string.Empty;
            chkTrangThai.Checked = true;
        }

        private void DataGridViewNCC_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewNCC.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewNCC.SelectedRows[0];
                txtMaNCC.Text = row.Cells["MaNCC"].Value.ToString();
                txtTenNCC.Text = row.Cells["TenNCC"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                txtSoDienThoai.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtNguoiLienHe.Text = row.Cells["NguoiLienHe"].Value?.ToString() ?? "";
                chkTrangThai.Checked = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNCC.Focus();
                    return;
                }

                // Add new supplier
                bool result = BUS.NhaCungCapBUS.Instance.AddNhaCungCap(
                    txtTenNCC.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSoDienThoai.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtNguoiLienHe.Text.Trim()
                );

                if (result)
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Thêm nhà cung cấp thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenNCC.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNCC.Focus();
                    return;
                }

                // Update supplier
                bool result = BUS.NhaCungCapBUS.Instance.UpdateNhaCungCap(
                    txtMaNCC.Text.Trim(),
                    txtTenNCC.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSoDienThoai.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtNguoiLienHe.Text.Trim(),
                    chkTrangThai.Checked
                );

                if (result)
                {
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Cập nhật nhà cung cấp thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate selection
                if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirm deletion
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa nhà cung cấp này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Delete supplier
                    bool success = BUS.NhaCungCapBUS.Instance.DeleteNhaCungCap(txtMaNCC.Text.Trim());

                    if (success)
                    {
                        MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhà cung cấp thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                dtNhaCungCap = BUS.NhaCungCapBUS.Instance.SearchNhaCungCap(keyword);

                totalRecords = dtNhaCungCap.Rows.Count;
                totalPages = (int)Math.Ceiling((double)totalRecords / recordsPerPage);

                currentPage = 1;
                DisplayDataAtPage(currentPage);
                UpdatePagingInfo();

                if (totalRecords == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFirstPage_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayDataAtPage(currentPage);
            UpdatePagingInfo();
        }

        private void BtnPreviousPage_Click(object sender, EventArgs e)
        {
            currentPage--;
            DisplayDataAtPage(currentPage);
            UpdatePagingInfo();
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            currentPage++;
            DisplayDataAtPage(currentPage);
            UpdatePagingInfo();
        }

        private void BtnLastPage_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            DisplayDataAtPage(currentPage);
            UpdatePagingInfo();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            LoadData();
        }
    }
}