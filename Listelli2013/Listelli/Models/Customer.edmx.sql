



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 02/16/2014 20:11:21
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

CREATE TABLE `Subscriber`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Email` char (200) NOT NULL, 
	`Guid` char (50) NOT NULL, 
	`Active` bool NOT NULL, 
	`SentConfirmation` bool NOT NULL, 
	`SentConfirmationAttempt` int NOT NULL);

ALTER TABLE `Subscriber` ADD PRIMARY KEY (Id);




CREATE TABLE `TestTable`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL);

ALTER TABLE `TestTable` ADD PRIMARY KEY (Id);




CREATE TABLE `SendEmailStatus`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ArticleId` int NOT NULL, 
	`SubscriberId` int NOT NULL, 
	`Status` int NOT NULL, 
	`Date` datetime NOT NULL, 
	`SendDate` datetime NOT NULL, 
	`ErrorMessage` char (255), 
	`Attempt` int NOT NULL);

ALTER TABLE `SendEmailStatus` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
