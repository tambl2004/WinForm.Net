using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace GUI
{
    public partial class FormQuanLy : Form
    {
        private Form currentChildForm; // Form con hiện tại
        private ContextMenuStrip currentContextMenuStrip;
        private string currentUserRole = FormDangNhap.LoggedInChucVu;

        public FormQuanLy(string userRole)
        {
            InitializeComponent();
            RegisterButtonEvents(); // Đăng ký sự kiện cho các nút
        
            CheckUserRole();
        }
        // phân quyền
        private void CheckUserRole()
        {
            bool isAdmin = currentUserRole == "Admin";
            bool isBanHang = currentUserRole == "BanHang";
            bool isNhapHang = currentUserRole == "NhapHang";
            bool isKho = currentUserRole == "Kho";
            bool isKeToan = currentUserRole == "KeToan";
            // Cấu hình hiển thị các nút dựa trên vai trò người dùng   
            btn_TongQuan.Enabled = isAdmin || isBanHang || isNhapHang || isKeToan;
            btn_ThongKe.Enabled = isAdmin || isBanHang || isNhapHang || isKeToan;
            btn_SanPham.Enabled = isAdmin || isNhapHang || isBanHang || isKeToan;
            btn_KhachHang.Enabled = isAdmin || isBanHang || isNhapHang || isKeToan;
            btn_NhanVien.Enabled = isAdmin;
            btn_NhaCungCap.Enabled = isAdmin || isBanHang || isNhapHang || isKeToan;
            btn_HoaDon.Enabled = isAdmin || isKeToan || isBanHang;
            btn_QuanLyPhieuNhap.Enabled = isAdmin || isNhapHang || isKeToan;
            btn_NhapHang.Enabled = isAdmin ||  isNhapHang;
            btn_BanHang.Enabled = isAdmin || isBanHang; // Chỉ cho phép admin và nhân viên bán hàng truy cập


        }
      

        private void FormQuanLy_Load(object sender, EventArgs e)
        {
            // Mở FormSanPham mặc định khi form tải
            OpenChildForm(new FormTrangChu());
        }

        /// <summary>
        /// Đăng ký sự kiện click cho các nút
        /// </summary>
        private void RegisterButtonEvents()
        {
            btn_TongQuan.Click += Btn_TongQuan_Click;
            btn_SanPham.Click += Btn_SanPham_Click;
            btn_KhachHang.Click += Btn_KhachHang_Click;
            btn_NhanVien.Click += Btn_NhanVien_Click;
            btn_NhaCungCap.Click += Btn_NhaCungCap_Click;
            btn_BanHang.Click += Btn_BanHang_Click;
            btn_HoaDon.Click += Btn_HoaDon_Click;
            btn_NhapHang.Click += Btn_NhapHang_Click;
            btn_QuanLyPhieuNhap.Click += Btn_QuanLyPhieuNhap_Click;
            btn_ThongKe.Click += Btn_ThongKe_Click;
            
        }

        /// <summary>
        /// Mở form con trong panelForm, đóng form hiện tại nếu có
        /// </summary>
        /// <param name="childForm">Form con cần mở</param>
            private void OpenChildForm(Form childForm)
            {
                // Đóng form con hiện tại nếu có
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                }
                panelForm.Controls.Clear();

                // Thiết lập form con
                currentChildForm = childForm;
                childForm.TopLevel = false; // Không phải form cấp cao
                childForm.FormBorderStyle = FormBorderStyle.None; // Xóa viền
                childForm.Dock = DockStyle.Fill; // Điền đầy panel
                panelForm.Controls.Add(childForm); // Thêm form con vào panel
                childForm.Show();
            }

        // Sự kiện click cho các nút
        private void Btn_TongQuan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTrangChu());
        }
        private void Btn_SanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormSanPham());
        }

        private void Btn_KhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormKhachHang());
        }

        private void Btn_NhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhanVien()); // Pass the userRole to FormNhanVien
        }

        private void Btn_NhaCungCap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhaCungCap());
        }

        private void Btn_ThongKe_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormBaoCao());
        }


        private void Btn_NhapHang_Click(object sender, EventArgs e)
        {
            // Check if the clicked button has a submenu open
            if (currentContextMenuStrip != null && currentContextMenuStrip.Visible)
            {
                currentContextMenuStrip.Close();
                return;
            }

            // Get the currently logged in employee
            if (string.IsNullOrEmpty(FormDangNhap.LoggedInMaNV))
            {
                MessageBox.Show("Vui lòng đăng nhập lại để tiếp tục!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NhanVien currentNhanVien = new NhanVien
            {
                MaNV = FormDangNhap.LoggedInMaNV,
                TenDangNhap = FormDangNhap.LoggedInTenDangNhap,
                ChucVu = FormDangNhap.LoggedInChucVu
            };

            // Directly open FormNhapHang
            OpenChildForm(new FormNhapHang(currentNhanVien));
        }

        private void Btn_BanHang_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin đăng nhập
            if (string.IsNullOrEmpty(FormDangNhap.LoggedInMaNV))
            {
                MessageBox.Show("Vui lòng đăng nhập lại để tiếp tục!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Truyền mã nhân viên hiện tại cho FormBanHang
            OpenChildForm(new FormBanHang(FormDangNhap.LoggedInMaNV));
        }
       

        private void Btn_HoaDon_Click(object sender, EventArgs e)
        {
            // Mở form hóa đơn
            OpenChildForm(new HoaDon());
        }

        private void Btn_QuanLyPhieuNhap_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyPhieuNhap());
        }
    }

}