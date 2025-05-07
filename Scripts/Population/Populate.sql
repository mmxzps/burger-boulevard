BEGIN TRANSACTION;

-- Reset database.
DELETE FROM Allergens;
DELETE FROM Categories;
DELETE FROM Components;
DELETE FROM Discounts;
DELETE FROM ComponentChildPolicies;
DELETE FROM FeaturedComponents;
DELETE FROM Images;

SET IDENTITY_INSERT Discounts ON;
INSERT INTO Discounts (Id, Multiplier) VALUES
  (1, 0.7); -- 30% off
SET IDENTITY_INSERT Discounts OFF;

SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (Id, Name) VALUES
  (0, 'Menyer'),
  (1, 'Hamburgare'),
  (2, 'Kyckling & Fisk'),
  (3, 'Snacks'),
  (4, 'Sides & Dip'),
  (5, 'Sallad'),
  (6, 'Kall Dryck'),
  (7, 'Varm Dryck'),
  (8, 'Dessert');
SET IDENTITY_INSERT Categories OFF;

SET IDENTITY_INSERT Allergens ON;
INSERT INTO Allergens (Id, Name) VALUES
  (1, 'Kött'),
  (2, 'Fågel'),
  (3, 'Gluten'),
  (4, 'Laktos'),
  (5, 'Ägg'),
  (6, 'Mjölkprotein');
SET IDENTITY_INSERT Allergens OFF;

-- Ingredients
SET IDENTITY_INSERT Components ON;
INSERT INTO Components (Id, Level, Name, Price, Independent) VALUES
  (1,  0, 'Bröd',                   5,  0),
  (2,  0, 'Veganskt bröd',          5,  0),
  (3,  0, 'Köttburgare',            10, 0),
  (4,  0, 'Kycklingburgare',        10, 0),
  (5,  0, 'Växtbaserad hamburgare', 10, 0),
  (6,  0, 'Ost',                    10, 0),
  (7,  0, 'Sallad',                 5,  0),
  (8,  0, 'Tomat',                  5,  0),
  (9,  0, 'Syltlök',                5,  0),
  (10, 0, 'Majonnäs',               5,  0),
  (11, 0, 'Quinoa',                 5,  0),
  (12, 0, 'Avokado',                5,  0),
  (13, 0, 'Rödlök',                 5,  0),
  (14, 0, 'Vegansk majonnäs',       5,  0),
  (15, 0, 'Bacon',                  10, 0),
  (16, 0, 'BBQ-sås',                5,  0),
  (17, 0, 'Coleslaw',               5,  0),
  (18, 0, 'Ketchup',                5,  0),
  (19, 0, 'Senap',                  5,  0);

INSERT INTO AllergenComponent (ComponentsId, AllergensId) VALUES
  (1, 3),
  (3, 1),
  (4, 2),
  (6, 4),
  (10, 5),
  (15, 1),
  (17, 4),
  (17, 6);

-- Products
INSERT INTO Components (Id, Level, Name, Description, Price, Independent) VALUES
  (20, 1, 'Cheeseburgare',          'Saftig nötköttsbiff med smält cheddarost, sallad, tomat, syltlök och majonnäs på ett mjukt bröd.',              99,  1),
  (21, 1, 'Vegoburgare',            'Växtbaserad burgare med bönor, quinoa, avokado, sallad, tomat, rödlök och vegansk majonnäs på veganskt bröd.',  99,  1),
  (22, 1, 'ITHS-special',           'Nötköttsbiff, bacon, BBQ-sås, cheddarost och coleslaw på briochebröd. En exklusiv burgare för ITHS-studenter.', 109, 1),
  (23, 1, 'Coca Cola',              '',                                                                                                              20,  1),
  (24, 1, 'Fanta',                  '',                                                                                                              20,  1),
  (25, 1, 'Pommes Frites (Liten)',  'En liten portion krispiga pommes frites, perfekt som tillbehör till en måltid.',                                10,  1),
  (26, 1, 'Pommes Frites (Medium)', 'En medium portion pommes frites för dem som vill ha en lagom mängd.',                                           15,  1),
  (27, 1, 'Pommes Frites (Stor)',   'En stor portion krispiga pommes frites, idealisk för de som vill ha mer.',                                      20,  1),
  (28, 1, 'Barbequesås',            'En rökig och söt sås som ger en extra dimension till dina favoriträtter.',                                      5,   1),
  (29, 1, 'Currysås',               'En kryddig och aromatisk sås som ger en indisk touch till dina måltider.',                                      5,   1),
  (30, 1, 'Cheddardipsås',          'En krämig och ostig sås med cheddar, perfekt för att doppa pommes frites eller grönsaksstavar.',                5,   1);

INSERT INTO Components (Id, Level, Name, Description, Price, Independent) VALUES
  (60, 1, 'Hot Wings', 'Krispiga och heta kycklingvingar med en rökig BBQ-sås', 20, 1);

INSERT INTO Components (Id, Level, Name, Price, Independent) VALUES
  (31, 2, 'Cheeseburgarmeny', 60, 1),
  (32, 2, 'Vegomeny',         65, 1),
  (33, 2, 'ITHS-meny',        89, 1);
SET IDENTITY_INSERT Components OFF;

UPDATE Components SET DiscountId = 1 WHERE Id = 24;
UPDATE Components SET Vat = 0.12;

INSERT INTO FeaturedComponents (Title, ComponentId) VALUES
  ('Exklusivt för ITHS-studenter', 22);

-- Policies
INSERT INTO ComponentChildPolicies (ParentId, ChildId, [Default], [Min], [Max]) VALUES
  -- Ingredienser: Cheeseburgare
  (20, 3,  1, 1, 3), -- Köttburgare
  (20, 1,  2, 0, 2), -- Bröd
  (20, 6,  1, 0, 3), -- Ost
  (20, 7,  1, 0, 3), -- Sallad
  (20, 8,  1, 0, 3), -- Tomat
  (20, 9,  1, 0, 3), -- Syltlök
  (20, 10, 1, 0, 1), -- Majonnäs

  -- Ingredienser: Vegoburgare
  (21, 5,  1, 1, 3), -- Växtbaserad hamburgare
  (21, 2,  2, 0, 2), -- Veganskt bröd
  (21, 11, 1, 0, 3), -- Quinoa
  (21, 12, 1, 0, 3), -- Avokado
  (21, 7,  1, 0, 3), -- Sallad
  (21, 8,  1, 0, 3), -- Tomat
  (21, 13, 1, 0, 3), -- Rödlök
  (21, 14, 1, 0, 1), -- Vegansk majonnäs

  -- Ingredienser: ITHS-special
  (22, 3,  1, 1, 3), -- Köttburgare
  (22, 1,  1, 0, 3), -- Bröd
  (22, 15, 1, 0, 3), -- Bacon
  (22, 16, 1, 0, 1), -- BBQ-sås
  (22, 6,  1, 0, 3), -- Ost
  (22, 17, 1, 0, 3); -- Coleslaw

-- Menus
INSERT INTO ComponentChildPolicies (ParentId, ChildId, [Default], [Min], [Max]) VALUES
  -- Cheeseburgarmeny
  (31, 20, 1, 1, 1), -- Cheeseburgare
  (31, 26, 1, 1, 1), -- Pommes Frites (Medium)
  (31, 23, 1, 1, 1), -- Coca Cola

  -- Vegoburgarmeny
  (32, 21, 1, 1, 1), -- Vegoburgare
  (32, 26, 1, 1, 1), -- Pommes Frites (Medium)
  (32, 23, 1, 1, 1), -- Coca Cola

  -- Vegoburgarmeny
  (33, 22, 1, 1, 1), -- ITHS-special
  (33, 26, 1, 1, 1), -- Pommes Frites (Medium)
  (33, 23, 1, 1, 1); -- Coca Cola

INSERT INTO CategoryComponent (CategoriesId, ComponentsId) VALUES
  (1, 20),
  (1, 21),
  (1, 22),
  (6, 23),
  (6, 24),
  (4, 25),
  (4, 26),
  (4, 27),
  (4, 28),
  (4, 29),
  (4, 30),
  (2, 60),

  (0, 31),
  (0, 32),
  (0, 33),
  (1, 31),
  (1, 32),
  (1, 33);

COMMIT;
