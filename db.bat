
dotnet ef database update 0
dotnet ef migrations remove
dotnet ef migrations add init
dotnet ef database update

REM dotnet ef migrations script --idempotent --output "script.sql"