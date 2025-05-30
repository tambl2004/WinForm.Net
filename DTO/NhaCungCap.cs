using System;

namespace DTO
{
    public class NhaCungCap
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string NguoiLienHe { get; set; }
        public bool TrangThai { get; set; }

        public NhaCungCap()
        {
            TrangThai = true;
        }

        public NhaCungCap(string maNCC, string tenNCC, string diaChi, string dienThoai,
                          string email, string nguoiLienHe, bool trangThai = true)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            Email = email;
            NguoiLienHe = nguoiLienHe;
            TrangThai = trangThai;
        }
    }
}