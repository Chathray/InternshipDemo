-- MySQL dump 10.13  Distrib 8.0.23, for Win64 (x86_64)
--
-- Host: localhost    Database: tmainternship
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
  `DepartmentId` int NOT NULL AUTO_INCREMENT,
  `DepName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `DepLocation` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`DepartmentId`),
  UNIQUE KEY `Name_UNIQUE` (`DepName`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departments`
--

LOCK TABLES `departments` WRITE;
/*!40000 ALTER TABLE `departments` DISABLE KEYS */;
INSERT INTO `departments` VALUES (1,'TMA Innovation Park','Quy Nhon City','2021-04-01 01:37:49','2021-04-01 01:37:49'),(2,'LAB 5','Ho Chi Minh City','2021-04-01 01:37:49','2021-04-01 08:52:44'),(3,'LAB 6','Ho Chi Minh City','2021-04-01 01:37:49','2021-04-01 08:52:44'),(4,'LAB 12','Ho Chi Minh City','2021-04-01 01:37:49','2021-04-01 08:52:44');
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
  `Title` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ClassName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Start` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `End` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` int DEFAULT NULL,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `RepeatField` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `GestsField` json DEFAULT NULL,
  `EventLocationLabel` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `EventDescriptionLabel` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Image` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT '/img/event.svg',
  PRIMARY KEY (`EventId`),
  UNIQUE KEY `Title_UNIQUE` (`Title`),
  KEY `FK_Events_Type_idx` (`Type`),
  KEY `FK_Events_CreatedBy_idx` (`CreatedBy`),
  KEY `FK_Events_UpdatedBy_idx` (`UpdatedBy`),
  CONSTRAINT `FK_Events_CreatedBy` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`),
  CONSTRAINT `FK_Events_EventTypes_Type` FOREIGN KEY (`Type`) REFERENCES `eventtypes` (`Type`),
  CONSTRAINT `FK_Events_UpdatedBy` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
/*!40000 ALTER TABLE `events` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `eventtypes`
--

DROP TABLE IF EXISTS `eventtypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `eventtypes` (
  `Type` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ClassName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Color` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `eventtypes`
--

LOCK TABLES `eventtypes` WRITE;
/*!40000 ALTER TABLE `eventtypes` DISABLE KEYS */;
INSERT INTO `eventtypes` VALUES ('Holidays','fullcalendar-custom-event-holidays','warning','2021-04-01 02:36:30','2021-04-01 02:36:30'),('Personal','fullcalendar-custom-event-hs-team','primary','2021-04-01 02:36:30','2021-04-01 02:36:30'),('Reminders','fullcalendar-custom-event-reminders','danger','2021-04-01 02:36:30','2021-04-01 02:36:30'),('Tasks','fullcalendar-custom-event-tasks','success','2021-04-01 02:36:30','2021-04-01 02:36:30');
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
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Gender` enum('female','male') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `DateOfBirth` date NOT NULL,
  `Duration` varchar(23) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `TrainingId` int NOT NULL,
  `Type` enum('Full time','Part time') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Mentor` int DEFAULT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Avatar` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '/img/intern.svg',
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `OrganizationId` int DEFAULT NULL,
  `DepartmentId` int DEFAULT NULL,
  PRIMARY KEY (`InternId`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  KEY `FK_Events_Mentor_idx` (`Mentor`),
  KEY `FK_Interns_UpdatedBy_idx` (`UpdatedBy`),
  KEY `FK_Interns_DepartmentId_idx` (`DepartmentId`),
  KEY `FK_Interns_OrganizationId_idx` (`OrganizationId`),
  KEY `PK_Interns_TrainingId_idx` (`TrainingId`),
  CONSTRAINT `FK_Interns_Created` FOREIGN KEY (`Mentor`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Interns_Department` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`DepartmentId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Interns_Organization` FOREIGN KEY (`OrganizationId`) REFERENCES `organizations` (`OrganizationId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Interns_Updated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE,
  CONSTRAINT `PK_Intern_Training` FOREIGN KEY (`TrainingId`) REFERENCES `trainings` (`TrainingId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interns`
--

LOCK TABLES `interns` WRITE;
/*!40000 ALTER TABLE `interns` DISABLE KEYS */;
INSERT INTO `interns` VALUES (1,'ngotran@tma','Khai','Tran Quang','male','2020-02-02','2020-02-02 - 2020-02-02',0,'Full time',8,NULL,'2021-03-17 02:26:42','2021-03-30 06:30:09','/img/intern.svg','4343',1,1),(3,'ngotran@t1','Ly','Tran Quang','male','2020-02-02','2020-02-02 - 2020-02-02',0,'Full time',8,NULL,'2021-03-18 00:51:05','2021-04-01 08:53:20','/img/intern.svg','4343',2,1),(4,'ngotran@t2','Tam','Tran Quang','male','2020-02-02','2020-02-02 - 2020-02-02',0,'Full time',1,NULL,'2021-03-18 00:13:10','2021-04-01 08:53:20','/img/intern.svg','09545845',3,1),(6,'ngotran@t3','Tan','Tran Thien','male','2020-02-02','2020-02-02 - 2020-02-02',0,'Full time',1,NULL,'2021-03-18 01:05:50','2021-04-01 08:53:20','/img/intern.svg','09545845',1,3);
/*!40000 ALTER TABLE `interns` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `organizations`
--

DROP TABLE IF EXISTS `organizations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `organizations` (
  `OrganizationId` int NOT NULL AUTO_INCREMENT,
  `OrgName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `OrgAddress` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `OrgPhone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`OrganizationId`),
  UNIQUE KEY `Name_UNIQUE` (`OrgName`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `organizations`
--

LOCK TABLES `organizations` WRITE;
/*!40000 ALTER TABLE `organizations` DISABLE KEYS */;
INSERT INTO `organizations` VALUES (1,'Quy Nhon University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 16','2021-04-01 01:36:54','2021-04-01 01:36:54'),(2,'Nha Trang University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 156','2021-04-01 01:36:54','2021-04-01 08:50:39'),(3,'Tay Nguyen University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 156','2021-04-01 01:36:54','2021-04-01 08:50:39');
/*!40000 ALTER TABLE `organizations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `points`
--

DROP TABLE IF EXISTS `points`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `points` (
  `InternId` int NOT NULL,
  `Marker` int DEFAULT NULL,
  `TechnicalSkill` decimal(4,2) DEFAULT NULL,
  `SoftSkill` decimal(4,2) DEFAULT NULL,
  `Attitude` decimal(4,2) DEFAULT NULL,
  `Score` decimal(4,2) GENERATED ALWAYS AS ((((`TechnicalSkill` + `SoftSkill`) + `Attitude`) / 3)) STORED,
  `Passed` tinyint GENERATED ALWAYS AS ((`Score` >= 5)) STORED,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`InternId`),
  KEY `FK_marker_user_idx` (`Marker`),
  CONSTRAINT `FK_marker_user` FOREIGN KEY (`Marker`) REFERENCES `users` (`UserId`),
  CONSTRAINT `FK_points_Interns` FOREIGN KEY (`InternId`) REFERENCES `interns` (`InternId`) ON DELETE CASCADE,
  CONSTRAINT `CHK_Attit` CHECK (((`Attitude` >= 0) and (`Attitude` <= 10))),
  CONSTRAINT `CHK_Soft` CHECK (((`SoftSkill` >= 0) and (`SoftSkill` <= 10))),
  CONSTRAINT `CHK_Tech` CHECK (((`TechnicalSkill` >= 0) and (`TechnicalSkill` <= 10)))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `points`
--

LOCK TABLES `points` WRITE;
/*!40000 ALTER TABLE `points` DISABLE KEYS */;
INSERT INTO `points` (`InternId`, `Marker`, `TechnicalSkill`, `SoftSkill`, `Attitude`, `CreatedDate`, `UpdatedDate`) VALUES (1,9,9.60,7.00,7.00,'2021-04-01 09:36:26','2021-04-01 09:36:53'),(3,9,1.00,1.00,3.00,'2021-04-01 09:04:19','2021-04-01 09:04:19'),(4,9,10.00,7.00,7.00,'2021-04-01 07:21:23','2021-04-01 09:02:52');
/*!40000 ALTER TABLE `points` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questions` (
  `QuestionId` int NOT NULL AUTO_INCREMENT,
  `Group` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `InData` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `OutData` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`QuestionId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES (1,'Recruit','Cho em hỏi TMA phỏng vấn tiếng Anh hay tiếng Việt?','Bên mình sẽ phỏng vấn cả tiếng Anh và tiếng Việt nha bạn.','2021-04-01 02:36:57','2021-04-01 02:36:57'),(2,'Recruit','Anh/chị cho em hỏi nếu test tiếng Anh ở công ty thì đề thi như thế nào ạ? Bao lâu sẽ có kết quả?','Chào bạn, đề thi sẽ theo chuẩn đề TOEIC, thông thường kết quả sẽ được thông báo trong vòng 1 tuần, tùy thuộc vào mỗi dự án.','2021-04-01 02:36:57','2021-04-01 02:36:57'),(3,'Recruit','Em là SV năm cuối (đang làm luận văn), chuyên ngành: Điện tử - Viễn thông, trường: ĐH Bách Khoa Tp. HCM. Em có 2 câu hỏi: TMA có tuyển thực tập sinh không? Nếu có, yêu cầu cụ thể (GPA, Tiếng Anh,...) như thế nào?','TMA thường xuyên tuyển thực tập sinh, sắp tới bên chị sẽ nhận hồ sơ thực tập để chuẩn bị cho đợt thực tập kế tiếp vào tháng 9. Hồ sơ ứng tuyển bao gồm:<ul class=\'mt-3\'><li>CV Tiếng Anh;</li><li> Bảng điểm hoặc bằng Tiếng Anh(nếu có);</li><li> Hình 3x4;</li><li> Giấy giới thiệu thực tập</li></ul> ','2021-04-01 02:36:57','2021-04-01 02:36:57'),(4,'Internship','Ngoại ngữ có yêu cầu cao không Ad?','Nếu em đã có các bằng như (TOEIC, TOEFL, IELTS) tương đương TOEIC 450 trở lên thì không phải làm bài test em nhé.','2021-04-01 02:36:57','2021-04-01 02:36:57'),(5,'Internship','Test thực tập đầu vào như thế nào chị?','Bài test đầu vào gồm: IQ (25’) và tiếng Anh (nếu em chưa có bằng)','2021-04-01 02:36:57','2021-04-01 02:36:57'),(6,'Internship','Thời gian thực tập yêu cầu là bao nhiêu vậy Ad?','Thời gian thực tập kéo dài 3 tháng, 2.5 ngày/tuần em à.','2021-04-01 02:36:57','2021-04-01 02:36:57');
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
  `TraName` varchar(45) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TraData` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `CreatedBy` int DEFAULT NULL,
  PRIMARY KEY (`TrainingId`),
  KEY `FK_createdby_user_idx` (`CreatedBy`),
  CONSTRAINT `FK_createdby_user` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trainings`
--

LOCK TABLES `trainings` WRITE;
/*!40000 ALTER TABLE `trainings` DISABLE KEYS */;
INSERT INTO `trainings` VALUES (0,'None','','2021-03-30 06:29:34','2021-04-01 08:49:40',NULL);
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
  `Email` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FirstName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `PasswordHash` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Status` enum('success','danger','warning') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'success',
  `Role` enum('admin','mentor','staff') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT 'staff',
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Avatar` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '../img/user.jpg',
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin@x','Thang','Huynh','$2a$11$ZwUGQzP5M.gaE/FzHrbGDuNJrWhefvsoiTmyIDowKnhZuXRMBtux6','success','mentor','2021-03-15 03:39:36',NULL,'../img/user.jpg',NULL),(2,'tan@tma','Tân','Trần','$2a$11$Y6RWgY8CxI7zyGHvTqz16eCdcZPSERWFTHHtlQRWlwIWIAhoG4md6','success','admin','2021-03-16 20:43:28','2021-03-22 04:49:25','../img/user.jpg',NULL),(3,'thanh@qnu','Thanh','Tran Thien','$2a$11$kWeq0c.p4h5ASXdbdnuRweg8TDzumiS1sfkmb.IormcRxpBao7nsu','success','staff','2021-03-17 18:41:37','2021-03-22 05:37:27','../img/user.jpg',NULL),(8,'by@tma','By','Le Thi','$2a$11$QmPcqj0ast0KIogZxIvZiesOLfcg/bpOlpx34ZahIyIixMd/OmVTK','success','staff','2021-03-17 02:25:34',NULL,'../img/user.jpg',NULL),(9,'lany@d','Lan','Nguyen','$2a$11$x/a3sChFcORDLIH/lENZz.FloqsS/Hm61.sIqtTluqXQNBee2.1xC','success','staff','2021-04-01 06:12:41','2021-04-01 06:12:41','../img/user.jpg',NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'tmainternship'
--
/*!50003 DROP PROCEDURE IF EXISTS `CheckUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CheckUser`(
  inEmail nvarchar(100)
)
BEGIN
  SELECT * FROM users WHERE Email = inEmail;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `EvaluateIntern` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `EvaluateIntern`(id int, marker int, technicalPoint FLOAT(10,2), softPoint FLOAT(10,2), attitudePoint FLOAT(10,2))
BEGIN
	INSERT INTO points (InternId, Marker, TechnicalSkill, SoftSkill, Attitude)
    VALUES(id, marker, technicalPoint, softPoint, attitudePoint)
    ON DUPLICATE KEY UPDATE
    Marker = marker,
    TechnicalSkill = technicalPoint,
    SoftSkill = softPoint,
    Attitude = attitudePoint;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetEventsJoined` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetEventsJoined`()
BEGIN
  SELECT Title, EventId, JSON_EXTRACT(GestsField, '$**.iid') AS Joined FROM events;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetEventsJson` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetEventsJson`()
BEGIN
SELECT 
	JSON_ARRAYAGG(JSON_OBJECT(
			'id', EventId,
			'title', Title,
			'start', Start,
			'end', End,
			'className',ClassName,
			'eventDescriptionLabel', EventDescriptionLabel,
			'eventLocationLabel', EventLocationLabel,
			'repeatField', RepeatField,
			'allDay', RepeatField = 'everyday',
			'gestsField', GestsField,
            'image',Image            
		))
AS json FROM events;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternDetail` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternDetail`(id int)
BEGIN
SELECT 
	JSON_OBJECT(
			'internid',t1.InternId,
			'email',t1.Email,
			'fullname',CONCAT(t1.FirstName," ",t1.LastName),
			'gender',t1.Gender,
			'birth',t1.DateOfBirth,
			'phone',t1.Phone,
			'type',t1.Type,
			'duration',t1.Duration,
			'organization',OrgName,
			'department',DepName,
			'training',TraName,
			'mentor',CONCAT(t5.FirstName," ",t5.LastName),
            'joindate',t1.CreatedDate
		)
AS json
FROM interns t1
	JOIN organizations t2 ON t2.OrganizationId = t1.OrganizationId
	JOIN departments t3 ON t3.DepartmentId = t1.DepartmentId
	JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
	JOIN users t5 ON t5.UserId = t1.Mentor

WHERE t1.InternId = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternInfo` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternInfo`(id int)
BEGIN
SELECT 
	JSON_OBJECT(
			'internid', InternId,
			'email', Email,
			'lastname', LastName,
			'firstname', Firstname,
			'gender',Gender,
			'birth',DateOfBirth,
			'phone',Phone,
			'type',Type,
			'duration',Duration,
			'organizationid',OrganizationId,
			'departmentid',DepartmentId,
			'trainingid',TrainingId,
            'avatar',Avatar            
		)
AS json FROM interns
WHERE InternId = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternList` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternList`(offset_value int,
 limit_value int,
 orderby int,
 search_on int,
  search_string nvarchar(20))
BEGIN

SELECT SQL_CALC_FOUND_ROWS
	CONCAT(t1.FirstName,' ',t1.LastName) AS FullName,
    t1.InternId, t1.Email,
    DateOfBirth, Gender,
    t1.Phone,
    CONCAT(t5.FirstName,' ',t5.LastName) AS Mentor,
    t1.Avatar, t1.CreatedDate,
    Duration, Type, DepName, OrgName,
    t4.TraName, t4.TrainingId
FROM interns t1
	JOIN organizations t2 ON t2.OrganizationId = t1.OrganizationId
	JOIN departments t3 ON t3.DepartmentId = t1.DepartmentId
	JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
	JOIN users t5 ON t5.UserId = t1.Mentor
    
WHERE
CASE 
    WHEN search_on = 0 THEN 1
    WHEN search_on = 1 AND search_string like t1.FirstName THEN 1
    WHEN search_on = 2 AND search_string like t1.LastName THEN 1
    WHEN search_on = 3 AND search_string like t1.Email THEN 1
    WHEN search_on = 4 AND search_string like t1.Phone THEN 1
    WHEN search_on = 5 AND search_string like t1.InternId THEN 1
    ELSE 0
END = 1

ORDER BY
	CASE WHEN orderby=1 THEN FullName END,
	CASE WHEN orderby=2 THEN InternId END DESC,
	CASE WHEN orderby=3 THEN t1.CreatedDate END DESC,
	CASE WHEN orderby=4 THEN Mentor END,
	CASE WHEN orderby=5 THEN TraName END
LIMIT offset_value, limit_value;

SELECT FOUND_ROWS() AS ShowLeadsTotalRows;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetTrainingData` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetTrainingData`(id int)
BEGIN
  select json_objectagg(TraName,TraData) AS json from trainings where trainingId = id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertEvent` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertEvent`(
	inTitle varchar(500),
	inType varchar(500),
	inClassName varchar(500),
	inStart varchar(500),
	inEnd varchar(500),
	inCreatedBy varchar(500),
	inGestsField json,
	inRepeatField varchar(500),
	inEventLocationLabel varchar(500),
	inEventDescriptionLabel varchar(500))
BEGIN
	INSERT INTO events (Title,Type,ClassName,Start,End,CreatedBy,GestsField,RepeatField,EventLocationLabel,EventDescriptionLabel)
	VALUES (inTitle,inType,inClassName,inStart,inEnd,inCreatedBy,inGestsField,inRepeatField,inEventLocationLabel,inEventDescriptionLabel);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertIntern` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertIntern`(
	inEmail varchar(500),
	inPhone varchar(500),
	inFirstName varchar(500),
	inLastName varchar(500),
	inDateOfBirth varchar(500),
	inGender varchar(500),
	inDuration varchar(500),
	inType varchar(500),
	inMentor varchar(500),
	inTrainingId varchar(500),
	inOrganizationId varchar(500),
	inDepartmentId varchar(500))
BEGIN
	INSERT INTO interns (Email,Phone,FirstName,LastName,DateOfBirth,Gender,Duration,Type,Mentor,TrainingId,OrganizationId,DepartmentId)
	VALUES (inEmail,inPhone,inFirstName,inLastName,inDateOfBirth,inGender,inDuration,inType,inMentor,inTrainingId,inOrganizationId,inDepartmentId);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertTraining` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertTraining`(
	inTraName varchar(500),
	inTraData text)
BEGIN
	INSERT INTO trainings (TraName, TraData)
    VALUES (inTraName, inTraData);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `InsertUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `InsertUser`(
	inEmail varchar(500),
	inFirstName varchar(500),
	inLastName varchar(500),
	inPasswordHash varchar(500))
BEGIN
	INSERT INTO users (Email, FirstName, LastName, PasswordHash)
	VALUES (inEmail, inFirstName, inLastName, inPasswordHash);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateIntern` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateIntern`(
	inInternId int,
	inEmail varchar(500),
	inPhone varchar(500),
	inFirstName varchar(500),
	inLastName varchar(500),
	inDateOfBirth varchar(500),
	inGender varchar(500),
	inDuration varchar(500),
	inType varchar(500),
	inMentor varchar(500),
	inTrainingId varchar(500),
	inOrganizationId varchar(500),
	inDepartmentId varchar(500))
BEGIN
	UPDATE interns
    SET
		Email=inEmail,
		Phone=inPhone,
		FirstName=inFirstName,
		LastName=inLastName,
		DateOfBirth=inDateOfBirth,
		Gender=inGender,
		Duration=inDuration,
		Type=inType,
		Mentor=inMentor,
		TrainingId=inTrainingId,
		OrganizationId=inOrganizationId,
		DepartmentId=inDepartmentId        
	WHERE Internid = inInternId;
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

-- Dump completed on 2021-04-01 17:37:07
