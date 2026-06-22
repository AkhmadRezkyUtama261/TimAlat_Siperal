-- Revisi 
-- Tambah Validasi 
-- CASCADE 
-- Buat Role 

--UCP 2
-- buat view
/*
1. Query Insert,Update,Delete, dan Search diubah menjadi STORED PROCEDURE, dimana STORED PROCEDURE yang dibuat tidak hanya berisi query dasar dari INSERT/UPDATE/DELETE/SEARCH
2. Query Select diubah menjadi VIEW.
3. terapkan SQL Injection disalah satu form yang ada, dan tulisakan skenario SQL Injection pada file Read.Me di GitHub
4. Manfaatkan Binding dalam DataGridView.
5. Buat Binding Navigator untuk memilig data pada DataGridView
*/

Create database DBPeminjamanAlat

use  DBPeminjamanAlat
go 

create table Admin (
	AdminID INT IDENTITY (1,1) PRIMARY KEY ,
	username VARCHAR(16) UNIQUE NOT NULL,
	password VARCHAR(16) NOT NULL,

	Constraint CK_Adm_Username CHECK (len(username) >= 5 AND username NOT LIKE '%[^A-Za-z0-9_]%'),
	Constraint CK_Pw_Password CHECK (len(password) >= 8 and len(password) <= 16)
);


create table Petugas(
	petugasID  INT IDENTITY (1,1)PRIMARY KEY,
	username VARCHAR(16) UNIQUE NOT NULL,
	password varchar(16) NOT NULL,

	Constraint CK_Ptg_Username CHECK (LEN(username) >= 5 AND username NOT LIKE '%[^A-Za-z0-9_]%'),
	Constraint CK_Ptg_Password CHECK (len(password) >= 8 and len(password) <= 16)
);
--Alamat
-- Jangan unique nama peminjam hehe 
create table Peminjam(
	PeminjamID INT IDENTITY (1,1) primary key,
	NIK CHAR (16) NOT NULL UNIQUE , 
	Nama_Peminjam VARCHAR (100) NOT NULL,
	Alamat VARCHAR (200) NOT NULL,
	NomorHP VARCHAR (15) NOT NULL UNIQUE, 

	Constraint CK_Nama_Peminjam  CHECK (Nama_Peminjam NOT LIKE '%[^A-Za-z ]%'), 
	Constraint CK_NIK CHECK (NIK Not like '%[^0-9]%' and len(NIK) = 16), 
	Constraint CK_NomorHP CHECK (NomorHP Not like '%[^0-9]%' and len(NomorHP) >= 10 and len(NomorHP) <= 15)
);

--Merek Inputannya mau jadi 1 atau misah ?
create table Alat(
	alatID Varchar(16) NOT NULL PRIMARY KEY,
	Nama_Alat Varchar(20) NOT NULL,
	Merek Varchar(20) NOT NULL,
	Stok smallint NOT NULL,
	AdminID INT NULL,

	Constraint CK_alt_Nama CHECK (Nama_Alat NOT LIKE '%[^A-Za-z ]%'),
	Constraint FK_alt_ALAT_ADMIN Foreign KEY (AdminID) REFERENCES Admin(AdminID),
	Constraint CK_alt_Stk CHECK (Stok >= 0),
	Constraint CK_alt_Merek CHECK (Merek NOT LIKE '%[^A-Za-z-0-9 ]%')
);


Create table Peminjaman(
	peminjamanID INT IDENTITY (1,1) PRIMARY KEY,
	petugasID INT NOT NULL,
	PeminjamID INT NOT NULL ,
	alatID Varchar (16) NOT NULL,
	Tanggal_Pinjam DATE DEFAULT GETDATE(),
	Status varchar(10) NOT NULL CHECK (Status in ('DIPINJAM','DIKEMBALIKAN','DENDA')),
	Jumlah_Pinjam smallint NOT NULL,

	Constraint FK_Peminjaman_Petugas Foreign KEY (petugasID) REFERENCES Petugas (petugasID), 
	Constraint FK_Peminjaman_Peminjam Foreign KEY (PeminjamID) REFERENCES Peminjam (PeminjamID) ON DELETE CASCADE,
	Constraint FK_Peminjaman_Alat Foreign KEY (alatID) REFERENCES Alat (alatID) ON DELETE CASCADE
);

Insert into Admin
values ('Admin1', 'Admin123');

Select * from Admin
Insert into Petugas
values ('Petugas2', 'Petugas123');

select * from Petugas
select * from Peminjam 

Insert into Peminjam
values ('3471035512010999','Akhmad Zain Stito', 'jl.Mencari Cinta yang Sudah Hilang , Perumahan Hubungan Tanpa Status RT900/RW008', '08921165712');

insert into Alat 
values	('MC_Advance_1', 'Mic','Advance',3, 1),
		('SPK_Advance_1', 'Speaker','Advance',6, 1 ),
		('SPK_Sony_2', 'Speaker', 'Sony',9, 1 ),
		('MC_Sony_2', 'Mic', 'sony','6', 1 )
insert into Peminjaman
values (1, 1, 'MC_Advance_1', GETDATE(), 'DIPINJAM', 3 )


		
-- Role 
-- Masih view Dasar
Create View vw_Admin AS
Select 
	AdminID,
	username 
from Admin;

Create View vw_Petugas AS
Select 
	petugasID,
	username 
from Petugas;

Create View vw_Peminjam AS
Select 
	PeminjamID,
	NIK, 
	Nama_Peminjam,
	Alamat,
	NomorHP
from Peminjam;

Alter View vw_Alat AS
Select 
	alatID ,
	Nama_Alat ,
	Merek,
	Stok ,
	AdminID
from Alat
where Stok >= 0


Alter view vm_MenampilkanDaftarPeminjaman AS
Select DISTINCT
	pjn.PeminjamanID,
	pj.Nama_Peminjam as NamaPeminjam,
	a.Nama_Alat as NamaAlat,
	pjn.Tanggal_Pinjam,
	pjn.Jumlah_Pinjam,
	pjn.Status
	

from Peminjaman pjn
JOIN Peminjam pj on pjn.PeminjamID = pj.PeminjamID
JOIN Alat a ON pjn.alatID = a.alatID

--Laporan Peminjaman yang di butuhkan PeminjamanID, NamaPetugas,Nama Peminjam,NamaAlat,TanggalPinjam,Status,JumlahPinjam

Create View vw_DataLaporanPeminjaman AS
Select 
	pjn.PeminjamanID,
	ptg.username as NamaPetugas,
	pj.Nama_Peminjam as NamaPeminjam,
	a.Nama_Alat as NamaAlat,
	pjn.Tanggal_Pinjam,
	pjn.Status,
	pjn.Jumlah_Pinjam

from Peminjaman pjn
JOIN Petugas ptg on pjn.petugasID = ptg.petugasID
JOIN Peminjam pj on pjn.PeminjamID = pj.PeminjamID
JOIN Alat a on pjn.alatID = pjn.alatID

Group BY 
	pjn.PeminjamanID,
	ptg.username,
	pj.Nama_Peminjam ,
	a.Nama_Alat,
	pjn.Tanggal_Pinjam,
	pjn.Status,
	pjn.Jumlah_Pinjam


/* 
=============================================
			  STORED PROCEDURE
=============================================
*/
Create proc sp_CekLoginAdmin
	@username VARCHAR(16),
	@password VARCHAR(16)
as
Begin
	
	IF Exists (Select 1 From Admin Where username = @username and password = @password)
	Begin
		print 'Sakses Selamat datang atmin';
	End
	Else
	Begin 
		Throw 5100, 'Username atau password salah nyak', 1;
	End

	Select Count(*) From Admin
	Where username = @username and password = @password
End;

Create Proc sp_CekLoginPetugas
	@username VARCHAR(16),
	@password VARCHAR(16)
as
Begin
	
	IF Exists (Select 1 From Petugas Where username = @username and password = @password)
	Begin
		print 'Sakses Selamat datang atmin';
	End
	Else
	Begin 
		Throw 5100, 'Username atau password salah nyak', 1;
	End

	Select Count(*) From Petugas
	Where username = @username and password = @password
End;

/* 
=======================
		INSERT
=======================
*/
Alter Proc sp_TambahPinjam
	@petugasID INT,
	@peminjamID INT,
	@alatID varchar(16),
	@jumlah_Pinjam smallint
as
Begin
	Declare @stok smallint
	Select @stok = Stok from Alat where alatID = @alatID

	IF @stok <  @jumlah_Pinjam
	begin 
		Throw 51000, 'kaga cukupp', 1
	End

	IF exists (Select 1 from Alat where alatID = @alatID and Stok = @stok )
	Begin 
		Throw 51000, 'Alat Gak ada cuy', 1
	End

	Begin try
		
		Insert Into Peminjaman (petugasID, PeminjamID, alatID, Tanggal_Pinjam, Status, Jumlah_Pinjam)
		Values (@petugasID, @peminjamID,@alatID, GETDATE(), 'DIPINJAM', @jumlah_Pinjam)

		update Alat
		set Stok = @stok - @jumlah_Pinjam
		where alatID = @alatID 
		

		print 'Sukses nyak';
	End try
	Begin Catch 
		print 'Stok g cukup nyak' ;
		print ERROR_MESSAGE();
	End Catch
end;

create Proc sp_TambahAlat
	@AlatID Varchar(16),
	@Nama varchar(20),
	@stok smallint
as 
Begin 
	IF @AlatID Not like '%[_]%[_]%' OR @AlatID Like '%[_]%[_]%[_]%'
	Begin 
		Throw 51000, 'Salah format nya nyak Contoh : MC_Sony_01', 1;
	End
	
	IF @AlatID NOT LIKE '%[0-9]'
	Begin 
		Throw 51000, 'Harus angka nyak contoh : MC_Sony_01',1;
	End

	IF Exists (Select 1 From  Alat Where alatID = @AlatID)
	Begin 
		Throw 51000, 'apasih dh ada.', 1;
	End

	Insert into Alat (alatID,Nama_Alat,Stok, AdminID)
	Values (@AlatID,@Nama, @stok, 1)

	print 'Dah';
end;

Alter proc sp_TambahPeminjam
	@NIK char (16),
	@NAMA varchar(100),
	@alamat varchar(200),
	@NoHp varchar(15)
as 
begin 
	
	Begin Try
		Begin transaction;
		IF EXISTS (Select 1 from Peminjam Where NIK = @NIK)
		Begin
			
			Throw 51000, 'Error : Peminjam Tersebut sudah terdaftar.', 1;
		End

			insert into Peminjam (NIK, Nama_Peminjam, Alamat, NomorHP)
			values (@NIK, @NAMA, @alamat, @NoHp)
		commit transaction;

		print 'Sukses : Peminjam Baru Berhasil ditambahkan ';
	End try 
	Begin Catch 
		
		print 'Gagal menambahkan Peminjam ,' ;
		print ERROR_MESSAGE();

	End Catch
End;

/* 
=======================
		UPDATE
=======================
*/
Alter Proc sp_UpdateAlat
	@AlatIDLama varchar(16),
	@AlatIDBaru varchar(16),
	@Nama Varchar(20),
	@Merek Varchar(20),
	@StokBaru Int
As 
Begin 
	Declare @StokLama int;
	Declare @NamaLama varchar(20);

	Select @StokLama = stok, @NamaLama = Nama_Alat
	from Alat where alatID = @AlatIDLama;
		
		IF @StokLama IS NULL
		Begin
			Throw 51000, 'Kaga ade',1;
		End

		IF @StokBaru < 0
		Begin 
			Throw 51000, 'hm masa stok minus hadeh',1;
		end

		Begin try 
			update Alat
			set Nama_Alat = @Nama,
				Stok = @StokBaru
			Where alatID =@AlatIDLama;
		end try
		Begin Catch 
		print 'Gagal,' ;
		print ERROR_MESSAGE();
		End Catch
End;

create proc sp_UpdatePeminjam
	@peminjamID int,
	@NIK char(16),
	@Nama varchar(100),
	@Alamat varchar(200),
	@NoHP Varchar(15)
as
Begin
	IF Not exists (Select 1 from Peminjam where PeminjamID = @PeminjamID)
	Begin
		Throw 51000, 'g ada kocak.', 1;
	End
	IF exists (Select 1 from Peminjam Where @NIK = NIK and PeminjamID <> @peminjamID)
	Begin
		Throw 51000, 'dah kepake HUUu', 1;
	End

	Begin try 
	Update Peminjam
	Set NIK = @NIK,
		Nama_Peminjam = @Nama,
		Alamat = @Alamat,
		NomorHP = @NoHP
	Where PeminjamID = @peminjamID

	print 'Dah' 
	End try

		Begin Catch 
		
		print 'Gagal,' ;
		print ERROR_MESSAGE();
	End Catch
End;

Alter proc sp_UpdatePeminjaman 
	@peminjamanID int
as 
	begin 
		DECLARE @alatID varchar(16), @JumlahPinjam smallint, @TanggalPinjam Date
		Declare @Hari INT, @Denda INT = 0, @Status Varchar(15) = 'DIKEMBALIKAN'
		
		Select @alatID = alatID, @JumlahPinjam = Jumlah_Pinjam , @TanggalPinjam = Tanggal_Pinjam From Peminjaman
		Where peminjamanID =	@peminjamanID

		IF EXISTS (SELECT 1 from Peminjaman Where peminjamanID = @peminjamanID and status = 'DIPINJAM')
		Begin TRY
			Begin Transaction 

			set @Hari = DATEDIFF(DAY, DATEADD(DAY, 5, @TanggalPinjam),GETDATE())

			If @Hari > 0
			Begin
				Set @Denda = @Hari * 5000
				Set @Status = 'Denda' 
				
				Print 'Napa Telat nyak ? Denda ya nyak' + CAst (@Denda As varchar)
			End
			
			Update Alat
			Set Stok = Stok + @JumlahPinjam
			where alatID = @alatID

			Update Peminjaman SET Status = @Status where peminjamanID = @peminjamanID
			
			Commit Transaction;
			Print 'Berhasil Nyak'
		END TRY
		Begin Catch 
			print 'Gagal nyak ,' ;
			print ERROR_MESSAGE();
		End Catch
End;

/* 
=======================
		DELETE
=======================
*/
--Hapus
Create Proc sp_HapusPeminjam
	@PeminjamID INT
as
Begin
	IF Exists (Select 1 from Peminjaman Where PeminjamID = @PeminjamID and Status = 'DIPINJAM')
	Begin 
		Throw 51000, 'Gagal! Warga ini masih meminjam alat dan belum dikembalikan.', 1;
	End
	Begin try

		DELETE FROM  Peminjam WHERE peminjamID = @PeminjamID;
		PRINT 'Peminjam Berhasil Dihapus! nyak';
	End try

	Begin Catch 
		print 'Gagal,' ;
		print ERROR_MESSAGE();
	End Catch
	
END;
		
CREATE PROC sp_HapusAlat
    @AlatID VARCHAR(16)
AS
BEGIN
	IF NOT EXIsts (Select 1 From Alat where alatID = @AlatID)
	Begin 
		THROW 51000, 'Alat nya kaga ada nyak', 1
	End

	-- Validasi: Cegah hapus alat jika sedang dipinjam
	IF EXISTS (SELECT 1 FROM Peminjaman WHERE alatID = @AlatID AND Status = 'DIPINJAM')
	BEGIN
		THROW 51000, 'Gagal! Alat ini sedang dipinjam dan belum dikembalikan.', 1;
	END

	Begin try
    DELETE FROM Alat WHERE alatID = @AlatID;
    PRINT 'Alat Berhasil Dihapus! nyak';
	End try


	Begin Catch 
		print 'Gagal,' ;
		print ERROR_MESSAGE();
	End Catch
	
END;

CREATE PROC sp_HapusPeminjaman
    @PeminjamanID INT
as
BEGIN
	Declare @alatID varchar(16);
	Declare @jumlah smallint;
	Declare @status varchar (10);

	Select @alatID = alatID, @jumlah = Jumlah_Pinjam, @status = Status
	From Peminjaman Where peminjamanID = @PeminjamanID

	IF @alatID IS NULL
		Begin 
			Throw 51000, 'g ad',1;
		end
	Begin try

		If @status = 'DIPINJAM'
		Begin 
			Update Alat
			Set stok = stok + @jumlah where alatID = @alatID

			print 'Stok nya balik karna di apus';
		End
			
    DELETE FROM Peminjaman WHERE PeminjamanID = @PeminjamanID;
    PRINT 'Alat Berhasil Dihapus! nyak';

	End try
	Begin Catch 
		print 'Gagal,' ;
		print ERROR_MESSAGE();
	End Catch
	
END;



/* 
=======================
		SEARCH
=======================
*/

create proc sp_SearchPeminjam
	@Search varchar(100)
as
Begin
	Select PeminjamID, NIK, Nama_Peminjam, Alamat,NomorHP
	From Peminjam
		Where NIK = @Search OR Nama_Peminjam Like '%' + @Search + '%'
		Order by
			Nama_Peminjam asc;
	
	If @@ROWCOUNT = 0

	Begin 
		Print 'Kaga ada cek aja lagi yang teliti'
	End
end;
		
create proc sp_SearchAlat
	@Search varchar(100)
as
Begin
	Select alatID, Nama_Alat, Stok
	From Alat
		Where alatID = @Search OR Nama_Alat Like '%' + @Search + '%'
		Order by
			Stok asc;
	
	If @@ROWCOUNT = 0

	Begin 
		Print 'Kaga ada cek aja lagi yang teliti'
	End
end;
		
Create proc Sp_SearchPeminjaman
	@Search Varchar(100)
as
Begin 
	IF Exists (
		Select 1
		from Peminjaman pjn
	JOIN Petugas ptg on pjn.petugasID = ptg.petugasID
	JOIN Peminjam pj on pjn.PeminjamID = pj.PeminjamID
	JOIN Alat a on pjn.alatID = pjn.alatID
	Where pj.Nama_Peminjam Like '%' + @Search + '%' OR a.Nama_Alat Like '%' + @Search + '%' OR pjn.Status Like '%' + @Search + '%'
		)
	Begin try
	Select 
		pjn.PeminjamanID,
		ptg.username as NamaPetugas,
		pj.Nama_Peminjam as NamaPeminjam,
		a.Nama_Alat as NamaAlat,
		pjn.Tanggal_Pinjam,
		pjn.Status,
		pjn.Jumlah_Pinjam

	from Peminjaman pjn
	JOIN Petugas ptg on pjn.petugasID = ptg.petugasID
	JOIN Peminjam pj on pjn.PeminjamID = pj.PeminjamID
	JOIN Alat a on pjn.alatID = pjn.alatID
	Where pj.Nama_Peminjam Like '%' + @Search + '%' OR a.Nama_Alat Like '%' + @Search + '%' OR pjn.Status Like '%' + @Search + '%'
	order BY pjn.Tanggal_Pinjam Desc;

	print 'dah';
	End try
	Begin catch
		print 'Gagal,' ;
		print ERROR_MESSAGE();
	end catch
end;


select * into Admin1_Backup From Admin;

select * into Petugas_Backup From Petugas;

select * into Peminjam_Backup From Peminjam;

select * into Alat_Backup From Alat;

select * into Peminjaman_Backup From Peminjaman;



	

