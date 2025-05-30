using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class NhapHangDAO
    {
        private static NhapHangDAO instance;

        public static NhapHangDAO Instance
        {
            get
            {
                if (

instance == null)
                    instance = new NhapHangDAO();
                return instance;
            }
        }

        private NhapHangDAO() { }

        // Generate new PhieuNhap ID
        public string GenerateNewMaPN()
        {
            string query = "SELECT TOP 1 MaPN FROM PhieuNhap ORDER BY MaPN DESC";
            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                string lastPN = data.Rows[0]["MaPN"].ToString();
                int number = int.Parse(lastPN.Substring(2)) + 1;
                return "PN" + number.ToString("D3");
            }
            return "PN001";
        }

        // Create new PhieuNhap
        public bool CreatePhieuNhap(PhieuNhap phieuNhap)
        {
            string query = "sp_ThemPhieuNhap";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPN", phieuNhap.MaPN),
                new SqlParameter("@NgayNhap", phieuNhap.NgayNhap),
                new SqlParameter("@MaNCC", phieuNhap.MaNCC),
                new SqlParameter("@MaNV", phieuNhap.MaNV),
                new SqlParameter("@GhiChu", phieuNhap.GhiChu ?? (object)DBNull.Value)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters) > 0;
        }

        // Add ChiTietPhieuNhap
        public bool AddChiTietPhieuNhap(ChiTietPhieuNhap chiTiet)
        {
            string query = "sp_ThemChiTietPhieuNhap";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPN", chiTiet.MaPN),
                new SqlParameter("@MaXe", chiTiet.MaXe),
                new SqlParameter("@SoLuong", chiTiet.SoLuong),
                new SqlParameter("@GiaNhap", chiTiet.GiaNhap)
            };

            return DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters) > 0;
        }

        // Get PhieuNhap by ID
        public PhieuNhap GetPhieuNhap(string maPN)
        {
            string query = "SELECT * FROM PhieuNhap WHERE MaPN = @MaPN";
            SqlParameter parameter = new SqlParameter("@MaPN", maPN);
            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query, parameter);

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new PhieuNhap(
                    row["MaPN"].ToString(),
                    Convert.ToDateTime(row["NgayNhap"]),
                    row["MaNCC"].ToString(),
                    row["MaNV"].ToString(),
                    row["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTien"]),
                    row["GhiChu"].ToString(),
                    Convert.ToBoolean(row["TrangThai"])
                );
            }
            return null;
        }

        // Get all PhieuNhap with pagination
        public DataTable GetAllPhieuNhap(int page = 1, int pageSize = 12)
        {
            string query = @"SELECT pn.MaPN, pn.NgayNhap, ncc.TenNCC, nv.HoTen AS NhanVien, 
                    pn.TongTien, ISNULL(pn.GhiChu, '') AS GhiChu, pn.TrangThai
                    FROM PhieuNhap pn
                    JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
                    JOIN NhanVien nv ON pn.MaNV = nv.MaNV
                    ORDER BY pn.NgayNhap DESC
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Offset", (page - 1) * pageSize),
                new SqlParameter("@PageSize", pageSize)
            };

            return DatabaseAcces.Instance.ExecuteQuery(query, parameters);
        }

        // Get total PhieuNhap count
        public int GetTotalCount()
        {
            string query = "SELECT COUNT(*) FROM PhieuNhap";
            return Convert.ToInt32(DatabaseAcces.Instance.ExecuteScalar(query));
        }

        // Get ChiTietPhieuNhap by MaPN
        public DataTable GetChiTietPhieuNhap(string maPN)
        {
            string query = @"SELECT ct.MaXe, x.TenXe, x.HangXe, x.MauSac, 
                    ct.SoLuong, ct.GiaNhap, ct.ThanhTien
                    FROM ChiTietPhieuNhap ct
                    JOIN XeMay x ON ct.MaXe = x.MaXe
                    WHERE ct.MaPN = @MaPN";

            SqlParameter parameter = new SqlParameter("@MaPN", maPN);
            return DatabaseAcces.Instance.ExecuteQuery(query, parameter);
        }

        // Update PhieuNhap
        public bool UpdatePhieuNhap(PhieuNhap phieuNhap)
        {
            string query = @"UPDATE PhieuNhap 
                           SET NgayNhap = @NgayNhap, 
                               MaNCC = @MaNCC,
                               MaNV = @MaNV,
                               GhiChu = @GhiChu,
                               TrangThai = @TrangThai
                           WHERE MaPN = @MaPN";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPN", phieuNhap.MaPN),
                new SqlParameter("@NgayNhap", phieuNhap.NgayNhap),
                new SqlParameter("@MaNCC", phieuNhap.MaNCC),
                new SqlParameter("@MaNV", phieuNhap.MaNV),
                new SqlParameter("@GhiChu", phieuNhap.GhiChu ?? (object)DBNull.Value),
                new SqlParameter("@TrangThai", phieuNhap.TrangThai)
            };

            return DatabaseAcces.Instance.ExecuteNonQuery(query, parameters) > 0;
        }

        // Search PhieuNhap
        public DataTable SearchPhieuNhap(string keyword, DateTime? fromDate = null, DateTime? toDate = null)
        {
            string query = @"SELECT pn.MaPN, pn.NgayNhap, ncc.TenNCC, nv.HoTen AS NhanVien, 
                    pn.TongTien, ISNULL(pn.GhiChu, '') AS GhiChu, pn.TrangThai
                    FROM PhieuNhap pn
                    JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
                    JOIN NhanVien nv ON pn.MaNV = nv.MaNV
                    WHERE (pn.MaPN LIKE @Keyword OR ncc.TenNCC LIKE @Keyword OR nv.HoTen LIKE @Keyword)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };

            if (fromDate.HasValue)
            {
                query += " AND pn.NgayNhap >= @FromDate";
                parameters.Add(new SqlParameter("@FromDate", fromDate.Value));
            }

            if (toDate.HasValue)
            {
                query += " AND pn.NgayNhap <= @ToDate";
                parameters.Add(new SqlParameter("@ToDate", toDate.Value));
            }

            query += " ORDER BY pn.NgayNhap DESC";
            return DatabaseAcces.Instance.ExecuteQuery(query, parameters.ToArray());
        }

        // Delete ChiTietPhieuNhap
        public bool DeleteChiTietPhieuNhap(string maPN, string maXe)
        {
            string query = "DELETE FROM ChiTietPhieuNhap WHERE MaPN = @MaPN AND MaXe = @MaXe";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaPN", maPN),
                new SqlParameter("@MaXe", maXe)
            };

            return DatabaseAcces.Instance.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}