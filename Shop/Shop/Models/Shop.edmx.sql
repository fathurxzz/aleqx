



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 09/18/2012 12:03:28
-- Generated from EDMX file: D:\projects\Shop\Shop\Models\Shop.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `toyplanet`;
CREATE DATABASE `toyplanet`;
USE `toyplanet`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `Category` DROP CONSTRAINT `FK_CategoryCategory`;
--    ALTER TABLE `ProductAttributeCategory` DROP CONSTRAINT `FK_ProductAttributeCategory_ProductAttribute`;
--    ALTER TABLE `ProductAttributeCategory` DROP CONSTRAINT `FK_ProductAttributeCategory_Category`;
--    ALTER TABLE `ProductAttributeValues` DROP CONSTRAINT `FK_ProductAttributeProductAttributeValues`;
--    ALTER TABLE `ProductAttributeValuesProduct` DROP CONSTRAINT `FK_ProductAttributeValuesProduct_ProductAttributeValues`;
--    ALTER TABLE `ProductAttributeValuesProduct` DROP CONSTRAINT `FK_ProductAttributeValuesProduct_Product`;
--    ALTER TABLE `ProductImage` DROP CONSTRAINT `FK_ProductProductImage`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_BrandProduct`;
--    ALTER TABLE `TagProduct` DROP CONSTRAINT `FK_TagProduct_Tag`;
--    ALTER TABLE `TagProduct` DROP CONSTRAINT `FK_TagProduct_Product`;
--    ALTER TABLE `ProductAttributeStaticValues` DROP CONSTRAINT `FK_ProductAttributeProductAttributeStaticValues`;
--    ALTER TABLE `ProductAttributeStaticValues` DROP CONSTRAINT `FK_ProductAttributeStaticValuesProduct`;
--    ALTER TABLE `Comment` DROP CONSTRAINT `FK_CommentComment`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `ProductAttribute`;
    DROP TABLE IF EXISTS `ProductAttributeValues`;
    DROP TABLE IF EXISTS `ProductImage`;
    DROP TABLE IF EXISTS `Brand`;
    DROP TABLE IF EXISTS `Tag`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `ProductAttributeStaticValues`;
    DROP TABLE IF EXISTS `Wish`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `Comment`;
    DROP TABLE IF EXISTS `ProductAttributeCategory`;
    DROP TABLE IF EXISTS `ProductAttributeValuesProduct`;
    DROP TABLE IF EXISTS `TagProduct`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Product'

CREATE TABLE `Product` (
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
    `CategoryId` int  NOT NULL,
    `BrandId` int  NOT NULL,
    `Title` mediumtext  NOT NULL,
    `SeoText` longtext  NULL,
    `Articul` mediumtext  NOT NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `CategoryId` int  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `SortOrder` int  NOT NULL,
    `Title` mediumtext  NOT NULL,
    `ImageSource` mediumtext  NULL,
    `SeoText` longtext  NULL,
    `Description` longtext  NULL
);

-- Creating table 'ProductAttribute'

CREATE TABLE `ProductAttribute` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `ValueType` mediumtext  NOT NULL,
    `ShowInCommonView` bool  NOT NULL,
    `SortOrder` int  NOT NULL,
    `Static` bool  NOT NULL
);

-- Creating table 'ProductAttributeValues'

CREATE TABLE `ProductAttributeValues` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Value` mediumtext  NOT NULL,
    `SortOrder` int  NOT NULL,
    `PruductAttributeId` int  NOT NULL
);

-- Creating table 'ProductImage'

CREATE TABLE `ProductImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Default` bool  NOT NULL,
    `ImageSource` mediumtext  NOT NULL,
    `ProductId` int  NOT NULL
);

-- Creating table 'Brand'

CREATE TABLE `Brand` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `Logo` mediumtext  NULL,
    `Description` longtext  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `Title` mediumtext  NOT NULL,
    `SeoText` longtext  NULL
);

-- Creating table 'Tag'

CREATE TABLE `Tag` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `Title` mediumtext  NOT NULL,
    `SeoText` longtext  NULL,
    `GroupName` longtext  NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `Title` mediumtext  NOT NULL,
    `PageTitle` mediumtext  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `SortOrder` int  NOT NULL,
    `Text` longtext  NULL,
    `SeoText` longtext  NULL,
    `Published` bool  NOT NULL,
    `MainPage` bool  NOT NULL
);

-- Creating table 'ProductAttributeStaticValues'

CREATE TABLE `ProductAttributeStaticValues` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Value` longtext  NOT NULL,
    `ProductAttributeId` int  NOT NULL,
    `ProductId` int  NOT NULL
);

-- Creating table 'Wish'

CREATE TABLE `Wish` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Category` longtext  NULL,
    `Brand` longtext  NULL,
    `Title` longtext  NULL,
    `Size` longtext  NULL,
    `Color` longtext  NULL,
    `UserName` longtext  NULL,
    `Phone` longtext  NULL,
    `Email` longtext  NULL
);

-- Creating table 'Article'

CREATE TABLE `Article` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Date` datetime  NOT NULL,
    `Published` bool  NOT NULL,
    `Text` longtext  NOT NULL,
    `Title` TEXT  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL
);

-- Creating table 'Comment'

CREATE TABLE `Comment` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NULL,
    `Date` datetime  NOT NULL,
    `Text` longtext  NOT NULL,
    `Email` varchar( 200 )  NULL,
    `Phone` varchar( 200 )  NULL,
    `IsAdmin` bool  NOT NULL,
    `CommentId` int  NULL
);

-- Creating table 'ProductAttributeCategory'

CREATE TABLE `ProductAttributeCategory` (
    `ProductAttributes_Id` int  NOT NULL,
    `Categories_Id` int  NOT NULL
);

-- Creating table 'ProductAttributeValuesProduct'

CREATE TABLE `ProductAttributeValuesProduct` (
    `ProductAttributeValues_Id` int  NOT NULL,
    `Products_Id` int  NOT NULL
);

-- Creating table 'TagProduct'

CREATE TABLE `TagProduct` (
    `Tags_Id` int  NOT NULL,
    `Products_Id` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `ProductAttributes_Id`, `Categories_Id` in table 'ProductAttributeCategory'

ALTER TABLE `ProductAttributeCategory`
ADD CONSTRAINT `PK_ProductAttributeCategory`
    PRIMARY KEY (`ProductAttributes_Id`, `Categories_Id` );

-- Creating primary key on `ProductAttributeValues_Id`, `Products_Id` in table 'ProductAttributeValuesProduct'

ALTER TABLE `ProductAttributeValuesProduct`
ADD CONSTRAINT `PK_ProductAttributeValuesProduct`
    PRIMARY KEY (`ProductAttributeValues_Id`, `Products_Id` );

-- Creating primary key on `Tags_Id`, `Products_Id` in table 'TagProduct'

ALTER TABLE `TagProduct`
ADD CONSTRAINT `PK_TagProduct`
    PRIMARY KEY (`Tags_Id`, `Products_Id` );



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

-- Creating foreign key on `ProductAttributes_Id` in table 'ProductAttributeCategory'

ALTER TABLE `ProductAttributeCategory`
ADD CONSTRAINT `FK_ProductAttributeCategory_ProductAttribute`
    FOREIGN KEY (`ProductAttributes_Id`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Categories_Id` in table 'ProductAttributeCategory'

ALTER TABLE `ProductAttributeCategory`
ADD CONSTRAINT `FK_ProductAttributeCategory_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeCategory_Category'

CREATE INDEX `IX_FK_ProductAttributeCategory_Category` 
    ON `ProductAttributeCategory`
    (`Categories_Id`);

-- Creating foreign key on `PruductAttributeId` in table 'ProductAttributeValues'

ALTER TABLE `ProductAttributeValues`
ADD CONSTRAINT `FK_ProductAttributeProductAttributeValues`
    FOREIGN KEY (`PruductAttributeId`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeProductAttributeValues'

CREATE INDEX `IX_FK_ProductAttributeProductAttributeValues` 
    ON `ProductAttributeValues`
    (`PruductAttributeId`);

-- Creating foreign key on `ProductAttributeValues_Id` in table 'ProductAttributeValuesProduct'

ALTER TABLE `ProductAttributeValuesProduct`
ADD CONSTRAINT `FK_ProductAttributeValuesProduct_ProductAttributeValues`
    FOREIGN KEY (`ProductAttributeValues_Id`)
    REFERENCES `ProductAttributeValues`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Products_Id` in table 'ProductAttributeValuesProduct'

ALTER TABLE `ProductAttributeValuesProduct`
ADD CONSTRAINT `FK_ProductAttributeValuesProduct_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeValuesProduct_Product'

CREATE INDEX `IX_FK_ProductAttributeValuesProduct_Product` 
    ON `ProductAttributeValuesProduct`
    (`Products_Id`);

-- Creating foreign key on `ProductId` in table 'ProductImage'

ALTER TABLE `ProductImage`
ADD CONSTRAINT `FK_ProductProductImage`
    FOREIGN KEY (`ProductId`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductImage'

CREATE INDEX `IX_FK_ProductProductImage` 
    ON `ProductImage`
    (`ProductId`);

-- Creating foreign key on `BrandId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_BrandProduct`
    FOREIGN KEY (`BrandId`)
    REFERENCES `Brand`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandProduct'

CREATE INDEX `IX_FK_BrandProduct` 
    ON `Product`
    (`BrandId`);

-- Creating foreign key on `Tags_Id` in table 'TagProduct'

ALTER TABLE `TagProduct`
ADD CONSTRAINT `FK_TagProduct_Tag`
    FOREIGN KEY (`Tags_Id`)
    REFERENCES `Tag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Products_Id` in table 'TagProduct'

ALTER TABLE `TagProduct`
ADD CONSTRAINT `FK_TagProduct_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TagProduct_Product'

CREATE INDEX `IX_FK_TagProduct_Product` 
    ON `TagProduct`
    (`Products_Id`);

-- Creating foreign key on `ProductAttributeId` in table 'ProductAttributeStaticValues'

ALTER TABLE `ProductAttributeStaticValues`
ADD CONSTRAINT `FK_ProductAttributeProductAttributeStaticValues`
    FOREIGN KEY (`ProductAttributeId`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeProductAttributeStaticValues'

CREATE INDEX `IX_FK_ProductAttributeProductAttributeStaticValues` 
    ON `ProductAttributeStaticValues`
    (`ProductAttributeId`);

-- Creating foreign key on `ProductId` in table 'ProductAttributeStaticValues'

ALTER TABLE `ProductAttributeStaticValues`
ADD CONSTRAINT `FK_ProductAttributeStaticValuesProduct`
    FOREIGN KEY (`ProductId`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeStaticValuesProduct'

CREATE INDEX `IX_FK_ProductAttributeStaticValuesProduct` 
    ON `ProductAttributeStaticValues`
    (`ProductId`);

-- Creating foreign key on `CommentId` in table 'Comment'

ALTER TABLE `Comment`
ADD CONSTRAINT `FK_CommentComment`
    FOREIGN KEY (`CommentId`)
    REFERENCES `Comment`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentComment'

CREATE INDEX `IX_FK_CommentComment` 
    ON `Comment`
    (`CommentId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
