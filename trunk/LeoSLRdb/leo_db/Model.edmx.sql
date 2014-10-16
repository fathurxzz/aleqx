



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/16/2014 23:20:30
-- Generated from EDMX file: C:\vsp\LeoSLRdb\leo_db\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `CategoryLang` DROP CONSTRAINT `FK_CategoryCategoryLang`;
--    ALTER TABLE `CategoryLang` DROP CONSTRAINT `FK_LanguageCategoryLang`;
--    ALTER TABLE `ArticleLang` DROP CONSTRAINT `FK_ArticleArticleLang`;
--    ALTER TABLE `ArticleLang` DROP CONSTRAINT `FK_LanguageArticleLang`;
--    ALTER TABLE `ArticleItem` DROP CONSTRAINT `FK_ArticleArticleItem`;
--    ALTER TABLE `ArticleItemImage` DROP CONSTRAINT `FK_ArticleItemArticleItemImage`;
--    ALTER TABLE `Category` DROP CONSTRAINT `FK_CategoryCategory`;
--    ALTER TABLE `ArticleItemLang` DROP CONSTRAINT `FK_ArticleItemArticleItemLang`;
--    ALTER TABLE `ArticleItemLang` DROP CONSTRAINT `FK_LanguageArticleItemLang`;
--    ALTER TABLE `ProductImage` DROP CONSTRAINT `FK_ProductProductImage`;
--    ALTER TABLE `ProductLang` DROP CONSTRAINT `FK_LanguageProductLang`;
--    ALTER TABLE `ProductLang` DROP CONSTRAINT `FK_ProductProductLang`;
--    ALTER TABLE `CategoryImage` DROP CONSTRAINT `FK_CategoryCategoryImage`;
--    ALTER TABLE `SpecialContentLang` DROP CONSTRAINT `FK_SpecialContentSpecialContentLang`;
--    ALTER TABLE `SpecialContentLang` DROP CONSTRAINT `FK_LanguageSpecialContentLang`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `Article` DROP CONSTRAINT `FK_CategoryArticle`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Language`;
    DROP TABLE IF EXISTS `CategoryLang`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `ArticleLang`;
    DROP TABLE IF EXISTS `ArticleItem`;
    DROP TABLE IF EXISTS `ArticleItemImage`;
    DROP TABLE IF EXISTS `ArticleItemLang`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `ProductImage`;
    DROP TABLE IF EXISTS `ProductLang`;
    DROP TABLE IF EXISTS `CategoryImage`;
    DROP TABLE IF EXISTS `SpecialContent`;
    DROP TABLE IF EXISTS `SpecialContentLang`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Category`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`CategoryId` int, 
	`SortOrder` int NOT NULL, 
	`CategoryLevel` int NOT NULL, 
	`CategoryType` int NOT NULL);

ALTER TABLE `Category` ADD PRIMARY KEY (Id);




CREATE TABLE `Language`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Code` varchar (2) NOT NULL, 
	`Name` varchar (50) NOT NULL);

ALTER TABLE `Language` ADD PRIMARY KEY (Id);




CREATE TABLE `CategoryLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`CategoryId` int NOT NULL, 
	`LanguageId` int NOT NULL, 
	`Text` longtext NOT NULL);

ALTER TABLE `CategoryLang` ADD PRIMARY KEY (Id);




CREATE TABLE `Article`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`Published` bool NOT NULL, 
	`CategoryId` int NOT NULL);

ALTER TABLE `Article` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` longtext NOT NULL, 
	`Description` longtext NOT NULL, 
	`ArticleId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ArticleLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`ArticleId` int NOT NULL, 
	`ContentType` int NOT NULL);

ALTER TABLE `ArticleItem` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleItemImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (255) NOT NULL, 
	`ArticleItemId` int NOT NULL, 
	`SortOrder` int NOT NULL);

ALTER TABLE `ArticleItemImage` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleItemLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` longtext NOT NULL, 
	`ArticleItemId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `ArticleItemLang` ADD PRIMARY KEY (Id);




CREATE TABLE `Product`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`CategoryId` int NOT NULL, 
	`ContentType` int NOT NULL);

ALTER TABLE `Product` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (255) NOT NULL, 
	`ProductId` int NOT NULL);

ALTER TABLE `ProductImage` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`LanguageId` int NOT NULL, 
	`ProductId` int NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`Text` longtext NOT NULL);

ALTER TABLE `ProductLang` ADD PRIMARY KEY (Id);




CREATE TABLE `CategoryImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (255) NOT NULL, 
	`CategoryId` int NOT NULL);

ALTER TABLE `CategoryImage` ADD PRIMARY KEY (Id);




CREATE TABLE `SpecialContent`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`PageImageSource` varchar (255) NOT NULL, 
	`ContentImageSource` varchar (255) NOT NULL);

ALTER TABLE `SpecialContent` ADD PRIMARY KEY (Id);




CREATE TABLE `SpecialContentLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`Text` longtext NOT NULL, 
	`SpecialContentId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `SpecialContentLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductTextBlock`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`ProductId` int NOT NULL);

ALTER TABLE `ProductTextBlock` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductTextBlockLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` varchar (10000), 
	`Title` varchar (200), 
	`LanguageId` int NOT NULL, 
	`ProductTextBlockId` int NOT NULL);

ALTER TABLE `ProductTextBlockLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductTextBlockFile`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200), 
	`ImageSource` varchar (500), 
	`FileName` varchar (500), 
	`ProductTextBlockId` int NOT NULL);

ALTER TABLE `ProductTextBlockFile` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on `CategoryId` in table 'CategoryImage'

ALTER TABLE `CategoryImage`
ADD CONSTRAINT `FK_CategoryCategoryImage`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategoryImage'

CREATE INDEX `IX_FK_CategoryCategoryImage` 
    ON `CategoryImage`
    (`CategoryId`);

-- Creating foreign key on `SpecialContentId` in table 'SpecialContentLang'

ALTER TABLE `SpecialContentLang`
ADD CONSTRAINT `FK_SpecialContentSpecialContentLang`
    FOREIGN KEY (`SpecialContentId`)
    REFERENCES `SpecialContent`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecialContentSpecialContentLang'

CREATE INDEX `IX_FK_SpecialContentSpecialContentLang` 
    ON `SpecialContentLang`
    (`SpecialContentId`);

-- Creating foreign key on `LanguageId` in table 'SpecialContentLang'

ALTER TABLE `SpecialContentLang`
ADD CONSTRAINT `FK_LanguageSpecialContentLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageSpecialContentLang'

CREATE INDEX `IX_FK_LanguageSpecialContentLang` 
    ON `SpecialContentLang`
    (`LanguageId`);

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

-- Creating foreign key on `CategoryId` in table 'Article'

ALTER TABLE `Article`
ADD CONSTRAINT `FK_CategoryArticle`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryArticle'

CREATE INDEX `IX_FK_CategoryArticle` 
    ON `Article`
    (`CategoryId`);

-- Creating foreign key on `LanguageId` in table 'ProductTextBlockLang'

ALTER TABLE `ProductTextBlockLang`
ADD CONSTRAINT `FK_LanguageProductTextBlockLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageProductTextBlockLang'

CREATE INDEX `IX_FK_LanguageProductTextBlockLang` 
    ON `ProductTextBlockLang`
    (`LanguageId`);

-- Creating foreign key on `ProductTextBlockId` in table 'ProductTextBlockLang'

ALTER TABLE `ProductTextBlockLang`
ADD CONSTRAINT `FK_ProductTextBlockProductTextBlockLang`
    FOREIGN KEY (`ProductTextBlockId`)
    REFERENCES `ProductTextBlock`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTextBlockProductTextBlockLang'

CREATE INDEX `IX_FK_ProductTextBlockProductTextBlockLang` 
    ON `ProductTextBlockLang`
    (`ProductTextBlockId`);

-- Creating foreign key on `ProductTextBlockId` in table 'ProductTextBlockFile'

ALTER TABLE `ProductTextBlockFile`
ADD CONSTRAINT `FK_ProductTextBlockProductTextBlockFile`
    FOREIGN KEY (`ProductTextBlockId`)
    REFERENCES `ProductTextBlock`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTextBlockProductTextBlockFile'

CREATE INDEX `IX_FK_ProductTextBlockProductTextBlockFile` 
    ON `ProductTextBlockFile`
    (`ProductTextBlockId`);

-- Creating foreign key on `ProductId` in table 'ProductTextBlock'

ALTER TABLE `ProductTextBlock`
ADD CONSTRAINT `FK_ProductProductTextBlock`
    FOREIGN KEY (`ProductId`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductTextBlock'

CREATE INDEX `IX_FK_ProductProductTextBlock` 
    ON `ProductTextBlock`
    (`ProductId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
