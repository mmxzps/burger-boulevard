# bb6

# Krav

- SQL Server (Developer/Express): <https://www.microsoft.com/en-us/sql-server/sql-server-downloads>
- .NET SDK 9: `winget install Microsoft.DotNet.SDK.9`
- .NET EF-CLI: `dotnet tool install --global dotnet-ef`
- Node.js: `winget install OpenJS.NodeJS`

# Förberedelser

## Backend

Ange databasadress- och inställningar (connection string) åt backend. SQL Servers installationsprogram avslutar med att visa din connection string. Denna bör fungera för SQL Server på Windows.

```powershell
dotnet user-secrets --project Backend set 'ConnectionStrings:Database' 'Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;'
```

Genomför databasmigrationer:

```powershell
dotnet ef database update --project Backend
```

Fyll databasen med data:

```powershell
Scripts/Population/Population.ps1
```

## Frontend

Länka API till frontend:

Skapa filen `Frontend/.env.local` med innehållet:

```env
VITE_API_BASE_URL = 'http://localhost:5269'
```

Se till att HTTP-scheme (`http` eller `https`) och port stämmer överens med backend. Se [launchSettings.json](Backend/Properties/launchSettings.json) för adress.

Installera Node.js-paket:

```powershell
npm install --prefix Frontend
```

## TODO: Köksfrontend
