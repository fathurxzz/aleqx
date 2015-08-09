



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 08/09/2015 15:49:02
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
--    ALTER TABLE `PreviewContentImage` DROP CONSTRAINT `FK_EventPreviewContentImage`;
--    ALTER TABLE `ContentImage` DROP CONSTRAINT `FK_EventContentImage`;
--    ALTER TABLE `ArticleImage` DROP CONSTRAINT `FK_ArticleArticleImage`;

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
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `Media`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `ArticleImage`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `MainBanner`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSrc` varchar (500) NOT NULL, 
	`Title` varchar (500) NOT NULL, 
	`Description` varchar (2000) NOT NULL, 
	`TitleEn` varchar (500), 
	`TitleUa` varchar (500), 
	`DescriptionEn` varchar (2000), 
	`DescriptionUa` varchar (2000));

ALTER TABLE `MainBanner` ADD PRIMARY KEY (Id);




CREATE TABLE `EventAnnouncement`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (500) NOT NULL, 
	`Text` varchar (10000) NOT NULL, 
	`TitleEn` varchar (500), 
	`TitleUa` varchar (500), 
	`TextEn` varchar (10000), 
	`TextUa` varchar (10000));

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
	`TitleEn` varchar (500) NOT NULL, 
	`TitleUa` varchar (500) NOT NULL, 
	`TitleDescription` varchar (5000), 
	`HighlightedText` varchar (1000), 
	`IsHighlighted` bool NOT NULL, 
	`LocationAddress` varchar (1000), 
	`LocationTitle` varchar (1000), 
	`Description` varchar (5000), 
	`TicketOrderType` int NOT NULL, 
	`PreviewContentType` int NOT NULL, 
	`PreviewContentImageSrc` varchar (1000), 
	`Action` longtext, 
	`Location` longtext, 
	`ArtGroup` longtext, 
	`Duration` varchar (100), 
	`IntervalQuantity` varchar (100), 
	`Price` varchar (100), 
	`PreviewContentVideoSrc` varchar (1000), 
	`LocationAddressMapUrl` varchar (500), 
	`TitleDescriptionEn` varchar (5000), 
	`TitleDescriptionUa` varchar (5000), 
	`HighlightedTextEn` varchar (1000), 
	`HighlightedTextUa` varchar (1000), 
	`LocationAddressEn` varchar (1000), 
	`LocationAddressUa` varchar (1000), 
	`LocationTitleEn` varchar (1000), 
	`LocationTitleUa` varchar (1000), 
	`DescriptionEn` varchar (5000), 
	`DescriptionUa` varchar (5000), 
	`ActionEn` longtext, 
	`ActionUa` longtext, 
	`LocationEn` longtext, 
	`LocationUa` longtext, 
	`ArtGroupEn` longtext, 
	`ArtGroupUa` longtext);

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




CREATE TABLE `Article`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (500), 
	`Date` datetime NOT NULL, 
	`TitlePosition` int NOT NULL, 
	`Text` longtext, 
	`Size` int NOT NULL, 
	`ImageSrc` varchar (500), 
	`VideoSrc` varchar (500), 
	`TitleEn` varchar (500), 
	`TitleUa` varchar (500), 
	`TextEn` longtext, 
	`TextUa` longtext);

ALTER TABLE `Article` ADD PRIMARY KEY (Id);




CREATE TABLE `Media`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (500), 
	`Text` longtext, 
	`SortOrder` int NOT NULL, 
	`ImageSrc` varchar (500), 
	`VideoSrc` varchar (500), 
	`ContentType` int NOT NULL, 
	`TitleEn` varchar (500), 
	`TitleUa` varchar (500), 
	`TextEn` longtext, 
	`TextUa` longtext);

ALTER TABLE `Media` ADD PRIMARY KEY (Id);




CREATE TABLE `Content`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (500), 
	`MenuTitle` varchar (500), 
	`ImageSrc` varchar (500), 
	`Text` longtext, 
	`SortOrder` int NOT NULL, 
	`Name` varchar (500), 
	`TitleEn` varchar (500), 
	`TitleUa` varchar (500), 
	`MenuTitleEn` varchar (500), 
	`MenuTitleUa` varchar (500), 
	`TextEn` longtext, 
	`TextUa` longtext);

ALTER TABLE `Content` ADD PRIMARY KEY (Id);




CREATE TABLE `ArticleImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSrc` varchar (500), 
	`ArticleId` int NOT NULL);

ALTER TABLE `ArticleImage` ADD PRIMARY KEY (Id);




CREATE TABLE `Author`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (100) NOT NULL, 
	`Photo` varchar (500), 
	`Avatar` varchar (500), 
	`SortOrder` int NOT NULL, 
	`Title` varchar (100), 
	`TitleEn` varchar (100) NOT NULL, 
	`TitleUa` varchar (100) NOT NULL, 
	`About` longtext, 
	`AboutEn` longtext NOT NULL, 
	`AboutUa` longtext NOT NULL, 
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




CREATE TABLE `AuthorTag`(
	`Authors_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `AuthorTag` ADD PRIMARY KEY (Authors_Id, Tags_Id);




CREATE TABLE `AuthorCategoryCategory`(
	`AuthorCategories_Id` int NOT NULL, 
	`Categories_Id` int NOT NULL);

ALTER TABLE `AuthorCategoryCategory` ADD PRIMARY KEY (AuthorCategories_Id, Categories_Id);




CREATE TABLE `AuthorCategoryTag`(
	`AuthorCategories_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `AuthorCategoryTag` ADD PRIMARY KEY (AuthorCategories_Id, Tags_Id);




CREATE TABLE `AuthorEvent`(
	`Authors_Id` int NOT NULL, 
	`Events_Id` int NOT NULL);

ALTER TABLE `AuthorEvent` ADD PRIMARY KEY (Authors_Id, Events_Id);






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

-- Creating foreign key on `ArticleId` in table 'ArticleImage'

ALTER TABLE `ArticleImage`
ADD CONSTRAINT `FK_ArticleArticleImage`
    FOREIGN KEY (`ArticleId`)
    REFERENCES `Article`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticleArticleImage'

CREATE INDEX `IX_FK_ArticleArticleImage` 
    ON `ArticleImage`
    (`ArticleId`);

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

-- Creating foreign key on `Authors_Id` in table 'AuthorEvent'

ALTER TABLE `AuthorEvent`
ADD CONSTRAINT `FK_AuthorEvent_Author`
    FOREIGN KEY (`Authors_Id`)
    REFERENCES `Author`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Events_Id` in table 'AuthorEvent'

ALTER TABLE `AuthorEvent`
ADD CONSTRAINT `FK_AuthorEvent_Event`
    FOREIGN KEY (`Events_Id`)
    REFERENCES `Event`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AuthorEvent_Event'

CREATE INDEX `IX_FK_AuthorEvent_Event` 
    ON `AuthorEvent`
    (`Events_Id`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
