using System;
using System.Collections.Generic;
using System.Data;
using DAO;
using DTO;

namespace BUS
{
    public class XeMayBUS
    {
        private static XeMayBUS instance;

        public static XeMayBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new XeMayBUS();
                return instance;
            }
        }

        private XeMayBUS() { }

        public List<XeMay> GetAllXeMay()
        {
            return XeMayDAO.Instance.GetAllXeMay();
        }

        public DataTable GetXeMayDataTable()
        {
            return XeMayDAO.Instance.GetXeMayDataTable();
        }

        public byte[] GetHinhAnhByMaXe(string maXe)
        {
            return XeMayDAO.Instance.GetHinhAnhByMaXe(maXe);
        }

        public bool AddXeMay(string tenXe, string hangXe, string dongXe, string mauSac,
string mauSac1, int namSX, string soKhung, string soMay, decimal giaNhap,
                            decimal giaBan, int soLuongTon, byte[] hinhAnh, string moTa, bool trangThai)
        {
            try
            {
                string maXe = XeMayDAO.Instance.GenerateNewMaXe();

                XeMay xeMay = new XeMay(
                    maXe,
                    tenXe,
                    hangXe,
                    dongXe,
                    mauSac,
                    namSX,
                    soKhung,
                    soMay,
                    giaNhap,
                    giaBan,
                    0,  // SoLuongTon mặc định là 0
                    hinhAnh,
                    moTa,
                    true  // TrangThai mặc định là true
                );

                return XeMayDAO.Instance.AddXeMay(xeMay);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm xe máy: " + ex.Message);
            }
        }

        public bool UpdateXeMay(string maXe, string tenXe, string hangXe, string dongXe,
                              string mauSac, int namSX, string soKhung, string soMay,
                              decimal giaNhap, decimal giaBan, int soLuongTon,
                              byte[] hinhAnh, string moTa, bool trangThai)
        {
            try
            {
                XeMay xeMay = new XeMay(
                    maXe,
                    tenXe,
                    hangXe,
                    dongXe,
                    mauSac,
                    namSX,
                    soKhung,
                    soMay,
                    giaNhap,
                    giaBan,
                    soLuongTon,
                    hinhAnh,
                    moTa,
                    trangThai
                );

                return XeMayDAO.Instance.UpdateXeMay(xeMay);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật xe máy: " + ex.Message);
            }
        }

        public bool DeleteXeMay(string maXe)
        {
            try
            {
                return XeMayDAO.Instance.DeleteXeMay(maXe);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa xe máy: " + ex.Message);
            }
        }

        public DataTable SearchXeMay(string searchValue)
        {
            try
            {
                return XeMayDAO.Instance.SearchXeMay(searchValue);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm xe máy: " + ex.Message);
            }
        }

        public bool UpdateSoLuongTon(string maXe, int soLuongTon)
        {
            try
            {
                return XeMayDAO.Instance.UpdateSoLuongTon(maXe, soLuongTon);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật số lượng tồn: " + ex.Message);
            }
        }

        public string GenerateNewMaXe()
        {
            try
            {
                return XeMayDAO.Instance.GenerateNewMaXe();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo mã xe mới: " + ex.Message);
            }
        }

        public bool AddXeMay(string tenXe, string hangXe, string dongXe, string mauSac, int namSX, string soKhung, string soMay, decimal giaNhap, decimal giaBan, byte[] imageData, string v)
        {
            throw new NotImplementedException();
        }
    }
}