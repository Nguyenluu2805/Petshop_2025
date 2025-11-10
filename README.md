# PetShop API

Dự án **PetShop API** là một backend ASP.NET Core 10 Web API sử dụng PostgreSQL để quản lý sản phẩm, người dùng, đơn hàng, thú cưng và dịch vụ.

1️⃣ Yêu cầu

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio Code](https://code.visualstudio.com/) hoặc Visual Studio 2022
- (Tùy chọn) [ngrok](https://ngrok.com/) để expose API ra ngoài

2️⃣ Clone dự án
git clone https://github.com/<your-github-username>/Petshop_2025.git
cd Petshop_2025
3️⃣ Cấu hình database
Cập nhật connection string trong appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=petshop;Username=petshop_user;Password=yourpassword"
}
4️⃣ Cấu hình JWT
"Jwt": {
  "Key": "YourSuperSecretKey123!",
  "Issuer": "PetShopAPI",
  "Audience": "PetShopClient"
}
5️⃣ Cài đặt và restore các package
- dotnet restore
- Nếu thiếu package thì cài đặt thêm
6️⃣ Apply migrations & seed dữ liệu
- dotnet ef database update --project ../PetShop.Infrastructure/PetShop.Infrastructure.csproj
- Dự án có sẵn migrations và data seeder(có thể thay đổi trong file DataSeeder.cs)
7️⃣ Chạy API:
- dotnet run --project PetShop.API
- Mặc định chạy trên http://localhost:5291
