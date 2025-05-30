namespace GUI
{
    partial class FormBaoCao
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gb_chonBaoCao = new System.Windows.Forms.GroupBox();
            this.lblReportSummary = new System.Windows.Forms.Label();
            this.btn_LocNgay = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.btn_xuatExel = new System.Windows.Forms.Button();
            this.btn_BaoCaoXuatHang = new System.Windows.Forms.Button();
            this.btn_BaoCaoNhapHang = new System.Windows.Forms.Button();
            this.btn_BaoCaoTonKho = new System.Windows.Forms.Button();
            this.btn_SanPhamBanChay = new System.Windows.Forms.Button();
            this.btn_DoanhThuThang = new System.Windows.Forms.Button();
            this.btn_DoanhThuNgay = new System.Windows.Forms.Button();
            this.gb_BaoCao = new System.Windows.Forms.GroupBox();
            this.dataGridViewBaoCao = new System.Windows.Forms.DataGridView();
            this.panelPaging = new System.Windows.Forms.Panel();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.gb_chonBaoCao.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gb_BaoCao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaoCao)).BeginInit();
            this.panelPaging.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1379, 60);
            this.panelHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(524, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(407, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BÁO CÁO VÀ THỐNG KÊ";
            // 
            // gb_chonBaoCao
            // 
            this.gb_chonBaoCao.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gb_chonBaoCao.BackColor = System.Drawing.Color.White;
            this.gb_chonBaoCao.Controls.Add(this.lblReportSummary);
            this.gb_chonBaoCao.Controls.Add(this.btn_LocNgay);
            this.gb_chonBaoCao.Controls.Add(this.btn_Reset);
            this.gb_chonBaoCao.Controls.Add(this.groupBox3);
            this.gb_chonBaoCao.Controls.Add(this.btn_xuatExel);
            this.gb_chonBaoCao.Controls.Add(this.btn_BaoCaoXuatHang);
            this.gb_chonBaoCao.Controls.Add(this.btn_BaoCaoNhapHang);
            this.gb_chonBaoCao.Controls.Add(this.btn_BaoCaoTonKho);
            this.gb_chonBaoCao.Controls.Add(this.btn_SanPhamBanChay);
            this.gb_chonBaoCao.Controls.Add(this.btn_DoanhThuThang);
            this.gb_chonBaoCao.Controls.Add(this.btn_DoanhThuNgay);
            this.gb_chonBaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_chonBaoCao.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gb_chonBaoCao.Location = new System.Drawing.Point(52, 77);
            this.gb_chonBaoCao.Name = "gb_chonBaoCao";
            this.gb_chonBaoCao.Size = new System.Drawing.Size(1315, 186);
            this.gb_chonBaoCao.TabIndex = 3;
            this.gb_chonBaoCao.TabStop = false;
            this.gb_chonBaoCao.Text = "Chọn báo cáo";
            // 
            // lblReportSummary
            // 
            this.lblReportSummary.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblReportSummary.AutoSize = true;
            this.lblReportSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblReportSummary.Location = new System.Drawing.Point(43, 144);
            this.lblReportSummary.Name = "lblReportSummary";
            this.lblReportSummary.Size = new System.Drawing.Size(227, 26);
            this.lblReportSummary.TabIndex = 29;
            this.lblReportSummary.Text = "Báo cáo: Chưa chọn";
            // 
            // btn_LocNgay
            // 
            this.btn_LocNgay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LocNgay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btn_LocNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LocNgay.FlatAppearance.BorderSize = 0;
            this.btn_LocNgay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LocNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LocNgay.ForeColor = System.Drawing.Color.White;
            this.btn_LocNgay.Location = new System.Drawing.Point(1141, 77);
            this.btn_LocNgay.Name = "btn_LocNgay";
            this.btn_LocNgay.Size = new System.Drawing.Size(150, 40);
            this.btn_LocNgay.TabIndex = 28;
            this.btn_LocNgay.Text = "Lọc Ngày";
            this.btn_LocNgay.UseVisualStyleBackColor = false;
            // 
            // btn_Reset
            // 
            this.btn_Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Reset.BackColor = System.Drawing.Color.Gray;
            this.btn_Reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Reset.FlatAppearance.BorderSize = 0;
            this.btn_Reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Reset.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reset.ForeColor = System.Drawing.Color.White;
            this.btn_Reset.Location = new System.Drawing.Point(1141, 122);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(150, 40);
            this.btn_Reset.TabIndex = 27;
            this.btn_Reset.Text = "Làm mới";
            this.btn_Reset.UseVisualStyleBackColor = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateTimePickerFrom);
            this.groupBox3.Controls.Add(this.dateTimePickerTo);
            this.groupBox3.Controls.Add(this.lblTuNgay);
            this.groupBox3.Controls.Add(this.lblDenNgay);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox3.Location = new System.Drawing.Point(827, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(282, 142);
            this.groupBox3.TabIndex = 26;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bộ lọc";
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dateTimePickerFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePickerFrom.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            this.dateTimePickerFrom.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dateTimePickerFrom.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFrom.Location = new System.Drawing.Point(122, 31);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(150, 33);
            this.dateTimePickerFrom.TabIndex = 7;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dateTimePickerTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dateTimePickerTo.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            this.dateTimePickerTo.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dateTimePickerTo.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTo.Location = new System.Drawing.Point(122, 76);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(150, 33);
            this.dateTimePickerTo.TabIndex = 8;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTuNgay.Location = new System.Drawing.Point(14, 37);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(91, 25);
            this.lblTuNgay.TabIndex = 9;
            this.lblTuNgay.Text = "Từ ngày: ";
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblDenNgay.Location = new System.Drawing.Point(12, 82);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(104, 25);
            this.lblDenNgay.TabIndex = 10;
            this.lblDenNgay.Text = "Đến ngày: ";
            // 
            // btn_xuatExel
            // 
            this.btn_xuatExel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_xuatExel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btn_xuatExel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_xuatExel.FlatAppearance.BorderSize = 0;
            this.btn_xuatExel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_xuatExel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xuatExel.ForeColor = System.Drawing.Color.White;
            this.btn_xuatExel.Location = new System.Drawing.Point(1141, 31);
            this.btn_xuatExel.Name = "btn_xuatExel";
            this.btn_xuatExel.Size = new System.Drawing.Size(150, 40);
            this.btn_xuatExel.TabIndex = 25;
            this.btn_xuatExel.Text = "Xuất Excel";
            this.btn_xuatExel.UseVisualStyleBackColor = false;
            // 
            // btn_BaoCaoXuatHang
            // 
            this.btn_BaoCaoXuatHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BaoCaoXuatHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8209F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BaoCaoXuatHang.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_BaoCaoXuatHang.Location = new System.Drawing.Point(566, 90);
            this.btn_BaoCaoXuatHang.Name = "btn_BaoCaoXuatHang";
            this.btn_BaoCaoXuatHang.Size = new System.Drawing.Size(255, 42);
            this.btn_BaoCaoXuatHang.TabIndex = 5;
            this.btn_BaoCaoXuatHang.Text = "Thống kê xuất hàng";
            this.btn_BaoCaoXuatHang.UseVisualStyleBackColor = true;
            // 
            // btn_BaoCaoNhapHang
            // 
            this.btn_BaoCaoNhapHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BaoCaoNhapHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8209F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BaoCaoNhapHang.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_BaoCaoNhapHang.Location = new System.Drawing.Point(299, 90);
            this.btn_BaoCaoNhapHang.Name = "btn_BaoCaoNhapHang";
            this.btn_BaoCaoNhapHang.Size = new System.Drawing.Size(259, 42);
            this.btn_BaoCaoNhapHang.TabIndex = 4;
            this.btn_BaoCaoNhapHang.Text = "Thống kê nhập hàng";
            this.btn_BaoCaoNhapHang.UseVisualStyleBackColor = true;
            // 
            // btn_BaoCaoTonKho
            // 
            this.btn_BaoCaoTonKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BaoCaoTonKho.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8209F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_BaoCaoTonKho.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_BaoCaoTonKho.Location = new System.Drawing.Point(24, 90);
            this.btn_BaoCaoTonKho.Name = "btn_BaoCaoTonKho";
            this.btn_BaoCaoTonKho.Size = new System.Drawing.Size(254, 38);
            this.btn_BaoCaoTonKho.TabIndex = 3;
            this.btn_BaoCaoTonKho.Text = "Báo cáo tồn kho";
            this.btn_BaoCaoTonKho.UseVisualStyleBackColor = true;
            // 
            // btn_SanPhamBanChay
            // 
            this.btn_SanPhamBanChay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SanPhamBanChay.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8209F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SanPhamBanChay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_SanPhamBanChay.Location = new System.Drawing.Point(566, 46);
            this.btn_SanPhamBanChay.Name = "btn_SanPhamBanChay";
            this.btn_SanPhamBanChay.Size = new System.Drawing.Size(255, 38);
            this.btn_SanPhamBanChay.TabIndex = 2;
            this.btn_SanPhamBanChay.Text = "Sản phẩm bán chạy";
            this.btn_SanPhamBanChay.UseVisualStyleBackColor = true;
            // 
            // btn_DoanhThuThang
            // 
            this.btn_DoanhThuThang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DoanhThuThang.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8209F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DoanhThuThang.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_DoanhThuThang.Location = new System.Drawing.Point(299, 46);
            this.btn_DoanhThuThang.Name = "btn_DoanhThuThang";
            this.btn_DoanhThuThang.Size = new System.Drawing.Size(259, 38);
            this.btn_DoanhThuThang.TabIndex = 1;
            this.btn_DoanhThuThang.Text = "Doanh thu theo tháng";
            this.btn_DoanhThuThang.UseVisualStyleBackColor = true;
            // 
            // btn_DoanhThuNgay
            // 
            this.btn_DoanhThuNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DoanhThuNgay.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.8209F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DoanhThuNgay.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btn_DoanhThuNgay.Location = new System.Drawing.Point(24, 46);
            this.btn_DoanhThuNgay.Name = "btn_DoanhThuNgay";
            this.btn_DoanhThuNgay.Size = new System.Drawing.Size(254, 38);
            this.btn_DoanhThuNgay.TabIndex = 0;
            this.btn_DoanhThuNgay.Text = "Doanh thu theo ngày";
            this.btn_DoanhThuNgay.UseVisualStyleBackColor = true;
            // 
            // gb_BaoCao
            // 
            this.gb_BaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_BaoCao.Controls.Add(this.dataGridViewBaoCao);
            this.gb_BaoCao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.74627F, System.Drawing.FontStyle.Bold);
            this.gb_BaoCao.ForeColor = System.Drawing.SystemColors.Highlight;
            this.gb_BaoCao.Location = new System.Drawing.Point(34, 281);
            this.gb_BaoCao.Name = "gb_BaoCao";
            this.gb_BaoCao.Size = new System.Drawing.Size(1325, 399);
            this.gb_BaoCao.TabIndex = 4;
            this.gb_BaoCao.TabStop = false;
            this.gb_BaoCao.Text = "Báo cáo";
            // 
            // dataGridViewBaoCao
            // 
            this.dataGridViewBaoCao.AllowUserToAddRows = false;
            this.dataGridViewBaoCao.AllowUserToDeleteRows = false;
            this.dataGridViewBaoCao.AllowUserToResizeColumns = false;
            this.dataGridViewBaoCao.AllowUserToResizeRows = false;
            this.dataGridViewBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewBaoCao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBaoCao.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewBaoCao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.74627F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewBaoCao.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewBaoCao.ColumnHeadersHeight = 42;
            this.dataGridViewBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.74627F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewBaoCao.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewBaoCao.EnableHeadersVisualStyles = false;
            this.dataGridViewBaoCao.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dataGridViewBaoCao.Location = new System.Drawing.Point(6, 31);
            this.dataGridViewBaoCao.MultiSelect = false;
            this.dataGridViewBaoCao.Name = "dataGridViewBaoCao";
            this.dataGridViewBaoCao.ReadOnly = true;
            this.dataGridViewBaoCao.RowHeadersVisible = false;
            this.dataGridViewBaoCao.RowHeadersWidth = 51;
            this.dataGridViewBaoCao.RowTemplate.Height = 35;
            this.dataGridViewBaoCao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBaoCao.Size = new System.Drawing.Size(1313, 362);
            this.dataGridViewBaoCao.TabIndex = 14;
            // 
            // panelPaging
            // 
            this.panelPaging.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelPaging.Controls.Add(this.btnLastPage);
            this.panelPaging.Controls.Add(this.btnNextPage);
            this.panelPaging.Controls.Add(this.lblPageInfo);
            this.panelPaging.Controls.Add(this.btnPreviousPage);
            this.panelPaging.Controls.Add(this.btnFirstPage);
            this.panelPaging.Location = new System.Drawing.Point(420, 692);
            this.panelPaging.Margin = new System.Windows.Forms.Padding(4);
            this.panelPaging.Name = "panelPaging";
            this.panelPaging.Size = new System.Drawing.Size(560, 50);
            this.panelPaging.TabIndex = 22;
            // 
            // btnLastPage
            // 
            this.btnLastPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(210)))));
            this.btnLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLastPage.FlatAppearance.BorderSize = 0;
            this.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLastPage.ForeColor = System.Drawing.Color.White;
            this.btnLastPage.Location = new System.Drawing.Point(442, 6);
            this.btnLastPage.Margin = new System.Windows.Forms.Padding(4);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(114, 35);
            this.btnLastPage.TabIndex = 4;
            this.btnLastPage.Text = "Trang cuối";
            this.btnLastPage.UseVisualStyleBackColor = false;
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(210)))));
            this.btnNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextPage.FlatAppearance.BorderSize = 0;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.ForeColor = System.Drawing.Color.White;
            this.btnNextPage.Location = new System.Drawing.Point(328, 6);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(4);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(100, 35);
            this.btnNextPage.TabIndex = 3;
            this.btnNextPage.Text = "Trang sau";
            this.btnNextPage.UseVisualStyleBackColor = false;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPageInfo.Location = new System.Drawing.Point(255, 12);
            this.lblPageInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(46, 25);
            this.lblPageInfo.TabIndex = 2;
            this.lblPageInfo.Text = "1 / 1";
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(210)))));
            this.btnPreviousPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviousPage.FlatAppearance.BorderSize = 0;
            this.btnPreviousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPreviousPage.ForeColor = System.Drawing.Color.White;
            this.btnPreviousPage.Location = new System.Drawing.Point(127, 6);
            this.btnPreviousPage.Margin = new System.Windows.Forms.Padding(4);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(120, 35);
            this.btnPreviousPage.TabIndex = 1;
            this.btnPreviousPage.Text = "Trang trước";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(210)))));
            this.btnFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirstPage.FlatAppearance.BorderSize = 0;
            this.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirstPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnFirstPage.ForeColor = System.Drawing.Color.White;
            this.btnFirstPage.Location = new System.Drawing.Point(13, 6);
            this.btnFirstPage.Margin = new System.Windows.Forms.Padding(4);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(100, 35);
            this.btnFirstPage.TabIndex = 0;
            this.btnFirstPage.Text = "Trang đầu";
            this.btnFirstPage.UseVisualStyleBackColor = false;
            // 
            // FormBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 755);
            this.Controls.Add(this.panelPaging);
            this.Controls.Add(this.gb_BaoCao);
            this.Controls.Add(this.gb_chonBaoCao);
            this.Controls.Add(this.panelHeader);
            this.Name = "FormBaoCao";
            this.Text = "Form1";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.gb_chonBaoCao.ResumeLayout(false);
            this.gb_chonBaoCao.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gb_BaoCao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBaoCao)).EndInit();
            this.panelPaging.ResumeLayout(false);
            this.panelPaging.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox gb_chonBaoCao;
        private System.Windows.Forms.GroupBox gb_BaoCao;
        private System.Windows.Forms.DataGridView dataGridViewBaoCao;
        private System.Windows.Forms.Button btn_BaoCaoTonKho;
        private System.Windows.Forms.Button btn_SanPhamBanChay;
        private System.Windows.Forms.Button btn_DoanhThuThang;
        private System.Windows.Forms.Button btn_DoanhThuNgay;
        private System.Windows.Forms.Button btn_BaoCaoNhapHang;
        private System.Windows.Forms.Button btn_BaoCaoXuatHang;
        private System.Windows.Forms.Button btn_xuatExel;
        private System.Windows.Forms.Panel panelPaging;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_LocNgay;
        private System.Windows.Forms.Label lblReportSummary;
    }
}