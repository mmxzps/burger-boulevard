BEGIN TRANSACTION;

-- Remember! Disable at end of script.
SET IDENTITY_INSERT Discounts ON;
SET IDENTITY_INSERT Prices ON;
SET IDENTITY_INSERT Categories ON;
SET IDENTITY_INSERT Components ON;

-- Reset database.
DELETE FROM Discounts,
            Prices,
            Categories,
            Components,
            FeaturedComponents,
            ComponentChildren;

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

INSERT INTO Components (Id, ComponentLevel, Name, PriceId, Vegan, DisplayOrderIndex) VALUES
  (1,  0, 'Bröd',                   1, false, 1),
  (2,  0, 'Veganskt bröd',          1, true,  2),
  (3,  0, 'Köttburgare',            2, false, 3),
  (4,  0, 'Kycklingburgare',        2, false, 4),
  (5,  0, 'Växtbaserad hamburgare', 2, true,  5),
  (6,  0, 'Ost',                    2, false, 6),
  (7,  0, 'Sallad',                 1, true,  7),
  (8,  0, 'Tomat',                  1, true,  8),
  (9,  0, 'Syltlök',                1, true,  9),
  (10, 0, 'Majonnäs',               1, false, 10),
  (11, 0, 'Quinoa',                 1, true,  11),
  (12, 0, 'Avokado',                1, true,  12),
  (13, 0, 'Rödlök',                 1, true,  13),
  (14, 0, 'Vegansk majonnäs',       1, true,  14),
  (15, 0, 'Bacon',                  2, false, 15),
  (16, 0, 'BBQ-sås',                1, false, 16),
  (17, 0, 'Coleslaw',               1, false, 17),
  (18, 0, 'Ketchup',                1, true,  18),
  (19, 0, 'Senap',                  1, true,  19);

INSERT INTO Components (Id, ComponentLevel, Name, Description, PriceId) VALUES
  (20, 1, 'Cheeseburgare',          'Saftig nötköttsbiff med smält cheddarost, sallad, tomat, syltlök och majonnäs på ett mjukt bröd.',              8, 1),
  (21, 1, 'Vegoburgare',            'Växtbaserad burgare med bönor, quinoa, avokado, sallad, tomat, rödlök och vegansk majonnäs på veganskt bröd.',  8, 1),
  (22, 1, 'ITHS-special',           'Nötköttsbiff, bacon, BBQ-sås, cheddarost och coleslaw på briochebröd. En exklusiv burgare för ITHS-studenter.', 9, 1),
  (23, 1, 'Coca Cola',              '',                                                                                                              4, 6),
  (24, 1, 'Fanta',                  '',                                                                                                              4, 6),
  (25, 1, 'Pommes Frites (Liten)',  'En liten portion krispiga pommes frites, perfekt som tillbehör till en måltid.',                                2, 4),
  (26, 1, 'Pommes Frites (Medium)', 'En medium portion pommes frites för dem som vill ha en lagom mängd.',                                           3, 4),
  (27, 1, 'Pommes Frites (Stor)',   'En stor portion krispiga pommes frites, idealisk för de som vill ha mer.',                                      4, 4),
  (28, 1, 'Barbequesås',            'En rökig och söt sås som ger en extra dimension till dina favoriträtter.',                                      1, 4),
  (29, 1, 'Currysås',               'En kryddig och aromatisk sås som ger en indisk touch till dina måltider.',                                      1, 4),
  (30, 1, 'Cheddardipsås',          'En krämig och ostig sås med cheddar, perfekt för att doppa pommes frites eller grönsaksstavar.',                1, 4);

INSERT INTO Components (Id, ComponentLevel, Name, Description, PriceId, Vegan) VALUES
  (30, 1, 'Hot Wing', 'Krispiga och heta kycklingvingar med en rökig BBQ-sås', 4, false, 2);

INSERT INTO Components (Id, ComponentLevel, Name, PriceId) VALUES
  (31, 2, 'Cheeseburgarmeny', 5, 1),
  (32, 2, 'Vegomeny',         6, 1),
  (33, 2, 'ITHS-meny',        7, 1);

INSERT INTO FeaturedComponents (Title, ComponentId) VALUES
  ('Exklusivt för ITHS-studenter', 22);

-- Default defaults to 0
-- Min defaults to 0
-- Max defaults to Default

-- Optional children (Min = 0)
INSERT INTO ComponentChildren (ParentId, ChildId, Default, Max) VALUES
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
INSERT INTO ComponentChildren (ParentId, ChildId, Default, Min, Max) VALUES
  (20, 3,  1, 1, 3), -- Köttburgare
  (21, 5,  1, 1, 3), -- Växtbaserad hamburgare
  (22, 3,  1, 1, 3); -- Köttburgare

-- Menus
INSERT INTO ComponentChildren (ParentId, ChildId, BaseAmount, MinAmount)
SELECT * FROM
  (SELECT Id AS ParentId FROM Components WHERE Name = 'Cheeseburgarmeny'),
  1 AS BaseAmount, 1 AS MinAmount,
  (SELECT Id AS ChildId FROM Components WHERE
    Name IN ('Cheeseburgare', 'Pommes Frites (Medium)', 'Coca Cola'));

INSERT INTO ComponentChildren (ParentId, ChildId, BaseAmount, MinAmount)
SELECT * FROM
  (SELECT Id AS ParentId FROM Components WHERE Name = 'Vegoburgarmeny'),
  1 AS BaseAmount, 1 AS MinAmount,
  (SELECT Id AS ChildId FROM Components WHERE
    Name IN ('Vegoburgare', 'Pommes Frites (Medium)', 'Coca Cola'));

INSERT INTO ComponentChildren (ParentId, ChildId, BaseAmount, MinAmount)
SELECT * FROM
  (SELECT Id AS ParentId FROM Components WHERE Name = 'ITHS-meny'),
  1 AS BaseAmount, 1 AS MinAmount,
  (SELECT Id AS ChildId FROM Components WHERE
    Name IN ('ITHS-special', 'Pommes Frites (Medium)', 'Coca Cola'));

-- TODO add discounts and categories

SET IDENTITY_INSERT Discounts OFF;
SET IDENTITY_INSERT Prices OFF;
SET IDENTITY_INSERT Categories OFF;
SET IDENTITY_INSERT Components OFF;

COMMIT;
