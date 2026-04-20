create database DBPeminjamanAlat

use DBPeminjamanAlat
go

create table Admin (
AdminID  INT IDENTITY (1,1)PRIMARY KEY,
username VARCHAR(16) UNIQUE NOT NULL,
password VARCHAR(16) NOT NULL
);

create table Petugas(
petugasID  INT IDENTITY (1,1)PRIMARY KEY,
username VARCHAR(16) UNIQUE NOT NULL,
password varchar(16) NOT NULL,
);

create table Peminjam(
NIK CHAR (16) PRIMARY KEY,
Nama_Peminjam VARCHAR (100) NOT NULL,
Alamat VARCHAR (200) NOT NULL,
NomorHP VARCHAR (15) NOT NULL,
);

create table Alat (
alatID INT IDENTITY (1,1) PRIMARY KEY,
Nama_Alat Varchar(20)NOT NULL UNIQUE,
Stok INT CHECK (Stok >= 0),
AdminID INT NULL,
Constraint FK_ALAT_ADMIN Foreign KEY (AdminID) REFERENCES Admin(AdminID) 
);

create table Peminjaman(
peminjamanID INT IDENTITY (1,1)PRIMARY KEY,
petugasID INT NOT NULL,
NIK CHAR(16) NOT NULL,
alatID INT NOT NULL,
Tanggal_Pinjam DATE DEFAULT GETDATE(),
Status varchar(10) NOT NULL CHECK (Status in ('DIPINJAM','TERSEDIA')),
Jumlah_Pinjam smallint NOT NULL

Constraint FK_Peminjaman_Petugas Foreign KEY (petugasID) REFERENCES Petugas (petugasID), 
Constraint FK_Peminjaman_Peminjam Foreign KEY (NIK) REFERENCES Peminjam (NIK),
Constraint FK_Peminjaman_Alat Foreign KEY (alatID) REFERENCES Alat (alatID),
);

INSERT INTO Admin (username, password)values
('Admin_1', 'Admin456');

INSERT INTO Petugas (username ,password) values
('Petugas_1' , 'Petugas 456');

INSERT INTO Peminjam  values
('3602041211870001', 'Asep Sujasmedi', 'jln Gunung Pancasila RT1/RW2', '081234843214'),
('3602042112090005', 'Ranti', 'Jln Gunung Pakuwon City RT1/RW6', '083156724983');

INSERT INTO Alat  (Nama_Alat, Stok, AdminID) VALUES
('Speaker', 5, 1),
('MIC', 8, 1),
('Stand MIC', 8, 1);

INSERT INTO Peminjaman (petugasID, NIK, alatID , Tanggal_Pinjam, Status, Jumlah_Pinjam) Values 
(1, '3602041211870001', 3, GETDATE(), 'DIPINJAM', 22 );

