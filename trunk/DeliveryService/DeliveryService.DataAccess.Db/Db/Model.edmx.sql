



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 03/17/2014 16:08:02
-- Generated from EDMX file: D:\projects\DeliveryService\DeliveryService.DataAccess.Db\Db\Model.edmx
-- Target version: 3.0.0.0
-- --------------------------------------------------

DROP DATABASE IF EXISTS `gbua_delivery`;
CREATE DATABASE `gbua_delivery`;
USE `gbua_delivery`;

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CompanyProduct`;
--    ALTER TABLE `CompanyCategory` DROP CONSTRAINT `FK_CompanyCategory_Company`;
--    ALTER TABLE `CompanyCategory` DROP CONSTRAINT `FK_CompanyCategory_Category`;
--    ALTER TABLE `CityMasterCategory` DROP CONSTRAINT `FK_CityMasterCategory_City`;
--    ALTER TABLE `CityMasterCategory` DROP CONSTRAINT `FK_CityMasterCategory_MasterCategory`;
--    ALTER TABLE `MasterCategoryCompany` DROP CONSTRAINT `FK_MasterCategoryCompany_MasterCategory`;
--    ALTER TABLE `MasterCategoryCompany` DROP CONSTRAINT `FK_MasterCategoryCompany_Company`;
--    ALTER TABLE `ProductLocalInfo` DROP CONSTRAINT `FK_ProductProductLocalInfo`;
--    ALTER TABLE `CompanyLocalInfo` DROP CONSTRAINT `FK_CompanyCompanyLocalInfo`;
--    ALTER TABLE `CompanyLocalInfo` DROP CONSTRAINT `FK_CityCompanyLocalInfo`;
--    ALTER TABLE `ProductLocalInfo` DROP CONSTRAINT `FK_CityProductLocalInfo`;
--    ALTER TABLE `ProductCategory` DROP CONSTRAINT `FK_ProductCategory_Product`;
--    ALTER TABLE `ProductCategory` DROP CONSTRAINT `FK_ProductCategory_Category`;
--    ALTER TABLE `CityCompany` DROP CONSTRAINT `FK_CityCompany_City`;
--    ALTER TABLE `CityCompany` DROP CONSTRAINT `FK_CityCompany_Company`;
--    ALTER TABLE `QualityCompany` DROP CONSTRAINT `FK_QualityCompany_Quality`;
--    ALTER TABLE `QualityCompany` DROP CONSTRAINT `FK_QualityCompany_Company`;
--    ALTER TABLE `CriteriaCompanyLocalInfo` DROP CONSTRAINT `FK_CriteriaCompanyLocalInfo_Criteria`;
--    ALTER TABLE `CriteriaCompanyLocalInfo` DROP CONSTRAINT `FK_CriteriaCompanyLocalInfo_CompanyLocalInfo`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `City`;
    DROP TABLE IF EXISTS `Company`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `MasterCategory`;
    DROP TABLE IF EXISTS `ProductLocalInfo`;
    DROP TABLE IF EXISTS `CompanyLocalInfo`;
    DROP TABLE IF EXISTS `Criteria`;
    DROP TABLE IF EXISTS `Quality`;
    DROP TABLE IF EXISTS `CompanyCategory`;
    DROP TABLE IF EXISTS `CityMasterCategory`;
    DROP TABLE IF EXISTS `MasterCategoryCompany`;
    DROP TABLE IF EXISTS `ProductCategory`;
    DROP TABLE IF EXISTS `CityCompany`;
    DROP TABLE IF EXISTS `QualityCompany`;
    DROP TABLE IF EXISTS `CriteriaCompanyLocalInfo`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

CREATE TABLE `City`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`IsActive` bool NOT NULL);

ALTER TABLE `City` ADD PRIMARY KEY (Id);




CREATE TABLE `Company`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`Description` longtext, 
	`ImageSource` varchar (200) NOT NULL);

ALTER TABLE `Company` ADD PRIMARY KEY (Id);




CREATE TABLE `Product`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`Title` varchar (200) NOT NULL, 
	`CompanyId` int NOT NULL, 
	`Description` longtext, 
	`ImageSource` varchar (200) NOT NULL);

ALTER TABLE `Product` ADD PRIMARY KEY (Id);




CREATE TABLE `Category`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`Title` varchar (200) NOT NULL);

ALTER TABLE `Category` ADD PRIMARY KEY (Id);




CREATE TABLE `MasterCategory`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Name` varchar (200) NOT NULL, 
	`Title` varchar (200) NOT NULL);

ALTER TABLE `MasterCategory` ADD PRIMARY KEY (Id);




CREATE TABLE `ProductLocalInfo`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Price` decimal( 10, 2 )  NOT NULL, 
	`ProductId` int NOT NULL, 
	`CityId` int NOT NULL);

ALTER TABLE `ProductLocalInfo` ADD PRIMARY KEY (Id);




CREATE TABLE `CompanyLocalInfo`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`WorkTime` varchar (200) NOT NULL, 
	`CompanyId` int NOT NULL, 
	`CityId` int NOT NULL, 
	`Description` varchar (200), 
	`DeliveryCost` decimal( 10, 2 )  NOT NULL, 
	`MinOrderAmount` longtext NOT NULL, 
	`Title` varchar (200) NOT NULL);

ALTER TABLE `CompanyLocalInfo` ADD PRIMARY KEY (Id);




CREATE TABLE `Criteria`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL);

ALTER TABLE `Criteria` ADD PRIMARY KEY (Id);




CREATE TABLE `Quality`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` varchar (200) NOT NULL);

ALTER TABLE `Quality` ADD PRIMARY KEY (Id);




CREATE TABLE `CategoryLocalInfo`(
	`Id` int NOT NULL AUTO_INCREMENT UNIQUE, 
	`Title` longtext NOT NULL, 
	`CityId` int NOT NULL, 
	`CategoryId` int NOT NULL);

ALTER TABLE `CategoryLocalInfo` ADD PRIMARY KEY (Id);




CREATE TABLE `CompanyCategory`(
	`Companies_Id` int NOT NULL, 
	`Categories_Id` int NOT NULL);

ALTER TABLE `CompanyCategory` ADD PRIMARY KEY (Companies_Id, Categories_Id);




CREATE TABLE `CityMasterCategory`(
	`Cities_Id` int NOT NULL, 
	`MasterCategories_Id` int NOT NULL);

ALTER TABLE `CityMasterCategory` ADD PRIMARY KEY (Cities_Id, MasterCategories_Id);




CREATE TABLE `MasterCategoryCompany`(
	`MasterCategories_Id` int NOT NULL, 
	`Companies_Id` int NOT NULL);

ALTER TABLE `MasterCategoryCompany` ADD PRIMARY KEY (MasterCategories_Id, Companies_Id);




CREATE TABLE `ProductCategory`(
	`Product_Id` int NOT NULL, 
	`Category_Id` int NOT NULL);

ALTER TABLE `ProductCategory` ADD PRIMARY KEY (Product_Id, Category_Id);




CREATE TABLE `CityCompany`(
	`Cities_Id` int NOT NULL, 
	`Companies_Id` int NOT NULL);

ALTER TABLE `CityCompany` ADD PRIMARY KEY (Cities_Id, Companies_Id);




CREATE TABLE `QualityCompany`(
	`Qualities_Id` int NOT NULL, 
	`Companies_Id` int NOT NULL);

ALTER TABLE `QualityCompany` ADD PRIMARY KEY (Qualities_Id, Companies_Id);




CREATE TABLE `CriteriaCompanyLocalInfo`(
	`Criteria_Id` int NOT NULL, 
	`CompanyLocalInfo_Id` int NOT NULL);

ALTER TABLE `CriteriaCompanyLocalInfo` ADD PRIMARY KEY (Criteria_Id, CompanyLocalInfo_Id);






-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `CompanyId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_CompanyProduct`
    FOREIGN KEY (`CompanyId`)
    REFERENCES `Company`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyProduct'

CREATE INDEX `IX_FK_CompanyProduct` 
    ON `Product`
    (`CompanyId`);

-- Creating foreign key on `Companies_Id` in table 'CompanyCategory'

ALTER TABLE `CompanyCategory`
ADD CONSTRAINT `FK_CompanyCategory_Company`
    FOREIGN KEY (`Companies_Id`)
    REFERENCES `Company`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Categories_Id` in table 'CompanyCategory'

ALTER TABLE `CompanyCategory`
ADD CONSTRAINT `FK_CompanyCategory_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyCategory_Category'

CREATE INDEX `IX_FK_CompanyCategory_Category` 
    ON `CompanyCategory`
    (`Categories_Id`);

-- Creating foreign key on `Cities_Id` in table 'CityMasterCategory'

ALTER TABLE `CityMasterCategory`
ADD CONSTRAINT `FK_CityMasterCategory_City`
    FOREIGN KEY (`Cities_Id`)
    REFERENCES `City`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `MasterCategories_Id` in table 'CityMasterCategory'

ALTER TABLE `CityMasterCategory`
ADD CONSTRAINT `FK_CityMasterCategory_MasterCategory`
    FOREIGN KEY (`MasterCategories_Id`)
    REFERENCES `MasterCategory`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityMasterCategory_MasterCategory'

CREATE INDEX `IX_FK_CityMasterCategory_MasterCategory` 
    ON `CityMasterCategory`
    (`MasterCategories_Id`);

-- Creating foreign key on `MasterCategories_Id` in table 'MasterCategoryCompany'

ALTER TABLE `MasterCategoryCompany`
ADD CONSTRAINT `FK_MasterCategoryCompany_MasterCategory`
    FOREIGN KEY (`MasterCategories_Id`)
    REFERENCES `MasterCategory`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Companies_Id` in table 'MasterCategoryCompany'

ALTER TABLE `MasterCategoryCompany`
ADD CONSTRAINT `FK_MasterCategoryCompany_Company`
    FOREIGN KEY (`Companies_Id`)
    REFERENCES `Company`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MasterCategoryCompany_Company'

CREATE INDEX `IX_FK_MasterCategoryCompany_Company` 
    ON `MasterCategoryCompany`
    (`Companies_Id`);

-- Creating foreign key on `ProductId` in table 'ProductLocalInfo'

ALTER TABLE `ProductLocalInfo`
ADD CONSTRAINT `FK_ProductProductLocalInfo`
    FOREIGN KEY (`ProductId`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductLocalInfo'

CREATE INDEX `IX_FK_ProductProductLocalInfo` 
    ON `ProductLocalInfo`
    (`ProductId`);

-- Creating foreign key on `CompanyId` in table 'CompanyLocalInfo'

ALTER TABLE `CompanyLocalInfo`
ADD CONSTRAINT `FK_CompanyCompanyLocalInfo`
    FOREIGN KEY (`CompanyId`)
    REFERENCES `Company`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyCompanyLocalInfo'

CREATE INDEX `IX_FK_CompanyCompanyLocalInfo` 
    ON `CompanyLocalInfo`
    (`CompanyId`);

-- Creating foreign key on `CityId` in table 'CompanyLocalInfo'

ALTER TABLE `CompanyLocalInfo`
ADD CONSTRAINT `FK_CityCompanyLocalInfo`
    FOREIGN KEY (`CityId`)
    REFERENCES `City`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityCompanyLocalInfo'

CREATE INDEX `IX_FK_CityCompanyLocalInfo` 
    ON `CompanyLocalInfo`
    (`CityId`);

-- Creating foreign key on `CityId` in table 'ProductLocalInfo'

ALTER TABLE `ProductLocalInfo`
ADD CONSTRAINT `FK_CityProductLocalInfo`
    FOREIGN KEY (`CityId`)
    REFERENCES `City`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityProductLocalInfo'

CREATE INDEX `IX_FK_CityProductLocalInfo` 
    ON `ProductLocalInfo`
    (`CityId`);

-- Creating foreign key on `Product_Id` in table 'ProductCategory'

ALTER TABLE `ProductCategory`
ADD CONSTRAINT `FK_ProductCategory_Product`
    FOREIGN KEY (`Product_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Category_Id` in table 'ProductCategory'

ALTER TABLE `ProductCategory`
ADD CONSTRAINT `FK_ProductCategory_Category`
    FOREIGN KEY (`Category_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory_Category'

CREATE INDEX `IX_FK_ProductCategory_Category` 
    ON `ProductCategory`
    (`Category_Id`);

-- Creating foreign key on `Cities_Id` in table 'CityCompany'

ALTER TABLE `CityCompany`
ADD CONSTRAINT `FK_CityCompany_City`
    FOREIGN KEY (`Cities_Id`)
    REFERENCES `City`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Companies_Id` in table 'CityCompany'

ALTER TABLE `CityCompany`
ADD CONSTRAINT `FK_CityCompany_Company`
    FOREIGN KEY (`Companies_Id`)
    REFERENCES `Company`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityCompany_Company'

CREATE INDEX `IX_FK_CityCompany_Company` 
    ON `CityCompany`
    (`Companies_Id`);

-- Creating foreign key on `Qualities_Id` in table 'QualityCompany'

ALTER TABLE `QualityCompany`
ADD CONSTRAINT `FK_QualityCompany_Quality`
    FOREIGN KEY (`Qualities_Id`)
    REFERENCES `Quality`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Companies_Id` in table 'QualityCompany'

ALTER TABLE `QualityCompany`
ADD CONSTRAINT `FK_QualityCompany_Company`
    FOREIGN KEY (`Companies_Id`)
    REFERENCES `Company`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QualityCompany_Company'

CREATE INDEX `IX_FK_QualityCompany_Company` 
    ON `QualityCompany`
    (`Companies_Id`);

-- Creating foreign key on `Criteria_Id` in table 'CriteriaCompanyLocalInfo'

ALTER TABLE `CriteriaCompanyLocalInfo`
ADD CONSTRAINT `FK_CriteriaCompanyLocalInfo_Criteria`
    FOREIGN KEY (`Criteria_Id`)
    REFERENCES `Criteria`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `CompanyLocalInfo_Id` in table 'CriteriaCompanyLocalInfo'

ALTER TABLE `CriteriaCompanyLocalInfo`
ADD CONSTRAINT `FK_CriteriaCompanyLocalInfo_CompanyLocalInfo`
    FOREIGN KEY (`CompanyLocalInfo_Id`)
    REFERENCES `CompanyLocalInfo`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CriteriaCompanyLocalInfo_CompanyLocalInfo'

CREATE INDEX `IX_FK_CriteriaCompanyLocalInfo_CompanyLocalInfo` 
    ON `CriteriaCompanyLocalInfo`
    (`CompanyLocalInfo_Id`);

-- Creating foreign key on `CityId` in table 'CategoryLocalInfo'

ALTER TABLE `CategoryLocalInfo`
ADD CONSTRAINT `FK_CityCategoryLocalInfo`
    FOREIGN KEY (`CityId`)
    REFERENCES `City`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityCategoryLocalInfo'

CREATE INDEX `IX_FK_CityCategoryLocalInfo` 
    ON `CategoryLocalInfo`
    (`CityId`);

-- Creating foreign key on `CategoryId` in table 'CategoryLocalInfo'

ALTER TABLE `CategoryLocalInfo`
ADD CONSTRAINT `FK_CategoryCategoryLocalInfo`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategoryLocalInfo'

CREATE INDEX `IX_FK_CategoryCategoryLocalInfo` 
    ON `CategoryLocalInfo`
    (`CategoryId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
