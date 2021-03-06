USE [master]
GO
/****** Object:  Database [AirlineReservation]    Script Date: 12/8/2017 12:48:10 AM ******/
CREATE DATABASE [AirlineReservation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AirlineReservation', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AirlineReservation.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AirlineReservation_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AirlineReservation_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AirlineReservation] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AirlineReservation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AirlineReservation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AirlineReservation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AirlineReservation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AirlineReservation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AirlineReservation] SET ARITHABORT OFF 
GO
ALTER DATABASE [AirlineReservation] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AirlineReservation] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [AirlineReservation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AirlineReservation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AirlineReservation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AirlineReservation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AirlineReservation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AirlineReservation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AirlineReservation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AirlineReservation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AirlineReservation] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AirlineReservation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AirlineReservation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AirlineReservation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AirlineReservation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AirlineReservation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AirlineReservation] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AirlineReservation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AirlineReservation] SET RECOVERY FULL 
GO
ALTER DATABASE [AirlineReservation] SET  MULTI_USER 
GO
ALTER DATABASE [AirlineReservation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AirlineReservation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AirlineReservation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AirlineReservation] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AirlineReservation', N'ON'
GO
USE [AirlineReservation]
GO
/****** Object:  User [BLRAMEXODC\AXM1007480]    Script Date: 12/8/2017 12:48:10 AM ******/

GO
/****** Object:  User [appuser]    Script Date: 12/8/2017 12:48:10 AM ******/
CREATE USER [appuser] FOR LOGIN [appuser] WITH DEFAULT_SCHEMA=[db_owner]
GO


/****** Object:  Table [dbo].[City]    Script Date: 12/8/2017 12:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[city_id] [int] IDENTITY(1,1) NOT NULL,
	[country_code] [varchar](2) NOT NULL,
	[airport_code] [varchar](3) NOT NULL,
	[city_name] [varchar](50) NOT NULL,
	[airport] [varchar](100) NOT NULL,
	[created_on] [datetime] NOT NULL,
	[modfied_on] [datetime] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[city_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 12/8/2017 12:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[country_code] [varchar](2) NOT NULL,
	[country_name] [varchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Country_1] PRIMARY KEY CLUSTERED 
(
	[country_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FareMapping]    Script Date: 12/8/2017 12:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FareMapping](
	[Fare_id] [int] IDENTITY(1,1) NOT NULL,
	[journey_id] [int] NOT NULL,
	[class] [varchar](2) NOT NULL,
	[cost] [money] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_FlightBooking_Info] PRIMARY KEY CLUSTERED 
(
	[Fare_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Journey]    Script Date: 12/8/2017 12:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Journey](
	[journey_id] [int] IDENTITY(1,1) NOT NULL,
	[source] [int] NOT NULL,
	[dest] [int] NOT NULL,
	[route] [varchar](50) NULL,
 CONSTRAINT [PK_Journey] PRIMARY KEY CLUSTERED 
(
	[journey_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 12/8/2017 12:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[journey_id] [int] NOT NULL,
	[dep_date_time] [datetime] NOT NULL,
	[arr_date_time] [datetime] NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Ticketing_Info]    Script Date: 12/8/2017 12:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Ticketing_Info](
	[ticket_id] [varchar](20) NOT NULL,
	[schedule_id] [int] NOT NULL,
	[cust_id] [int] NOT NULL,
	[Fare_Id] [int] NOT NULL,
	[status] [varchar](3) NOT NULL,
 CONSTRAINT [PK_Ticketing_Info] PRIMARY KEY CLUSTERED 
(
	[ticket_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 12/8/2017 12:48:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfo](
	[cust_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](50) NOT NULL,
	[password] [nvarchar](35) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[country] [varchar](2) NOT NULL,
	[mobile] [varchar](10) NOT NULL,
	[date_of_birth] [date] NOT NULL,
	[email_addr] [nvarchar](50) NOT NULL,
	[passport_no] [varchar](15) NULL,
	[misc_info] [varchar](50) NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[cust_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [IX_UserInfo]    Script Date: 12/8/2017 12:48:10 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserInfo] ON [dbo].[UserInfo]
(
	[cust_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_created_on]  DEFAULT (getdate()) FOR [created_on]
GO
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_modfied_on]  DEFAULT (getdate()) FOR [modfied_on]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[FareMapping] ADD  CONSTRAINT [DF_FlightBooking_Info_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_Country] FOREIGN KEY([country_code])
REFERENCES [dbo].[Country] ([country_code])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_Country]
GO
ALTER TABLE [dbo].[FareMapping]  WITH CHECK ADD  CONSTRAINT [FK_FareMapping_Journey] FOREIGN KEY([journey_id])
REFERENCES [dbo].[Journey] ([journey_id])
GO
ALTER TABLE [dbo].[FareMapping] CHECK CONSTRAINT [FK_FareMapping_Journey]
GO
ALTER TABLE [dbo].[Journey]  WITH CHECK ADD  CONSTRAINT [FK_Journey_City_Dest] FOREIGN KEY([dest])
REFERENCES [dbo].[City] ([city_id])
GO
ALTER TABLE [dbo].[Journey] CHECK CONSTRAINT [FK_Journey_City_Dest]
GO
ALTER TABLE [dbo].[Journey]  WITH CHECK ADD  CONSTRAINT [FK_Journey_City_Source] FOREIGN KEY([source])
REFERENCES [dbo].[City] ([city_id])
GO
ALTER TABLE [dbo].[Journey] CHECK CONSTRAINT [FK_Journey_City_Source]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Journey] FOREIGN KEY([journey_id])
REFERENCES [dbo].[Journey] ([journey_id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Journey]
GO
ALTER TABLE [dbo].[Ticketing_Info]  WITH CHECK ADD  CONSTRAINT [FK_Ticketing_Info_FareMapping] FOREIGN KEY([Fare_Id])
REFERENCES [dbo].[FareMapping] ([Fare_id])
GO
ALTER TABLE [dbo].[Ticketing_Info] CHECK CONSTRAINT [FK_Ticketing_Info_FareMapping]
GO
ALTER TABLE [dbo].[Ticketing_Info]  WITH CHECK ADD  CONSTRAINT [FK_Ticketing_Info_Schedule] FOREIGN KEY([schedule_id])
REFERENCES [dbo].[Schedule] ([schedule_id])
GO
ALTER TABLE [dbo].[Ticketing_Info] CHECK CONSTRAINT [FK_Ticketing_Info_Schedule]
GO
ALTER TABLE [dbo].[Ticketing_Info]  WITH CHECK ADD  CONSTRAINT [FK_Ticketing_Info_UserInfo] FOREIGN KEY([cust_id])
REFERENCES [dbo].[UserInfo] ([cust_id])
GO
ALTER TABLE [dbo].[Ticketing_Info] CHECK CONSTRAINT [FK_Ticketing_Info_UserInfo]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_UserInfo_Country] FOREIGN KEY([country])
REFERENCES [dbo].[Country] ([country_code])
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_Country]
GO
USE [master]
GO
ALTER DATABASE [AirlineReservation] SET  READ_WRITE 
GO
