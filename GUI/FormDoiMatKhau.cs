using System;
using System.Windows.Forms;
using DTO;
using BUS;

namespace GUI
{
    public partial class FormDoiMatKhau : Form
    {
        private readonly string loggedInUsername;
        private NhanVien currentUser;

        public FormDoiMatKhau(string username)
        {
            InitializeComponent();
            loggedInUsername = username;
            InitializeForm();
            LoadUserData();
        }

        private void InitializeForm()
        {
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void LoadUserData()
        {
            currentUser = NhanVienBUS.Instance.GetByUsername(loggedInUsername);
            if (currentUser == null)
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                if (txtCurrentPassword.Text != currentUser.MatKhau)
                {
                    MessageBox.Show("Mật khẩu hiện tại không đúng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                currentUser.MatKhau = txtNewPassword.Text;
                bool updateSuccess = NhanVienBUS.Instance.Update(currentUser);
                if (updateSuccess)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmNewPassword.Clear();
        }

        private bool ValidateInputs()
        {
            txtCurrentPassword.Text = txtCurrentPassword.Text.Trim();
            txtNewPassword.Text = txtNewPassword.Text.Trim();
            txtConfirmNewPassword.Text = txtConfirmNewPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmNewPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới và xác nhận!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtNewPassword.Text != txtConfirmNewPassword.Text)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận không khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtNewPassword.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}