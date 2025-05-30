using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DAO
{
    public class XeMayDAO
    {
        private static XeMayDAO instance;

        public static XeMayDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new XeMayDAO();
                return instance;
            }
        }

        private XeMayDAO() { }

        public List<XeMay> GetAllXeMay()
        {
            List<XeMay> list = new List<XeMay>();
            string query = "SELECT * FROM XeMay WHERE TrangThai = 1";

            try
            {
                DataTable data = DatabaseAcces.Instance.ExecuteQuery(query);

                foreach (DataRow row in data.Rows)
                {
                    byte[] hinhAnh = null;
                    if (row["HinhAnh"] != DBNull.Value)
                    {
                        hinhAnh = (byte[])row["HinhAnh"];
                    }

                    XeMay xeMay = new XeMay(
                        row["MaXe"].ToString(),
                        row["TenXe"].ToString(),
                        row["HangXe"].ToString(),
                        row["DongXe"].ToString(),
                        row["MauSac"].ToString(),
                        Convert.ToInt32(row["NamSX"]),
                        row["SoKhung"].ToString(),
                        row["SoMay"].ToString(),
                        Convert.ToDecimal(row["GiaNhap"]),
                        Convert.ToDecimal(row["GiaBan"]),
                        Convert.ToInt32(row["SoLuongTon"]),
                        hinhAnh,
                        row["MoTa"].ToString(),
                        Convert.ToBoolean(row["TrangThai"])
                    );
                    list.Add(xeMay);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn dữ liệu xe máy: " + ex.Message);
            }

            return list;
        }

        public DataTable GetXeMayDataTable()
        {
            string query = @"SELECT MaXe, TenXe, HangXe, DongXe, MauSac, NamSX,
                          GiaNhap, GiaBan, SoLuongTon, TrangThai
                          FROM XeMay
                          WHERE TrangThai = 1
                          ORDER BY MaXe DESC";
            return DatabaseAcces.Instance.ExecuteQuery(query);
        }

        public byte[] GetHinhAnhByMaXe(string maXe)
        {
            string query = "SELECT HinhAnh FROM XeMay WHERE MaXe = @MaXe";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaXe", maXe)
            };

            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query, parameters);
            if (data.Rows.Count > 0 && data.Rows[0]["HinhAnh"] != DBNull.Value)
            {
                return (byte[])data.Rows[0]["HinhAnh"];
            }
            return null;
        }

        public bool AddXeMay(XeMay xeMay)
        {
            string query = @"INSERT INTO XeMay (MaXe, TenXe, HangXe, DongXe, MauSac, 
                            NamSX, SoKhung, SoMay, GiaNhap, GiaBan, HinhAnh, MoTa)
                            VALUES (@MaXe, @TenXe, @HangXe, @DongXe, @MauSac, @NamSX,
                            @SoKhung, @SoMay, @GiaNhap, @GiaBan, @HinhAnh, @MoTa)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaXe", xeMay.MaXe),
                new SqlParameter("@TenXe", xeMay.TenXe),
                new SqlParameter("@HangXe", xeMay.HangXe),
                new SqlParameter("@DongXe", xeMay.DongXe),
                new SqlParameter("@MauSac", xeMay.MauSac),
                new SqlParameter("@NamSX", xeMay.NamSX),
                new SqlParameter("@SoKhung", xeMay.SoKhung),
                new SqlParameter("@SoMay", xeMay.SoMay),
                new SqlParameter("@GiaNhap", xeMay.GiaNhap),
                new SqlParameter("@GiaBan", xeMay.GiaBan),
                new SqlParameter("@HinhAnh", (object)xeMay.HinhAnh ?? DBNull.Value),
                new SqlParameter("@MoTa", xeMay.MoTa ?? (object)DBNull.Value)
            };

            int result = DatabaseAcces.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool UpdateXeMay(XeMay xeMay)
        {
            string query;
            SqlParameter[] parameters;

            if (xeMay.HinhAnh != null && xeMay.HinhAnh.Length > 0)
            {
                query = @"UPDATE XeMay 
                       SET TenXe = @TenXe,
                           HangXe = @HangXe,
                           DongXe = @DongXe,
                           MauSac = @MauSac,
                           NamSX = @NamSX,
                           SoKhung = @SoKhung,
                           SoMay = @SoMay,
                           GiaNhap = @GiaNhap,
                           GiaBan = @GiaBan,
                           SoLuongTon = @SoLuongTon,
                           HinhAnh = @HinhAnh,
                           MoTa = @MoTa,
                           TrangThai = @TrangThai
                       WHERE MaXe = @MaXe";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaXe", xeMay.MaXe),
                    new SqlParameter("@TenXe", xeMay.TenXe),
                    new SqlParameter("@HangXe", xeMay.HangXe),
                    new SqlParameter("@DongXe", xeMay.DongXe),
                    new SqlParameter("@MauSac", xeMay.MauSac),
                    new SqlParameter("@NamSX", xeMay.NamSX),
                    new SqlParameter("@SoKhung", xeMay.SoKhung),
                    new SqlParameter("@SoMay", xeMay.SoMay),
                    new SqlParameter("@GiaNhap", xeMay.GiaNhap),
                    new SqlParameter("@GiaBan", xeMay.GiaBan),
                    new SqlParameter("@SoLuongTon", xeMay.SoLuongTon),
                    new SqlParameter("@HinhAnh", xeMay.HinhAnh),
                    new SqlParameter("@MoTa", xeMay.MoTa ?? (object)DBNull.Value),
                    new SqlParameter("@TrangThai", xeMay.TrangThai)
                };
            }
            else
            {
                query = @"UPDATE XeMay 
                       SET TenXe = @TenXe,
                           HangXe = @HangXe,
                           DongXe = @DongXe,
                           MauSac = @MauSac,
                           NamSX = @NamSX,
                           SoKhung = @SoKhung,
                           SoMay = @SoMay,
                           GiaNhap = @GiaNhap,
                           GiaBan = @GiaBan,
                           SoLuongTon = @SoLuongTon,
                           MoTa = @MoTa,
                           TrangThai = @TrangThai
                       WHERE MaXe = @MaXe";

                parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaXe", xeMay.MaXe),
                    new SqlParameter("@TenXe", xeMay.TenXe),
                    new SqlParameter("@HangXe", xeMay.HangXe),
                    new SqlParameter("@DongXe", xeMay.DongXe),
                    new SqlParameter("@MauSac", xeMay.MauSac),
                    new SqlParameter("@NamSX", xeMay.NamSX),
                    new SqlParameter("@SoKhung", xeMay.SoKhung),
                    new SqlParameter("@SoMay", xeMay.SoMay),
                    new SqlParameter("@GiaNhap", xeMay.GiaNhap),
                    new SqlParameter("@GiaBan", xeMay.GiaBan),
                    new SqlParameter("@SoLuongTon", xeMay.SoLuongTon),
                    new SqlParameter("@MoTa", xeMay.MoTa ?? (object)DBNull.Value),
                    new SqlParameter("@TrangThai", xeMay.TrangThai)
                };
            }

            int result = DatabaseAcces.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public bool DeleteXeMay(string maXe)
        {
            string query = "UPDATE XeMay SET TrangThai = 0 WHERE MaXe = @MaXe";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaXe", maXe)
            };

            int result = DatabaseAcces.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        public DataTable SearchXeMay(string searchValue)
        {
            string query = @"SELECT MaXe, TenXe, HangXe, DongXe, MauSac, NamSX,
                          GiaNhap, GiaBan, SoLuongTon, TrangThai
                          FROM XeMay
                          WHERE (MaXe LIKE N'%' + @searchValue + '%'
                          OR TenXe LIKE N'%' + @searchValue + '%'
                          OR HangXe LIKE N'%' + @searchValue + '%'
                          OR DongXe LIKE N'%' + @searchValue + '%'
                          OR MauSac LIKE N'%' + @searchValue + '%')
                          AND TrangThai = 1
                          ORDER BY MaXe DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@searchValue", searchValue)
            };

            return DatabaseAcces.Instance.ExecuteQuery(query, parameters);
        }

        public string GenerateNewMaXe()
        {
            string query = "SELECT TOP 1 MaXe FROM XeMay ORDER BY MaXe DESC";
            DataTable data = DatabaseAcces.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                string maXe = data.Rows[0]["MaXe"].ToString();
                string soXe = maXe.Substring(2);
                int nextSo = int.Parse(soXe) + 1;
                return "XM" + nextSo.ToString("D3");
            }
            return "XM001";
        }

        public bool UpdateSoLuongTon(string maXe, int soLuongTon)
        {
            string query = "UPDATE XeMay SET SoLuongTon = @SoLuongTon WHERE MaXe = @MaXe";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@SoLuongTon", soLuongTon),
                new SqlParameter("@MaXe", maXe)
            };

            int result = DatabaseAcces.Instance.ExecuteNonQuery(query, parameters);
            return result > 0;
        }
    }
}