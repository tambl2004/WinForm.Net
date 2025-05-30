using System;
using System.Data;
using DAO;

namespace BUS
{
    public class BaoCaoBUS
    {
        private static BaoCaoBUS instance;

        public static BaoCaoBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaoCaoBUS();
                return instance;
            }
        }

        private BaoCaoBUS() { }

        // Báo cáo doanh thu theo khoảng thời gian
        public DataTable BaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                return BaoCaoDAO.Instance.BaoCaoDoanhThu(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy báo cáo doanh thu: " + ex.Message);
            }
        }

        // Báo cáo tồn kho
        public DataTable BaoCaoTonKho()
        {
            try
            {
                return BaoCaoDAO.Instance.BaoCaoTonKho();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy báo cáo tồn kho: " + ex.Message);
            }
        }

        // Báo cáo xe bán chạy
        public DataTable BaoCaoXeBanChay(DateTime tuNgay, DateTime denNgay, int top = 10)
        {
            try
            {
                return BaoCaoDAO.Instance.BaoCaoXeBanChay(tuNgay, denNgay, top);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy báo cáo xe bán chạy: " + ex.Message);
            }
        }

        // Thống kê lợi nhuận theo tháng
        public DataTable ThongKeLuongNhuanTheoThang(int nam)
        {
            try
            {
                return BaoCaoDAO.Instance.ThongKeLuongNhuanTheoThang(nam);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thống kê lợi nhuận: " + ex.Message);
            }
        }

        // Báo cáo doanh thu theo tháng trong năm
        public DataTable BaoCaoDoanhThuTheoThang(int nam)
        {
            try
            {
                return BaoCaoDAO.Instance.BaoCaoDoanhThuTheoThang(nam);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy báo cáo doanh thu theo tháng: " + ex.Message);
            }
        }
        // Báo cáo nhập hàng
        public DataTable BaoCaoNhapHang(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                return BaoCaoDAO.Instance.BaoCaoNhapHang(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy báo cáo nhập hàng: " + ex.Message);
            }
        }

        // Báo cáo xuất hàng
        public DataTable BaoCaoXuatHang(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                return BaoCaoDAO.Instance.BaoCaoXuatHang(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy báo cáo xuất hàng: " + ex.Message);
            }
        }
    }
}