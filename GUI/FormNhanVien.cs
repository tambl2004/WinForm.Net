using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class FormNhanVien : Form
    {
        private byte[] currentImage = null;
        private int pageSize = 10;
        private int currentPage = 1;
        private int totalPages = 1;
        private DataTable allNhanVien = null;
        private string currentUserRole = FormDangNhap.LoggedInChucVu;

        public FormNhanVien()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadGioiTinhComboBox();
            LoadChucVuComboBox();
            SetupDataGridView();
            UpdateButtonState();
            CheckUserRole();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            btnSearch.Click += btnSearch_Click;
            btnChooseImage.Click += btnChooseImage_Click;
            btnClearImage.Click += btnClearImage_Click;
            btnFirstPage.Click += btnFirstPage_Click;
            btnPreviousPage.Click += btnPreviousPage_Click;
            btnNextPage.Click += btnNextPage_Click;
            btnLastPage.Click += btnLastPage_Click;
            txtSearch.KeyDown += txtSearch_KeyDown;
            dgvEmployees.SelectionChanged += DgvEmployees_SelectionChanged;
        }

        private void LoadGioiTinhComboBox()
        {
            cbGender.Items.Clear();
            cbGender.Items.Add("Nam");
            cbGender.Items.Add("Nữ");
            cbGender.SelectedIndex = 0;
        }

        private void LoadChucVuComboBox()
        {
            cbRole.Items.Clear();
            cbRole.Items.Add("Admin");
            cbRole.Items.Add("Nhân viên bán hàng");
            cbRole.Items.Add("Nhân viên kho");
            cbRole.SelectedIndex = 1;
        }

        private void CheckUserRole()
        {
            bool isAdmin = currentUserRole == "Admin";
            btnAdd.Enabled = isAdmin;
            btnUpdate.Enabled = isAdmin;
            btnDelete.Enabled = isAdmin;
            txtUsername.ReadOnly = !isAdmin;
            txtPassword.ReadOnly = !isAdmin;
            cbRole.Enabled = isAdmin;
            if (!isAdmin)
            {
                gbActions.Text = "Thao tác (Chỉ quản lý mới có quyền chỉnh sửa)";
            }
        }

        private void SetupDataGridView()
        {
            dgvEmployees.AutoGenerateColumns = false;
            dgvEmployees.Columns.Clear();
            dgvEmployees.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "MaNV", HeaderText = "Mã NV", DataPropertyName = "MaNV" },
                new DataGridViewTextBoxColumn { Name = "HoTen", HeaderText = "Họ Tên", DataPropertyName = "HoTen" },
                new DataGridViewTextBoxColumn { Name = "GioiTinh", HeaderText = "Giới Tính", DataPropertyName = "GioiTinh" },
                new DataGridViewTextBoxColumn { Name = "NgaySinh", HeaderText = "Ngày Sinh", DataPropertyName = "NgaySinh" },
                new DataGridViewTextBoxColumn { Name = "DiaChi", HeaderText = "Địa Chỉ", DataPropertyName = "DiaChi" },
                new DataGridViewTextBoxColumn { Name = "DienThoai", HeaderText = "SĐT", DataPropertyName = "DienThoai" },
                new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email" },
                new DataGridViewTextBoxColumn { Name = "ChucVu", HeaderText = "Chức Vụ", DataPropertyName = "ChucVu" },
                new DataGridViewTextBoxColumn { Name = "TenDangNhap", HeaderText = "Tài Khoản", DataPropertyName = "TenDangNhap" },
                new DataGridViewTextBoxColumn { Name = "HinhAnh", HeaderText = "Hình Ảnh", DataPropertyName = "HinhAnh" },
                new DataGridViewTextBoxColumn { Name = "MatKhau", HeaderText = "Mật Khẩu", DataPropertyName = "MatKhau" },
                new DataGridViewTextBoxColumn { Name = "TrangThai", HeaderText = "Trạng Thái", DataPropertyName = "TrangThai" }
            );

            // Format columns
            dgvEmployees.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Hide columns
            if (dgvEmployees.Columns.Contains("HinhAnh"))
                dgvEmployees.Columns["HinhAnh"].Visible = false;
            if (dgvEmployees.Columns.Contains("MatKhau"))
                dgvEmployees.Columns["MatKhau"].Visible = false;
            if (dgvEmployees.Columns.Contains("TrangThai"))
                dgvEmployees.Columns["TrangThai"].Visible = false;

            // Customize appearance
            dgvEmployees.CellFormatting += DgvEmployees_CellFormatting;
        }

        private void DgvEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEmployees.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                if (e.Value is bool)
                {
                    bool value = (bool)e.Value;
                    e.Value = value ? "Hoạt động" : "Không hoạt động";
                    e.FormattingApplied = true;
                }
            }
        }

        private void LoadNhanVien()
        {
            try
            {
                allNhanVien = NhanVienBUS.Instance.GetAllNhanVien();
                CalculateTotalPages();
                LoadCurrentPageData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotalPages()
        {
            if (allNhanVien != null)
            {
                totalPages = (int)Math.Ceiling(allNhanVien.Rows.Count / (double)pageSize);
                if (totalPages == 0) totalPages = 1;
                lblPageInfo.Text = $"{currentPage} / {totalPages}";
            }
        }

        private void LoadCurrentPageData()
        {
            if (allNhanVien != null)
            {
                DataTable pageData = new DataTable();
                pageData = allNhanVien.Clone();

                int startIndex = (currentPage - 1) * pageSize;
                int endIndex = Math.Min(startIndex + pageSize - 1, allNhanVien.Rows.Count - 1);

                for (int i = startIndex; i <= endIndex && i < allNhanVien.Rows.Count; i++)
                {
                    pageData.ImportRow(allNhanVien.Rows[i]);
                }

                dgvEmployees.DataSource = pageData;
                lblPageInfo.Text = $"{currentPage} / {totalPages}";
            }
        }

        private void DgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow != null && dgvEmployees.CurrentRow.Index >= 0)
            {
                try
                {
                    DataGridViewRow row = dgvEmployees.CurrentRow;
                    txtId.Text = row.Cells["MaNV"].Value?.ToString() ?? string.Empty;
                    txtName.Text = row.Cells["HoTen"].Value?.ToString() ?? string.Empty;
                    cbGender.Text = row.Cells["GioiTinh"].Value?.ToString() ?? string.Empty;
                    if (row.Cells["NgaySinh"].Value != DBNull.Value)
                        dtpBirthday.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    else
                        dtpBirthday.Value = DateTime.Now;
                    txtAddress.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                    txtPhone.Text = row.Cells["DienThoai"].Value?.ToString() ?? string.Empty;
                    txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;
                    cbRole.Text = row.Cells["ChucVu"].Value?.ToString() ?? string.Empty;
                    txtUsername.Text = row.Cells["TenDangNhap"].Value?.ToString() ?? string.Empty;
                    txtPassword.Text = row.Cells["MatKhau"].Value?.ToString() ?? string.Empty;

                    // Hiển thị ảnh
                    if (row.Cells["HinhAnh"].Value != DBNull.Value && row.Cells["HinhAnh"].Value != null)
                    {
                        currentImage = (byte[])row.Cells["HinhAnh"].Value;
                        using (MemoryStream ms = new MemoryStream(currentImage))
                        {
                            pictureBox.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBox.Image = null;
                        currentImage = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn dòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn ảnh nhân viên";
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image originalImage = Image.FromFile(openFileDialog.FileName);
                        int maxWidth = 300;
                        int maxHeight = 300;
                        int newWidth, newHeight;
                        if (originalImage.Width > originalImage.Height)
                        {
                            newWidth = maxWidth;
                            newHeight = (int)(originalImage.Height * ((float)maxWidth / originalImage.Width));
                        }
                        else
                        {
                            newHeight = maxHeight;
                            newWidth = (int)(originalImage.Width * ((float)maxHeight / originalImage.Height));
                        }
                        Bitmap resizedImage = new Bitmap(originalImage, newWidth, newHeight);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                            encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 80L);
                            System.Drawing.Imaging.ImageCodecInfo jpegCodec = Array.Find(
                                System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders(),
                                codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                            resizedImage.Save(ms, jpegCodec, encoderParams);
                            currentImage = ms.ToArray();
                            pictureBox.Image = resizedImage;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            currentImage = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput() == false) return;

                NhanVien nhanVien = new NhanVien
                {
                    MaNV = NhanVienBUS.Instance.GenerateNewMaNV(),
                    HoTen = txtName.Text.Trim(),
                    GioiTinh = cbGender.Text,
                    NgaySinh = dtpBirthday.Value,
                    DiaChi = txtAddress.Text.Trim(),
                    DienThoai = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    ChucVu = cbRole.Text,
                    TenDangNhap = txtUsername.Text.Trim(),
                    MatKhau = txtPassword.Text.Trim(),
                    TrangThai = true,
                    HinhAnh = currentImage
                };

                if (NhanVienBUS.Instance.CheckUsernameExists(nhanVien.TenDangNhap))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (NhanVienBUS.Instance.Add(nhanVien))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadNhanVien();
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ValidateInput() == false) return;

                // Check if an image has been selected
                if (currentImage == null)
                {
                    MessageBox.Show("Bạn chưa thêm ảnh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NhanVien nhanVien = new NhanVien
                {
                    MaNV = txtId.Text.Trim(),
                    HoTen = txtName.Text.Trim(),
                    GioiTinh = cbGender.Text,
                    NgaySinh = dtpBirthday.Value,
                    DiaChi = txtAddress.Text.Trim(),
                    DienThoai = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    ChucVu = cbRole.Text,
                    TenDangNhap = txtUsername.Text.Trim(),
                    MatKhau = txtPassword.Text.Trim(),
                    TrangThai = true,
                    HinhAnh = currentImage
                };

                if (NhanVienBUS.Instance.Update(nhanVien))
                {
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    LoadNhanVien();
                }
                else
                {
                    MessageBox.Show("Cập nhật nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maNV = txtId.Text.Trim();
                string tenNV = txtName.Text.Trim();

                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{tenNV}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (NhanVienBUS.Instance.Delete(maNV))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearInputs();
                        LoadNhanVien();
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cbGender.SelectedIndex = 0;
            cbRole.SelectedIndex = 1;
            dtpBirthday.Value = DateTime.Now;
            pictureBox.Image = null;
            currentImage = null;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPhone.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text.Trim(), @"^(0)[0-9]{9}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập số điện thoại 10 số, bắt đầu bằng số 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Trim().Length < 3)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 3 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            DateTime today = DateTime.Today;
            int age = today.Year - dtpBirthday.Value.Year;
            if (dtpBirthday.Value.Date > today.AddYears(-age)) age--;

            if (age < 18)
            {
                MessageBox.Show("Nhân viên phải đủ 18 tuổi trở lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpBirthday.Focus();
                return false;
            }

            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadNhanVien();
                return;
            }

            DataTable filteredTable = new DataTable();
            filteredTable = allNhanVien.Clone();

            foreach (DataRow row in allNhanVien.Rows)
            {
                if (row["MaNV"].ToString().ToLower().Contains(searchText) ||
                    row["HoTen"].ToString().ToLower().Contains(searchText) ||
                    row["DienThoai"].ToString().ToLower().Contains(searchText) ||
                    row["Email"].ToString().ToLower().Contains(searchText) ||
                    row["TenDangNhap"].ToString().ToLower().Contains(searchText) ||
                    row["ChucVu"].ToString().ToLower().Contains(searchText))
                {
                    filteredTable.ImportRow(row);
                }
            }

            dgvEmployees.DataSource = filteredTable;
            if (filteredTable.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            LoadCurrentPageData();
            UpdateButtonState();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadCurrentPageData();
                UpdateButtonState();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadCurrentPageData();
                UpdateButtonState();
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            currentPage = totalPages;
            LoadCurrentPageData();
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            btnFirstPage.Enabled = true;
            btnPreviousPage.Enabled = true;
            btnNextPage.Enabled = true;
            btnLastPage.Enabled = true;
        }
    }
}