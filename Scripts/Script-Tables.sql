-- MySQL dump 10.13  Distrib 5.5.62, for Win64 (AMD64)
--
-- Host: digital-documento.cluster-c58ihsrye390.us-east-1.rds.amazonaws.com    Database: credito
-- ------------------------------------------------------
-- Server version	5.6.10

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Customer`
--

DROP TABLE IF EXISTS `Customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Customer` (
  `Id` varchar(32) NOT NULL,
  `CpfCnpj` varchar(14) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `BirthDate` datetime DEFAULT NULL,
  `Modificado` datetime NOT NULL,
  `StatusRow` char(1) NOT NULL,
  `IdUserInsert` int(11) NOT NULL,
  `IdUserUpdate` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Customer`
--

LOCK TABLES `Customer` WRITE;
/*!40000 ALTER TABLE `Customer` DISABLE KEYS */;
INSERT INTO `Customer` VALUES ('792dd04f97b342309931ec933830bea6','33825838870','Everton','2020-03-14 13:20:16','2020-03-14 10:20:45','U',0,-1),('d6b37520f09c45c8a4eeea290652209c','12345678901','Everton','2000-03-14 14:36:53','2020-03-14 11:37:27','U',0,-1);
/*!40000 ALTER TABLE `Customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `InterestRate`
--

DROP TABLE IF EXISTS `InterestRate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `InterestRate` (
  `Id` varchar(32) NOT NULL,
  `StartScore` int(11) NOT NULL,
  `EndScore` int(11) NOT NULL,
  `IdTerm` varchar(32) NOT NULL,
  `VlInterest` decimal(14,9) NOT NULL,
  `Modificado` datetime NOT NULL,
  `StatusRow` char(1) NOT NULL,
  `IdUserInsert` int(11) NOT NULL,
  `IdUserUpdate` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `InterestRate`
--

LOCK TABLES `InterestRate` WRITE;
/*!40000 ALTER TABLE `InterestRate` DISABLE KEYS */;
INSERT INTO `InterestRate` VALUES ('',600,699,'3b7c087db0fb4b97912e73c8db48c61b',6.400000000,'2020-03-13 15:44:26','I',-1,NULL),('',600,699,'c42576fb66944a18b78d136958f47f87',6.600000000,'2020-03-13 15:44:31','I',-1,NULL),('',600,699,'1494d6021fbf4681b40202c6054cc69e',6.900000000,'2020-03-13 15:44:33','I',-1,NULL),('',700,799,'3b7c087db0fb4b97912e73c8db48c61b',5.500000000,'2020-03-13 15:44:36','I',-1,NULL),('',700,799,'c42576fb66944a18b78d136958f47f87',5.800000000,'2020-03-13 15:44:39','I',-1,NULL),('',700,799,'1494d6021fbf4681b40202c6054cc69e',6.100000000,'2020-03-13 15:44:41','I',-1,NULL),('',800,899,'3b7c087db0fb4b97912e73c8db48c61b',4.700000000,'2020-03-13 15:44:45','I',-1,NULL),('',800,899,'c42576fb66944a18b78d136958f47f87',5.000000000,'2020-03-13 15:44:48','I',-1,NULL),('',800,899,'1494d6021fbf4681b40202c6054cc69e',5.300000000,'2020-03-13 15:44:54','I',-1,NULL),('',900,1000,'3b7c087db0fb4b97912e73c8db48c61b',3.900000000,'2020-03-13 15:44:57','I',-1,NULL),('',900,1000,'c42576fb66944a18b78d136958f47f87',4.200000000,'2020-03-13 15:45:01','I',-1,NULL),('',900,1000,'1494d6021fbf4681b40202c6054cc69e',4.500000000,'2020-03-13 15:45:04','I',-1,NULL);
/*!40000 ALTER TABLE `InterestRate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `LoanProcess`
--

DROP TABLE IF EXISTS `LoanProcess`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `LoanProcess` (
  `Id` varchar(32) NOT NULL,
  `IdLoanRequest` varchar(32) NOT NULL,
  `IdStatus` varchar(32) DEFAULT NULL,
  `Result` varchar(20) DEFAULT NULL,
  `RefusedPolicy` varchar(255) DEFAULT NULL,
  `VlAmount` decimal(14,9) NOT NULL,
  `IdTerms` varchar(32) NOT NULL,
  `Modificado` datetime NOT NULL,
  `StatusRow` char(1) NOT NULL,
  `IdUserInsert` int(11) NOT NULL,
  `IdUserUpdate` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `LoanProcess`
--

LOCK TABLES `LoanProcess` WRITE;
/*!40000 ALTER TABLE `LoanProcess` DISABLE KEYS */;
INSERT INTO `LoanProcess` VALUES ('d2c55adef17e440da7e879917b6a3ca9','d05628bcb9864846b1aabeda02bd4d95','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,100.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-12 11:20:04','I',-1,NULL),('2660c72659e84331984e4d4e37f204dd','a6cf10cfea944ee88774187330066bca','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,100.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-12 11:20:16','I',-1,NULL),('18e41b8cbc024a49be6e54201ac0b311','4bb684fd7ba14320970fd6e1aaacb180','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,100.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-12 11:40:03','I',-1,NULL),('20a14886f352464aaf699735470aef0b','250c71e9141c43408fae8775ae591479','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,100.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-12 11:40:56','I',-1,NULL),('88fad3b7851d44c7a4fe547ea15ae57b','92c8522a2fdd4199a41e9374d3144733','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,100.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-12 11:41:35','I',-1,NULL),('a7eae4a99646420598fa651378843ea4','ff4deaff01644cdcb7852685c580bce5','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,700.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-13 11:32:50','I',-1,NULL),('0486ff4c6e764f04a68d6aee860a786d','891565138199434f8ddda0973f1b34fc','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,800.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-14 10:20:49','I',-1,NULL),('8a858272006d41a297a036ba12d7e815','3e5a643eb12d48ba9c0d7a092ebbbf1b','3e63f0f13b8643c783d679f1081f854e',NULL,NULL,3000.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-14 10:55:34','I',-1,NULL),('40fdd44532894e31ac0fbd91fa2c80ab','18b3c39cba2341d38456438461edc514','87a53a3b12ac48afae005fc628923bf3','refused','commitment',25000.000000000,'3b7c087db0fb4b97912e73c8db48c61b','2020-03-14 11:48:37','U',0,-1);
/*!40000 ALTER TABLE `LoanProcess` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `LoanRequest`
--

DROP TABLE IF EXISTS `LoanRequest`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `LoanRequest` (
  `Id` varchar(32) NOT NULL,
  `IdCustomer` varchar(32) NOT NULL,
  `VlAmount` decimal(14,9) NOT NULL,
  `IdTerms` varchar(32) NOT NULL,
  `VlIncome` decimal(14,9) NOT NULL,
  `Modificado` datetime NOT NULL,
  `StatusRow` char(1) NOT NULL,
  `IdUserInsert` int(11) NOT NULL,
  `IdUserUpdate` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `LoanRequest`
--

LOCK TABLES `LoanRequest` WRITE;
/*!40000 ALTER TABLE `LoanRequest` DISABLE KEYS */;
INSERT INTO `LoanRequest` VALUES ('d05628bcb9864846b1aabeda02bd4d95','792dd04f97b342309931ec933830bea6',100.000000000,'3b7c087db0fb4b97912e73c8db48c61b',700.000000000,'2020-03-12 11:20:04','I',-1,NULL),('a6cf10cfea944ee88774187330066bca','792dd04f97b342309931ec933830bea6',100.000000000,'3b7c087db0fb4b97912e73c8db48c61b',700.000000000,'2020-03-12 11:20:15','I',-1,NULL),('4bb684fd7ba14320970fd6e1aaacb180','792dd04f97b342309931ec933830bea6',100.000000000,'3b7c087db0fb4b97912e73c8db48c61b',700.000000000,'2020-03-12 11:40:03','I',-1,NULL),('250c71e9141c43408fae8775ae591479','792dd04f97b342309931ec933830bea6',100.000000000,'3b7c087db0fb4b97912e73c8db48c61b',700.000000000,'2020-03-12 11:40:50','I',-1,NULL),('92c8522a2fdd4199a41e9374d3144733','792dd04f97b342309931ec933830bea6',100.000000000,'3b7c087db0fb4b97912e73c8db48c61b',700.000000000,'2020-03-12 11:41:32','I',-1,NULL),('ff4deaff01644cdcb7852685c580bce5','d6b37520f09c45c8a4eeea290652209c',700.000000000,'3b7c087db0fb4b97912e73c8db48c61b',1250.000000000,'2020-03-13 11:32:47','I',-1,NULL),('891565138199434f8ddda0973f1b34fc','792dd04f97b342309931ec933830bea6',800.000000000,'3b7c087db0fb4b97912e73c8db48c61b',1000.000000000,'2020-03-14 10:20:48','I',-1,NULL),('3e5a643eb12d48ba9c0d7a092ebbbf1b','d6b37520f09c45c8a4eeea290652209c',3000.000000000,'3b7c087db0fb4b97912e73c8db48c61b',3500.000000000,'2020-03-14 10:55:33','I',-1,NULL),('18b3c39cba2341d38456438461edc514','d6b37520f09c45c8a4eeea290652209c',25000.000000000,'3b7c087db0fb4b97912e73c8db48c61b',3500.000000000,'2020-03-14 11:37:35','I',-1,NULL);
/*!40000 ALTER TABLE `LoanRequest` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Status`
--

DROP TABLE IF EXISTS `Status`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Status` (
  `Id` varchar(32) DEFAULT NULL,
  `Description` varchar(20) NOT NULL,
  `Modificado` datetime NOT NULL,
  `StatusRow` char(1) NOT NULL,
  `IdUserInsert` int(11) NOT NULL,
  `IdUserUpdate` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Status`
--

LOCK TABLES `Status` WRITE;
/*!40000 ALTER TABLE `Status` DISABLE KEYS */;
INSERT INTO `Status` VALUES ('3e63f0f13b8643c783d679f1081f854e','Processing','2020-03-12 13:57:31','I',-1,NULL),('87a53a3b12ac48afae005fc628923bf3','Completed','2020-03-12 13:57:34','I',-1,NULL);
/*!40000 ALTER TABLE `Status` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Terms`
--

DROP TABLE IF EXISTS `Terms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Terms` (
  `Id` varchar(32) DEFAULT NULL,
  `Term` int(11) NOT NULL,
  `Modificado` datetime NOT NULL,
  `StatusRow` char(1) NOT NULL,
  `IdUserInsert` int(11) NOT NULL,
  `IdUserUpdate` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Terms`
--

LOCK TABLES `Terms` WRITE;
/*!40000 ALTER TABLE `Terms` DISABLE KEYS */;
INSERT INTO `Terms` VALUES ('3b7c087db0fb4b97912e73c8db48c61b',6,'2020-03-12 13:58:36','I',-1,NULL),('c42576fb66944a18b78d136958f47f87',9,'2020-03-12 13:58:39','I',-1,NULL),('1494d6021fbf4681b40202c6054cc69e',12,'2020-03-12 13:58:41','I',-1,NULL);
/*!40000 ALTER TABLE `Terms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'credito'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-03-14 12:50:35
