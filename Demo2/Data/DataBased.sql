USE [DemoProduct]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/9/2021 8:07:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Email] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetFullName]    Script Date: 3/9/2021 8:07:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFullName]
                        @LastName nvarchar(50),
                        @FirstName nvarchar(50)
                    AS
                        RETURN @LastName + @FirstName;
GO
/****** Object:  StoredProcedure [dbo].[UserCheck]    Script Date: 3/9/2021 8:07:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserCheck]
                        @Email varchar(100)
                    AS
                        SELECT * FROM Users WHERE Email = @Email;
GO
/****** Object:  StoredProcedure [dbo].[UserCreate]    Script Date: 3/9/2021 8:07:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UserCreate]   
                        @FirstName nvarchar(50),
                        @LastName nvarchar(50),   
                        @Email varchar(100),
                        @PasswordHash varchar(100)
                    AS   
                        INSERT INTO Users (FirstName,LastName,Email,PasswordHash)
                        VALUES (@FirstName,@LastName,@Email,@PasswordHash);
GO
