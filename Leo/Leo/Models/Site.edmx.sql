



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 08/09/2012 23:58:35
-- Generated from EDMX file: D:\projects\Leo\Leo\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `leo`;
CREATE DATABASE `leo`;
USE `leo`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `CategoryProductAttribute` DROP CONSTRAINT `FK_CategoryProductAttribute_Category`;
--    ALTER TABLE `CategoryProductAttribute` DROP CONSTRAINT `FK_CategoryProductAttribute_ProductAttribute`;
--    ALTER TABLE `ProductProductAttribute` DROP CONSTRAINT `FK_ProductProductAttribute_Product`;
--    ALTER TABLE `ProductProductAttribute` DROP CONSTRAINT `FK_ProductProductAttribute_ProductAttribute`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `ProductAttribute`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Feedback`;
    DROP TABLE IF EXISTS `CategoryProductAttribute`;
    DROP TABLE IF EXISTS `ProductProductAttribute`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Description` longtext  NULL,
    `ImageSource` TEXT  NOT NULL,
    `CategoryId` int  NOT NULL
);

-- Creating table 'ProductAttribute'

CREATE TABLE `ProductAttribute` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 100 )  NOT NULL,
    `Text` longtext  NOT NULL
);

-- Creating table 'Feedback'

CREATE TABLE `Feedback` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Email` longtext  NULL,
    `Text` longtext  NOT NULL,
    `Title` longtext  NOT NULL,
    `ErrorMessage` longtext  NULL,
    `Sent` bool  NOT NULL
);

-- Creating table 'CategoryProductAttribute'

CREATE TABLE `CategoryProductAttribute` (
    `Categories_Id` int  NOT NULL,
    `ProductAttributes_Id` int  NOT NULL
);

-- Creating table 'ProductProductAttribute'

CREATE TABLE `ProductProductAttribute` (
    `Products_Id` int  NOT NULL,
    `ProductAttributes_Id` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `Categories_Id`, `ProductAttributes_Id` in table 'CategoryProductAttribute'

ALTER TABLE `CategoryProductAttribute`
ADD CONSTRAINT `PK_CategoryProductAttribute`
    PRIMARY KEY (`Categories_Id`, `ProductAttributes_Id` );

-- Creating primary key on `Products_Id`, `ProductAttributes_Id` in table 'ProductProductAttribute'

ALTER TABLE `ProductProductAttribute`
ADD CONSTRAINT `PK_ProductProductAttribute`
    PRIMARY KEY (`Products_Id`, `ProductAttributes_Id` );



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

-- Creating foreign key on `Categories_Id` in table 'CategoryProductAttribute'

ALTER TABLE `CategoryProductAttribute`
ADD CONSTRAINT `FK_CategoryProductAttribute_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `ProductAttributes_Id` in table 'CategoryProductAttribute'

ALTER TABLE `CategoryProductAttribute`
ADD CONSTRAINT `FK_CategoryProductAttribute_ProductAttribute`
    FOREIGN KEY (`ProductAttributes_Id`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProductAttribute_ProductAttribute'

CREATE INDEX `IX_FK_CategoryProductAttribute_ProductAttribute` 
    ON `CategoryProductAttribute`
    (`ProductAttributes_Id`);

-- Creating foreign key on `Products_Id` in table 'ProductProductAttribute'

ALTER TABLE `ProductProductAttribute`
ADD CONSTRAINT `FK_ProductProductAttribute_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `ProductAttributes_Id` in table 'ProductProductAttribute'

ALTER TABLE `ProductProductAttribute`
ADD CONSTRAINT `FK_ProductProductAttribute_ProductAttribute`
    FOREIGN KEY (`ProductAttributes_Id`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductAttribute_ProductAttribute'

CREATE INDEX `IX_FK_ProductProductAttribute_ProductAttribute` 
    ON `ProductProductAttribute`
    (`ProductAttributes_Id`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
