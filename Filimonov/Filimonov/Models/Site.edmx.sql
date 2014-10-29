



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/29/2014 19:02:08
-- Generated from EDMX file: C:\vsp\Filimonov\Filimonov\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `ProjectImage` DROP CONSTRAINT `FK_ProjectProjectImage`;
--    ALTER TABLE `FlashContent` DROP CONSTRAINT `FK_ProjectFlashContent`;
--    ALTER TABLE `Song` DROP CONSTRAINT `FK_ProjectSong`;
--    ALTER TABLE `Project` DROP CONSTRAINT `FK_ContentProject`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Project`;
    DROP TABLE IF EXISTS `ProjectImage`;
    DROP TABLE IF EXISTS `FlashContent`;
    DROP TABLE IF EXISTS `Song`;
    DROP TABLE IF EXISTS `SiteBackground`;
    DROP TABLE IF EXISTS `MusicItem`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Content`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` char (200) NOT NULL, 
	`ContentType` int NOT NULL, 
	`Text` longtext, 
	`SeoDescription` longtext, 
	`SeoKeywords` longtext, 
	`SortOrder` int NOT NULL);

ALTER TABLE `Content` ADD PRIMARY KEY (Id);




CREATE TABLE `Project`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` char (200) NOT NULL, 
	`Title` char (200) NOT NULL, 
	`DescriptionTitle` char (200), 
	`Description` longtext, 
	`SortOrder` int NOT NULL, 
	`ImageSource` char (200), 
	`VideoSource` longtext, 
	`ContentId` int NOT NULL);

ALTER TABLE `Project` ADD PRIMARY KEY (Id);




CREATE TABLE `ProjectImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` char (200) NOT NULL, 
	`ProjectId` int NOT NULL, 
	`MainImage` bool NOT NULL);

ALTER TABLE `ProjectImage` ADD PRIMARY KEY (Id);




CREATE TABLE `FlashContent`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (200) NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`ProjectId` int NOT NULL, 
	`ImageSourcePreview` varchar (200) NOT NULL);

ALTER TABLE `FlashContent` ADD PRIMARY KEY (Id);




CREATE TABLE `Song`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL, 
	`FileName` varchar (200) NOT NULL, 
	`ProjectId` int NOT NULL);

ALTER TABLE `Song` ADD PRIMARY KEY (Id);




CREATE TABLE `SiteBackground`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSource` varchar (200) NOT NULL);

ALTER TABLE `SiteBackground` ADD PRIMARY KEY (Id);




CREATE TABLE `MusicItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`FileName` varchar (200) NOT NULL);

ALTER TABLE `MusicItem` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `ProjectId` in table 'ProjectImage'

ALTER TABLE `ProjectImage`
ADD CONSTRAINT `FK_ProjectProjectImage`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `Project`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectImage'

CREATE INDEX `IX_FK_ProjectProjectImage` 
    ON `ProjectImage`
    (`ProjectId`);

-- Creating foreign key on `ProjectId` in table 'FlashContent'

ALTER TABLE `FlashContent`
ADD CONSTRAINT `FK_ProjectFlashContent`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `Project`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectFlashContent'

CREATE INDEX `IX_FK_ProjectFlashContent` 
    ON `FlashContent`
    (`ProjectId`);

-- Creating foreign key on `ProjectId` in table 'Song'

ALTER TABLE `Song`
ADD CONSTRAINT `FK_ProjectSong`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `Project`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectSong'

CREATE INDEX `IX_FK_ProjectSong` 
    ON `Song`
    (`ProjectId`);

-- Creating foreign key on `ContentId` in table 'Project'

ALTER TABLE `Project`
ADD CONSTRAINT `FK_ContentProject`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentProject'

CREATE INDEX `IX_FK_ContentProject` 
    ON `Project`
    (`ContentId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
