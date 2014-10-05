



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/05/2014 18:06:47
-- Generated from EDMX file: C:\vsp\active-land\Shop.DatabaseModel\Shop.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_active_dev`;
CREATE DATABASE `gbua_active_dev`;
USE `gbua_active_dev`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `CategoryLang` DROP CONSTRAINT `FK_LanguageCategoryLang`;
--    ALTER TABLE `ProductLang` DROP CONSTRAINT `FK_LanguageProductLang`;
--    ALTER TABLE `ProductAttributeLang` DROP CONSTRAINT `FK_LanguageProductAttributeLang`;
--    ALTER TABLE `ProductAttributeValueTagLang` DROP CONSTRAINT `FK_LanguageProductAttributeValueTagLang`;
--    ALTER TABLE `ProductAttributeValueLang` DROP CONSTRAINT `FK_LanguageProductAttributeValueLang`;
--    ALTER TABLE `ProductAttributeStaticValueLang` DROP CONSTRAINT `FK_LanguageProductAttributeStaticValueLang`;
--    ALTER TABLE `Category` DROP CONSTRAINT `FK_CategoryCategory`;
--    ALTER TABLE `CategoryLang` DROP CONSTRAINT `FK_CategoryCategoryLang`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `CategoryProductAttribute` DROP CONSTRAINT `FK_CategoryProductAttribute_Category`;
--    ALTER TABLE `CategoryProductAttribute` DROP CONSTRAINT `FK_CategoryProductAttribute_ProductAttribute`;
--    ALTER TABLE `ProductImage` DROP CONSTRAINT `FK_ProductProductImage`;
--    ALTER TABLE `ProductLang` DROP CONSTRAINT `FK_ProductProductLang`;
--    ALTER TABLE `ProductAttributeValue` DROP CONSTRAINT `FK_ProductAttributeProductAttributeValue`;
--    ALTER TABLE `ProductAttributeValue` DROP CONSTRAINT `FK_ProductAttributeValueTagProductAttributeValue`;
--    ALTER TABLE `ProductAttributeValueLang` DROP CONSTRAINT `FK_ProductAttributeValueProductAttributeValueLang`;
--    ALTER TABLE `ProductAttributeValueProduct` DROP CONSTRAINT `FK_ProductAttributeValueProduct_ProductAttributeValue`;
--    ALTER TABLE `ProductAttributeValueProduct` DROP CONSTRAINT `FK_ProductAttributeValueProduct_Product`;
--    ALTER TABLE `ProductAttributeValueTagLang` DROP CONSTRAINT `FK_ProductAttributeValueTagProductAttributeValueTagLang`;
--    ALTER TABLE `ProductAttributeStaticValue` DROP CONSTRAINT `FK_ProductAttributeProductAttributeStaticValue`;
--    ALTER TABLE `ProductAttributeStaticValue` DROP CONSTRAINT `FK_ProductProductAttributeStaticValue`;
--    ALTER TABLE `ProductAttributeStaticValueLang` DROP CONSTRAINT `FK_ProductAttributeStaticValueProductAttributeStaticValueLang`;
--    ALTER TABLE `ProductAttributeLang` DROP CONSTRAINT `FK_ProductAttributeProductAttributeLang`;
--    ALTER TABLE `ContentLang` DROP CONSTRAINT `FK_LanguageContentLang`;
--    ALTER TABLE `ContentLang` DROP CONSTRAINT `FK_ContentContentLang`;
--    ALTER TABLE `ArticleLang` DROP CONSTRAINT `FK_LanguageArticleLang`;
--    ALTER TABLE `ArticleLang` DROP CONSTRAINT `FK_ArticleArticleLang`;
--    ALTER TABLE `ArticleItem` DROP CONSTRAINT `FK_ArticleArticleItem`;
--    ALTER TABLE `ArticleItemLang` DROP CONSTRAINT `FK_ArticleItemArticleItemLang`;
--    ALTER TABLE `ArticleItemLang` DROP CONSTRAINT `FK_LanguageArticleItemLang`;
--    ALTER TABLE `ArticleItemImage` DROP CONSTRAINT `FK_ArticleItemArticleItemImage`;
--    ALTER TABLE `ContentItem` DROP CONSTRAINT `FK_ContentContentItem`;
--    ALTER TABLE `ContentItemLang` DROP CONSTRAINT `FK_ContentItemContentItemLang`;
--    ALTER TABLE `ContentItemLang` DROP CONSTRAINT `FK_LanguageContentItemLang`;
--    ALTER TABLE `ContentItemImage` DROP CONSTRAINT `FK_ContentItemContentItemImage`;
--    ALTER TABLE `OrderItem` DROP CONSTRAINT `FK_OrderOrderItem`;
--    ALTER TABLE `QuickAdviceLang` DROP CONSTRAINT `FK_QuickAdviceQuickAdviceLang`;
--    ALTER TABLE `QuickAdviceLang` DROP CONSTRAINT `FK_LanguageQuickAdviceLang`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Language`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `CategoryLang`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `ProductAttribute`;
    DROP TABLE IF EXISTS `ProductImage`;
    DROP TABLE IF EXISTS `ProductLang`;
    DROP TABLE IF EXISTS `ProductAttributeLang`;
    DROP TABLE IF EXISTS `ProductAttributeValue`;
    DROP TABLE IF EXISTS `ProductAttributeValueLang`;
    DROP TABLE IF EXISTS `ProductAttributeValueTag`;
    DROP TABLE IF EXISTS `ProductAttributeValueTagLang`;
    DROP TABLE IF EXISTS `ProductAttributeStaticValue`;
    DROP TABLE IF EXISTS `ProductAttributeStaticValueLang`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `ContentLang`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `ArticleLang`;
    DROP TABLE IF EXISTS `ArticleItem`;
    DROP TABLE IF EXISTS `ArticleItemLang`;
    DROP TABLE IF EXISTS `ArticleItemImage`;
    DROP TABLE IF EXISTS `ContentItem`;
    DROP TABLE IF EXISTS `ContentItemImage`;
    DROP TABLE IF EXISTS `ContentItemLang`;
    DROP TABLE IF EXISTS `Order`;
    DROP TABLE IF EXISTS `OrderItem`;
    DROP TABLE IF EXISTS `QuickAdvice`;
    DROP TABLE IF EXISTS `QuickAdviceLang`;
    DROP TABLE IF EXISTS `ShopSetting`;
    DROP TABLE IF EXISTS `CategoryProductAttribute`;
    DROP TABLE IF EXISTS `ProductAttributeValueProduct`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Language`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Code` char (2) NOT NULL, 
	`Name` varchar (50) NOT NULL, 
	`IsDefault` bool NOT NULL, 
	`IsAdminDefault` bool NOT NULL);

ALTER TABLE `Language` ADD PRIMARY KEY (Id);




CREATE TABLE `Category`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`CategoryLevel` int NOT NULL, 
	`CategoryId` int, 
	`IsActive` bool NOT NULL);

ALTER TABLE `Category` ADD PRIMARY KEY (Id);




CREATE TABLE `CategoryLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`LanguageId` int NOT NULL, 
	`CategoryId` int NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`SeoDescription` varchar (10000), 
	`SeoKeywords` varchar (1000), 
	`SeoText` varchar (10000));

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
	`IsActive` bool NOT NULL, 
	`SearchCriteria` varchar (1000) NOT NULL, 
	`SearchCriteriaAttributes` varchar (1000) NOT NULL);

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
	`Description` varchar (1000), 
	`SeoDescription` varchar (10000), 
	`SeoKeywords` varchar (1000), 
	`SeoText` varchar (10000), 
	`ProductId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ProductLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductAttributeLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`UnitTitle` varchar (200), 
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
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (50) NOT NULL);

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




CREATE TABLE `Content`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`ContentType` int NOT NULL, 
	`SortOrder` int NOT NULL);

ALTER TABLE `Content` ADD PRIMARY KEY (Id);




CREATE TABLE `ContentLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`LanguageId` int NOT NULL, 
	`ContentId` int NOT NULL, 
	`Text` varchar (10000) NOT NULL, 
	`SeoDescription` varchar (10000), 
	`SeoKeywords` varchar (1000), 
	`SeoText` varchar (10000));

ALTER TABLE `ContentLang` ADD PRIMARY KEY (Id);




CREATE TABLE `Article`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`Name` varchar (200) NOT NULL, 
	`IsActive` bool NOT NULL, 
	`ImageSource` varchar (200) NOT NULL);

ALTER TABLE `Article` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`Description` varchar (1000) NOT NULL, 
	`Text` varchar (10000) NOT NULL, 
	`LanguageId` int NOT NULL, 
	`ArticleId` int NOT NULL);

ALTER TABLE `ArticleLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`ArticleId` int NOT NULL);

ALTER TABLE `ArticleItem` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleItemLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` varchar (10000) NOT NULL, 
	`ArticleItemId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ArticleItemLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleItemImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (200) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`ArticleItemId` int NOT NULL);

ALTER TABLE `ArticleItemImage` ADD PRIMARY KEY (Id);




CREATE TABLE `ContentItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`ContentId` int NOT NULL);

ALTER TABLE `ContentItem` ADD PRIMARY KEY (Id);




CREATE TABLE `ContentItemImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ContentItemId` int NOT NULL, 
	`ImageSource` varchar (200) NOT NULL);

ALTER TABLE `ContentItemImage` ADD PRIMARY KEY (Id);




CREATE TABLE `ContentItemLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` varchar (10000) NOT NULL, 
	`ContentItemId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ContentItemLang` ADD PRIMARY KEY (Id);




CREATE TABLE `Order`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`CustomerName` varchar (200), 
	`CustomerPhone` varchar (200), 
	`CustomerEmail` varchar (200), 
	`DeliveryAddress` varchar (200), 
	`Completed` bool NOT NULL, 
	`Info` varchar (1000), 
	`Subscribed` bool NOT NULL, 
	`DeliveryMethod` int NOT NULL, 
	`DeliveryCity` varchar (200) NOT NULL, 
	`DeliveryStreet` varchar (200) NOT NULL, 
	`DeliveryOffice` varchar (200) NOT NULL, 
	`PaymentMethod` int NOT NULL);

ALTER TABLE `Order` ADD PRIMARY KEY (Id);




CREATE TABLE `OrderItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Description` varchar (1000), 
	`ImageSource` varchar (200), 
	`Price` decimal( 10, 2 )  NOT NULL, 
	`Quantity` int NOT NULL, 
	`ProductName` varchar (200), 
	`ProductId` int NOT NULL, 
	`ProductTitle` varchar (200) NOT NULL, 
	`OrderId` int NOT NULL);

ALTER TABLE `OrderItem` ADD PRIMARY KEY (Id);




CREATE TABLE `QuickAdvice`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Active` bool NOT NULL, 
	`SortOrder` int NOT NULL);

ALTER TABLE `QuickAdvice` ADD PRIMARY KEY (Id);




CREATE TABLE `QuickAdviceLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`QuickAdviceId` int NOT NULL, 
	`LanguageId` int NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`Text` varchar (10000) NOT NULL);

ALTER TABLE `QuickAdviceLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ShopSetting`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Key` varchar (20) NOT NULL, 
	`Value` varchar (200), 
	`Title` varchar (200) NOT NULL);

ALTER TABLE `ShopSetting` ADD PRIMARY KEY (Id);




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

-- Creating foreign key on `ProductAttributeId` in table 'ProductAttributeLang'

ALTER TABLE `ProductAttributeLang`
ADD CONSTRAINT `FK_ProductAttributeProductAttributeLang`
    FOREIGN KEY (`ProductAttributeId`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAttributeProductAttributeLang'

CREATE INDEX `IX_FK_ProductAttributeProductAttributeLang` 
    ON `ProductAttributeLang`
    (`ProductAttributeId`);

-- Creating foreign key on `LanguageId` in table 'ContentLang'

ALTER TABLE `ContentLang`
ADD CONSTRAINT `FK_LanguageContentLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageContentLang'

CREATE INDEX `IX_FK_LanguageContentLang` 
    ON `ContentLang`
    (`LanguageId`);

-- Creating foreign key on `ContentId` in table 'ContentLang'

ALTER TABLE `ContentLang`
ADD CONSTRAINT `FK_ContentContentLang`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentContentLang'

CREATE INDEX `IX_FK_ContentContentLang` 
    ON `ContentLang`
    (`ContentId`);

-- Creating foreign key on `LanguageId` in table 'ArticleLang'

ALTER TABLE `ArticleLang`
ADD CONSTRAINT `FK_LanguageArticleLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageArticleLang'

CREATE INDEX `IX_FK_LanguageArticleLang` 
    ON `ArticleLang`
    (`LanguageId`);

-- Creating foreign key on `ArticleId` in table 'ArticleLang'

ALTER TABLE `ArticleLang`
ADD CONSTRAINT `FK_ArticleArticleLang`
    FOREIGN KEY (`ArticleId`)
    REFERENCES `Article`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleArticleLang'

CREATE INDEX `IX_FK_ArticleArticleLang` 
    ON `ArticleLang`
    (`ArticleId`);

-- Creating foreign key on `ArticleId` in table 'ArticleItem'

ALTER TABLE `ArticleItem`
ADD CONSTRAINT `FK_ArticleArticleItem`
    FOREIGN KEY (`ArticleId`)
    REFERENCES `Article`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleArticleItem'

CREATE INDEX `IX_FK_ArticleArticleItem` 
    ON `ArticleItem`
    (`ArticleId`);

-- Creating foreign key on `ArticleItemId` in table 'ArticleItemLang'

ALTER TABLE `ArticleItemLang`
ADD CONSTRAINT `FK_ArticleItemArticleItemLang`
    FOREIGN KEY (`ArticleItemId`)
    REFERENCES `ArticleItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleItemArticleItemLang'

CREATE INDEX `IX_FK_ArticleItemArticleItemLang` 
    ON `ArticleItemLang`
    (`ArticleItemId`);

-- Creating foreign key on `LanguageId` in table 'ArticleItemLang'

ALTER TABLE `ArticleItemLang`
ADD CONSTRAINT `FK_LanguageArticleItemLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageArticleItemLang'

CREATE INDEX `IX_FK_LanguageArticleItemLang` 
    ON `ArticleItemLang`
    (`LanguageId`);

-- Creating foreign key on `ArticleItemId` in table 'ArticleItemImage'

ALTER TABLE `ArticleItemImage`
ADD CONSTRAINT `FK_ArticleItemArticleItemImage`
    FOREIGN KEY (`ArticleItemId`)
    REFERENCES `ArticleItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleItemArticleItemImage'

CREATE INDEX `IX_FK_ArticleItemArticleItemImage` 
    ON `ArticleItemImage`
    (`ArticleItemId`);

-- Creating foreign key on `ContentId` in table 'ContentItem'

ALTER TABLE `ContentItem`
ADD CONSTRAINT `FK_ContentContentItem`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentContentItem'

CREATE INDEX `IX_FK_ContentContentItem` 
    ON `ContentItem`
    (`ContentId`);

-- Creating foreign key on `ContentItemId` in table 'ContentItemLang'

ALTER TABLE `ContentItemLang`
ADD CONSTRAINT `FK_ContentItemContentItemLang`
    FOREIGN KEY (`ContentItemId`)
    REFERENCES `ContentItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentItemContentItemLang'

CREATE INDEX `IX_FK_ContentItemContentItemLang` 
    ON `ContentItemLang`
    (`ContentItemId`);

-- Creating foreign key on `LanguageId` in table 'ContentItemLang'

ALTER TABLE `ContentItemLang`
ADD CONSTRAINT `FK_LanguageContentItemLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageContentItemLang'

CREATE INDEX `IX_FK_LanguageContentItemLang` 
    ON `ContentItemLang`
    (`LanguageId`);

-- Creating foreign key on `ContentItemId` in table 'ContentItemImage'

ALTER TABLE `ContentItemImage`
ADD CONSTRAINT `FK_ContentItemContentItemImage`
    FOREIGN KEY (`ContentItemId`)
    REFERENCES `ContentItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentItemContentItemImage'

CREATE INDEX `IX_FK_ContentItemContentItemImage` 
    ON `ContentItemImage`
    (`ContentItemId`);

-- Creating foreign key on `OrderId` in table 'OrderItem'

ALTER TABLE `OrderItem`
ADD CONSTRAINT `FK_OrderOrderItem`
    FOREIGN KEY (`OrderId`)
    REFERENCES `Order`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderItem'

CREATE INDEX `IX_FK_OrderOrderItem` 
    ON `OrderItem`
    (`OrderId`);

-- Creating foreign key on `QuickAdviceId` in table 'QuickAdviceLang'

ALTER TABLE `QuickAdviceLang`
ADD CONSTRAINT `FK_QuickAdviceQuickAdviceLang`
    FOREIGN KEY (`QuickAdviceId`)
    REFERENCES `QuickAdvice`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuickAdviceQuickAdviceLang'

CREATE INDEX `IX_FK_QuickAdviceQuickAdviceLang` 
    ON `QuickAdviceLang`
    (`QuickAdviceId`);

-- Creating foreign key on `LanguageId` in table 'QuickAdviceLang'

ALTER TABLE `QuickAdviceLang`
ADD CONSTRAINT `FK_LanguageQuickAdviceLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageQuickAdviceLang'

CREATE INDEX `IX_FK_LanguageQuickAdviceLang` 
    ON `QuickAdviceLang`
    (`LanguageId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
