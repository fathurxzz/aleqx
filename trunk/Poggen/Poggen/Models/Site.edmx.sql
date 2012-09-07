



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 09/07/2012 15:25:04
-- Generated from EDMX file: D:\AlexK\projects\Poggen\Poggen\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `poggen`;
CREATE DATABASE `poggen`;
USE `poggen`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `Category`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` longtext  NOT NULL,
    `Text` longtext  NOT NULL,
    `Name` longtext  NOT NULL,
    `MainPage` longtext  NOT NULL,
    `SeoDescription` longtext  NOT NULL,
    `SeoKeywords` longtext  NOT NULL,
    `SortOrder` longtext  NOT NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` longtext  NOT NULL,
    `Title` longtext  NOT NULL,
    `SortOrder` longtext  NOT NULL,
    `SeoDescription` longtext  NOT NULL,
    `SeoKeywords` longtext  NOT NULL,
    `Text` longtext  NOT NULL,
    `MainPage` longtext  NOT NULL
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
