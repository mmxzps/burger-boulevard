#!/usr/bin/env nu

sudo sqlcmd create mssql --port 1433 --accept-eula
sudo sqlcmd config connection-strings
| split row (char newline)
| find ADO.NET
| get 0
| str replace 'ADO.NET: ' ''
| dotnet user-secrets --project ($env.FILE_PWD | path dirname | path join Backend) set ConnectionStrings:Database $in
