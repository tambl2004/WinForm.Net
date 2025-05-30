using System;
using System.Data;
using DAO;
using DTO;

namespace BUS
{
    public class NhaCungCapBUS
    {
        private static NhaCungCapBUS instance;

        public static NhaCungCapBUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new NhaCungCapBUS();
                return instance;
            }
        }

        private NhaCungCapBUS() { }

        public NhaCungCap GetById(string maNCC)
        {
            return NhaCungCapDAO.Instance.GetById(maNCC);
        }

        public DataTable GetAllNhaCungCap()
        {
            return NhaCungCapDAO.Instance.GetAllNhaCungCap();
        }

        public string GenerateNewMaNCC()
        {
            return NhaCungCapDAO.Instance.GenerateNewMaNCC();
        }

        public bool AddNhaCungCap(string tenNCC, string diaChi, string dienThoai, string email, string nguoiLienHe)
        {
            // Validation
            if (string.IsNullOrEmpty(tenNCC))
                return false;

            string maNCC = GenerateNewMaNCC();
            NhaCungCap ncc = new NhaCungCap(maNCC, tenNCC, diaChi, dienThoai, email, nguoiLienHe);

            return NhaCungCapDAO.Instance.Add(ncc);
        }

        public bool UpdateNhaCungCap(string maNCC, string tenNCC, string diaChi, string dienThoai, string email, string nguoiLienHe, bool trangThai)
        {
            // Validation
            if (string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(tenNCC))
                return false;

            NhaCungCap ncc = new NhaCungCap(maNCC, tenNCC, diaChi, dienThoai, email, nguoiLienHe, trangThai);

            return NhaCungCapDAO.Instance.Update(ncc);
        }

        public bool DeleteNhaCungCap(string maNCC)
        {
            if (string.IsNullOrEmpty(maNCC))
                return false;

            return NhaCungCapDAO.Instance.Delete(maNCC);
        }

        public DataTable SearchNhaCungCap(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return GetAllNhaCungCap();

            return NhaCungCapDAO.Instance.SearchNhaCungCap(keyword);
        }
    }
}
