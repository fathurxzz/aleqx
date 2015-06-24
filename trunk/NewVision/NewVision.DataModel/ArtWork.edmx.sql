



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 06/25/2015 01:02:30
-- Generated from EDMX file: C:\vsp\NewVision\NewVision.DataModel\ArtWork.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_new_vision`;
CREATE DATABASE `gbua_new_vision`;
USE `gbua_new_vision`;

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
	`Label` varchar (10) NOT NULL);

ALTER TABLE `Language` ADD PRIMARY KEY (Id);




CREATE TABLE `Author`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (100) NOT NULL, 
	`Photo` varchar (500), 
	`Avatar` varchar (500), 
	`SortOrder` int NOT NULL);

ALTER TABLE `Author` ADD PRIMARY KEY (Id);




CREATE TABLE `Tag`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE);

ALTER TABLE `Tag` ADD PRIMARY KEY (Id);




CREATE TABLE `Product`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`AuthorId` int NOT NULL, 
	`Price` varchar (10), 
	`SortOrder` int NOT NULL, 
	`ViewDate` datetime , 
	`ImageSrc` varchar (500) NOT NULL);

ALTER TABLE `Product` ADD PRIMARY KEY (Id);




CREATE TABLE `AuthorLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`LanguageId` int NOT NULL, 
	`AuthorId` int NOT NULL, 
	`Title` varchar (100) NOT NULL, 
	`About` longtext, 
	`Description` longtext);

ALTER TABLE `AuthorLang` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ProductId` int NOT NULL, 
	`LanguageId` int NOT NULL, 
	`Title` varchar (100) NOT NULL);

ALTER TABLE `ProductLang` ADD PRIMARY KEY (Id);




CREATE TABLE `TagLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (100) NOT NULL, 
	`LanguageId` int NOT NULL, 
	`TagId` int NOT NULL);

ALTER TABLE `TagLang` ADD PRIMARY KEY (Id);




CREATE TABLE `AuthorCategory`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL);

ALTER TABLE `AuthorCategory` ADD PRIMARY KEY (Id);




CREATE TABLE `AuthorCategoryLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (100) NOT NULL, 
	`AuthorCategoryId` int NOT NULL, 
	`LanguageId` int NOT NULL);

ALTER TABLE `AuthorCategoryLang` ADD PRIMARY KEY (Id);




CREATE TABLE `Category`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL);

ALTER TABLE `Category` ADD PRIMARY KEY (Id);




CREATE TABLE `CategoryLang`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (100) NOT NULL, 
	`LanguageId` int NOT NULL, 
	`CategoryId` int NOT NULL);

ALTER TABLE `CategoryLang` ADD PRIMARY KEY (Id);




CREATE TABLE `AuthorCategoryCategory`(
	`AuthorCategories_Id` int NOT NULL, 
	`Categories_Id` int NOT NULL);

ALTER TABLE `AuthorCategoryCategory` ADD PRIMARY KEY (AuthorCategories_Id, Categories_Id);




CREATE TABLE `AuthorTag`(
	`Authors_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `AuthorTag` ADD PRIMARY KEY (Authors_Id, Tags_Id);




CREATE TABLE `ProductTag`(
	`Products_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `ProductTag` ADD PRIMARY KEY (Products_Id, Tags_Id);




CREATE TABLE `AuthorCategoryTag`(
	`AuthorCategories_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `AuthorCategoryTag` ADD PRIMARY KEY (AuthorCategories_Id, Tags_Id);




CREATE TABLE `CategoryTag`(
	`Categories_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `CategoryTag` ADD PRIMARY KEY (Categories_Id, Tags_Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `AuthorId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_AuthorProduct`
    FOREIGN KEY (`AuthorId`)
    REFERENCES `Author`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorProduct'

CREATE INDEX `IX_FK_AuthorProduct` 
    ON `Product`
    (`AuthorId`);

-- Creating foreign key on `LanguageId` in table 'AuthorLang'

ALTER TABLE `AuthorLang`
ADD CONSTRAINT `FK_LanguageAuthorLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageAuthorLang'

CREATE INDEX `IX_FK_LanguageAuthorLang` 
    ON `AuthorLang`
    (`LanguageId`);

-- Creating foreign key on `AuthorId` in table 'AuthorLang'

ALTER TABLE `AuthorLang`
ADD CONSTRAINT `FK_AuthorAuthorLang`
    FOREIGN KEY (`AuthorId`)
    REFERENCES `Author`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorAuthorLang'

CREATE INDEX `IX_FK_AuthorAuthorLang` 
    ON `AuthorLang`
    (`AuthorId`);

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

-- Creating foreign key on `LanguageId` in table 'TagLang'

ALTER TABLE `TagLang`
ADD CONSTRAINT `FK_LanguageTagLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageTagLang'

CREATE INDEX `IX_FK_LanguageTagLang` 
    ON `TagLang`
    (`LanguageId`);

-- Creating foreign key on `TagId` in table 'TagLang'

ALTER TABLE `TagLang`
ADD CONSTRAINT `FK_TagTagLang`
    FOREIGN KEY (`TagId`)
    REFERENCES `Tag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TagTagLang'

CREATE INDEX `IX_FK_TagTagLang` 
    ON `TagLang`
    (`TagId`);

-- Creating foreign key on `AuthorCategoryId` in table 'AuthorCategoryLang'

ALTER TABLE `AuthorCategoryLang`
ADD CONSTRAINT `FK_AuthorCategoryAuthorCategoryLang`
    FOREIGN KEY (`AuthorCategoryId`)
    REFERENCES `AuthorCategory`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorCategoryAuthorCategoryLang'

CREATE INDEX `IX_FK_AuthorCategoryAuthorCategoryLang` 
    ON `AuthorCategoryLang`
    (`AuthorCategoryId`);

-- Creating foreign key on `LanguageId` in table 'AuthorCategoryLang'

ALTER TABLE `AuthorCategoryLang`
ADD CONSTRAINT `FK_LanguageAuthorCategoryLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageAuthorCategoryLang'

CREATE INDEX `IX_FK_LanguageAuthorCategoryLang` 
    ON `AuthorCategoryLang`
    (`LanguageId`);

-- Creating foreign key on `AuthorCategories_Id` in table 'AuthorCategoryCategory'

ALTER TABLE `AuthorCategoryCategory`
ADD CONSTRAINT `FK_AuthorCategoryCategory_AuthorCategory`
    FOREIGN KEY (`AuthorCategories_Id`)
    REFERENCES `AuthorCategory`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Categories_Id` in table 'AuthorCategoryCategory'

ALTER TABLE `AuthorCategoryCategory`
ADD CONSTRAINT `FK_AuthorCategoryCategory_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorCategoryCategory_Category'

CREATE INDEX `IX_FK_AuthorCategoryCategory_Category` 
    ON `AuthorCategoryCategory`
    (`Categories_Id`);

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

-- Creating foreign key on `Authors_Id` in table 'AuthorTag'

ALTER TABLE `AuthorTag`
ADD CONSTRAINT `FK_AuthorTag_Author`
    FOREIGN KEY (`Authors_Id`)
    REFERENCES `Author`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Tags_Id` in table 'AuthorTag'

ALTER TABLE `AuthorTag`
ADD CONSTRAINT `FK_AuthorTag_Tag`
    FOREIGN KEY (`Tags_Id`)
    REFERENCES `Tag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorTag_Tag'

CREATE INDEX `IX_FK_AuthorTag_Tag` 
    ON `AuthorTag`
    (`Tags_Id`);

-- Creating foreign key on `Products_Id` in table 'ProductTag'

ALTER TABLE `ProductTag`
ADD CONSTRAINT `FK_ProductTag_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Tags_Id` in table 'ProductTag'

ALTER TABLE `ProductTag`
ADD CONSTRAINT `FK_ProductTag_Tag`
    FOREIGN KEY (`Tags_Id`)
    REFERENCES `Tag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTag_Tag'

CREATE INDEX `IX_FK_ProductTag_Tag` 
    ON `ProductTag`
    (`Tags_Id`);

-- Creating foreign key on `AuthorCategories_Id` in table 'AuthorCategoryTag'

ALTER TABLE `AuthorCategoryTag`
ADD CONSTRAINT `FK_AuthorCategoryTag_AuthorCategory`
    FOREIGN KEY (`AuthorCategories_Id`)
    REFERENCES `AuthorCategory`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Tags_Id` in table 'AuthorCategoryTag'

ALTER TABLE `AuthorCategoryTag`
ADD CONSTRAINT `FK_AuthorCategoryTag_Tag`
    FOREIGN KEY (`Tags_Id`)
    REFERENCES `Tag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorCategoryTag_Tag'

CREATE INDEX `IX_FK_AuthorCategoryTag_Tag` 
    ON `AuthorCategoryTag`
    (`Tags_Id`);

-- Creating foreign key on `Categories_Id` in table 'CategoryTag'

ALTER TABLE `CategoryTag`
ADD CONSTRAINT `FK_CategoryTag_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Tags_Id` in table 'CategoryTag'

ALTER TABLE `CategoryTag`
ADD CONSTRAINT `FK_CategoryTag_Tag`
    FOREIGN KEY (`Tags_Id`)
    REFERENCES `Tag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryTag_Tag'

CREATE INDEX `IX_FK_CategoryTag_Tag` 
    ON `CategoryTag`
    (`Tags_Id`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
