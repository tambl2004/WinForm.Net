using System;
using System.Collections.Generic;
using System.Data;
using DAO;
using DTO;

namespace BUS
{
    public class HoaDonBUS
    {
        private static HoaDonBUS instance;

        public static HoaDonBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new HoaDonBUS();
                return instance;
            }
        }

        private HoaDonBUS() { }

        // Tạo hóa đơn mới
        public string TaoHoaDon(string maKH, string maNV, decimal giamGia = 0, string ghiChu = null)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(maNV))
                    return null;

                // Tạo mã hóa đơn mới
                string maHD = BanHangDAO.Instance.GenerateNewMaHD();

                // Tạo đối tượng hóa đơn với đầy đủ thông tin
                HoaDon hoaDon = new HoaDon
                {
                    MaHD = maHD,
                    NgayBan = DateTime.Now,
                    MaKH = maKH,
                    MaNV = maNV,
                    TongTien = 0, // Khởi tạo tổng tiền là 0, sẽ cập nhật sau khi thêm chi tiết
                    GiamGia = giamGia,
                    ThanhToan = 0, // Khởi tạo thanh toán là 0
                    GhiChu = ghiChu,
                    TrangThai = false // Chưa thanh toán
                };

                // Thêm hóa đơn vào CSDL
                bool result = BanHangDAO.Instance.ThemHoaDon(hoaDon);

                if (result)
                    return maHD;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo hóa đơn: " + ex.Message);
            }
        }

        // Thêm chi tiết hóa đơn
        public bool ThemChiTietHoaDon(string maHD, string maXe, int soLuong, decimal giaBan, decimal giamGia = 0, string ghiChu = null)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maXe) || soLuong <= 0)
                    return false;

                // Tính thành tiền
                decimal thanhTien = (soLuong * giaBan) - giamGia;

                // Tạo đối tượng chi tiết hóa đơn
                ChiTietHoaDon chiTiet = new ChiTietHoaDon
                {
                    MaHD = maHD,
                    MaXe = maXe,
                    SoLuong = soLuong,
                    GiaBan = giaBan,
                    GiamGia = giamGia,
                    ThanhTien = thanhTien,
                    GhiChu = ghiChu
                };

                // Thêm chi tiết hóa đơn vào CSDL
                return BanHangDAO.Instance.ThemChiTietHoaDon(chiTiet);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết hóa đơn: " + ex.Message);
            }
        }

        // Lấy hóa đơn theo mã
        public HoaDon GetHoaDonByMa(string maHD)
        {
            try
            {
                return BanHangDAO.Instance.GetHoaDonByMa(maHD);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin hóa đơn: " + ex.Message);
            }
        }

        // Lấy danh sách hóa đơn
        public DataTable GetDanhSachHoaDon(int page = 1, int pageSize = 20)
        {
            try
            {
                return BanHangDAO.Instance.GetAllHoaDon(page, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách hóa đơn: " + ex.Message);
            }
        }

        // Lấy tổng số hóa đơn
        public int GetTotalCount()
        {
            try
            {
                return BanHangDAO.Instance.GetTotalCount();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy tổng số hóa đơn: " + ex.Message);
            }
        }

        // Lấy chi tiết hóa đơn
        public DataTable GetChiTietHoaDon(string maHD)
        {
            try
            {
                return BanHangDAO.Instance.GetChiTietHoaDon(maHD);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết hóa đơn: " + ex.Message);
            }
        }

        // Cập nhật hóa đơn
        public bool CapNhatHoaDon(string maHD, string maKH, string maNV, DateTime ngayBan, decimal giamGia = 0, string ghiChu = null, bool trangThai = true)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(maHD) || string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(maNV))
                    return false;

                // Lấy thông tin hóa đơn hiện tại
                HoaDon hoaDon = BanHangDAO.Instance.GetHoaDonByMa(maHD);

                if (hoaDon == null)
                    return false;

                // Cập nhật thông tin
                hoaDon.MaKH = maKH;
                hoaDon.MaNV = maNV;
                hoaDon.NgayBan = ngayBan;
                hoaDon.GiamGia = giamGia;
                hoaDon.GhiChu = ghiChu;
                hoaDon.TrangThai = trangThai;

                // Cập nhật vào CSDL
                return BanHangDAO.Instance.UpdateHoaDon(hoaDon);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật hóa đơn: " + ex.Message);
            }
        }


    

        // Tìm kiếm hóa đơn
        public DataTable TimKiemHoaDon(string keyword, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                return BanHangDAO.Instance.SearchHoaDon(keyword, fromDate, toDate);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm hóa đơn: " + ex.Message);
            }
        }
    }
}