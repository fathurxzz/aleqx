



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 08/13/2013 17:05:22
-- Generated from EDMX file: D:\projects\Listelli2013\Listelli\Models\Portfolio.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `listelli`;
CREATE DATABASE `listelli`;
USE `listelli`;

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

-- Creating table 'Designer'

CREATE TABLE `Designer` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` longtext  NOT NULL,
    `DesignerName` longtext  NOT NULL,
    `DesignerNameF` longtext  NOT NULL,
    `Description` longtext  NOT NULL,
    `ImageSource` longtext  NOT NULL
);

-- Creating table 'DesignerContent'

CREATE TABLE `DesignerContent` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `DesignerId` int  NOT NULL,
    `RoomType` longtext  NOT NULL,
    `RoomTitle` longtext  NOT NULL,
    `Description` longtext  NOT NULL
);

-- Creating table 'DesignerContantImage'

CREATE TABLE `DesignerContantImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `DesignerContentId` int  NOT NULL,
    `ImageSource` longtext  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `DesignerId` in table 'DesignerContent'

ALTER TABLE `DesignerContent`
ADD CONSTRAINT `FK_DesignerDesignerContent`
    FOREIGN KEY (`DesignerId`)
    REFERENCES `Designer`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DesignerDesignerContent'

CREATE INDEX `IX_FK_DesignerDesignerContent` 
    ON `DesignerContent`
    (`DesignerId`);

-- Creating foreign key on `DesignerContentId` in table 'DesignerContantImage'

ALTER TABLE `DesignerContantImage`
ADD CONSTRAINT `FK_DesignerContentDesignerContantImage`
    FOREIGN KEY (`DesignerContentId`)
    REFERENCES `DesignerContent`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DesignerContentDesignerContantImage'

CREATE INDEX `IX_FK_DesignerContentDesignerContantImage` 
    ON `DesignerContantImage`
    (`DesignerContentId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
