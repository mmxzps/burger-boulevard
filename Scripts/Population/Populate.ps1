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

Get-ChildItem $PSScriptRoot/Images | ForEach-Object {
  $tableName = $_.Name
  Get-ChildItem $_ | ForEach-Object {
    $id = Get-Random
    $linkedId = $_.Name
    $data = '0x' + ((Format-Hex -Path $_.FullName | Select -ExpandProperty HexBytes) | Join-String).Replace(' ', '')
    Write-Host "Populating Images and linking to $tableName (ImageId column) with image: $linkedId. Id is set to $id."
    Invoke-SqlCmd -ConnectionString $ConnectionString -Query "SET IDENTITY_INSERT Images ON; INSERT INTO Images (Id, Data) VALUES ($id, $data); UPDATE $tableName SET ImageId = $id WHERE Id = $linkedId;" -QueryTimeout 120
  }
}

Write-Host "Database populated with images."
Write-Host "Done."
