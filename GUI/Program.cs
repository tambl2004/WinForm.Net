using System;
using System.Windows.Forms;

namespace GUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            while (true)
            {
                FormDangNhap loginForm = new FormDangNhap();
                if (loginForm.ShowDialog() == DialogResult.OK) // Đăng nhập thành công
                {
                    string tenDangNhap = FormDangNhap.LoggedInTenDangNhap;
                    string chucVu = FormDangNhap.LoggedInChucVu; // Lấy chức vụ từ FormDangNhap
                    Admin adminForm = new Admin(tenDangNhap, chucVu);
                    adminForm.ShowDialog(); // Hiển thị Admin như dialog để kiểm soát vòng lặp
                    if (adminForm.DialogResult != DialogResult.Cancel)
                    {
                        break; // Thoát nếu đóng Admin mà không đăng xuất
                    }
                    // Nếu DialogResult là Cancel (đăng xuất), vòng lặp tiếp tục quay lại đăng nhập
                }
                else
                {
                    break; // Thoát nếu đóng form đăng nhập mà không đăng nhập
                }
            }
        }
    }
}