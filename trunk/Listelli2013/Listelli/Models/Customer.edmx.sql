



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/08/2013 13:00:29
-- Generated from EDMX file: D:\projects\Listelli2013\Listelli\Models\Customer.edmx
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
    DROP TABLE IF EXISTS `Subscriber`;
    DROP TABLE IF EXISTS `TestTable`;
    DROP TABLE IF EXISTS `SendEmailStatus`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Subscriber'

CREATE TABLE `Subscriber` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Email` varchar( 200 )  NOT NULL,
    `Guid` varchar( 50 )  NOT NULL,
    `Active` bool  NOT NULL
);

-- Creating table 'TestTable'

CREATE TABLE `TestTable` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Date` datetime  NOT NULL
);

-- Creating table 'SendEmailStatus'

CREATE TABLE `SendEmailStatus` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ArticleId` int  NOT NULL,
    `SubscriberId` int  NOT NULL,
    `Status` int  NOT NULL,
    `Date` datetime  NOT NULL,
    `SendDate` datetime  NOT NULL,
    `ErrorMessage` TEXT  NULL,
    `Attempt` int  NOT NULL
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
