using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class BanHangDAO
    {
        private static BanHangDAO instance;

        public static BanHangDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new BanHangDAO();
                return instance;
            }
        }

        private BanHangDAO() { }

        // Tạo mã hóa đơn mới
        public string GenerateNewMaHD()
        {
            string query = "SELECT TOP 1 MaHD FROM HoaDon ORDER BY MaHD DESC";
            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                string lastMaHD = data.Rows[0]["MaHD"].ToString();
                if (lastMaHD.StartsWith("HD"))
                {
                    int number = Convert.ToInt32(lastMaHD.Substring(2)) + 1;
                    return "HD" + number.ToString("D3");
                }
            }
            return "HD001";
        }

        // Thêm hóa đơn mới
        public bool ThemHoaDon(HoaDon hoaDon)
        {
            string query = "sp_ThemHoaDon";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHD", hoaDon.MaHD),
                new SqlParameter("@NgayBan", hoaDon.NgayBan),
                new SqlParameter("@MaKH", hoaDon.MaKH),
                new SqlParameter("@MaNV", hoaDon.MaNV),
                new SqlParameter("@TongTien", hoaDon.TongTien),
                new SqlParameter("@GiamGia", hoaDon.GiamGia),
                new SqlParameter("@ThanhToan", hoaDon.ThanhToan),
                new SqlParameter("@GhiChu", hoaDon.GhiChu ?? (object)DBNull.Value),
                new SqlParameter("@TrangThai", hoaDon.TrangThai)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters) > 0;
        }

        // Thêm chi tiết hóa đơn
        public bool ThemChiTietHoaDon(ChiTietHoaDon chiTiet)
        {
            string query = "sp_ThemChiTietHoaDon";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHD", chiTiet.MaHD),
                new SqlParameter("@MaXe", chiTiet.MaXe),
                new SqlParameter("@SoLuong", chiTiet.SoLuong),
                new SqlParameter("@GiaBan", chiTiet.GiaBan),
                new SqlParameter("@GiamGia", chiTiet.GiamGia),
                new SqlParameter("@ThanhTien", chiTiet.ThanhTien),
                new SqlParameter("@GhiChu", chiTiet.GhiChu ?? (object)DBNull.Value)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters) > 0;
        }

        // Lấy hóa đơn theo mã
        public HoaDon GetHoaDonByMa(string maHD)
        {
            string query = "SELECT * FROM HoaDon WHERE MaHD = @MaHD";
            SqlParameter parameter = new SqlParameter("@MaHD", maHD);
            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query, parameter);

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new HoaDon(
                    row["MaHD"].ToString(),
                    Convert.ToDateTime(row["NgayBan"]),
                    row["MaKH"].ToString(),
                    row["MaNV"].ToString(),
                    Convert.ToDecimal(row["TongTien"]),
                    Convert.ToDecimal(row["GiamGia"]),
                    Convert.ToDecimal(row["ThanhToan"]),
                    row["GhiChu"].ToString(),
                    Convert.ToBoolean(row["TrangThai"])
                );
            }
            return null;
        }

        // Lấy danh sách hóa đơn (phân trang)
        public DataTable GetAllHoaDon(int page = 1, int pageSize = 20)
        {
            string query = @"SELECT hd.MaHD, hd.NgayBan, kh.HoTen AS KhachHang, nv.HoTen AS NhanVien, 
                    hd.TongTien, hd.GiamGia, hd.ThanhToan, 
                    CASE WHEN hd.TrangThai = 1 THEN N'Đã Thanh Toán' ELSE N'Chưa Thanh Toán' END AS TrangThai
                    FROM HoaDon hd
                    JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                    JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                    ORDER BY hd.NgayBan DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Offset", (page - 1) * pageSize),
        new SqlParameter("@PageSize", pageSize)
            };

            return DatabaseAcces.Instance.ExecuteQuery(query, parameters);
        }

        // Đếm tổng số hóa đơn
        public int GetTotalCount()
        {
            string query = "SELECT COUNT(*) FROM HoaDon";
            return Convert.ToInt32(DatabaseAcces.Instance.ExecuteScalar(query));
        }

        // Lấy chi tiết hóa đơn theo mã hóa đơn
        public DataTable GetChiTietHoaDon(string maHD)
        {
            string query = @"SELECT ct.MaXe, x.TenXe, x.HangXe, x.MauSac, 
                           ct.SoLuong, ct.GiaBan, ct.GiamGia, ct.ThanhTien, ct.GhiChu
                           FROM ChiTietHoaDon ct
                           JOIN XeMay x ON ct.MaXe = x.MaXe
                           WHERE ct.MaHD = @MaHD";

            SqlParameter parameter = new SqlParameter("@MaHD", maHD);
            return DatabaseAcces.Instance.ExecuteQuery(query, parameter);
        }

        // Cập nhật hóa đơn
        public bool UpdateHoaDon(HoaDon hoaDon)
        {
            string query = @"UPDATE HoaDon 
                           SET NgayBan = @NgayBan, 
                               MaKH = @MaKH, 
                               MaNV = @MaNV, 
                               GiamGia = @GiamGia, 
                               GhiChu = @GhiChu, 
                               TrangThai = @TrangThai
                           WHERE MaHD = @MaHD";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaHD", hoaDon.MaHD),
                new SqlParameter("@NgayBan", hoaDon.NgayBan),
                new SqlParameter("@MaKH", hoaDon.MaKH),
                new SqlParameter("@MaNV", hoaDon.MaNV),
                new SqlParameter("@GiamGia", hoaDon.GiamGia),
                new SqlParameter("@GhiChu", hoaDon.GhiChu ?? (object)DBNull.Value),
                new SqlParameter("@TrangThai", hoaDon.TrangThai)
            };

            return DatabaseAcces.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

       

        // Tìm kiếm hóa đơn
        public DataTable SearchHoaDon(string keyword, DateTime? fromDate = null, DateTime? toDate = null)
        {
            string query = @"SELECT hd.MaHD, hd.NgayBan, kh.HoTen AS KhachHang, nv.HoTen AS NhanVien, 
                    hd.TongTien, hd.GiamGia, hd.ThanhToan, 
                    CASE WHEN hd.TrangThai = 1 THEN N'Đã Thanh Toán' ELSE N'Chưa Thanh Toán' END AS TrangThai
                    FROM HoaDon hd
                    JOIN KhachHang kh ON hd.MaKH = kh.MaKH
                    JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                    WHERE (hd.MaHD LIKE @Keyword OR kh.HoTen LIKE @Keyword OR nv.HoTen LIKE @Keyword)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            if (fromDate.HasValue)
            {
                query += " AND hd.NgayBan >= @FromDate";
                parameters.Add(new SqlParameter("@FromDate", fromDate.Value));
            }

            if (toDate.HasValue)
            {
                query += " AND hd.NgayBan <= @ToDate";
                parameters.Add(new SqlParameter("@ToDate", toDate.Value));
            }

            query += " ORDER BY hd.NgayBan DESC";
            return DatabaseAcces.Instance.ExecuteQuery(query, parameters.ToArray());
        }
    }
}