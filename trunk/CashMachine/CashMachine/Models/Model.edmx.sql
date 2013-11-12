



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 11/12/2013 00:55:37
-- Generated from EDMX file: D:\projects\CashMachine\CashMachine\Models\Model.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Operation` DROP CONSTRAINT `FK_CardOperation`;
--    ALTER TABLE `Operation` DROP CONSTRAINT `FK_OperationTypeOperation`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Card`;
    DROP TABLE IF EXISTS `Operation`;
    DROP TABLE IF EXISTS `OperationType`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Card'

CREATE TABLE `Card` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Number` varchar( 16 )  NOT NULL,
    `Locked` bool  NOT NULL,
    `Balance` decimal(20,2)  NOT NULL,
    `Pin` varchar( 4 )  NOT NULL,
    `PinAttemptsCount` int  NOT NULL
);

-- Creating table 'Operation'

CREATE TABLE `Operation` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Amount` decimal(20,2)  NOT NULL,
    `Date` datetime  NOT NULL,
    `CardId` int  NOT NULL,
    `OperationTypeId` int  NOT NULL
);

-- Creating table 'OperationType'

CREATE TABLE `OperationType` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Description` varchar( 200 )  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `CardId` in table 'Operation'

ALTER TABLE `Operation`
ADD CONSTRAINT `FK_CardOperation`
    FOREIGN KEY (`CardId`)
    REFERENCES `Card`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CardOperation'

CREATE INDEX `IX_FK_CardOperation` 
    ON `Operation`
    (`CardId`);

-- Creating foreign key on `OperationTypeId` in table 'Operation'

ALTER TABLE `Operation`
ADD CONSTRAINT `FK_OperationTypeOperation`
    FOREIGN KEY (`OperationTypeId`)
    REFERENCES `OperationType`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OperationTypeOperation'

CREATE INDEX `IX_FK_OperationTypeOperation` 
    ON `Operation`
    (`OperationTypeId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
