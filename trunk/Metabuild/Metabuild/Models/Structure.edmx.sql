



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 06/19/2012 13:39:57
-- Generated from EDMX file: D:\projects\Metabuild\Metabuild\Models\Structure.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `metalbuild`;
CREATE DATABASE `metalbuild`;
USE `metalbuild`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Content` DROP CONSTRAINT `FK_ContentContent`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Content`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` longtext  NOT NULL,
    `PageTitle` longtext  NOT NULL,
    `Text` longtext  NOT NULL,
    `MainPage` bool  NOT NULL,
    `Name` longtext  NOT NULL,
    `SortOrder` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `ContentId` int  NULL,
    `ContentLevel` int  NOT NULL
);

-- Creating table 'ContentImageSet'

CREATE TABLE `ContentImageSet` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` longtext  NOT NULL,
    `ContentId` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `ContentId` in table 'Content'

ALTER TABLE `Content`
ADD CONSTRAINT `FK_ContentContent`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentContent'

CREATE INDEX `IX_FK_ContentContent` 
    ON `Content`
    (`ContentId`);

-- Creating foreign key on `ContentId` in table 'ContentImageSet'

ALTER TABLE `ContentImageSet`
ADD CONSTRAINT `FK_ContentImageContent`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentImageContent'

CREATE INDEX `IX_FK_ContentImageContent` 
    ON `ContentImageSet`
    (`ContentId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
