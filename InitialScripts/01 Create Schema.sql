-- ===================================================================
-- 0. ОЧИСТКА СТАРОЙ СХЕМЫ (Удаление таблиц, если они уже существуют)
-- ===================================================================
-- Удаляем в обратном порядке (сначала зависимые, потом справочники)
DROP TABLE IF EXISTS WorkerCageAssignments;
DROP TABLE IF EXISTS Chickens;
DROP TABLE IF EXISTS Cages;
DROP TABLE IF EXISTS Shops;
DROP TABLE IF EXISTS Breeds;
DROP TABLE IF EXISTS Diets;
DROP TABLE IF EXISTS Workers;

-- ===================================================================
-- 1. БЛОК СПРАВОЧНИКОВ (dict) — Ровно 5 таблиц
-- ===================================================================

-- Справочник 1: Рекомендованные диеты
CREATE TABLE Diets (
    DietId BIGINT IDENTITY(1,1) PRIMARY KEY,
    DietNumber INT NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL
);

-- Справочник 2: Породы кур
CREATE TABLE Breeds (
    BreedId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    AvgEggsPerMonth INT NOT NULL,
    AvgWeight FLOAT NOT NULL,
    DietId BIGINT NOT NULL,
    CONSTRAINT FK_Breeds_Diets FOREIGN KEY (DietId) REFERENCES Diets(DietId)
);

-- Справочник 3: Цеха птицефабрики
CREATE TABLE Shops (
    ShopId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ShopNumber INT NOT NULL UNIQUE,
    Name NVARCHAR(100) NULL
);

-- Справочник 4: Физические клетки
CREATE TABLE Cages (
    CageId BIGINT IDENTITY(1,1) PRIMARY KEY,
    RowNumber INT NOT NULL,
    CageNumber INT NOT NULL,
    ShopId BIGINT NOT NULL,
    CONSTRAINT FK_Cages_Shops FOREIGN KEY (ShopId) REFERENCES Shops(ShopId) ON DELETE CASCADE
);

-- Справочник 5: Работники
CREATE TABLE Workers (
    WorkerId BIGINT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL, -- Заменили PassportData на FullName
    Salary DECIMAL(18,2) NOT NULL
);

-- ===================================================================
-- 2. БЛОК ОСНОВНЫХ ТАБЛИЦ (dbo) — Ровно 2 таблицы
-- ===================================================================

-- Основная таблица 1: Куры (учет поголовья и производительности)
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

-- Основная таблица 2: Журнал закрепления клеток за работниками
CREATE TABLE WorkerCageAssignments (
    AssignmentId BIGINT IDENTITY(1,1) PRIMARY KEY,
    WorkerId BIGINT NOT NULL,
    CageId BIGINT NOT NULL,
    AssignedDate DATE NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Assignments_Workers FOREIGN KEY (WorkerId) REFERENCES Workers(WorkerId) ON DELETE CASCADE,
    CONSTRAINT FK_Assignments_Cages FOREIGN KEY (CageId) REFERENCES Cages(CageId) ON DELETE CASCADE,
    CONSTRAINT UQ_Worker_Cage UNIQUE (WorkerId, CageId)
);