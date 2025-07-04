-- Tạo cơ sở dữ liệu
CREATE DATABASE QuanLyCuaHangXeMay
GO

USE QuanLyCuaHangXeMay
GO

-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    MaNV varchar(10) PRIMARY KEY,
    HoTen nvarchar(100) NOT NULL,
    GioiTinh nvarchar(10) CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    NgaySinh date,
    DiaChi nvarchar(200),
    DienThoai varchar(15),
    Email varchar(100),
    ChucVu nvarchar(50),
    TenDangNhap varchar(50) UNIQUE,
    MatKhau varchar(50),
    TrangThai bit DEFAULT 1,
    HinhAnh VARBINARY(MAX) NULL
)
GO

-- Tạo bảng KhachHang
CREATE TABLE KhachHang (
    MaKH varchar(10) PRIMARY KEY,
    HoTen nvarchar(100) NOT NULL,
    GioiTinh nvarchar(10) CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    NgaySinh date,
    DiaChi nvarchar(200),
    DienThoai varchar(15),
    Email varchar(100),
    CMND varchar(20),
    NgayDangKy date DEFAULT GETDATE(),
    TrangThai bit DEFAULT 1
)
GO

-- Tạo bảng NhaCungCap
CREATE TABLE NhaCungCap (
    MaNCC varchar(10) PRIMARY KEY,
    TenNCC nvarchar(100) NOT NULL,
    DiaChi nvarchar(200),
    DienThoai varchar(15),
    Email varchar(100),
    NguoiLienHe nvarchar(100),
    TrangThai bit DEFAULT 1
)
GO

-- Tạo bảng XeMay
CREATE TABLE XeMay (
    MaXe varchar(10) PRIMARY KEY,
    TenXe nvarchar(100) NOT NULL,
    HangXe nvarchar(100),
    DongXe nvarchar(100),
    MauSac nvarchar(50),
    NamSX int,
    SoKhung varchar(50) UNIQUE,
    SoMay varchar(50) UNIQUE,
    GiaNhap decimal(18,0),
    GiaBan decimal(18,0),
    SoLuongTon int DEFAULT 0,
    HinhAnh VARBINARY(MAX) NULL,
    MoTa nvarchar(500),
    TrangThai bit DEFAULT 1
)
GO

-- Tạo bảng PhieuNhap
CREATE TABLE PhieuNhap (
    MaPN varchar(10) PRIMARY KEY,
    NgayNhap datetime DEFAULT GETDATE(),
    MaNCC varchar(10) REFERENCES NhaCungCap(MaNCC),
    MaNV varchar(10) REFERENCES NhanVien(MaNV),
    TongTien decimal(18,0) DEFAULT 0,
    GhiChu nvarchar(500),
    TrangThai bit DEFAULT 1
)
GO

-- Tạo bảng ChiTietPhieuNhap
CREATE TABLE ChiTietPhieuNhap (
    MaPN varchar(10) REFERENCES PhieuNhap(MaPN),
    MaXe varchar(10) REFERENCES XeMay(MaXe),
    SoLuong int DEFAULT 1,
    GiaNhap decimal(18,0),
    ThanhTien decimal(18,0),
    GhiChu nvarchar(500),
    PRIMARY KEY (MaPN, MaXe)
)
GO

-- Tạo bảng HoaDon
CREATE TABLE HoaDon (
    MaHD varchar(10) PRIMARY KEY,
    NgayBan datetime DEFAULT GETDATE(),
    MaKH varchar(10) REFERENCES KhachHang(MaKH),
    MaNV varchar(10) REFERENCES NhanVien(MaNV),
    TongTien decimal(18,0) DEFAULT 0,
    GiamGia decimal(18,0) DEFAULT 0,
    ThanhToan decimal(18,0) DEFAULT 0,
    GhiChu nvarchar(500),
    TrangThai bit DEFAULT 1
)
GO

-- Tạo bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaHD varchar(10) REFERENCES HoaDon(MaHD),
    MaXe varchar(10) REFERENCES XeMay(MaXe),
    SoLuong int DEFAULT 1,
    GiaBan decimal(18,0),
    GiamGia decimal(18,0) DEFAULT 0,
    ThanhTien decimal(18,0),
    GhiChu nvarchar(500),
    PRIMARY KEY (MaHD, MaXe)
)
GO

-- Tạo các Trigger

-- Trigger cập nhật tổng tiền phiếu nhập
CREATE TRIGGER trg_CapNhatTongTienPhieuNhap
ON ChiTietPhieuNhap
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @MaPN varchar(10)
    
    -- Lấy MaPN từ bảng inserted
    IF EXISTS (SELECT * FROM inserted)
        SELECT @MaPN = MaPN FROM inserted
    ELSE
        SELECT @MaPN = MaPN FROM deleted
    
    -- Cập nhật TongTien trong PhieuNhap
    UPDATE PhieuNhap
    SET TongTien = (
        SELECT SUM(ThanhTien)
        FROM ChiTietPhieuNhap
        WHERE MaPN = @MaPN
    )
    WHERE MaPN = @MaPN
END
GO

-- Trigger cập nhật tổng tiền hóa đơn
CREATE TRIGGER trg_CapNhatTongTienHoaDon
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @MaHD varchar(10)
    
    -- Lấy MaHD từ bảng inserted
    IF EXISTS (SELECT * FROM inserted)
        SELECT @MaHD = MaHD FROM inserted
    ELSE
        SELECT @MaHD = MaHD FROM deleted
    
    -- Cập nhật TongTien, ThanhToan trong HoaDon
    UPDATE HoaDon
    SET TongTien = (
        SELECT SUM(ThanhTien)
        FROM ChiTietHoaDon
        WHERE MaHD = @MaHD
    ),
    ThanhToan = (
        SELECT SUM(ThanhTien)
        FROM ChiTietHoaDon
        WHERE MaHD = @MaHD
    ) - GiamGia
    WHERE MaHD = @MaHD
END
GO


GO

-- Trigger cập nhật số lượng tồn kho khi bán xe
CREATE TRIGGER trg_CapNhatSoLuongTonKhi_Ban
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    DECLARE @MaXe varchar(10), @SoLuongMoi int, @SoLuongCu int

    -- Xử lý khi có thêm mới hoặc cập nhật
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        -- Lấy thông tin từ bảng inserted
        SELECT @MaXe = MaXe, @SoLuongMoi = SoLuong FROM inserted

        -- Lấy thông tin từ bảng deleted nếu là cập nhật
        IF EXISTS (SELECT * FROM deleted)
        BEGIN
            SELECT @SoLuongCu = SoLuong FROM deleted
            -- Cập nhật số lượng tồn
            UPDATE XeMay
            SET SoLuongTon = SoLuongTon - @SoLuongMoi + @SoLuongCu
            WHERE MaXe = @MaXe
        END
        ELSE
        BEGIN
            -- Thêm mới, giảm số lượng tồn
            UPDATE XeMay
            SET SoLuongTon = SoLuongTon - @SoLuongMoi
            WHERE MaXe = @MaXe
        END
    END
    ELSE
    BEGIN
        -- Xử lý khi xóa
        SELECT @MaXe = MaXe, @SoLuongCu = SoLuong FROM deleted
        -- Tăng số lượng tồn
        UPDATE XeMay
        SET SoLuongTon = SoLuongTon + @SoLuongCu
        WHERE MaXe = @MaXe
    END
END
GO

-- Tạo các Stored Procedure

-- SP thêm hóa đơn
CREATE PROCEDURE sp_ThemHoaDon
    @MaHD NVARCHAR(10),
    @NgayBan DATE,
    @MaKH NVARCHAR(10),
    @MaNV NVARCHAR(10),
    @TongTien DECIMAL(18,2),
    @GiamGia DECIMAL(18,2),
    @ThanhToan DECIMAL(18,2),
    @GhiChu NVARCHAR(200),
    @TrangThai BIT
AS
BEGIN
    INSERT INTO HoaDon (MaHD, NgayBan, MaKH, MaNV, TongTien, GiamGia, ThanhToan, GhiChu, TrangThai)
    VALUES (@MaHD, @NgayBan, @MaKH, @MaNV, @TongTien, @GiamGia, @ThanhToan, @GhiChu, @TrangThai)
END
GO

-- SP thêm chi tiết hóa đơn
CREATE PROCEDURE sp_ThemChiTietHoaDon
    @MaHD NVARCHAR(10),
    @MaXe NVARCHAR(10),
    @SoLuong INT,
    @GiaBan DECIMAL(18,2),
    @GiamGia DECIMAL(18,2),
    @ThanhTien DECIMAL(18,2),
    @GhiChu NVARCHAR(200)
AS
BEGIN
    INSERT INTO ChiTietHoaDon (MaHD, MaXe, SoLuong, GiaBan, GiamGia, ThanhTien, GhiChu)
    VALUES (@MaHD, @MaXe, @SoLuong, @GiaBan, @GiamGia, @ThanhTien, @GhiChu)
END
GO

-- SP tìm kiếm xe máy theo nhiều tiêu chí
CREATE PROCEDURE sp_TimKiemXeMay
    @MaXe varchar(10) = NULL,
    @TenXe nvarchar(100) = NULL,
    @HangXe nvarchar(100) = NULL,
    @DongXe nvarchar(100) = NULL,
    @MauSac nvarchar(50) = NULL,
    @GiaTu decimal(18,0) = NULL,
    @GiaDen decimal(18,0) = NULL
AS
BEGIN
    SELECT x.MaXe, x.TenXe, x.HangXe, x.DongXe, x.MauSac, x.NamSX, 
           x.SoKhung, x.SoMay, x.GiaNhap, x.GiaBan, x.SoLuongTon
    FROM XeMay x
    WHERE (@MaXe IS NULL OR x.MaXe LIKE '%' + @MaXe + '%')
      AND (@TenXe IS NULL OR x.TenXe LIKE N'%' + @TenXe + N'%')
      AND (@HangXe IS NULL OR x.HangXe = @HangXe)
      AND (@DongXe IS NULL OR x.DongXe = @DongXe)
      AND (@MauSac IS NULL OR x.MauSac LIKE N'%' + @MauSac + N'%')
      AND (@GiaTu IS NULL OR x.GiaBan >= @GiaTu)
      AND (@GiaDen IS NULL OR x.GiaBan <= @GiaDen)
      AND x.TrangThai = 1
END
GO

-- SP báo cáo doanh thu theo khoảng thời gian
CREATE PROCEDURE sp_BaoCaoDoanhThu
    @TuNgay datetime,
    @DenNgay datetime
AS
BEGIN
    SELECT 
        CONVERT(varchar(10), hd.NgayBan, 103) AS NgayBan,
        COUNT(DISTINCT hd.MaHD) AS SoHoaDon,
        SUM(hd.TongTien) AS TongDoanhThu,
        SUM(hd.GiamGia) AS TongGiamGia,
        SUM(hd.ThanhToan) AS DoanhThuThuc
    FROM HoaDon hd
    WHERE hd.NgayBan BETWEEN @TuNgay AND @DenNgay
      AND hd.TrangThai = 1
    GROUP BY CONVERT(varchar(10), hd.NgayBan, 103)
    ORDER BY CONVERT(varchar(10), hd.NgayBan, 103)
END
GO

-- SP báo cáo tồn kho
CREATE PROCEDURE sp_BaoCaoTonKho
AS
BEGIN
    SELECT 
        x.MaXe,
        x.TenXe,
        x.HangXe,
        x.DongXe,
        x.MauSac,
        x.NamSX,
        x.SoLuongTon,
        x.GiaNhap,
        x.GiaBan,
        (x.SoLuongTon * x.GiaNhap) AS GiaTriTon
    FROM XeMay x
    WHERE x.TrangThai = 1
    ORDER BY x.SoLuongTon DESC
END
GO

-- SP báo cáo xe bán chạy
CREATE PROCEDURE sp_BaoCaoXeBanChay
    @TuNgay datetime,
    @DenNgay datetime,
    @Top int = 10
AS
BEGIN
    SELECT TOP (@Top)
        x.MaXe,
        x.TenXe,
        x.HangXe,
        x.DongXe,
        SUM(ct.SoLuong) AS TongSoLuongBan,
        AVG(ct.GiaBan) AS GiaBanTrungBinh,
        SUM(ct.ThanhTien) AS TongDoanhThu
    FROM ChiTietHoaDon ct
    INNER JOIN HoaDon hd ON ct.MaHD = hd.MaHD
    INNER JOIN XeMay x ON ct.MaXe = x.MaXe
    WHERE hd.NgayBan BETWEEN @TuNgay AND @DenNgay
      AND hd.TrangThai = 1
    GROUP BY x.MaXe, x.TenXe, x.HangXe, x.DongXe
    ORDER BY TongSoLuongBan DESC
END
GO

-- SP thống kê lợi nhuận
CREATE PROCEDURE sp_ThongKeLuongNhuanTheoThang
    @Nam int
AS
BEGIN
    SELECT 
        MONTH(hd.NgayBan) AS Thang,
        SUM(ct.ThanhTien) AS DoanhThu,
        SUM(ct.SoLuong * x.GiaNhap) AS GiaVon,
        SUM(ct.ThanhTien) - SUM(ct.SoLuong * x.GiaNhap) AS LoiNhuan
    FROM HoaDon hd
    INNER JOIN ChiTietHoaDon ct ON hd.MaHD = ct.MaHD
    INNER JOIN XeMay x ON ct.MaXe = x.MaXe
    WHERE YEAR(hd.NgayBan) = @Nam
      AND hd.TrangThai = 1
    GROUP BY MONTH(hd.NgayBan)
    ORDER BY MONTH(hd.NgayBan)
END
GO

-- SP kiểm tra đăng nhập
CREATE PROCEDURE sp_KiemTraDangNhap
    @TenDangNhap varchar(50),
    @MatKhau varchar(50)
AS
BEGIN
    SELECT MaNV, HoTen, ChucVu
    FROM NhanVien
    WHERE TenDangNhap = @TenDangNhap 
      AND MatKhau = @MatKhau
      AND TrangThai = 1
END
GO


-- SP thêm nhân viên
CREATE PROCEDURE sp_ThemNhanVien
    @MaNV varchar(10),
    @HoTen nvarchar(100),
    @GioiTinh nvarchar(10),
    @NgaySinh date,
    @DiaChi nvarchar(200),
    @DienThoai varchar(15),
    @Email varchar(100),
    @ChucVu nvarchar(50),
    @TenDangNhap varchar(50),
    @MatKhau varchar(50),
    @HinhAnh VARBINARY(MAX) = NULL
AS
BEGIN
    INSERT INTO NhanVien(MaNV, HoTen, GioiTinh, NgaySinh, DiaChi, DienThoai, Email, ChucVu, TenDangNhap, MatKhau, HinhAnh)
    VALUES(@MaNV, @HoTen, @GioiTinh, @NgaySinh, @DiaChi, @DienThoai, @Email, @ChucVu, @TenDangNhap, @MatKhau, @HinhAnh)
END
GO

-- SP sửa nhân viên
CREATE PROCEDURE sp_SuaNhanVien
    @MaNV varchar(10),
    @HoTen nvarchar(100),
    @GioiTinh nvarchar(10),
    @NgaySinh date,
    @DiaChi nvarchar(200),
    @DienThoai varchar(15),
    @Email varchar(100),
    @ChucVu nvarchar(50),
    @TenDangNhap varchar(50),
    @MatKhau varchar(50),
    @TrangThai bit,
    @HinhAnh VARBINARY(MAX) = NULL
AS
BEGIN
    UPDATE NhanVien
    SET HoTen = @HoTen,
        GioiTinh = @GioiTinh,
        NgaySinh = @NgaySinh,
        DiaChi = @DiaChi,
        DienThoai = @DienThoai,
        Email = @Email,
        ChucVu = @ChucVu,
        TenDangNhap = @TenDangNhap,
        MatKhau = @MatKhau,
        TrangThai = @TrangThai,
        HinhAnh = CASE WHEN @HinhAnh IS NULL THEN HinhAnh ELSE @HinhAnh END
    WHERE MaNV = @MaNV
END
GO

-- SP thêm khách hàng
CREATE PROCEDURE sp_ThemKhachHang
    @MaKH varchar(10),
    @HoTen nvarchar(100),
    @GioiTinh nvarchar(10),
    @NgaySinh date,
    @DiaChi nvarchar(200),
    @DienThoai varchar(15),
    @Email varchar(100),
    @CMND varchar(20)
AS
BEGIN
    INSERT INTO KhachHang(MaKH, HoTen, GioiTinh, NgaySinh, DiaChi, DienThoai, Email, CMND, NgayDangKy)
    VALUES(@MaKH, @HoTen, @GioiTinh, @NgaySinh, @DiaChi, @DienThoai, @Email, @CMND, GETDATE())
END
GO

-- SP sửa khách hàng
CREATE PROCEDURE sp_SuaKhachHang
    @MaKH varchar(10),
    @HoTen nvarchar(100),
    @GioiTinh nvarchar(10),
    @NgaySinh date,
    @DiaChi nvarchar(200),
    @DienThoai varchar(15),
    @Email varchar(100),
    @CMND varchar(20),
    @TrangThai bit
AS
BEGIN
    UPDATE KhachHang
    SET HoTen = @HoTen,
        GioiTinh = @GioiTinh,
        NgaySinh = @NgaySinh,
        DiaChi = @DiaChi,
        DienThoai = @DienThoai,
        Email = @Email,
        CMND = @CMND,
        TrangThai = @TrangThai
    WHERE MaKH = @MaKH
END
GO

-- SP thêm nhà cung cấp
CREATE PROCEDURE sp_ThemNhaCungCap
    @MaNCC varchar(10),
    @TenNCC nvarchar(100),
    @DiaChi nvarchar(200),
    @DienThoai varchar(15),
    @Email varchar(100),
    @NguoiLienHe nvarchar(100)
AS
BEGIN
    INSERT INTO NhaCungCap(MaNCC, TenNCC, DiaChi, DienThoai, Email, NguoiLienHe)
    VALUES(@MaNCC, @TenNCC, @DiaChi, @DienThoai, @Email, @NguoiLienHe)
END
GO

-- SP sửa nhà cung cấp
CREATE PROCEDURE sp_SuaNhaCungCap
    @MaNCC varchar(10),
    @TenNCC nvarchar(100),
    @DiaChi nvarchar(200),
    @DienThoai varchar(15),
    @Email varchar(100),
    @NguoiLienHe nvarchar(100),
    @TrangThai bit
AS
BEGIN
    UPDATE NhaCungCap
    SET TenNCC = @TenNCC,
        DiaChi = @DiaChi,
        DienThoai = @DienThoai,
        Email = @Email,
        NguoiLienHe = @NguoiLienHe,
        TrangThai = @TrangThai
    WHERE MaNCC = @MaNCC
END
GO

-- SP thêm xe máy
CREATE PROCEDURE sp_ThemXeMay
    @MaXe varchar(10),
    @TenXe nvarchar(100),
    @HangXe nvarchar(100),
    @DongXe nvarchar(100),
    @MauSac nvarchar(50),
    @NamSX int,
    @SoKhung varchar(50),
    @SoMay varchar(50),
    @GiaNhap decimal(18,0),
    @GiaBan decimal(18,0),
    @HinhAnh VARBINARY(MAX),
    @MoTa nvarchar(500)
AS
BEGIN
    INSERT INTO XeMay(MaXe, TenXe, HangXe, DongXe, MauSac, NamSX, SoKhung, SoMay, GiaNhap, GiaBan, HinhAnh, MoTa)
    VALUES(@MaXe, @TenXe, @HangXe, @DongXe, @MauSac, @NamSX, @SoKhung, @SoMay, @GiaNhap, @GiaBan, @HinhAnh, @MoTa)
END
GO

-- SP sửa xe máy
CREATE PROCEDURE sp_SuaXeMay
    @MaXe varchar(10),
    @TenXe nvarchar(100),
    @HangXe nvarchar(100),
    @DongXe nvarchar(100),
    @MauSac nvarchar(50),
    @NamSX int,
    @SoKhung varchar(50),
    @SoMay varchar(50),
    @GiaNhap decimal(18,0),
    @GiaBan decimal(18,0),
    @SoLuongTon int,
    @HinhAnh VARBINARY(MAX),
    @MoTa nvarchar(500),
    @TrangThai bit
AS
BEGIN
    UPDATE XeMay
    SET TenXe = @TenXe,
        HangXe = @HangXe,
        DongXe = @DongXe,
        MauSac = @MauSac,
        NamSX = @NamSX,
        SoKhung = @SoKhung,
        SoMay = @SoMay,
        GiaNhap = @GiaNhap,
        GiaBan = @GiaBan,
        SoLuongTon = @SoLuongTon,
        HinhAnh = CASE WHEN @HinhAnh IS NULL THEN HinhAnh ELSE @HinhAnh END,
        MoTa = @MoTa,
        TrangThai = @TrangThai
    WHERE MaXe = @MaXe
END
GO

ALTER PROCEDURE sp_ThemPhieuNhap
    @MaPN varchar(10),
    @NgayNhap datetime,
    @MaNCC varchar(10),
    @MaNV varchar(10),
    @GhiChu nvarchar(500)
AS
BEGIN
    INSERT INTO PhieuNhap(MaPN, NgayNhap, MaNCC, MaNV, GhiChu)
    VALUES(@MaPN, @NgayNhap, @MaNCC, @MaNV, @GhiChu)
END
GO

-- SP thêm chi tiết phiếu nhập
CREATE PROCEDURE [dbo].[sp_ThemChiTietPhieuNhap]
    @MaPN varchar(10),
    @MaXe varchar(10),
    @SoLuong int,
    @GiaNhap decimal(18,0),
    @GhiChu nvarchar(500)
AS
BEGIN
    DECLARE @ThanhTien decimal(18,0)
    SET @ThanhTien = @SoLuong * @GiaNhap
    
    INSERT INTO ChiTietPhieuNhap(MaPN, MaXe, SoLuong, GiaNhap, ThanhTien, GhiChu)
    VALUES(@MaPN, @MaXe, @SoLuong, @GiaNhap, @ThanhTien, @GhiChu)
    
    -- Cập nhật giá nhập và số lượng tồn cho xe máy
    UPDATE XeMay
    SET GiaNhap = @GiaNhap,
        SoLuongTon = SoLuongTon + @SoLuong
    WHERE MaXe = @MaXe
END
GO

GO

CREATE TRIGGER sp_CapNhatTongTienHoaDon
ON ChiTietHoaDon
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Cập nhật TongTien và ThanhToan trong HoaDon cho tất cả MaHD bị ảnh hưởng
    UPDATE HoaDon
    SET TongTien = ISNULL((
        SELECT SUM(ThanhTien)
        FROM ChiTietHoaDon ct
        WHERE ct.MaHD = HoaDon.MaHD
    ), 0),
    ThanhToan = ISNULL((
        SELECT SUM(ThanhTien)
        FROM ChiTietHoaDon ct
        WHERE ct.MaHD = HoaDon.MaHD
    ), 0) - ISNULL(GiamGia, 0)
    WHERE MaHD IN (
        SELECT MaHD FROM inserted
        UNION
        SELECT MaHD FROM deleted
    )
END
GO



-- triger cập nhật phiếu nhập
CREATE PROCEDURE sp_CapNhatPhieuNhap
    @MaPN NVARCHAR(10),
    @NgayNhap DATETIME,
    @MaNCC NVARCHAR(10),
    @MaNV NVARCHAR(10),
    @GhiChu NVARCHAR(500),
    @TongTien DECIMAL(18, 2),
    @TrangThai BIT
AS
BEGIN
    UPDATE PhieuNhap
    SET NgayNhap = @NgayNhap,
        MaNCC = @MaNCC,
        MaNV = @MaNV,
        GhiChu = @GhiChu,
        TongTien = @TongTien,
        TrangThai = @TrangThai
    WHERE MaPN = @MaPN;
END

-- SP báo cáo nhập hàng
CREATE PROCEDURE sp_BaoCaoNhapHang
    @TuNgay datetime,
    @DenNgay datetime
AS
BEGIN
    SELECT 
        CONVERT(varchar(10), pn.NgayNhap, 103) AS NgayNhap,
        COUNT(DISTINCT pn.MaPN) AS SoPhieuNhap,
        SUM(ct.SoLuong) AS TongSoLuong,
        SUM(pn.TongTien) AS TongTien
    FROM PhieuNhap pn
    INNER JOIN ChiTietPhieuNhap ct ON pn.MaPN = ct.MaPN
    WHERE pn.NgayNhap BETWEEN @TuNgay AND @DenNgay
      AND pn.TrangThai = 1
    GROUP BY CONVERT(varchar(10), pn.NgayNhap, 103)
    ORDER BY CONVERT(varchar(10), pn.NgayNhap, 103)
END
GO

-- SP báo cáo xuất hàng
CREATE PROCEDURE sp_BaoCaoXuatHang
    @TuNgay datetime,
    @DenNgay datetime
AS
BEGIN
    SELECT 
        CONVERT(varchar(10), hd.NgayBan, 103) AS NgayBan,
        COUNT(DISTINCT hd.MaHD) AS SoHoaDon,
        SUM(ct.SoLuong) AS TongSoLuong,
        SUM(hd.TongTien) AS TongDoanhThu
    FROM HoaDon hd
    INNER JOIN ChiTietHoaDon ct ON hd.MaHD = ct.MaHD
    WHERE hd.NgayBan BETWEEN @TuNgay AND @DenNgay
      AND hd.TrangThai = 1
    GROUP BY CONVERT(varchar(10), hd.NgayBan, 103)
    ORDER BY CONVERT(varchar(10), hd.NgayBan, 103)
END
GO