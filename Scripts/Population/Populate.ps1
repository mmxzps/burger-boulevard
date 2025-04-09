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
Write-Host "Database populated with data."

Get-ChildItem ./Images/
| ForEach-Object {
  $id = $_.Name
  $data = '0x' + ((Format-Hex -Path $_.FullName | Select -ExpandProperty HexBytes) | Join-String).Replace(' ', '')
  Write-Host "Populating database with image: $id"
  Invoke-SqlCmd -ConnectionString $ConnectionString -Query "SET IDENTITY_INSERT Images ON; INSERT INTO Images (Id, Data) VALUES ($id, $data); UPDATE Components SET ImageId = $id WHERE Id = $id;" -QueryTimeout 120
}

Write-Host "Database populated with images."
Write-Host "Done."
