



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/28/2013 14:45:09
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

--    ALTER TABLE `Technology` DROP CONSTRAINT `FK_CategoryCategory`;
--    ALTER TABLE `TechnologyImage` DROP CONSTRAINT `FK_TechnologyTechnologyImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Technology`;
    DROP TABLE IF EXISTS `TechnologyImage`;
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
    `CategoryId` int  NOT NULL,
    `CategoryLevel` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `Active` bool  NOT NULL
);

-- Creating table 'TechnologyImage'

CREATE TABLE `TechnologyImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `TechnologyId` int  NOT NULL,
    `ImageSource` TEXT  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `CategoryId` in table 'Technology'

ALTER TABLE `Technology`
ADD CONSTRAINT `FK_CategoryCategory`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Technology`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategory'

CREATE INDEX `IX_FK_CategoryCategory` 
    ON `Technology`
    (`CategoryId`);

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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
