-- 1. Создание таблицы "Породы"
CREATE TABLE Breeds (
    BreedId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    AvgEggsPerMonth INT NOT NULL,
    AvgWeight FLOAT NOT NULL,
    DietNumber INT NOT NULL
);

-- 2. Создание таблицы "Работники"
CREATE TABLE Workers (
    WorkerId BIGINT IDENTITY(1,1) PRIMARY KEY,
    PassportData NVARCHAR(255) NOT NULL,
    Salary DECIMAL(18,2) NOT NULL
);

-- 3. Создание таблицы "Клетки"
-- Клетка ссылается на Работника (WorkerId)
CREATE TABLE Cages (
    CageId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ShopNumber INT NOT NULL,
    RowNumber INT NOT NULL,
    CageNumber INT NOT NULL,
    WorkerId BIGINT NULL,
    CONSTRAINT FK_Cages_Workers FOREIGN KEY (WorkerId) REFERENCES Workers(WorkerId) ON DELETE SET NULL
);

-- 4. Создание таблицы "Куры"
-- Курица ссылается на Породу (BreedId) и Клетку (CageId)
CREATE TABLE Chickens (
    ChickenId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Weight FLOAT NOT NULL,
    Age INT NOT NULL,
    EggsPerMonth INT NOT NULL,
    BreedId BIGINT NULL,
    CageId BIGINT NULL,
    CONSTRAINT FK_Chickens_Breeds FOREIGN KEY (BreedId) REFERENCES Breeds(BreedId) ON DELETE SET NULL,
    CONSTRAINT FK_Chickens_Cages FOREIGN KEY (CageId) REFERENCES Cages(CageId) ON DELETE SET NULL
);