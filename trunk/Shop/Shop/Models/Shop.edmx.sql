



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/17/2012 16:51:22
-- Generated from EDMX file: D:\projects\Shop\Shop\Models\Shop.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `Category` DROP CONSTRAINT `FK_CategoryCategory`;
--    ALTER TABLE `PruductAttributeCategory` DROP CONSTRAINT `FK_PruductAttributeCategory_PruductAttribute`;
--    ALTER TABLE `PruductAttributeCategory` DROP CONSTRAINT `FK_PruductAttributeCategory_Category`;
--    ALTER TABLE `ProductAttributeValues` DROP CONSTRAINT `FK_PruductAttributeProductAttributeValues`;
--    ALTER TABLE `ProductAttributeValuesProduct` DROP CONSTRAINT `FK_ProductAttributeValuesProduct_ProductAttributeValues`;
--    ALTER TABLE `ProductAttributeValuesProduct` DROP CONSTRAINT `FK_ProductAttributeValuesProduct_Product`;
--    ALTER TABLE `ProductImage` DROP CONSTRAINT `FK_ProductProductImage`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_BrandProduct`;
--    ALTER TABLE `TagProduct` DROP CONSTRAINT `FK_TagProduct_Tag`;
--    ALTER TABLE `TagProduct` DROP CONSTRAINT `FK_TagProduct_Product`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `PruductAttribute`;
    DROP TABLE IF EXISTS `ProductAttributeValues`;
    DROP TABLE IF EXISTS `ProductImage`;
    DROP TABLE IF EXISTS `Brand`;
    DROP TABLE IF EXISTS `Tag`;
    DROP TABLE IF EXISTS `PruductAttributeCategory`;
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
    `BrandId` int  NOT NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `CategoryId` int  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'PruductAttribute'

CREATE TABLE `PruductAttribute` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `ValueType` mediumtext  NOT NULL,
    `ShowInCommonView` bool  NOT NULL,
    `SortOrder` int  NOT NULL
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
    `SeoKeywords` longtext  NULL
);

-- Creating table 'Tag'

CREATE TABLE `Tag` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Value` mediumtext  NOT NULL
);

-- Creating table 'PruductAttributeCategory'

CREATE TABLE `PruductAttributeCategory` (
    `PruductAttributes_Id` int  NOT NULL,
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

-- Creating primary key on `PruductAttributes_Id`, `Categories_Id` in table 'PruductAttributeCategory'

ALTER TABLE `PruductAttributeCategory`
ADD CONSTRAINT `PK_PruductAttributeCategory`
    PRIMARY KEY (`PruductAttributes_Id`, `Categories_Id` );

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

-- Creating foreign key on `PruductAttributes_Id` in table 'PruductAttributeCategory'

ALTER TABLE `PruductAttributeCategory`
ADD CONSTRAINT `FK_PruductAttributeCategory_PruductAttribute`
    FOREIGN KEY (`PruductAttributes_Id`)
    REFERENCES `PruductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Categories_Id` in table 'PruductAttributeCategory'

ALTER TABLE `PruductAttributeCategory`
ADD CONSTRAINT `FK_PruductAttributeCategory_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PruductAttributeCategory_Category'

CREATE INDEX `IX_FK_PruductAttributeCategory_Category` 
    ON `PruductAttributeCategory`
    (`Categories_Id`);

-- Creating foreign key on `PruductAttributeId` in table 'ProductAttributeValues'

ALTER TABLE `ProductAttributeValues`
ADD CONSTRAINT `FK_PruductAttributeProductAttributeValues`
    FOREIGN KEY (`PruductAttributeId`)
    REFERENCES `PruductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PruductAttributeProductAttributeValues'

CREATE INDEX `IX_FK_PruductAttributeProductAttributeValues` 
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
