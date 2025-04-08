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

Install-Module SqlServer
Import-Module SqlServer

Invoke-SqlCmd -ConnectionString $ConnectionString -InputFile $PSScriptRoot/Populate.sql -QueryTimeout 120 &&
Write-Host "Done. Database populated."
