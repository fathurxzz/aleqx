



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 04/29/2013 11:51:34
-- Generated from EDMX file: D:\projects\Listelli2013\Listelli\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Language`;
    DROP TABLE IF EXISTS `Content`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Language'

CREATE TABLE `Language` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Code` longtext  NOT NULL,
    `Name` longtext  NOT NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `MainPage` bool  NOT NULL
);

-- Creating table 'ContentLang'

CREATE TABLE `ContentLang` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` longtext  NOT NULL,
    `Text` longtext  NOT NULL,
    `ContentId` int  NOT NULL,
    `LanguageId` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `ContentId` in table 'ContentLang'

ALTER TABLE `ContentLang`
ADD CONSTRAINT `FK_ContentContentLang`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentContentLang'

CREATE INDEX `IX_FK_ContentContentLang` 
    ON `ContentLang`
    (`ContentId`);

-- Creating foreign key on `LanguageId` in table 'ContentLang'

ALTER TABLE `ContentLang`
ADD CONSTRAINT `FK_LanguageContentLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageContentLang'

CREATE INDEX `IX_FK_LanguageContentLang` 
    ON `ContentLang`
    (`LanguageId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
