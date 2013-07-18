



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 07/18/2013 18:17:29
-- Generated from EDMX file: D:\projects\Bikini\Shop\Models\Shop.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `OrderItem` DROP CONSTRAINT `FK_OrderOrderItem`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Order`;
    DROP TABLE IF EXISTS `OrderItem`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 255 )  NOT NULL,
    `Title` varchar( 255 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 255 )  NOT NULL,
    `Title` varchar( 255 )  NOT NULL,
    `Description` longtext  NULL,
    `Price` decimal(10,0)  NOT NULL,
    `Size` varchar( 255 )  NULL,
    `Composition` longtext  NULL,
    `Brand` varchar( 255 )  NULL,
    `SortOrder` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `ImageSource` varchar( 255 )  NOT NULL,
    `Preview` varchar( 255 )  NOT NULL,
    `CategoryId` int  NOT NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 255 )  NOT NULL,
    `Title` varchar( 255 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `MainPage` bool  NOT NULL,
    `Text` longtext  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL
);

-- Creating table 'Order'

CREATE TABLE `Order` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `OrderDate` datetime  NOT NULL,
    `Name` varchar( 255 )  NOT NULL,
    `Phone` varchar( 255 )  NOT NULL,
    `DeliveryAddress` varchar( 255 )  NOT NULL,
    `Complited` bool  NOT NULL,
    `Email` varchar( 255 )  NULL,
    `Info` longtext  NULL
);

-- Creating table 'OrderItem'

CREATE TABLE `OrderItem` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Description` longtext  NULL,
    `ImageSource` varchar( 255 )  NOT NULL,
    `Price` decimal(10,0)  NOT NULL,
    `ProductId` int  NOT NULL,
    `Quantity` int  NOT NULL,
    `OrderId` int  NOT NULL,
    `ProductName` varchar( 200 )  NULL,
    `ProductTitle` varchar( 200 )  NULL,
    `CategoryName` varchar( 200 )  NULL,
    `CategoryTitle` varchar( 200 )  NULL
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
