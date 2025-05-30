# 🏍️ HỆ THỐNG QUẢN LÝ CỬA HÀNG XE MÁY

Một phần mềm quản lý toàn diện cho các đại lý bán xe máy, được phát triển bằng **C# WinForms** trên kiến trúc 3 lớp (GUI - BUS - DAO), kết nối với SQL Server và hỗ trợ xuất báo cáo chuyên nghiệp.

![WinForms App](https://img.shields.io/badge/Platform-Windows-blue?logo=windows)
![.NET Framework](https://img.shields.io/badge/.NET-Framework%204.7.2-blueviolet)
![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)

## 🖼️ ẢNH DEMO GIAO DIỆN
![Ảnh chụp màn hình 2025-05-21 013716](https://github.com/user-attachments/assets/712ad841-c2f1-4072-ba66-1da1972da3b4)
![Ảnh chụp màn hình 2025-05-21 013617](https://github.com/user-attachments/assets/a68026fb-d8d2-483c-9790-02e0be760539)


## ✨ TÍNH NĂNG NỔI BậT

### 📆 Quản Lý Bán Hàng

* Thực hiện giao dịch bán xe
* Quản lý khách hàng
* Xuất hóa đơn, phiếu thu
* Tự động trừ kho sau khi bán

### 🏪 Quản Lý Kho Hàng

* Theo dõi số lượng xe trong kho
* Quản lý nhà cung cấp
* Nhập hàng và xử lý đơn đặt
* Cảnh báo tồn kho thấp

### 📊 Báo Cáo & Thống Kê

* Báo cáo doanh số theo ngày, tháng, năm
* Thống kê tồn kho
* Phân tích doanh thu
* Lọc top mẫu xe bán chạy

### 👤 Quản Lý Người Dùng

* Phân quyền theo vai trò (Quản lý, Nhân viên)
* Quản lý tài khoản nhân viên
* Đổi mật khẩu, thiết lập bảo mật

## 🔧 CÔNG NGHỆ SỬ DỤNG

| Thành phần    | Công nghệ                          |
| ------------- | ---------------------------------- |
| Giao diện     | Windows Forms (.NET Framework)     |
| Ngôn ngữ      | C#                                 |
| Cơ sở dữ liệu | SQL Server                         |
| Báo cáo       | PdfSharp, EPPlus (xuất PDF, Excel) |
| Kiến trúc     | 3 lớp: GUI - BUS - DAO             |

## 📁 CẤU TRÚc DỰ ÁN

```
/GUI         → Giao diện người dùng (Form chính, form con)
/BUS         → Xử lý nghiệp vụ (business logic)
/DAO         → Truy cập dữ liệu từ CSDL
/DTO         → Đối tượng truyền dữ liệu
/Reports     → In hóa đơn, báo cáo PDF hoặc Excel
/App.config  → Cấu hình chuỗi kết nối SQL Server
```

## 🚀 HƯỚNG DẪN CÀI ĐẶT

1. **Clone dự án:**

```bash
git clone https://github.com/tambl2004/WinForm.Net.git
```

2. **Khôi phục gói NuGet:**

   * Mở Visual Studio
   * Vào `Tools → NuGet Package Manager → Restore Packages`

3. **Khởi tạo cơ sở dữ liệu:**

   * Mở `xemay.sql` trong SQL Server Management Studio
   * Thực thi script để tạo database

4. **Cập nhật chuỗi kết nối:**

   * Mở `App.config`, thay đổi `Data Source` và `Initial Catalog` nếu cần

5. **Build & Run:**

   * Nhấn `F5` trong Visual Studio để chạy chương trình

## 🧪 YÊU CẦU HỆ THỐNG

* Windows 10 hoặc mới hơn
* Visual Studio 2019/2022
* .NET Framework 4.7.2 trở lên
* SQL Server Express hoặc Developer Edition

## 📍 GIẤY PHÉP

Phần mềm này được phát hành theo giấy phép [MIT License](LICENSE).

## 👥 NGƯỜI PHÁT TRIỂN

* 👨‍💼 **Đào Văn Tâm**


## 🤝 GÓP Ý & ĐÓNG GÓP

Chúng tôi luôn hoan nghênh các đóng góp từ cộng đồng. Hãy gửi Pull Request hoặc tạo Issue tại:

👉 [GitHub Issues](https://github.com/tambl2004/WinForm.Net/issues)

## 📢 LIÊN HỆ

* 📧 Email: [vantamst97@gmail.com](mailto:vantamst97@gmail.com)

> **© 2025 – Hệ thống quản lý cửa hàng xe máy – Phát triển bởi Đào Văn Tâm**
