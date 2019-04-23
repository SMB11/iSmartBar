USE [master]
GO
/****** Object:  Database [MiniBar]    Script Date: 4/23/2019 5:00:30 PM ******/
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
/****** Object:  Table [dbo].[Brands]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[BrandID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 4/23/2019 5:00:31 PM ******/
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
/****** Object:  Table [dbo].[CategoryNames]    Script Date: 4/23/2019 5:00:31 PM ******/
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
/****** Object:  Table [dbo].[IdentityRole]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRole](
	[Id] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_IdentityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityRoleClaim`1]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRoleClaim`1](
	[Id] [int] NOT NULL,
	[RoleId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserClaim`1]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserClaim`1](
	[Id] [int] NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserLogin`1]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserLogin`1](
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserRole`1]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserRole`1](
	[UserId] [nvarchar](max) NULL,
	[RoleId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IdentityUserToken`1]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserToken`1](
	[UserId] [nvarchar](max) NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Languages]    Script Date: 4/23/2019 5:00:31 PM ******/
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
/****** Object:  Table [dbo].[ProductInfo]    Script Date: 4/23/2019 5:00:31 PM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[BrandID] [int] NOT NULL,
	[Size] [int] NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([BrandID], [Name]) VALUES (1, N'Coca Cola')
INSERT [dbo].[Brands] ([BrandID], [Name]) VALUES (2, N'Pepsi')
SET IDENTITY_INSERT [dbo].[Brands] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (21, NULL)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (22, NULL)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (23, 21)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (24, 22)
INSERT [dbo].[Categories] ([CategoryID], [ParentID]) VALUES (25, NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (21, N'en', N'Food')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (21, N'it', N'Cibo')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (22, N'en', N'Drinks')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (22, N'it', N'Bevanda')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (23, N'en', N'Snacks')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (23, N'it', N'Spuntini')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (24, N'en', N'Alcoholic')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (24, N'it', N'Alcolizzata')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (25, N'en', N'Kids')
INSERT [dbo].[CategoryNames] ([CategoryID], [LanguageID], [Name]) VALUES (25, N'it', N'Bambine')
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'en', N'English')
INSERT [dbo].[Languages] ([LanguageID], [Name]) VALUES (N'it', N'Italian')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (19, N'en', N'asdasutyyu edit', N'asdasdasdasd')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (19, N'it', N'asdasdhgjhb', N'asdasd')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (21, N'en', N'asdasd', N'asd')
INSERT [dbo].[ProductInfo] ([ProductID], [LanguageID], [Name], [Description]) VALUES (21, N'it', N'asdasd', N'asdasd')
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price]) VALUES (19, 23, 2, 0, 2.0099999904632568)
INSERT [dbo].[Products] ([ProductID], [CategoryID], [BrandID], [Size], [Price]) VALUES (21, 24, 2, 0, 1.559999942779541)
SET IDENTITY_INSERT [dbo].[Products] OFF
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b56cc577-929b-4402-8a4e-fc904316f074', N'admin', N'ADMIN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAELzyq0RkzixlSF5dT7vzD1l2ZJS/C0uEJtshCJmBXIa2iet+HIAJyK5VRjCYGc791A==', N'YS5WKLTEYCTWK7OUZLCUZMPHG2DGWXPL', N'5ed9e67d-cc34-46f4-91e6-1c52578dbf93', NULL, 0, 0, NULL, 1, 0)
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
/****** Object:  StoredProcedure [dbo].[GetCategoryBrands]    Script Date: 4/23/2019 5:00:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetCategoryBrands]
	@categoryID [int] = 0
AS
SELECT DISTINCT Brands.* FROM dbo.Products
	LEFT JOIN dbo.Brands
		ON Brands.BrandID = Products.BrandID
WHERE Products.CategoryID = @categoryID

GO
USE [master]
GO
ALTER DATABASE [MiniBar] SET  READ_WRITE 
GO
