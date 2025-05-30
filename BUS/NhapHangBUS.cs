using System;
using System.Collections.Generic;
using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class NhapHangBUS
    {
        private static NhapHangBUS instance;
        public static NhapHangBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhapHangBUS();
                return instance;
            }
        }

        private NhapHangBUS() { }

        // Get all suppliers for dropdown
        public DataTable GetAllNhaCungCap()
        {
            string query = "SELECT MaNCC, TenNCC FROM NhaCungCap WHERE TrangThai = 1";
            return DatabaseAcces.Instance.ExecuteQuery(query);
        }

        // Get all employees for dropdown
        public DataTable GetAllNhanVien()
        {
            string query = "SELECT MaNV, HoTen FROM NhanVien WHERE TrangThai = 1";
            return DatabaseAcces.Instance.ExecuteQuery(query);
        }

        // Get all motorcycles for dropdown
        public DataTable GetAllXeMay()
        {
            try
            {
                string query = @"SELECT MaXe, TenXe, HangXe, MauSac, GiaNhap 
                           FROM XeMay 
                           WHERE TrangThai = 1 
                           ORDER BY TenXe";
                return DatabaseAcces.Instance.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách xe: " + ex.Message);
            }
        }

        // Create new import receipt
        public bool CreatePhieuNhap(string maNCC, string maNV, string ghiChu,
                                  List<ChiTietPhieuNhap> chiTietList)
        {
            if (string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(maNV) || chiTietList.Count == 0)
                return false;

            string maPN = NhapHangDAO.Instance.GenerateNewMaPN();

            // Create PhieuNhap
            PhieuNhap phieuNhap = new PhieuNhap
            {
                MaPN = maPN,
                NgayNhap = DateTime.Now,
                MaNCC = maNCC,
                MaNV = maNV,
                GhiChu = ghiChu
            };

            bool success = NhapHangDAO.Instance.CreatePhieuNhap(phieuNhap);
            if (!success) return false;

            // Add all ChiTietPhieuNhap
            foreach (var chiTiet in chiTietList)
            {
                chiTiet.MaPN = maPN;
                chiTiet.ThanhTien = chiTiet.SoLuong * chiTiet.GiaNhap;

                success = NhapHangDAO.Instance.AddChiTietPhieuNhap(chiTiet);
                if (!success) return false;
            }

            return true;
        }

        // Get all PhieuNhap with pagination
        public DataTable GetAllPhieuNhap(int page = 1, int pageSize = 20)
        {
            return NhapHangDAO.Instance.GetAllPhieuNhap(page, pageSize);
        }

        // Get total PhieuNhap count
        public int GetTotalCount()
        {
            return NhapHangDAO.Instance.GetTotalCount();
        }

        // Get PhieuNhap details
        public PhieuNhap GetPhieuNhap(string maPN)
        {
            return NhapHangDAO.Instance.GetPhieuNhap(maPN);
        }

        // Get ChiTietPhieuNhap by MaPN
        public DataTable GetChiTietPhieuNhap(string maPN)
        {
            return NhapHangDAO.Instance.GetChiTietPhieuNhap(maPN);
        }

        // Update PhieuNhap
        public bool UpdatePhieuNhap(PhieuNhap phieuNhap)
        {
            return NhapHangDAO.Instance.UpdatePhieuNhap(phieuNhap);
        }

        // Search PhieuNhap
        public DataTable SearchPhieuNhap(string keyword, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return NhapHangDAO.Instance.SearchPhieuNhap(keyword, fromDate, toDate);
        }

        // Delete ChiTietPhieuNhap
        public bool DeleteChiTietPhieuNhap(string maPN, string maXe)
        {
            return NhapHangDAO.Instance.DeleteChiTietPhieuNhap(maPN, maXe);
        }
    }
}