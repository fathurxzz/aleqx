



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/17/2012 13:06:39
-- Generated from EDMX file: D:\projects\Shop\Shop\Models\Shop.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Products` DROP CONSTRAINT `FK_CategoryProduct`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Products`;
    DROP TABLE IF EXISTS `Categories`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'

CREATE TABLE `Products` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `ShortDescription` longtext  NULL,
    `Description` longtext  NULL,
    `IsNew` bool  NOT NULL,
    `IsSpecialOffer` bool  NOT NULL,
    `Published` bool  NOT NULL,
    `SortOrder` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `Price` decimal(10,0)  NOT NULL,
    `OldPrice` decimal(10,0)  NOT NULL,
    `CategoryId` int  NOT NULL
);

-- Creating table 'Categories'

CREATE TABLE `Categories` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `CategoryId` in table 'Products'

ALTER TABLE `Products`
ADD CONSTRAINT `FK_CategoryProduct`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Categories`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProduct'

CREATE INDEX `IX_FK_CategoryProduct` 
    ON `Products`
    (`CategoryId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
