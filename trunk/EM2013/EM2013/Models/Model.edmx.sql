



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/03/2012 10:37:05
-- Generated from EDMX file: D:\AlexK\projects\EM2013\EM2013\Models\Model.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `ProductItem` DROP CONSTRAINT `FK_ProductProductItem`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `ProductItem`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `HomePage` bool  NOT NULL,
    `Title` varchar( 255 )  NULL,
    `Text` longtext  NULL,
    `Name` varchar( 255 )  NULL,
    `SeoDescription` TEXT  NULL,
    `SeoKeywords` TEXT  NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 255 )  NOT NULL,
    `Name` varchar( 255 )  NOT NULL,
    `SeoDescription` TEXT  NULL,
    `SeoKeywords` TEXT  NULL,
    `SortOrder` int  NOT NULL,
    `Description` longtext  NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 255 )  NOT NULL,
    `Title` varchar( 255 )  NOT NULL,
    `Description` longtext  NULL,
    `ImageSource` TEXT  NOT NULL,
    `CategoryId` int  NOT NULL,
    `SortOrder` int  NOT NULL,
    `SeoDescription` TEXT  NULL,
    `SeoKeywords` TEXT  NULL,
    `Date` datetime  NOT NULL
);

-- Creating table 'ProductItem'

CREATE TABLE `ProductItem` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` TEXT  NULL,
    `Description` longtext  NULL,
    `Text` longtext  NULL,
    `ProductId` int  NOT NULL,
    `SortOrder` int  NOT NULL
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

-- Creating foreign key on `ProductId` in table 'ProductItem'

ALTER TABLE `ProductItem`
ADD CONSTRAINT `FK_ProductProductItem`
    FOREIGN KEY (`ProductId`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductItem'

CREATE INDEX `IX_FK_ProductProductItem` 
    ON `ProductItem`
    (`ProductId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
