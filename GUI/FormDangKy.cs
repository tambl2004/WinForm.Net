using System;
using System.Windows.Forms;
using DTO;
using BUS;

namespace GUI
{
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra mật khẩu khớp
            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo đối tượng NhanVien
            NhanVien nhanVien = new NhanVien
            {
                HoTen = txtHoTen.Text.Trim(),
                TenDangNhap = txtTaiKhoan.Text.Trim(),
                MatKhau = txtMatKhau.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ChucVu = "Nhân viên bán hàng" // Mặc định là nhân viên bán hàng cho người đăng ký mới
            };

            // Gọi phương thức đăng ký từ BUS
            string result = NhanVienBUS.Instance.Register(nhanVien);

            switch (result)
            {
                case "Tên đăng nhập không được để trống":
                    MessageBox.Show("Tên đăng nhập không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTaiKhoan.Focus();
                    return;
                case "Mật khẩu không được để trống":
                    MessageBox.Show("Mật khẩu không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return;
                case "Họ tên không được để trống":
                    MessageBox.Show("Họ tên không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.Focus();
                    return;
                case "Tên đăng nhập đã tồn tại":
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTaiKhoan.Focus();
                    return;
                case "success":
                    MessageBox.Show("Đăng ký tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FormDangNhap frm = new FormDangNhap();
                    frm.ShowDialog();
                    this.Close();
                    return;
                default:
                    MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
        }

        private void linkDangNhap_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close(); // Đóng form đăng ký, quay lại form đăng nhập cũ (đã bị Hide)
        }
    }
}