



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/12/2013 12:58:48
-- Generated from EDMX file: D:\AlexK\projects\Filimonov\Filimonov\Models\Library.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `fili`;
CREATE DATABASE `fili`;
USE `fili`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_LayoutProduct`;
--    ALTER TABLE `ProductContainer` DROP CONSTRAINT `FK_ClientProductContainer`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_ProductContainerProduct`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Layout`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Client`;
    DROP TABLE IF EXISTS `ProductContainer`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `ImageSource` TEXT  NOT NULL
);

-- Creating table 'Layout'

CREATE TABLE `Layout` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` TEXT  NOT NULL,
    `CategoryId` int  NOT NULL,
    `LayoutId` int  NOT NULL,
    `ProductContainerId` int  NOT NULL
);

-- Creating table 'Client'

CREATE TABLE `Client` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'ProductContainer'

CREATE TABLE `ProductContainer` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ClientId` int  NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'ProductProductContainer'

CREATE TABLE `ProductProductContainer` (
    `Products_Id` int  NOT NULL,
    `ProductContainers_Id` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `Products_Id`, `ProductContainers_Id` in table 'ProductProductContainer'

ALTER TABLE `ProductProductContainer`
ADD CONSTRAINT `PK_ProductProductContainer`
    PRIMARY KEY (`Products_Id`, `ProductContainers_Id` );



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

-- Creating foreign key on `LayoutId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_LayoutProduct`
    FOREIGN KEY (`LayoutId`)
    REFERENCES `Layout`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LayoutProduct'

CREATE INDEX `IX_FK_LayoutProduct` 
    ON `Product`
    (`LayoutId`);

-- Creating foreign key on `ClientId` in table 'ProductContainer'

ALTER TABLE `ProductContainer`
ADD CONSTRAINT `FK_ClientProductContainer`
    FOREIGN KEY (`ClientId`)
    REFERENCES `Client`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProductContainer'

CREATE INDEX `IX_FK_ClientProductContainer` 
    ON `ProductContainer`
    (`ClientId`);

-- Creating foreign key on `Products_Id` in table 'ProductProductContainer'

ALTER TABLE `ProductProductContainer`
ADD CONSTRAINT `FK_ProductProductContainer_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `ProductContainers_Id` in table 'ProductProductContainer'

ALTER TABLE `ProductProductContainer`
ADD CONSTRAINT `FK_ProductProductContainer_ProductContainer`
    FOREIGN KEY (`ProductContainers_Id`)
    REFERENCES `ProductContainer`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductContainer_ProductContainer'

CREATE INDEX `IX_FK_ProductProductContainer_ProductContainer` 
    ON `ProductProductContainer`
    (`ProductContainers_Id`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
