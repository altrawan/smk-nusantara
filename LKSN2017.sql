USE [master]
GO
/****** Object:  Database [LKSN2017]    Script Date: 3/5/2022 8:14:21 PM ******/
CREATE DATABASE [LKSN2017]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LKSN2017', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LKSN2017.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LKSN2017_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LKSN2017_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LKSN2017] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LKSN2017].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LKSN2017] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LKSN2017] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LKSN2017] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LKSN2017] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LKSN2017] SET ARITHABORT OFF 
GO
ALTER DATABASE [LKSN2017] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LKSN2017] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LKSN2017] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LKSN2017] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LKSN2017] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LKSN2017] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LKSN2017] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LKSN2017] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LKSN2017] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LKSN2017] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LKSN2017] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LKSN2017] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LKSN2017] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LKSN2017] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LKSN2017] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LKSN2017] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LKSN2017] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LKSN2017] SET RECOVERY FULL 
GO
ALTER DATABASE [LKSN2017] SET  MULTI_USER 
GO
ALTER DATABASE [LKSN2017] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LKSN2017] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LKSN2017] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LKSN2017] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LKSN2017] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LKSN2017', N'ON'
GO
ALTER DATABASE [LKSN2017] SET QUERY_STORE = OFF
GO
USE [LKSN2017]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 3/5/2022 8:14:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectID] [char](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Assignment] [int] NOT NULL,
	[MidExam] [int] NOT NULL,
	[FinalExam] [int] NOT NULL,
	[ShiftDuration] [int] NOT NULL,
	[Grade] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [varchar](8) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[Gender] [varchar](7) NOT NULL,
	[DateofBirth] [date] NOT NULL,
	[PhoneNumber] [varchar](12) NOT NULL,
	[Photo] [varchar](100) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherID] [varchar](8) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[PhoneNumber] [varchar](12) NOT NULL,
	[DateofBirth] [date] NOT NULL,
	[Gender] [varchar](7) NOT NULL,
	[Address] [varchar](100) NOT NULL,
	[Photo] [varchar](100) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HeaderSchedule]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HeaderSchedule](
	[ScheduleID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](5) NOT NULL,
	[Finalize] [int] NOT NULL,
 CONSTRAINT [PK_HeaderSchedule] PRIMARY KEY CLUSTERED 
(
	[ScheduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailSchedule]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailSchedule](
	[DetailID] [int] IDENTITY(1,1) NOT NULL,
	[ScheduleID] [int] NOT NULL,
	[SubjectID] [char](5) NOT NULL,
	[TeacherID] [varchar](8) NOT NULL,
	[ShiftID] [int] NOT NULL,
	[Day] [char](10) NOT NULL,
 CONSTRAINT [PK_DetailSchedule] PRIMARY KEY CLUSTERED 
(
	[DetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailScore]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailScore](
	[ScoreDetailID] [int] IDENTITY(1,1) NOT NULL,
	[DetailID] [int] NOT NULL,
	[StudentID] [varchar](8) NOT NULL,
	[Assignment] [int] NULL,
	[MidExam] [int] NULL,
	[FinalExam] [int] NULL,
 CONSTRAINT [PK_DetailScore] PRIMARY KEY CLUSTERED 
(
	[ScoreDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_1]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_1]
AS
SELECT        dbo.DetailScore.StudentID, dbo.Student.Name, dbo.DetailScore.Assignment, dbo.DetailScore.MidExam, dbo.DetailScore.FinalExam, dbo.DetailSchedule.SubjectID, dbo.HeaderSchedule.ClassName, 
                         dbo.Subject.Name AS Expr1
FROM            dbo.DetailSchedule INNER JOIN
                         dbo.DetailScore ON dbo.DetailSchedule.DetailID = dbo.DetailScore.DetailID INNER JOIN
                         dbo.HeaderSchedule ON dbo.DetailSchedule.ScheduleID = dbo.HeaderSchedule.ScheduleID INNER JOIN
                         dbo.Student ON dbo.DetailScore.StudentID = dbo.Student.StudentID INNER JOIN
                         dbo.Subject ON dbo.DetailSchedule.SubjectID = dbo.Subject.SubjectID INNER JOIN
                         dbo.Teacher ON dbo.DetailSchedule.TeacherID = dbo.Teacher.TeacherID
GO
/****** Object:  Table [dbo].[Class]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassName] [varchar](5) NOT NULL,
	[Grade] [int] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailClass]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailClass](
	[DetailClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](5) NOT NULL,
	[StudentID] [varchar](8) NOT NULL,
 CONSTRAINT [PK_DetailClass] PRIMARY KEY CLUSTERED 
(
	[DetailClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expertise]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expertise](
	[ExpertiseID] [int] IDENTITY(1,1) NOT NULL,
	[TeacherID] [varchar](8) NOT NULL,
	[SubjectID] [char](5) NOT NULL,
 CONSTRAINT [PK_Expertise] PRIMARY KEY CLUSTERED 
(
	[ExpertiseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[Sender] [varchar](8) NOT NULL,
	[Receiver] [varchar](8) NOT NULL,
	[Detail] [varchar](160) NOT NULL,
	[Status] [varchar](10) NOT NULL,
	[SentTime] [varchar](20) NOT NULL,
	[Title] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shift]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shift](
	[ShiftID] [int] IDENTITY(1,1) NOT NULL,
	[Time] [char](13) NOT NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[ShiftID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[temp]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp](
	[id_temp] [int] IDENTITY(1,1) NOT NULL,
	[id] [varchar](8) NULL,
	[sender] [varchar](8) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/5/2022 8:14:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](8) NULL,
	[Password] [varchar](10) NULL,
	[Role] [varchar](8) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetailClass]  WITH CHECK ADD  CONSTRAINT [FK_DetailClass_Class] FOREIGN KEY([ClassName])
REFERENCES [dbo].[Class] ([ClassName])
GO
ALTER TABLE [dbo].[DetailClass] CHECK CONSTRAINT [FK_DetailClass_Class]
GO
ALTER TABLE [dbo].[DetailClass]  WITH CHECK ADD  CONSTRAINT [FK_DetailClass_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[DetailClass] CHECK CONSTRAINT [FK_DetailClass_Student]
GO
ALTER TABLE [dbo].[DetailSchedule]  WITH CHECK ADD  CONSTRAINT [FK_DetailSchedule_HeaderSchedule] FOREIGN KEY([ScheduleID])
REFERENCES [dbo].[HeaderSchedule] ([ScheduleID])
GO
ALTER TABLE [dbo].[DetailSchedule] CHECK CONSTRAINT [FK_DetailSchedule_HeaderSchedule]
GO
ALTER TABLE [dbo].[DetailSchedule]  WITH CHECK ADD  CONSTRAINT [FK_DetailSchedule_Shift] FOREIGN KEY([ShiftID])
REFERENCES [dbo].[Shift] ([ShiftID])
GO
ALTER TABLE [dbo].[DetailSchedule] CHECK CONSTRAINT [FK_DetailSchedule_Shift]
GO
ALTER TABLE [dbo].[DetailSchedule]  WITH CHECK ADD  CONSTRAINT [FK_DetailSchedule_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[DetailSchedule] CHECK CONSTRAINT [FK_DetailSchedule_Subject]
GO
ALTER TABLE [dbo].[DetailSchedule]  WITH CHECK ADD  CONSTRAINT [FK_DetailSchedule_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([TeacherID])
GO
ALTER TABLE [dbo].[DetailSchedule] CHECK CONSTRAINT [FK_DetailSchedule_Teacher]
GO
ALTER TABLE [dbo].[DetailScore]  WITH CHECK ADD  CONSTRAINT [FK_DetailScore_DetailSchedule] FOREIGN KEY([DetailID])
REFERENCES [dbo].[DetailSchedule] ([DetailID])
GO
ALTER TABLE [dbo].[DetailScore] CHECK CONSTRAINT [FK_DetailScore_DetailSchedule]
GO
ALTER TABLE [dbo].[DetailScore]  WITH CHECK ADD  CONSTRAINT [FK_DetailScore_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[DetailScore] CHECK CONSTRAINT [FK_DetailScore_Student]
GO
ALTER TABLE [dbo].[Expertise]  WITH CHECK ADD  CONSTRAINT [FK_Expertise_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[Expertise] CHECK CONSTRAINT [FK_Expertise_Subject]
GO
ALTER TABLE [dbo].[Expertise]  WITH CHECK ADD  CONSTRAINT [FK_Expertise_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([TeacherID])
GO
ALTER TABLE [dbo].[Expertise] CHECK CONSTRAINT [FK_Expertise_Teacher]
GO
ALTER TABLE [dbo].[HeaderSchedule]  WITH CHECK ADD  CONSTRAINT [FK_HeaderSchedule_Class] FOREIGN KEY([ClassName])
REFERENCES [dbo].[Class] ([ClassName])
GO
ALTER TABLE [dbo].[HeaderSchedule] CHECK CONSTRAINT [FK_HeaderSchedule_Class]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -192
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DetailSchedule"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DetailScore"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "HeaderSchedule"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Student"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Subject"
            Begin Extent = 
               Top = 252
               Left = 38
               Bottom = 382
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Teacher"
            Begin Extent = 
               Top = 270
               Left = 246
               Bottom = 400
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'4065
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_1'
GO
USE [master]
GO
ALTER DATABASE [LKSN2017] SET  READ_WRITE 
GO
