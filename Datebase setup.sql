﻿USE [master]
GO
/****** Object:  Database [Contact_DB]    Script Date: 2/17/2018 6:56:28 PM ******/
CREATE DATABASE [Contact_DB]
 CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'Contact_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Contact_DB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
-- LOG ON 
--( NAME = N'Contact_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Contact_DB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)

GO
ALTER DATABASE [Contact_DB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Contact_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Contact_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Contact_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Contact_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Contact_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Contact_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Contact_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Contact_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Contact_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Contact_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Contact_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Contact_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Contact_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Contact_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Contact_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Contact_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Contact_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Contact_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Contact_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Contact_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Contact_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Contact_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Contact_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Contact_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Contact_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Contact_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Contact_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Contact_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Contact_DB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Contact_DB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Contact_DB', N'ON'
GO
USE [Contact_DB]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Birthdate] [date] NULL,
	[Photo] [nvarchar](200) NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ContactGroups]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactGroups](
	[ContactId] [int] NOT NULL,
	[GroupId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nchar](100) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ContactGroups]  WITH CHECK ADD  CONSTRAINT [FK_ContactGroups_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactGroups] CHECK CONSTRAINT [FK_ContactGroups_Contact]
GO
ALTER TABLE [dbo].[ContactGroups]  WITH CHECK ADD  CONSTRAINT [FK_ContactGroups_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ContactGroups] CHECK CONSTRAINT [FK_ContactGroups_Groups]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteContactGroup]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 2/19/2018
-- Description:	Delete contact from group
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteContactGroup]
	-- Add the parameters for the stored procedure here
	@contactId INT,
	@groupid INT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @result NVARCHAR(100) = 'No match was found.'

    IF EXISTS( SELECT 1 FROM dbo.ContactGroups WHERE ContactId = @contactId AND GroupId = @groupid)
		DELETE FROM dbo.ContactGroups WHERE ContactId = @contactId AND GroupId = @groupid

		SET @result = 'Contact has been deleted from group successfully.'

		SELECT @result Result
         
END

GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteGroup]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		B. Marthone
-- Create date: 2/13/2018
-- Description:	Delete from dbo.Groups
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteGroup]
	-- Add the parameters for the stored procedure here
	@id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM dbo.ContactGroups WHERE GroupId = @id)
		SELECT 'Can''t delete group cause it''s link to a contact. Please unlink group to continue.' Result
	ELSE 
		BEGIN
			DELETE FROM dbo.Groups WHERE Id = @id
			SELECT 'Group has been deleted successfully.' Result
		END 
		

END

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertGroup]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		B. Marthone
-- Create date: 2/13/2018
-- Description:	Insert into dbo.Groups
-- =============================================
CREATE PROCEDURE [dbo].[sp_InsertGroup]
	-- Add the parameters for the stored procedure here
	@groupName NVARCHAR(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT dbo.Groups
    (
        GroupName
    )
    VALUES
    (
		@groupName     
	)

	SELECT 'New group has been added successfully.' Result
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SelectGroup]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau M
-- Create date: 2/13/2018
-- Description:	Select * from dbo.Groups
-- =============================================
CREATE PROCEDURE [dbo].[sp_SelectGroup]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, LTRIM(RTRIM(GroupName)) GroupName FROM dbo.Groups ORDER BY GroupName
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SelectGroupById]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beau Marthone
-- Create date: 2/15/2018
-- Description:	Select group by id
-- =============================================
CREATE PROCEDURE [dbo].[sp_SelectGroupById] 
	-- Add the parameters for the stored procedure here
@Id int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT c.*, g.GroupName FROM dbo.Contact c
	 JOIN dbo.ContactGroups cp ON cp.ContactId = c.Id
	 JOIN dbo.Groups g ON cp.GroupId = g.Id
	 WHERE g.Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateGroup]    Script Date: 2/17/2018 6:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		B. Marthone
-- Create date: 2/13/2018
-- Description:	Update dbo.Groups
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateGroup]
	-- Add the parameters for the stored procedure here
	@id INT,
	@groupName NVARCHAR(50) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE dbo.Groups
	SET GroupName = @groupName
	WHERE id = @id  

	SELECT 'Group has been updated successfully.' Result
END


GO
USE [master]
GO
ALTER DATABASE [Contact_DB] SET  READ_WRITE 
GO


--- Insert default data

GO
USE [Contact_DB]
GO 

INSERT INTO dbo.Groups
(GroupName)
VALUES
('Family'),('Friend'),('Colleague'),('Associate Options')

INSERT INTO dbo.Contact
(
    FirstName,
    LastName,
    Email,
    Phone,
    Birthdate,
    Photo,
    Comments
)
VALUES
(   N'Beta',       -- FirstName - nvarchar(50)
    N'Tester',       -- LastName - nvarchar(50)
    N'beta.tester@gmail.com',       -- Email - nvarchar(50)
    N'(123) 123-4567',       -- Phone - nvarchar(50)
    GETDATE(), -- Birthdate - date
    N'default_avatar.png',       -- Photo - nvarchar(200)
    N'No comments for now'        -- Comments - nvarchar(max)
    )

DECLARE @_id INT = SCOPE_IDENTITY();

INSERT INTO dbo.ContactGroups
(
    ContactId,
    GroupId
)
VALUES
(@_id, 1), (@_id, 2)

go
