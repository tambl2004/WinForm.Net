using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Linq;

namespace GUI
{
    public partial class FormCaNhan : Form
    {
        private readonly string loggedInUsername;
        private string selectedImagePath;
        private NhanVien currentUser;

        public FormCaNhan(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            selectedImagePath = string.Empty;
            InitializeForm();
            LoadUserData();
        }

        private void InitializeForm()
        {
            btnUploadImage.Click += BtnUploadImage_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnExit.Click += BtnExit_Click;
          
            txtUsername.ReadOnly = true;
        }

        private void LoadUserData()
        {
            try
            {
                currentUser = NhanVienBUS.Instance.GetByUsername(loggedInUsername);
                if (currentUser == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                txtFullName.Text = currentUser.HoTen;
                txtEmail.Text = currentUser.Email ?? string.Empty;
                txtUsername.Text = currentUser.TenDangNhap;
                txtPhone.Text = currentUser.DienThoai ?? string.Empty;

                if (currentUser.HinhAnh != null)
                {
                    using (MemoryStream ms = new MemoryStream(currentUser.HinhAnh))
                    {
                        pictureBoxAvatar.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        selectedImagePath = openFileDialog.FileName;
                        Image originalImage = Image.FromFile(selectedImagePath);
                        Image resizedImage = ResizeImage(originalImage, pictureBoxAvatar.Width, pictureBoxAvatar.Height);
                        pictureBoxAvatar.Image = resizedImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        selectedImagePath = string.Empty;
                        pictureBoxAvatar.Image = null;
                    }
                }
            }
        }

        private Image ResizeImage(Image originalImage, int targetWidth, int targetHeight)
        {
            float ratioX = (float)targetWidth / originalImage.Width;
            float ratioY = (float)targetHeight / originalImage.Height;
            float ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalImage.Width * ratio);
            int newHeight = (int)(originalImage.Height * ratio);

            Bitmap resizedImage = new Bitmap(targetWidth, targetHeight);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.Clear(Color.Transparent);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                int offsetX = (targetWidth - newWidth) / 2;
                int offsetY = (targetHeight - newHeight) / 2;
                graphics.DrawImage(originalImage, offsetX, offsetY, newWidth, newHeight);
            }

            return resizedImage;
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                currentUser.HoTen = txtFullName.Text.Trim();
                currentUser.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                currentUser.DienThoai = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();

                if (!string.IsNullOrEmpty(selectedImagePath) && pictureBoxAvatar.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pictureBoxAvatar.Image.Save(ms, ImageFormat.Jpeg);
                        currentUser.HinhAnh = ms.ToArray();
                    }
                }

                bool updateSuccess = NhanVienBUS.Instance.Update(currentUser);
                if (updateSuccess)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            txtFullName.Text = txtFullName.Text.Trim();
            txtEmail.Text = txtEmail.Text.Trim();
            txtPhone.Text = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Họ và tên không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !IsValidPhone(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập 8-15 chữ số.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsValidPhone(string phone)
        {
            return phone.Length >= 8 && phone.Length <= 15 && phone.All(char.IsDigit);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}