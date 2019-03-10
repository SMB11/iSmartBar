USE [master]
GO
/****** Object:  Database [MiniBar]    Script Date: 3/9/2019 12:16:35 PM ******/
CREATE DATABASE [MiniBar]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MiniBar', FILENAME = N'c:\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\Data\MiniBar.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MiniBar_log', FILENAME = N'c:\Microsoft SQL Server\MSSQL12.MSSQLSERVER2014\MSSQL\Data\MiniBar_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MiniBar] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiniBar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiniBar] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MiniBar] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MiniBar] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MiniBar] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MiniBar] SET ARITHABORT OFF 
GO
ALTER DATABASE [MiniBar] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MiniBar] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MiniBar] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MiniBar] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MiniBar] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MiniBar] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MiniBar] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MiniBar] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MiniBar] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MiniBar] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MiniBar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MiniBar] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MiniBar] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MiniBar] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MiniBar] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MiniBar] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MiniBar] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MiniBar] SET RECOVERY FULL 
GO
ALTER DATABASE [MiniBar] SET  MULTI_USER 
GO
ALTER DATABASE [MiniBar] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MiniBar] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MiniBar] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MiniBar] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MiniBar] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MiniBar', N'ON'
GO
USE [MiniBar]
GO
/****** Object:  Table [dbo].[BrandNames]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BrandNames](
	[BrandID] [int] NOT NULL,
	[LanguageID] [varchar](10) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_BrandNames] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC,
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Brands](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[Image] [varchar](500) NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryNames]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryNames](
	[CategoryID] [int] NOT NULL,
	[LanguageID] [varchar](10) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_CategoryNames] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC,
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageID] [varchar](10) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LanguageTexts]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LanguageTexts](
	[TextKey] [varchar](50) NOT NULL,
	[LanguageID] [varchar](10) NOT NULL,
	[Value] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_LanguageTexts] PRIMARY KEY CLUSTERED 
(
	[TextKey] ASC,
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductInfo]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductInfo](
	[ProductID] [int] NOT NULL,
	[LanguageID] [varchar](10) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
 CONSTRAINT [PK_ProductInfo] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[LanguageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Products]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[BrandID] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[BrandNames] ([BrandID], [LanguageID], [Name]) VALUES (1, N'en', N'Test')
INSERT [dbo].[BrandNames] ([BrandID], [LanguageID], [Name]) VALUES (1, N'it', N'Test IT')
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([BrandID], [Image]) VALUES (1, N'test')
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (1, NULL)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (2, NULL)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (3, 1)
SET IDENTITY_INSERT [dbo].[Categories] OFF
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (1, N'en', N'Food')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (1, N'it', N'Food IT')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (2, N'en', N'Drink')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (2, N'it', N'Drink IT')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (3, N'en', N'Test')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (3, N'it', N'Test IT')
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'en', N'English')
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'it', N'Italian')
INSERT [dbo].[LanguageTexts] ([TextKey], [LanguageID], [Value]) VALUES (N'TEST_TEXT', N'en', N'Hello World')
INSERT [dbo].[LanguageTexts] ([TextKey], [LanguageID], [Value]) VALUES (N'TEST_TEXT', N'it', N'Hello World IT')
INSERT [dbo].[LanguageTexts] ([TextKey], [LanguageID], [Value]) VALUES (N'TEST_TEXT2', N'en', N'Hello World 2')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (1, N'en', N'Product Name', N'Product Desc')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (1, N'it', N'Product Name IT', N'Product Desc IT')
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID]) VALUES (1, 3, 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
ALTER TABLE [dbo].[BrandNames]  WITH CHECK ADD  CONSTRAINT [FK_BrandNames_Brands] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brands] ([BrandID])
GO
ALTER TABLE [dbo].[BrandNames] CHECK CONSTRAINT [FK_BrandNames_Brands]
GO
ALTER TABLE [dbo].[BrandNames]  WITH CHECK ADD  CONSTRAINT [FK_BrandNames_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([LanguageID])
GO
ALTER TABLE [dbo].[BrandNames] CHECK CONSTRAINT [FK_BrandNames_Languages]
GO
ALTER TABLE [dbo].[CategoryNames]  WITH NOCHECK ADD  CONSTRAINT [FK_CategoryNames_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[CategoryNames] CHECK CONSTRAINT [FK_CategoryNames_Categories]
GO
ALTER TABLE [dbo].[CategoryNames]  WITH NOCHECK ADD  CONSTRAINT [FK_CategoryNames_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([LanguageID])
GO
ALTER TABLE [dbo].[CategoryNames] CHECK CONSTRAINT [FK_CategoryNames_Languages]
GO
ALTER TABLE [dbo].[LanguageTexts]  WITH CHECK ADD  CONSTRAINT [FK_LanguageTexts_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([LanguageID])
GO
ALTER TABLE [dbo].[LanguageTexts] CHECK CONSTRAINT [FK_LanguageTexts_Languages]
GO
ALTER TABLE [dbo].[ProductInfo]  WITH CHECK ADD  CONSTRAINT [FK_ProductInfo_Languages] FOREIGN KEY([LanguageID])
REFERENCES [dbo].[Languages] ([LanguageID])
GO
ALTER TABLE [dbo].[ProductInfo] CHECK CONSTRAINT [FK_ProductInfo_Languages]
GO
ALTER TABLE [dbo].[ProductInfo]  WITH CHECK ADD  CONSTRAINT [FK_ProductInfo_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[ProductInfo] CHECK CONSTRAINT [FK_ProductInfo_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Brands] FOREIGN KEY([BrandID])
REFERENCES [dbo].[Brands] ([BrandID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Brands]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryBrands]    Script Date: 3/9/2019 12:16:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCategoryBrands]
	@categoryID [int] = 0
AS
SELECT Brands.* FROM dbo.Products
	LEFT JOIN dbo.Brands
		ON Brands.BrandID = Products.BrandID
WHERE Products.CategoryID = @categoryID

GO
USE [master]
GO
ALTER DATABASE [MiniBar] SET  READ_WRITE 
GO
