# TimAlat_Siperal Simulasi SQL Injection

SQL Injection Scenario
Deskripsi
Pada fitur pencarian data barang inventaris alat RT, sistem menggunakan query SQL yang digabung langsung dengan input pengguna tanpa menggunakan parameterized query atau Stored Procedure. Hal ini menyebabkan fitur pencarian tersebut sangat rentan terhadap serangan SQL Injection.

Query Rentan SQL Injection
C#
string queryBocor = "SELECT * FROM vw_Alat WHERE Nama_Alat LIKE '%" + txtSearch.Text + "%'";
Query di atas secara langsung menggabungkan input pengguna (txtSearch.Text) ke dalam query SQL tanpa adanya validasi, enkapsulasi, atau parameterized query.

Langkah-langkah SQL Injection
1. Pengguna membuka form data alat Pengguna membuka halaman data alat inventaris pada aplikasi SIPERAL (baik login sebagai Admin maupun Petugas).

2. Pengguna memasukkan input SQL Injection Input atau payload berikut dimasukkan ke dalam kolom TextBox pencarian (txtSearch):

SQL
' OR 1=1 --
3. Sistem membentuk query SQL Setelah input digabungkan secara mentah oleh program di backend, query yang dieksekusi oleh SQL Server menjadi:

SQL
SELECT * FROM vw_Alat WHERE Nama_Alat LIKE '%' OR 1=1 --%'
4. Database menjalankan query dan memicu efek manipulasi Karakter -- akan mematikan sisa query di belakangnya menjadi comment. Karena kondisi logika matematika 1=1 selalu bernilai TRUE, database dipaksa meloloskan hak akses data.

Sistem C# menangkap eksploitasi ini lalu menampilkan kotak pesan darurat: 🚨 WARNING: SYSTEM HACKED! 🚨. Setelah ditekan OK, DataGridView secara interaktif dimanipulasi untuk menampilkan baris data tiruan bertuliskan HACKED, SYSTEM HACKED, dan VULNERABLE di seluruh kolom tabel sebagai bukti visual bahwa pertahanan database telah ditembus.

Dampak SQL Injection
SQL Injection dapat menyebabkan kebocoran data (Data Leakage) dan manipulasi tampilan antarmuka visual aplikasi, karena pengguna dapat merusak alur logika pencarian database tanpa batasan yang sesuai.

Penyebab SQL Injection
SQL Injection terjadi karena query SQL dibuat menggunakan metode concatenation string (penggabungan teks manual menggunakan tanda +) dan tidak menggunakan parameterized query atau Stored Procedure.

Pencegahan SQL Injection
SQL Injection pada aplikasi SIPERAL telah dicegah dan dimitigasi secara total pada form lainnya (seperti Form Peminjaman dan proses CRUD manipulasi data alat) dengan menggunakan Stored Procedure sehingga input pengguna diproses murni sebagai teks literal, bukan sebagai perintah SQL executable.

Contoh penggunaan Stored Procedure yang aman pada sistem SIPERAL:

C#
SqlCommand cmd = new SqlCommand("sp_TambahAlat", conn);
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.AddWithValue("@AlatID", txtKodeAlat.Text.Trim());
cmd.Parameters.AddWithValue("@Nama", txtNama.Text.Trim());
