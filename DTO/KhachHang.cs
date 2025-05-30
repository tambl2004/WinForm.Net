using System;

namespace DTO
{
    public class KhachHang
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string CMND { get; set; }
        public DateTime NgayDangKy { get; set; }
        public bool TrangThai { get; set; }

        public KhachHang()
        {
            NgayDangKy = DateTime.Now;
            TrangThai = true;
        }

        public KhachHang(string maKH, string hoTen, string gioiTinh, DateTime? ngaySinh,
                         string diaChi, string dienThoai, string email, string cmnd,
                         DateTime ngayDangKy, bool trangThai = true)
        {
            MaKH = maKH;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            Email = email;
            CMND = cmnd;
            NgayDangKy = ngayDangKy;
            TrangThai = trangThai;
        }
    }
}