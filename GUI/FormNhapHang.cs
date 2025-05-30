using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;

namespace GUI
{
    public partial class FormNhapHang : Form
    {
        private NhanVien currentUser;
        private DataTable dtChiTiet;
        private decimal tongTien = 0;

        public FormNhapHang(NhanVien nhanVien = null)
        {
            InitializeComponent();
            currentUser = nhanVien;

            SetupDataTable();
            LoadNhaCungCap();
            LoadXeMay();
            RegisterEvents();
        }

        private void SetupDataTable()
        {
            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("MaXe", typeof(string));
            dtChiTiet.Columns.Add("TenXe", typeof(string));
            dtChiTiet.Columns.Add("HangXe", typeof(string));
            dtChiTiet.Columns.Add("MauSac", typeof(string));
            dtChiTiet.Columns.Add("SoLuong", typeof(int));
            dtChiTiet.Columns.Add("GiaNhap", typeof(decimal));
            dtChiTiet.Columns.Add("ThanhTien", typeof(decimal));
            
            dgvChiTietNhap.DataSource = dtChiTiet;
            
            // Setup data grid view columns
            if (dgvChiTietNhap.Columns.Count > 0)
            {
                dgvChiTietNhap.Columns["MaXe"].HeaderText = "Mã xe";
                dgvChiTietNhap.Columns["TenXe"].HeaderText = "Tên xe";
                dgvChiTietNhap.Columns["HangXe"].HeaderText = "Hãng xe";
                dgvChiTietNhap.Columns["MauSac"].HeaderText = "Màu sắc";
                dgvChiTietNhap.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvChiTietNhap.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dgvChiTietNhap.Columns["ThanhTien"].HeaderText = "Thành tiền";
                
                dgvChiTietNhap.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                dgvChiTietNhap.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
        }
        
        private void LoadNhaCungCap()
        {
            DataTable dt = NhapHangBUS.Instance.GetAllNhaCungCap();
            cmbNhaCungCap.DataSource = dt;
            cmbNhaCungCap.DisplayMember = "TenNCC";
            cmbNhaCungCap.ValueMember = "MaNCC";
        }

        private void LoadXeMay()
        {
            DataTable dt = NhapHangBUS.Instance.GetAllXeMay(); // Make sure this returns GiaNhap
            cmbXe.DataSource = dt;
            cmbXe.DisplayMember = "TenXe";
            cmbXe.ValueMember = "MaXe";
        }


        private void RegisterEvents()
        {
            btnThemXe.Click += BtnThemXe_Click;
            btnXoaXe.Click += BtnXoaXe_Click;
            btnNhapHang.Click += BtnNhapHang_Click;
            numSoLuong.ValueChanged += UpdateCalculation;
            cmbXe.SelectedIndexChanged += CmbXe_SelectedIndexChanged; // Add this new event handler
        }
        private void CmbXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbXe.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)cmbXe.SelectedItem;
                decimal giaNhap = Convert.ToDecimal(selectedRow["GiaNhap"]);
                txtDonGia.Text = giaNhap.ToString("N0");
                txtDonGia.ReadOnly = true; // Make sure it's read-only
            }
        }
        private void UpdateCalculation(object sender, EventArgs e)
        {
            // This method intentionally left empty - just for registering the event
        }

        private void BtnThemXe_Click(object sender, EventArgs e)
        {
            if (cmbXe.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn xe cần nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maXe = cmbXe.SelectedValue.ToString();
            string tenXe = cmbXe.Text;

            // Get additional info from selected item
            DataRowView selectedRow = (DataRowView)cmbXe.SelectedItem;
            string hangXe = selectedRow["HangXe"].ToString();
            string mauSac = selectedRow["MauSac"].ToString();

            int soLuong = (int)numSoLuong.Value;
            decimal giaNhap;
            if (!decimal.TryParse(txtDonGia.Text.Replace(",", ""), out giaNhap))
            {
                MessageBox.Show("Giá nhập không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal thanhTien = soLuong * giaNhap;


            // Check if the motorcycle already exists in the detail list
            foreach (DataRow row in dtChiTiet.Rows)
            {
                if (row["MaXe"].ToString() == maXe)
                {
                    MessageBox.Show("Xe này đã có trong danh sách nhập hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
            // Add to data table
            dtChiTiet.Rows.Add(maXe, tenXe, hangXe, mauSac, soLuong, giaNhap, thanhTien);
            
            // Update total
            tongTien += thanhTien;
            lblTongTien.Text = "Tổng tiền: " + string.Format("{0:N0}", tongTien) + " ₫";
            
            // Reset inputs
            numSoLuong.Value = 1;
           
        }
        
        private void BtnXoaXe_Click(object sender, EventArgs e)
        {
            if (dgvChiTietNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn xe cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            DataGridViewRow selectedRow = dgvChiTietNhap.SelectedRows[0];
            decimal thanhTien = Convert.ToDecimal(selectedRow.Cells["ThanhTien"].Value);
            
            // Update total
            tongTien -= thanhTien;
            lblTongTien.Text = "Tổng tiền: " + string.Format("{0:N0}", tongTien) + " ₫";
            
            // Remove from data table
            dtChiTiet.Rows.RemoveAt(selectedRow.Index);
        }
        
        private void BtnNhapHang_Click(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (dtChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một xe vào phiếu nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (currentUser == null)
            {
                MessageBox.Show("Không xác định được nhân viên đang đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Create list of ChiTietPhieuNhap
            List<ChiTietPhieuNhap> chiTietList = new List<ChiTietPhieuNhap>();
            foreach (DataRow row in dtChiTiet.Rows)
            {
                ChiTietPhieuNhap chiTiet = new ChiTietPhieuNhap
                {
                    MaXe = row["MaXe"].ToString(),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    GiaNhap = Convert.ToDecimal(row["GiaNhap"]),
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"])
                };
                chiTietList.Add(chiTiet);
            }
            
            // Create PhieuNhap
            string maNCC = cmbNhaCungCap.SelectedValue.ToString();
            string maNV = currentUser.MaNV;
            
            bool success = NhapHangBUS.Instance.CreatePhieuNhap(maNCC, maNV, "", chiTietList);
            
            if (success)
            {
                MessageBox.Show("Nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Reset form
                dtChiTiet.Clear();
                tongTien = 0;
                lblTongTien.Text = "Tổng tiền: 0 ₫";
            }
            else
            {
                MessageBox.Show("Nhập hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnChiTietPhieuNhap_Click(object sender, EventArgs e)
        {
            // Mở form quản lý phiếu nhập
            FormQuanLyPhieuNhap formQuanLyPhieuNhap = new FormQuanLyPhieuNhap();
            formQuanLyPhieuNhap.ShowDialog();
        }
    }
}