



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/19/2013 13:24:15
-- Generated from EDMX file: D:\AlexK\projects\Kulumu2013\Kulumu\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `ProductSize` DROP CONSTRAINT `FK_CategoryProductSize`;
--    ALTER TABLE `Category` DROP CONSTRAINT `FK_CategoryCategory`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `ProductSize`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Description` longtext  NOT NULL,
    `BottomDescriptionTitle` varchar( 200 )  NOT NULL,
    `BottomDescription` longtext  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` TEXT  NULL,
    `CategoryId` int  NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Description` longtext  NOT NULL,
    `ImageSource` TEXT  NOT NULL,
    `CategoryId` int  NOT NULL,
    `Discount` bool  NOT NULL,
    `DiscountText` longtext  NULL,
    `Price` varchar( 100 )  NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Description` longtext  NOT NULL,
    `DescriptionTitle` varchar( 200 )  NOT NULL,
    `MainPage` bool  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` TEXT  NULL,
    `Text` longtext  NOT NULL
);

-- Creating table 'Article'

CREATE TABLE `Article` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Date` datetime  NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Text` longtext  NOT NULL,
    `Description` longtext  NOT NULL,
    `OldPrice` varchar( 100 )  NOT NULL,
    `NewPrice` varchar( 100 )  NOT NULL,
    `ImageSource` longtext  NOT NULL
);

-- Creating table 'ProductSize'

CREATE TABLE `ProductSize` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Size` varchar( 50 )  NOT NULL,
    `CategoryId` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `CategoryId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_CategoryProduct`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProduct'

CREATE INDEX `IX_FK_CategoryProduct` 
    ON `Product`
    (`CategoryId`);

-- Creating foreign key on `CategoryId` in table 'ProductSize'

ALTER TABLE `ProductSize`
ADD CONSTRAINT `FK_CategoryProductSize`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProductSize'

CREATE INDEX `IX_FK_CategoryProductSize` 
    ON `ProductSize`
    (`CategoryId`);

-- Creating foreign key on `CategoryId` in table 'Category'

ALTER TABLE `Category`
ADD CONSTRAINT `FK_CategoryCategory`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategory'

CREATE INDEX `IX_FK_CategoryCategory` 
    ON `Category`
    (`CategoryId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
