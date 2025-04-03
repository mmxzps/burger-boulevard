param(
    [string]$ConnectionString
)

if (-not $ConnectionString) {
    Write-Host "Taking connection string from user-secrets."
    $ConnectionString = (
        dotnet user-secrets list --json --project $PSScriptRoot/../Backend
        | ConvertFrom-Json
        | Select -ExpandProperty 'ConnectionStrings:Database')
}

# SQL commands to insert the data
$sqlQueries = @"
-- Insert Discounts
INSERT INTO Discounts (Multiplier) VALUES (1.0);  -- Discount 1 (No discount)
INSERT INTO Discounts (Multiplier) VALUES (0.7);  -- Discount 2 (30% off)

-- Insert Prices, referencing Discount ID 1
INSERT INTO Prices (BasePrice) VALUES (5); -- -1
INSERT INTO Prices (BasePrice) VALUES (10); -- -2
INSERT INTO Prices (BasePrice) VALUES (15); -- -3
INSERT INTO Prices (BasePrice) VALUES (20); -- -4
INSERT INTO Prices (BasePrice) VALUES (60); -- -5
INSERT INTO Prices (BasePrice) VALUES (65); -- -6
INSERT INTO Prices (BasePrice) VALUES (89); -- -7
INSERT INTO Prices (BasePrice) VALUES (99); -- -8
INSERT INTO Prices (BasePrice) VALUES (109); -- -9

-- Insert Categories
INSERT INTO Categories (Name) VALUES ('Hamburgare');
INSERT INTO Categories (Name) VALUES ('Kyckling & Fisk');
INSERT INTO Categories (Name) VALUES ('Snacks');
INSERT INTO Categories (Name) VALUES ('Sides & Dip');
INSERT INTO Categories (Name) VALUES ('Sallad');
INSERT INTO Categories (Name) VALUES ('Kall Dryck');
INSERT INTO Categories (Name) VALUES ('Varm Dryck');
INSERT INTO Categories (Name) VALUES ('Dessert');

-- Insert Ingredients with display order index and price references
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Bröd', 1, 0, 1);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Vegetariskt Bröd', 1, 1, 2);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Köttburgare', 2, 0, 3);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Kycklingburgare', 2, 0, 4);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Växtbaserad hamburgare', 2, 1, 5);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Ost', 2, 0, 6);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Sallad', 1, 1, 7);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Tomat', 1, 1, 8);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Syltlök', 1, 1, 9);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Majonnäs', 1, 0, 10);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Quinoa', 1, 1, 11);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Avokado', 1, 1, 12);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Rödlök', 1, 1, 13);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Vegansk Majonnäs', 1, 1, 14);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Bacon', 2, 0, 15);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('BBQ-sås', 1, 0, 16);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Coleslaw', 1, 0, 17);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Ketchup', 1, 0, 18);
INSERT INTO Ingredients (Name, PriceId, Vegan, DisplayOrderIndex) VALUES ('Senap', 1, 0, 19);

-- Insert Productss
INSERT INTO Products (Name, Description, PriceId, CategoryId) 
VALUES ('Cheeseburgare', 'Saftig nötköttsbiff med smält cheddarost, sallad, tomat, syltlök och majonnäs på ett mjukt bröd.', 8, 1); -- PriceId 6 for price, CategoryId 1 for 'Hamburgare'
INSERT INTO Products (Name, Description, PriceId, CategoryId) 
VALUES ('Vegoburgare', 'Växtbaserad burgare med bönor, quinoa, avokado, sallad, tomat, rödlök och vegansk majonnäs på veganskt bröd.', 8, 1); -- PriceId 6 for price, CategoryId 1 for 'Hamburgare'
INSERT INTO Products (Name, Description, PriceId, CategoryId) 
VALUES ('ITHS-special', 'Nötköttsbiff, bacon, BBQ-sås, cheddarost och coleslaw på briochebröd. En exklusiv burgare för ITHS-studenter.', 9, 1); -- PriceId 6 for price, CategoryId 1 for 'Hamburgare'

-- Insert Beverages (Cola, Fanta)
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Cola', '', 4, 6); -- Cola
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Fanta', '', 4, 6); -- Fanta

-- Insert Pommes Frites (Liten, Medium, Stor)
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Pommes frites (Liten)', 'En liten portion krispiga pommes frites, perfekt som tillbehör till en måltid.', 2, 4); -- Pommes frites (Liten)
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Pommes Frites (Medium)', 'En medium portion pommes frites för dem som vill ha en lagom mängd.', 3, 4); -- Pommes Frites (Medium)
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Pommes Frites (Stor)', 'En stor portion krispiga pommes frites, idealisk för de som vill ha mer.', 4, 4); -- Pommes Frites (Stor)
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Hot Wing', 'Krispiga och heta kycklingvingar med en rökig BBQ-sås', 4, 2);

-- Insert Sauces (Barbequesås, Currysås, Cheddardipsås)
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Barbequesås', 'En rökig och söt sås som ger en extra dimension till dina favoriträtter.', 1, 4); -- Barbequesås
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Currysås', 'En kryddig och aromatisk sås som ger en indisk touch till dina måltider.', 1, 4); -- Currysås
INSERT INTO Products (Name, Description, PriceId, CategoryId) VALUES ('Cheddardipsås', 'En krämig och ostig sås med cheddar, perfekt för att doppa pommes frites eller grönsaksstavar.', 1, 4); -- Cheddardipsås

-- Insert FeaturedProducts
INSERT INTO FeaturedProducts (Title, ProductId) VALUES ('Exklusivt för ITHS-studenter', 3);

-- Cheeseburgare ProductIngredients
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (1, 3, 1, 1, 3); -- Köttburgare
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (1, 6, 1, 0, 3); -- Ost (Cheddarost)
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (1, 7, 1, 0, 3); -- Sallad
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (1, 8, 1, 0, 3); -- Tomat
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (1, 9, 1, 0, 3); -- Syltlök
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (1, 10, 1, 0, 1); -- Majonnäs

-- Vegoburgare ProductsIngredientss
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (2, 5, 1, 1, 3); -- Växtbaserad hamburgare
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (2, 11, 1, 0, 3); -- Quinoa
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (2, 12, 1, 0, 3); -- Avokado
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (2, 7, 1, 0, 3); -- Sallad
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (2, 8, 1, 0, 3); -- Tomat
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (2, 13, 1, 0, 3); -- Rödlök
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (2, 14, 1, 0, 1); -- Vegansk Majonnäs

-- ITHS-special ProductsIngredientss
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (3, 3, 1, 1, 3); -- Köttburgare
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (3, 15, 1, 0, 3); -- Bacon
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (3, 16, 1, 0, 1); -- BBQ-sås
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (3, 6, 1, 0, 3); -- Ost (Cheddarost)
INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES (3, 17, 1, 0, 3); -- Coleslaw

-- Insert Menus and link products
INSERT INTO Menus (Name, PriceId, DiscountId, CategoryId)
VALUES 
('Cheeseburgare Menu', 5, 1, 1),
('Vego Menu', 6, 1, 1),
('ITHS Menu', 7, 1, 1);

-- MenuProducts Insertion (link menus to productss)
INSERT INTO MenuProducts (MenuId, ProductId)
VALUES
(1, 1), -- Cheeseburgare in Cheeseburgare Menu
(1, 2), -- Vegoburgare in Cheeseburgare Menu
(2, 3); -- ITHS-special in ITHS Menu

-- Add default sides & drinks to menus (Pommes Medium, Cola)
INSERT INTO MenuProducts (MenuId, ProductId)
VALUES
(1, 7), -- Medium Fries in Cheeseburgare Menu
(2, 6), -- Medium Fries in Vego Menu
(3, 6), -- Medium Fries in ITHS Menu
(1, 6), -- Cola in Cheeseburgare Menu
(2, 6), -- Cola in Vego Menu
(3, 6); -- Cola in ITHS Menu

"@


# Establish database connection
$connection = New-Object System.Data.SqlClient.SqlConnection
$connection.ConnectionString = $ConnectionString

# Open the connection and execute the SQL queries
try {
    # Debugging: Check if the connection string is valid
    if ($connection.ConnectionString -eq "") {
        throw "Connection string is empty."
    }

    $connection.Open()
    Write-Host "Connection to the database successful!" -ForegroundColor Green

    $command = $connection.CreateCommand()
    $command.CommandText = $sqlQueries
    $command.ExecuteNonQuery()
    Write-Host "Database populated successfully!" -ForegroundColor Green
} catch {
    Write-Host "Error opening connection: $_" -ForegroundColor Red
    Write-Host "Stack Trace: $($_.Exception.StackTrace)" -ForegroundColor Yellow
} finally {
    $connection.Close()
}