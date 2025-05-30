using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class FormBanHang : Form
    {
        private string maNV; // Mã nhân viên hiện tại
        private string maHD; // Mã hóa đơn hiện tại
        private decimal tongTien = 0; // Tổng tiền
        private DataTable dtChiTiet; // Bảng chi tiết hóa đơn

        public FormBanHang(string maNV)
        {
            InitializeComponent();
            this.maNV = maNV;
            
            // Khởi tạo DataTable chi tiết
            dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("MaXe", typeof(string));
            dtChiTiet.Columns.Add("TenXe", typeof(string));
            dtChiTiet.Columns.Add("SoLuong", typeof(int));
            dtChiTiet.Columns.Add("GiaBan", typeof(decimal));
            dtChiTiet.Columns.Add("ThanhTien", typeof(decimal));
            
            // Cài đặt các sự kiện
            this.Load += FormBanHang_Load;
            btnThemXe.Click += BtnThemXe_Click;
            btnXoaXe.Click += BtnXoaXe_Click;
            btnThanhToan.Click += BtnThanhToan_Click;
            dgvChiTietHoaDon.CellClick += DgvChiTietHoaDon_CellClick;
            cmbXe.SelectedIndexChanged += CmbXe_SelectedIndexChanged;
            txt_TonKho.Enabled = false;
            txt_TonKho.BackColor = Color.White; // Giữ màu nền trắng
        }

        private void FormBanHang_Load(object sender, EventArgs e)
        {
            // Nạp dữ liệu cho ComboBox khách hàng
            LoadKhachHang();
            
            // Nạp dữ liệu cho ComboBox xe máy
            LoadXeMay();
            
            // Thiết lập cấu trúc DataGridView
            SetupDataGridView();

        }

        private void LoadKhachHang()
        {
            try
            {
                // Lấy danh sách khách hàng từ BUS
                DataTable dtKhachHang = KhachHangBUS.Instance.GetAll();
                
                // Thiết lập nguồn dữ liệu cho ComboBox
                cmbKhachHang.DataSource = dtKhachHang;
                cmbKhachHang.DisplayMember = "HoTen";
                cmbKhachHang.ValueMember = "MaKH";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadXeMay()
        {
            try
            {
                // Lấy danh sách xe máy từ BUS
                DataTable dtXeMay = XeMayBUS.Instance.GetXeMayDataTable();

                // Lọc chỉ lấy xe có số lượng tồn > 0
                DataView dv = new DataView(dtXeMay);
                dv.RowFilter = "SoLuongTon > 0";

                // Thiết lập nguồn dữ liệu cho ComboBox
                cmbXe.DataSource = dv.ToTable();
                cmbXe.DisplayMember = "TenXe";
                cmbXe.ValueMember = "MaXe";

                // Gọi hàm cập nhật số lượng tồn kho cho xe đầu tiên (nếu có)
                if (cmbXe.Items.Count > 0)
                {
                    CmbXe_SelectedIndexChanged(null, null);
                }
                else
                {
                    txt_TonKho.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách xe máy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            // Thiết lập cột cho DataGridView
            dgvChiTietHoaDon.DataSource = dtChiTiet;
            
            // Định dạng hiển thị
            dgvChiTietHoaDon.Columns["MaXe"].HeaderText = "Mã xe";
            dgvChiTietHoaDon.Columns["TenXe"].HeaderText = "Tên xe";
            dgvChiTietHoaDon.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTietHoaDon.Columns["GiaBan"].HeaderText = "Giá bán";
            dgvChiTietHoaDon.Columns["ThanhTien"].HeaderText = "Thành tiền";
            
            // Định dạng số tiền
            dgvChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
            dgvChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

       

        private void BtnThemXe_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ giao diện
                string maXe = cmbXe.SelectedValue?.ToString();
                string tenXe = cmbXe.Text;
                int soLuong = (int)numSoLuong.Value;

                if (string.IsNullOrEmpty(maXe))
                {
                    MessageBox.Show("Vui lòng chọn xe", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin xe
                List<XeMay> danhSachXe = XeMayBUS.Instance.GetAllXeMay();
                XeMay xe = danhSachXe.FirstOrDefault(x => x.MaXe == maXe);

                if (xe == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin xe", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra số lượng tồn
                if (xe.SoLuongTon < soLuong)
                {
                    MessageBox.Show($"Số lượng tồn kho không đủ. Hiện chỉ còn {xe.SoLuongTon} xe.", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem xe đã có trong giỏ hàng chưa
                bool found = false;
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    if (row["MaXe"].ToString() == maXe)
                    {
                        int soLuongCu = Convert.ToInt32(row["SoLuong"]);
                        int soLuongMoi = soLuongCu + soLuong;

                        if (xe.SoLuongTon < soLuongMoi)
                        {
                            MessageBox.Show($"Số lượng tồn kho không đủ. Hiện chỉ còn {xe.SoLuongTon} xe.", "Thông báo",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        decimal thanhTien = soLuongMoi * xe.GiaBan;
                        row["SoLuong"] = soLuongMoi;
                        row["ThanhTien"] = thanhTien;
                        found = true;
                        break;
                    }
                }

                // Nếu xe chưa có trong giỏ hàng, thêm mới
                if (!found)
                {
                    decimal thanhTien = soLuong * xe.GiaBan;
                    dtChiTiet.Rows.Add(maXe, tenXe, soLuong, xe.GiaBan, thanhTien);
                }

                // Cập nhật tổng tiền
                CapNhatTongTien();

                // Reset số lượng về 1
                numSoLuong.Value = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoaXe_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dgvChiTietHoaDon.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn xe cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy thông tin dòng được chọn
                DataGridViewRow selectedRow = dgvChiTietHoaDon.SelectedRows[0];
                string maXe = selectedRow.Cells["MaXe"].Value.ToString();
                string tenXe = selectedRow.Cells["TenXe"].Value.ToString();

                // Xác nhận xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa xe '{tenXe}' khỏi hóa đơn?", "Xác nhận",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Nếu maHD chưa được tạo (chưa thanh toán), chỉ xóa khỏi DataTable
                    if (string.IsNullOrEmpty(maHD))
                    {
                        foreach (DataRow row in dtChiTiet.Rows)
                        {
                            if (row["MaXe"].ToString() == maXe)
                            {
                                dtChiTiet.Rows.Remove(row);
                                break;
                            }
                        }
                    }
                    else
                    {
                        // Nếu maHD đã được tạo (hóa đơn đã tồn tại trong CSDL), xóa khỏi CSDL
                    

                        // Xóa khỏi DataTable
                        foreach (DataRow row in dtChiTiet.Rows)
                        {
                            if (row["MaXe"].ToString() == maXe)
                            {
                                dtChiTiet.Rows.Remove(row);
                                break;
                            }
                        }

                        // Cập nhật tổng tiền của hóa đơn trong CSDL (nếu cần)
                        DTO.HoaDon hoaDon = HoaDonBUS.Instance.GetHoaDonByMa(maHD);
                        if (hoaDon != null)
                        {
                            hoaDon.TongTien = tongTien; // Cập nhật tổng tiền mới
                            HoaDonBUS.Instance.CapNhatHoaDon(maHD, hoaDon.MaKH, hoaDon.MaNV, hoaDon.NgayBan,
                                                            hoaDon.GiamGia, hoaDon.GhiChu, hoaDon.TrangThai);
                        }
                    }

                    // Cập nhật tổng tiền trên giao diện
                    CapNhatTongTien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa xe: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có sản phẩm nào trong hóa đơn không
                if (dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất một xe vào hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo hóa đơn mới
                string maKH = cmbKhachHang.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maKH))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                maHD = HoaDonBUS.Instance.TaoHoaDon(maKH, maNV); // Tạo hóa đơn mới
                if (string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show("Lỗi khi tạo hóa đơn mới", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Text = "Quản lý bán hàng - Hóa đơn: " + maHD;

                // Thêm tất cả chi tiết hóa đơn vào CSDL
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    string maXe = row["MaXe"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    decimal giaBan = Convert.ToDecimal(row["GiaBan"]);
                    // Không cần truyền thanhTien, vì nó sẽ được tính trong HoaDonBUS
                    HoaDonBUS.Instance.ThemChiTietHoaDon(maHD, maXe, soLuong, giaBan, 0, null);
                }

                // Xác nhận thanh toán
                DialogResult result = MessageBox.Show($"Xác nhận thanh toán hóa đơn {maHD} với tổng tiền {tongTien:N0} VNĐ?",
                                                     "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Cập nhật trạng thái hóa đơn
                    DTO.HoaDon hoaDon = HoaDonBUS.Instance.GetHoaDonByMa(maHD);
                    if (hoaDon != null)
                    {
                        hoaDon.TrangThai = true;
                        hoaDon.ThanhToan = tongTien; // Cập nhật số tiền thanh toán
                        hoaDon.TongTien = tongTien;  // Cập nhật tổng tiền
                        HoaDonBUS.Instance.CapNhatHoaDon(maHD, hoaDon.MaKH, hoaDon.MaNV, hoaDon.NgayBan,
                                                        hoaDon.GiamGia, hoaDon.GhiChu, hoaDon.TrangThai);
                    }

                    // Hiển thị thông báo thành công
                    MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xóa giỏ hàng và reset
                    dtChiTiet.Rows.Clear();
                    tongTien = 0;
                    lblTongTien.Text = "Tổng tiền: 0 ₫";
                    maHD = null; // Reset mã hóa đơn
                    this.Text = "Quản lý bán hàng";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý khi click vào cell trong DataGridView
            if (e.RowIndex >= 0)
            {
                dgvChiTietHoaDon.Rows[e.RowIndex].Selected = true;
            }
        }
        private void CmbXe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã xe được chọn
                string maXe = cmbXe.SelectedValue?.ToString();

                if (!string.IsNullOrEmpty(maXe))
                {
                    // Lấy thông tin xe từ BUS
                    List<XeMay> danhSachXe = XeMayBUS.Instance.GetAllXeMay();
                    XeMay xe = danhSachXe.FirstOrDefault(x => x.MaXe == maXe);

                    if (xe != null)
                    {
                        // Hiển thị số lượng tồn kho lên txt_TonKho
                        txt_TonKho.Text = xe.SoLuongTon.ToString();
                    }
                    else
                    {
                        txt_TonKho.Text = "0"; // Nếu không tìm thấy xe, hiển thị 0
                    }
                }
                else
                {
                    txt_TonKho.Text = "0"; // Nếu không có xe được chọn, hiển thị 0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị số lượng tồn kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CapNhatTongTien()
        {
            // Tính tổng tiền từ DataTable
            tongTien = 0;
            foreach (DataRow row in dtChiTiet.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }
            
            // Cập nhật label
            lblTongTien.Text = $"Tổng tiền: {tongTien:N0} ₫";
        }

        private void BtnLichSuBanHang_Click(object sender, EventArgs e)
        {
            // Mở form lịch sử bán hàng
            HoaDon formLichSu = new HoaDon();
            formLichSu.ShowDialog();
        }
    }
}