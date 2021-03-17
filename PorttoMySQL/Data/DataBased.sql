USE demoproduct;

DROP TABLE IF EXISTS `departments`;
CREATE TABLE `departments` (
  `DepartmentId` int NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Location` varchar(100) NOT NULL,
  PRIMARY KEY (`DepartmentId`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


LOCK TABLES `departments` WRITE;
INSERT INTO `departments` VALUES (1,'TIP','Quy Nhon'),(2,'LAB6','HCM'),(3,'LAB8','HCM');
UNLOCK TABLES;


DROP TABLE IF EXISTS `events`;
CREATE TABLE `events` (
  `EventId` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) NOT NULL,
  `Type` varchar(50) NOT NULL,
  `ClassName` varchar(50) NOT NULL,
  `Start` varchar(100) NOT NULL,
  `End` varchar(100) NOT NULL,
  `CreatedBy` int NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` int DEFAULT NULL,
  `UpdatedDate` timestamp NULL DEFAULT NULL,
  `RepeatField` varchar(100) DEFAULT NULL,
  `GestsField` json DEFAULT NULL,
  `EventLocationLabel` varchar(100) DEFAULT NULL,
  `EventDescriptionLabel` varchar(500) DEFAULT NULL,
  `Image` varchar(200) DEFAULT '../img/event.png',
  PRIMARY KEY (`EventId`),
  KEY `IX_Events_Type` (`Type`),
  KEY `FK_Events_Created_idx` (`CreatedBy`,`UpdatedBy`),
  KEY `FK_Events_Updated_idx` (`UpdatedBy`),
  KEY `fgfgf_idx` (`UpdatedDate`),
  KEY `dfdfg_idx` (`CreatedDate`),
  CONSTRAINT `FK_Events_Created` FOREIGN KEY (`CreatedBy`) REFERENCES `users` (`UserId`),
  CONSTRAINT `FK_Events_EventTypes_Type` FOREIGN KEY (`Type`) REFERENCES `eventtypes` (`Type`) ON DELETE CASCADE,
  CONSTRAINT `FK_Events_Updated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

LOCK TABLES `events` WRITE;
UNLOCK TABLES;


DROP TABLE IF EXISTS `eventtypes`;
CREATE TABLE `eventtypes` (
  `Type` varchar(50) NOT NULL,
  `ClassName` varchar(100) NOT NULL,
  `Color` varchar(50) NOT NULL,
  PRIMARY KEY (`Type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

LOCK TABLES `eventtypes` WRITE;
INSERT INTO `eventtypes` VALUES ('Holidays','fullcalendar-custom-event-holidays','warning'),('Personal','fullcalendar-custom-event-hs-team','primary'),('Reminders','fullcalendar-custom-event-reminders','danger'),('Tasks','fullcalendar-custom-event-tasks','dark');
UNLOCK TABLES;


DROP TABLE IF EXISTS `trainings`;
CREATE TABLE `trainings` (
  `TrainingId` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Data` json DEFAULT NULL,
  PRIMARY KEY (`TrainingId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


DROP TABLE IF EXISTS `interns`;
CREATE TABLE `interns` (
  `InternId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `Gender` enum('female','male') NOT NULL,
  `DateOfBirth` datetime NOT NULL,
  `Duration` varchar(23) NOT NULL,
  `TrainingId` int DEFAULT NULL,
  `Type` enum('Full time','Part time') NOT NULL,
  `Mentor` int NOT NULL,
  `UpdatedBy` int DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT NULL,
  `Avatar` varchar(100) NOT NULL DEFAULT '../img/intern.png',
  `Phone` varchar(15) DEFAULT NULL,
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
  CONSTRAINT `FK_Interns_Updated` FOREIGN KEY (`UpdatedBy`) REFERENCES `users` (`UserId`),
  CONSTRAINT `FK_Interns_Training` FOREIGN KEY (`TrainingId`) REFERENCES `trainings` (`TrainingId`)
) ENGINE=InnoDB AUTO_INCREMENT=58 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


LOCK TABLES `interns` WRITE;
UNLOCK TABLES;

DROP TABLE IF EXISTS `internshippoints`;
CREATE TABLE `internshippoints` (
  `InternId` int NOT NULL,
  `TechnicalSkill` decimal(2,2) DEFAULT NULL,
  `SoftSkill` decimal(2,2) DEFAULT NULL,
  `Attitude` decimal(2,2) DEFAULT NULL,
  `Result` decimal(2,2) DEFAULT NULL,
  PRIMARY KEY (`InternId`),
  CONSTRAINT `FK_internshippoints_Intern` FOREIGN KEY (`InternId`) REFERENCES `interns` (`InternId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

LOCK TABLES `internshippoints` WRITE;
UNLOCK TABLES;


DROP TABLE IF EXISTS `organizations`;
CREATE TABLE `organizations` (
  `OrganizationId` int NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Address` varchar(100) NOT NULL,
  `Phone` varchar(15) NOT NULL,
  PRIMARY KEY (`OrganizationId`),
  UNIQUE KEY `Name_UNIQUE` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


LOCK TABLES `organizations` WRITE;
INSERT INTO `organizations` VALUES (1,'Quy Nhon University','170 An Dương Vương, Nguyễn Văn Cừ, Thành phố Qui Nhơn, Bình Định','0256 3846 156');
UNLOCK TABLES;


DROP TABLE IF EXISTS `questions`;
CREATE TABLE `questions` (
  `QuestionId` int NOT NULL AUTO_INCREMENT,
  `Group` varchar(50) NOT NULL,
  `InData` varchar(2000) NOT NULL,
  `OutData` varchar(5000) NOT NULL,
  PRIMARY KEY (`QuestionId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

LOCK TABLES `questions` WRITE;
INSERT INTO `questions` VALUES (1,'Recruit','Cho em hỏi TMA phỏng vấn tiếng Anh hay tiếng Việt?','Bên mình sẽ phỏng vấn cả tiếng Anh và tiếng Việt nha bạn.'),(2,'Recruit','Anh/chị cho em hỏi nếu test tiếng Anh ở công ty thì đề thi như thế nào ạ? Bao lâu sẽ có kết quả?','Chào bạn, đề thi sẽ theo chuẩn đề TOEIC, thông thường kết quả sẽ được thông báo trong vòng 1 tuần, tùy thuộc vào mỗi dự án.'),(3,'Recruit','Em là SV năm cuối (đang làm luận văn), chuyên ngành: Điện tử - Viễn thông, trường: ĐH Bách Khoa Tp. HCM. Em có 2 câu hỏi: TMA có tuyển thực tập sinh không? Nếu có, yêu cầu cụ thể (GPA, Tiếng Anh,...) như thế nào?','TMA thường xuyên tuyển thực tập sinh, sắp tới bên chị sẽ nhận hồ sơ thực tập để chuẩn bị cho đợt thực tập kế tiếp vào tháng 9. Hồ sơ ứng tuyển bao gồm:<ul class=\'mt-3\'><li>CV Tiếng Anh;</li><li> Bảng điểm hoặc bằng Tiếng Anh(nếu có);</li><li> Hình 3x4;</li><li> Giấy giới thiệu thực tập</li></ul> '),(4,'Internship','Ngoại ngữ có yêu cầu cao không Ad?','Nếu em đã có các bằng như (TOEIC, TOEFL, IELTS) tương đương TOEIC 450 trở lên thì không phải làm bài test em nhé.'),(5,'Internship','Test thực tập đầu vào như thế nào chị?','Bài test đầu vào gồm: IQ (25’) và tiếng Anh (nếu em chưa có bằng)'),(6,'Internship','Thời gian thực tập yêu cầu là bao nhiêu vậy Ad?','Thời gian thực tập kéo dài 3 tháng, 2.5 ngày/tuần em à.');
UNLOCK TABLES;

DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(100) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL,
  `Status` enum('success','danger','warning') NOT NULL,
  `Role` enum('admin','mentor','staff') NOT NULL DEFAULT 'staff',
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDate` timestamp NULL DEFAULT NULL,
  `Avatar` varchar(100) DEFAULT '../img/user.jpg',
  `Phone` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;


LOCK TABLES `users` WRITE;
INSERT INTO `users` VALUES (1,'admin@x','Nguyen','Thach','$2a$11$ZwUGQzP5M.gaE/FzHrbGDuNJrWhefvsoiTmyIDowKnhZuXRMBtux6','success','staff','2021-03-15 10:39:36',NULL,'../img/user.jpg',NULL),(6,'tan@x','Tân','Trần','$2a$11$Y6RWgY8CxI7zyGHvTqz16eCdcZPSERWFTHHtlQRWlwIWIAhoG4md6','success','staff','2021-03-17 03:43:28',NULL,'../img/user.jpg',NULL);
UNLOCK TABLES;
