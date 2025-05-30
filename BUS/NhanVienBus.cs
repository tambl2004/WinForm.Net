using System;
using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class NhanVienBUS
    {
        private static NhanVienBUS instance;

        public static NhanVienBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhanVienBUS();
                return instance;
            }
        }

        private NhanVienBUS() { }

        public NhanVien Login(string tenDangNhap, string matKhau)
        {
            // Validate input
            if (string.IsNullOrEmpty(tenDangNhap))
                return null;

            if (string.IsNullOrEmpty(matKhau))
                return null;

            return NhanVienDAO.Instance.Login(tenDangNhap, matKhau);
        }

        public string Register(NhanVien nhanVien)
        {
            // Validate input
            if (string.IsNullOrEmpty(nhanVien.TenDangNhap))
                return "Tên đăng nhập không được để trống";

            if (string.IsNullOrEmpty(nhanVien.MatKhau))
                return "Mật khẩu không được để trống";

            if (string.IsNullOrEmpty(nhanVien.HoTen))
                return "Họ tên không được để trống";

            // Check if username exists
            if (NhanVienDAO.Instance.CheckUsernameExists(nhanVien.TenDangNhap))
                return "Tên đăng nhập đã tồn tại";

            // Generate new MaNV
            nhanVien.MaNV = NhanVienDAO.Instance.GenerateNewMaNV();

            // Set default values for required fields
            if (string.IsNullOrEmpty(nhanVien.GioiTinh))
                nhanVien.GioiTinh = "Nam"; // Default gender

            if (string.IsNullOrEmpty(nhanVien.ChucVu))
                nhanVien.ChucVu = "BanHang"; // Default role

            // Add new employee
            bool result = NhanVienDAO.Instance.Add(nhanVien);

            if (result)
                return "success";
            else
                return "Đăng ký không thành công";
        }

        public DataTable GetAllNhanVien()
        {
            return NhanVienDAO.Instance.GetAllNhanVien();
        }

        public NhanVien GetById(string maNV)
        {
            return NhanVienDAO.Instance.GetById(maNV);
        }

        public NhanVien GetByUsername(string tenDangNhap)
        {
            return NhanVienDAO.Instance.GetByUsername(tenDangNhap);
        }

        public bool Update(NhanVien nhanVien)
        {
            return NhanVienDAO.Instance.Update(nhanVien);
        }

        public bool Delete(string maNV)
        {
            return NhanVienDAO.Instance.Delete(maNV);
        }

        public bool CheckUsernameExists(string tenDangNhap)
        {
            return NhanVienDAO.Instance.CheckUsernameExists(tenDangNhap);
        }

        public string GenerateNewMaNV()
        {
            return NhanVienDAO.Instance.GenerateNewMaNV();
        }

        public bool Add(NhanVien nhanVien)
        {
            // Thêm kiểm tra dữ liệu nếu cần
            return NhanVienDAO.Instance.Add(nhanVien);
        }
    }
}