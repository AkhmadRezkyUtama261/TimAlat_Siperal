# Skenario SQL Injection (Tugas UCP 2)

## 1. Apa itu SQL Injection?
SQL Injection adalah teknik serangan siber di mana penyerang memasukkan (menginjeksi) kode SQL berbahaya ke dalam input aplikasi (seperti form login atau form pencarian) untuk memanipulasi database di belakangnya.

## 2. Skenario Serangan pada Form Login
Misalkan pada `FormLogin`, query pengecekan user ditulis menggunakan penggabungan string biasa seperti ini:
```csharp
// CONTOH KODE YANG RENTAN (VULNERABLE)
string query = "SELECT * FROM Admin WHERE username = '" + txtUsername.Text + "' AND password = '" + txtPassword.Text + "'";
```

### Langkah-langkah Serangan:
1. Penyerang membuka aplikasi dan masuk ke **Form Login**.
2. Pada kolom **Username**, penyerang mengetikkan payload berikut:
   `admin' OR '1'='1`
3. Pada kolom **Password**, penyerang bisa mengisi bebas (misal: `123`).
4. Saat tombol Login ditekan, query yang terkirim ke database akan berubah menjadi:
   ```sql
   SELECT * FROM Admin WHERE username = 'admin' OR '1'='1' AND password = '123'
   ```
5. **Hasilnya:** Karena kondisi `'1'='1'` selalu bernilai **TRUE**, database akan mengabaikan pengecekan password yang salah, dan penyerang berhasil masuk ke dalam sistem sebagai Admin tanpa perlu tahu password yang sebenarnya.

## 3. Cara Mencegah di SIPERAL
Aplikasi SIPERAL ini sudah **kebal (immune)** terhadap serangan SQL Injection karena seluruh operasi database (Login, Insert, Update, Delete) sudah menggunakan **Parameterized Queries** (`SqlCommand.Parameters.AddWithValue`) dan **Stored Procedures** (`sp_CekLoginAdmin`, dll).

Dengan Parameterized Query, input dari user (seperti `' OR '1'='1`) akan diperlakukan murni sebagai teks biasa (string) dan tidak akan dieksekusi sebagai perintah SQL oleh database.
