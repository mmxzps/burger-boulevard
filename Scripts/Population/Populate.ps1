param(
    [string]$ConnectionString
)

if (-not $ConnectionString) {
    Write-Host "Taking connection string from user-secrets."
    $ConnectionString = (
        dotnet user-secrets list --json --project $PSScriptRoot/../../Backend
        | ConvertFrom-Json
        | Select -ExpandProperty 'ConnectionStrings:Database')
}

Write-Host "Using connection string: $ConnectionString"

$connection = New-Object System.Data.SqlClient.SqlConnection
$connection.ConnectionString = $ConnectionString

try {
    $connection.Open()
    Write-Host "Connected to database."

    $command = $connection.CreateCommand()
    $command.CommandText = Get-Content $PSScriptRoot/Populate.sql
    Write-Host "Executing..."
    $command.ExecuteNonQuery()
    Write-Host "Done. Database populated."
} catch {
    Write-Host "Error: $_" -ForegroundColor Red
    Write-Host "Stack Trace: $($_.Exception.StackTrace)" -ForegroundColor Yellow
} finally {
    $connection.Close()
}
