using System;

namespace DTO
{
    public class ChiTietPhieuNhap
    {
        public string MaPN { get; set; }
        public string MaXe { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal ThanhTien { get; set; }

        public ChiTietPhieuNhap()
        {
            SoLuong = 1;
            ThanhTien = 0;
        }

        public ChiTietPhieuNhap(string maPN, string maXe, int soLuong, decimal giaNhap, decimal thanhTien)
        {
            MaPN = maPN;
            MaXe = maXe;
            SoLuong = soLuong;
            GiaNhap = giaNhap;
            ThanhTien = thanhTien;
        }
    }
}