BEGIN TRANSACTION;

-- Reset database.
DELETE FROM Discounts;
DELETE FROM Prices;
DELETE FROM Categories;
DELETE FROM Components;
DELETE FROM ComponentChildPolicies;
DELETE FROM FeaturedComponents;
DELETE FROM Images;

SET IDENTITY_INSERT Discounts ON;
INSERT INTO Discounts (Id, Multiplier) VALUES
  (1, 0.7); -- 30% off
SET IDENTITY_INSERT Discounts OFF;

SET IDENTITY_INSERT Prices ON;
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
SET IDENTITY_INSERT Prices OFF;

SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (Id, Name) VALUES
  (1, 'Hamburgare'),
  (2, 'Kyckling & Fisk'),
  (3, 'Snacks'),
  (4, 'Sides & Dip'),
  (5, 'Sallad'),
  (6, 'Kall Dryck'),
  (7, 'Varm Dryck'),
  (8, 'Dessert');
SET IDENTITY_INSERT Categories OFF;

-- Ingredients
SET IDENTITY_INSERT Components ON;
INSERT INTO Components (Id, Level, Name, PriceId, Vegan, DisplayOrderIndex) VALUES
  (1,  0, 'Bröd',                   1, 0, 1),
  (2,  0, 'Veganskt bröd',          1, 1, 2),
  (3,  0, 'Köttburgare',            2, 0, 3),
  (4,  0, 'Kycklingburgare',        2, 0, 4),
  (5,  0, 'Växtbaserad hamburgare', 2, 1, 5),
  (6,  0, 'Ost',                    2, 0, 6),
  (7,  0, 'Sallad',                 1, 1, 7),
  (8,  0, 'Tomat',                  1, 1, 8),
  (9,  0, 'Syltlök',                1, 1, 9),
  (10, 0, 'Majonnäs',               1, 0, 10),
  (11, 0, 'Quinoa',                 1, 1, 11),
  (12, 0, 'Avokado',                1, 1, 12),
  (13, 0, 'Rödlök',                 1, 1, 13),
  (14, 0, 'Vegansk majonnäs',       1, 1, 14),
  (15, 0, 'Bacon',                  2, 0, 15),
  (16, 0, 'BBQ-sås',                1, 0, 16),
  (17, 0, 'Coleslaw',               1, 0, 17),
  (18, 0, 'Ketchup',                1, 1, 18),
  (19, 0, 'Senap',                  1, 1, 19);

-- Products
INSERT INTO Components (Id, Level, Name, Description, PriceId) VALUES
  (20, 1, 'Cheeseburgare',          'Saftig nötköttsbiff med smält cheddarost, sallad, tomat, syltlök och majonnäs på ett mjukt bröd.',              8),
  (21, 1, 'Vegoburgare',            'Växtbaserad burgare med bönor, quinoa, avokado, sallad, tomat, rödlök och vegansk majonnäs på veganskt bröd.',  8),
  (22, 1, 'ITHS-special',           'Nötköttsbiff, bacon, BBQ-sås, cheddarost och coleslaw på briochebröd. En exklusiv burgare för ITHS-studenter.', 9),
  (23, 1, 'Coca Cola',              '',                                                                                                              4),
  (24, 1, 'Fanta',                  '',                                                                                                              4),
  (25, 1, 'Pommes Frites (Liten)',  'En liten portion krispiga pommes frites, perfekt som tillbehör till en måltid.',                                2),
  (26, 1, 'Pommes Frites (Medium)', 'En medium portion pommes frites för dem som vill ha en lagom mängd.',                                           3),
  (27, 1, 'Pommes Frites (Stor)',   'En stor portion krispiga pommes frites, idealisk för de som vill ha mer.',                                      4),
  (28, 1, 'Barbequesås',            'En rökig och söt sås som ger en extra dimension till dina favoriträtter.',                                      1),
  (29, 1, 'Currysås',               'En kryddig och aromatisk sås som ger en indisk touch till dina måltider.',                                      1),
  (30, 1, 'Cheddardipsås',          'En krämig och ostig sås med cheddar, perfekt för att doppa pommes frites eller grönsaksstavar.',                1);

INSERT INTO Components (Id, Level, Name, Description, PriceId, Vegan) VALUES
  (60, 1, 'Hot Wing', 'Krispiga och heta kycklingvingar med en rökig BBQ-sås', 4, 0);

INSERT INTO Components (Id, Level, Name, PriceId) VALUES
  (31, 2, 'Cheeseburgarmeny', 5),
  (32, 2, 'Vegomeny',         6),
  (33, 2, 'ITHS-meny',        7);
SET IDENTITY_INSERT Components OFF;

INSERT INTO FeaturedComponents (Title, ComponentId) VALUES
  ('Exklusivt för ITHS-studenter', 22);

-- Default defaults to 0
-- Min defaults to 0
-- Max defaults to Default

-- Optional children (Min = 0)
INSERT INTO ComponentChildPolicies (ParentId, ChildId, [Default], [Max]) VALUES
  -- Ingredienser: Cheeseburgare
  (20, 1,  2, 2), -- Bröd
  (20, 6,  1, 3), -- Ost
  (20, 7,  1, 3), -- Sallad
  (20, 8,  1, 3), -- Tomat
  (20, 9,  1, 3), -- Syltlök
  (20, 10, 1, 1), -- Majonnäs

  -- Ingredienser: Vegoburgare
  (21, 2,  2, 2), -- Veganskt bröd
  (21, 11, 1, 3), -- Quinoa
  (21, 12, 1, 3), -- Avokado
  (21, 7,  1, 3), -- Sallad
  (21, 8,  1, 3), -- Tomat
  (21, 13, 1, 3), -- Rödlök
  (21, 14, 1, 1), -- Vegansk majonnäs

  -- Ingredienser: ITHS-special
  (22, 1,  1, 3), -- Bröd
  (22, 15, 1, 3), -- Bacon
  (22, 16, 1, 1), -- BBQ-sås
  (22, 6,  1, 3), -- Ost
  (22, 17, 1, 3); -- Coleslaw

-- Required children (Min > 0)
INSERT INTO ComponentChildPolicies (ParentId, ChildId, [Default], [Min], [Max]) VALUES
  (20, 3,  1, 1, 3), -- Köttburgare
  (21, 5,  1, 1, 3), -- Växtbaserad hamburgare
  (22, 3,  1, 1, 3); -- Köttburgare

-- Menus
INSERT INTO ComponentChildPolicies (ParentId, ChildId, [Default], [Min]) VALUES
  -- Cheeseburgarmeny
  (31, 20, 1, 1), -- Cheeseburgare
  (31, 26, 1, 1), -- Pommes Frites (Medium)
  (31, 23, 1, 1), -- Coca Cola

  -- Vegoburgarmeny
  (32, 21, 1, 1), -- Vegoburgare
  (32, 26, 1, 1), -- Pommes Frites (Medium)
  (32, 23, 1, 1), -- Coca Cola

  -- Vegoburgarmeny
  (33, 22, 1, 1), -- ITHS-special
  (33, 26, 1, 1), -- Pommes Frites (Medium)
  (33, 23, 1, 1); -- Coca Cola

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

  (1, 31),
  (1, 32),
  (1, 33);

-- TODO add discounts

COMMIT;
