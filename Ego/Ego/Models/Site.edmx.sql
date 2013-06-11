



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 06/11/2013 16:44:28
-- Generated from EDMX file: D:\projects\Ego\Ego\Models\Site.edmx
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
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Order`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `Text` longtext  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `MainPage` bool  NOT NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` TEXT  NOT NULL,
    `PreviewImageSource` TEXT  NOT NULL,
    `Description` TEXT  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Order'

CREATE TABLE `Order` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ProductId` int  NOT NULL,
    `Name` varchar( 200 )  NULL,
    `Phone` varchar( 200 )  NULL,
    `Email` varchar( 200 )  NULL,
    `Size` varchar( 200 )  NULL,
    `Description` varchar( 200 )  NULL,
    `ProductImageSource` TEXT  NULL,
    `Date` datetime  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
