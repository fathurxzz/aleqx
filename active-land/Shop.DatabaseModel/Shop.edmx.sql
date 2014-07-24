



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 07/24/2014 17:27:50
-- Generated from EDMX file: D:\projects\active-land\Shop.DatabaseModel\Shop.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_active_dev`;
CREATE DATABASE `gbua_active_dev`;
USE `gbua_active_dev`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Language`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Code` char (2) NOT NULL, 
	`Name` varchar (50) NOT NULL);

ALTER TABLE `Language` ADD PRIMARY KEY (Id);




CREATE TABLE `Category`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`CategoryLevel` longtext NOT NULL, 
	`CategoryId` int);

ALTER TABLE `Category` ADD PRIMARY KEY (Id);




CREATE TABLE `CategoryLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`LanguageId` int NOT NULL, 
	`CategoryId` int NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`SeoDescription` longtext, 
	`SeoKeywords` longtext, 
	`SeoText` longtext);

ALTER TABLE `CategoryLang` ADD PRIMARY KEY (Id);




CREATE TABLE `Product`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`CategoryId` int NOT NULL, 
	`Name` varchar (200) NOT NULL, 
	`IsNew` bool NOT NULL, 
	`IsDiscount` bool NOT NULL, 
	`IsTopSale` bool NOT NULL, 
	`Price` decimal( 10, 2 )  NOT NULL, 
	`OldPrice` decimal( 10, 2 )  NOT NULL, 
	`IsActive` bool NOT NULL);

ALTER TABLE `Product` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttribute`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`IsStatic` bool NOT NULL, 
	`DisplayOnPreview` bool NOT NULL, 
	`IsFilterable` bool NOT NULL);

ALTER TABLE `ProductAttribute` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ProductId` int NOT NULL, 
	`ImageSource` varchar (200) NOT NULL, 
	`IsDefault` bool NOT NULL);

ALTER TABLE `ProductImage` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`Description` longtext, 
	`SeoDescription` longtext, 
	`SeoKeywords` longtext, 
	`SeoText` longtext, 
	`ProductId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ProductLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`UnitTitle` varchar (200) NOT NULL, 
	`LanguageId` int NOT NULL, 
	`ProductAttributeId` int NOT NULL);

ALTER TABLE `ProductAttributeLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeValue`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`ProductAttributeId` int NOT NULL, 
	`ProductAttributeValueTagId` int);

ALTER TABLE `ProductAttributeValue` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeValueLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`ProductAttributeValueId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ProductAttributeValueLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeValueTag`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE);

ALTER TABLE `ProductAttributeValueTag` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeValueTagLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ProductAttributeValueTagId` int NOT NULL, 
	`LanguageId` int NOT NULL, 
	`Title` varchar (50) NOT NULL);

ALTER TABLE `ProductAttributeValueTagLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeStaticValue`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ProductAttributeId` int NOT NULL, 
	`ProductId` int NOT NULL);

ALTER TABLE `ProductAttributeStaticValue` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeStaticValueLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`ProductAttributeStaticValueId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ProductAttributeStaticValueLang` ADD PRIMARY KEY (Id);




CREATE TABLE `CategoryProductAttribute`(
	`Categories_Id` int NOT NULL, 
	`ProductAttributes_Id` int NOT NULL);

ALTER TABLE `CategoryProductAttribute` ADD PRIMARY KEY (Categories_Id, ProductAttributes_Id);




CREATE TABLE `ProductAttributeValueProduct`(
	`ProductAttributeValues_Id` int NOT NULL, 
	`Products_Id` int NOT NULL);

ALTER TABLE `ProductAttributeValueProduct` ADD PRIMARY KEY (ProductAttributeValues_Id, Products_Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `LanguageId` in table 'CategoryLang'

ALTER TABLE `CategoryLang`
ADD CONSTRAINT `FK_LanguageCategoryLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageCategoryLang'

CREATE INDEX `IX_FK_LanguageCategoryLang` 
    ON `CategoryLang`
    (`LanguageId`);

-- Creating foreign key on `LanguageId` in table 'ProductLang'

ALTER TABLE `ProductLang`
ADD CONSTRAINT `FK_LanguageProductLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProductLang'

CREATE INDEX `IX_FK_LanguageProductLang` 
    ON `ProductLang`
    (`LanguageId`);

-- Creating foreign key on `LanguageId` in table 'ProductAttributeLang'

ALTER TABLE `ProductAttributeLang`
ADD CONSTRAINT `FK_LanguageProductAttributeLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProductAttributeLang'

CREATE INDEX `IX_FK_LanguageProductAttributeLang` 
    ON `ProductAttributeLang`
    (`LanguageId`);

-- Creating foreign key on `LanguageId` in table 'ProductAttributeValueTagLang'

ALTER TABLE `ProductAttributeValueTagLang`
ADD CONSTRAINT `FK_LanguageProductAttributeValueTagLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProductAttributeValueTagLang'

CREATE INDEX `IX_FK_LanguageProductAttributeValueTagLang` 
    ON `ProductAttributeValueTagLang`
    (`LanguageId`);

-- Creating foreign key on `LanguageId` in table 'ProductAttributeValueLang'

ALTER TABLE `ProductAttributeValueLang`
ADD CONSTRAINT `FK_LanguageProductAttributeValueLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProductAttributeValueLang'

CREATE INDEX `IX_FK_LanguageProductAttributeValueLang` 
    ON `ProductAttributeValueLang`
    (`LanguageId`);

-- Creating foreign key on `LanguageId` in table 'ProductAttributeStaticValueLang'

ALTER TABLE `ProductAttributeStaticValueLang`
ADD CONSTRAINT `FK_LanguageProductAttributeStaticValueLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProductAttributeStaticValueLang'

CREATE INDEX `IX_FK_LanguageProductAttributeStaticValueLang` 
    ON `ProductAttributeStaticValueLang`
    (`LanguageId`);

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

-- Creating foreign key on `CategoryId` in table 'CategoryLang'

ALTER TABLE `CategoryLang`
ADD CONSTRAINT `FK_CategoryCategoryLang`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategoryLang'

CREATE INDEX `IX_FK_CategoryCategoryLang` 
    ON `CategoryLang`
    (`CategoryId`);

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

-- Creating foreign key on `ProductId` in table 'ProductLang'

ALTER TABLE `ProductLang`
ADD CONSTRAINT `FK_ProductProductLang`
    FOREIGN KEY (`ProductId`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductLang'

CREATE INDEX `IX_FK_ProductProductLang` 
    ON `ProductLang`
    (`ProductId`);

-- Creating foreign key on `ProductAttributeId` in table 'ProductAttributeValue'

ALTER TABLE `ProductAttributeValue`
ADD CONSTRAINT `FK_ProductAttributeProductAttributeValue`
    FOREIGN KEY (`ProductAttributeId`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeProductAttributeValue'

CREATE INDEX `IX_FK_ProductAttributeProductAttributeValue` 
    ON `ProductAttributeValue`
    (`ProductAttributeId`);

-- Creating foreign key on `ProductAttributeValueTagId` in table 'ProductAttributeValue'

ALTER TABLE `ProductAttributeValue`
ADD CONSTRAINT `FK_ProductAttributeValueTagProductAttributeValue`
    FOREIGN KEY (`ProductAttributeValueTagId`)
    REFERENCES `ProductAttributeValueTag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeValueTagProductAttributeValue'

CREATE INDEX `IX_FK_ProductAttributeValueTagProductAttributeValue` 
    ON `ProductAttributeValue`
    (`ProductAttributeValueTagId`);

-- Creating foreign key on `ProductAttributeValueId` in table 'ProductAttributeValueLang'

ALTER TABLE `ProductAttributeValueLang`
ADD CONSTRAINT `FK_ProductAttributeValueProductAttributeValueLang`
    FOREIGN KEY (`ProductAttributeValueId`)
    REFERENCES `ProductAttributeValue`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeValueProductAttributeValueLang'

CREATE INDEX `IX_FK_ProductAttributeValueProductAttributeValueLang` 
    ON `ProductAttributeValueLang`
    (`ProductAttributeValueId`);

-- Creating foreign key on `ProductAttributeValues_Id` in table 'ProductAttributeValueProduct'

ALTER TABLE `ProductAttributeValueProduct`
ADD CONSTRAINT `FK_ProductAttributeValueProduct_ProductAttributeValue`
    FOREIGN KEY (`ProductAttributeValues_Id`)
    REFERENCES `ProductAttributeValue`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Products_Id` in table 'ProductAttributeValueProduct'

ALTER TABLE `ProductAttributeValueProduct`
ADD CONSTRAINT `FK_ProductAttributeValueProduct_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeValueProduct_Product'

CREATE INDEX `IX_FK_ProductAttributeValueProduct_Product` 
    ON `ProductAttributeValueProduct`
    (`Products_Id`);

-- Creating foreign key on `ProductAttributeValueTagId` in table 'ProductAttributeValueTagLang'

ALTER TABLE `ProductAttributeValueTagLang`
ADD CONSTRAINT `FK_ProductAttributeValueTagProductAttributeValueTagLang`
    FOREIGN KEY (`ProductAttributeValueTagId`)
    REFERENCES `ProductAttributeValueTag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeValueTagProductAttributeValueTagLang'

CREATE INDEX `IX_FK_ProductAttributeValueTagProductAttributeValueTagLang` 
    ON `ProductAttributeValueTagLang`
    (`ProductAttributeValueTagId`);

-- Creating foreign key on `ProductAttributeId` in table 'ProductAttributeStaticValue'

ALTER TABLE `ProductAttributeStaticValue`
ADD CONSTRAINT `FK_ProductAttributeProductAttributeStaticValue`
    FOREIGN KEY (`ProductAttributeId`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeProductAttributeStaticValue'

CREATE INDEX `IX_FK_ProductAttributeProductAttributeStaticValue` 
    ON `ProductAttributeStaticValue`
    (`ProductAttributeId`);

-- Creating foreign key on `ProductId` in table 'ProductAttributeStaticValue'

ALTER TABLE `ProductAttributeStaticValue`
ADD CONSTRAINT `FK_ProductProductAttributeStaticValue`
    FOREIGN KEY (`ProductId`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductAttributeStaticValue'

CREATE INDEX `IX_FK_ProductProductAttributeStaticValue` 
    ON `ProductAttributeStaticValue`
    (`ProductId`);

-- Creating foreign key on `ProductAttributeStaticValueId` in table 'ProductAttributeStaticValueLang'

ALTER TABLE `ProductAttributeStaticValueLang`
ADD CONSTRAINT `FK_ProductAttributeStaticValueProductAttributeStaticValueLang`
    FOREIGN KEY (`ProductAttributeStaticValueId`)
    REFERENCES `ProductAttributeStaticValue`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeStaticValueProductAttributeStaticValueLang'

CREATE INDEX `IX_FK_ProductAttributeStaticValueProductAttributeStaticValueLang` 
    ON `ProductAttributeStaticValueLang`
    (`ProductAttributeStaticValueId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
