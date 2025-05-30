using System;

namespace DTO
{
    public class ChiTietHoaDon
    {
        public string MaHD { get; set; }
        public string MaXe { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
        public decimal GiamGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string GhiChu { get; set; }

        public ChiTietHoaDon()
        {
            SoLuong = 1;
            GiamGia = 0;
        }

        public ChiTietHoaDon(string maHD, string maXe, int soLuong, decimal giaBan,
                            decimal giamGia, decimal thanhTien, string ghiChu)
        {
            MaHD = maHD;
            MaXe = maXe;
            SoLuong = soLuong;
            GiaBan = giaBan;
            GiamGia = giamGia;
            ThanhTien = thanhTien;
            GhiChu = ghiChu;
        }
    }
}