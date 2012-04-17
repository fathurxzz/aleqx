-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               5.1.46-community - MySQL Community Server (GPL)
-- Server OS:                    Win32
-- HeidiSQL version:             7.0.0.4053
-- Date/time:                    2012-04-17 17:01:06
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET FOREIGN_KEY_CHECKS=0 */;
-- Dumping data for table posh.category: ~6 rows (approximately)
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
INSERT INTO `category` (`Id`, `Title`, `SortOrder`, `ImageSource`) VALUES
	(1, 'Отель', 0, 'category-bg1111111.gif'),
	(2, 'Ресторан/ </br>Кафе/ </br>Бар', 1, 'category-bg11111111.gif'),
	(3, 'Фудкорт', 3, 'category-bg111111111.gif'),
	(4, 'Фитнес-</br>клуб', 3, 'category-bg1111111111.gif'),
	(5, 'Spa-</br>салон', 4, 'category-bg11111111111.gif'),
	(6, 'мой дом', 5, 'category-bg111111111111.gif');
/*!40000 ALTER TABLE `category` ENABLE KEYS */;

-- Dumping data for table posh.content: ~7 rows (approximately)
/*!40000 ALTER TABLE `content` DISABLE KEYS */;
INSERT INTO `content` (`Id`, `Title`, `Text`, `SeoTitle`, `SeoText`, `SeoDescription`, `SeoKeywords`, `SortOrder`, `Static`, `MainPage`, `Name`, `PageTitle`) VALUES
	(1, 'main', 'main', 'main', NULL, 'main', 'main', 0, 0, 1, 'main', 'main'),
	(2, 'Каталог продукции', '<p>\r\n catalogue</p>\r\n', 'catalogue', '', 'catalogue', 'catalogue', 1, 1, 0, 'catalogue', 'Каталог продукции'),
	(3, 'Наши услуги', '', NULL, '', NULL, NULL, 2, 0, 0, 'service', 'Наши услуги'),
	(4, 'Новости', '', NULL, '', NULL, NULL, 3, 1, 0, 'news', 'Новости'),
	(5, 'Готовые проекты', '', NULL, '', NULL, NULL, 4, 1, 0, 'projects', 'Готовые проекты'),
	(6, 'Статьи', '', NULL, '', NULL, NULL, 5, 1, 0, 'articles', 'Статьи'),
	(7, 'Обратная связь', '', NULL, '', NULL, NULL, 6, 0, 0, 'contacts', 'Обратная связь');
/*!40000 ALTER TABLE `content` ENABLE KEYS */;

-- Dumping data for table posh.element: ~7 rows (approximately)
/*!40000 ALTER TABLE `element` DISABLE KEYS */;
INSERT INTO `element` (`Id`, `Title`, `SortOrder`) VALUES
	(1, 'Объекты для летней площадки и террассы', 0),
	(2, 'Объекты для помещения', 1),
	(3, 'Стулья и столы', 2),
	(4, 'Диваны и кресла', 3),
	(5, 'Лежаки', 4),
	(6, 'Аксессуары', 5),
	(7, 'Цветные плетения и ткани', 6);
/*!40000 ALTER TABLE `element` ENABLE KEYS */;
/*!40014 SET FOREIGN_KEY_CHECKS=1 */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
