BEGIN TRANSACTION;

-- Remember! Disable at end of script.
SET IDENTITY_INSERT Discounts ON;
SET IDENTITY_INSERT Prices ON;
SET IDENTITY_INSERT Categories ON;
SET IDENTITY_INSERT Ingredients ON;
SET IDENTITY_INSERT Products ON;
SET IDENTITY_INSERT Menus ON;

-- Reset database.
DELETE FROM Discounts,
            Prices,
            Categories,
            Ingredients,
            Products,
            FeaturedProducts,
            ProductIngredients,
            Menus,
            MenuProducts;

INSERT INTO Discounts (Id, Multiplier) VALUES
  (1, 0.7); -- 30% off

INSERT INTO Prices (Id, BasePrice) VALUES
  (1, 5),
  (2, 10),
  (3, 15),
  (4, 20),
  (5, 60),
  (6, 65),
  (7, 89),
  (8, 99),
  (9, 109);

INSERT INTO Categories (Id, Name) VALUES
  (1, 'Hamburgare'),
  (2, 'Kyckling & Fisk'),
  (3, 'Snacks'),
  (4, 'Sides & Dip'),
  (5, 'Sallad'),
  (6, 'Kall Dryck'),
  (7, 'Varm Dryck'),
  (8, 'Dessert');

INSERT INTO Ingredients (Id, Name, PriceId, Vegan, DisplayOrderIndex) VALUES
  (1, 'Bröd', 1, false, 1),
  (2, 'Veganskt bröd', 1, true, 2),
  (3, 'Köttburgare', 2, false, 3),
  (4, 'Kycklingburgare', 2, false, 4),
  (5, 'Växtbaserad hamburgare', 2, true, 5),
  (6, 'Ost', 2, false, 6),
  (7, 'Sallad', 1, true, 7),
  (8, 'Tomat', 1, true, 8),
  (9, 'Syltlök', 1, true, 9),
  (10, 'Majonnäs', 1, false, 10),
  (11, 'Quinoa', 1, true, 11),
  (12, 'Avokado', 1, true, 12),
  (13, 'Rödlök', 1, true, 13),
  (14, 'Vegansk majonnäs', 1, true, 14),
  (15, 'Bacon', 2, false, 15),
  (16, 'BBQ-sås', 1, false, 16),
  (17, 'Coleslaw', 1, false, 17),
  (18, 'Ketchup', 1, true, 18),
  (19, 'Senap', 1, true, 19);

INSERT INTO Products (Id, Name, Description, PriceId, CategoryId) VALUES
  (1, 'Cheeseburgare', 'Saftig nötköttsbiff med smält cheddarost, sallad, tomat, syltlök och majonnäs på ett mjukt bröd.', 8, 1),
  (2, 'Vegoburgare', 'Växtbaserad burgare med bönor, quinoa, avokado, sallad, tomat, rödlök och vegansk majonnäs på veganskt bröd.', 8, 1),
  (3, 'ITHS-special', 'Nötköttsbiff, bacon, BBQ-sås, cheddarost och coleslaw på briochebröd. En exklusiv burgare för ITHS-studenter.', 9, 1),
  (4, 'Coca Cola', '', 4, 6),
  (5, 'Fanta', '', 4, 6),
  (6, 'Pommes Frites (Liten)', 'En liten portion krispiga pommes frites, perfekt som tillbehör till en måltid.', 2, 4),
  (7, 'Pommes Frites (Medium)', 'En medium portion pommes frites för dem som vill ha en lagom mängd.', 3, 4),
  (8, 'Pommes Frites (Stor)', 'En stor portion krispiga pommes frites, idealisk för de som vill ha mer.', 4, 4),
  (9, 'Hot Wing', 'Krispiga och heta kycklingvingar med en rökig BBQ-sås', 4, 2),
  (10, 'Barbequesås', 'En rökig och söt sås som ger en extra dimension till dina favoriträtter.', 1, 4),
  (11, 'Currysås', 'En kryddig och aromatisk sås som ger en indisk touch till dina måltider.', 1, 4),
  (12, 'Cheddardipsås', 'En krämig och ostig sås med cheddar, perfekt för att doppa pommes frites eller grönsaksstavar.', 1, 4);

INSERT INTO FeaturedProducts (Title, ProductId) VALUES
  ('Exklusivt för ITHS-studenter', 3);

INSERT INTO ProductIngredients (ProductId, IngredientId, BaseAmount, MinAmount, MaxAmount) VALUES

  -- Ingredienser: Cheeseburgare
  (1, 3, 1, 1, 3), -- Köttburgare
  (1, 6, 1, 0, 3), -- Ost (Cheddarost)
  (1, 7, 1, 0, 3), -- Sallad
  (1, 8, 1, 0, 3), -- Tomat
  (1, 9, 1, 0, 3), -- Syltlök
  (1, 10, 1, 0, 1), -- Majonnäs

  -- Ingredienser: Vegoburgare
  (2, 5, 1, 1, 3), -- Växtbaserad hamburgare
  (2, 11, 1, 0, 3), -- Quinoa
  (2, 12, 1, 0, 3), -- Avokado
  (2, 7, 1, 0, 3), -- Sallad
  (2, 8, 1, 0, 3), -- Tomat
  (2, 13, 1, 0, 3), -- Rödlök
  (2, 14, 1, 0, 1), -- Vegansk majonnäs

  -- Ingredienser: ITHS-special
  (3, 3, 1, 1, 3), -- Köttburgare
  (3, 15, 1, 0, 3), -- Bacon
  (3, 16, 1, 0, 1), -- BBQ-sås
  (3, 6, 1, 0, 3), -- Ost (Cheddarost)
  (3, 17, 1, 0, 3); -- Coleslaw

INSERT INTO Menus (Id, Name, PriceId, DiscountId, CategoryId) VALUES
  (1, 'Cheeseburgarmeny', 5, 1, 1),
  (2, 'Vegomeny', 6, 1, 1),
  (3, 'ITHS-meny', 7, 1, 1);

INSERT INTO MenuProducts (MenuId, ProductId) VALUES
  -- Meny: Cheeseburgare
  (1, 1), -- Cheeseburgare
  (1, 2), -- Vegoburgare
  (1, 7), -- Medium Fries
  (1, 6), -- Cola

  -- Meny: Vego
  (2, 6), -- Medium Fries
  (2, 6), -- Cola

  -- Meny: ITHS
  (3, 3), -- ITHS-special
  (3, 6), -- Medium Fries
  (3, 6); -- Cola

SET IDENTITY_INSERT Discounts OFF;
SET IDENTITY_INSERT Prices OFF;
SET IDENTITY_INSERT Categories OFF;
SET IDENTITY_INSERT Ingredients OFF;
SET IDENTITY_INSERT Products OFF;
SET IDENTITY_INSERT Menus OFF;

COMMIT;
