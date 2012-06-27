



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 06/27/2012 16:36:57
-- Generated from EDMX file: D:\projects\Rvk\Rvk\Models\Model.edmx
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
    DROP TABLE IF EXISTS `FeedbackSet`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Subscriber'

CREATE TABLE `Subscriber` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Email` longtext  NOT NULL
);

-- Creating table 'Feedback'

CREATE TABLE `Feedback` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` longtext  NOT NULL,
    `Email` longtext  NULL,
    `Text` longtext  NOT NULL
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
