using System;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class NhaCungCapDAO
    {
        private static NhaCungCapDAO instance;

        public static NhaCungCapDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhaCungCapDAO();
                return instance;
            }
        }

        private NhaCungCapDAO() { }

        public NhaCungCap GetById(string maNCC)
        {
            string query = "SELECT * FROM NhaCungCap WHERE MaNCC = @MaNCC";
            SqlParameter param = new SqlParameter("@MaNCC", maNCC);

            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query, param);

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new NhaCungCap(
                    row["MaNCC"].ToString(),
                    row["TenNCC"].ToString(),
                    row["DiaChi"].ToString(),
                    row["DienThoai"].ToString(),
                    row["Email"].ToString(),
                    row["NguoiLienHe"].ToString(),
                    Convert.ToBoolean(row["TrangThai"])
                );
            }

            return null;
        }

        public string GenerateNewMaNCC()
        {
            string query = "SELECT TOP 1 MaNCC FROM NhaCungCap ORDER BY MaNCC DESC";
            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query);

            string newMaNCC = "NCC001";

            if (data.Rows.Count > 0)
            {
                string lastMaNCC = data.Rows[0]["MaNCC"].ToString();
                if (lastMaNCC.StartsWith("NCC"))
                {
                    int number = Convert.ToInt32(lastMaNCC.Substring(3)) + 1;
                    newMaNCC = "NCC" + number.ToString("D3");
                }
            }

            return newMaNCC;
        }

        public bool Add(NhaCungCap nhaCungCap)
        {
            string query = "sp_ThemNhaCungCap";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", nhaCungCap.MaNCC),
                new SqlParameter("@TenNCC", nhaCungCap.TenNCC),
                new SqlParameter("@DiaChi", nhaCungCap.DiaChi ?? (object)DBNull.Value),
                new SqlParameter("@DienThoai", nhaCungCap.DienThoai ?? (object)DBNull.Value),
                new SqlParameter("@Email", nhaCungCap.Email ?? (object)DBNull.Value),
                new SqlParameter("@NguoiLienHe", nhaCungCap.NguoiLienHe ?? (object)DBNull.Value)
            };

            int result = DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters);
            return result > 0;
        }

        public bool Update(NhaCungCap nhaCungCap)
        {
            string query = "sp_SuaNhaCungCap";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNCC", nhaCungCap.MaNCC),
                new SqlParameter("@TenNCC", nhaCungCap.TenNCC),
                new SqlParameter("@DiaChi", nhaCungCap.DiaChi ?? (object)DBNull.Value),
                new SqlParameter("@DienThoai", nhaCungCap.DienThoai ?? (object)DBNull.Value),
                new SqlParameter("@Email", nhaCungCap.Email ?? (object)DBNull.Value),
                new SqlParameter("@NguoiLienHe", nhaCungCap.NguoiLienHe ?? (object)DBNull.Value),
                new SqlParameter("@TrangThai", nhaCungCap.TrangThai)
            };

            int result = DatabaseAcces.Instance.ExecuteStoredProcedure(query, parameters);
            return result > 0;
        }

        public bool Delete(string maNCC)
        {
            string query = "UPDATE NhaCungCap SET TrangThai = 0 WHERE MaNCC = @MaNCC";
            SqlParameter param = new SqlParameter("@MaNCC", maNCC);

            int result = DatabaseAcces.Instance.ExecuteNonQuery(query, param);
            return result > 0;
        }

        public DataTable GetAllNhaCungCap()
        {
            string query = "SELECT * FROM NhaCungCap"; // Removed WHERE TrangThai = 1
            return DatabaseAcces.Instance.ExecuteQuery(query);
        }

        public DataTable SearchNhaCungCap(string keyword)
        {
            string query = "SELECT * FROM NhaCungCap WHERE (MaNCC LIKE @Keyword OR TenNCC LIKE @Keyword OR DienThoai LIKE @Keyword OR Email LIKE @Keyword OR NguoiLienHe LIKE @Keyword)"; // Removed TrangThai = 1
            SqlParameter param = new SqlParameter("@Keyword", "%" + keyword + "%");

            return DatabaseAcces.Instance.ExecuteQuery(query, param);
        }
    }
}