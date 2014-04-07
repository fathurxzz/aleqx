



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 04/07/2014 11:03:16
-- Generated from EDMX file: D:\projects\EM2014\EM2014\Model1.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_em2014`;
CREATE DATABASE `gbua_em2014`;
USE `gbua_em2014`;

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

CREATE TABLE `Content`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (255) NOT NULL, 
	`Title` varchar (255) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`Text` longtext, 
	`SeoDescription` longtext, 
	`SeoKeywords` longtext, 
	`IsHomepage` bool NOT NULL);

ALTER TABLE `Content` ADD PRIMARY KEY (Id);




CREATE TABLE `Product`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (255) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`ContentId` int NOT NULL, 
	`Text` longtext, 
	`Title` varchar (255) NOT NULL);

ALTER TABLE `Product` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`Text` longtext, 
	`ImageSource` varchar (255), 
	`ProductId` int NOT NULL);

ALTER TABLE `ProductItem` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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
