using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using DTO;

namespace DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public static NhanVienDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhanVienDAO();
                return instance;
            }
        }

        private NhanVienDAO() { }

        public NhanVien GetById(string maNV)
        {
            string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV AND TrangThai = 1";
            SqlParameter param = new SqlParameter("@MaNV", maNV);

            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query, param);

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new NhanVien(
                    row["MaNV"].ToString(),
                    row["HoTen"].ToString(),
                    row["GioiTinh"].ToString(),
                    row["NgaySinh"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["NgaySinh"]),
                    row["DiaChi"].ToString(),
                    row["DienThoai"].ToString(),
                    row["Email"].ToString(),
                    row["ChucVu"].ToString(),
                    row["TenDangNhap"].ToString(),
                    row["MatKhau"].ToString(),
                    Convert.ToBoolean(row["TrangThai"]),
                    row["HinhAnh"] == DBNull.Value ? null : (byte[])row["HinhAnh"]
                );
            }

            return null;
        }

        public NhanVien GetByUsername(string tenDangNhap)
        {
            string query = "SELECT * FROM NhanVien WHERE TenDangNhap = @TenDangNhap AND TrangThai = 1";
            SqlParameter param = new SqlParameter("@TenDangNhap", tenDangNhap);

            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query, param);

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new NhanVien(
                    row["MaNV"].ToString(),
                    row["HoTen"].ToString(),
                    row["GioiTinh"].ToString(),
                    row["NgaySinh"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["NgaySinh"]),
                    row["DiaChi"].ToString(),
                    row["DienThoai"].ToString(),
                    row["Email"].ToString(),
                    row["ChucVu"].ToString(),
                    row["TenDangNhap"].ToString(),
                    row["MatKhau"].ToString(),
                    Convert.ToBoolean(row["TrangThai"]),
                    row["HinhAnh"] == DBNull.Value ? null : (byte[])row["HinhAnh"]
                );
            }

            return null;
        }

        public bool CheckUsernameExists(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM NhanVien WHERE TenDangNhap = @TenDangNhap";
            SqlParameter param = new SqlParameter("@TenDangNhap", tenDangNhap);

            int count = Convert.ToInt32(DatabaseAcces.Instance.ExecuteScalar(query, param));
            return count > 0;
        }

        public string GenerateNewMaNV()
        {
            string query = "SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC";
            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query);

            string newMaNV = "NV001";

            if (data.Rows.Count > 0)
            {
                string lastMaNV = data.Rows[0]["MaNV"].ToString();
                if (lastMaNV.StartsWith("NV"))
                {
                    int number = Convert.ToInt32(lastMaNV.Substring(2)) + 1;
                    newMaNV = "NV" + number.ToString("D3");
                }
            }

            return newMaNV;
        }

        public bool Add(NhanVien nhanVien)
        {
            string query = "sp_ThemNhanVien";

            // Chỉ định kiểu dữ liệu cho tham số @HinhAnh
            SqlParameter hinhAnhParam = new SqlParameter("@HinhAnh", SqlDbType.VarBinary)
            {
                Value = nhanVien.HinhAnh ?? (object)DBNull.Value
            };

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", nhanVien.MaNV),
                new SqlParameter("@HoTen", nhanVien.HoTen),
                new SqlParameter("@GioiTinh", nhanVien.GioiTinh),
                new SqlParameter("@NgaySinh", nhanVien.NgaySinh ?? (object)DBNull.Value),
                new SqlParameter("@DiaChi", nhanVien.DiaChi ?? (object)DBNull.Value),
                new SqlParameter("@DienThoai", nhanVien.DienThoai ?? (object)DBNull.Value),
                new SqlParameter("@Email", nhanVien.Email ?? (object)DBNull.Value),
                new SqlParameter("@ChucVu", nhanVien.ChucVu),
                new SqlParameter("@TenDangNhap", nhanVien.TenDangNhap),
                new SqlParameter("@MatKhau", nhanVien.MatKhau),
                hinhAnhParam // Sử dụng tham số đã được định nghĩa kiểu
            };

            int result = DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters);
            return result > 0;
        }

        public bool Update(NhanVien nhanVien)
        {
            string query = "sp_SuaNhanVien";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNV", nhanVien.MaNV),
                new SqlParameter("@HoTen", nhanVien.HoTen),
                new SqlParameter("@GioiTinh", nhanVien.GioiTinh),
                new SqlParameter("@NgaySinh", nhanVien.NgaySinh ?? (object)DBNull.Value),
                new SqlParameter("@DiaChi", nhanVien.DiaChi ?? (object)DBNull.Value),
                new SqlParameter("@DienThoai", nhanVien.DienThoai ?? (object)DBNull.Value),
                new SqlParameter("@Email", nhanVien.Email ?? (object)DBNull.Value),
                new SqlParameter("@ChucVu", nhanVien.ChucVu),
                new SqlParameter("@TenDangNhap", nhanVien.TenDangNhap),
                new SqlParameter("@MatKhau", nhanVien.MatKhau),
                new SqlParameter("@TrangThai", nhanVien.TrangThai),
                new SqlParameter("@HinhAnh", nhanVien.HinhAnh ?? (object)DBNull.Value)
            };

            int result = DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters);
            return result > 0;
        }

        public bool Delete(string maNV)
        {
            string query = "UPDATE NhanVien SET TrangThai = 0 WHERE MaNV = @MaNV";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaNV", maNV)
        };

            int result = DatabaseAcces.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public DataTable GetAllNhanVien()
        {
            string query = "SELECT * FROM NhanVien WHERE TrangThai = 1";
            return DatabaseAcces.Instance.ExecuteQuery(query);
        }

        public NhanVien Login(string tenDangNhap, string matKhau)
        {
            string query = "SELECT MaNV, HoTen, ChucVu FROM NhanVien WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau AND TrangThai = 1";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tenDangNhap),
                new SqlParameter("@MatKhau", matKhau)
            };

            DataTable result = DatabaseAcces.Instance.ExecuteQuery(query, parameters);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                return GetById(row["MaNV"].ToString());
            }

            return null;
        }
    }
}