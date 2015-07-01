



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 07/01/2015 23:46:37
-- Generated from EDMX file: C:\vsp\NewVision\NewVision.DataModel\ArtWork.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_AuthorProduct`;
--    ALTER TABLE `AuthorCategoryCategory` DROP CONSTRAINT `FK_AuthorCategoryCategory_AuthorCategory`;
--    ALTER TABLE `AuthorCategoryCategory` DROP CONSTRAINT `FK_AuthorCategoryCategory_Category`;
--    ALTER TABLE `AuthorTag` DROP CONSTRAINT `FK_AuthorTag_Author`;
--    ALTER TABLE `AuthorTag` DROP CONSTRAINT `FK_AuthorTag_Tag`;
--    ALTER TABLE `ProductTag` DROP CONSTRAINT `FK_ProductTag_Product`;
--    ALTER TABLE `ProductTag` DROP CONSTRAINT `FK_ProductTag_Tag`;
--    ALTER TABLE `AuthorCategoryTag` DROP CONSTRAINT `FK_AuthorCategoryTag_AuthorCategory`;
--    ALTER TABLE `AuthorCategoryTag` DROP CONSTRAINT `FK_AuthorCategoryTag_Tag`;
--    ALTER TABLE `CategoryTag` DROP CONSTRAINT `FK_CategoryTag_Category`;
--    ALTER TABLE `CategoryTag` DROP CONSTRAINT `FK_CategoryTag_Tag`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Author`;
    DROP TABLE IF EXISTS `Tag`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `AuthorCategory`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `AuthorCategoryCategory`;
    DROP TABLE IF EXISTS `AuthorTag`;
    DROP TABLE IF EXISTS `ProductTag`;
    DROP TABLE IF EXISTS `AuthorCategoryTag`;
    DROP TABLE IF EXISTS `CategoryTag`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Author`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (100) NOT NULL, 
	`Photo` varchar (500), 
	`Avatar` varchar (500), 
	`SortOrder` int NOT NULL, 
	`Title` varchar (100), 
	`TitleEn` varchar (100), 
	`TitleUa` varchar (100), 
	`About` longtext, 
	`AboutEn` longtext, 
	`AboutUa` longtext, 
	`Description` longtext, 
	`DescriptionEn` longtext, 
	`DescriptionUa` longtext);

ALTER TABLE `Author` ADD PRIMARY KEY (Id);




CREATE TABLE `Tag`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (100), 
	`TitleEn` varchar (100), 
	`TitleUa` varchar (100));

ALTER TABLE `Tag` ADD PRIMARY KEY (Id);




CREATE TABLE `Product`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`AuthorId` int NOT NULL, 
	`Price` varchar (10), 
	`SortOrder` int NOT NULL, 
	`ViewDate` datetime, 
	`ImageSrc` varchar (500) NOT NULL, 
	`Title` varchar (100), 
	`TitleEn` varchar (100), 
	`TitleUa` varchar (100));

ALTER TABLE `Product` ADD PRIMARY KEY (Id);




CREATE TABLE `AuthorCategory`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`Title` varchar (100), 
	`TitleEn` varchar (100), 
	`TitleUa` varchar (100));

ALTER TABLE `AuthorCategory` ADD PRIMARY KEY (Id);




CREATE TABLE `Category`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`Title` varchar (100), 
	`TitleEn` varchar (100), 
	`TitleUa` varchar (100));

ALTER TABLE `Category` ADD PRIMARY KEY (Id);




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
