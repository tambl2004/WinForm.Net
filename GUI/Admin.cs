using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class Admin : Form
    {
        private Form currentChildForm;
        private readonly string tenDangNhap;
        private readonly string chucVu;

        public Admin(string tenDangNhap, string chucVu)
        {
            InitializeComponent();
            this.tenDangNhap = tenDangNhap;
            this.chucVu = chucVu;
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;

       
            RegisterMenuEvents();
            OpenChildForm(new FormQuanLy(chucVu));
        }

     

        private void RegisterMenuEvents()
        {
            MenuItemDangXuat.Click += MenuItemDangXuat_Click;
            
            // Đăng ký các sự kiện cho menu Tài khoản nếu người dùng là Quản lý hoặc Admin
            if ((chucVu == "Admin") && MenuItemTaiKhoan != null && MenuItemTaiKhoan.Visible)
            {
                MenuItemTaiKhoan.Click += MenuItemNhanVien_Click;
            }
        }

        private void MenuItemNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormNhanVien());
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.MdiParent = this;
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Show();
        }

        private void MenuItemDangXuat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCaNhan formCaNhan = new FormCaNhan(tenDangNhap);
            formCaNhan.ShowDialog();
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLy(chucVu));
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau formDoiMatKhau = new FormDoiMatKhau(tenDangNhap);
            formDoiMatKhau.ShowDialog();
        }
    }
}