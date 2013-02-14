



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/14/2013 08:57:31
-- Generated from EDMX file: D:\AlexK\projects\Filimonov\Filimonov\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `ProjectImage` DROP CONSTRAINT `FK_ProjectProjectImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Project`;
    DROP TABLE IF EXISTS `ProjectImage`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `ContentType` int  NOT NULL,
    `Text` longtext  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Project'

CREATE TABLE `Project` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `DescriptionTitle` varchar( 200 )  NULL,
    `Description` longtext  NULL,
    `SortOrder` int  NOT NULL,
    `ImageSource` varchar( 200 )  NULL,
    `VideoSource` longtext  NULL
);

-- Creating table 'ProjectImage'

CREATE TABLE `ProjectImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` varchar( 200 )  NOT NULL,
    `ProjectId` int  NOT NULL,
    `MainImage` bool  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
