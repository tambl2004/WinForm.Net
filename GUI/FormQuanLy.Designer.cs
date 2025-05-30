namespace GUI
{
    partial class FormQuanLy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelForm = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_QuanLyPhieuNhap = new System.Windows.Forms.Button();
            this.btn_HoaDon = new System.Windows.Forms.Button();
            this.btn_TongQuan = new System.Windows.Forms.Button();
            this.btn_NhapHang = new System.Windows.Forms.Button();
            this.btn_BanHang = new System.Windows.Forms.Button();
            this.btn_ThongKe = new System.Windows.Forms.Button();
            this.btn_NhaCungCap = new System.Windows.Forms.Button();
            this.btn_NhanVien = new System.Windows.Forms.Button();
            this.btn_KhachHang = new System.Windows.Forms.Button();
            this.btn_SanPham = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelForm
            // 
            this.panelForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelForm.Location = new System.Drawing.Point(291, 15);
            this.panelForm.Margin = new System.Windows.Forms.Padding(4);
            this.panelForm.Name = "panelForm";
            this.panelForm.Size = new System.Drawing.Size(803, 858);
            this.panelForm.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_QuanLyPhieuNhap);
            this.panel1.Controls.Add(this.btn_HoaDon);
            this.panel1.Controls.Add(this.btn_TongQuan);
            this.panel1.Controls.Add(this.btn_NhapHang);
            this.panel1.Controls.Add(this.btn_BanHang);
            this.panel1.Controls.Add(this.btn_ThongKe);
            this.panel1.Controls.Add(this.btn_NhaCungCap);
            this.panel1.Controls.Add(this.btn_NhanVien);
            this.panel1.Controls.Add(this.btn_KhachHang);
            this.panel1.Controls.Add(this.btn_SanPham);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(16, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 858);
            this.panel1.TabIndex = 2;
            // 
            // btn_QuanLyPhieuNhap
            // 
            this.btn_QuanLyPhieuNhap.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_QuanLyPhieuNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_QuanLyPhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_QuanLyPhieuNhap.Location = new System.Drawing.Point(30, 697);
            this.btn_QuanLyPhieuNhap.Margin = new System.Windows.Forms.Padding(4);
            this.btn_QuanLyPhieuNhap.Name = "btn_QuanLyPhieuNhap";
            this.btn_QuanLyPhieuNhap.Size = new System.Drawing.Size(209, 54);
            this.btn_QuanLyPhieuNhap.TabIndex = 12;
            this.btn_QuanLyPhieuNhap.Text = "Quản lý phiếu nhập";
            this.btn_QuanLyPhieuNhap.UseVisualStyleBackColor = false;
            this.btn_QuanLyPhieuNhap.Click += new System.EventHandler(this.Btn_QuanLyPhieuNhap_Click);
            // 
            // btn_HoaDon
            // 
            this.btn_HoaDon.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_HoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_HoaDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_HoaDon.Location = new System.Drawing.Point(30, 550);
            this.btn_HoaDon.Margin = new System.Windows.Forms.Padding(4);
            this.btn_HoaDon.Name = "btn_HoaDon";
            this.btn_HoaDon.Size = new System.Drawing.Size(209, 54);
            this.btn_HoaDon.TabIndex = 11;
            this.btn_HoaDon.Text = "Hóa Đơn";
            this.btn_HoaDon.UseVisualStyleBackColor = false;
            this.btn_HoaDon.Click += new System.EventHandler(this.Btn_HoaDon_Click);
            // 
            // btn_TongQuan
            // 
            this.btn_TongQuan.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_TongQuan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_TongQuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TongQuan.Location = new System.Drawing.Point(30, 113);
            this.btn_TongQuan.Margin = new System.Windows.Forms.Padding(4);
            this.btn_TongQuan.Name = "btn_TongQuan";
            this.btn_TongQuan.Size = new System.Drawing.Size(209, 54);
            this.btn_TongQuan.TabIndex = 1;
            this.btn_TongQuan.Text = "Tổng Quan";
            this.btn_TongQuan.UseVisualStyleBackColor = false;
            // 
            // btn_NhapHang
            // 
            this.btn_NhapHang.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_NhapHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_NhapHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NhapHang.Location = new System.Drawing.Point(30, 623);
            this.btn_NhapHang.Margin = new System.Windows.Forms.Padding(4);
            this.btn_NhapHang.Name = "btn_NhapHang";
            this.btn_NhapHang.Size = new System.Drawing.Size(209, 54);
            this.btn_NhapHang.TabIndex = 7;
            this.btn_NhapHang.Text = "Nhập hàng";
            this.btn_NhapHang.UseVisualStyleBackColor = false;
            // 
            // btn_BanHang
            // 
            this.btn_BanHang.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_BanHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BanHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BanHang.Location = new System.Drawing.Point(30, 476);
            this.btn_BanHang.Margin = new System.Windows.Forms.Padding(4);
            this.btn_BanHang.Name = "btn_BanHang";
            this.btn_BanHang.Size = new System.Drawing.Size(209, 54);
            this.btn_BanHang.TabIndex = 6;
            this.btn_BanHang.Text = "Bán hàng";
            this.btn_BanHang.UseVisualStyleBackColor = false;
            // 
            // btn_ThongKe
            // 
            this.btn_ThongKe.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_ThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ThongKe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ThongKe.Location = new System.Drawing.Point(30, 773);
            this.btn_ThongKe.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ThongKe.Name = "btn_ThongKe";
            this.btn_ThongKe.Size = new System.Drawing.Size(209, 54);
            this.btn_ThongKe.TabIndex = 10;
            this.btn_ThongKe.Text = "Báo Cáo Thống Kê";
            this.btn_ThongKe.UseVisualStyleBackColor = false;
            // 
            // btn_NhaCungCap
            // 
            this.btn_NhaCungCap.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_NhaCungCap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_NhaCungCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NhaCungCap.Location = new System.Drawing.Point(30, 404);
            this.btn_NhaCungCap.Margin = new System.Windows.Forms.Padding(4);
            this.btn_NhaCungCap.Name = "btn_NhaCungCap";
            this.btn_NhaCungCap.Size = new System.Drawing.Size(209, 54);
            this.btn_NhaCungCap.TabIndex = 5;
            this.btn_NhaCungCap.Text = "Quản Lý Nhà Cung Cấp";
            this.btn_NhaCungCap.UseVisualStyleBackColor = false;
            // 
            // btn_NhanVien
            // 
            this.btn_NhanVien.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_NhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_NhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NhanVien.Location = new System.Drawing.Point(30, 331);
            this.btn_NhanVien.Margin = new System.Windows.Forms.Padding(4);
            this.btn_NhanVien.Name = "btn_NhanVien";
            this.btn_NhanVien.Size = new System.Drawing.Size(209, 54);
            this.btn_NhanVien.TabIndex = 4;
            this.btn_NhanVien.Text = "Quản Lý Nhân Viên";
            this.btn_NhanVien.UseVisualStyleBackColor = false;
            // 
            // btn_KhachHang
            // 
            this.btn_KhachHang.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_KhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_KhachHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KhachHang.Location = new System.Drawing.Point(30, 259);
            this.btn_KhachHang.Margin = new System.Windows.Forms.Padding(4);
            this.btn_KhachHang.Name = "btn_KhachHang";
            this.btn_KhachHang.Size = new System.Drawing.Size(209, 54);
            this.btn_KhachHang.TabIndex = 3;
            this.btn_KhachHang.Text = "Quản Lý Khách hàng";
            this.btn_KhachHang.UseVisualStyleBackColor = false;
            // 
            // btn_SanPham
            // 
            this.btn_SanPham.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_SanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SanPham.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SanPham.Location = new System.Drawing.Point(30, 187);
            this.btn_SanPham.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SanPham.Name = "btn_SanPham";
            this.btn_SanPham.Size = new System.Drawing.Size(209, 54);
            this.btn_SanPham.TabIndex = 2;
            this.btn_SanPham.Text = "Quản Lý Sản Phẩm";
            this.btn_SanPham.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(37, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh Mục";
            // 
            // FormQuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1125, 888);
            this.Controls.Add(this.panelForm);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormQuanLy";
            this.Text = "QuanLy";
            this.Load += new System.EventHandler(this.FormQuanLy_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_BanHang;
        private System.Windows.Forms.Button btn_ThongKe;
        private System.Windows.Forms.Button btn_NhaCungCap;
        private System.Windows.Forms.Button btn_NhanVien;
        private System.Windows.Forms.Button btn_KhachHang;
        private System.Windows.Forms.Button btn_SanPham;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_NhapHang;
        private System.Windows.Forms.Button btn_TongQuan;
        private System.Windows.Forms.Button btn_QuanLyPhieuNhap;
        private System.Windows.Forms.Button btn_HoaDon;
    }
}