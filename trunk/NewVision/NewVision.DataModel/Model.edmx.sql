



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/28/2015 20:38:57
-- Generated from EDMX file: C:\vsp\NewVision\NewVision.DataModel\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `EventAnnouncementImage` DROP CONSTRAINT `FK_EventAnnouncementEventAnnouncementImage`;
--    ALTER TABLE `PreviewContentImage` DROP CONSTRAINT `FK_EventPreviewContentImage`;
--    ALTER TABLE `ContentImage` DROP CONSTRAINT `FK_EventContentImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `MainBanner`;
    DROP TABLE IF EXISTS `EventAnnouncement`;
    DROP TABLE IF EXISTS `EventAnnouncementImage`;
    DROP TABLE IF EXISTS `Event`;
    DROP TABLE IF EXISTS `PreviewContentImage`;
    DROP TABLE IF EXISTS `ContentImage`;
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




CREATE TABLE `Event`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`Title` varchar (500), 
	`TitleDescription` varchar (10000), 
	`HighlightedText` varchar (1000), 
	`IsHighlighted` bool NOT NULL, 
	`LocationAddress` varchar (1000), 
	`LocationTitle` varchar (1000), 
	`Description` varchar (10000), 
	`TicketOrderType` int NOT NULL, 
	`PreviewContentType` int NOT NULL, 
	`PreviewContentImageSrc` varchar (1000), 
	`Action` longtext, 
	`Location` longtext, 
	`ArtGroup` longtext, 
	`Duration` varchar (100), 
	`IntervalQuantity` varchar (100), 
	`Price` varchar (100), 
	`PreviewContentVideoSrc` varchar (1000));

ALTER TABLE `Event` ADD PRIMARY KEY (Id);




CREATE TABLE `PreviewContentImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSrc` varchar (500) NOT NULL, 
	`EventId` int NOT NULL);

ALTER TABLE `PreviewContentImage` ADD PRIMARY KEY (Id);




CREATE TABLE `ContentImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSrc` varchar (500) NOT NULL, 
	`EventId` int NOT NULL);

ALTER TABLE `ContentImage` ADD PRIMARY KEY (Id);






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

-- Creating foreign key on `EventId` in table 'PreviewContentImage'

ALTER TABLE `PreviewContentImage`
ADD CONSTRAINT `FK_EventPreviewContentImage`
    FOREIGN KEY (`EventId`)
    REFERENCES `Event`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventPreviewContentImage'

CREATE INDEX `IX_FK_EventPreviewContentImage` 
    ON `PreviewContentImage`
    (`EventId`);

-- Creating foreign key on `EventId` in table 'ContentImage'

ALTER TABLE `ContentImage`
ADD CONSTRAINT `FK_EventContentImage`
    FOREIGN KEY (`EventId`)
    REFERENCES `Event`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EventContentImage'

CREATE INDEX `IX_FK_EventContentImage` 
    ON `ContentImage`
    (`EventId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
