



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 08/15/2013 11:19:36
-- Generated from EDMX file: D:\projects\Listelli2013\Listelli\Models\Portfolio.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `DesignerContent` DROP CONSTRAINT `FK_DesignerDesignerContent`;
--    ALTER TABLE `DesignerContantImage` DROP CONSTRAINT `FK_DesignerContentDesignerContantImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Designer`;
    DROP TABLE IF EXISTS `DesignerContent`;
    DROP TABLE IF EXISTS `DesignerContantImage`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Designer'

CREATE TABLE `Designer` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `DesignerName` varchar( 200 )  NOT NULL,
    `DesignerNameF` varchar( 200 )  NOT NULL,
    `Description` longtext  NULL,
    `ImageSource` TEXT  NOT NULL
);

-- Creating table 'DesignerContent'

CREATE TABLE `DesignerContent` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `DesignerId` int  NOT NULL,
    `RoomType` int  NOT NULL,
    `RoomTitle` varchar( 200 )  NOT NULL,
    `Description` longtext  NULL
);

-- Creating table 'DesignerContantImage'

CREATE TABLE `DesignerContantImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `DesignerContentId` int  NOT NULL,
    `ImageSource` TEXT  NOT NULL
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
