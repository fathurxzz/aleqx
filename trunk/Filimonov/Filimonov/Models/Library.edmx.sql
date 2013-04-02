



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 04/02/2013 17:56:46
-- Generated from EDMX file: D:\AlexK\projects\Filimonov\Filimonov\Models\Library.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_LayoutProduct`;
--    ALTER TABLE `ProductSet` DROP CONSTRAINT `FK_CustomerProductSet`;
--    ALTER TABLE `ProductProductSet` DROP CONSTRAINT `FK_ProductProductSet_Product`;
--    ALTER TABLE `ProductProductSet` DROP CONSTRAINT `FK_ProductProductSet_ProductSet`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Layout`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Customer`;
    DROP TABLE IF EXISTS `ProductSet`;
    DROP TABLE IF EXISTS `ProductProductSet`;
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
    `LayoutId` int  NOT NULL
);

-- Creating table 'Customer'

CREATE TABLE `Customer` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `SurveyTitle` varchar( 200 )  NOT NULL,
    `SurveyDate` datetime  NOT NULL,
    `SurveyDescription` longtext  NULL
);

-- Creating table 'ProductSet'

CREATE TABLE `ProductSet` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `CustomerId` int  NOT NULL
);

-- Creating table 'Survey'

CREATE TABLE `Survey` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Question` longtext  NULL,
    `Answer` longtext  NULL,
    `Note` longtext  NULL,
    `Number` varchar( 200 )  NOT NULL,
    `CustomerId` int  NOT NULL
);

-- Creating table 'ProductProductSet'

CREATE TABLE `ProductProductSet` (
    `Products_Id` int  NOT NULL,
    `ProductSets_Id` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `Products_Id`, `ProductSets_Id` in table 'ProductProductSet'

ALTER TABLE `ProductProductSet`
ADD CONSTRAINT `PK_ProductProductSet`
    PRIMARY KEY (`Products_Id`, `ProductSets_Id` );



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

-- Creating foreign key on `CustomerId` in table 'ProductSet'

ALTER TABLE `ProductSet`
ADD CONSTRAINT `FK_CustomerProductSet`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `Customer`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerProductSet'

CREATE INDEX `IX_FK_CustomerProductSet` 
    ON `ProductSet`
    (`CustomerId`);

-- Creating foreign key on `Products_Id` in table 'ProductProductSet'

ALTER TABLE `ProductProductSet`
ADD CONSTRAINT `FK_ProductProductSet_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `ProductSets_Id` in table 'ProductProductSet'

ALTER TABLE `ProductProductSet`
ADD CONSTRAINT `FK_ProductProductSet_ProductSet`
    FOREIGN KEY (`ProductSets_Id`)
    REFERENCES `ProductSet`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductSet_ProductSet'

CREATE INDEX `IX_FK_ProductProductSet_ProductSet` 
    ON `ProductProductSet`
    (`ProductSets_Id`);

-- Creating foreign key on `CustomerId` in table 'Survey'

ALTER TABLE `Survey`
ADD CONSTRAINT `FK_CustomerSurvey`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `Customer`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerSurvey'

CREATE INDEX `IX_FK_CustomerSurvey` 
    ON `Survey`
    (`CustomerId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
