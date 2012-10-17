



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/17/2012 13:36:36
-- Generated from EDMX file: D:\AlexK\projects\Kulumu\Kulumu\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `kulumu`;
CREATE DATABASE `kulumu`;
USE `kulumu`;

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

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Description` TEXT  NULL,
    `ImageSource` varchar( 200 )  NOT NULL,
    `Discount` bool  NOT NULL,
    `DiscountText` varchar( 200 )  NOT NULL,
    `Price` varchar( 200 )  NOT NULL,
    `CategoryId` int  NOT NULL
);

-- Creating table 'Article'

CREATE TABLE `Article` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Date` datetime  NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Text` longtext  NULL,
    `Description` TEXT  NOT NULL
);

-- Creating table 'Conten'

CREATE TABLE `Conten` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` longtext  NOT NULL,
    `Title` longtext  NOT NULL,
    `Description` longtext  NOT NULL,
    `DescriptionTitle` longtext  NOT NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` longtext  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `CategoryId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_CategoryProduct`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProduct'

CREATE INDEX `IX_FK_CategoryProduct` 
    ON `Product`
    (`CategoryId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
