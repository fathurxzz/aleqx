



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/04/2014 16:38:23
-- Generated from EDMX file: D:\projects\Mayka\Mayka\DB\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `PhotoGalleryItem` DROP CONSTRAINT `FK_ContentPhotoGalleryItem`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_ContentProduct`;
--    ALTER TABLE `ProductImage` DROP CONSTRAINT `FK_ProductProductImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `PhotoGalleryItem`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `ProductImage`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Content`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (255) NOT NULL, 
	`Title` varchar (255) NOT NULL, 
	`MenuTitle` varchar (255) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`Text` text null, 
	`SeoDescription` text null, 
	`SeoKeywords` text null, 
	`MainPage` bool NOT NULL);

ALTER TABLE `Content` ADD PRIMARY KEY (Id);




CREATE TABLE `PhotoGalleryItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (255) NOT NULL, 
	`ImageSource` varchar (255) NOT NULL, 
	`Url` varchar (255) NOT NULL, 
	`ContentId` int NOT NULL);

ALTER TABLE `PhotoGalleryItem` ADD PRIMARY KEY (Id);




CREATE TABLE `Product`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Description` text null, 
	`PreviewImageSource` varchar (255) NOT NULL, 
	`ContentId` int NOT NULL);

ALTER TABLE `Product` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (255) NOT NULL, 
	`ProductId` int NOT NULL);

ALTER TABLE `ProductImage` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `ContentId` in table 'PhotoGalleryItem'

ALTER TABLE `PhotoGalleryItem`
ADD CONSTRAINT `FK_ContentPhotoGalleryItem`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentPhotoGalleryItem'

CREATE INDEX `IX_FK_ContentPhotoGalleryItem` 
    ON `PhotoGalleryItem`
    (`ContentId`);

-- Creating foreign key on `ContentId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_ContentProduct`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentProduct'

CREATE INDEX `IX_FK_ContentProduct` 
    ON `Product`
    (`ContentId`);

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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
