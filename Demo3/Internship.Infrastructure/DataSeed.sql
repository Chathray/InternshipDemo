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
  `SharedTrainings` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`DepartmentId`),
  UNIQUE KEY `Name_UNIQUE` (`DepName`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departments`
--

LOCK TABLES `departments` WRITE;
/*!40000 ALTER TABLE `departments` DISABLE KEYS */;
INSERT INTO `departments` VALUES (1,'DC8-Ext','TMA Innovation Park','2021-04-01 01:37:49','2021-04-08 07:16:53','8,9'),(2,'DC13-Ext','TMA Innovation Park','2021-04-01 01:37:49','2021-04-08 07:16:31','9,8'),(3,'DC8','TMA Innovation Park','2021-04-01 01:37:49','2021-04-08 07:11:38','8,9'),(4,'DC1','LAB 6','2021-04-01 01:37:49','2021-04-08 07:16:31','9,8');
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
  `End` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` int DEFAULT NULL,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `RepeatField` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `GestsField` json DEFAULT NULL,
  `EventLocationLabel` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `EventDescriptionLabel` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `Image` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '/img/event.svg',
  PRIMARY KEY (`EventId`),
  UNIQUE KEY `Title_UNIQUE` (`Title`),
  KEY `FK_Events_Type_idx` (`Type`),
  KEY `FK_Events_CreatedBy_idx` (`CreatedBy`),
  KEY `FK_Events_UpdatedBy_idx` (`UpdatedBy`),
  CONSTRAINT `FK_Events_CreatedBy` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_Events_EventTypes_Type` FOREIGN KEY (`Type`) REFERENCES `eventtypes` (`Type`),
  CONSTRAINT `FK_Events_UpdatedBy` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `events`
--

LOCK TABLES `events` WRITE;
/*!40000 ALTER TABLE `events` DISABLE KEYS */;
INSERT INTO `events` VALUES (1,'TOEIC !','Reminders','fullcalendar-custom-event-reminders','2021-04-09','2021-04-10',1,'2021-04-09 05:57:01',NULL,'2021-04-09 05:57:01','everyday','[{\"iid\": 1, \"src\": \"/img/avatar/intern.jpg\", \"value\": \"Phap Duong Dieu\"}, {\"iid\": 6, \"src\": \"/img/avatar/intern.svg\", \"value\": \"Tan Tran Thien\"}]','Quy Nhon','Joy',NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=208 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interns`
--

LOCK TABLES `interns` WRITE;
/*!40000 ALTER TABLE `interns` DISABLE KEYS */;
INSERT INTO `interns` VALUES (1,'ngotran@tma','Phap','Duong Dieu','male','2021-04-01','2020-02-02 - 2020-02-02',0,'Full time',1,NULL,'2021-03-17 02:26:42','2021-04-09 09:41:57','ngotran@tma.jpg','4343',1,1),(3,'ngotran@t1','Ly','Tran Quang','male','2020-02-02','2021-05-02 - 2020-02-02',0,'Full time',1,NULL,'2021-03-18 00:51:05','2021-04-09 09:32:02','img5.jpg','4343',2,1),(4,'ngotran@t2','Tam','Tran Quang','male','2020-02-02','2020-05-16 - 2020-02-02',0,'Full time',1,NULL,'2021-03-18 00:13:10','2021-04-09 09:42:22','ngotran@t2.jpg','09545845',3,4),(6,'ngotran@t3','Tan','Tran Thien','male','2020-02-02','2020-02-22 - 2020-02-02',0,'Full time',1,NULL,'2021-03-18 01:05:50','2021-04-09 07:44:04','intern.jpg','09545845',1,3),(207,'s@3','Linh','Lam','female','2021-04-10','2021-04-09 - 2021-04-10',8,'Full time',1,NULL,'2021-04-09 08:52:23','2021-04-09 09:42:51','s@3.jpg','+43 43',2,1);
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
INSERT INTO `organizations` VALUES (1,'Quy Nhon University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 16','2021-04-01 01:36:54','2021-04-02 08:37:26'),(2,'Nha Trang University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Nha Trang','0256 3846 156','2021-04-01 01:36:54','2021-04-02 08:37:37'),(3,'Tay Nguyen University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Tây Nguyên','0256 3846 156','2021-04-01 01:36:54','2021-04-02 08:37:49');
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
  CONSTRAINT `FK_marker_user` FOREIGN KEY (`Marker`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE,
  CONSTRAINT `FK_points_Interns` FOREIGN KEY (`InternId`) REFERENCES `interns` (`InternId`) ON DELETE RESTRICT ON UPDATE CASCADE,
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
  UNIQUE KEY `TraName_UNIQUE` (`TraName`),
  KEY `FK_createdby_user_idx` (`CreatedBy`),
  CONSTRAINT `FK_createdby_user` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `trainings`
--

LOCK TABLES `trainings` WRITE;
/*!40000 ALTER TABLE `trainings` DISABLE KEYS */;
INSERT INTO `trainings` VALUES (0,'None','{\"ops\":[{\"insert\":\"Nothing here\\n\"}]}','2021-03-30 06:29:34','2021-04-05 05:48:01',1),(8,'ASP.NET Core MVC','{\"ops\":[{\"insert\":\"Basic\"},{\"attributes\":{\"list\":\"ordered\"},\"insert\":\"\\n\"},{\"insert\":\"Inter\"},{\"attributes\":{\"list\":\"ordered\"},\"insert\":\"\\n\"},{\"insert\":\"Advanced\"},{\"attributes\":{\"list\":\"ordered\"},\"insert\":\"\\n\"}]}','2021-04-05 06:36:10','2021-04-08 02:37:57',1),(9,'PHP','{\"ops\":[{\"insert\":\"5\\n5\\n5\\n5\\n\"}]}','2021-04-05 06:38:30','2021-04-05 06:38:30',1);
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
  `Status` enum('success','danger','warning') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'success',
  `Role` enum('admin','mentor','staff') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'staff',
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Avatar` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT '../img/user.jpg',
  `Phone` varchar(15) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin@x','Thang','Huynh','$2a$11$ZwUGQzP5M.gaE/FzHrbGDuNJrWhefvsoiTmyIDowKnhZuXRMBtux6','success','mentor','2021-03-15 03:39:36','2021-04-07 01:43:45','/img/user.jpg',NULL),(2,'tan@tma','Tân','Trần','$2a$11$Y6RWgY8CxI7zyGHvTqz16eCdcZPSERWFTHHtlQRWlwIWIAhoG4md6','success','admin','2021-03-16 20:43:28','2021-04-07 01:43:45','/img/user.jpg',NULL),(3,'thanh@qnu','Thanh','Tran Thien','$2a$11$kWeq0c.p4h5ASXdbdnuRweg8TDzumiS1sfkmb.IormcRxpBao7nsu','success','staff','2021-03-17 18:41:37','2021-04-07 01:43:45','/img/user.jpg',NULL),(4,'by@tma','By','Le Thi','$2a$11$QmPcqj0ast0KIogZxIvZiesOLfcg/bpOlpx34ZahIyIixMd/OmVTK','success','staff','2021-03-17 02:25:34','2021-04-07 01:43:45','/img/user.jpg',NULL),(5,'dat@n','Dat','Nguyen','$2a$11$FYKU9vMh4H/IBn./T/o1M.JUTXeqagGdd8wHcAAw1gxK.6bNtdG7O','success','mentor','2021-04-05 01:49:07','2021-04-07 01:41:11','/img/user.jpg',NULL);
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
/*!50003 DROP PROCEDURE IF EXISTS `GetAllPointWithName` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllPointWithName`()
BEGIN
	select
    t1.InternId,
    TechnicalSkill,
    SoftSkill,
    Attitude,
    Score,
    Passed,
    CONCAT(t2.FirstName,' ',t2.LastName) AS Surname,
    CONCAT(t3.FirstName,' ',t3.LastName) AS Marker
    from points as t1
    	JOIN interns t2 ON t2.InternId = t1.InternId
    	JOIN users t3 ON t3.UserId = t1.Marker;

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
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternList`(
	p_type nvarchar(100))
BEGIN

SET @base = "SELECT SQL_CALC_FOUND_ROWS
CONCAT(t1.FirstName,' ',t1.LastName) AS FullName,
CONCAT(t5.FirstName,' ',t5.LastName) AS Mentor,
t1.InternId,t1.Email,t1.Avatar,
t1.CreatedDate,
t1.Phone,t1.DateOfBirth,t1.Gender,
t1.Duration,t1.Type,
t2.OrgName,t3.DepName,t4.TraName,
t4.TrainingId FROM interns t1
JOIN organizations t2 ON t2.OrganizationId = t1.OrganizationId
JOIN departments t3 ON t3.DepartmentId = t1.DepartmentId
JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
JOIN users t5 ON t5.UserId = t1.Mentor
WHERE ";

SET @query = CONCAT(@base, p_type);

PREPARE stmt FROM @query;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SELECT FOUND_ROWS() AS FOUND_ROWS;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetInternListWithFilter` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `GetInternListWithFilter`(
	on_passed INT,

	date_filter INT,
	start_date NVARCHAR(10),
	end_date NVARCHAR(10),
    
	offset_value INT,
	limit_value INT,
	orderby INT,
	search_on INT,
	search_string NVARCHAR(20))
BEGIN

SELECT SQL_CALC_FOUND_ROWS
	CONCAT(t1.FirstName,' ',t1.LastName) AS FullName,
    CONCAT(t5.FirstName,' ',t5.LastName) AS Mentor,
	t1.InternId,
    t1.Email,
    t1.Avatar,
    t1.CreatedDate,
    t1.Phone,
	t1.DateOfBirth,
    t1.Gender,
    t1.Duration, 
    t1.Type,
    t2.OrgName,
	t3.DepName,
    t4.TraName,
    t4.TrainingId
FROM interns t1
	JOIN organizations t2 ON t2.OrganizationId = t1.OrganizationId
	JOIN departments t3 ON t3.DepartmentId = t1.DepartmentId
	JOIN trainings t4 ON t4.TrainingId = t1.TrainingId
	JOIN users t5 ON t5.UserId = t1.Mentor
	LEFT JOIN points t6 ON t6.InternId = t1.InternId
WHERE
CASE
    WHEN search_on = 0 THEN 1
    WHEN search_on = 1 AND search_string LIKE t1.InternId THEN 1
    WHEN search_on = 2 AND search_string LIKE t1.Email THEN 1
    WHEN search_on = 3 AND search_string LIKE t1.FirstName THEN 1
    WHEN search_on = 4 AND search_string LIKE t1.Phone THEN 1
    WHEN search_on = 5 AND search_string LIKE DepName THEN 1
    WHEN search_on = 6 AND search_string LIKE OrgName THEN 1
    WHEN search_on = 7 AND search_string LIKE TraName THEN 1
    WHEN search_on = 8 AND search_string LIKE t5.FirstName THEN 1
END = 1

AND	CASE 
	WHEN date_filter = 0 THEN 1
	WHEN date_filter = 1 AND SUBSTRING(Duration, 1, 10) > start_date THEN 1
	WHEN date_filter = 2 AND SUBSTRING(Duration, 1, 10) = start_date THEN 1
	WHEN date_filter = 3 AND SUBSTRING(Duration, 1, 10) < start_date THEN 1
	WHEN date_filter = 4 AND SUBSTRING(Duration, 1, 10) BETWEEN start_date AND end_date THEN 1
	WHEN date_filter = 5 AND SUBSTRING(Duration, 1, 10) NOT BETWEEN start_date AND end_date THEN 1
END = 1

AND	CASE 
	WHEN on_passed = 2 THEN 1
	WHEN on_passed = 0 AND t6.Passed = FALSE THEN 1
	WHEN on_passed = 1 AND t6.Passed = TRUE THEN 1
END = 1

ORDER BY
	CASE WHEN orderby=1 THEN t1.InternId END,
	CASE WHEN orderby=2 THEN t1.Email END,
	CASE WHEN orderby=3 THEN t1.FirstName END,
	CASE WHEN orderby=4 THEN t1.Phone END,
	CASE WHEN orderby=5 THEN DepName END,
	CASE WHEN orderby=6 THEN OrgName END,
	CASE WHEN orderby=7 THEN TraName END,
	CASE WHEN orderby=8 THEN t5.FirstName END
LIMIT offset_value, limit_value;

SELECT FOUND_ROWS() AS FOUND_ROWS;
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
/*!50003 DROP PROCEDURE IF EXISTS `HowManyPassed` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `HowManyPassed`()
BEGIN
	SELECT COUNT(*) AS result FROM points
    WHERE passed;
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
/*!50003 DROP PROCEDURE IF EXISTS `SetSharedTraining` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SetSharedTraining`(depId int, sharedArray varchar(100))
BEGIN
	UPDATE departments
    SET SharedTrainings = sharedArray
    WHERE departmentId = depId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `UpdateEventByTitle` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `UpdateEventByTitle`(
	inTitle nvarchar(500),
	inType nvarchar(500),
	inClassName nvarchar(500),
	inStart nvarchar(500),
	inEnd nvarchar(500),
	inCreatedBy nvarchar(500),
	inGestsField nvarchar(500),
	inRepeatField nvarchar(500),
	inEventLocationLabel nvarchar(500),
	inEventDescriptionLabel nvarchar(500))
BEGIN
	UPDATE events
    SET 
		Title = inTitle,
		Type = inType,
		ClassName = inClassName,
		Start = inStart,
		End = inEnd,
		CreatedBy = inCreatedBy,
		GestsField = inGestsField,
		RepeatField = inRepeatField,
		EventLocationLabel = inEventLocationLabel,
		EventDescriptionLabel = inEventDescriptionLabel
    WHERE Title = inTitle;
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

-- Dump completed on 2021-04-09 17:06:37
