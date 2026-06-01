-- Добавляем Породы
INSERT INTO Breeds (Name, AvgEggsPerMonth, AvgWeight, DietNumber) VALUES
('Леггорн', 25, 2.1, 1),
('Хайсекс', 28, 2.4, 2),
('Ломан Браун', 26, 2.2, 1);

-- Добавляем Работников
INSERT INTO Workers (PassportData, Salary) VALUES
('1111 222333 Иванов И.И.', 45000.00),
('4444 555666 Петров П.П.', 48000.00);

-- Добавляем Клетки (Цех, Ряд, Номер, ID Работника)
-- Иванов (WorkerId = 1) обслуживает Цех 1
INSERT INTO Cages (ShopNumber, RowNumber, CageNumber, WorkerId) VALUES
(1, 1, 1, 1),
(1, 1, 2, 1),
(1, 2, 1, 1);

-- Петров (WorkerId = 2) обслуживает Цех 2
INSERT INTO Cages (ShopNumber, RowNumber, CageNumber, WorkerId) VALUES
(2, 1, 1, 2),
(2, 1, 2, 2);

-- Добавляем Кур (Вес, Возраст(мес), Яиц в мес, ID Породы, ID Клетки)
INSERT INTO Chickens (Weight, Age, EggsPerMonth, BreedId, CageId) VALUES
(2.2, 12, 26, 1, 1),
(2.0, 10, 24, 1, 2),
(2.5, 8,  29, 2, 3),
(2.4, 14, 27, 2, 4),
(2.3, 11, 25, 3, 5);