using System;

namespace DTO
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public bool TrangThai { get; set; }
        public byte[] HinhAnh { get; set; }

        public NhanVien()
        {
            TrangThai = true;
        }

        public NhanVien(string maNV, string hoTen, string gioiTinh, DateTime? ngaySinh,
                        string diaChi, string dienThoai, string email, string chucVu,
                        string tenDangNhap, string matKhau, bool trangThai = true, byte[] hinhAnh = null)
        {
            MaNV = maNV;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            Email = email;
            ChucVu = chucVu;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            TrangThai = trangThai;
            HinhAnh = hinhAnh;
        }
    }
}