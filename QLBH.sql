create database QLBH

use QLBH
/*LẤY TOÀN BỘ MẶT HÀNG*/
CREATE PROCEDURE LayToanBoMatHang
AS
Select * FROM tblMatHang

exec LayToanBoMatHang

/*THÊM MẶT HÀNG*/
CREATE PROCEDURE InsertMatHang 
	@MaSP nchar(5),
	@TenSP nvarchar(30),
	@NgaySX Date,
    @NgayHH Date,
    @DonVi nvarchar(10),
    @DonGia float,
	@GhiChu nvarchar(200)
AS
   INSERT INTO tblMatHang(MaSP,TenSP,NgaySX,NgayHH,DonVi,DonGia,GhiChu)
   VALUES(@MaSP,@TenSP,@NgaySX,@NgayHH,@DonVi,@DonGia,@GhiChu) 

/*CẬP NHẬT MẶT HÀNG*/
CREATE PROCEDURE CapNhatMatHang
(
    @MaSP nchar(5),
	@TenSP nvarchar(30),
	@NgaySX Date,
    @NgayHH Date,
    @DonVi nvarchar(10),
    @DonGia float,
	@GhiChu nvarchar(200)
)
AS BEGIN
update tblMatHang set TenSP=@TenSP, NgaySX=@NgaySX, NgayHH=@NgayHH, DonVi=@DonVi, DonGia=@DonGia, GhiChu=@GhiChu where MaSP = @MaSP

End

Go


exec CapNhatMatHang

/*TÌM THEO MÃ SP*/
CREATE PROCEDURE TimTheoMaSP @MaSP nchar(5)
AS
Select * FROM tblMatHang WHERE MaSP = @MaSP
/*TÌM THEO TÊN SP*/
CREATE PROCEDURE TimTheoTenSP @TenSP nvarchar(30)
AS
Select * FROM tblMatHang WHERE TenSP=@TenSP


/*XÓA MẶT HÀNG*/
CREATE PROCEDURE Xoa @MaSP nchar(5)
AS
DELETE  FROM tblMatHang WHERE MaSP = @MaSP

