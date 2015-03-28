



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/28/2015 20:44:29
-- Generated from EDMX file: C:\vsp\FashionIntention\FashionIntention.DataModel\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_fashint`;
CREATE DATABASE `gbua_fashint`;
USE `gbua_fashint`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `PostItem` DROP CONSTRAINT `FK_PostPostItem`;
--    ALTER TABLE `PostTag` DROP CONSTRAINT `FK_PostTag_Post`;
--    ALTER TABLE `PostTag` DROP CONSTRAINT `FK_PostTag_Tag`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Post`;
    DROP TABLE IF EXISTS `Tag`;
    DROP TABLE IF EXISTS `PostItem`;
    DROP TABLE IF EXISTS `PostTag`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `Post`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Date` datetime NOT NULL, 
	`Title` varchar (500), 
	`Description` varchar (1000), 
	`ImageSrc` varchar (500), 
	`Published` bool NOT NULL);

ALTER TABLE `Post` ADD PRIMARY KEY (Id);




CREATE TABLE `Tag`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (100));

ALTER TABLE `Tag` ADD PRIMARY KEY (Id);




CREATE TABLE `PostItem`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`ImageSrc` varchar (500), 
	`Text` longtext, 
	`SortOrder` int NOT NULL, 
	`PostId` int NOT NULL);

ALTER TABLE `PostItem` ADD PRIMARY KEY (Id);




CREATE TABLE `PostTag`(
	`Posts_Id` int NOT NULL, 
	`Tags_Id` int NOT NULL);

ALTER TABLE `PostTag` ADD PRIMARY KEY (Posts_Id, Tags_Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `PostId` in table 'PostItem'

ALTER TABLE `PostItem`
ADD CONSTRAINT `FK_PostPostItem`
    FOREIGN KEY (`PostId`)
    REFERENCES `Post`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostPostItem'

CREATE INDEX `IX_FK_PostPostItem` 
    ON `PostItem`
    (`PostId`);

-- Creating foreign key on `Posts_Id` in table 'PostTag'

ALTER TABLE `PostTag`
ADD CONSTRAINT `FK_PostTag_Post`
    FOREIGN KEY (`Posts_Id`)
    REFERENCES `Post`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Tags_Id` in table 'PostTag'

ALTER TABLE `PostTag`
ADD CONSTRAINT `FK_PostTag_Tag`
    FOREIGN KEY (`Tags_Id`)
    REFERENCES `Tag`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostTag_Tag'

CREATE INDEX `IX_FK_PostTag_Tag` 
    ON `PostTag`
    (`Tags_Id`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
