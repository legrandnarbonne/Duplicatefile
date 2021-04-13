-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: duplicatefile
-- ------------------------------------------------------
-- Server version	8.0.17

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `analyse`
--

DROP TABLE IF EXISTS `analyse`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `analyse` (
  `Fichier` varchar(300) NOT NULL,
  `Hash` varchar(45) DEFAULT NULL,
  `Modified` datetime DEFAULT NULL,
  `isDuplicate` tinyint(1) DEFAULT '0',
  `Taille` bigint(8) DEFAULT NULL,
  `Erreur` varchar(45) DEFAULT NULL,
  `analysispath` int(11) DEFAULT NULL,
  `Status` tinyint(8) DEFAULT NULL,
  `lastAccess` datetime DEFAULT NULL,
  `user` varchar(45) DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `created` datetime DEFAULT NULL,
  `isOriginal` tinyint(1) DEFAULT '0',
  `Chemin` int(11) DEFAULT NULL,
  `adr` int(11) DEFAULT NULL,
  `tel` int(11) DEFAULT NULL,
  `prenom` int(11) DEFAULT NULL,
  `mail` int(11) DEFAULT NULL,
  `q` int(11) DEFAULT NULL COMMENT '-1 if not needed',
  `debug` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fichier_key` (`Fichier`),
  KEY `status_key` (`Status`),
  KEY `hash_key` (`Hash`),
  KEY `isduplicate_key` (`isDuplicate`) /*!80000 INVISIBLE */,
  KEY `isoriginal_key` (`isOriginal`)
) ENGINE=InnoDB AUTO_INCREMENT=5135722 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `stats`
--

DROP TABLE IF EXISTS `stats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stats` (
  `idStats` int(11) NOT NULL AUTO_INCREMENT,
  `Duration` bigint(20) DEFAULT NULL,
  `Rate` decimal(2,0) DEFAULT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `CalculatedHash` int(11) DEFAULT NULL,
  `DeletedFiles` int(11) DEFAULT NULL,
  `NewFiles` int(11) DEFAULT NULL,
  `NumberOfDuplicateFile` int(11) DEFAULT NULL,
  `DuplicateFileSize` bigint(20) DEFAULT NULL,
  `NumberOfDir` int(11) DEFAULT NULL,
  `NumberOfFile` int(11) DEFAULT NULL,
  `JobId` varchar(45) DEFAULT '',
  PRIMARY KEY (`idStats`)
) ENGINE=InnoDB AUTO_INCREMENT=928 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `iduser` int(11) NOT NULL AUTO_INCREMENT,
  `login` varchar(45) NOT NULL,
  `email` varchar(45) DEFAULT NULL,
  `title` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`iduser`,`login`)
) ENGINE=InnoDB AUTO_INCREMENT=299 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping events for database 'duplicatefile'
--

--
-- Dumping routines for database 'duplicatefile'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-04-13 14:12:35
