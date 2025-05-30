using System;
using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class KhachHangBUS
    {
        private static KhachHangBUS instance;
        public static KhachHangBUS Instance
        {
            get
            {
                if (instance == null) instance = new KhachHangBUS();
                return instance;
            }
        }
        private KhachHangBUS() { }

        public DataTable GetAll(int page = 1, int pageSize = 20) => KhachHangDAO.Instance.GetAll(page, pageSize);
        public int GetTotalCount() => KhachHangDAO.Instance.GetTotalCount();

        public bool Add(KhachHang kh)
        {
            if (string.IsNullOrWhiteSpace(kh.HoTen)) return false;

            // Generate MaKH
            kh.MaKH = KhachHangDAO.Instance.GenerateNewMaKH();

            // Check if MaKH already exists
            if (KhachHangDAO.Instance.IsMaKHExists(kh.MaKH))
            {
                return false; // Let the UI handle the warning
            }

            return KhachHangDAO.Instance.Add(kh);
        }

        public bool Update(KhachHang kh) => KhachHangDAO.Instance.Update(kh);
        public bool Delete(string maKH) => KhachHangDAO.Instance.Delete(maKH);
        public DataTable Search(string keyword) => KhachHangDAO.Instance.Search(keyword);

        public bool IsMaKHExists(string maKH)
        {
            return KhachHangDAO.Instance.IsMaKHExists(maKH);
        }
    }
}