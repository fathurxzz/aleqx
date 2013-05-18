



-- -----------------------------------------------------------
-- Entity Designer DDL Script for MySQL Server 4.1 and higher
-- -----------------------------------------------------------
-- Date Created: 05/18/2013 09:30:18
-- Generated from EDMX file: D:\projects\Listelli2013\Listelli\Models\Site.edmx
-- Target version: 2.0.0.0
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------

--    ALTER TABLE `ContentLang` DROP CONSTRAINT `FK_ContentContentLang`;
--    ALTER TABLE `ContentLang` DROP CONSTRAINT `FK_LanguageContentLang`;
--    ALTER TABLE `BrandItem` DROP CONSTRAINT `FK_BrandBrandItem`;
--    ALTER TABLE `BrandItemImage` DROP CONSTRAINT `FK_BrandItemBrandItemImage`;
--    ALTER TABLE `BrandLang` DROP CONSTRAINT `FK_LanguageBrandLang`;
--    ALTER TABLE `BrandLang` DROP CONSTRAINT `FK_BrandBrandLang`;
--    ALTER TABLE `BrandItemLang` DROP CONSTRAINT `FK_LanguageBrandItemLang`;
--    ALTER TABLE `BrandItemLang` DROP CONSTRAINT `FK_BrandItemBrandItemLang`;
--    ALTER TABLE `CategoryLang` DROP CONSTRAINT `FK_CategoryCategoryLang`;
--    ALTER TABLE `CategoryLang` DROP CONSTRAINT `FK_LanguageCategoryLang`;
--    ALTER TABLE `CategoryBrand` DROP CONSTRAINT `FK_CategoryCategoryBrand`;
--    ALTER TABLE `CategoryBrandItem` DROP CONSTRAINT `FK_CategoryBrandCategoryBrandItem`;
--    ALTER TABLE `CategoryBrandItemLang` DROP CONSTRAINT `FK_CategoryBrandItemCategoryBrandItemLang`;
--    ALTER TABLE `CategoryBrandItemLang` DROP CONSTRAINT `FK_LanguageCategoryBrandItemLang`;

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------
SET foreign_key_checks = 0;
    DROP TABLE IF EXISTS `Language`;
    DROP TABLE IF EXISTS `Content`;
    DROP TABLE IF EXISTS `ContentLang`;
    DROP TABLE IF EXISTS `Brand`;
    DROP TABLE IF EXISTS `BrandItem`;
    DROP TABLE IF EXISTS `BrandItemImage`;
    DROP TABLE IF EXISTS `BrandLang`;
    DROP TABLE IF EXISTS `BrandItemLang`;
    DROP TABLE IF EXISTS `Category`;
    DROP TABLE IF EXISTS `CategoryLang`;
    DROP TABLE IF EXISTS `CategoryBrand`;
    DROP TABLE IF EXISTS `CategoryBrandItem`;
    DROP TABLE IF EXISTS `CategoryBrandItemLang`;
SET foreign_key_checks = 1;

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Language'

CREATE TABLE `Language` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Code` longtext  NOT NULL,
    `Name` longtext  NOT NULL
);

-- Creating table 'Content'

CREATE TABLE `Content` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `MainPage` bool  NOT NULL
);

-- Creating table 'ContentLang'

CREATE TABLE `ContentLang` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` longtext  NOT NULL,
    `Text` longtext  NOT NULL,
    `ContentId` int  NOT NULL,
    `LanguageId` int  NOT NULL,
    `SeoDescription` TEXT  NULL,
    `SeoKeywords` TEXT  NULL
);

-- Creating table 'Brand'

CREATE TABLE `Brand` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL,
    `ImageSource` varchar( 200 )  NULL
);

-- Creating table 'BrandItem'

CREATE TABLE `BrandItem` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ContentType` int  NOT NULL,
    `BrandId` int  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'BrandItemImage'

CREATE TABLE `BrandItemImage` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `ImageSource` varchar( 200 )  NOT NULL,
    `BrandItemId` int  NOT NULL
);

-- Creating table 'BrandLang'

CREATE TABLE `BrandLang` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Description` TEXT  NULL,
    `LanguageId` int  NOT NULL,
    `BrandId` int  NOT NULL
);

-- Creating table 'BrandItemLang'

CREATE TABLE `BrandItemLang` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Text` longtext  NOT NULL,
    `LanguageId` int  NOT NULL,
    `BrandItemId` int  NOT NULL
);

-- Creating table 'Category'

CREATE TABLE `Category` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `ImageSource` varchar( 200 )  NOT NULL,
    `SortOrder` int  NOT NULL
);

-- Creating table 'CategoryLang'

CREATE TABLE `CategoryLang` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `CategoryId` int  NOT NULL,
    `LanguageId` int  NOT NULL
);

-- Creating table 'CategoryBrand'

CREATE TABLE `CategoryBrand` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Name` varchar( 200 )  NOT NULL,
    `CategoryId` int  NOT NULL
);

-- Creating table 'CategoryBrandItem'

CREATE TABLE `CategoryBrandItem` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `CategoryBrandId` int  NOT NULL,
    `Content` longtext  NULL
);

-- Creating table 'CategoryBrandItemLang'

CREATE TABLE `CategoryBrandItemLang` (
    `Id` int AUTO_INCREMENT PRIMARY KEY NOT NULL,
    `Title` varchar( 200 )  NOT NULL,
    `Text` longtext  NULL,
    `CategoryBrandItemId` int  NOT NULL,
    `LanguageId` int  NOT NULL
);



-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------



-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on `ContentId` in table 'ContentLang'

ALTER TABLE `ContentLang`
ADD CONSTRAINT `FK_ContentContentLang`
    FOREIGN KEY (`ContentId`)
    REFERENCES `Content`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContentContentLang'

CREATE INDEX `IX_FK_ContentContentLang` 
    ON `ContentLang`
    (`ContentId`);

-- Creating foreign key on `LanguageId` in table 'ContentLang'

ALTER TABLE `ContentLang`
ADD CONSTRAINT `FK_LanguageContentLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageContentLang'

CREATE INDEX `IX_FK_LanguageContentLang` 
    ON `ContentLang`
    (`LanguageId`);

-- Creating foreign key on `BrandId` in table 'BrandItem'

ALTER TABLE `BrandItem`
ADD CONSTRAINT `FK_BrandBrandItem`
    FOREIGN KEY (`BrandId`)
    REFERENCES `Brand`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandBrandItem'

CREATE INDEX `IX_FK_BrandBrandItem` 
    ON `BrandItem`
    (`BrandId`);

-- Creating foreign key on `BrandItemId` in table 'BrandItemImage'

ALTER TABLE `BrandItemImage`
ADD CONSTRAINT `FK_BrandItemBrandItemImage`
    FOREIGN KEY (`BrandItemId`)
    REFERENCES `BrandItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandItemBrandItemImage'

CREATE INDEX `IX_FK_BrandItemBrandItemImage` 
    ON `BrandItemImage`
    (`BrandItemId`);

-- Creating foreign key on `LanguageId` in table 'BrandLang'

ALTER TABLE `BrandLang`
ADD CONSTRAINT `FK_LanguageBrandLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageBrandLang'

CREATE INDEX `IX_FK_LanguageBrandLang` 
    ON `BrandLang`
    (`LanguageId`);

-- Creating foreign key on `BrandId` in table 'BrandLang'

ALTER TABLE `BrandLang`
ADD CONSTRAINT `FK_BrandBrandLang`
    FOREIGN KEY (`BrandId`)
    REFERENCES `Brand`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandBrandLang'

CREATE INDEX `IX_FK_BrandBrandLang` 
    ON `BrandLang`
    (`BrandId`);

-- Creating foreign key on `LanguageId` in table 'BrandItemLang'

ALTER TABLE `BrandItemLang`
ADD CONSTRAINT `FK_LanguageBrandItemLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageBrandItemLang'

CREATE INDEX `IX_FK_LanguageBrandItemLang` 
    ON `BrandItemLang`
    (`LanguageId`);

-- Creating foreign key on `BrandItemId` in table 'BrandItemLang'

ALTER TABLE `BrandItemLang`
ADD CONSTRAINT `FK_BrandItemBrandItemLang`
    FOREIGN KEY (`BrandItemId`)
    REFERENCES `BrandItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BrandItemBrandItemLang'

CREATE INDEX `IX_FK_BrandItemBrandItemLang` 
    ON `BrandItemLang`
    (`BrandItemId`);

-- Creating foreign key on `CategoryId` in table 'CategoryLang'

ALTER TABLE `CategoryLang`
ADD CONSTRAINT `FK_CategoryCategoryLang`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategoryLang'

CREATE INDEX `IX_FK_CategoryCategoryLang` 
    ON `CategoryLang`
    (`CategoryId`);

-- Creating foreign key on `LanguageId` in table 'CategoryLang'

ALTER TABLE `CategoryLang`
ADD CONSTRAINT `FK_LanguageCategoryLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageCategoryLang'

CREATE INDEX `IX_FK_LanguageCategoryLang` 
    ON `CategoryLang`
    (`LanguageId`);

-- Creating foreign key on `CategoryId` in table 'CategoryBrand'

ALTER TABLE `CategoryBrand`
ADD CONSTRAINT `FK_CategoryCategoryBrand`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `Category`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategoryBrand'

CREATE INDEX `IX_FK_CategoryCategoryBrand` 
    ON `CategoryBrand`
    (`CategoryId`);

-- Creating foreign key on `CategoryBrandId` in table 'CategoryBrandItem'

ALTER TABLE `CategoryBrandItem`
ADD CONSTRAINT `FK_CategoryBrandCategoryBrandItem`
    FOREIGN KEY (`CategoryBrandId`)
    REFERENCES `CategoryBrand`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryBrandCategoryBrandItem'

CREATE INDEX `IX_FK_CategoryBrandCategoryBrandItem` 
    ON `CategoryBrandItem`
    (`CategoryBrandId`);

-- Creating foreign key on `CategoryBrandItemId` in table 'CategoryBrandItemLang'

ALTER TABLE `CategoryBrandItemLang`
ADD CONSTRAINT `FK_CategoryBrandItemCategoryBrandItemLang`
    FOREIGN KEY (`CategoryBrandItemId`)
    REFERENCES `CategoryBrandItem`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryBrandItemCategoryBrandItemLang'

CREATE INDEX `IX_FK_CategoryBrandItemCategoryBrandItemLang` 
    ON `CategoryBrandItemLang`
    (`CategoryBrandItemId`);

-- Creating foreign key on `LanguageId` in table 'CategoryBrandItemLang'

ALTER TABLE `CategoryBrandItemLang`
ADD CONSTRAINT `FK_LanguageCategoryBrandItemLang`
    FOREIGN KEY (`LanguageId`)
    REFERENCES `Language`
        (`Id`)
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageCategoryBrandItemLang'

CREATE INDEX `IX_FK_LanguageCategoryBrandItemLang` 
    ON `CategoryBrandItemLang`
    (`LanguageId`);

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
