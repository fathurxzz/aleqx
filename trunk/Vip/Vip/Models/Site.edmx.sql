



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 08/27/2012 12:35:02
-- Generated from EDMX file: D:\AlexK\projects\Vip\Vip\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `LayoutProduct` DROP CONSTRAINT `FK_LayoutProduct_Layout`;
--    ALTER TABLE `LayoutProduct` DROP CONSTRAINT `FK_LayoutProduct_Product`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_CategoryProduct`;
--    ALTER TABLE `ProductProductAttribute` DROP CONSTRAINT `FK_ProductProductAttribute_Product`;
--    ALTER TABLE `ProductProductAttribute` DROP CONSTRAINT `FK_ProductProductAttribute_ProductAttribute`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_MakerProduct`;
--    ALTER TABLE `Layout` DROP CONSTRAINT `FK_LayoutLayout`;
--    ALTER TABLE `Product` DROP CONSTRAINT `FK_BrandProduct`;
--    ALTER TABLE `CategoryProductAttribute` DROP CONSTRAINT `FK_CategoryProductAttribute_Category`;
--    ALTER TABLE `CategoryProductAttribute` DROP CONSTRAINT `FK_CategoryProductAttribute_ProductAttribute`;
--    ALTER TABLE `ProjectImage` DROP CONSTRAINT `FK_ProjectProjectImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Maker`;
    DROP TABLE IF EXISTS `ProductAttribute`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Layout`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Brand`;
    DROP TABLE IF EXISTS `Project`;
    DROP TABLE IF EXISTS `ProjectImage`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `LayoutProduct`;
    DROP TABLE IF EXISTS `ProductProductAttribute`;
    DROP TABLE IF EXISTS `CategoryProductAttribute`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Maker'

CREATE TABLE `Maker` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'ProductAttribute'

CREATE TABLE `ProductAttribute` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `ImageSource` TEXT  NOT NULL,
    `DescriptionTitle` varchar( 200 )  NULL,
    `Description` longtext  NULL
);

-- Creating table 'Layout'

CREATE TABLE `Layout` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `LayoutId` int  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `CategoryId` int  NOT NULL,
    `MakerId` int  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `ImageSource` TEXT  NOT NULL,
    `BrandId` int  NOT NULL
);

-- Creating table 'Brand'

CREATE TABLE `Brand` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'Project'

CREATE TABLE `Project` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `DescriptionTitle` varchar( 200 )  NULL,
    `Description` longtext  NULL,
    `ImageSource` TEXT  NOT NULL,
    `Manager` varchar( 200 )  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'ProjectImage'

CREATE TABLE `ProjectImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` varchar( 200 )  NOT NULL,
    `ProjectId` int  NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Text` longtext  NULL,
    `DescriptionTitle` varchar( 200 )  NULL,
    `Description` longtext  NULL,
    `Name` varchar( 200 )  NOT NULL,
    `MainPage` bool  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'Article'

CREATE TABLE `Article` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Date` datetime  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Text` longtext  NOT NULL
);

-- Creating table 'LayoutProduct'

CREATE TABLE `LayoutProduct` (
    `Layouts_Id` int  NOT NULL,
    `Products_Id` int  NOT NULL
);

-- Creating table 'ProductProductAttribute'

CREATE TABLE `ProductProductAttribute` (
    `Products_Id` int  NOT NULL,
    `ProductAttributes_Id` int  NOT NULL
);

-- Creating table 'CategoryProductAttribute'

CREATE TABLE `CategoryProductAttribute` (
    `Categories_Id` int  NOT NULL,
    `ProductAttributes_Id` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `Layouts_Id`, `Products_Id` in table 'LayoutProduct'

ALTER TABLE `LayoutProduct`
ADD CONSTRAINT `PK_LayoutProduct`
    PRIMARY KEY (`Layouts_Id`, `Products_Id` );

-- Creating primary key on `Products_Id`, `ProductAttributes_Id` in table 'ProductProductAttribute'

ALTER TABLE `ProductProductAttribute`
ADD CONSTRAINT `PK_ProductProductAttribute`
    PRIMARY KEY (`Products_Id`, `ProductAttributes_Id` );

-- Creating primary key on `Categories_Id`, `ProductAttributes_Id` in table 'CategoryProductAttribute'

ALTER TABLE `CategoryProductAttribute`
ADD CONSTRAINT `PK_CategoryProductAttribute`
    PRIMARY KEY (`Categories_Id`, `ProductAttributes_Id` );



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `Layouts_Id` in table 'LayoutProduct'

ALTER TABLE `LayoutProduct`
ADD CONSTRAINT `FK_LayoutProduct_Layout`
    FOREIGN KEY (`Layouts_Id`)
    REFERENCES `Layout`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `Products_Id` in table 'LayoutProduct'

ALTER TABLE `LayoutProduct`
ADD CONSTRAINT `FK_LayoutProduct_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LayoutProduct_Product'

CREATE INDEX `IX_FK_LayoutProduct_Product` 
    ON `LayoutProduct`
    (`Products_Id`);

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

-- Creating foreign key on `Products_Id` in table 'ProductProductAttribute'

ALTER TABLE `ProductProductAttribute`
ADD CONSTRAINT `FK_ProductProductAttribute_Product`
    FOREIGN KEY (`Products_Id`)
    REFERENCES `Product`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `ProductAttributes_Id` in table 'ProductProductAttribute'

ALTER TABLE `ProductProductAttribute`
ADD CONSTRAINT `FK_ProductProductAttribute_ProductAttribute`
    FOREIGN KEY (`ProductAttributes_Id`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductAttribute_ProductAttribute'

CREATE INDEX `IX_FK_ProductProductAttribute_ProductAttribute` 
    ON `ProductProductAttribute`
    (`ProductAttributes_Id`);

-- Creating foreign key on `MakerId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_MakerProduct`
    FOREIGN KEY (`MakerId`)
    REFERENCES `Maker`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_MakerProduct'

CREATE INDEX `IX_FK_MakerProduct` 
    ON `Product`
    (`MakerId`);

-- Creating foreign key on `LayoutId` in table 'Layout'

ALTER TABLE `Layout`
ADD CONSTRAINT `FK_LayoutLayout`
    FOREIGN KEY (`LayoutId`)
    REFERENCES `Layout`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LayoutLayout'

CREATE INDEX `IX_FK_LayoutLayout` 
    ON `Layout`
    (`LayoutId`);

-- Creating foreign key on `BrandId` in table 'Product'

ALTER TABLE `Product`
ADD CONSTRAINT `FK_BrandProduct`
    FOREIGN KEY (`BrandId`)
    REFERENCES `Brand`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandProduct'

CREATE INDEX `IX_FK_BrandProduct` 
    ON `Product`
    (`BrandId`);

-- Creating foreign key on `Categories_Id` in table 'CategoryProductAttribute'

ALTER TABLE `CategoryProductAttribute`
ADD CONSTRAINT `FK_CategoryProductAttribute_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `ProductAttributes_Id` in table 'CategoryProductAttribute'

ALTER TABLE `CategoryProductAttribute`
ADD CONSTRAINT `FK_CategoryProductAttribute_ProductAttribute`
    FOREIGN KEY (`ProductAttributes_Id`)
    REFERENCES `ProductAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProductAttribute_ProductAttribute'

CREATE INDEX `IX_FK_CategoryProductAttribute_ProductAttribute` 
    ON `CategoryProductAttribute`
    (`ProductAttributes_Id`);

-- Creating foreign key on `ProjectId` in table 'ProjectImage'

ALTER TABLE `ProjectImage`
ADD CONSTRAINT `FK_ProjectProjectImage`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `Project`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectProjectImage'

CREATE INDEX `IX_FK_ProjectProjectImage` 
    ON `ProjectImage`
    (`ProjectId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
