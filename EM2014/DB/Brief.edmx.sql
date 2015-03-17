



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 01/20/2015 20:38:15
-- Generated from EDMX file: C:\vsp\EM2014\DB\Brief.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_leoslr`;
CREATE DATABASE `gbua_leoslr`;
USE `gbua_leoslr`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `BlankItem` DROP CONSTRAINT `FK_BlankBlankItem`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Blank`;
    DROP TABLE IF EXISTS `BlankItem`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Blank`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (500), 
	`Title` varchar (500), 
	`CustomerName` varchar (500), 
	`Status` int NOT NULL);

ALTER TABLE `Blank` ADD PRIMARY KEY (Id);




CREATE TABLE `BlankItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Question` varchar (2000), 
	`QuestionDescription` varchar (2000), 
	`Answer` longtext, 
	`CreateDate` datetime NOT NULL, 
	`UpdateDate` datetime NOT NULL, 
	`BlankId` int NOT NULL);

ALTER TABLE `BlankItem` ADD PRIMARY KEY (Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `BlankId` in table 'BlankItem'

ALTER TABLE `BlankItem`
ADD CONSTRAINT `FK_BlankBlankItem`
    FOREIGN KEY (`BlankId`)
    REFERENCES `Blank`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlankBlankItem'

CREATE INDEX `IX_FK_BlankBlankItem` 
    ON `BlankItem`
    (`BlankId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
