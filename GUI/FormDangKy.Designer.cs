namespace GUI
{
    partial class FormDangKy
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
            this.label_taikhoan = new System.Windows.Forms.Label();
            this.label_matkhau = new System.Windows.Forms.Label();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtXacNhanMatKhau = new System.Windows.Forms.TextBox();
            this.label_xacnhanmatkhau = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label_email = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label_Hovaten = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkDangNhap = new System.Windows.Forms.LinkLabel();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_taikhoan
            // 
            this.label_taikhoan.AutoSize = true;
            this.label_taikhoan.Location = new System.Drawing.Point(51, 151);
            this.label_taikhoan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_taikhoan.Name = "label_taikhoan";
            this.label_taikhoan.Size = new System.Drawing.Size(113, 25);
            this.label_taikhoan.TabIndex = 0;
            this.label_taikhoan.Text = "Tài khoản:";
            // 
            // label_matkhau
            // 
            this.label_matkhau.AutoSize = true;
            this.label_matkhau.Location = new System.Drawing.Point(57, 215);
            this.label_matkhau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_matkhau.Name = "label_matkhau";
            this.label_matkhau.Size = new System.Drawing.Size(107, 25);
            this.label_matkhau.TabIndex = 1;
            this.label_matkhau.Text = "Mật khẩu:";
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Location = new System.Drawing.Point(242, 148);
            this.txtTaiKhoan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaiKhoan.Multiline = true;
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(331, 48);
            this.txtTaiKhoan.TabIndex = 9;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(242, 209);
            this.txtMatKhau.Margin = new System.Windows.Forms.Padding(4);
            this.txtMatKhau.Multiline = true;
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(331, 45);
            this.txtMatKhau.TabIndex = 11;
            // 
            // btnDangKy
            // 
            this.btnDangKy.Location = new System.Drawing.Point(301, 384);
            this.btnDangKy.Margin = new System.Windows.Forms.Padding(4);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(147, 47);
            this.btnDangKy.TabIndex = 4;
            this.btnDangKy.Text = "Đăng ký";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8806F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(434, 45);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 44);
            this.label3.TabIndex = 7;
            this.label3.Text = "Đăng ký hệ thống";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtXacNhanMatKhau);
            this.groupBox3.Controls.Add(this.label_xacnhanmatkhau);
            this.groupBox3.Controls.Add(this.txtEmail);
            this.groupBox3.Controls.Add(this.label_email);
            this.groupBox3.Controls.Add(this.txtHoTen);
            this.groupBox3.Controls.Add(this.label_Hovaten);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.linkDangNhap);
            this.groupBox3.Controls.Add(this.txtMatKhau);
            this.groupBox3.Controls.Add(this.label_taikhoan);
            this.groupBox3.Controls.Add(this.label_matkhau);
            this.groupBox3.Controls.Add(this.txtTaiKhoan);
            this.groupBox3.Controls.Add(this.btnDangKy);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(187, 107);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(772, 452);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(242, 267);
            this.txtXacNhanMatKhau.Margin = new System.Windows.Forms.Padding(4);
            this.txtXacNhanMatKhau.Multiline = true;
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(331, 45);
            this.txtXacNhanMatKhau.TabIndex = 13;
            // 
            // label_xacnhanmatkhau
            // 
            this.label_xacnhanmatkhau.AutoSize = true;
            this.label_xacnhanmatkhau.Location = new System.Drawing.Point(57, 273);
            this.label_xacnhanmatkhau.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_xacnhanmatkhau.Name = "label_xacnhanmatkhau";
            this.label_xacnhanmatkhau.Size = new System.Drawing.Size(191, 25);
            this.label_xacnhanmatkhau.TabIndex = 12;
            this.label_xacnhanmatkhau.Text = "Nhập lại mật khẩu:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(242, 92);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(331, 48);
            this.txtEmail.TabIndex = 3;
            // 
            // label_email
            // 
            this.label_email.AutoSize = true;
            this.label_email.Location = new System.Drawing.Point(57, 98);
            this.label_email.Name = "label_email";
            this.label_email.Size = new System.Drawing.Size(65, 25);
            this.label_email.TabIndex = 10;
            this.label_email.Text = "Email";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(242, 40);
            this.txtHoTen.Multiline = true;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(331, 48);
            this.txtHoTen.TabIndex = 2;
            // 
            // label_Hovaten
            // 
            this.label_Hovaten.AutoSize = true;
            this.label_Hovaten.Location = new System.Drawing.Point(57, 43);
            this.label_Hovaten.Name = "label_Hovaten";
            this.label_Hovaten.Size = new System.Drawing.Size(117, 25);
            this.label_Hovaten.TabIndex = 8;
            this.label_Hovaten.Text = "Họ và Tên:\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Bạn đã tài khoản?";
            // 
            // linkDangNhap
            // 
            this.linkDangNhap.AutoSize = true;
            this.linkDangNhap.Location = new System.Drawing.Point(403, 341);
            this.linkDangNhap.Name = "linkDangNhap";
            this.linkDangNhap.Size = new System.Drawing.Size(170, 25);
            this.linkDangNhap.TabIndex = 6;
            this.linkDangNhap.TabStop = true;
            this.linkDangNhap.Text = "Đăng nhập ngay";
            this.linkDangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDangNhap_Clicked);
            // 
            // FormDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 611);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng ký";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_taikhoan;
        private System.Windows.Forms.Label label_matkhau;
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkDangNhap;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label_Hovaten;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label_email;
        private System.Windows.Forms.TextBox txtXacNhanMatKhau;
        private System.Windows.Forms.Label label_xacnhanmatkhau;
    }
}