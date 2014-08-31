



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 08/31/2014 11:59:16
-- Generated from EDMX file: C:\vsp\kiki\Kiki.Database\Model1.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_kiki`;
CREATE DATABASE `gbua_kiki`;
USE `gbua_kiki`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `ServiceItem` DROP CONSTRAINT `FK_ServiceServiceItem`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Reason`;
    DROP TABLE IF EXISTS `Attention`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Subscriber`;
    DROP TABLE IF EXISTS `SiteImage`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `Service`;
    DROP TABLE IF EXISTS `ServiceItem`;
    DROP TABLE IF EXISTS `Sale`;
    DROP TABLE IF EXISTS `GalleryImage`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Reason`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`SortOrder` int NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`Text` varchar (1000));

ALTER TABLE `Reason` ADD PRIMARY KEY (Id);




CREATE TABLE `Attention`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` varchar (200), 
	`ImageSource` varchar (200) NOT NULL);

ALTER TABLE `Attention` ADD PRIMARY KEY (Id);




CREATE TABLE `Content`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`MenuTitle` varchar (200), 
	`SortOrder` int NOT NULL, 
	`ContentType` int NOT NULL, 
	`SeoDescription` varchar (1000), 
	`SeoKeywords` varchar (1000), 
	`SeoText` varchar (10000), 
	`Text` varchar (10000), 
	`TitleImageSource` longtext NOT NULL, 
	`BannerImageSource` longtext NOT NULL);

ALTER TABLE `Content` ADD PRIMARY KEY (Id);




CREATE TABLE `Subscriber`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Email` varchar (200) NOT NULL);

ALTER TABLE `Subscriber` ADD PRIMARY KEY (Id);




CREATE TABLE `SiteImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (200) NOT NULL, 
	`ImageType` int NOT NULL, 
	`Text` varchar (200));

ALTER TABLE `SiteImage` ADD PRIMARY KEY (Id);




CREATE TABLE `Article`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`Text` varchar (10000) NOT NULL, 
	`Name` varchar (200) NOT NULL, 
	`ImageSource` varchar (200) NOT NULL, 
	`Description` varchar (200) NOT NULL);

ALTER TABLE `Article` ADD PRIMARY KEY (Id);




CREATE TABLE `Service`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`Title` varchar (200), 
	`Description` varchar (1000), 
	`Price` varchar (50), 
	`Text` varchar (10000) NOT NULL, 
	`ImageSource` varchar (200), 
	`SortOrder` int NOT NULL);

ALTER TABLE `Service` ADD PRIMARY KEY (Id);




CREATE TABLE `ServiceItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200), 
	`Description` varchar (1000), 
	`Price` varchar (50), 
	`Text` varchar (10000), 
	`SortOrder` int NOT NULL, 
	`ServiceId` int NOT NULL);

ALTER TABLE `ServiceItem` ADD PRIMARY KEY (Id);




CREATE TABLE `Sale`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`Description` varchar (1000), 
	`Text` varchar (10000), 
	`StartDate` datetime NOT NULL, 
	`EndDate` datetime NOT NULL, 
	`ImageSource` varchar (200));

ALTER TABLE `Sale` ADD PRIMARY KEY (Id);




CREATE TABLE `GalleryImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (200) NOT NULL, 
	`SortOrder` int NOT NULL);

ALTER TABLE `GalleryImage` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `ServiceId` in table 'ServiceItem'

ALTER TABLE `ServiceItem`
ADD CONSTRAINT `FK_ServiceServiceItem`
    FOREIGN KEY (`ServiceId`)
    REFERENCES `Service`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ServiceServiceItem'

CREATE INDEX `IX_FK_ServiceServiceItem` 
    ON `ServiceItem`
    (`ServiceId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
