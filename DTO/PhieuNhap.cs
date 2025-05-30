using System;

namespace DTO
{
    public class PhieuNhap
    {
        public string MaPN { get; set; }
        public DateTime NgayNhap { get; set; }
        public string MaNCC { get; set; }
        public string MaNV { get; set; }
        public decimal TongTien { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }

        public PhieuNhap()
        {
            NgayNhap = DateTime.Now;
            TongTien = 0;
            TrangThai = true;
        }

        public PhieuNhap(string maPN, DateTime ngayNhap, string maNCC, string maNV,
                        decimal tongTien, string ghiChu, bool trangThai = true)
        {
            MaPN = maPN;
            NgayNhap = ngayNhap;
            MaNCC = maNCC;
            MaNV = maNV;
            TongTien = tongTien;
            GhiChu = ghiChu;
            TrangThai = trangThai;
        }
    }
}