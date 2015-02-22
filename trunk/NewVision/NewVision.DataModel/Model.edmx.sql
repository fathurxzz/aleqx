



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/22/2015 21:41:30
-- Generated from EDMX file: C:\vsp\NewVision\NewVision.DataModel\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_new_vision`;
CREATE DATABASE `gbua_new_vision`;
USE `gbua_new_vision`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `EventAnnouncementImage` DROP CONSTRAINT `FK_EventAnnouncementEventAnnouncementImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `MainBanner`;
    DROP TABLE IF EXISTS `EventAnnouncement`;
    DROP TABLE IF EXISTS `EventAnnouncementImage`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `MainBanner`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSrc` varchar (500) NOT NULL, 
	`Title` varchar (500) NOT NULL, 
	`Description` varchar (2000) NOT NULL);

ALTER TABLE `MainBanner` ADD PRIMARY KEY (Id);




CREATE TABLE `EventAnnouncement`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (500) NOT NULL, 
	`Text` varchar (10000) NOT NULL);

ALTER TABLE `EventAnnouncement` ADD PRIMARY KEY (Id);




CREATE TABLE `EventAnnouncementImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSrc` varchar (500) NOT NULL, 
	`EventAnnouncementId` int NOT NULL);

ALTER TABLE `EventAnnouncementImage` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `EventAnnouncementId` in table 'EventAnnouncementImage'

ALTER TABLE `EventAnnouncementImage`
ADD CONSTRAINT `FK_EventAnnouncementEventAnnouncementImage`
    FOREIGN KEY (`EventAnnouncementId`)
    REFERENCES `EventAnnouncement`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventAnnouncementEventAnnouncementImage'

CREATE INDEX `IX_FK_EventAnnouncementEventAnnouncementImage` 
    ON `EventAnnouncementImage`
    (`EventAnnouncementId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
