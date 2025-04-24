param(
    [string]$ConnectionString
)

if (-not $ConnectionString) {
    Write-Host "Taking connection string from user-secrets."
    $ConnectionString = (
        dotnet user-secrets list --json --project $PSScriptRoot/../../Backend
        | ConvertFrom-Json
        | Select-Object -ExpandProperty 'ConnectionStrings:Database')
}

Write-Host "Using connection string: $ConnectionString"

$ApiUrl = "https://localhost:7115/api/Orders"

$burgers = @(20, 21, 22)
$breads = @(1, 2)
$meats = @(3, 4, 5)
$ingredients = @(6..19)
$fries = @(25, 26, 27)
$drinks = @(23, 24)

function CreateRandomOrderPayload {
    $burgerId = Get-Random -InputObject $burgers
    $breadId = Get-Random -InputObject $breads
    $meatId = Get-Random -InputObject $meats
    $randomIngredients = Get-Random -InputObject $ingredients -Count 2

    $children = @(
    @{ "componentId" = $breadId; "children" = @() },
    @{ "componentId" = $meatId; "children" = @() }
)

    foreach ($ingredientId in $randomIngredients) {
        $children += @{
            "componentId" = $ingredientId
            "children"    = @()
        }
    }

    $components = @()

    $components += @{
        "componentId" = $burgerId
        "children" = $children
    }

    $components += @{
        "componentId" = (Get-Random -InputObject $fries)
        "children" = @()
    }

    $components += @{
        "componentId" = (Get-Random -InputObject $drinks)
        "children" = @()
    }

    return @{
        "components" = $components
        "takeAway"   = $true
    }
}

do {
# Create the random order payload
$orderPayload = CreateRandomOrderPayload

# Convert the payload to JSON
$orderJson = $orderPayload | ConvertTo-Json -Depth 10

# Set headers for authentication (if needed)
$headers = @{
    "Content-Type"  = "application/json"
}

# Make the POST request to the API
$response = Invoke-RestMethod -Uri $ApiUrl -Method Post -Headers $headers -Body $orderJson

# Output the response (you can modify this depending on your API response)
Write-Host "Order created successfully. Response: $($response | ConvertTo-Json)"
$userInput = Read-Host "Do you want to create another order? (y/n)"
} while ($userInput -eq "y")

Write-Host "Order creation stopped."