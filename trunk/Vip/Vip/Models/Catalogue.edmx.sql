



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 10/12/2012 09:49:52
-- Generated from EDMX file: D:\AlexK\projects\Vip\Vip\Models\Catalogue.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `Product` DROP CONSTRAINT `FK_BrandProduct`;
--    ALTER TABLE `Brand` DROP CONSTRAINT `FK_CategoryBrand`;
--    ALTER TABLE `CategoryCategoryAttribute` DROP CONSTRAINT `FK_CategoryCategoryAttribute_Category`;
--    ALTER TABLE `CategoryCategoryAttribute` DROP CONSTRAINT `FK_CategoryCategoryAttribute_CategoryAttribute`;
--    ALTER TABLE `BrandCategoryAttribute` DROP CONSTRAINT `FK_BrandCategoryAttribute_Brand`;
--    ALTER TABLE `BrandCategoryAttribute` DROP CONSTRAINT `FK_BrandCategoryAttribute_CategoryAttribute`;
--    ALTER TABLE `ProjectImage` DROP CONSTRAINT `FK_ProjectProjectImage`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `Product`;
    DROP TABLE IF EXISTS `Brand`;
    DROP TABLE IF EXISTS `CategoryAttribute`;
    DROP TABLE IF EXISTS `Article`;
    DROP TABLE IF EXISTS `Project`;
    DROP TABLE IF EXISTS `ProjectImage`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `MainPageImage`;
    DROP TABLE IF EXISTS `CategoryCategoryAttribute`;
    DROP TABLE IF EXISTS `BrandCategoryAttribute`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `DescriptionTitle` varchar( 200 )  NULL,
    `Description` longtext  NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL
);

-- Creating table 'Product'

CREATE TABLE `Product` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` TEXT  NOT NULL,
    `BrandId` int  NOT NULL
);

-- Creating table 'Brand'

CREATE TABLE `Brand` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `CategoryId` int  NOT NULL,
    `SortOrder` int  NOT NULL,
    `Href` varchar( 255 )  NULL
);

-- Creating table 'CategoryAttribute'

CREATE TABLE `CategoryAttribute` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL
);

-- Creating table 'Article'

CREATE TABLE `Article` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Date` datetime  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Text` longtext  NOT NULL
);

-- Creating table 'Project'

CREATE TABLE `Project` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `DescriptionTitle` varchar( 200 )  NULL,
    `Description` longtext  NULL,
    `SortOrder` int  NOT NULL,
    `SeoDescription` longtext  NULL,
    `SeoKeywords` longtext  NULL
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

-- Creating table 'MainPageImage'

CREATE TABLE `MainPageImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` varchar( 255 )  NOT NULL
);

-- Creating table 'CategoryCategoryAttribute'

CREATE TABLE `CategoryCategoryAttribute` (
    `Categories_Id` int  NOT NULL,
    `CategoryAttributes_Id` int  NOT NULL
);

-- Creating table 'BrandCategoryAttribute'

CREATE TABLE `BrandCategoryAttribute` (
    `Brands_Id` int  NOT NULL,
    `CategoryAttributes_Id` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on `Categories_Id`, `CategoryAttributes_Id` in table 'CategoryCategoryAttribute'

ALTER TABLE `CategoryCategoryAttribute`
ADD CONSTRAINT `PK_CategoryCategoryAttribute`
    PRIMARY KEY (`Categories_Id`, `CategoryAttributes_Id` );

-- Creating primary key on `Brands_Id`, `CategoryAttributes_Id` in table 'BrandCategoryAttribute'

ALTER TABLE `BrandCategoryAttribute`
ADD CONSTRAINT `PK_BrandCategoryAttribute`
    PRIMARY KEY (`Brands_Id`, `CategoryAttributes_Id` );



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on `CategoryId` in table 'Brand'

ALTER TABLE `Brand`
ADD CONSTRAINT `FK_CategoryBrand`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryBrand'

CREATE INDEX `IX_FK_CategoryBrand` 
    ON `Brand`
    (`CategoryId`);

-- Creating foreign key on `Categories_Id` in table 'CategoryCategoryAttribute'

ALTER TABLE `CategoryCategoryAttribute`
ADD CONSTRAINT `FK_CategoryCategoryAttribute_Category`
    FOREIGN KEY (`Categories_Id`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `CategoryAttributes_Id` in table 'CategoryCategoryAttribute'

ALTER TABLE `CategoryCategoryAttribute`
ADD CONSTRAINT `FK_CategoryCategoryAttribute_CategoryAttribute`
    FOREIGN KEY (`CategoryAttributes_Id`)
    REFERENCES `CategoryAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategoryAttribute_CategoryAttribute'

CREATE INDEX `IX_FK_CategoryCategoryAttribute_CategoryAttribute` 
    ON `CategoryCategoryAttribute`
    (`CategoryAttributes_Id`);

-- Creating foreign key on `Brands_Id` in table 'BrandCategoryAttribute'

ALTER TABLE `BrandCategoryAttribute`
ADD CONSTRAINT `FK_BrandCategoryAttribute_Brand`
    FOREIGN KEY (`Brands_Id`)
    REFERENCES `Brand`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating foreign key on `CategoryAttributes_Id` in table 'BrandCategoryAttribute'

ALTER TABLE `BrandCategoryAttribute`
ADD CONSTRAINT `FK_BrandCategoryAttribute_CategoryAttribute`
    FOREIGN KEY (`CategoryAttributes_Id`)
    REFERENCES `CategoryAttribute`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandCategoryAttribute_CategoryAttribute'

CREATE INDEX `IX_FK_BrandCategoryAttribute_CategoryAttribute` 
    ON `BrandCategoryAttribute`
    (`CategoryAttributes_Id`);

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
