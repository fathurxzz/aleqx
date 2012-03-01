



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/01/2012 22:44:34
-- Generated from EDMX file: D:\projects\Shop\Shop\Models\Orders.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `toy`;
CREATE DATABASE `toy`;
USE `toy`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `OrderItem` DROP CONSTRAINT `FK_OrderOrderItem`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Order`;
    DROP TABLE IF EXISTS `OrderItem`;
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
    `Email` mediumtext  NULL,
    `Info` mediumtext  NULL
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
