



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/24/2012 12:43:14
-- Generated from EDMX file: D:\projects\Shop\Shop\Models\Orders.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `toyplanet`;
CREATE DATABASE `toyplanet`;
USE `toyplanet`;

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

-- Creating table 'Order'

CREATE TABLE `Order` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `OrderDate` datetime  NOT NULL,
    `Name` mediumtext  NOT NULL,
    `Phone` mediumtext  NOT NULL,
    `DeliveryAddress` mediumtext  NOT NULL,
    `Processed` bool  NOT NULL,
    `Email` mediumtext  NULL
);

-- Creating table 'OrderItem'

CREATE TABLE `OrderItem` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Description` longtext  NULL,
    `Image` longtext  NULL,
    `Price` decimal(10,0)  NOT NULL,
    `ProductId` int  NULL,
    `Quantity` int  NOT NULL,
    `Name` longtext  NOT NULL,
    `OrderId` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `OrderId` in table 'OrderItem'

ALTER TABLE `OrderItem`
ADD CONSTRAINT `FK_OrderOrderItem`
    FOREIGN KEY (`OrderId`)
    REFERENCES `Order`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderItem'

CREATE INDEX `IX_FK_OrderOrderItem` 
    ON `OrderItem`
    (`OrderId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
