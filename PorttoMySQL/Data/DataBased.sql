-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: demoproduct
-- ------------------------------------------------------
-- Server version	8.0.23

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
-- Table structure for table `departments`
--

DROP TABLE IF EXISTS `departments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `departments` (
  `DepartmentId` int NOT NULL,
  `DepName` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `DepLocation` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`DepartmentId`),
  UNIQUE KEY `Name_UNIQUE` (`DepName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departments`
--

LOCK TABLES `departments` WRITE;
/*!40000 ALTER TABLE `departments` DISABLE KEYS */;
INSERT INTO `departments` VALUES (1,'TIP','Quy Nhon'),(2,'LAB6','HCM'),(3,'LAB8','HCM');
/*!40000 ALTER TABLE `departments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `events`
--

DROP TABLE IF EXISTS `events`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `events` (
  `EventId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Type` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `ClassName` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `Start` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `End` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` int DEFAULT NULL,
  `UpdatedDate` timestamp NULL DEFAULT NULL,
  `RepeatField` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `GestsField` json DEFAULT NULL,
  `EventLocationLabel` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `EventDescriptionLabel` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Image` varchar(200) COLLATE utf8mb4_general_ci DEFAULT '../img/event.png',
  PRIMARY KEY (`EventId`),
  UNIQUE KEY `Title_UNIQUE` (`Title`),
  KEY `IX_Events_Type` (`Type`),
  KEY `FK_Events_Created_idx` (`CreatedBy`,`UpdatedBy`),
  KEY `FK_Events_Updated_idx` (`UpdatedBy`),
  KEY `fgfgf_idx` (`UpdatedDate`),
  KEY `dfdfg_idx` (`CreatedDate`),
  CONSTRAINT `FK_Events_Created` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`),
  CONSTRAINT `FK_Events_EventTypes_Type` FOREIGN KEY (`Type`) REFERENCES `eventtypes` (`Type`) ON DELETE CASCADE,
  CONSTRAINT `FK_Events_Updated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
INSERT INTO `events` VALUES (4,'1234','Holidays','fullcalendar-custom-event-holidays','2021-03-18','2021-03-19',1,'2021-03-18 09:27:11',NULL,NULL,'everyday','[{\"iid\": 9, \"src\": \"../img/intern.png\", \"value\": \"A B\"}, {\"iid\": \"90\", \"src\": \"../img/intern.png\", \"value\": \"A B\"}]','','','../img/event.png');
/*!40000 ALTER TABLE `events` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `eventtypes`
--

DROP TABLE IF EXISTS `eventtypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `eventtypes` (
  `Type` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `ClassName` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Color` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventtypes`
--

LOCK TABLES `eventtypes` WRITE;
/*!40000 ALTER TABLE `eventtypes` DISABLE KEYS */;
INSERT INTO `eventtypes` VALUES ('Holidays','fullcalendar-custom-event-holidays','warning'),('Personal','fullcalendar-custom-event-hs-team','primary'),('Reminders','fullcalendar-custom-event-reminders','danger'),('Tasks','fullcalendar-custom-event-tasks','dark');
/*!40000 ALTER TABLE `eventtypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interns`
--

DROP TABLE IF EXISTS `interns`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `interns` (
  `InternId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `FirstName` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `Gender` enum('female','male') COLLATE utf8mb4_general_ci NOT NULL,
  `DateOfBirth` date NOT NULL,
  `Duration` varchar(23) COLLATE utf8mb4_general_ci NOT NULL,
  `TrainingId` int NOT NULL,
  `Type` enum('Full time','Part time') COLLATE utf8mb4_general_ci NOT NULL,
  `Mentor` int NOT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT NULL,
  `Avatar` varchar(100) COLLATE utf8mb4_general_ci NOT NULL DEFAULT '../img/intern.png',
  `Phone` varchar(15) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Organization` int NOT NULL,
  `Department` int NOT NULL,
  PRIMARY KEY (`InternId`),
  KEY `FK_Events_Created_idx` (`Mentor`),
  KEY `FK_Interns_Updated_idx` (`UpdatedBy`),
  KEY `FK_Interns_Department_idx` (`Department`),
  KEY `FK_Interns_Organization_idx` (`Organization`),
  KEY `FK_Interns_Training_idx` (`TrainingId`),
  CONSTRAINT `FK_Interns_Created` FOREIGN KEY (`Mentor`) REFERENCES `users` (`UserId`),
  CONSTRAINT `FK_Interns_Department` FOREIGN KEY (`Department`) REFERENCES `departments` (`DepartmentId`),
  CONSTRAINT `FK_Interns_Organization` FOREIGN KEY (`Organization`) REFERENCES `organizations` (`OrganizationId`),
  CONSTRAINT `FK_Interns_Updated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=4552 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interns`
--

LOCK TABLES `interns` WRITE;
/*!40000 ALTER TABLE `interns` DISABLE KEYS */;
INSERT INTO `interns` VALUES (9,'a@cc','A','B','male','2021-03-04','20/21/2102 - 10/21/0202',1,'Full time',1,NULL,'2021-03-17 09:26:42',NULL,'../img/intern.png','+12 212 121 212',1,1),(10,'a@cc','A','B','male','2021-03-04','20/21/2102 - 10/21/0202',1,'Full time',1,NULL,'2021-03-17 09:26:42',NULL,'../img/intern.png','+12 212 121 212',1,1),(11,'a@cc','A','B','male','2021-03-04','20/21/2102 - 10/21/0202',2,'Full time',1,NULL,'2021-03-17 09:26:42',NULL,'../img/intern.png','+12 212 121 212',1,1),(44,'a@cc34','Anh','Xtanh','female','2021-03-02','20/21/2102 - 10/21/0202',1,'Full time',1,NULL,'2021-03-18 07:41:01',NULL,'../img/intern.png','+12 212 121 212',1,2),(45,'a@ccaa','as','Xtanh','female','2021-03-31','20/21/2102 - 10/21/0202',3,'Full time',1,NULL,'2021-03-18 07:51:05',NULL,'../img/intern.png','+12 212 121 212',2,2),(1244,'a@ccaa','A','As','female','2021-03-25','20/21/2102 - 10/21/0202',1,'Part time',1,NULL,'2021-03-18 07:13:10',NULL,'../img/intern.png','+12 212 121 212',1,2),(4547,'a@cc','Anh','As','male','2021-03-02','20/21/2102 - 10/21/0202',1,'Full time',1,NULL,'2021-03-18 07:59:57',NULL,'../img/intern.png','+12 212 121 212',1,1),(4548,'a@c','A','Xtanh','female','2021-03-12','20/21/2102 - 10/21/0202',3,'Full time',1,NULL,'2021-03-18 08:05:50',NULL,'../img/intern.png','+12 212 121 212',1,1),(4549,'a@cc','Anh','đ','female','2021-03-09','20/21/2102 - 10/21/0202',1,'Full time',1,NULL,'2021-03-18 08:08:51',NULL,'../img/intern.png','+12 212 121 212',1,1),(4550,'mamdmadh@mfdsafsa.com','Thạch','f','female','2021-03-03','66/78/8899 - 90/99/0909',1,'Part time',1,NULL,'2021-03-18 08:15:44',NULL,'../img/intern.png','+84 333 33',1,2),(4551,'z@z','Thạc ','Pro','female','2021-03-10','66/78/8899 - 90/99/0909',1,'Full time',1,NULL,'2021-03-18 08:36:19',NULL,'../img/intern.png','+84 333 33',1,1);
/*!40000 ALTER TABLE `interns` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `internshippoints`
--

DROP TABLE IF EXISTS `internshippoints`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `internshippoints` (
  `InternId` int NOT NULL,
  `TechnicalSkill` decimal(2,2) DEFAULT NULL,
  `SoftSkill` decimal(2,2) DEFAULT NULL,
  `Attitude` decimal(2,2) DEFAULT NULL,
  `Result` decimal(2,2) DEFAULT NULL,
  PRIMARY KEY (`InternId`),
  CONSTRAINT `FK_internshippoints_Intern` FOREIGN KEY (`InternId`) REFERENCES `interns` (`InternId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `internshippoints`
--

LOCK TABLES `internshippoints` WRITE;
/*!40000 ALTER TABLE `internshippoints` DISABLE KEYS */;
/*!40000 ALTER TABLE `internshippoints` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizations`
--

DROP TABLE IF EXISTS `organizations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizations` (
  `OrganizationId` int NOT NULL,
  `OrgName` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `OrgAddress` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `OrgPhone` varchar(15) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`OrganizationId`),
  UNIQUE KEY `Name_UNIQUE` (`OrgName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizations`
--

LOCK TABLES `organizations` WRITE;
/*!40000 ALTER TABLE `organizations` DISABLE KEYS */;
INSERT INTO `organizations` VALUES (1,'Quy Nhon University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 156'),(2,'Phu Yen University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 156');
/*!40000 ALTER TABLE `organizations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questions` (
  `QuestionId` int NOT NULL AUTO_INCREMENT,
  `Group` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `InData` varchar(2000) COLLATE utf8mb4_general_ci NOT NULL,
  `OutData` varchar(5000) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`QuestionId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES (1,'Recruit','Cho em hỏi TMA phỏng vấn tiếng Anh hay tiếng Việt?','Bên mình sẽ phỏng vấn cả tiếng Anh và tiếng Việt nha bạn.'),(2,'Recruit','Anh/chị cho em hỏi nếu test tiếng Anh ở công ty thì đề thi như thế nào ạ? Bao lâu sẽ có kết quả?','Chào bạn, đề thi sẽ theo chuẩn đề TOEIC, thông thường kết quả sẽ được thông báo trong vòng 1 tuần, tùy thuộc vào mỗi dự án.'),(3,'Recruit','Em là SV năm cuối (đang làm luận văn), chuyên ngành: Điện tử - Viễn thông, trường: ĐH Bách Khoa Tp. HCM. Em có 2 câu hỏi: TMA có tuyển thực tập sinh không? Nếu có, yêu cầu cụ thể (GPA, Tiếng Anh,...) như thế nào?','TMA thường xuyên tuyển thực tập sinh, sắp tới bên chị sẽ nhận hồ sơ thực tập để chuẩn bị cho đợt thực tập kế tiếp vào tháng 9. Hồ sơ ứng tuyển bao gồm:<ul class=\'mt-3\'><li>CV Tiếng Anh;</li><li> Bảng điểm hoặc bằng Tiếng Anh(nếu có);</li><li> Hình 3x4;</li><li> Giấy giới thiệu thực tập</li></ul> '),(4,'Internship','Ngoại ngữ có yêu cầu cao không Ad?','Nếu em đã có các bằng như (TOEIC, TOEFL, IELTS) tương đương TOEIC 450 trở lên thì không phải làm bài test em nhé.'),(5,'Internship','Test thực tập đầu vào như thế nào chị?','Bài test đầu vào gồm: IQ (25’) và tiếng Anh (nếu em chưa có bằng)'),(6,'Internship','Thời gian thực tập yêu cầu là bao nhiêu vậy Ad?','Thời gian thực tập kéo dài 3 tháng, 2.5 ngày/tuần em à.');
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `trainings`
--

DROP TABLE IF EXISTS `trainings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `trainings` (
  `TrainingId` int NOT NULL AUTO_INCREMENT,
  `TraName` varchar(45) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `TraData` json DEFAULT NULL,
  PRIMARY KEY (`TrainingId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trainings`
--

LOCK TABLES `trainings` WRITE;
/*!40000 ALTER TABLE `trainings` DISABLE KEYS */;
INSERT INTO `trainings` VALUES (1,'C#','[{\"Content\": \"Basic GIT usage\", \"Duration\": \"8\", \"Training Outline\": \"GIT\"}, {\"Content\": \"C# Syntax, Data Type, Operators and Statements\", \"Duration\": \"8\", \"Training Outline\": \".NET basic\"}]'),(2,'Java','[{\"frame\": \"Java Spring\"}]'),(3,'Php','[{\"frame\": \"Laravel\"}]');
/*!40000 ALTER TABLE `trainings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `FirstName` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(50) COLLATE utf8mb4_general_ci NOT NULL,
  `PasswordHash` varchar(100) COLLATE utf8mb4_general_ci NOT NULL,
  `Status` enum('success','danger','warning') COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'success',
  `Role` enum('admin','mentor','staff') COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'staff',
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT NULL,
  `Avatar` varchar(100) COLLATE utf8mb4_general_ci DEFAULT '../img/user.jpg',
  `Phone` varchar(15) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin@x','Nguyen','Thach','$2a$11$ZwUGQzP5M.gaE/FzHrbGDuNJrWhefvsoiTmyIDowKnhZuXRMBtux6','success','staff','2021-03-15 10:39:36',NULL,'../img/user.jpg',NULL),(6,'tan@x','Tân','Trần','$2a$11$Y6RWgY8CxI7zyGHvTqz16eCdcZPSERWFTHHtlQRWlwIWIAhoG4md6','success','staff','2021-03-17 03:43:28',NULL,'../img/user.jpg',NULL),(8,'Tân','Trần','tan@xc','$2a$11$QmPcqj0ast0KIogZxIvZiesOLfcg/bpOlpx34ZahIyIixMd/OmVTK','success','staff','2021-03-17 09:25:34',NULL,'../img/user.jpg',NULL),(9,'qw','qw','c@s','$2a$11$kWeq0c.p4h5ASXdbdnuRweg8TDzumiS1sfkmb.IormcRxpBao7nsu','success','staff','2021-03-18 01:41:37',NULL,'../img/user.jpg',NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'demoproduct'
--
/*!50003 DROP PROCEDURE IF EXISTS `CheckUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CheckUser`(
	email varchar(500)
)
BEGIN
	select * from users	where Email = email;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateEvent` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateEvent`(
	title varchar(500),
	type varchar(500),
	classname varchar(500),
	start varchar(500),
	end varchar(500),
	createdby varchar(500),
	gestsfield varchar(500),
	repeatfield varchar(500),
	eventlocationlabel varchar(500),
	eventdescriptionlabel varchar(500)
)
BEGIN
	insert into events (Title,Type,ClassName,Start,End,CreatedBy,GestsField,RepeatField,EventLocationLabel,EventDescriptionLabel)
	values (title,type,classname,start,end,createdby,gestsfield,repeatfield,eventlocationlabel,eventdescriptionlabel);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateIntern` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateIntern`(
	email varchar(500),
	phone varchar(500),
	firstname varchar(500),
	lastname varchar(500),
	dateofbirth varchar(500),
	gender varchar(500),
	duration varchar(500),
	type varchar(500),
	mentor varchar(500),
	trainingid varchar(500),
	organization varchar(500),
	department varchar(500)
)
BEGIN
	insert into interns (Email,Phone,FirstName,LastName,DateOfBirth,Gender,Duration,Type,Mentor,TrainingId,Organization,Department)
	values (email,phone,firstname,lastname,dateofbirth,gender,duration,type,mentor,trainingid,organization,department);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CreateUser`(
 email varchar(500),
 firstName varchar(500),
 lastName varchar(500),
 passwordHash varchar(500))
BEGIN
	insert into users (Email, FirstName, LastName, PasswordHash)
	values (email,firstName,lastName,passwordHash);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetEventJoined` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetEventJoined`(id varchar(20))
BEGIN
	SELECT Title,GestsField  FROM events  WHERE GestsField->"$[0]" = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetFullIntern` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetFullIntern`(offse int, limi int)
BEGIN

SELECT CONCAT(t1.FirstName,' ',t1.LastName) AS FullName,
			  t1.InternId,t1.Email,DateOfBirth,Gender,t1.Phone,
	          t5.FirstName,t5.LastName,t1.CreatedDate,Duration,
              Type,DepName,OrgName,t4.TraName,t4.TrainingId
              
FROM interns t1
JOIN organizations t2 ON t2.OrganizationId = t1.Organization
JOIN departments t3 ON t3.DepartmentId = t1.Department
JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
JOIN users t5 ON t5.UserId = t1.Mentor

ORDER BY FullName
LIMIT limi OFFSET offse;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTrainData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTrainData`(id int)
BEGIN
	select TraData from trainings where trainingId = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-03-19  9:07:00
