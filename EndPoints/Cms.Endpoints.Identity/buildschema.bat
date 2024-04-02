rmdir /S /Q "Data/Migrations"

dotnet ef migrations add Users -c CmsDbContext -o Data/Migrations
