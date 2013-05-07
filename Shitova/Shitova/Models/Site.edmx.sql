



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 05/07/2013 14:21:02
-- Generated from EDMX file: D:\projects\Shitova\Shitova\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `shitova`;
CREATE DATABASE `shitova`;
USE `shitova`;

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

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `Text` longtext  NULL,
    `MainPage` bool  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'ContentItem'

CREATE TABLE `ContentItem` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ContentType` int  NOT NULL,
    `Text` longtext  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'ContentItemImage'

CREATE TABLE `ContentItemImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` varchar( 200 )  NOT NULL,
    `ContentItemId` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `ContentItemId` in table 'ContentItemImage'

ALTER TABLE `ContentItemImage`
ADD CONSTRAINT `FK_ContentItemContentItemImage`
    FOREIGN KEY (`ContentItemId`)
    REFERENCES `ContentItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentItemContentItemImage'

CREATE INDEX `IX_FK_ContentItemContentItemImage` 
    ON `ContentItemImage`
    (`ContentItemId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
