



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/29/2012 10:03:51
-- Generated from EDMX file: D:\projects\Posh\Posh\Models\Model.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `posh`;
CREATE DATABASE `posh`;
USE `posh`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `ProductCategory` DROP CONSTRAINT `FK_ProductCategory_Product`;
--    ALTER TABLE `ProductCategory` DROP CONSTRAINT `FK_ProductCategory_Category`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_ProductAlbum`;
--    ALTER TABLE `ProductElement` DROP CONSTRAINT `FK_ProductElement_Product`;
--    ALTER TABLE `ProductElement` DROP CONSTRAINT `FK_ProductElement_Element`;
--    ALTER TABLE `ProjectItem` DROP CONSTRAINT `FK_ProjectProjectItem`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Element`;
    DROP TABLE IF EXISTS `Album`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `Project`;
    DROP TABLE IF EXISTS `ProjectItem`;
    DROP TABLE IF EXISTS `News`;
    DROP TABLE IF EXISTS `ProductCategory`;
    DROP TABLE IF EXISTS `ProductElement`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Tittle` mediumtext  NOT NULL,
    `Text` longtext  NULL,
    `SeoTitle` mediumtext  NULL,
    `SeoText` longtext  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` mediumtext  NOT NULL,
    `AlbumId` int  NOT NULL,
    `Name` mediumtext  NOT NULL,
    `ImageSource` mediumtext  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` mediumtext  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Element'

CREATE TABLE `Element` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` mediumtext  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Album'

CREATE TABLE `Album` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` mediumtext  NOT NULL,
    `Name` mediumtext  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Article'

CREATE TABLE `Article` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` mediumtext  NOT NULL,
    `Text` longtext  NOT NULL,
    `Name` mediumtext  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Project'

CREATE TABLE `Project` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` mediumtext  NOT NULL,
    `TextTop` longtext  NULL,
    `TextBottom` longtext  NULL,
    `ImageSource` mediumtext  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'ProjectItem'

CREATE TABLE `ProjectItem` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` mediumtext  NOT NULL,
    `ImageSource` longtext  NOT NULL,
    `ProjectId` int  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'News'

CREATE TABLE `News` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` mediumtext  NOT NULL,
    `Title` mediumtext  NOT NULL,
    `Date` datetime  NOT NULL,
    `Text` longtext  NOT NULL
);

-- Creating table 'ProductCategory'

CREATE TABLE `ProductCategory` (
    `Products_Id` int  NOT NULL,
    `Categories_Id` int  NOT NULL
);

-- Creating table 'ProductElement'

CREATE TABLE `ProductElement` (
    `Products_Id` int  NOT NULL,
    `Elements_Id` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `Products_Id`, `Categories_Id` in table 'ProductCategory'

ALTER TABLE `ProductCategory`
ADD CONSTRAINT `PK_ProductCategory`
    PRIMARY KEY (`Products_Id`, `Categories_Id` );

-- Creating primary key on `Products_Id`, `Elements_Id` in table 'ProductElement'

ALTER TABLE `ProductElement`
ADD CONSTRAINT `PK_ProductElement`
    PRIMARY KEY (`Products_Id`, `Elements_Id` );



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `Products_Id` in table 'ProductCategory'

ALTER TABLE `ProductCategory`
ADD CONSTRAINT `FK_ProductCategory_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Categories_Id` in table 'ProductCategory'

ALTER TABLE `ProductCategory`
ADD CONSTRAINT `FK_ProductCategory_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory_Category'

CREATE INDEX `IX_FK_ProductCategory_Category` 
    ON `ProductCategory`
    (`Categories_Id`);

-- Creating foreign key on `AlbumId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_ProductAlbum`
    FOREIGN KEY (`AlbumId`)
    REFERENCES `Album`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductAlbum'

CREATE INDEX `IX_FK_ProductAlbum` 
    ON `Product`
    (`AlbumId`);

-- Creating foreign key on `Products_Id` in table 'ProductElement'

ALTER TABLE `ProductElement`
ADD CONSTRAINT `FK_ProductElement_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Elements_Id` in table 'ProductElement'

ALTER TABLE `ProductElement`
ADD CONSTRAINT `FK_ProductElement_Element`
    FOREIGN KEY (`Elements_Id`)
    REFERENCES `Element`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductElement_Element'

CREATE INDEX `IX_FK_ProductElement_Element` 
    ON `ProductElement`
    (`Elements_Id`);

-- Creating foreign key on `ProjectId` in table 'ProjectItem'

ALTER TABLE `ProjectItem`
ADD CONSTRAINT `FK_ProjectProjectItem`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `Project`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectItem'

CREATE INDEX `IX_FK_ProjectProjectItem` 
    ON `ProjectItem`
    (`ProjectId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
