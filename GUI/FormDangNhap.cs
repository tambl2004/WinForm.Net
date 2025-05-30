using System;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class FormDangNhap : Form
    {
        public static string LoggedInTenDangNhap { get; private set; }
        public static string LoggedInChucVu { get; private set; }
        public static string LoggedInMaNV { get; private set; }
        private bool isMatKhauVisible = false;

        public FormDangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                MessageBox.Show("Tài khoản không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Sử dụng NhanVienBUS để kiểm tra đăng nhập
                NhanVien nhanVien = NhanVienBUS.Instance.Login(tenDangNhap, matKhau);

                if (nhanVien != null)
                {
                    // Lấy thông tin đăng nhập từ kết quả
                    LoggedInMaNV = nhanVien.MaNV;
                    LoggedInTenDangNhap = nhanVien.TenDangNhap;
                    LoggedInChucVu = nhanVien.ChucVu;

                    MessageBox.Show($"Đăng nhập thành công! Xin chào {nhanVien.HoTen}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkDangKy_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormDangKy frm = new FormDangKy();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void picEyeMatKhau_Click(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !isMatKhauVisible;
            isMatKhauVisible = !isMatKhauVisible;
            picEyeMatKhau.Image = isMatKhauVisible ? global::GUI.Properties.Resources.eye_open : global::GUI.Properties.Resources.eye_closed;
        }
    }
}