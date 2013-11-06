



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 11/06/2013 12:11:26
-- Generated from EDMX file: D:\projects\Penetron\Penetron\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `penetron`;
CREATE DATABASE `penetron`;
USE `penetron`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `TechnologyImage` DROP CONSTRAINT `FK_TechnologyTechnologyImage`;
--    ALTER TABLE `Technology` DROP CONSTRAINT `FK_TechnologyTechnology`;
--    ALTER TABLE `Building` DROP CONSTRAINT `FK_BuildingBuilding`;
--    ALTER TABLE `BuildingObj` DROP CONSTRAINT `FK_BuildingBuildingObj`;
--    ALTER TABLE `BuildingImage` DROP CONSTRAINT `FK_BuildingObjBuildingImage`;

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
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Technology'

CREATE TABLE `Technology` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `Text` longtext  NULL,
    `CategoryLevel` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `Active` bool  NOT NULL,
    `TechnologyId` int  NULL
);

-- Creating table 'TechnologyImage'

CREATE TABLE `TechnologyImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `TechnologyId` int  NOT NULL,
    `ImageSource` TEXT  NOT NULL
);

-- Creating table 'Building'

CREATE TABLE `Building` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `Text` longtext  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `Active` bool  NOT NULL,
    `BuildingId` int  NULL,
    `CategoryLevel` int  NOT NULL
);

-- Creating table 'BuildingObj'

CREATE TABLE `BuildingObj` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'BuildingImage'

CREATE TABLE `BuildingImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `BuildingObjId` int  NOT NULL,
    `ImageSource` TEXT  NOT NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Text` longtext  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `MainPage` bool  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
