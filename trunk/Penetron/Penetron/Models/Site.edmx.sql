



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 11/21/2013 18:57:25
-- Generated from EDMX file: D:\projects\Penetron\Penetron\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `TechnologyImage` DROP CONSTRAINT `FK_TechnologyTechnologyImage`;
--    ALTER TABLE `Technology` DROP CONSTRAINT `FK_TechnologyTechnology`;
--    ALTER TABLE `Building` DROP CONSTRAINT `FK_BuildingBuilding`;
--    ALTER TABLE `BuildingImage` DROP CONSTRAINT `FK_BuildingObjBuildingImage`;
--    ALTER TABLE `ContentItem` DROP CONSTRAINT `FK_ContentContentItem`;
--    ALTER TABLE `TechnologyItem` DROP CONSTRAINT `FK_TechnologyTechnologyItem`;
--    ALTER TABLE `BuildingItem` DROP CONSTRAINT `FK_BuildingBuildingItem`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Technology`;
    DROP TABLE IF EXISTS `TechnologyImage`;
    DROP TABLE IF EXISTS `Building`;
    DROP TABLE IF EXISTS `BuildingObj`;
    DROP TABLE IF EXISTS `BuildingImage`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `ContentItem`;
    DROP TABLE IF EXISTS `TechnologyItem`;
    DROP TABLE IF EXISTS `BuildingItem`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `Reason`;
    DROP TABLE IF EXISTS `Slider`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Technology`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` char (200) NOT NULL, 
	`Title` char (200) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`Text` varchar (1000), 
	`CategoryLevel` int NOT NULL, 
	`SeoDescription` varchar (1000), 
	`SeoKeywords` varchar (1000), 
	`Active` bool NOT NULL, 
	`TechnologyId` int);

ALTER TABLE `Technology` ADD PRIMARY KEY (Id);




CREATE TABLE `TechnologyImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`TechnologyId` int NOT NULL, 
	`ImageSource` char (255) NOT NULL);

ALTER TABLE `TechnologyImage` ADD PRIMARY KEY (Id);




CREATE TABLE `Building`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` char (200) NOT NULL, 
	`Title` char (200) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`Text` varchar (1000), 
	`SeoDescription` varchar (1000), 
	`SeoKeywords` varchar (1000), 
	`Active` bool NOT NULL, 
	`BuildingId` int, 
	`CategoryLevel` int NOT NULL, 
	`ContentType` int NOT NULL);

ALTER TABLE `Building` ADD PRIMARY KEY (Id);




CREATE TABLE `BuildingObj`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` char (200) NOT NULL);

ALTER TABLE `BuildingObj` ADD PRIMARY KEY (Id);




CREATE TABLE `BuildingImage`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`BuildingObjId` int NOT NULL, 
	`ImageSource` char (255) NOT NULL, 
	`Title` char (200) NOT NULL);

ALTER TABLE `BuildingImage` ADD PRIMARY KEY (Id);




CREATE TABLE `Content`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` char (200) NOT NULL, 
	`Title` char (200) NOT NULL, 
	`Text` varchar (1000), 
	`SeoDescription` varchar (1000), 
	`SeoKeywords` varchar (1000), 
	`MainPage` bool NOT NULL);

ALTER TABLE `Content` ADD PRIMARY KEY (Id);




CREATE TABLE `ContentItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` varchar (1000), 
	`SortOrder` int NOT NULL, 
	`ContentId` int NOT NULL);

ALTER TABLE `ContentItem` ADD PRIMARY KEY (Id);




CREATE TABLE `TechnologyItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` varchar (1000) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`TechnologyId` int NOT NULL);

ALTER TABLE `TechnologyItem` ADD PRIMARY KEY (Id);




CREATE TABLE `BuildingItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Text` varchar (1000) NOT NULL, 
	`SortOrder` int NOT NULL, 
	`BuildingId` int NOT NULL);

ALTER TABLE `BuildingItem` ADD PRIMARY KEY (Id);




CREATE TABLE `Article`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` char (200) NOT NULL, 
	`Title` char (200) NOT NULL, 
	`Description` char (255) NOT NULL, 
	`Text` varchar (1000) NOT NULL, 
	`Date` datetime NOT NULL, 
	`Published` bool NOT NULL);

ALTER TABLE `Article` ADD PRIMARY KEY (Id);




CREATE TABLE `Reason`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (1000) NOT NULL, 
	`Text` varchar (1000) NOT NULL);

ALTER TABLE `Reason` ADD PRIMARY KEY (Id);




CREATE TABLE `Slider`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Url` char (200) NOT NULL, 
	`ImageSource` char (255) NOT NULL);

ALTER TABLE `Slider` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `TechnologyId` in table 'TechnologyImage'

ALTER TABLE `TechnologyImage`
ADD CONSTRAINT `FK_TechnologyTechnologyImage`
    FOREIGN KEY (`TechnologyId`)
    REFERENCES `Technology`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TechnologyTechnologyImage'

CREATE INDEX `IX_FK_TechnologyTechnologyImage` 
    ON `TechnologyImage`
    (`TechnologyId`);

-- Creating foreign key on `TechnologyId` in table 'Technology'

ALTER TABLE `Technology`
ADD CONSTRAINT `FK_TechnologyTechnology`
    FOREIGN KEY (`TechnologyId`)
    REFERENCES `Technology`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TechnologyTechnology'

CREATE INDEX `IX_FK_TechnologyTechnology` 
    ON `Technology`
    (`TechnologyId`);

-- Creating foreign key on `BuildingId` in table 'Building'

ALTER TABLE `Building`
ADD CONSTRAINT `FK_BuildingBuilding`
    FOREIGN KEY (`BuildingId`)
    REFERENCES `Building`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingBuilding'

CREATE INDEX `IX_FK_BuildingBuilding` 
    ON `Building`
    (`BuildingId`);

-- Creating foreign key on `BuildingObjId` in table 'BuildingImage'

ALTER TABLE `BuildingImage`
ADD CONSTRAINT `FK_BuildingObjBuildingImage`
    FOREIGN KEY (`BuildingObjId`)
    REFERENCES `BuildingObj`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingObjBuildingImage'

CREATE INDEX `IX_FK_BuildingObjBuildingImage` 
    ON `BuildingImage`
    (`BuildingObjId`);

-- Creating foreign key on `ContentId` in table 'ContentItem'

ALTER TABLE `ContentItem`
ADD CONSTRAINT `FK_ContentContentItem`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentContentItem'

CREATE INDEX `IX_FK_ContentContentItem` 
    ON `ContentItem`
    (`ContentId`);

-- Creating foreign key on `TechnologyId` in table 'TechnologyItem'

ALTER TABLE `TechnologyItem`
ADD CONSTRAINT `FK_TechnologyTechnologyItem`
    FOREIGN KEY (`TechnologyId`)
    REFERENCES `Technology`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TechnologyTechnologyItem'

CREATE INDEX `IX_FK_TechnologyTechnologyItem` 
    ON `TechnologyItem`
    (`TechnologyId`);

-- Creating foreign key on `BuildingId` in table 'BuildingItem'

ALTER TABLE `BuildingItem`
ADD CONSTRAINT `FK_BuildingBuildingItem`
    FOREIGN KEY (`BuildingId`)
    REFERENCES `Building`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildingBuildingItem'

CREATE INDEX `IX_FK_BuildingBuildingItem` 
    ON `BuildingItem`
    (`BuildingId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
