using System;

namespace DTO
{
    public class HoaDon
    {
        public string MaHD { get; set; }
        public DateTime NgayBan { get; set; }
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public decimal TongTien { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhToan { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }

        public HoaDon()
        {
            NgayBan = DateTime.Now;
            TongTien = 0;
            GiamGia = 0;
            ThanhToan = 0;
            TrangThai = true;
        }

        public HoaDon(string maHD, DateTime ngayBan, string maKH, string maNV,
                      decimal tongTien, decimal giamGia, decimal thanhToan, string ghiChu, bool trangThai = true)
        {
            MaHD = maHD;
            NgayBan = ngayBan;
            MaKH = maKH;
            MaNV = maNV;
            TongTien = tongTien;
            GiamGia = giamGia;
            ThanhToan = thanhToan;
            GhiChu = ghiChu;
            TrangThai = trangThai;
        }
    }
}