using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance;
        public static KhachHangDAO Instance
        {
            get
            {
                if (instance == null) instance = new KhachHangDAO();
                return instance;
            }
        }
        private KhachHangDAO() { }

        public DataTable GetAll(int page = 1, int pageSize = 20)
        {
            string query = @"SELECT * FROM KhachHang WHERE TrangThai = 1
                             ORDER BY NgayDangKy DESC
                             OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            return DatabaseAcces.Instance.ExecuteQuery(query,
                new SqlParameter("@Offset", (page - 1) * pageSize),
                new SqlParameter("@PageSize", pageSize));
        }

        public int GetTotalCount()
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE TrangThai = 1";
            return Convert.ToInt32(DatabaseAcces.Instance.ExecuteScalar(query));
        }

        public bool Add(KhachHang kh)
        {
            string proc = "sp_ThemKhachHang";
            SqlParameter[] p = {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@GioiTinh", kh.GioiTinh),
                new SqlParameter("@NgaySinh", kh.NgaySinh ?? (object)DBNull.Value),
                new SqlParameter("@DiaChi", kh.DiaChi ?? (object)DBNull.Value),
                new SqlParameter("@DienThoai", kh.DienThoai ?? (object)DBNull.Value),
                new SqlParameter("@Email", kh.Email ?? (object)DBNull.Value),
                new SqlParameter("@CMND", kh.CMND ?? (object)DBNull.Value)
            };
            return DatabaseAcces.Instance.ExecuteStoredProcedure(proc, p) > 0;
        }

        public bool Update(KhachHang kh)
        {
            string proc = "sp_SuaKhachHang";
            SqlParameter[] p = {
                new SqlParameter("@MaKH", kh.MaKH),
                new SqlParameter("@HoTen", kh.HoTen),
                new SqlParameter("@GioiTinh", kh.GioiTinh),
                new SqlParameter("@NgaySinh", kh.NgaySinh ?? (object)DBNull.Value),
                new SqlParameter("@DiaChi", kh.DiaChi ?? (object)DBNull.Value),
                new SqlParameter("@DienThoai", kh.DienThoai ?? (object)DBNull.Value),
                new SqlParameter("@Email", kh.Email ?? (object)DBNull.Value),
                new SqlParameter("@CMND", kh.CMND ?? (object)DBNull.Value),
                new SqlParameter("@TrangThai", kh.TrangThai)
            };
            return DatabaseAcces.Instance.ExecuteStoredProcedure(proc, p) > 0;
        }

        public bool Delete(string maKH)
        {
            string query = "UPDATE KhachHang SET TrangThai = 0 WHERE MaKH = @MaKH";
            return DatabaseAcces.Instance.ExecuteNonQuery(query, new SqlParameter("@MaKH", maKH)) > 0;
        }

        public DataTable Search(string keyword)
        {
            string query = @"SELECT * FROM KhachHang WHERE TrangThai = 1 AND 
                            (MaKH LIKE @kw OR HoTen LIKE @kw OR DienThoai LIKE @kw OR Email LIKE @kw)";
            return DatabaseAcces.Instance.ExecuteQuery(query, new SqlParameter("@kw", "%" + keyword + "%"));
        }

        public string GenerateNewMaKH()
        {
            string query = "SELECT TOP 1 MaKH FROM KhachHang ORDER BY MaKH DESC";
            DataTable dt = DatabaseAcces.Instance.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                string last = dt.Rows[0][0].ToString();
                int num = int.Parse(last.Substring(2)) + 1;
                return "KH" + num.ToString("D3");
            }
            return "KH001";
        }

        public bool IsMaKHExists(string maKH)
        {
            string query = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
            int count = Convert.ToInt32(DatabaseAcces.Instance.ExecuteScalar(query, new SqlParameter("@MaKH", maKH)));
            return count > 0;
        }
    }
}