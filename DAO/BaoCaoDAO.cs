using System;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class BaoCaoDAO
    {
        private static BaoCaoDAO instance;

        public static BaoCaoDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaoCaoDAO();
                return instance;
            }
        }

        private BaoCaoDAO() { }

        // Báo cáo doanh thu theo khoảng thời gian
        public DataTable BaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string query = "sp_BaoCaoDoanhThu";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedureWithReturn(query, parameters);
        }

        // Báo cáo tồn kho
        public DataTable BaoCaoTonKho()
        {
            string query = "sp_BaoCaoTonKho";
            return DatabaseAcces.Instance.ExecuteStoredProcedureWithReturn(query);
        }

        // Báo cáo xe bán chạy
        public DataTable BaoCaoXeBanChay(DateTime tuNgay, DateTime denNgay, int top = 10)
        {
            string query = "sp_BaoCaoXeBanChay";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay),
                new SqlParameter("@Top", top)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedureWithReturn(query, parameters);
        }

        // Thống kê lợi nhuận theo tháng
        public DataTable ThongKeLuongNhuanTheoThang(int nam)
        {
            string query = "sp_ThongKeLuongNhuanTheoThang";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Nam", nam)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedureWithReturn(query, parameters);
        }

        // Báo cáo doanh thu theo tháng trong năm
        public DataTable BaoCaoDoanhThuTheoThang(int nam)
        {
            string query = @"SELECT 
                           MONTH(hd.NgayBan) AS Thang,
                           COUNT(DISTINCT hd.MaHD) AS SoHoaDon,
                           SUM(hd.TongTien) AS TongDoanhThu,
                           SUM(hd.GiamGia) AS TongGiamGia,
                           SUM(hd.ThanhToan) AS DoanhThuThuc
                        FROM HoaDon hd
                        WHERE YEAR(hd.NgayBan) = @Nam AND hd.TrangThai = 1
                        GROUP BY MONTH(hd.NgayBan)
                        ORDER BY MONTH(hd.NgayBan)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Nam", nam)
            };

            return DatabaseAcces.Instance.ExecuteQuery(query, parameters);
        }
        // Báo cáo nhập hàng
        public DataTable BaoCaoNhapHang(DateTime tuNgay, DateTime denNgay)
        {
            string query = "sp_BaoCaoNhapHang";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedureWithReturn(query, parameters);
        }

        // Báo cáo xuất hàng
        public DataTable BaoCaoXuatHang(DateTime tuNgay, DateTime denNgay)
        {
            string query = "sp_BaoCaoXuatHang";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedureWithReturn(query, parameters);
        }
    }
}