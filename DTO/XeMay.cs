namespace DTO
{
    public class XeMay
    {
        public string MaXe { get; set; }
        public string TenXe { get; set; }
        public string HangXe { get; set; }
        public string DongXe { get; set; }
        public string MauSac { get; set; }
        public int NamSX { get; set; }
        public string SoKhung { get; set; }
        public string SoMay { get; set; }
        public decimal GiaNhap { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
        public byte[] HinhAnh { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }

        public XeMay()
        {
            SoLuongTon = 0;
            TrangThai = true;
        }

        public XeMay(string maXe, string tenXe, string hangXe, string dongXe, 
                    string mauSac, int namSX, string soKhung, string soMay, 
                    decimal giaNhap, decimal giaBan, int soLuongTon,
                    byte[] hinhAnh, string moTa, bool trangThai = true)
        {
            MaXe = maXe;
            TenXe = tenXe;
            HangXe = hangXe;
            DongXe = dongXe;
            MauSac = mauSac;
            NamSX = namSX;
            SoKhung = soKhung;
            SoMay = soMay;
            GiaNhap = giaNhap;
            GiaBan = giaBan;
            SoLuongTon = soLuongTon;
            HinhAnh = hinhAnh;
            MoTa = moTa;
            TrangThai = trangThai;
        }
    }
}